namespace TaskManagement.API.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public required string UserId { get; set; }

        public required string Token { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public bool IsActive { get; set; }





    }
}
