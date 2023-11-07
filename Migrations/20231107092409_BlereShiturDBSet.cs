using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class BlereShiturDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Naftas_BlereShitur_BlereShiturId",
                table: "Naftas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlereShitur",
                table: "BlereShitur");

            migrationBuilder.RenameTable(
                name: "BlereShitur",
                newName: "BlereShiturs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlereShiturs",
                table: "BlereShiturs",
                column: "BlereShiturId");

            migrationBuilder.AddForeignKey(
                name: "FK_Naftas_BlereShiturs_BlereShiturId",
                table: "Naftas",
                column: "BlereShiturId",
                principalTable: "BlereShiturs",
                principalColumn: "BlereShiturId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Naftas_BlereShiturs_BlereShiturId",
                table: "Naftas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlereShiturs",
                table: "BlereShiturs");

            migrationBuilder.RenameTable(
                name: "BlereShiturs",
                newName: "BlereShitur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlereShitur",
                table: "BlereShitur",
                column: "BlereShiturId");

            migrationBuilder.AddForeignKey(
                name: "FK_Naftas_BlereShitur_BlereShiturId",
                table: "Naftas",
                column: "BlereShiturId",
                principalTable: "BlereShitur",
                principalColumn: "BlereShiturId");
        }
    }
}
