using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_Quotation1837 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidityDays",
                table: "Quotations",
                newName: "DiscountInPercent");

            migrationBuilder.RenameColumn(
                name: "PaaymentTerms",
                table: "Quotations",
                newName: "DiscountInAmount");

            migrationBuilder.AddColumn<string>(
                name: "PriceValidity",
                table: "Quotations",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TermOfPayment",
                table: "Quotations",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceValidity",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "TermOfPayment",
                table: "Quotations");

            migrationBuilder.RenameColumn(
                name: "DiscountInPercent",
                table: "Quotations",
                newName: "ValidityDays");

            migrationBuilder.RenameColumn(
                name: "DiscountInAmount",
                table: "Quotations",
                newName: "PaaymentTerms");
        }
    }
}
