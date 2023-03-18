using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Application.Helpers
{
    public class RandomPW
    {
        public static string Generate(int c)
        {
            const string random = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder r = new StringBuilder();
            Random rnd = new Random();
            while (0 < c--)
            {
                r.Append(random[rnd.Next(random.Length)]);
            }
            return r.ToString();

        }

    }

}

