using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class PagesaRealeFitime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PagesaVertet",
                table: "RrugaShpenzimeEkstras",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PagesaReale",
                table: "RrugaFitimes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PagesaReale",
                table: "RrugaFitimeEkstras",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesaVertet",
                table: "RrugaShpenzimeEkstras");

            migrationBuilder.DropColumn(
                name: "PagesaReale",
                table: "RrugaFitimes");

            migrationBuilder.DropColumn(
                name: "PagesaReale",
                table: "RrugaFitimeEkstras");
        }
    }
}
