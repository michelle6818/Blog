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
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Updated { get; set; }

        //Properties for the Moderator of Comments
        [DataType(DataType.Date)]
        public DateTime? Moderated { get; set; }
        //public bool HasBeenModerated { get; set; }
        public string ModeratedReason { get; set; }
        public string ModeratedBody { get; set; }

        //Navigational properties
        public virtual CategoryPost CategoryPost { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
