using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZingMp3API.Migrations
{
    /// <inheritdoc />
    public partial class AddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailsTypeMovie",
                table: "DetailsTypeMovie");

            migrationBuilder.AlterColumn<string>(
                name: "TypeMovieId",
                table: "DetailsTypeMovie",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailsTypeMovie",
                table: "DetailsTypeMovie",
                columns: new[] { "MovieId", "TypeMovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_DetailsTypeMovie_TypeMovieId",
                table: "DetailsTypeMovie",
                column: "TypeMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailsTypeMovie_Movie_MovieId",
                table: "DetailsTypeMovie",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailsTypeMovie_TypeMovie_TypeMovieId",
                table: "DetailsTypeMovie",
                column: "TypeMovieId",
                principalTable: "TypeMovie",
                principalColumn: "TypeMovieId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailsTypeMovie_Movie_MovieId",
                table: "DetailsTypeMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailsTypeMovie_TypeMovie_TypeMovieId",
                table: "DetailsTypeMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailsTypeMovie",
                table: "DetailsTypeMovie");

            migrationBuilder.DropIndex(
                name: "IX_DetailsTypeMovie_TypeMovieId",
                table: "DetailsTypeMovie");

            migrationBuilder.AlterColumn<string>(
                name: "TypeMovieId",
                table: "DetailsTypeMovie",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailsTypeMovie",
                table: "DetailsTypeMovie",
                column: "MovieId");
        }
    }
}
