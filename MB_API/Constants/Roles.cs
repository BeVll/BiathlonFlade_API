namespace MB_API.Constants
{
    public static class Roles
    {
        public static List<string> All = new()
        {
            Owner,
            Admin,
            Server,
            Helper,
            User
        };
        public const string Owner = "Owner";
        public const string Server = "Server";
        public const string Admin = "Admin";
        public const string Helper = "Helper";
        public const string User = "User";
    }
}
