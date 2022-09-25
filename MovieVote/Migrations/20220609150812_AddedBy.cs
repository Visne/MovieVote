using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVote.Migrations
{
    public partial class AddedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Movies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_AddedById",
                table: "Movies",
                column: "AddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_AddedById",
                table: "Movies",
                column: "AddedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_AddedById",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_AddedById",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Movies");
        }
    }
}
