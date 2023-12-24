using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace trrne.Secret
{
    public sealed partial class Save
    {
        public static void Write(
            object data,
            string password,
            string path,
            FileMode mode = FileMode.Create
        )
        {
            using FileStream stream = new(path, mode);
            IEncryption encrypt = new Rijndael(password);
            byte[] dataArr = encrypt.Encrypt(JsonUtility.ToJson(data));
            stream.Write(dataArr, 0, dataArr.Length);
        }

        public static bool Read<T>(
            out T read,
            string password,
            string path
        )
        {
            using (FileStream stream = new(path, FileMode.Open))
            {
                byte[] readArr = new byte[stream.Length];
                stream.Read(readArr, 0, (int)stream.Length);
                IEncryption decrypt = new Rijndael(password);
                read = JsonUtility.FromJson<T>(decrypt.Decrypt2String(readArr));
            }
            return read == null;
        }

        public static T Read<T>(string password, string path)
        {
            _ = Read(out T data, password, path);
            return data;
        }

        [Obsolete]
        public static void Write2(object data, string password, string path)
        {
            using FileStream stream = new(path, FileMode.Create);
            byte[] arr = new Rijndael(password).Encrypt(JsonSerializer.Serialize(data));
            stream.Write(arr, 0, arr.Length);
        }

        [Obsolete]
        public static bool Read2<T>(out T read, string password, string path)
        {
            using (FileStream stream = new(path, FileMode.Open))
            {
                byte[] arr = new byte[stream.Length];
                stream.Read(arr, 0, (int)stream.Length);
                read = JsonSerializer.Deserialize<T>(new Rijndael(password).Decrypt2String(arr));
            }
            return read == null;
        }

        [Obsolete]
        public static T Read2<T>(string password, string path)
        {
            _ = Read2(out T data, password, path);
            return data;
        }
    }
}
