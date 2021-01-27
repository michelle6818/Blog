using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogCategory
    {
        //keys section
        public int Id { get; set; }

        //Descriptive properties section
        [Required]
        [StringLength(50, ErrorMessage ="The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 10)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Updated { get; set; }

        //Navigation Section --info about the parent or children
        //As a Blog Category I am likely to have zero or more Category Post children
        public virtual ICollection<CategoryPost> CategoryPosts { get; set; } =
            new HashSet<CategoryPost>();
             
    }
}
