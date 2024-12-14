using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProgram.Data.Migrations
{
    /// <inheritdoc />
    public partial class Teacherasdefaultidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f862218d-8e1a-4855-9f6c-0e752fd18064", "AQAAAAIAAYagAAAAEM1WfZDdd3m1BmGST8T/66YzKXFopmNqkr0am0VlwmSAHQH+360TR4R2fPshSXQ6Qg==", "e2af68ef-3a10-4350-b531-4659d16bab8e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14cb6b3b-48a1-4142-a63a-c5a8cefc09b9", "AQAAAAIAAYagAAAAELd4DZcyuZXewzOgVQFs7oQzewmmCqTnj1voi4/syantMUIspF3sPzUyoK7P24ZA/g==", "a5eb2211-ba78-47dd-9bfd-92269c1e2d41" });
        }
    }
}
