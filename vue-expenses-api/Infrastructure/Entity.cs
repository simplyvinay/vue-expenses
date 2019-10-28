using System;
using Newtonsoft.Json;

namespace vue_expenses_api.Infrastructure
{
    public interface IEntity
    {
        int Id { get; }
    }

    public class Entity : IEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}