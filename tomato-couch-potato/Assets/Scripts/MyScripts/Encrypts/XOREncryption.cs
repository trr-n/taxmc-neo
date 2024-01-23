using System.Text;

namespace trrne.Secret
{
    public class XOREncryption : IEncryption
    {
        readonly byte key;

        public XOREncryption(byte key) => this.key = key;

        public byte[] Encrypt(byte[] src)
        {
            for (int i = 0; i < src.Length; ++i)
            {
                src[i] ^= key;
            }
            return src;
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            for (int i = 0; i < src.Length; ++i)
            {
                src[i] ^= key;
            }
            return src;
        }

        public string DecryptToString(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}
