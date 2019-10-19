using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_UnitPrice8685 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrices_Products_ProductId",
                table: "UnitPrices");

            migrationBuilder.DropIndex(
                name: "IX_UnitPrices_ProductId",
                table: "UnitPrices");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "UnitPrices");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "UnitPrices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrices_ProductCategoryId",
                table: "UnitPrices",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPrices_ProductCategories_ProductCategoryId",
                table: "UnitPrices",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrices_ProductCategories_ProductCategoryId",
                table: "UnitPrices");

            migrationBuilder.DropIndex(
                name: "IX_UnitPrices_ProductCategoryId",
                table: "UnitPrices");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "UnitPrices");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "UnitPrices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrices_ProductId",
                table: "UnitPrices",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPrices_Products_ProductId",
                table: "UnitPrices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
