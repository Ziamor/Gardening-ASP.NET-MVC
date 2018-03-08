using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.Models
{
    public class Plan
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Planting Date")]
        public DateTime PlantingTime;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Late Plant Date")]
        public DateTime LatePlantTime;

        public int PlantID { get; set; }
        public int HardinessZoneID { get; set; }

        public virtual Plant Plant { get; set; }
        public virtual HardinessZone HardinessZone { get; set; }
    }
}
