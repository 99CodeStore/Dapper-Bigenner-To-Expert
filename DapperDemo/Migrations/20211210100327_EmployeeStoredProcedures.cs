using Microsoft.EntityFrameworkCore.Migrations;

namespace DapperDemo.Migrations
{
    public partial class EmployeeStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_CompanyId",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
                CREATE PROC usp_GetCompany
                    @CompanyId int
                AS 
                BEGIN 
                    SELECT *
                    FROM Companies
                    WHERE CompanyId = @CompanyId
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_GetALLCompany
                AS 
                BEGIN 
                    SELECT *
                    FROM Companies
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_AddCompany
                    @CompanyId int OUTPUT,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);
	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_UpdateCompany
	                @CompanyId int,
                    @Name varchar(MAX),
	                @Address  varchar(MAX),
	                @City varchar(MAX),
	                @State varchar(MAX),
	                @PostalCode varchar(MAX)
                AS
                BEGIN 
                    UPDATE Companies  
	                SET 
		                Name = @Name, 
		                Address = @Address,
		                City=@City, 
		                State=@State, 
		                PostalCode=@PostalCode
	                WHERE CompanyId=@CompanyId;
	                SELECT @CompanyId = SCOPE_IDENTITY();
                END
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_RemoveCompany
                    @CompanyId int
                AS 
                BEGIN 
                    DELETE
                    FROM Companies
                    WHERE CompanyId  = @CompanyId
                END
                GO	
            ");

            migrationBuilder.Sql(@"
                CREATE PROC usp_ExistsCompany
                    @CompanyId int
                AS 
                BEGIN 
                    IF (EXISTS(SELECT Name FROM Companies WHERE CompanyId  = @CompanyId))
                    BEGIN
                        SELECT CAST(1 AS BIT)
                    END
                    ELSE  
                    BEGIN
                        SELECT CAST(0 AS BIT)
                    END
                END
                GO	
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                table: "Employee",
                newName: "IX_Employee_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employee",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
