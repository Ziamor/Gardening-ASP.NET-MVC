using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class PlantType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PlantPlantType> PlantPlantType { get; set; }
    }
}
