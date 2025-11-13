using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quick.DataAccess.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddUniversityToGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Groups",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "University",
                table: "Groups");
        }
    }
}
