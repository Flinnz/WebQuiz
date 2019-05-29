using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public class MongoPlayerRepository : IPlayerRepository
    {
        public PlayerEntity FindById(Guid id)
        {

            throw new NotImplementedException();
        }

        public void Insert(PlayerEntity player)
        {
            throw new NotImplementedException();
        }

        public void Update(PlayerEntity player)
        {
            throw new NotImplementedException();
        }
    }
}
