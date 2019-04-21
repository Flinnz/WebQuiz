using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.Models
{
    public class GameToSendDto
    {
        public Guid Guid;
        public int PlayerCount;
        public Guid HostPlayerGuid;
        public Guid YourPlayerGuid;
    }
}
