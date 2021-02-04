using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.Services
{
    public interface IImageService
    {
        //I need to declare a method like EncodeFileAsync
        Task<byte[]>EncodeFileAsync(IFormFile formfile);

        string DecodeFile(byte[] imageData, string contentType);

        string RecordContentType(IFormFile formfile);
    }
}
