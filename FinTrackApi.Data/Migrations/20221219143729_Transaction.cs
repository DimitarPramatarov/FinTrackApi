using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrackApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Balances");

            migrationBuilder.CreateTable(
                name: "MoneyTransactions",
                columns: table => new
                {
                    MoneyTransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MoneyTransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyTransactionValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransactions", x => x.MoneyTransactionId);
                    table.ForeignKey(
                        name: "FK_MoneyTransactions_Balances_BalanceId",
                        column: x => x.BalanceId,
                        principalTable: "Balances",
                        principalColumn: "BalanceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyTransactions_BalanceId",
                table: "MoneyTransactions",
                column: "BalanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransactions");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Balances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
