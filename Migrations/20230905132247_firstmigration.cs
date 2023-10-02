using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PikaShkarkimis",
                columns: table => new
                {
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Emri = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Model = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaShkarkimis", x => x.PikaShkarkimiId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shofers",
                columns: table => new
                {
                    ShoferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Emri = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Model = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shofers", x => x.ShoferId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rrugas",
                columns: table => new
                {
                    RrugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Emri = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dogana = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Model = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shenime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    shpenzimeEkstra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FitimeEkstra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NaftaShpenzuarLitra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NaftaPerTuShiturLitra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Shpenzime = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Xhiro = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Fitime = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PagesaShoferit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShoferId = table.Column<int>(type: "int", nullable: false),
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rrugas", x => x.RrugaId);
                    table.ForeignKey(
                        name: "FK_Rrugas_PikaShkarkimis_PikaShkarkimiId",
                        column: x => x.PikaShkarkimiId,
                        principalTable: "PikaShkarkimis",
                        principalColumn: "PikaShkarkimiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rrugas_Shofers_ShoferId",
                        column: x => x.ShoferId,
                        principalTable: "Shofers",
                        principalColumn: "ShoferId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Naftas",
                columns: table => new
                {
                    NaftaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Litra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Cmimi = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BlereShiturSelect = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Leke = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naftas", x => x.NaftaId);
                    table.ForeignKey(
                        name: "FK_Naftas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_RrugaId",
                table: "Naftas",
                column: "RrugaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rrugas_PikaShkarkimiId",
                table: "Rrugas",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rrugas_ShoferId",
                table: "Rrugas",
                column: "ShoferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Naftas");

            migrationBuilder.DropTable(
                name: "Rrugas");

            migrationBuilder.DropTable(
                name: "PikaShkarkimis");

            migrationBuilder.DropTable(
                name: "Shofers");
        }
    }
}
