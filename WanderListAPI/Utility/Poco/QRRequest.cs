using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanderListAPI.Utility.Poco
{
    public class QRRequest
    {
        public Guid QRCode { get; set; }
        public Guid UserId { get; set; }
    }
}
