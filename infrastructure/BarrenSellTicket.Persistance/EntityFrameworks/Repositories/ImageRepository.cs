using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class ImageRepository : EfGenericRepository<Image>, IImageRepository
    {
       
        public ImageRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
        }


        //public async Task<string> SaveImageAsync(IFormFile imageFile)
        //{
        //    if (imageFile is null || imageFile.Length == 0)
        //    {
        //        throw new ArgumentException("Image file is not provided.");
        //    }

        //    var contentPath = _environment.ContentRootPath;
        //    var path = Path.Combine(contentPath, "Uploads");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    var ext = Path.GetExtension(imageFile.FileName);
        //    var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
        //    if (!allowedExtensions.Contains(ext))
        //    {
        //        throw new ArgumentException("Only .jpg, .png, .jpeg extensions are allowed.");
        //    }

        //    string uniqueString = Guid.NewGuid().ToString();
        //    var newFileName = uniqueString + ext;
        //    var fileWithPath = Path.Combine(path, newFileName);
        //    var stream = new FileStream(fileWithPath, FileMode.Create);
        //    await imageFile.CopyToAsync(stream);
        //    stream.Close();

        //    return newFileName;
        //}
    }
}
