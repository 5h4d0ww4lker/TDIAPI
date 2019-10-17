using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ClientUnitPrice8018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUnitPrices_ProductCategories_ProductCategoryId",
                table: "ClientUnitPrices");

            migrationBuilder.DropIndex(
                name: "IX_ClientUnitPrices_ProductCategoryId",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "ClientUnitPrices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "ClientUnitPrices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientUnitPrices_ProductCategoryId",
                table: "ClientUnitPrices",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUnitPrices_ProductCategories_ProductCategoryId",
                table: "ClientUnitPrices",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
