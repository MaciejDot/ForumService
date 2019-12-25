using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Forum");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "Thumbnails",
                schema: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<byte[]>(maxLength: 256000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thumbnail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                schema: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Descriprion = table.Column<string>(maxLength: 3000, nullable: false),
                    ThumbnailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Thumbnails",
                        column: x => x.ThumbnailId,
                        principalSchema: "Forum",
                        principalTable: "Thumbnails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                schema: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Question = table.Column<string>(maxLength: 4000, nullable: false),
                    AuthorId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thread_Author",
                        column: x => x.AuthorId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thread_Subjects",
                        column: x => x.SubjectId,
                        principalSchema: "Forum",
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Forum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answear = table.Column<string>(maxLength: 4000, nullable: true),
                    AuthorId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ThreadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Author",
                        column: x => x.AuthorId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Thread",
                        column: x => x.ThreadId,
                        principalSchema: "Forum",
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                schema: "Forum",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Created",
                schema: "Forum",
                table: "Posts",
                column: "Created",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadId",
                schema: "Forum",
                table: "Posts",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ThumbnailId",
                schema: "Forum",
                table: "Subjects",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectName",
                schema: "Forum",
                table: "Subjects",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_AuthorId",
                schema: "Forum",
                table: "Threads",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Thread_Created",
                schema: "Forum",
                table: "Threads",
                column: "Created",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thread_SubjectId",
                schema: "Forum",
                table: "Threads",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Forum");

            migrationBuilder.DropTable(
                name: "Threads",
                schema: "Forum");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Subjects",
                schema: "Forum");

            migrationBuilder.DropTable(
                name: "Thumbnails",
                schema: "Forum");
        }
    }
}
