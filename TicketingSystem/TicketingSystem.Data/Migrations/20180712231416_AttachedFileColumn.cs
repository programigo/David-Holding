using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketingSystem.Data.Migrations
{
    public partial class AttachedFileColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageAttachedFile");

            migrationBuilder.DropTable(
                name: "TicketAttachedFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "AttachedFiles",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AttachedFiles",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachedFiles",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AttachedFiles",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "MessageAttachedFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MessageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachedFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachedFile_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachedFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachedFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketAttachedFile_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachedFile_MessageId",
                table: "MessageAttachedFile",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachedFile_TicketId",
                table: "TicketAttachedFile",
                column: "TicketId");
        }
    }
}
