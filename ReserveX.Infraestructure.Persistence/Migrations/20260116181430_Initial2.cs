using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReserveX.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "LastName", "Name", "PasswordHash", "Role", "Status" },
                values: new object[] { new Guid("3edaf5d5-2967-433d-af4f-c8ff2e29379d"), new DateTime(2026, 1, 16, 13, 14, 29, 308, DateTimeKind.Local).AddTicks(804), "johalypolanco13@gmail.com", "Concepcion", "Johaly", "$2b$12$p5ZtMFQRYB7y0wi/BGOOP.VxMAQP70mh4kVInaGTGj4R2ce0XZZHm", 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("3edaf5d5-2967-433d-af4f-c8ff2e29379d"));
        }
    }
}
