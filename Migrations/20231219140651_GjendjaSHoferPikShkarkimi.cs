using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class GjendjaSHoferPikShkarkimi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PikaShkarkimiId",
                table: "ZbritShtoGjendjas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoferId",
                table: "ZbritShtoGjendjas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZbritShtoGjendjas_PikaShkarkimiId",
                table: "ZbritShtoGjendjas",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_ZbritShtoGjendjas_ShoferId",
                table: "ZbritShtoGjendjas",
                column: "ShoferId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritShtoGjendjas_PikaShkarkimis_PikaShkarkimiId",
                table: "ZbritShtoGjendjas",
                column: "PikaShkarkimiId",
                principalTable: "PikaShkarkimis",
                principalColumn: "PikaShkarkimiId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZbritShtoGjendjas_Shofers_ShoferId",
                table: "ZbritShtoGjendjas",
                column: "ShoferId",
                principalTable: "Shofers",
                principalColumn: "ShoferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZbritShtoGjendjas_PikaShkarkimis_PikaShkarkimiId",
                table: "ZbritShtoGjendjas");

            migrationBuilder.DropForeignKey(
                name: "FK_ZbritShtoGjendjas_Shofers_ShoferId",
                table: "ZbritShtoGjendjas");

            migrationBuilder.DropIndex(
                name: "IX_ZbritShtoGjendjas_PikaShkarkimiId",
                table: "ZbritShtoGjendjas");

            migrationBuilder.DropIndex(
                name: "IX_ZbritShtoGjendjas_ShoferId",
                table: "ZbritShtoGjendjas");

            migrationBuilder.DropColumn(
                name: "PikaShkarkimiId",
                table: "ZbritShtoGjendjas");

            migrationBuilder.DropColumn(
                name: "ShoferId",
                table: "ZbritShtoGjendjas");
        }
    }
}
