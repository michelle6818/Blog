using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Blog User image
        [Display(Name = "Choose Your Image")]
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }


        //What if I wanted to ask this class for the user's Full Name?
        //use 
        [NotMapped]
        public string FullName 
        { 
            get
            {
                return $"{FirstName} {LastName}";
            }
                
        }

        //Navigational properites
        public virtual ICollection<Comment> Comments { get; set; } =
            new HashSet<Comment>();



        //What about a more formal form....LastName, FirstName
        //[NotMapped]
        //public string FormalName 
        //{ 

        //    get
        //    {
        //        return $"{LastName}, {FirstName}";
        //    }

        //}

        


    }
}
