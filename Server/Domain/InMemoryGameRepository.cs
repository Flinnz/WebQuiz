using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.Domain
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<Guid, GameEntity> gameEntities = new Dictionary<Guid, GameEntity>();

        public GameEntity FindById(Guid id)
        {
            return gameEntities.GetValueOrDefault(id);
        }

        public void Insert(GameEntity gameEntity)
        {
            gameEntities[gameEntity.Id] = gameEntity;
        }

        public void Update(GameEntity gameEntity)
        {
            gameEntities[gameEntity.Id] = gameEntity;
        }

        public void Upsert(Guid id, GameEntity gameEntity)
        {
            throw new NotImplementedException();
        }

        public GameEntity FindNotFullGame()
        {
            throw new NotImplementedException();
        }
    }
}
