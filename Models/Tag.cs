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
        public int CategoryPostId { get; set; }

        //Descriptive properties
        public string Name { get; set; }

        //Navigational properties
        public virtual CategoryPost CategoryPost { get; set; }

        public virtual ICollection<CategoryPost> CategoryPosts { get; set; } =
            new HashSet<CategoryPost>();
    }
}
