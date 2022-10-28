using Application.Interface;
using Application.Security;
using Domain.Models;
using Domain.ViewModels.Product;
using Domain.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Eshop.Areas.Profile.Controllers
{
  
    public class HomeController : BaseProfileController
    {
        #region Injections

        private IUserService _userService;
        IProductService _productService;

        public HomeController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("/Profile/UserDetail")]
        public IActionResult UserDetail()
        {

            var res = _userService.GetUserByEmail(User.Identity.Name);
            var user = new UserForProfileDetailViewModel()
            {
                Gender = res.Gender,
                PhoneNumber = res.PhoneNumber,
                BirthDay = res.BirthDate,
                FirstName = res.FirstName,
                LastName = res.LastName
            };

            if (user.FirstName == null && user.LastName == null)
            {
                user.FirstName = "";
                user.LastName = "";
            }

            return View(user);
        }

        [HttpPost("Profile/UserDetail")]
        public IActionResult UserDetail(UserForProfileDetailViewModel userView)
        {
            if (!ModelState.IsValid) return View(userView);
            var edit = _userService.UpdateProfileUser(User.GetUserId(), userView);

            switch (edit)
            {
                case EditUserStatus.notFoundUser:
                    {
                        ViewBag.NotFound = true;
                        return View();
                    }
                   
                case EditUserStatus.success:
                    {
                        return Redirect("/Profile");
                    }
            }

            return Redirect("/Profile");


        }
        [Route("/Profile/Comment")]
        public async Task<IActionResult> Comments(CommentViewModel filter)
        {
            var comments = await _productService.GetCommentsByUserId(filter,User.GetUserId());
            return View(comments);
        }

        [Route("/Profile/Comment/Delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var res = await _productService.DeleteCommentFromUser(id);
            if (res==false)
            {
                return NotFound();
            }

            return Redirect("/Profile/Comment");
        }

        [Route("/Profile/FavoriteProduct")]
        public async Task<IActionResult> FavoriteProductUser(FavoriteProductViewModel model,int UserId)
        {
            UserId=User.GetUserId();
            return View( await _productService.GetFavoriteProductForAdmin(model,UserId));
        }
    }
}
