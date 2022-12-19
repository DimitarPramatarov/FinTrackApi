using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrackApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixTransactionValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MoneyTransactionValue",
                table: "MoneyTransactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MoneyTransactionValue",
                table: "MoneyTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
