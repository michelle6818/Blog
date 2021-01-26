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
            public string Title { get; set; }
            public string Abstract { get; set; }
            public string Content { get; set; }
            public bool IsProductionReady { get; set; }

            //Programmatically derived properties
            [DataType(DataType.DateTime)]
            public DateTime Created { get; set; }
            [DataType(DataType.DateTime)]
            public DateTime? Updated { get; set; }
            public string Slug { get; set; }

            //Navigational Properties
            public virtual BlogCategory BlogCategory { get; set; }

            public virtual ICollection<Comment> Comments { get; set; } =
            new HashSet<Comment>();
        
        public virtual ICollection<Tag> Tags { get; set; }

    }
}
