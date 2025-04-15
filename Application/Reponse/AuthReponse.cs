namespace Application.Reponse
{
    public class AuthResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } // JWT token (nếu có)
    }

}
