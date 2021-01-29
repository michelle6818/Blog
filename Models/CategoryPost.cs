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
        [Display(Name = "Category Name")]
        public int BlogCategoryId { get; set; }

        //Main properties
        [Required]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        [Required]
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
        [Display(Name = "Category Name")]
        public virtual BlogCategory BlogCategory { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } =
        new HashSet<Comment>();
        public virtual ICollection<Tag> Tags { get; set; } =
        new HashSet<Tag>();

    }
}
