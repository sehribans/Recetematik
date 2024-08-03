using System;
using System.Collections.Generic;

namespace Recetematik.Models
{
    public partial class TblUrun
    {
        public int Id { get; set; }
        public string? Urunadi { get; set; }
        public decimal? Fiyat { get; set; }
    }

    public class urunModel
    {
        public int Id { get; set; }
        public string? Urunadi { get; set; }
        public decimal? Fiyat { get; set; }
        public int? Adet { get; set; }

    }
}
