using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesaPikaShkarkimitId",
                table: "PikaRrugas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PagesaPikaShkarkimitId",
                table: "PikaRrugas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
