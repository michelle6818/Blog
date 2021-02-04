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
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Updated { get; set; }

        [Display(Name = "Choose An Image")]
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        //Navigation Section --info about the parent or children
        //As a Blog Category I am likely to have zero or more Category Post children
        public virtual ICollection<CategoryPost> CategoryPosts { get; set; } =
            new HashSet<CategoryPost>();
             
    }
}
