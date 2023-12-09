using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class naftanegative : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NaftaNegative",
                table: "Rrugas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NaftaNegative",
                table: "Rrugas");
        }
    }
}
