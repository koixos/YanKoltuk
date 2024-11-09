using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YanKoltukBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Driver_DriverId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Stewardess_StewardessId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Stewardess");

            migrationBuilder.DropIndex(
                name: "IX_Service_DriverId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_StewardessId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "StewardessId",
                table: "Service",
                newName: "StewardessIdNo");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Service",
                newName: "DriverIdNo");

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "Service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverPhone",
                table: "Service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverPhoto",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StewardessName",
                table: "Service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StewardessPhone",
                table: "Service",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StewardessPhoto",
                table: "Service",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "DriverPhone",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "DriverPhoto",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StewardessName",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StewardessPhone",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StewardessPhoto",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "StewardessIdNo",
                table: "Service",
                newName: "StewardessId");

            migrationBuilder.RenameColumn(
                name: "DriverIdNo",
                table: "Service",
                newName: "DriverId");

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stewardess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stewardess", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_DriverId",
                table: "Service",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StewardessId",
                table: "Service",
                column: "StewardessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Driver_DriverId",
                table: "Service",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Stewardess_StewardessId",
                table: "Service",
                column: "StewardessId",
                principalTable: "Stewardess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
