using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ProductSubCategory6918 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extruder",
                table: "ProductSubCategories",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PipeHead",
                table: "ProductSubCategories",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extruder",
                table: "ProductSubCategories");

            migrationBuilder.DropColumn(
                name: "PipeHead",
                table: "ProductSubCategories");
        }
    }
}
