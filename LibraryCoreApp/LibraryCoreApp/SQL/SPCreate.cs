using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCoreApp.SQL
{
    public class SPCreate : Migration
    {
        private const string MIGRATION_SQL_SCRIPT_FILE_NAME = @"SQL\SPCreate.sql";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, MIGRATION_SQL_SCRIPT_FILE_NAME);
            migrationBuilder.Sql(File.ReadAllText(sql));
        //    var sp = @"CREATE PROCEDURE [dbo].[UserLogin]
        //@Email nvarchar(max),
        //@Password nvarchar(15)
        //AS
        //BEGIN
        //    select * from Users where Email = @Email and Password = @Password
        //END";

        //    migrationBuilder.Sql(sp);

            //Insert Admin record
            //var record = 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //...
        }
    }
}
