using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class AddedPagesaKryerForAllPAgesaFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "RrugaShpenzimeEkstras",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "RrugaFitimeEkstras",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "PagesaShoferits",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "PagesaPikaShkarkimits",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "PagesaNafta",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PagesaKryer",
                table: "PagesaDoganas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "RrugaShpenzimeEkstras");

            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "RrugaFitimeEkstras");

            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "PagesaShoferits");

            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "PagesaPikaShkarkimits");

            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "PagesaNafta");

            migrationBuilder.DropColumn(
                name: "PagesaKryer",
                table: "PagesaDoganas");
        }
    }
}
