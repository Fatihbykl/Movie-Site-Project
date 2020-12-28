using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProgramlamaProje.Data.Migrations
{
    public partial class movuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUser");

            migrationBuilder.CreateTable(
                name: "UserMovies",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDMovie = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MovieID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMovies", x => new { x.IDUser, x.IDMovie });
                    table.ForeignKey(
                        name: "FK_UserMovies_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMovies_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMovies_MovieID",
                table: "UserMovies",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovies_userId",
                table: "UserMovies",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMovies");

            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    ViewersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WatchedMoviesMovieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.ViewersId, x.WatchedMoviesMovieID });
                    table.ForeignKey(
                        name: "FK_MovieUser_AspNetUsers_ViewersId",
                        column: x => x.ViewersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser_Movies_WatchedMoviesMovieID",
                        column: x => x.WatchedMoviesMovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_WatchedMoviesMovieID",
                table: "MovieUser",
                column: "WatchedMoviesMovieID");
        }
    }
}
