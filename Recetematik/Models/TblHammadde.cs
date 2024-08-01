using System;
using System.Collections.Generic;

namespace Recetematik.Models
{
    public partial class TblHammadde
    {
        public int Id { get; set; }
        public string? MaddeAd { get; set; }
        public int? Adet { get; set; }
        public decimal? Fiyat { get; set; }
        public int? BirimId { get; set; }
    }
}
