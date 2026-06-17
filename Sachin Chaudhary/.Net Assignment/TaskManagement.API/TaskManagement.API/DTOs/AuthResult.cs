namespace TaskManagement.API.DTOs
{
    public class AuthResult
    {
        public bool Success { get; set; }

        public  string Message { get; set; }

        public string? AccessToken { get; set; }

        public  string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
        public string Email { get; internal set; }
        public string UserName { get; internal set; }
        public string Role { get; internal set; }
        public List<string> Roles { get; internal set; }
    }
}
