using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DECLARE @sql NVARCHAR(MAX) = N'';\n" +
                             "SELECT @sql += 'DROP TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ';' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';\n" +
                             "EXEC(@sql);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Optionally, you can provide a method to revert the migration, but it's not possible to automatically recreate the tables with EF Core migrations.
        }

    }
}
