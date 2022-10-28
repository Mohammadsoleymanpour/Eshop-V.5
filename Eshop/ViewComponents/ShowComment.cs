using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using Application.Interface;
using Application.Sender;
using Domain.Models;
using Domain.Models.Enums;
using Domain.ViewModels.User;

namespace Eshop.ViewComponents
{
    public class ShowComment:ViewComponent
    {
        private IProductService _productService;
        IUserService _userService;
        private IVoteService _voteService;

        public ShowComment(IProductService productService, IUserService userService, IVoteService voteService)
        {
            _productService = productService;
            _userService = userService;
            _voteService = voteService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var comments = await _productService.GetAllCommentsByProductId(id);
            var votes = _voteService.GetCommentVote(id);
            return await Task.FromResult((IViewComponentResult) View("_ShowComment", Tuple.Create(comments,votes)));
        }
    }
}
