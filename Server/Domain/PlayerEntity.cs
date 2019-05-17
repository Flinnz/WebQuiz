using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class PlayerEntity
    {
        public Guid Id { get; }
        public string ConnectionId { get; set; }
        public int Score { get; set; }
        public Guid GameId { get; set; }
        public PlayerEntity(string id)
        {
            this.Id = Guid.NewGuid();
            this.ConnectionId = id;
            this.Score = 0;
        }

        public PlayerEntity(string id, Guid gameId)
        {
            this.Id = Guid.NewGuid();
            this.ConnectionId = id;
            this.Score = 0;
            this.GameId = gameId;
        }
    }
}
