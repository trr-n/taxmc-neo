using System.Security.Cryptography;
using System.Text;

namespace trrne.Secret
{
    public sealed class RSAEncryption : IEncryption
    {
        readonly RSA rsa;
        readonly RSAEncryptionPadding padding;
        public RSAEncryption(RSAEncryptionPadding padding, int key = 16)
        {
            this.padding = padding;
            rsa = RSA.Create(key);
        }

        public byte[] Encrypt(byte[] src) => rsa.Encrypt(src, padding);
        public byte[] Encrypt(string src) => rsa.Encrypt(Encoding.UTF8.GetBytes(src), padding);

        public byte[] Decrypt(byte[] src) => rsa.Decrypt(src, padding);
        public string DecryptToString(byte[] src) => Encoding.UTF8.GetString(rsa.Decrypt(src, padding));
    }
}