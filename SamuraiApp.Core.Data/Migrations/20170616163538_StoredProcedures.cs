using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Core.Data.Migrations
{
    public partial class StoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE FilterSamuraiByNamePart
                    @namepart varchar(50) 
                    AS 
                    SELECT * FROM Samurais 
                        WHERE Name LIKE '%'+@namepart+'%'"
            );
            migrationBuilder.Sql(
                @"CREATE PROCEDURE FindLongestName
                    @result varchar(50) OUTPUT
                    AS 
                    BEGIN
                    SET NOCOUNT ON;
                    SELECT TOP 1 @result = Name FROM Samurais ORDER BY len(Name) DESC
                    END"
            );
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE PROCEDURE FilterSamuraiByNamePart");
            migrationBuilder.Sql(@"DELETE PROCEDURE FindLongestName");            
        }
    }
}
