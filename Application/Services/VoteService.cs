using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Product;
using SQLitePCL;

namespace Application.Services
{
    public class VoteService:IVoteService
    {
        private IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<int> AddProductVote(int productId, int userId, bool vote)
        {
            return await _voteRepository.AddProductVote(productId, userId, vote);
        }

        public Tuple<int, int> GetProductVote(int productId)
        {
            return _voteRepository.GetProductVote(productId);
        }

        public async Task<int> AddCommentVote(int CommentId, int userId, bool vote)
        {
          return  await _voteRepository.AddCommentVote(CommentId, userId, vote);
        }

        public  Tuple<int, int> GetCommentVote(int CommentId)
        {
            return  _voteRepository.GetCommentVote(CommentId);
        }

        public Product GetProductByCommentId(int commentId)
        {
            return _voteRepository.GetProductByCommentId(commentId);
        }
    }
}
