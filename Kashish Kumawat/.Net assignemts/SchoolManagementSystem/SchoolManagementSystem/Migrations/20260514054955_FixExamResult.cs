using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixExamResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classroom",
                columns: table => new
                {
                    classroom_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    year = table.Column<int>(type: "int", nullable: true),
                    section = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    remarks = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__classroo__448E90B86B6189E7", x => x.classroom_id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    description = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__course__8F1EF7AE6E78DD1D", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "exam_type",
                columns: table => new
                {
                    exam_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    desc = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__exam_typ__FAB1396DE961C7A2", x => x.exam_type_id);
                });

            migrationBuilder.CreateTable(
                name: "parent",
                columns: table => new
                {
                    parent_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    fname = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    lname = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    mobile = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    last_login_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__parent__F2A60819B34729B1", x => x.parent_id);
                });

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    exam_type_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__exam__9C8C7BE993F1FAAC", x => x.exam_id);
                    table.ForeignKey(
                        name: "FK__exam__exam_type___6A30C649",
                        column: x => x.exam_type_id,
                        principalTable: "exam_type",
                        principalColumn: "exam_type_id");
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    password = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    fname = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    lname = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    mobile = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    date_of_join = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student__2A33069A0662101F", x => x.student_id);
                    table.ForeignKey(
                        name: "FK__student__parent___60A75C0F",
                        column: x => x.parent_id,
                        principalTable: "parent",
                        principalColumn: "parent_id");
                });

            migrationBuilder.CreateTable(
                name: "exam_result",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    marks = table.Column<int>(type: "int", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__exam_res__81A05577F94738FF", x => new { x.exam_id, x.student_id, x.course_id });
                    table.ForeignKey(
                        name: "FK__exam_resu__cours__6EF57B66",
                        column: x => x.course_id,
                        principalTable: "course",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK__exam_resu__exam___6D0D32F4",
                        column: x => x.exam_id,
                        principalTable: "exam",
                        principalColumn: "exam_id");
                    table.ForeignKey(
                        name: "FK__exam_resu__stude__6E01572D",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "student_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_exam_exam_type_id",
                table: "exam",
                column: "exam_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_course_id",
                table: "exam_result",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_exam_result_student_id",
                table: "exam_result",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_parent_id",
                table: "student",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "classroom");

            migrationBuilder.DropTable(
                name: "exam_result");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "exam_type");

            migrationBuilder.DropTable(
                name: "parent");
        }
    }
}
