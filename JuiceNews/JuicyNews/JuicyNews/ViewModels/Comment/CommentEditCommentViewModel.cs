using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.Comment
{
    public class CommentEditCommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int NewsId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Author { get; set; }
    }
}