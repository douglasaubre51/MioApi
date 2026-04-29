using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsBookmarkedpropinProjectmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBookmarked",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBookmarked",
                table: "Projects");
        }
    }
}
