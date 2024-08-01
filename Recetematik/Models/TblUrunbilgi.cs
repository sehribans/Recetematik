using System;
using System.Collections.Generic;

namespace Recetematik.Models
{
    public partial class TblUrunbilgi
    {
        public int Id { get; set; }
        public int? UrunId { get; set; }
        public int? HammaddeId { get; set; }
        public int? Miktar { get; set; }
    }
}
