using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Product_Comment
{
    public class AnswerCommentViewModel
    {
        public int CommentId { get; set; }
        public string UserEmail { get; set; }
        public string? UserComment { get; set; }
        public string? Answer { get; set; }
        public int SenderId { get; set; }
        public int? ParentId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }



    }
}
