using System;
using System.Text;

namespace trrne.Secret
{
    public class Zatsu : IEncryption
    {
        readonly int pw;
        public int pws => pw;

        public Zatsu(string pw)
        {
            this.pw = BitConverter.ToInt32(Encoding.UTF8.GetBytes(pw));
        }

        public byte[] Encrypt(byte[] src) => BitConverter.GetBytes(BitConverter.ToInt32(src) + pw);
        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src) => BitConverter.GetBytes(BitConverter.ToInt32(src) - pw);
        public string DecryptToString(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}