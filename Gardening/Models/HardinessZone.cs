using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class HardinessZone
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
