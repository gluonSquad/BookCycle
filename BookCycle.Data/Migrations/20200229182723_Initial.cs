using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookCycle.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Mail = table.Column<string>(maxLength: 70, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    Telephone = table.Column<string>(maxLength: 11, nullable: true),
                    ProfileImageFile = table.Column<string>(maxLength: 30, nullable: true),
                    ActivateGuid = table.Column<Guid>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Penal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BookPages = table.Column<int>(nullable: false),
                    BookPublisher = table.Column<string>(maxLength: 70, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Isbn = table.Column<string>(maxLength: 13, nullable: false),
                    BookImageFile = table.Column<string>(maxLength: 200, nullable: true),
                    AuthorId = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    BorrowingDate = table.Column<DateTime>(nullable: false),
                    BroughtDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    BookId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: true),
                    LikedUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Users_LikedUserId",
                        column: x => x.LikedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Jose Mauro De Vasconcelos" },
                    { 2, "Ali Ural" },
                    { 3, "Douglas Adams" },
                    { 4, "Zülfü Livaneli" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Roman" },
                    { 2, "Deneme" },
                    { 3, "Bilim-Kurgu" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookImageFile", "BookPages", "BookPublisher", "CategoryId", "Description", "Isbn", "LikeCount", "Name", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "https://imageserver.kitapyurdu.com/select.php?imageid=1105506&width=180&isWatermarked=true", 260, "CAN YAYINLARI", 1, null, "9750738609", 0, "Şeker Portakalı", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, "https://imageserver.kitapyurdu.com/select.php?imageid=2876841&width=180&isWatermarked=true&pagecount=160", 160, "DOĞAN KİTAP", 1, null, "6050939828", 0, "Huzursuzluk", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "https://imageserver.kitapyurdu.com/select.php?imageid=10289747&width=180&isWatermarked=true&pagecount=206", 206, "ŞULE YAYINLARI", 2, null, "9756841357", 0, "Posta Kutusundaki Mızıka", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "https://imageserver.kitapyurdu.com/select.php?imageid=10904955&width=180&isWatermarked=true&pagecount=248", 248, "ALFA YAYINLARI", 3, null, "6051715124", 0, "Otostopçunun Galaksi Rehberi", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, "https://imageserver.kitapyurdu.com/select.php?imageid=10459513&width=180&isWatermarked=true&pagecount=248", 248, "ALFA YAYINLARI", 3, null, "6051715100", 0, "Hayat, Evren ve Herşey", new DateTime(2020, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_UserId",
                table: "BorrowedBooks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BookId",
                table: "Likes",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikedUserId",
                table: "Likes",
                column: "LikedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
