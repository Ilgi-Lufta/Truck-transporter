using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class addedPikaPagesaAndPikaRrugaPagesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PikaRrugas_PagesaPikaShkarkimits_PagesaPikaShkarkimitId",
                table: "PikaRrugas");

            migrationBuilder.DropIndex(
                name: "IX_PikaRrugas_PagesaPikaShkarkimitId",
                table: "PikaRrugas");

            migrationBuilder.CreateTable(
                name: "PikaRrugaPagesa",
                columns: table => new
                {
                    PikaRrugaPagesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PikaRrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaRrugaPagesa", x => x.PikaRrugaPagesaId);
                    table.ForeignKey(
                        name: "FK_PikaRrugaPagesa_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PikaRrugaPagesa_PikaRrugas_PikaRrugaId",
                        column: x => x.PikaRrugaId,
                        principalTable: "PikaRrugas",
                        principalColumn: "PikaRrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugaPagesa_CurrencyId",
                table: "PikaRrugaPagesa",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugaPagesa_PikaRrugaId",
                table: "PikaRrugaPagesa",
                column: "PikaRrugaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PikaRrugaPagesa");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugas_PagesaPikaShkarkimitId",
                table: "PikaRrugas",
                column: "PagesaPikaShkarkimitId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PikaRrugas_PagesaPikaShkarkimits_PagesaPikaShkarkimitId",
                table: "PikaRrugas",
                column: "PagesaPikaShkarkimitId",
                principalTable: "PagesaPikaShkarkimits",
                principalColumn: "PagesaPikaShkarkimitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
