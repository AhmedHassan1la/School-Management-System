using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolManagmen.Migrations
{
    /// <inheritdoc />
    public partial class seedidentitytabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60588eb9-1a69-4794-afe6-47f1edd8a3f4", "fdf628eb-27b3-424e-b21a-29f4270bd15d", false, false, "Admin", "ADMIN" },
                    { "98906716-db5b-4520-b554-4ee0e751e6f2", "ab98841f-1c69-4025-83ba-2f1ddd0f81c5", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67673230-c86c-487b-aa1f-1b14e99b0a61", 0, "409b5734-71c7-401d-b38b-4ae56dc4e62c", "admin@SchoolManagmen.com", true, "SchoolManagmen", "Admin", false, null, "ADMIN@SCHOOLMANAGMEN.COM", "ADMIN@SCHOOLMANAGMEN.COM", "AQAAAAIAAYagAAAAEJc9NPnF6hTQlZZIYNSmNj8wlmmZFBHO1KfuOxIKgxBpCg+tSQznAl21qW1cSMj8Wg==", null, false, "ddc9ad66e3e9461292f482980bbf7228", false, "admin@SchoolManagmen.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "teachers:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 2, "permissions", "teachers:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 3, "permissions", "teachers:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 4, "permissions", "teachers:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 5, "permissions", "subjects:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 6, "permissions", "subjects:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 7, "permissions", "subjects:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 8, "permissions", "subjects:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 9, "permissions", "students:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 10, "permissions", "students:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 11, "permissions", "students:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 12, "permissions", "students:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 13, "permissions", "gradeReports:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 14, "permissions", "gradeReports:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 15, "permissions", "gradeReports:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 16, "permissions", "gradeReports:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 17, "permissions", "enrollments:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 18, "permissions", "enrollments:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 19, "permissions", "enrollments:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 20, "permissions", "enrollments:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 21, "permissions", "classes:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 22, "permissions", "classes:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 23, "permissions", "classes:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 24, "permissions", "classes:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 25, "permissions", "courses:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 26, "permissions", "courses:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 27, "permissions", "courses:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 28, "permissions", "courses:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 29, "permissions", "attendance:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 30, "permissions", "attendance:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 31, "permissions", "attendance:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 32, "permissions", "attendance:delete", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 33, "permissions", "users:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 34, "permissions", "users:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 35, "permissions", "users:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 36, "permissions", "roles:read", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 37, "permissions", "roles:add", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" },
                    { 38, "permissions", "roles:update", "60588eb9-1a69-4794-afe6-47f1edd8a3f4" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "60588eb9-1a69-4794-afe6-47f1edd8a3f4", "67673230-c86c-487b-aa1f-1b14e99b0a61" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98906716-db5b-4520-b554-4ee0e751e6f2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "60588eb9-1a69-4794-afe6-47f1edd8a3f4", "67673230-c86c-487b-aa1f-1b14e99b0a61" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60588eb9-1a69-4794-afe6-47f1edd8a3f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67673230-c86c-487b-aa1f-1b14e99b0a61");
        }
    }
}
