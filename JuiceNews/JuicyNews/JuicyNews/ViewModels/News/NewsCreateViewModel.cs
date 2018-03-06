using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JuicyNews.ViewModels.News
{
    public class NewsCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field is Required")]
        [MinLength(5), MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field is Required")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime DateOfPublishing { get; set; }
    }
}