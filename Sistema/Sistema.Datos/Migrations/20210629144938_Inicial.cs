using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sistema.Datos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gasto",
                columns: table => new
                {
                    idgasto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    codigo = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(nullable: false),
                    condicion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gasto", x => x.idgasto);
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    idpersona = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tipo_persona = table.Column<string>(nullable: false),
                    nombre = table.Column<string>(maxLength: 100, nullable: false),
                    tipo_documento = table.Column<string>(nullable: true),
                    num_documento = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true),
                    telefono = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona", x => x.idpersona);
                });

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    idrol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(maxLength: 30, nullable: false),
                    descripcion = table.Column<string>(maxLength: 256, nullable: true),
                    condicion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.idrol);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    idusuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idrol = table.Column<int>(nullable: false),
                    nombre = table.Column<string>(maxLength: 100, nullable: false),
                    tipo_documento = table.Column<string>(nullable: true),
                    num_documento = table.Column<string>(nullable: true),
                    direccion = table.Column<string>(nullable: true),
                    telefono = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: false),
                    password_hash = table.Column<byte[]>(nullable: false),
                    password_salt = table.Column<byte[]>(nullable: false),
                    condicion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.idusuario);
                    table.ForeignKey(
                        name: "FK_usuario_rol_idrol",
                        column: x => x.idrol,
                        principalTable: "rol",
                        principalColumn: "idrol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cartera",
                columns: table => new
                {
                    idcartera = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idcliente = table.Column<int>(nullable: false),
                    idusuario = table.Column<int>(nullable: false),
                    serie_comprobante = table.Column<string>(nullable: true),
                    num_comprobante = table.Column<string>(nullable: false),
                    fecha_emision = table.Column<DateTime>(nullable: false),
                    fecha_pago = table.Column<DateTime>(nullable: false),
                    fecha_descuento = table.Column<DateTime>(nullable: false),
                    moneda = table.Column<string>(nullable: false),
                    tipo_tasa = table.Column<string>(nullable: true),
                    tasa = table.Column<decimal>(nullable: false),
                    capaitalizacion = table.Column<string>(nullable: true),
                    valor_entregado = table.Column<decimal>(nullable: false),
                    valor_recibido = table.Column<decimal>(nullable: false),
                    valor_nominal = table.Column<decimal>(nullable: false),
                    valor_neto = table.Column<decimal>(nullable: false),
                    TCEA = table.Column<decimal>(nullable: false),
                    estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartera", x => x.idcartera);
                    table.ForeignKey(
                        name: "FK_cartera_persona_idcliente",
                        column: x => x.idcliente,
                        principalTable: "persona",
                        principalColumn: "idpersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartera_usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detalle_cartera",
                columns: table => new
                {
                    iddetalle_cartera = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idcartera = table.Column<int>(nullable: false),
                    idgasto = table.Column<int>(nullable: false),
                    valor = table.Column<decimal>(nullable: false),
                    tipo_valor = table.Column<string>(nullable: false),
                    tipo_gasto = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_cartera", x => x.iddetalle_cartera);
                    table.ForeignKey(
                        name: "FK_detalle_cartera_cartera_idcartera",
                        column: x => x.idcartera,
                        principalTable: "cartera",
                        principalColumn: "idcartera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_cartera_gasto_idgasto",
                        column: x => x.idgasto,
                        principalTable: "gasto",
                        principalColumn: "idgasto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartera_idcliente",
                table: "cartera",
                column: "idcliente");

            migrationBuilder.CreateIndex(
                name: "IX_cartera_idusuario",
                table: "cartera",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_cartera_idcartera",
                table: "detalle_cartera",
                column: "idcartera");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_cartera_idgasto",
                table: "detalle_cartera",
                column: "idgasto");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_idrol",
                table: "usuario",
                column: "idrol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_cartera");

            migrationBuilder.DropTable(
                name: "cartera");

            migrationBuilder.DropTable(
                name: "gasto");

            migrationBuilder.DropTable(
                name: "persona");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
