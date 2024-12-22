using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.Migrations
{
    /// <inheritdoc />
    public partial class finalResultAndFailedCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "finalResults",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    totalScore = table.Column<int>(type: "int", nullable: false),
                    finalScore = table.Column<int>(type: "int", nullable: false),
                    finalRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finalResults", x => x.id);
                    table.ForeignKey(
                        name: "FK_finalResults_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "failedCourses",
                columns: table => new
                {
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    finalResultId = table.Column<int>(type: "int", nullable: false),
                    studentCourseScore = table.Column<int>(type: "int", nullable: false),
                    studentCourseRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_failedCourses", x => new { x.studentId, x.courseId });
                    table.ForeignKey(
                        name: "FK_failedCourses_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_failedCourses_finalResults_finalResultId",
                        column: x => x.finalResultId,
                        principalTable: "finalResults",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_failedCourses_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_failedCourses_courseId",
                table: "failedCourses",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_failedCourses_finalResultId",
                table: "failedCourses",
                column: "finalResultId");

            migrationBuilder.CreateIndex(
                name: "IX_finalResults_studentId",
                table: "finalResults",
                column: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "failedCourses");

            migrationBuilder.DropTable(
                name: "finalResults");
        }
    }
}
