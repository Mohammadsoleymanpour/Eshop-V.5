using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.Models.UserAgg;
using Domain.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<FilterUserOrdersForAdmin> GetAllOrdersOfUserById(FilterUserOrdersForAdmin filter)
        {
            var query = _context.Orders.Where(o => o.UserId == filter.UserId)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.TrackingNumber) && query.Any(o => o.TrackingNumber != null))
            {
                query = query.Where(o => o.TrackingNumber.Contains(filter.TrackingNumber));
            }

            #endregion

            #region Status

            switch (filter.Status)
            {
                case FilterStatusOrderFinally.All:
                    break;
                case FilterStatusOrderFinally.Finalized:
                    {
                        query = query.Where(o => o.IsFinally == true);
                        break;
                    }
                case FilterStatusOrderFinally.NotFinalized:
                    {
                        query = query.Where(o => o.IsFinally == false);
                        break;
                    }
            }

            #endregion

            await filter.Paging(query);
            return filter;
        }

        public async Task<List<Order>> GetAllFinalizedOrders(int take)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .Where(o => o.IsFinally)
                .OrderByDescending(p => p.CreatDate)
                .Take(take)
                .ToListAsync();
        }


        public async Task<int> AddOrderFromUser(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            var res = await _context.Orders.Include(c => c.OrderDetails)
                .ThenInclude(c => c.Product).ThenInclude(c => c.ProductGalleries)
                .Include(c => c.OrderDetails).ThenInclude(c => c.Product)
                .ThenInclude(c => c.productPrices).ThenInclude(c => c.productSelectedFeatures)
                .ThenInclude(c => c.Feature).ThenInclude(c => c.FeatureValues)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsFinally);
            return res;
        }

        public async Task<int> GetTotalPrice(int orderId)
        {
            var order = await GetOrderById(orderId);

            // var price = order.OrderDetails.Where(c => c.OrderId == orderId).Sum(c => c.Price);

            var count = order.OrderDetails.Where(c => c.OrderId == orderId).Sum(c => c.Count);


            var res = _context.Orders.FirstOrDefault(c => c.Id == orderId).OrderDetails.Sum(c => c.Price);
            return res * count;
        }

        public async Task<List<OrderDetail>> GetListOrderDetailsByOrderId(int orderId)
        {
            return await _context.OrderDetails.Where(c => c.OrderId == orderId).ToListAsync();

        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(c => c.OrderDetails).FirstOrDefaultAsync(c => c.Id == orderId && !c.IsFinally);

        }

        public async Task<OrderDetail> GetOrderDetailByOrderId(int orderId, int productId, int? productPriceId)
        {
            if (productPriceId != null)
            {

                return await _context.OrderDetails.FirstOrDefaultAsync(
                    c => c.OrderId == orderId && c.ProductId == productId && c.ProductPriceId == productPriceId);
            }
            else
            {
                return await _context.OrderDetails.FirstOrDefaultAsync(
                    c => c.OrderId == orderId && c.ProductId == productId);
            }
        }

        public async Task<int> AddOrderDetialFromUser(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail.Id;
        }

        public async Task<bool> EditOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> AddOrderDetailProductFeature(OrderDetailProductFeature model)
        {
            _context.OrderDetailProductFeatures.Add(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> UpdateOrderDetail(OrderDetail model)
        {



            _context.OrderDetails.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId)
        {
            return await _context.OrderDetails.FindAsync(orderDetailId);
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            var res = await _context.OrderDetails
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Order)
                .ThenInclude(o => o.User)
                .Include(o => o.Product)
                .ThenInclude(p => p.ProductGalleries)
                .ToListAsync();

            return res;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int[]> GetWeeklySalesOrderForChart()
        {
            int[] weekDays = { 0, 1, 2, 3, 4, 5, 6 };
            int[] res = new int[7];
            List<int> result = new List<int>(6);

            var orders = _context.Orders.OrderByDescending(o => o.FinalizedDate)
                .Include(o => o.OrderDetails)
                .Where(o => o.IsFinally)
                .AsQueryable();
            foreach (int item in weekDays)
            {
                int itemData = 0;
                var res1 = orders.Where(o => o.FinalizedDate == DateTime.Now.Date.AddDays(-item));
                foreach (var order in res1)
                {
                    itemData += order.OrderDetails.Sum(o => o.Price);
                }

                res[item] = itemData;
            }
            return res;
        }

        public async Task<Tuple<DiscountUseType, int>> UseDiscount(string code, int userId)
        {
            var discount = await _context.Discounts.SingleOrDefaultAsync(d => d.DiscountCode == code);

            if (discount == null)
                return Tuple.Create(DiscountUseType.NotFound, 0);

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return Tuple.Create(DiscountUseType.NotStartedDate, 0);

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return Tuple.Create(DiscountUseType.ExpiredDate, 0);

            if (discount.Useable != null && discount.Useable < 1)
                return Tuple.Create(DiscountUseType.Finished, 0);

            if (await _context.UserDiscountCodes.AnyAsync(u => u.DiscountId == discount.Id && u.UserId == userId))
                return Tuple.Create(DiscountUseType.UserUsed, 0);

            if (discount.Useable != null)
                discount.Useable -= 1;

            await UpdateDiscount(discount);

            await _context.AddAsync(new UserDiscountCode()
            {
                CreatDate = DateTime.Now,
                DiscountId = discount.Id,
                UserId = userId,
                IsDelete = false
            });

            await _context.SaveChangesAsync();

            return Tuple.Create(DiscountUseType.Success, discount.DicountPercent);


        }

        public async Task<bool> UpdateDiscount(Discount discount)
        {
            _context.Update(discount);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteDiscount(int discountId)
        {
            var discountDB = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);
            if (discountDB == null)
                throw new NullReferenceException();

             discountDB.IsDelete = true;

            return await UpdateDiscount(discountDB); 
        }

        public async Task<bool> AddDiscount(Discount discount)
        {
            await _context.AddAsync(discount);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<FilterDiscountViewModel> GetAllDiscountForAdmin(FilterDiscountViewModel filter)
        {
            var query = _context.Discounts.AsQueryable();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(d => d.DiscountCode.Contains(filter.Code));
            }
            if (filter.StartDate != null)
            {
                query = query.Where(d => d.StartDate < filter.StartDate);
            }
            if (filter.EndDate != null)
            {
                query = query.Where(d => d.EndDate > filter.EndDate);
            }

            #endregion

            #region Status

            var list = new List<CustonizedDiscount>();
            foreach (var discount in filter.Entities)
            {
                var custome = new CustonizedDiscount()
                {
                    Id = discount.Id,
                    CreatDate = discount.CreatDate,
                    DicountPercent = discount.DicountPercent,
                    DiscountCode = discount.DiscountCode,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate,
                    IsDelete = discount.IsDelete,
                    Useable = discount.Useable,
                    Status = (filter.StartDate != null && discount.StartDate < DateTime.Now) ? DiscountStatus.NotStartedDate
                            : (discount.EndDate != null && discount.EndDate >= DateTime.Now) ? DiscountStatus.ExpiredDate
                            : (discount.Useable != null && discount.Useable < 1) ? DiscountStatus.Finished : DiscountStatus.Active

                };
                list.Add(custome);
            };

            #endregion


            var quueryable = list.AsQueryable();
            await filter.Paging(quueryable);
            return filter;
        }

        public async Task<Discount> GetDiscountById(int discount)
        {
            return await _context.Discounts.SingleOrDefaultAsync(d => d.Id == discount);   
        }
    }
}

