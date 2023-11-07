using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class AddedBlereShiturNaftaOToM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlereShiturId",
                table: "Naftas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlereShitur",
                columns: table => new
                {
                    BlereShiturId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlereShitur", x => x.BlereShiturId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_BlereShiturId",
                table: "Naftas",
                column: "BlereShiturId");

            migrationBuilder.AddForeignKey(
                name: "FK_Naftas_BlereShitur_BlereShiturId",
                table: "Naftas",
                column: "BlereShiturId",
                principalTable: "BlereShitur",
                principalColumn: "BlereShiturId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Naftas_BlereShitur_BlereShiturId",
                table: "Naftas");

            migrationBuilder.DropTable(
                name: "BlereShitur");

            migrationBuilder.DropIndex(
                name: "IX_Naftas_BlereShiturId",
                table: "Naftas");

            migrationBuilder.DropColumn(
                name: "BlereShiturId",
                table: "Naftas");
        }
    }
}
