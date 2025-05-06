namespace ProductTrial.Data.Entities
{
    public class User
    {
        public User(string username, string firstname, string email,  string password)
        {
            Username = username;
            Firstname = firstname;
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}