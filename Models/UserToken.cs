namespace vueChain.Models
{
    public class UserToken
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime created_at { get; set; }
        public DateTime expires_at { get; set; }
    }
}