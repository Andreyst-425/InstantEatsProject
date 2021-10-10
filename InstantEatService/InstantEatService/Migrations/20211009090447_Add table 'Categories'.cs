using Microsoft.EntityFrameworkCore.Migrations;

namespace InstantEatService.Migrations
{
    public partial class AddtableCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Id", x => x.Id);
                });

           
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
