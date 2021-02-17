using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForBHHC.Data.Entities
{
    public class Reason
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public StoreUser User { get; set; }
    }
}
