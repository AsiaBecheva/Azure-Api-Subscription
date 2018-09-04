using Microsoft.EntityFrameworkCore.Migrations;

namespace ASE.Data.Migrations
{
    public partial class BearerToken_become_Alias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BearerToken",
                table: "Subscriptions",
                newName: "Alias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alias",
                table: "Subscriptions",
                newName: "BearerToken");
        }
    }
}
