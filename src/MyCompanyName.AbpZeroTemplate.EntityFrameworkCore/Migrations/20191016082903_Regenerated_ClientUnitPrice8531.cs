using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ClientUnitPrice8531 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUnitPrices_UnitPrices_UnitPriceId",
                table: "ClientUnitPrices");

            migrationBuilder.DropIndex(
                name: "IX_ClientUnitPrices_UnitPriceId",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "UnitPriceId",
                table: "ClientUnitPrices");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "ClientUnitPrices",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ClientUnitPrices",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ClientUnitPrices");

            migrationBuilder.AddColumn<int>(
                name: "UnitPriceId",
                table: "ClientUnitPrices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientUnitPrices_UnitPriceId",
                table: "ClientUnitPrices",
                column: "UnitPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUnitPrices_UnitPrices_UnitPriceId",
                table: "ClientUnitPrices",
                column: "UnitPriceId",
                principalTable: "UnitPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
