using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrackApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class @enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyTransactionType",
                table: "MoneyTransactions");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "MoneyTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "MoneyTransactions");

            migrationBuilder.AddColumn<string>(
                name: "MoneyTransactionType",
                table: "MoneyTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
