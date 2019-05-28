using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Domain
{
    public interface IPlayerRepository
    {
        PlayerEntity FindById(Guid id);
        void Insert(PlayerEntity player);
        void Update(PlayerEntity player);
    }
}
