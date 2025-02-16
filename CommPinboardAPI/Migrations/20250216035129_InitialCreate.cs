using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommPinboardAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ExternalId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ExternalId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserExternalId",
                        column: x => x.UserExternalId,
                        principalTable: "Users",
                        principalColumn: "ExternalId");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ExternalId);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostExternalId",
                        column: x => x.PostExternalId,
                        principalTable: "Posts",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserExternalId",
                        column: x => x.UserExternalId,
                        principalTable: "Users",
                        principalColumn: "ExternalId");
                });

            migrationBuilder.CreateTable(
                name: "PinnedPosts",
                columns: table => new
                {
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PinnedPostId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinnedPosts", x => x.ExternalId);
                    table.ForeignKey(
                        name: "FK_PinnedPosts_Posts_PostExternalId",
                        column: x => x.PostExternalId,
                        principalTable: "Posts",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PinnedPosts_Users_UserExternalId",
                        column: x => x.UserExternalId,
                        principalTable: "Users",
                        principalColumn: "ExternalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostExternalId",
                table: "Comments",
                column: "PostExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserExternalId",
                table: "Comments",
                column: "UserExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedPosts_PostExternalId",
                table: "PinnedPosts",
                column: "PostExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PinnedPosts_UserExternalId",
                table: "PinnedPosts",
                column: "UserExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserExternalId",
                table: "Posts",
                column: "UserExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PinnedPosts");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
