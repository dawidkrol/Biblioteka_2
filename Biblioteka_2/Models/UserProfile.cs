namespace Biblioteka_2
{
    public class UserProfile : IUserProfile
    {
        public string _FirstName { get; }
        public string _LastName { get; }
        public string _password { get; }
        public string _login { get; }
        public UserProfile(string FirstName, string LastName, string UserPassword, string login)
        {
            _FirstName = FirstName;
            _LastName = LastName;
            _password = UserPassword;
            _login = login;
        }
    }
}