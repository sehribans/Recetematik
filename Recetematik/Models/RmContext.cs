using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Recetematik.Models
{
    public partial class RmContext : DbContext
    {
        public RmContext()
        {
        }

        public RmContext(DbContextOptions<RmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBirim> TblBirims { get; set; } = null!;
        public virtual DbSet<TblCari> TblCaris { get; set; } = null!;
        public virtual DbSet<TblHammadde> TblHammaddes { get; set; } = null!;
        public virtual DbSet<TblSatis> TblSatis { get; set; } = null!;
        public virtual DbSet<TblUrun> TblUruns { get; set; } = null!;
        public virtual DbSet<TblUrunbilgi> TblUrunbilgis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HLR82UJ\\SQLEXPRESS;Database=Recetematik;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBirim>(entity =>
            {
                entity.ToTable("TBL_BIRIM");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ad)
                    .HasMaxLength(100)
                    .HasColumnName("AD");
            });

            modelBuilder.Entity<TblCari>(entity =>
            {
                entity.ToTable("TBL_CARI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adres)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ADRES");

                entity.Property(e => e.Bakiye)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("BAKIYE");

                entity.Property(e => e.Eposta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EPOSTA");

                entity.Property(e => e.OlusturmaTarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("OLUSTURMA_TARIHI");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TELEFON");

                entity.Property(e => e.Unvan)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UNVAN");
            });

            modelBuilder.Entity<TblHammadde>(entity =>
            {
                entity.ToTable("TBL_HAMMADDE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adet).HasColumnName("ADET");

                entity.Property(e => e.BirimId).HasColumnName("BIRIM_ID");

                entity.Property(e => e.Fiyat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIYAT");

                entity.Property(e => e.MaddeAd)
                    .HasMaxLength(100)
                    .HasColumnName("MADDE_AD");
            });

            modelBuilder.Entity<TblSatis>(entity =>
            {
                entity.ToTable("TBL_SATIS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CariId).HasColumnName("CARI_ID");

                entity.Property(e => e.Maliyet)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("MALIYET");
                entity.Property(e => e.Fiyat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIYAT");
                entity.Property(e => e.Miktar).HasColumnName("MİKTAR");

                entity.Property(e => e.Tarih)
                    .HasColumnType("datetime")
                    .HasColumnName("TARIH");

                entity.Property(e => e.UrunId).HasColumnName("URUN_ID");
            });

            modelBuilder.Entity<TblUrun>(entity =>
            {
                entity.ToTable("TBL_URUN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fiyat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIYAT");

                entity.Property(e => e.Urunadi)
                    .HasMaxLength(500)
                    .HasColumnName("URUNADI");
            });

            modelBuilder.Entity<TblUrunbilgi>(entity =>
            {
                entity.ToTable("TBL_URUNBILGI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HammaddeId).HasColumnName("HAMMADDE_ID");

                entity.Property(e => e.Miktar).HasColumnName("MIKTAR");

                entity.Property(e => e.UrunId).HasColumnName("URUN_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
