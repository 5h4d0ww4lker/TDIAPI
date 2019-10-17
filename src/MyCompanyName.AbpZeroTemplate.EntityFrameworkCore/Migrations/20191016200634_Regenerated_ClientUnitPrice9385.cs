using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_ClientUnitPrice9385 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ClientUnitPrices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ClientUnitPrices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ClientUnitPrices",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ClientUnitPrices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientUnitPrices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ClientUnitPrices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ClientUnitPrices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ClientUnitPrices");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ClientUnitPrices");
        }
    }
}
