using Newtonsoft.Json;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Domain
{
    public class User : Entity
    {
        public User(
            string firstName,
            string lastName,
            string email,
            byte[] hash,
            byte[] salt)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{FirstName} {LastName}";
            Email = email;
            Hash = hash;
            Salt = salt;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }
}