using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQtidorly.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeGroups",
                schema: "iqtidorly",
                columns: table => new
                {
                    AgeGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    MinAge = table.Column<int>(type: "integer", nullable: false),
                    MaxAge = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroups", x => x.AgeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                schema: "iqtidorly",
                columns: table => new
                {
                    BookAuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirsName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PhotoFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.BookAuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                schema: "iqtidorly",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "iqtidorly",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CoverFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortDecriptionFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPages = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    BookAuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_BookAuthors_BookAuthorId",
                        column: x => x.BookAuthorId,
                        principalSchema: "iqtidorly",
                        principalTable: "BookAuthors",
                        principalColumn: "BookAuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectChapters",
                schema: "iqtidorly",
                columns: table => new
                {
                    SubjectChapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectChapters", x => x.SubjectChapterId);
                    table.ForeignKey(
                        name: "FK_SubjectChapters_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "iqtidorly",
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookAuthorId",
                schema: "iqtidorly",
                table: "Books",
                column: "BookAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectChapters_SubjectId",
                schema: "iqtidorly",
                table: "SubjectChapters",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeGroups",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "SubjectChapters",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "BookAuthors",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "Subjects",
                schema: "iqtidorly");
        }
    }
}
