using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQtidorly.Api.Migrations
{
    /// <inheritdoc />
    public partial class Question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Olympiads",
                schema: "iqtidorly",
                columns: table => new
                {
                    OlympiadId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Olympiads", x => x.OlympiadId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "iqtidorly",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    SubjectChapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgeGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_AgeGroups_AgeGroupId",
                        column: x => x.AgeGroupId,
                        principalSchema: "iqtidorly",
                        principalTable: "AgeGroups",
                        principalColumn: "AgeGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_SubjectChapters_SubjectChapterId",
                        column: x => x.SubjectChapterId,
                        principalSchema: "iqtidorly",
                        principalTable: "SubjectChapters",
                        principalColumn: "SubjectChapterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OlympiadResults",
                schema: "iqtidorly",
                columns: table => new
                {
                    OlympiadResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    OlympiadId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CorrectCount = table.Column<int>(type: "integer", nullable: false),
                    WrongCount = table.Column<int>(type: "integer", nullable: false),
                    EmptyCount = table.Column<int>(type: "integer", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OlympiadResults", x => x.OlympiadResultId);
                    table.ForeignKey(
                        name: "FK_OlympiadResults_Olympiads_OlympiadId",
                        column: x => x.OlympiadId,
                        principalSchema: "iqtidorly",
                        principalTable: "Olympiads",
                        principalColumn: "OlympiadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OlympiadResults_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "iqtidorly",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OlympiadQuestions",
                schema: "iqtidorly",
                columns: table => new
                {
                    OlympiadQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    OlympiadId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OlympiadQuestions", x => x.OlympiadQuestionId);
                    table.ForeignKey(
                        name: "FK_OlympiadQuestions_Olympiads_OlympiadId",
                        column: x => x.OlympiadId,
                        principalSchema: "iqtidorly",
                        principalTable: "Olympiads",
                        principalColumn: "OlympiadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OlympiadQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "iqtidorly",
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                schema: "iqtidorly",
                columns: table => new
                {
                    QuestionOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.QuestionOptionId);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "iqtidorly",
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OlympiadResultAnswers",
                schema: "iqtidorly",
                columns: table => new
                {
                    OlympiadResultAnswerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OlympiadResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    SelectedOptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OlympiadResultAnswers", x => x.OlympiadResultAnswerId);
                    table.ForeignKey(
                        name: "FK_OlympiadResultAnswers_OlympiadResults_OlympiadResultId",
                        column: x => x.OlympiadResultId,
                        principalSchema: "iqtidorly",
                        principalTable: "OlympiadResults",
                        principalColumn: "OlympiadResultId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OlympiadResultAnswers_QuestionOptions_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalSchema: "iqtidorly",
                        principalTable: "QuestionOptions",
                        principalColumn: "QuestionOptionId");
                    table.ForeignKey(
                        name: "FK_OlympiadResultAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "iqtidorly",
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadQuestions_OlympiadId",
                schema: "iqtidorly",
                table: "OlympiadQuestions",
                column: "OlympiadId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadQuestions_QuestionId",
                schema: "iqtidorly",
                table: "OlympiadQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadResultAnswers_OlympiadResultId",
                schema: "iqtidorly",
                table: "OlympiadResultAnswers",
                column: "OlympiadResultId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadResultAnswers_QuestionId",
                schema: "iqtidorly",
                table: "OlympiadResultAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadResultAnswers_SelectedOptionId",
                schema: "iqtidorly",
                table: "OlympiadResultAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadResults_OlympiadId",
                schema: "iqtidorly",
                table: "OlympiadResults",
                column: "OlympiadId");

            migrationBuilder.CreateIndex(
                name: "IX_OlympiadResults_UserId",
                schema: "iqtidorly",
                table: "OlympiadResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                schema: "iqtidorly",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AgeGroupId",
                schema: "iqtidorly",
                table: "Questions",
                column: "AgeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SubjectChapterId",
                schema: "iqtidorly",
                table: "Questions",
                column: "SubjectChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OlympiadQuestions",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "OlympiadResultAnswers",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "OlympiadResults",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "QuestionOptions",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "Olympiads",
                schema: "iqtidorly");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "iqtidorly");
        }
    }
}
