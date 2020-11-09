using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace IDisposableTraining
{
    public class Program
    {
        static void Main(string[] args)
        {
            RSA rsa = RSA.Create();

            string privateInfo = "Joao Campos";

            // Create a UnicodeEncoder to convert string -> byte[]
            var byteConverter = new UnicodeEncoding();
            
            // let's initialize our RSA algo with the private key
            var privateKey = File.ReadAllText("..\\..\\..\\Certificates\\private_key.xml"); // I'm at bin folder, i need to get to the root and continue
            rsa.FromXmlStringExtended(privateKey);

            var encodedInfo = byteConverter.GetBytes(privateInfo);
            rsa.Encrypt(encodedInfo, RSAEncryptionPadding.OaepSHA256);
        }
    }
}
