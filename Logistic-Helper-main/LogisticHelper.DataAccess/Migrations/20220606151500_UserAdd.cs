using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticHelper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAZWISKO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMIE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA_URODZENIA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFON = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
