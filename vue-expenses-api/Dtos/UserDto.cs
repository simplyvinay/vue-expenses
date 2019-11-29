using Newtonsoft.Json;

namespace vue_expenses_api.Dtos
{
    public class UserDto
    {
        protected UserDto()
        {
        }

        public UserDto(
            string email,
            string token,
            string refreshToken,
            bool useDarkMode)
        {
            Email = email;
            Token = token;
            RefreshToken = refreshToken;
            Theme = useDarkMode ? "dark" : "light";
        }

        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Theme { get; set; }
        
        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}