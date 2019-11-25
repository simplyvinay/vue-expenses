using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Domain
{
    public class User : ArchivableEntity
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
        public bool UseDarkMode { get; set; }

        [JsonIgnore]
        public byte[] Hash { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        public void AddRefreshToken(
            string token,
            double daysToExpire = 2)
        {
            _refreshTokens.Add(
                new RefreshToken(
                    token,
                    DateTime.UtcNow.AddDays(daysToExpire),
                    this));
        }

        public void RemoveRefreshToken(
            string refreshToken)
        {
            _refreshTokens.Remove(_refreshTokens.First(t => t.Token == refreshToken));
        }

        public bool IsValidRefreshToken(
            string refreshToken)
        {
            return _refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
        }
    }
}