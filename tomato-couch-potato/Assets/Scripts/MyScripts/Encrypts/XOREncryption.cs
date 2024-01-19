using System.Text;

namespace trrne.Secret
{
    public class XOREncryption : IEncryption
    {
        readonly byte key;

        public XOREncryption(byte key) => this.key = key;

        public byte[] En(byte[] src)
        {
            for (int i = 0; i < src.Length; ++i)
            {
                src[i] ^= key;
            }
            return src;
        }

        public byte[] En(string src) => En(Encoding.UTF8.GetBytes(src));

        public byte[] De(byte[] src)
        {
            for (int i = 0; i < src.Length; ++i)
            {
                src[i] ^= key;
            }
            return src;
        }

        public string De2Str(byte[] src) => Encoding.UTF8.GetString(De(src));
    }
}
