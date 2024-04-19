using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HistoryId",
                table: "Orders",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_History_HistoryId",
                table: "Orders",
                column: "HistoryId",
                principalTable: "History",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_History_HistoryId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HistoryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Orders");
        }
    }
}
