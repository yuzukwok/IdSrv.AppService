using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib
{
   public class CertificateHelper
    {

        public static X509Certificate2 Load(string filename,string pwd)
        {
           
            using (var stream = File.OpenRead(filename))
            {
               
                return new X509Certificate2(ReadStream(stream), pwd);
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
