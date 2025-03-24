using System;
using System.Collections.Generic;

namespace Recetematik.Models
{
    public partial class TblSatis
    {
        public int Id { get; set; }
        public int? CariId { get; set; }
        public int? UrunId { get; set; }
        public int? Miktar { get; set; }
        public decimal? Maliyet {  get; set; }
        public decimal? Fiyat {  get; set; }
        public DateTime? Tarih { get; set; }
    }
}
