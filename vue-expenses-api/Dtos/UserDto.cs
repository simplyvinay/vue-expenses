using Newtonsoft.Json;

namespace vue_expenses_api.Dtos
{
    public class UserDto
    {
        protected UserDto()
        {
        }

        public UserDto(
            string firstName,
            string lastName,
            string fullName,
            string systemName,
            string email,
            string token,
            string refreshToken,
            bool useDarkMode)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            SystemName = systemName;
            Email = email;
            Token = token;
            RefreshToken = refreshToken;
            UseDarkMode = useDarkMode;
            Theme = useDarkMode ? "dark" : "light";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string SystemName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool UseDarkMode { get; set; }
        public string Theme { get; set; }
        
        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}