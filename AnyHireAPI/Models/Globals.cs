using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AnyHireAPI.Models
{
    public static class Encrypt
    {
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inp = Encoding.ASCII.GetBytes(input);
                byte[] hash = md5.ComputeHash(inp);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}