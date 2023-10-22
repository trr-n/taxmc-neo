// 学校提供
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Chickenen.Pancreas
{
    public sealed class Rijndael : IEncryption
    {
        readonly string password;
        readonly (int bufferKey, int block, int key) size;

        public Rijndael(string password, int bufferKey = 32, int blockSize = 256, int keySize = 256)
        {
            this.password = password;
            size.bufferKey = bufferKey;
            size.block = blockSize;
            size.key = keySize;
        }

        public byte[] Encrypt(byte[] src)
        {
            RijndaelManaged managed = new()
            {
                BlockSize = size.block,
                KeySize = size.key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            Rfc2898DeriveBytes deriveBytes = new(password, size.bufferKey);
            byte[] salt = deriveBytes.Salt;
            managed.Key = deriveBytes.GetBytes(size.bufferKey);
            managed.GenerateIV();

            using (ICryptoTransform encrypt = managed.CreateEncryptor(managed.Key, managed.IV))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                List<byte> compile = new(salt);
                compile.AddRange(managed.IV);
                compile.AddRange(dest);
                return compile.ToArray();
            }
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            RijndaelManaged managed = new()
            {
                BlockSize = size.block,
                KeySize = size.key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            List<byte> compile = new(src);
            List<byte> salt = compile.GetRange(0, size.bufferKey);
            managed.IV = compile.GetRange(size.bufferKey, size.bufferKey).ToArray();

            Rfc2898DeriveBytes rfc = new(password, salt.ToArray());
            managed.Key = rfc.GetBytes(size.bufferKey);

            using ICryptoTransform decrypt = managed.CreateDecryptor(managed.Key, managed.IV);
            int index = size.bufferKey * 2, count = compile.Count - (size.bufferKey * 2);
            byte[] plain = compile.GetRange(index, count).ToArray();
            return decrypt.TransformFinalBlock(plain, 0, plain.Length);
        }

        public string Decrypt2String(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}

// https://www.tohoho-web.com/ex/crypt.html
// CBC https://ja.wikipedia.org/wiki/%E6%9A%97%E5%8F%B7%E5%88%A9%E7%94%A8%E3%83%A2%E3%83%BC%E3%83%89#Cipher_Block_Chaining_(CBC)
// PKCS7 https://www.mtioutput.com/entry/2019/01/08/152559
// salt https://ja.wikipedia.org/wiki/%E3%82%BD%E3%83%AB%E3%83%88_(%E6%9A%97%E5%8F%B7)