using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticHelper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class tercFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tercs",
                columns: table => new
                {
                    WOJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POW = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GMI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RODZ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAZWA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAZWA_DOD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STAN_NA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tercs");

        }
    }
}
