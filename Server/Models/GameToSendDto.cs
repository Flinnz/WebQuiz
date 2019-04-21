using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain;

namespace Server.Models
{
    public class GameToSendDto
    {
        public Guid Guid {get;set;}
        public int PlayerCount {get;set;}
        public Guid HostPlayerGuid {get;set;}
        public Guid YourPlayerGuid {get;set;}
    }
}
