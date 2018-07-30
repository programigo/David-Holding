using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketingSystem.Data.Migrations
{
    public partial class CascadeDeleteAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SenderId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SenderId",
                table: "Tickets",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_SenderId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_SenderId",
                table: "Tickets",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
