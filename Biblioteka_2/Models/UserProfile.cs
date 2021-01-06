namespace Biblioteka_2
{
    public class UserProfile : IUserProfile
    {
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
        public string _password { get; set; }
        public string _login { get; set; }

        public UserProfile() { }

        public UserProfile(string FirstName, string LastName, string UserPassword, string login)
        {
            _FirstName = FirstName;
            _LastName = LastName;
            _password = UserPassword;
            _login = login;
        }
    }
}