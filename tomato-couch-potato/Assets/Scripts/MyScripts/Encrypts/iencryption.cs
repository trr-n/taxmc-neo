namespace trrne.Secret
{
    public interface IEncryption
    {
        byte[] En(byte[] src);
        byte[] En(string src);
        byte[] De(byte[] src);
        string De2Str(byte[] src);
    }

    public enum EncryptionTypes
    {
        RSA,
        XOR,
        Rijndael
    }
}