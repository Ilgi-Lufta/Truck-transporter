using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class removedShoferAndPikaFromRrugeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rrugas_PikaShkarkimis_PikaShkarkimiId",
                table: "Rrugas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rrugas_Shofers_ShoferId",
                table: "Rrugas");

            migrationBuilder.DropIndex(
                name: "IX_Rrugas_PikaShkarkimiId",
                table: "Rrugas");

            migrationBuilder.DropIndex(
                name: "IX_Rrugas_ShoferId",
                table: "Rrugas");

            migrationBuilder.DropColumn(
                name: "PikaShkarkimiId",
                table: "Rrugas");

            migrationBuilder.DropColumn(
                name: "ShoferId",
                table: "Rrugas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PikaShkarkimiId",
                table: "Rrugas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoferId",
                table: "Rrugas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rrugas_PikaShkarkimiId",
                table: "Rrugas",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rrugas_ShoferId",
                table: "Rrugas",
                column: "ShoferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rrugas_PikaShkarkimis_PikaShkarkimiId",
                table: "Rrugas",
                column: "PikaShkarkimiId",
                principalTable: "PikaShkarkimis",
                principalColumn: "PikaShkarkimiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rrugas_Shofers_ShoferId",
                table: "Rrugas",
                column: "ShoferId",
                principalTable: "Shofers",
                principalColumn: "ShoferId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
