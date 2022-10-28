using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IVoteService
    {
        Task<int> AddProductVote(int productId, int userId, bool vote);
        Tuple<int, int> GetProductVote(int productId);
        Task<int> AddCommentVote(int CommentId, int userId, bool vote);

        Tuple<int, int> GetCommentVote(int CommentId);
        Product GetProductByCommentId(int commentId);
    }
}
