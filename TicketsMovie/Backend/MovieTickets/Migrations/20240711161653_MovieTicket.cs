using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZingMp3API.Migrations
{
    /// <inheritdoc />
    public partial class MovieTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccumulationAccount",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccumulationAccount", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DetailsTypeMovie",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeMovieId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsTypeMovie", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "HistoryPay",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypePay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPay", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "LanguageMovie",
                columns: table => new
                {
                    LanguageMovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageMovieName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageMovie", x => x.LanguageMovieId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrther",
                columns: table => new
                {
                    ProductOrtherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductOrtherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrther", x => x.ProductOrtherId);
                });

            migrationBuilder.CreateTable(
                name: "TypeMovie",
                columns: table => new
                {
                    TypeMovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeMovieName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMovie", x => x.TypeMovieId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfor",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfor", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieTime = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dicrectory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Performer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageMovieId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movie_LanguageMovie_LanguageMovieId",
                        column: x => x.LanguageMovieId,
                        principalTable: "LanguageMovie",
                        principalColumn: "LanguageMovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Money = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketsId);
                    table.ForeignKey(
                        name: "FK_Ticket_UserInfor_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailsProductOrther",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductOrtherId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsProductOrther", x => new { x.TicketId, x.ProductOrtherId });
                    table.ForeignKey(
                        name: "FK_DetailsProductOrther_ProductOrther_ProductOrtherId",
                        column: x => x.ProductOrtherId,
                        principalTable: "ProductOrther",
                        principalColumn: "ProductOrtherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailsProductOrther_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailsTicket",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceTicket = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Seat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsTicket", x => new { x.TicketId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_DetailsTicket_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailsTicket_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "TicketsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailsProductOrther_ProductOrtherId",
                table: "DetailsProductOrther",
                column: "ProductOrtherId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsTicket_MovieId",
                table: "DetailsTicket",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_LanguageMovieId",
                table: "Movie",
                column: "LanguageMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccumulationAccount");

            migrationBuilder.DropTable(
                name: "DetailsProductOrther");

            migrationBuilder.DropTable(
                name: "DetailsTicket");

            migrationBuilder.DropTable(
                name: "DetailsTypeMovie");

            migrationBuilder.DropTable(
                name: "HistoryPay");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "TypeMovie");

            migrationBuilder.DropTable(
                name: "ProductOrther");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "LanguageMovie");

            migrationBuilder.DropTable(
                name: "UserInfor");
        }
    }
}
