using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                name: "PagesaPikaShkarkimits",
                columns: table => new
                {
                    PagesaPikaShkarkimitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_PagesaPikaShkarkimits_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
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
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    ShoferId = table.Column<int>(type: "int", nullable: false),
                    Pagesa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShpenzimXhiro = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                        name: "FK_PagesaShoferits_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagesaShoferits_Shofers_ShoferId",
                        column: x => x.ShoferId,
                        principalTable: "Shofers",
                        principalColumn: "ShoferId",
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
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikaRrugas", x => x.PikaRrugaId);
                    table.ForeignKey(
                        name: "FK_PikaRrugas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
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
                    PikaShkarkimiId = table.Column<int>(type: "int", nullable: false),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ShoferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoferRrugas", x => x.ShoferRrugaId);
                    table.ForeignKey(
                        name: "FK_ShoferRrugas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoferRrugas_PikaShkarkimis_PikaShkarkimiId",
                        column: x => x.PikaShkarkimiId,
                        principalTable: "PikaShkarkimis",
                        principalColumn: "PikaShkarkimiId",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "ShoferId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NaftaRrugas",
                columns: table => new
                {
                    NaftaRrugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RrugaId = table.Column<int>(type: "int", nullable: false),
                    NaftaId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaftaRrugas", x => x.NaftaRrugaId);
                    table.ForeignKey(
                        name: "FK_NaftaRrugas_Currencys_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencys",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaftaRrugas_Naftas_NaftaId",
                        column: x => x.NaftaId,
                        principalTable: "Naftas",
                        principalColumn: "NaftaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaftaRrugas_Rrugas_RrugaId",
                        column: x => x.RrugaId,
                        principalTable: "Rrugas",
                        principalColumn: "RrugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaRrugas_CurrencyId",
                table: "NaftaRrugas",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaRrugas_NaftaId",
                table: "NaftaRrugas",
                column: "NaftaId");

            migrationBuilder.CreateIndex(
                name: "IX_NaftaRrugas_RrugaId",
                table: "NaftaRrugas",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Naftas_RrugaId",
                table: "Naftas",
                column: "RrugaId",
                unique: true);

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
                name: "IX_PagesaPikaShkarkimits_RrugaId",
                table: "PagesaPikaShkarkimits",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaShoferits_CurrencyId",
                table: "PagesaShoferits",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaShoferits_RrugaId",
                table: "PagesaShoferits",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_PagesaShoferits_ShoferId",
                table: "PagesaShoferits",
                column: "ShoferId");

            migrationBuilder.CreateIndex(
                name: "IX_PikaRrugas_CurrencyId",
                table: "PikaRrugas",
                column: "CurrencyId");

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
                name: "IX_Rrugas_PikaShkarkimiId",
                table: "Rrugas",
                column: "PikaShkarkimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rrugas_ShoferId",
                table: "Rrugas",
                column: "ShoferId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaShpenzimeEkstras_CurrencyId",
                table: "RrugaShpenzimeEkstras",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RrugaShpenzimeEkstras_RrugaId",
                table: "RrugaShpenzimeEkstras",
                column: "RrugaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoferRrugas_CurrencyId",
                table: "ShoferRrugas",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoferRrugas_PikaShkarkimiId",
                table: "ShoferRrugas",
                column: "PikaShkarkimiId");

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
                name: "NaftaRrugas");

            migrationBuilder.DropTable(
                name: "PagesaDoganas");

            migrationBuilder.DropTable(
                name: "PagesaPikaShkarkimits");

            migrationBuilder.DropTable(
                name: "PagesaShoferits");

            migrationBuilder.DropTable(
                name: "PikaRrugas");

            migrationBuilder.DropTable(
                name: "RrugaFitimeEkstras");

            migrationBuilder.DropTable(
                name: "RrugaFitimes");

            migrationBuilder.DropTable(
                name: "RrugaShpenzimeEkstras");

            migrationBuilder.DropTable(
                name: "ShoferRrugas");

            migrationBuilder.DropTable(
                name: "ZbritShtoGjendjas");

            migrationBuilder.DropTable(
                name: "Naftas");

            migrationBuilder.DropTable(
                name: "Currencys");

            migrationBuilder.DropTable(
                name: "Rrugas");

            migrationBuilder.DropTable(
                name: "PikaShkarkimis");

            migrationBuilder.DropTable(
                name: "Shofers");
        }
    }
}
