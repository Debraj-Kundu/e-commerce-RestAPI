using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageEvents
{
    public class CheckInventoryResponse
    {
        public string OrderId { get; set; }
        public bool IsValid { get; set; }
    }
}
