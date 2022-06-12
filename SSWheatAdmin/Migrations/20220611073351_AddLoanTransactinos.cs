using Microsoft.EntityFrameworkCore.Migrations;

namespace SSWheatAdmin.Migrations
{
    public partial class AddLoanTransactinos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanTransactinos",
                columns: table => new
                {
                    Id = table.Column<ushort>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<long>(type: "INTEGER", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    LoanId = table.Column<ushort>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<ushort>(type: "INTEGER", nullable: false),
                    PersonalAccountNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactinos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanTransactinos_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactinos_AccountId",
                table: "LoanTransactinos",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanTransactinos");
        }
    }
}
