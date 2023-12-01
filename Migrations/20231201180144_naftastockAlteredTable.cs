using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class naftastockAlteredTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NaftaStocks",
                columns: table => new
                {
                    NaftaStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Litra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BlereShiturSelect = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: true),
                    BlereShiturId = table.Column<int>(type: "int", nullable: true),
                    Shenime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaftaStocks", x => x.NaftaStockId);
                    table.ForeignKey(
                        name: "FK_NaftaStocks_BlereShiturs_BlereShiturId",
                        column: x => x.BlereShiturId,
                        principalTable: "BlereShiturs",
                        principalColumn: "BlereShiturId");
                    table.ForeignKey(
                        name: "FK_NaftaStocks_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaftaStocks_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaStocks_BlereShiturId",
                table: "NaftaStocks",
                column: "BlereShiturId");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaStocks_CurrencyId",
                table: "NaftaStocks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaStocks_RrugaId",
                table: "NaftaStocks",
                column: "RrugaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NaftaStocks");
        }
    }
}
