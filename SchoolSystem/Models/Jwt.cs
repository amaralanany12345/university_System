namespace SchoolSystem.Models
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int LifeTime { get; set; }
        public string SigningKey { get; set; }
    }
}
