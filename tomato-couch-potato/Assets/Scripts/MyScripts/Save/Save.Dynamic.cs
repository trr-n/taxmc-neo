using System;
using System.IO;

namespace trrne.Secret
{
    public sealed partial class Save
    {
        readonly string password, path;
        readonly object data;

        public Save(object data, string password, string path)
        {
            this.data = data;
            this.password = password;
            this.path = path;
        }

        public void Write() => Write(data, password, path);

        public object Read()
        {
            Read(out object read, password, path);
            return read;
        }

        public bool TryRead(out object read)
        {
            Read(out read, password, path);
            return read == null;
        }
    }
}