using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Infrastructure.Helper
{
    public static class ImageHelper
    {
        public static string ImageToBase64(string imagePath)
        {
            byte[] imageArrey=File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageArrey);
        }
    }
}
