using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Order;
using Domain.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _repository;
        IProductRepository _productRepository;

        public OrderService(IOrderRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }


        public async Task<OrderDetailForAdminViewModel> GetOrderDetailForAdminById(int orderId)
        {
            var order = await _repository.GetOrderDetailByOrderId(orderId);
            int totalPrice = 0;
            foreach (var orderDetail in order)
            {
                totalPrice += orderDetail.Price * orderDetail.Count;
            }
            var res = new OrderDetailForAdminViewModel()
            {
                OrderDetails = order,
                TotalPriceOfOrder = totalPrice

            };

            return res;
        }

        public async Task<OrderListPartialViewModel> GetFinalizedOrdersForAdmin(int take)
        {
            var orders = await _repository.GetAllFinalizedOrders(take);

            var res = new OrderListPartialViewModel() { Orders = orders };

            return res;
        }



        public async Task<int> GetTotalPrice(int orderId)
        {
            return await _repository.GetTotalPrice(orderId);
        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            return await _repository.GetOrderByUserId(userId);
        }


        public async Task<int> AddOrderFromUser(int userId, int productId, int? productPriceId)
        {
            var getOrder = await _repository.GetOrderByUserId(userId);
            var product = await _productRepository.GetProductById(productId);
            var productPrice = product.productPrices.FirstOrDefault(c => c.Id == productPriceId);

            if (productId != null)
            {
                if (getOrder == null)
                {

                    var addOrder = new Order()
                    {
                        CreatDate = DateTime.Now,
                        IsDelete = false,
                        IsFinally = false,
                        UserId = userId,
                        OrderDetails = new List<OrderDetail>()
                        {
                            new OrderDetail()
                            {
                                IsDelete = false,
                                Count = 1,
                                CreatDate = DateTime.Now,
                                ProductId = product.Id,
                                Price = (productPriceId == null) ? product.Price : productPrice.Price,
                                ProductPriceId = (productPriceId != null ? productPriceId.Value : null)
                            }
                        }
                    };



                    if (productPriceId != null)
                    {
                        List<string> OrderPrdouctFeature = productPrice.productSelectedFeatures
                            .Where(c => c.ProductPriceId == productPriceId).Select(c => c.Feature.Title).ToList();


                        var OrderProductFeatureValue = productPrice.productSelectedFeatures
                            .Where(c => c.ProductPriceId == productPriceId).Select(c => c.featureValue.Value).ToList();

                        var productFeatureAndValues =
                            new Tuple<List<string>, List<string>>(OrderPrdouctFeature, OrderProductFeatureValue);

                        foreach (var item1 in productFeatureAndValues.Item1)
                        {
                            foreach (var item2 in productFeatureAndValues.Item2)
                            {
                                var addOrderProductFeature = new OrderDetailProductFeature()
                                {
                                    CreatDate = DateTime.Now,
                                    IsDelete = false,
                                    FeatureTitle = item1,
                                    FeatureValue = item2,
                                    OrderDetailId = addOrder.OrderDetails.FirstOrDefault(c => c.OrderId == addOrder.Id)
                                        .Id
                                };
                            }
                        }

                   
                    }
                    return await _repository.AddOrderFromUser(addOrder);

                }



                else
                {
                    var orderDetial = await _repository.GetOrderDetailByOrderId(getOrder.Id, product.Id,null);
                    if (orderDetial != null)
                    {
                        orderDetial.Count += 1;
                        var result = await _repository.EditOrderDetail(orderDetial);

                        if (productPriceId!=null)
                        {
                            var getorderDetial = await _repository.GetOrderDetailByOrderId(getOrder.Id, product.Id, productPrice.Id);

                            List<string> OrderPrdouctFeature = productPrice.productSelectedFeatures
                                .Where(c => c.ProductPriceId == productPriceId).Select(c => c.Feature.Title).ToList();


                            var OrderProductFeatureValue = productPrice.productSelectedFeatures
                                .Where(c => c.ProductPriceId == productPriceId).Select(c => c.featureValue.Value).ToList();

                            var productFeatureAndValues =
                                new Tuple<List<string>, List<string>>(OrderPrdouctFeature, OrderProductFeatureValue);

                            foreach (var item1 in productFeatureAndValues.Item1)
                            {
                                foreach (var item2 in productFeatureAndValues.Item2)
                                {
                                    var addOrderProductFeature = new OrderDetailProductFeature()
                                    {
                                        CreatDate = DateTime.Now,
                                        IsDelete = false,
                                        FeatureTitle = item1,
                                        FeatureValue = item2,
                                        OrderDetailId = getorderDetial.Id
                                    };
                                    int orderDetailProductFeatureId = await _repository.AddOrderDetailProductFeature(addOrderProductFeature);
                                }
                            }
                        }
                       
                    }
                    else
                    {
                        var addDetail = new OrderDetail()
                        {
                            Count = 1,
                            CreatDate = DateTime.Now,
                            IsDelete = false,
                            OrderId = getOrder.Id,
                            Price = (productPriceId == null) ? product.Price : productPrice.Price,
                            ProductId = product.Id,
                            ProductPriceId = productPriceId
                        };

                        var res = await _repository.AddOrderDetialFromUser(addDetail);
                        if (productPriceId != null)
                        {
                            List<string> OrderPrdouctFeature = productPrice.productSelectedFeatures
                                .Where(c => c.ProductPriceId == productPriceId).Select(c => c.Feature.Title).ToList();


                            var OrderProductFeatureValue = productPrice.productSelectedFeatures
                                .Where(c => c.ProductPriceId == productPriceId).Select(c => c.featureValue.Value).ToList();

                            var productFeatureAndValues =
                                new Tuple<List<string>, List<string>>(OrderPrdouctFeature, OrderProductFeatureValue);

                            foreach (var item1 in productFeatureAndValues.Item1)
                            {
                                foreach (var item2 in productFeatureAndValues.Item2)
                                {
                                    var addOrderProductFeature = new OrderDetailProductFeature()
                                    {
                                        CreatDate = DateTime.Now,
                                        IsDelete = false,
                                        FeatureTitle = item1,
                                        FeatureValue = item2,
                                        OrderDetailId = addDetail.Id
                                    };

                                    int orderDetailProductFeatureId =
                                        await _repository.AddOrderDetailProductFeature(addOrderProductFeature);
                                }
                            }
                        }

                        return res;
                    }

                }

            }
            return getOrder.Id;

        }

        public async Task<List<OrderDetail>> GetListOrderDetailsByOrderId(int orderId)
        {
            return await _repository.GetListOrderDetailsByOrderId(orderId);
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId)
        {
            var orderDetail = await _repository.GetOrderDetailById(orderDetailId);
            return orderDetail;
        }

        public async Task<Tuple<DiscountUseType, int>> UseDiscount(int userId, int totalPrice, string code)
        {
            var discount =  await _repository.UseDiscount(code, userId);

            if (discount.Item1!=DiscountUseType.Success)
            {
                return Tuple.Create(discount.Item1, totalPrice);
            }
            int percent =(totalPrice * discount.Item2) / 100;

            int newPrice = totalPrice - percent;

            return Tuple.Create(discount.Item1, newPrice);
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await _repository.GetOrderDetailByOrderId(orderId);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _repository.GetOrderById(orderId);
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            return await _repository.UpdateOrder(order);
        }

        public async Task<int> AddOrderDetailProductFeature(OrderDetailProductFeature model)
        {
            return await _repository.AddOrderDetailProductFeature(model);
        }

        public async Task<bool> UpdateOrderDetail(OrderDetail model)
        {
            return await _repository.UpdateOrderDetail(model);
        }

        public async Task<FilterUserOrdersForAdmin> GetAllOrdersOfUserForAdmin(FilterUserOrdersForAdmin filter)
        {
            return await _repository.GetAllOrdersOfUserById(filter);
        }

        public async Task<SalesOrderChartViewModel> GetSalesOrderChartForAdmin()
        {
            var res = new SalesOrderChartViewModel()
            {
                ChartData = await _repository.GetWeeklySalesOrderForChart()
            };
            return res;
        }

        public async Task<bool> DeleteDiscount(int discountId)
        {
            return await _repository.DeleteDiscount(discountId);
        }

        public async Task<bool> AddDiscount(AddDiscountViewModel discount)
        {
            return await _repository.AddDiscount(new Discount()
            {
                CreatDate = DateTime.Now,
                DicountPercent = discount.DicountPercent,
                DiscountCode = discount.DiscountCode,
                EndDate = discount.EndDate,
                IsDelete =false,
                StartDate = discount.StartDate,
                Useable = discount.Useable
            });
        }

        public async Task<bool> EditDiscount(EditDiscountViewModel editDiscount)
        {
            var discount = await _repository.GetDiscountById(editDiscount.Id);
            discount.StartDate = editDiscount.StartDate;
            discount.EndDate = editDiscount.EndDate;
            discount.Useable = editDiscount.Useable;
            discount.DiscountCode = editDiscount.DiscountCode;
            discount.DicountPercent = editDiscount.DicountPercent;

            return await _repository.UpdateDiscount(discount);
        }

        public async Task<FilterDiscountViewModel> GetAllDiscountForAdmin(FilterDiscountViewModel filter)
        {
            return await _repository.GetAllDiscountForAdmin(filter);
        }

        public async Task<EditDiscountViewModel> GetDiscountForAdmin(int discountId)
        {
            var discount = await _repository.GetDiscountById(discountId);
            var res =  new EditDiscountViewModel()
            {
                DicountPercent = discount.DicountPercent,
                Id = discount.Id,
                DiscountCode = discount.DiscountCode,
                EndDate = discount.EndDate ,
                StartDate = discount.StartDate ,
                Useable = discount.Useable, 
            };

            return res;
        }
    }
}
