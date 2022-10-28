using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Product;
using Domain.ViewModels.Shared;

namespace Domain.ViewModels.Product_Comment
{
    public class FilterProductCommentsViewModel:BasePaging<ProductComment>
    {
        public string SenderEmail { get; set; }
        public CommentStatus.CommentFilterStatus Status { get; set; }
        public int ProductId { get; set; }
    }


    public class CommentStatus
    {
        public enum CommentFilterStatus
        {
            [Display(Name = "همه")] All,
            [Display(Name = "پاسخ داده شده")] Answered,
            [Display(Name = "بدون پاسخ")] NotAnswered
        }
    }
}
