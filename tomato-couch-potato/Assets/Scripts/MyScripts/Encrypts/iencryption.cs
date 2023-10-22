namespace Chickenen.Pancreas
{
    public interface IEncryption
    {
        string Decrypt2String(byte[] src);
        byte[] Decrypt(byte[] src);
        byte[] Encrypt(string src);
        byte[] Encrypt(byte[] src);
    }
}