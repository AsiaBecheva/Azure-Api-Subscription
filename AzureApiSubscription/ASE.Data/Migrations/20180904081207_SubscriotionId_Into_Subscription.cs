using Microsoft.EntityFrameworkCore.Migrations;

namespace ASE.Data.Migrations
{
    public partial class SubscriotionId_Into_Subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriptionId",
                table: "Subscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Subscriptions");
        }
    }
}
