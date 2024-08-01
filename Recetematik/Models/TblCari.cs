using System;
using System.Collections.Generic;

namespace Recetematik.Models
{
    public partial class TblCari
    {
        public int Id { get; set; }
        public string? Unvan { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public decimal? Bakiye { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }
    }
}
