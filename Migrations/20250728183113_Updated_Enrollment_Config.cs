using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillHub.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Enrollment_Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Sessions_SessionId1",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_LearnerId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Sessions_SessionId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_LearnerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_MentorId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_LearnerId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_SessionId1",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "SessionId1",
                table: "Enrollments");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Reports",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "LearnerId", "SessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_LearnerId",
                table: "Enrollments",
                column: "LearnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Sessions_SessionId",
                table: "Reports",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_LearnerId",
                table: "Reviews",
                column: "LearnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_MentorId",
                table: "Sessions",
                column: "MentorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_LearnerId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Sessions_SessionId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_LearnerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_MentorId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Reports",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "SessionId1",
                table: "Enrollments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "UserId", "SessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_LearnerId",
                table: "Enrollments",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SessionId1",
                table: "Enrollments",
                column: "SessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Sessions_SessionId1",
                table: "Enrollments",
                column: "SessionId1",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_LearnerId",
                table: "Enrollments",
                column: "LearnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Sessions_SessionId",
                table: "Reports",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_LearnerId",
                table: "Reviews",
                column: "LearnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_MentorId",
                table: "Sessions",
                column: "MentorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
