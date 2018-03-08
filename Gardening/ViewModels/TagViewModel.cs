using Gardening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardening.ViewModels
{
    public class TagViewModel
    {
        public int PlantTypeID { get; set; }
        public PlantType PlantType { get; set; }
        public bool IsSelected { get; set; }
    }
}
