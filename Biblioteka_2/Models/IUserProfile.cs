namespace Biblioteka_2
{
    public interface IUserProfile
    {
        public string firstName { get; set; }
        public  string lastName { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}