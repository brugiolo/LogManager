using Microsoft.EntityFrameworkCore.Migrations;

namespace LogManager.Data.Migrations
{
    public partial class RenameColumnClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Client",
                table: "RequestLog",
                newName: "UserAgent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserAgent",
                table: "RequestLog",
                newName: "Client");
        }
    }
}
