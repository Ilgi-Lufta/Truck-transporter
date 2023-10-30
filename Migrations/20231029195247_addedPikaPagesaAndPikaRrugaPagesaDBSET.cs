using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioLab.Migrations
{
    public partial class addedPikaPagesaAndPikaRrugaPagesaDBSET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PikaRrugaPagesa_Currencys_CurrencyId",
                table: "PikaRrugaPagesa");

            migrationBuilder.DropForeignKey(
                name: "FK_PikaRrugaPagesa_PikaRrugas_PikaRrugaId",
                table: "PikaRrugaPagesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikaRrugaPagesa",
                table: "PikaRrugaPagesa");

            migrationBuilder.RenameTable(
                name: "PikaRrugaPagesa",
                newName: "PikaRrugaPagesas");

            migrationBuilder.RenameIndex(
                name: "IX_PikaRrugaPagesa_PikaRrugaId",
                table: "PikaRrugaPagesas",
                newName: "IX_PikaRrugaPagesas_PikaRrugaId");

            migrationBuilder.RenameIndex(
                name: "IX_PikaRrugaPagesa_CurrencyId",
                table: "PikaRrugaPagesas",
                newName: "IX_PikaRrugaPagesas_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikaRrugaPagesas",
                table: "PikaRrugaPagesas",
                column: "PikaRrugaPagesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PikaRrugaPagesas_Currencys_CurrencyId",
                table: "PikaRrugaPagesas",
                column: "CurrencyId",
                principalTable: "Currencys",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikaRrugaPagesas_PikaRrugas_PikaRrugaId",
                table: "PikaRrugaPagesas",
                column: "PikaRrugaId",
                principalTable: "PikaRrugas",
                principalColumn: "PikaRrugaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PikaRrugaPagesas_Currencys_CurrencyId",
                table: "PikaRrugaPagesas");

            migrationBuilder.DropForeignKey(
                name: "FK_PikaRrugaPagesas_PikaRrugas_PikaRrugaId",
                table: "PikaRrugaPagesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikaRrugaPagesas",
                table: "PikaRrugaPagesas");

            migrationBuilder.RenameTable(
                name: "PikaRrugaPagesas",
                newName: "PikaRrugaPagesa");

            migrationBuilder.RenameIndex(
                name: "IX_PikaRrugaPagesas_PikaRrugaId",
                table: "PikaRrugaPagesa",
                newName: "IX_PikaRrugaPagesa_PikaRrugaId");

            migrationBuilder.RenameIndex(
                name: "IX_PikaRrugaPagesas_CurrencyId",
                table: "PikaRrugaPagesa",
                newName: "IX_PikaRrugaPagesa_CurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikaRrugaPagesa",
                table: "PikaRrugaPagesa",
                column: "PikaRrugaPagesaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PikaRrugaPagesa_Currencys_CurrencyId",
                table: "PikaRrugaPagesa",
                column: "CurrencyId",
                principalTable: "Currencys",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikaRrugaPagesa_PikaRrugas_PikaRrugaId",
                table: "PikaRrugaPagesa",
                column: "PikaRrugaId",
                principalTable: "PikaRrugas",
                principalColumn: "PikaRrugaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
