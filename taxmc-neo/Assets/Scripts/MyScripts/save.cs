using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace trrne.utils
{
    public sealed class Save
    {
        public static void Write(object data, string password, string path, FileMode mode = FileMode.Create)
        {
            using FileStream stream = new(path, mode);
            IEncryption encrypt = new Rijndael(password);
            byte[] dataArr = encrypt.Encrypt(JsonUtility.ToJson(data));
            stream.Write(dataArr, 0, dataArr.Length);
        }

        public static void Read<T>(out T read, string password, string path)
        {
            using FileStream stream = new(path, FileMode.Open);
            byte[] readArr = new byte[stream.Length];
            stream.Read(readArr, 0, (int)stream.Length);
            IEncryption decrypt = new Rijndael(password);
            read = JsonUtility.FromJson<T>(decrypt.DecryptToString(readArr));
        }

        public static T Read<T>(string password, string path)
        {
            Read(out T data, password, path);
            return data;
        }

        [Obsolete]
        public static void Write2(object data, string password, string path)
        {
            using FileStream stream = new(path, FileMode.Create);
            Rijndael enc = new(password);
            byte[] arr = enc.Encrypt(JsonSerializer.Serialize(data));
            stream.Write(arr, 0, arr.Length);
        }

        [Obsolete]
        public static T Read2<T>(string password, string path)
        {
            using FileStream stream = new(path, FileMode.Open);
            byte[] arr = new byte[stream.Length];
            stream.Read(arr, 0, (int)stream.Length);
            Rijndael dec = new(password);
            return JsonSerializer.Deserialize<T>(dec.DecryptToString(arr));
        }
    }
}