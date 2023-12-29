using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalBlog.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addNullableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f7c4ace-7e1b-4370-a0ec-671061377756");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7743c921-aca4-4111-a929-0df16d966fd5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f639d58-7d27-4d02-bc30-ec34b56e7702", null, "author", "AUTHOR" },
                    { "953a5396-9134-43f0-85e6-c45baefd898d", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f639d58-7d27-4d02-bc30-ec34b56e7702");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "953a5396-9134-43f0-85e6-c45baefd898d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f7c4ace-7e1b-4370-a0ec-671061377756", null, "author", "AUTHOR" },
                    { "7743c921-aca4-4111-a929-0df16d966fd5", null, "user", "USER" }
                });
        }
    }
}
