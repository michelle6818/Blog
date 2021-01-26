using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Tag
    {
        //keys
        public int Id { get; set; }
       
        //Descriptive properties
        public string Name { get; set; }

        //Navigational properties
        public virtual ICollection<CategoryPost> CategoryPosts { get; set; } =
            new HashSet<CategoryPost>();
    }
}
