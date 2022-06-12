using Microsoft.EntityFrameworkCore.Migrations;

namespace SSWheatAdmin.Migrations
{
    public partial class availableCredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalAccountNumber",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "LendDate",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonalAccountNumber",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvailableCredit",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LendDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalAccountNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AvailableCredit",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalAccountNumber",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiveDate",
                table: "Loans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
