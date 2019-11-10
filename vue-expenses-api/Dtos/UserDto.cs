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
            string useDarkMode)
        {
            Email = email;
            Token = token;
            UseDarkMode = useDarkMode;
        }

        public string Email { get; set; }
        public string Token { get; set; }
        public string UseDarkMode { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}