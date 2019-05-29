using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.Domain
{
    public interface IGameRepository
    {
        GameEntity FindById(Guid id);
        void Insert(GameEntity gameEntity);
        void Update(GameEntity gameEntity);
        void Upsert(Guid id, GameEntity gameEntity);
        GameEntity FindNotFullGame();
    }
}
