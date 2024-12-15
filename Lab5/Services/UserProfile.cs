namespace Lab5.Services
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> user_metadata { get; set; }
    }
}
