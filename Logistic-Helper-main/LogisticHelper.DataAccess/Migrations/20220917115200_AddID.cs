using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticHelper.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_TERC",
                table: "Tercs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tercs",
                table: "Tercs",
                column: "ID_TERC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tercs",
                table: "Tercs");

            migrationBuilder.DropColumn(
                name: "ID_TERC",
                table: "Tercs");
        }
    }
}
