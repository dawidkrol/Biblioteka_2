namespace Biblioteka_2
{
    public class UserProfile : IUserProfile
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string login { get; set; }

        public UserProfile() { }

        public UserProfile(string FirstName, string LastName, string UserPassword, string Login)
        {
            firstName = FirstName;
            lastName = LastName;
            password = UserPassword;
            login = Login;
        }
    }
}