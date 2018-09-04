using Microsoft.EntityFrameworkCore.Migrations;

namespace ASE.Data.Migrations
{
    public partial class SubscriptionAzureId_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subscriptions",
                newName: "SubscriptionAzureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionAzureId",
                table: "Subscriptions",
                newName: "SubscriptionId");
        }
    }
}
