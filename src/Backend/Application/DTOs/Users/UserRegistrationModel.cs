namespace Application.DTOs
{
    public class UserRegistrationModel
    {
        public UserRegistrationModel(string UserName, string Password)
        {
            this.Username = UserName;
            this.Password = Password;
        }
        public string Username { get; set; }
        public string Password { get; private set; }
    }
}