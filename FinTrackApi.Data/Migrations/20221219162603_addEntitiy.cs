using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrackApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addEntitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MoneyTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "MoneyTransactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MoneyTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifedOn",
                table: "MoneyTransactions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MoneyTransactions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MoneyTransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MoneyTransactions");

            migrationBuilder.DropColumn(
                name: "ModifedOn",
                table: "MoneyTransactions");
        }
    }
}
