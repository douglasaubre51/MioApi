using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedprojectstatuspropstoProjectmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOngoing",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsOngoing",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Projects");
        }
    }
}
