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

        public byte[] En(byte[] src) => rsa.Encrypt(src, padding);
        public byte[] En(string src) => rsa.Encrypt(Encoding.UTF8.GetBytes(src), padding);

        public byte[] De(byte[] src) => rsa.Decrypt(src, padding);
        public string De2Str(byte[] src) => Encoding.UTF8.GetString(rsa.Decrypt(src, padding));
    }
}