using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MioApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnfinishedTasksNopropinProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnfinishedTasksNo",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnfinishedTasksNo",
                table: "Projects");
        }
    }
}
