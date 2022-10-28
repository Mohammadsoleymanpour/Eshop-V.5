using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext;
using Domain.Interfaces;
using Domain.Models.Product;
using Domain.Models.Votes;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> AddProductVote(int productId, int userId, bool vote)
        {

            var userVote = _context.ProductVotes.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (userVote!=null)
            {
                userVote.Vote = vote;
            }
            else
            {
                userVote = new ProductVotes()
                {
                    CreatDate = DateTime.Now,
                    IsDelete = false,
                    ProductId = productId,
                    UserId = userId,
                    Vote = vote,
                };
              await  _context.ProductVotes.AddAsync(userVote);
            }

            await _context.SaveChangesAsync();
            return userVote.Id;
        }

        public Tuple<int, int> GetProductVote(int productId)
        {
            var res = _context.ProductVotes.Where(c => c.ProductId == productId).Select(c => c.Vote).ToList();

            return Tuple.Create(res.Count(c => c == true), res.Count(c => c == false));
        }

        public async Task<int> AddCommentVote(int CommentId, int userId, bool vote)
        {

            var userVote = _context.CommentVotes.FirstOrDefault(c => c.CommentId == CommentId && c.UserId == userId);

            if (userVote != null)
            {
                userVote.Vote = vote;
            }
            else
            {
                userVote = new CommentVote()
                {
                    CreatDate = DateTime.Now,
                    IsDelete = false,
                    CommentId = CommentId,
                    UserId = userId,
                    Vote = vote,
                };
                await _context.CommentVotes.AddAsync(userVote);
            }

            await _context.SaveChangesAsync();
            return userVote.Id;
        }

        public Tuple<int, int> GetCommentVote(int CommentId)
        {
            var comments = _context.CommentVotes.Where(c=>c.Comment.ProductId==CommentId).Select(c=>c.Vote);
            //var res = comments.Select(c => c.CommentVotes).ToList();
            
            return Tuple.Create(comments.Count(c => c == true), comments.Count(c => c == false));
        }

        public Product GetProductByCommentId(int commentId)
        {
            return _context.ProductComments.Include(c=>c.Product).FirstOrDefault(c => c.Id == commentId).Product;
        }
    }
}
