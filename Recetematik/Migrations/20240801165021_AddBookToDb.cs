using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recetematik.Migrations
{
    public partial class AddBookToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_BIRIM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_BIRIM", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_CARI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UNVAN = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EPOSTA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TELEFON = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ADRES = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    BAKIYE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OLUSTURMA_TARIHI = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CARI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_HAMMADDE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MADDE_AD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ADET = table.Column<int>(type: "int", nullable: true),
                    FIYAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BIRIM_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_HAMMADDE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_URUN",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URUNADI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FIYAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_URUN", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_URUNBILGI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URUN_ID = table.Column<int>(type: "int", nullable: true),
                    HAMMADDE_ID = table.Column<int>(type: "int", nullable: true),
                    MIKTAR = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_URUNBILGI", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_BIRIM");

            migrationBuilder.DropTable(
                name: "TBL_CARI");

            migrationBuilder.DropTable(
                name: "TBL_HAMMADDE");

            migrationBuilder.DropTable(
                name: "TBL_URUN");

            migrationBuilder.DropTable(
                name: "TBL_URUNBILGI");
        }
    }
}
