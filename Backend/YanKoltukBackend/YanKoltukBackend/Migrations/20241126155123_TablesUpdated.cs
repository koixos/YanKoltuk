using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YanKoltukBackend.Migrations
{
    /// <inheritdoc />
    public partial class TablesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentService_StudentId",
                table: "StudentService");

            migrationBuilder.CreateIndex(
                name: "IX_StudentService_StudentId",
                table: "StudentService",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentService_StudentId",
                table: "StudentService");

            migrationBuilder.CreateIndex(
                name: "IX_StudentService_StudentId",
                table: "StudentService",
                column: "StudentId");
        }
    }
}
