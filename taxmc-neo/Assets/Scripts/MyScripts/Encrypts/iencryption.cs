namespace trrne.Appendix
{
    public interface IEncryption
    {
        byte[] Encrypt(byte[] src);
        byte[] Encrypt(string src);
        byte[] Decrypt(byte[] src);
        string DecryptToString(byte[] src);
    }
}