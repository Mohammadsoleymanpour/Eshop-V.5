using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
namespace Eshop.ViewComponents
{
    public class GetVotes:ViewComponent
    {
        private IVoteService _voteService;

        public GetVotes(IVoteService voteService)
        {
            _voteService = voteService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var model = _voteService.GetProductVote(productId);
            return await Task.FromResult((IViewComponentResult)View("GetVotes", model));
        }
    }
}
