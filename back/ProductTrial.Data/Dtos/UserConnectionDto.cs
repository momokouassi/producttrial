namespace ProductTrial.Data.Dtos
{
    public class UserConnectionDto
    {
        public UserConnectionDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}