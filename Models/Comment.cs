using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        //Keys
        public int Id { get; set; }
        public int CategoryPostId { get; set; }
        public string BlogUserId { get; set; }

        //Properties
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Updated { get; set; }

        //Properties for the Moderator of Comments
        public bool HasBeenModerated { get; set; }
        public string ModerationReason { get; set; }

        //Navigational properties
        public virtual CategoryPost CategoryPost { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
