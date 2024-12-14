using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProgram.Data.Migrations
{
    /// <inheritdoc />
    public partial class Twofactordisables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2f11637-eed5-4de1-a289-2bd552acc7b5", "AQAAAAIAAYagAAAAEOJdKdX+ylFFmD0eaUJh1m8uDTviQ1lUGF2OpvSRI2t68QO9Nl1p2kdcOuafwA4D4g==", "20b98aff-4bac-4056-8237-f7e437f953a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f862218d-8e1a-4855-9f6c-0e752fd18064", "AQAAAAIAAYagAAAAEM1WfZDdd3m1BmGST8T/66YzKXFopmNqkr0am0VlwmSAHQH+360TR4R2fPshSXQ6Qg==", "e2af68ef-3a10-4350-b531-4659d16bab8e" });
        }
    }
}
