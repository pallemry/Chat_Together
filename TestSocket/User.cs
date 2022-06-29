namespace TestSocket
{
    public class User
    {
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public string Name { get; }
        public string Password { get; }
        public bool HasAdminPrivileges { get; set; }
    }
}