using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class PlantPlantType
    {
        public int ID { get; set; }

        public int PlantID { get; set; }
        public int PlantTypeID { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual PlantType PlantType { get; set; }
    }
}
