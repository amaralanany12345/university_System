using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.Migrations
{
    /// <inheritdoc />
    public partial class attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_attendances",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_studentId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "id",
                table: "attendances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attendances",
                table: "attendances",
                columns: new[] { "studentId", "courseId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_attendances",
                table: "attendances");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "attendances",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attendances",
                table: "attendances",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_attendances_studentId",
                table: "attendances",
                column: "studentId");
        }
    }
}
