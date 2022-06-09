using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVote.Migrations
{
    public partial class UniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_OAuthId_OAuthProvider",
                table: "Users",
                columns: new[] { "OAuthId", "OAuthProvider" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_OAuthId_OAuthProvider",
                table: "Users");
        }
    }
}
