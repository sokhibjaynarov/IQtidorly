using IQtidorly.Api.Models.AgeGroups;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IQtidorly.Api.Migrations
{
    /// <inheritdoc />
    public partial class lang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<AgeGroupTranslation>(
                name: "Translation",
                schema: "iqtidorly",
                table: "AgeGroups",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Translation",
                schema: "iqtidorly",
                table: "AgeGroups");
        }
    }
}
