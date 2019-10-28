using Newtonsoft.Json;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Domain
{
    public class User : Entity
    {
        public User(
            string userName,
            string email,
            byte[] hash,
            byte[] salt)
        {
            UserName = userName;
            Email = email;
            Hash = hash;
            Salt = salt;
        }

        public string UserName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}