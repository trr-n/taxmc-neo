using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace trrne.Pancreas
{
    [Obsolete]
    public class AES : IEncryption
    {
        readonly string password;
        readonly (int bufferKey, int block, int key) size;

        public AES(string password, int buffer = 32, int block = 256, int key = 256)
        {
            this.password = password;
            size.bufferKey = buffer;
            size.block = block;
            size.key = key;
        }

        public byte[] Encrypt(byte[] src)
        {
            AesManaged managed = new()
            {
                BlockSize = size.block,
                KeySize = size.key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            Rfc2898DeriveBytes rfc = new(password, size.bufferKey);
            byte[] salt = rfc.Salt;
            managed.Key = rfc.GetBytes(size.bufferKey);
            managed.GenerateIV();

            using var encrypt = managed.CreateEncryptor(managed.Key, managed.IV);
            byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
            List<byte> bytes = new(salt);
            bytes.AddRange(managed.IV);
            bytes.AddRange(dest);
            return bytes.ToArray();
        }

        public byte[] Encrypt(string src) => Encrypt(Encoding.UTF8.GetBytes(src));

        public byte[] Decrypt(byte[] src)
        {
            AesManaged managed = new()
            {
                BlockSize = size.block,
                KeySize = size.key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            List<byte> bytes = new(src), salt = bytes.GetRange(0, size.bufferKey);
            managed.IV = bytes.GetRange(size.bufferKey, size.bufferKey).ToArray();

            Rfc2898DeriveBytes rfc = new(password, salt.ToArray());
            managed.Key = rfc.GetBytes(size.bufferKey);

            using var decrypt = managed.CreateDecryptor(managed.Key, managed.IV);
            int index = size.bufferKey * 2, count = bytes.Count - (size.bufferKey * 2);
            byte[] plain = bytes.GetRange(index, count).ToArray();
            return decrypt.TransformFinalBlock(plain, 0, plain.Length);
        }

        public string Decrypt2String(byte[] src) => Encoding.UTF8.GetString(Decrypt(src));
    }
}