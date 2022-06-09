using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVote.Migrations
{
    public partial class TypoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OAauthProvider",
                table: "Users",
                newName: "OAuthProvider");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OAuthProvider",
                table: "Users",
                newName: "OAauthProvider");
        }
    }
}
