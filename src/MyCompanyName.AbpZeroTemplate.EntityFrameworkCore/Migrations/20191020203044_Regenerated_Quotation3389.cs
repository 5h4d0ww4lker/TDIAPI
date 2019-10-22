using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_Quotation3389 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Quotations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "Quotations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Quotations",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quotations");
        }
    }
}
