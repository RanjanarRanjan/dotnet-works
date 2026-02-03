using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBookIssueAndBookManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "BookIssues",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "BookIssues",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "IssueID",
                table: "BookIssues",
                newName: "IssueId");

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "BookIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookName",
                table: "BookIssues");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookIssues",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookIssues",
                newName: "BookID");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "BookIssues",
                newName: "IssueID");
        }
    }
}
