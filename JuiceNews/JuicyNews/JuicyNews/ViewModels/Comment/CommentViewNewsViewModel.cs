using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.Comment
{
    public class CommentViewNewsViewModel
    {
        public int Id { get; set; }
        
        public int NewsId { get; set; }
        
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required]
        public string Author { get; set; }
    }
}