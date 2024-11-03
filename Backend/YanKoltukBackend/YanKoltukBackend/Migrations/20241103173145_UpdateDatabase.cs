using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YanKoltukBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_Id",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Admins_AdminId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_Id",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Users_Id",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLogs_Services_ServiceId",
                table: "ServiceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLogs_Students_StudentId",
                table: "ServiceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Drivers_DriverId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Managers_ManagerId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Stewardesses_StewardessId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_Id",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentServices_Services_ServiceId",
                table: "StudentServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentServices_Students_StudentId",
                table: "StudentServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentServices",
                table: "StudentServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stewardesses",
                table: "Stewardesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceLogs",
                table: "ServiceLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parents",
                table: "Parents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "StudentServices",
                newName: "StudentService");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Stewardesses",
                newName: "Stewardess");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "ServiceLogs",
                newName: "ServiceLog");

            migrationBuilder.RenameTable(
                name: "Parents",
                newName: "Parent");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Driver");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admin");

            migrationBuilder.RenameIndex(
                name: "IX_StudentServices_StudentId",
                table: "StudentService",
                newName: "IX_StudentService_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentServices_ServiceId",
                table: "StudentService",
                newName: "IX_StudentService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ParentId",
                table: "Student",
                newName: "IX_Student_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_StewardessId",
                table: "Service",
                newName: "IX_Service_StewardessId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ManagerId",
                table: "Service",
                newName: "IX_Service_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_DriverId",
                table: "Service",
                newName: "IX_Service_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceLogs_StudentId",
                table: "ServiceLog",
                newName: "IX_ServiceLog_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceLogs_ServiceId",
                table: "ServiceLog",
                newName: "IX_ServiceLog_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_AdminId",
                table: "Manager",
                newName: "IX_Manager_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentService",
                table: "StudentService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stewardess",
                table: "Stewardess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceLog",
                table: "ServiceLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parent",
                table: "Parent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Driver",
                table: "Driver",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_User_Id",
                table: "Admin",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Admin_AdminId",
                table: "Manager",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_User_Id",
                table: "Manager",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parent_User_Id",
                table: "Parent",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Driver_DriverId",
                table: "Service",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Manager_ManagerId",
                table: "Service",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Stewardess_StewardessId",
                table: "Service",
                column: "StewardessId",
                principalTable: "Stewardess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_User_Id",
                table: "Service",
                column: "Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLog_Service_ServiceId",
                table: "ServiceLog",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLog_Student_StudentId",
                table: "ServiceLog",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Parent_ParentId",
                table: "Student",
                column: "ParentId",
                principalTable: "Parent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentService_Service_ServiceId",
                table: "StudentService",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentService_Student_StudentId",
                table: "StudentService",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_User_Id",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Admin_AdminId",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_User_Id",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Parent_User_Id",
                table: "Parent");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Driver_DriverId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Manager_ManagerId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Stewardess_StewardessId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_User_Id",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLog_Service_ServiceId",
                table: "ServiceLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLog_Student_StudentId",
                table: "ServiceLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Parent_ParentId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentService_Service_ServiceId",
                table: "StudentService");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentService_Student_StudentId",
                table: "StudentService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentService",
                table: "StudentService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stewardess",
                table: "Stewardess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceLog",
                table: "ServiceLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parent",
                table: "Parent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Driver",
                table: "Driver");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "StudentService",
                newName: "StudentServices");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Stewardess",
                newName: "Stewardesses");

            migrationBuilder.RenameTable(
                name: "ServiceLog",
                newName: "ServiceLogs");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "Parent",
                newName: "Parents");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.RenameTable(
                name: "Driver",
                newName: "Drivers");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Admins");

            migrationBuilder.RenameIndex(
                name: "IX_StudentService_StudentId",
                table: "StudentServices",
                newName: "IX_StudentServices_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentService_ServiceId",
                table: "StudentServices",
                newName: "IX_StudentServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_ParentId",
                table: "Students",
                newName: "IX_Students_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceLog_StudentId",
                table: "ServiceLogs",
                newName: "IX_ServiceLogs_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceLog_ServiceId",
                table: "ServiceLogs",
                newName: "IX_ServiceLogs_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_StewardessId",
                table: "Services",
                newName: "IX_Services_StewardessId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ManagerId",
                table: "Services",
                newName: "IX_Services_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_DriverId",
                table: "Services",
                newName: "IX_Services_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Manager_AdminId",
                table: "Managers",
                newName: "IX_Managers_AdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentServices",
                table: "StudentServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stewardesses",
                table: "Stewardesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceLogs",
                table: "ServiceLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parents",
                table: "Parents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_Id",
                table: "Admins",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Admins_AdminId",
                table: "Managers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_Id",
                table: "Managers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Users_Id",
                table: "Parents",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLogs_Services_ServiceId",
                table: "ServiceLogs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLogs_Students_StudentId",
                table: "ServiceLogs",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Drivers_DriverId",
                table: "Services",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Managers_ManagerId",
                table: "Services",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Stewardesses_StewardessId",
                table: "Services",
                column: "StewardessId",
                principalTable: "Stewardesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_Id",
                table: "Services",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentId",
                table: "Students",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentServices_Services_ServiceId",
                table: "StudentServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentServices_Students_StudentId",
                table: "StudentServices",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
