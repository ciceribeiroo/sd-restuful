using Microsoft.EntityFrameworkCore.Migrations;

namespace api_restful.Migrations
{
    public partial class AlterandoNomes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prazo",
                table: "ToDoLists",
                newName: "deadLine");

            migrationBuilder.RenameColumn(
                name: "completa",
                table: "ToDoLists",
                newName: "complete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deadLine",
                table: "ToDoLists",
                newName: "prazo");

            migrationBuilder.RenameColumn(
                name: "complete",
                table: "ToDoLists",
                newName: "completa");
        }
    }
}
