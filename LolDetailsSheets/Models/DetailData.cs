using LolDetailsSheets.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LolDetailsSheets.Models
{
    public class DetailDataViewModel
    {
        [Required]
        public int Id { get; set; }
        public string CharName { get; set; }
        public string Type { get; set; }
        public string Skins { get; set; }
        public string Skin_Spotlight { get; set; }
    }

    public class DetailData
    {
        public List<DetailDataViewModel> Data = new List<DetailDataViewModel>();

    }


}
