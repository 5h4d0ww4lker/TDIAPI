using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_QuotationItem9957 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationItems_ClientUnitPrices_ClientUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationItems_QuotationUnitPrices_QuotationUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationItems_UnitPrices_UnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropIndex(
                name: "IX_QuotationItems_ClientUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropIndex(
                name: "IX_QuotationItems_QuotationUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropIndex(
                name: "IX_QuotationItems_UnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropColumn(
                name: "ClientUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropColumn(
                name: "QuotationUnitPriceId",
                table: "QuotationItems");

            migrationBuilder.DropColumn(
                name: "UnitPriceId",
                table: "QuotationItems");

            migrationBuilder.AddColumn<string>(
                name: "CustomUnitPrice",
                table: "QuotationItems",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomUnitPrice",
                table: "QuotationItems");

            migrationBuilder.AddColumn<int>(
                name: "ClientUnitPriceId",
                table: "QuotationItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuotationUnitPriceId",
                table: "QuotationItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitPriceId",
                table: "QuotationItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_ClientUnitPriceId",
                table: "QuotationItems",
                column: "ClientUnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_QuotationUnitPriceId",
                table: "QuotationItems",
                column: "QuotationUnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_UnitPriceId",
                table: "QuotationItems",
                column: "UnitPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationItems_ClientUnitPrices_ClientUnitPriceId",
                table: "QuotationItems",
                column: "ClientUnitPriceId",
                principalTable: "ClientUnitPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationItems_QuotationUnitPrices_QuotationUnitPriceId",
                table: "QuotationItems",
                column: "QuotationUnitPriceId",
                principalTable: "QuotationUnitPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationItems_UnitPrices_UnitPriceId",
                table: "QuotationItems",
                column: "UnitPriceId",
                principalTable: "UnitPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
