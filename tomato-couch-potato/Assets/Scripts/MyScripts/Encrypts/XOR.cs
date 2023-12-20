using System.Text;

namespace trrne.Secret
{
    public class XOR : IEncryption
    {
        readonly byte key;

        public XOR(byte key) => this.key = key;

        public byte[] Encrypt(byte[] src)
        {
            byte[] dst = new byte[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dst[i] = src[i];
                dst[i] ^= key;
            }
            return dst;
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            byte[] dst = new byte[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dst[i] = src[i];
                dst[i] ^= key;
            }
            return dst;
        }

        public string Decrypt2String(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}
