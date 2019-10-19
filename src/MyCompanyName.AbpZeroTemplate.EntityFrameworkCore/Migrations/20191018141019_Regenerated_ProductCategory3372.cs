using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ProductCategory3372 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pipehead",
                table: "ProductCategories",
                newName: "UOM");

            migrationBuilder.RenameColumn(
                name: "Extruder",
                table: "ProductCategories",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UOM",
                table: "ProductCategories",
                newName: "Pipehead");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProductCategories",
                newName: "Extruder");
        }
    }
}
