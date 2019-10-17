using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Added_QuotationItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuotationItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    UnitOfMeasurement = table.Column<string>(maxLength: 100, nullable: false),
                    Quantity = table.Column<string>(nullable: false),
                    TotalMountInETB = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    QuotationId = table.Column<int>(nullable: true),
                    ProductCategoryId = table.Column<int>(nullable: true),
                    ProductSubCategoryId = table.Column<int>(nullable: true),
                    UnitPriceId = table.Column<int>(nullable: true),
                    ClientUnitPriceId = table.Column<int>(nullable: true),
                    QuotationUnitPriceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationItems_ClientUnitPrices_ClientUnitPriceId",
                        column: x => x.ClientUnitPriceId,
                        principalTable: "ClientUnitPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationItems_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationItems_ProductSubCategories_ProductSubCategoryId",
                        column: x => x.ProductSubCategoryId,
                        principalTable: "ProductSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationItems_Quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationItems_QuotationUnitPrices_QuotationUnitPriceId",
                        column: x => x.QuotationUnitPriceId,
                        principalTable: "QuotationUnitPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationItems_UnitPrices_UnitPriceId",
                        column: x => x.UnitPriceId,
                        principalTable: "UnitPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_ClientUnitPriceId",
                table: "QuotationItems",
                column: "ClientUnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_ProductCategoryId",
                table: "QuotationItems",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_ProductSubCategoryId",
                table: "QuotationItems",
                column: "ProductSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_QuotationId",
                table: "QuotationItems",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_QuotationUnitPriceId",
                table: "QuotationItems",
                column: "QuotationUnitPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationItems_UnitPriceId",
                table: "QuotationItems",
                column: "UnitPriceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationItems");
        }
    }
}
