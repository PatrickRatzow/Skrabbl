namespace Skrabbl.API.Services
{
    public interface ICryptographyService
    {
        byte[] CreateSalt();
        string GenerateHash(string input, byte[] salt);
        bool AreEqual(string plainTextInput, string hashedInput, byte[] salt);
    }
}