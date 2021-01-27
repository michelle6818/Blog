using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class CategoryPost
    {

        //keys
        public int Id { get; set; }
        public int BlogCategoryId { get; set; }

        //Main properties
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Abstract { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 10)]
        public string Content { get; set; }
        [Display(Name = "Production Ready")]
        public bool IsProductionReady { get; set; }

        //Programmatically derived properties
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Updated { get; set; }
        public string Slug { get; set; }

        //Navigational Properties
        public virtual BlogCategory BlogCategory { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } =
        new HashSet<Comment>();
        public virtual ICollection<Tag> Tags { get; set; } =
        new HashSet<Tag>();

    }
}
