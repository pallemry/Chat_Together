namespace TestSocket
{
    public class User
    {
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool HasAdminPrivileges { get; set; }
    }
}