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
        [Display(Name = "Post Title")]
        public int CategoryPostId { get; set; }
        [Display(Name = "Blog User")]
        public string BlogUserId { get; set; }

        //Properties
        [Required]
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Updated { get; set; }

        //Properties for the Moderator of Comments
        [DataType(DataType.Date)]
        public DateTime? Moderated { get; set; }
        [Display(Name = "Moderated?")]
        public bool HasBeenModerated { get; set; }
        [Display(Name = "Reason")]
        public string ModeratedReason { get; set; }
        [Display(Name = "Moderated Content")]
        public string ModeratedBody { get; set; }

        //Navigational properties
        [Display(Name = "Post Title")]
        public virtual CategoryPost CategoryPost { get; set; }
        [Display(Name = "Blog User")]
        public virtual BlogUser BlogUser { get; set; }
    }
}
