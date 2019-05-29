using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Domain
{
    public class PlayerEntity
    {
        [BsonElement]
        public Guid Id { get; }
        public string ConnectionId { get; set; }
        public int Score { get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
        public PlayerEntity(string connectionId)
        {
            this.Id = Guid.NewGuid();
            this.ConnectionId = connectionId;
            this.Score = 0;
        }

        public PlayerEntity(string connectionId, Guid gameId)
        {
            this.Id = Guid.NewGuid();
            this.ConnectionId = connectionId;
            this.Score = 0;
            this.GameId = gameId;
        }
        [BsonConstructor]
        public PlayerEntity(Guid id, string connectionId, int score, Guid gameId, string name, string profilePictureUrl)
        {
            this.Id = id;
            this.ConnectionId = connectionId;
            this.Score = score;
            this.GameId = gameId;
            this.Name = name;
            this.ProfilePictureUrl = profilePictureUrl;
        }
    }
}
