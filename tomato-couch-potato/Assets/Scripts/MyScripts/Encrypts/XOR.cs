using System.Text;

namespace trrne.Secret
{
    public class XOR : IEncryption
    {
        readonly byte encryptKey;

        public XOR(byte key)
        {
            encryptKey = key;
        }

        public byte[] Encrypt(byte[] src)
        {
            byte[] dest = new byte[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dest[i] = src[i];
                dest[i] ^= encryptKey;
            }
            return dest;
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            byte[] dest = new byte[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                dest[i] = src[i];
                dest[i] ^= encryptKey;
            }
            return dest;
        }

        public string Decrypt2String(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}
