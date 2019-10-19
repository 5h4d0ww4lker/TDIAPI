using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ClientUnitPrice8931 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUnitPrices_ProductSubCategories_ProductSubCategoryId",
                table: "ClientUnitPrices");

            migrationBuilder.RenameColumn(
                name: "ProductSubCategoryId",
                table: "ClientUnitPrices",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUnitPrices_ProductSubCategoryId",
                table: "ClientUnitPrices",
                newName: "IX_ClientUnitPrices_ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUnitPrices_ProductCategories_ProductCategoryId",
                table: "ClientUnitPrices",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUnitPrices_ProductCategories_ProductCategoryId",
                table: "ClientUnitPrices");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "ClientUnitPrices",
                newName: "ProductSubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientUnitPrices_ProductCategoryId",
                table: "ClientUnitPrices",
                newName: "IX_ClientUnitPrices_ProductSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUnitPrices_ProductSubCategories_ProductSubCategoryId",
                table: "ClientUnitPrices",
                column: "ProductSubCategoryId",
                principalTable: "ProductSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
