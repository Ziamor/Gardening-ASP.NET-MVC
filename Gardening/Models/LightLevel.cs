using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class LightLevel
    {
        public int ID { get; set; }
        public int Text { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
