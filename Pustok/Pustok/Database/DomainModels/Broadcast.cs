using Pustok.Database.Abstracts;
using System;

namespace Pustok.Database.DomainModels
{
    public class Broadcast : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public Order UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
