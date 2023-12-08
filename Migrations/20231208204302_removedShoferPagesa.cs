using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class removedShoferPagesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pagesa",
                table: "Shofers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Pagesa",
                table: "Shofers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
