using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProgram.Data.Migrations
{
    /// <inheritdoc />
    public partial class expected_answers_text : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExceptedAnswer",
                table: "Questions",
                newName: "ExpectedAnswer");

            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe1c9196-6fa2-4f43-928a-0ad11efbcbae", "AQAAAAIAAYagAAAAEGXMOrdbzJuJLV+lt2PkQ2Y57ToosNmJbos5HtQMMlHpR+QOPyUcSsa+GYa6wQyZlA==", "6c0eb1e3-3558-452c-aed8-b9888c122c6e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedAnswer",
                table: "Questions",
                newName: "ExceptedAnswer");

            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2f11637-eed5-4de1-a289-2bd552acc7b5", "AQAAAAIAAYagAAAAEOJdKdX+ylFFmD0eaUJh1m8uDTviQ1lUGF2OpvSRI2t68QO9Nl1p2kdcOuafwA4D4g==", "20b98aff-4bac-4056-8237-f7e437f953a0" });
        }
    }
}
