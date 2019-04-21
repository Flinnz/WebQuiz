using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class PlayerEntity
    {
        public Guid Guid { get; }
        public int Score { get; }
        public PlayerEntity()
        {
            this.Guid = Guid.NewGuid();
            this.Score = 0;
        }
    }
}
