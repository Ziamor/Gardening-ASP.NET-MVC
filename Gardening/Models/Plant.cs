using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class Plant
    {
        public int ID { get; set; }

        [Required]
        public int HardinessZoneID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual HardinessZone HardinessZone { get; set; }
        public virtual ICollection<PlantPlantType> PlantPlantType { get; set; }
    }
}
