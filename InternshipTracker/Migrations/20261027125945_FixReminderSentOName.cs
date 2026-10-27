using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipTracker.Migrations
{
    /// <inheritdoc />
    public partial class FixReminderSentOName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReminderSenton",
                table: "JobApplications",
                newName: "ReminderSentOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReminderSentOn",
                table: "JobApplications",
                newName: "ReminderSenton");
        }
    }
}
