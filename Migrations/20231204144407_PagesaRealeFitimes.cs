using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class PagesaRealeFitimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagesaVertet",
                table: "RrugaShpenzimeEkstras",
                newName: "PagesaReale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagesaReale",
                table: "RrugaShpenzimeEkstras",
                newName: "PagesaVertet");
        }
    }
}
