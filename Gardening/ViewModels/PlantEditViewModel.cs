using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gardening.Models;

namespace Gardening.ViewModels
{
    public class PlantEditViewModel
    {
        public Plant Plant { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public PlantEditViewModel()
        {
            Tags = new List<TagViewModel>();
        }
    }
}
