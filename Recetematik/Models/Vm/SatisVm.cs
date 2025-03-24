namespace Recetematik.Models.Vm
{
    public class SatisVm
    {
        public TblSatis Satis { get; set; }
       
        public TblCari Cari { get; set; }
        public TblUrun Urun { get; set; }
        public TblHammadde Hammadde { get; set; }
        public List<TblHammadde> HammaddeListe { get; set; }
        public List<TblUrunbilgi> UrunBilgi { get; set; }
        public List<TblUrun> UrunListe { get; set; }
        public List<TblCari> CariListe { get; set; }
        public List<TblSatis> SatisListe { get; set; }

    }
}
