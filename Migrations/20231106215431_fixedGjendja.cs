using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class fixedGjendja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZbritShtoSelect",
                table: "ZbritShtoGjendjas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZbritShtoSelect",
                table: "ZbritShtoGjendjas");
        }
    }
}
