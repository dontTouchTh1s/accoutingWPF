using Microsoft.EntityFrameworkCore.Migrations;

namespace SSWheatAdmin.Migrations
{
    public partial class accoue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "Id");
        }
    }
}
