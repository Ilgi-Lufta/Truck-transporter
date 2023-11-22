using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BlereShiturs",
                columns: table => new
                {
                    BlereShiturId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlereShiturs", x => x.BlereShiturId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currencys",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyUnit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencys", x => x.CurrencyId);
                })
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
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaShkarkimis", x => x.PikaShkarkimiId);
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
                    Model = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shenime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NaftaShpenzuarLitra = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rrugas", x => x.RrugaId);
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
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shofers", x => x.ShoferId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZbritShtoGjendjas",
                columns: table => new
                {
                    ZbritShtoGjendjaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Shenime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ZbritShtoSelect = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZbritShtoGjendjas", x => x.ZbritShtoGjendjaId);
                    table.ForeignKey(
                        name: "FK_ZbritShtoGjendjas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PagesaPikaShkarkimits",
                columns: table => new
                {
                    PagesaPikaShkarkimitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagesaPikaShkarkimits", x => x.PagesaPikaShkarkimitId);
                    table.ForeignKey(
                        name: "FK_PagesaPikaShkarkimits_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagesaPikaShkarkimits_PikaShkarkimis_PikaShkarkimiId",
                        column: x => x.PikaShkarkimiId,
                        principalTable: "PikaShkarkimis",
                        principalColumn: "PikaShkarkimiId",
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
                    BlereShiturSelect = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: true),
                    BlereShiturId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naftas", x => x.NaftaId);
                    table.ForeignKey(
                        name: "FK_Naftas_BlereShiturs_BlereShiturId",
                        column: x => x.BlereShiturId,
                        principalTable: "BlereShiturs",
                        principalColumn: "BlereShiturId");
                    table.ForeignKey(
                        name: "FK_Naftas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Naftas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PagesaDoganas",
                columns: table => new
                {
                    PagesaDoganaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagesaDoganas", x => x.PagesaDoganaId);
                    table.ForeignKey(
                        name: "FK_PagesaDoganas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagesaDoganas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PikaRrugas",
                columns: table => new
                {
                    PikaRrugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaRrugas", x => x.PikaRrugaId);
                    table.ForeignKey(
                        name: "FK_PikaRrugas_PikaShkarkimis_PikaShkarkimiId",
                        column: x => x.PikaShkarkimiId,
                        principalTable: "PikaShkarkimis",
                        principalColumn: "PikaShkarkimiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PikaRrugas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RrugaFitimeEkstras",
                columns: table => new
                {
                    RrugaFitimeEkstraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shenime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RrugaFitimeEkstras", x => x.RrugaFitimeEkstraId);
                    table.ForeignKey(
                        name: "FK_RrugaFitimeEkstras_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RrugaFitimeEkstras_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RrugaFitimes",
                columns: table => new
                {
                    RrugaFitimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RrugaFitimes", x => x.RrugaFitimeId);
                    table.ForeignKey(
                        name: "FK_RrugaFitimes_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RrugaFitimes_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RrugaShpenzimeEkstras",
                columns: table => new
                {
                    RrugaShpenzimeEkstraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    shenime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RrugaShpenzimeEkstras", x => x.RrugaShpenzimeEkstraId);
                    table.ForeignKey(
                        name: "FK_RrugaShpenzimeEkstras_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RrugaShpenzimeEkstras_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShoferRrugas",
                columns: table => new
                {
                    ShoferRrugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShoferId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoferRrugas", x => x.ShoferRrugaId);
                    table.ForeignKey(
                        name: "FK_ShoferRrugas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoferRrugas_Shofers_ShoferId",
                        column: x => x.ShoferId,
                        principalTable: "Shofers",
                        principalColumn: "ShoferId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PikaRrugaPagesas",
                columns: table => new
                {
                    PikaRrugaPagesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PikaRrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaRrugaPagesas", x => x.PikaRrugaPagesaId);
                    table.ForeignKey(
                        name: "FK_PikaRrugaPagesas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PikaRrugaPagesas_PikaRrugas_PikaRrugaId",
                        column: x => x.PikaRrugaId,
                        principalTable: "PikaRrugas",
                        principalColumn: "PikaRrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PagesaShoferits",
                columns: table => new
                {
                    PagesaShoferitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ShoferRrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PagesaKryer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagesaShoferits", x => x.PagesaShoferitId);
                    table.ForeignKey(
                        name: "FK_PagesaShoferits_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagesaShoferits_ShoferRrugas_ShoferRrugaId",
                        column: x => x.ShoferRrugaId,
                        principalTable: "ShoferRrugas",
                        principalColumn: "ShoferRrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_BlereShiturId",
                table: "Naftas",
                column: "BlereShiturId");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_CurrencyId",
                table: "Naftas",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_RrugaId",
                table: "Naftas",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaDoganas_CurrencyId",
                table: "PagesaDoganas",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaDoganas_RrugaId",
                table: "PagesaDoganas",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaPikaShkarkimits_CurrencyId",
                table: "PagesaPikaShkarkimits",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaPikaShkarkimits_PikaShkarkimiId",
                table: "PagesaPikaShkarkimits",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaShoferits_CurrencyId",
                table: "PagesaShoferits",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaShoferits_ShoferRrugaId",
                table: "PagesaShoferits",
                column: "ShoferRrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugaPagesas_CurrencyId",
                table: "PikaRrugaPagesas",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugaPagesas_PikaRrugaId",
                table: "PikaRrugaPagesas",
                column: "PikaRrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugas_PikaShkarkimiId",
                table: "PikaRrugas",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugas_RrugaId",
                table: "PikaRrugas",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaFitimeEkstras_CurrencyId",
                table: "RrugaFitimeEkstras",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaFitimeEkstras_RrugaId",
                table: "RrugaFitimeEkstras",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaFitimes_CurrencyId",
                table: "RrugaFitimes",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaFitimes_RrugaId",
                table: "RrugaFitimes",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaShpenzimeEkstras_CurrencyId",
                table: "RrugaShpenzimeEkstras",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaShpenzimeEkstras_RrugaId",
                table: "RrugaShpenzimeEkstras",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoferRrugas_RrugaId",
                table: "ShoferRrugas",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoferRrugas_ShoferId",
                table: "ShoferRrugas",
                column: "ShoferId");

            migrationBuilder.CreateIndex(
                name: "IX_ZbritShtoGjendjas_CurrencyId",
                table: "ZbritShtoGjendjas",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Naftas");

            migrationBuilder.DropTable(
                name: "PagesaDoganas");

            migrationBuilder.DropTable(
                name: "PagesaPikaShkarkimits");

            migrationBuilder.DropTable(
                name: "PagesaShoferits");

            migrationBuilder.DropTable(
                name: "PikaRrugaPagesas");

            migrationBuilder.DropTable(
                name: "RrugaFitimeEkstras");

            migrationBuilder.DropTable(
                name: "RrugaFitimes");

            migrationBuilder.DropTable(
                name: "RrugaShpenzimeEkstras");

            migrationBuilder.DropTable(
                name: "ZbritShtoGjendjas");

            migrationBuilder.DropTable(
                name: "BlereShiturs");

            migrationBuilder.DropTable(
                name: "ShoferRrugas");

            migrationBuilder.DropTable(
                name: "PikaRrugas");

            migrationBuilder.DropTable(
                name: "Currencys");

            migrationBuilder.DropTable(
                name: "Shofers");

            migrationBuilder.DropTable(
                name: "PikaShkarkimis");

            migrationBuilder.DropTable(
                name: "Rrugas");
        }
    }
}
