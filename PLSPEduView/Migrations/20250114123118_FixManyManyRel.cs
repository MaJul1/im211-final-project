using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLSPEduView.Migrations
{
    /// <inheritdoc />
    public partial class FixManyManyRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSkills_Skills_SkillsId",
                table: "StudentSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSkills_Students_StudentsId",
                table: "StudentSkills");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseCode",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentSkills",
                newName: "SkillId");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "StudentSkills",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSkills_StudentsId",
                table: "StudentSkills",
                newName: "IX_StudentSkills_SkillId");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentCourses",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "StudentCourses",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentsId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSkills_Skills_SkillId",
                table: "StudentSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSkills_Students_StudentId",
                table: "StudentSkills",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSkills_Skills_SkillId",
                table: "StudentSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSkills_Students_StudentId",
                table: "StudentSkills");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "StudentSkills",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentSkills",
                newName: "SkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSkills_SkillId",
                table: "StudentSkills",
                newName: "IX_StudentSkills_StudentsId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "StudentCourses",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentCourses",
                newName: "CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCode",
                table: "Courses",
                column: "CourseCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSkills_Skills_SkillsId",
                table: "StudentSkills",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSkills_Students_StudentsId",
                table: "StudentSkills",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
