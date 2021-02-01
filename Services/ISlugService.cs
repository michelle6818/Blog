using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;

namespace Blog.Services
{
    public interface ISlugService
    {
        //Method
        string URLFriendly(string title);

        bool IsUnique(ApplicationDbContext dbContext, string slug);
    }


}
