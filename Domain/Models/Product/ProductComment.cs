using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Enums;
using Domain.Models.UserAgg;
using Domain.Models.Votes;

namespace Domain.Models.Product
{
    public class ProductComment:BaseEntity<int>
    {
        public int? ParentId { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public int SenderId { get; set; }
        public CommentStatus.Status Status { get; set; }




        #region Relations
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("SenderId")]
        public User User { get; set; }
        
        [ForeignKey("ParentId")]
        public ProductComment ProductComments { get; set; }

        public List<CommentVote> CommentVotes { get; set; }

        #endregion

    }
}
