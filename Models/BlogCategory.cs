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
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        //Navigation Section --info about the parent or children
        //As a Blog Category I am likely to have zero or more Category Post children
        public virtual ICollection<CategoryPost> CategoryPosts { get; set; } =
            new HashSet<CategoryPost>();
             
    }
}
