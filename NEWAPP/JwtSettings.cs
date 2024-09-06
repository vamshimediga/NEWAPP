namespace NEWAPP
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string RefreshSecretKey { get; set; }
        public int AccessTokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }

}
