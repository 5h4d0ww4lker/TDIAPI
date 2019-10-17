using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_QuotationUnitPrice1376 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrices_ProductSubCategories_ProductSubCategoryId",
                table: "QuotationUnitPrices");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPrices_ProductSubCategoryId",
                table: "QuotationUnitPrices");

            migrationBuilder.DropColumn(
                name: "ProductSubCategoryId",
                table: "QuotationUnitPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductSubCategoryId",
                table: "QuotationUnitPrices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPrices_ProductSubCategoryId",
                table: "QuotationUnitPrices",
                column: "ProductSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrices_ProductSubCategories_ProductSubCategoryId",
                table: "QuotationUnitPrices",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
