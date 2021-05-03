namespace Cryptography
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            using (RSA rsa = RSA.Create())
            {
                string privateInfo = "Joao Campos";

                // Create a UnicodeEncoder to convert string -> byte[]
                var byteConverter = new UnicodeEncoding();

                // let's initialize our RSA algo with the private key
                var publicKey = File.ReadAllText("..\\..\\..\\Certificates\\public_key.xml"); // I'm at bin folder, i need to get to the root and continue
                rsa.FromXmlStringExtended(publicKey);

                var encodedInfo = byteConverter.GetBytes(privateInfo);
                var encryptedInfo = rsa.Encrypt(encodedInfo, RSAEncryptionPadding.OaepSHA256);

                // let's decoded this...

                var privateKey = File.ReadAllText("..\\..\\..\\Certificates\\private_key.xml");
                using (RSA rsa2 = RSA.Create())
                {
                    rsa2.FromXmlStringExtended(privateKey);
                    var decryptedInfo = rsa2.Decrypt(encryptedInfo, RSAEncryptionPadding.OaepSHA256);
                    var privateInfo2 = byteConverter.GetString(decryptedInfo); 
                }
            }
        }
    }
}
