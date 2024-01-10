namespace MB_API.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Image { get; set; }
        public string National { get; set; }
        public bool IsLightTheme { get; set; }      
    }
}
