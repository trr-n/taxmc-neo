namespace trrne.Teeth
{
    public interface IEncryption
    {
        byte[] Encrypt(byte[] src);
        byte[] Encrypt(string src);
        byte[] Decrypt(byte[] src);
        string Decrypt2String(byte[] src);
    }
}