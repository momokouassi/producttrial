namespace ProductTrial.Data.Dtos
{
    public class UserDto
    {
        public UserDto(string username, string firstname, string email)
        {
            Username = username;
            Firstname = firstname;
            Email = email;
        }

        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Username { get; set; }
    }
}