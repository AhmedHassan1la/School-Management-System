using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagmen.Migrations
{
    /// <inheritdoc />
    public partial class applicationUserisdisabled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67673230-c86c-487b-aa1f-1b14e99b0a61",
                columns: new[] { "IsDisabled", "PasswordHash" },
                values: new object[] { false, "AQAAAAIAAYagAAAAEHuv2bYEituDXKXteB27y+QYvCFa+/0D1/noJXMT6FuP9ZRcv6yU81h8b4psNY5B5w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67673230-c86c-487b-aa1f-1b14e99b0a61",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJc9NPnF6hTQlZZIYNSmNj8wlmmZFBHO1KfuOxIKgxBpCg+tSQznAl21qW1cSMj8Wg==");
        }
    }
}
