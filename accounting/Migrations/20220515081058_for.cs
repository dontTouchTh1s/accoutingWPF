using Microsoft.EntityFrameworkCore.Migrations;

namespace accounting.Migrations
{
    public partial class @for : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Peoples_PeopleDTONationalId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PeopleDTONationalId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PeopleDTONationalId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "OwnerNationalId",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerNationalId",
                table: "Accounts",
                column: "OwnerNationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Peoples_OwnerNationalId",
                table: "Accounts",
                column: "OwnerNationalId",
                principalTable: "Peoples",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Peoples_OwnerNationalId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_OwnerNationalId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OwnerNationalId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "PeopleDTONationalId",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PeopleDTONationalId",
                table: "Accounts",
                column: "PeopleDTONationalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Peoples_PeopleDTONationalId",
                table: "Accounts",
                column: "PeopleDTONationalId",
                principalTable: "Peoples",
                principalColumn: "NationalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
