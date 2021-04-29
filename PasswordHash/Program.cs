using Site.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHash
{
    class Program
    {
        static void Main(string[] args)
        {
            var md5 = "PutinHuylo".GetMD5Hash();
        }
    }
}
