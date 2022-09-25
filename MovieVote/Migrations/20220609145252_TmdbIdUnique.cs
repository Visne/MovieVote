using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieVote.Migrations
{
    public partial class TmdbIdUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Movies_TmdbId",
                table: "Movies",
                column: "TmdbId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Movies_TmdbId",
                table: "Movies");
        }
    }
}
