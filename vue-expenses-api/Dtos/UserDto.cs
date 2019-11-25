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
            //UseDarkMode = useDarkMode;
        }

        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool UseDarkMode => true;

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}