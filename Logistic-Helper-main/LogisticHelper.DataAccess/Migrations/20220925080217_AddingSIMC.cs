using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticHelper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingSIMC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Simcs",
                columns: table => new
                {
                    SYM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WOJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GMI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RODZ_GMI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAZWA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SYMPOD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STAN_NA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simcs", x => x.SYM);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Simcs");
        }
    }
}
