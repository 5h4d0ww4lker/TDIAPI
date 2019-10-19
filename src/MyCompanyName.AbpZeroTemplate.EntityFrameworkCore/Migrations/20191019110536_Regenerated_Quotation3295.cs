using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_Quotation3295 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceOfDelivery",
                table: "Quotations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Quotations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_ProductCategoryId",
                table: "Quotations",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_ProductCategories_ProductCategoryId",
                table: "Quotations",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_ProductCategories_ProductCategoryId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_ProductCategoryId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PlaceOfDelivery",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Quotations");
        }
    }
}
