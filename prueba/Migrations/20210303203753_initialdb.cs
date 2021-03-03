using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace prueba.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Clientes",
                columns: table => new
                {
                    ID_Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Cedula = table.Column<double>(type: "float", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Clientes", x => x.ID_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "T_Listas",
                columns: table => new
                {
                    ID_lista = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Descripcion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Listas", x => x.ID_lista);
                });

            migrationBuilder.CreateTable(
                name: "T_ListasClientes",
                columns: table => new
                {
                    ID_ListaCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    ID_Cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Lista = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ListasClientes", x => x.ID_ListaCliente);
                    table.ForeignKey(
                        name: "FK_T_ListasClientes_T_Clientes_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "T_Clientes",
                        principalColumn: "ID_Cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_ListasClientes_T_Listas_ID_Lista",
                        column: x => x.ID_Lista,
                        principalTable: "T_Listas",
                        principalColumn: "ID_lista",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "T_Listas",
                columns: new[] { "ID_lista", "Descripcion", "Duracion" },
                values: new object[] { new Guid("8bf1a02f-548f-474e-b19b-05aed0b3befa"), "Lista 1", 2 });

            migrationBuilder.InsertData(
                table: "T_Listas",
                columns: new[] { "ID_lista", "Descripcion", "Duracion" },
                values: new object[] { new Guid("f25a4403-b412-4cf2-b6db-395a61bc7c49"), "Lista 2", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_T_ListasClientes_ID_Cliente",
                table: "T_ListasClientes",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_T_ListasClientes_ID_Lista",
                table: "T_ListasClientes",
                column: "ID_Lista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ListasClientes");

            migrationBuilder.DropTable(
                name: "T_Clientes");

            migrationBuilder.DropTable(
                name: "T_Listas");
        }
    }
}
