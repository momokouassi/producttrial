namespace ProductTrial.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string username, string firstname, string email);
    }
}