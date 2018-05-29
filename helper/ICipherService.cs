namespace rmc.helper
{
    public interface ICipherService
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}