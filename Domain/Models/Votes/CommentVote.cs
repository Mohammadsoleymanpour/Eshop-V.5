using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Product;
using Domain.Models.UserAgg;

namespace Domain.Models.Votes
{
    public class CommentVote : BaseEntity<int>
    {
        public bool Vote { get; set; }

        public int CommentId { get; set; }

        public int UserId { get; set; }

        #region Relations
        [ForeignKey("CommentId")]
        public ProductComment Comment { get; set; }

        [ForeignKey("UserId")] 
        public User User { get; set; }

        #endregion
    }
}
