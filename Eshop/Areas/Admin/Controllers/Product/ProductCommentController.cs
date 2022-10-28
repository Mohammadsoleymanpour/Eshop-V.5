using Application.Interface;
using Application.Security;
using Domain.ViewModels.Product_Comment;
using Eshop.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.Admin.Controllers.Product
{
    [CheckPermission(Permissions.ProductComments)]
    public class ProductCommentController : AdminBaseController
    {
        #region Injections

        private IProductService _productService;
        private ILoggerService _loggerService;

        public ProductCommentController(IProductService productService, ILoggerService loggerService)
        {
            _productService = productService;
            _loggerService = loggerService;
        }



        #endregion


        [Route("/Comments/{id}")]

        public async Task<IActionResult> Index(int id,FilterProductCommentsViewModel filter)
        {
            filter.ProductId = id;
            var model =await _productService.GetAllCommentOfProductForAdmin(filter);

            return View(model);
        }


        [Route("Comment/{id}")]
        public async Task<IActionResult> Comment(int id)
        {
            var model = await _productService.GetCommentForAdmin(id);

            return View("CommentManager",model);
        }


        [HttpGet("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int commentIdForDel)
        {
            if (!ModelState.IsValid)
                return View("CommentManager", commentIdForDel);

            var delete = await _productService.DeleteCommentByIdFromAdmin(commentIdForDel);

            if (!delete)
            {
                ModelState.AddModelError("Answer","عملیات با مشکل مواجه شد");
                return View("CommentManager", commentIdForDel);
            }
            await _loggerService.AddLog(commentIdForDel, User.GetUserId(), "حذف کامنت");
            return Redirect("Admin/Product");

        }

        [HttpPost("Answer")]
        public async Task<IActionResult> Answer(AnswerCommentViewModel answer)
        {
            if (!ModelState.IsValid)
                return View("CommentManager", answer);

            var res = await _productService.AnswerCommentFromAdmin(answer);



            if (!res)
            {
                ModelState.AddModelError("Answer", "عملیات با مشکل مواجه شد");
                return View("CommentManager", answer);
            }
            await _loggerService.AddLog((int)answer.CommentId, User.GetUserId(), "پاسخ به کامنت");
            return Redirect("Index/" + answer.ProductId);
        }
    }
}
