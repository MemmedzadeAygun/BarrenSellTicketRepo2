using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Helper
{
    public static class HasHelper
    {
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA256()) 
            {
                byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
