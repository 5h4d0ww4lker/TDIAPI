using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_Quotation1602 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceValidity",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "TermOfPayment",
                table: "Quotations");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTermId",
                table: "Quotations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceValidityId",
                table: "Quotations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PaymentTermId",
                table: "Quotations",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotations_PriceValidityId",
                table: "Quotations",
                column: "PriceValidityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_PaymentTerms_PaymentTermId",
                table: "Quotations",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotations_PriceValidities_PriceValidityId",
                table: "Quotations",
                column: "PriceValidityId",
                principalTable: "PriceValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_PaymentTerms_PaymentTermId",
                table: "Quotations");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotations_PriceValidities_PriceValidityId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PaymentTermId",
                table: "Quotations");

            migrationBuilder.DropIndex(
                name: "IX_Quotations_PriceValidityId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "PriceValidityId",
                table: "Quotations");

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
    }
}
