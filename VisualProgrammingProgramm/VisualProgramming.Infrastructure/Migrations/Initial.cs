using Microsoft.EntityFrameworkCore.Migrations;

namespace VisualProgramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    TypeOperation = table.Column<int>(type: "integer", nullable: true),
                    GrafOperationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grafs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grafs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypePort = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ports_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementGrafs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: true),
                    LevelLevelOfDepthOperation = table.Column<int>(type: "integer", nullable: false),
                    IsModul = table.Column<bool>(type: "boolean", nullable: false),
                    ParentGrafId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionX = table.Column<double>(type: "double precision", nullable: false),
                    PositionY = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementGrafs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementGrafs_Grafs_ParentGrafId",
                        column: x => x.ParentGrafId,
                        principalTable: "Grafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementGrafs_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NodePortConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PortId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodePortConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodePortConnections_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodePortConnections_Ports_PortId",
                        column: x => x.PortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourcePortId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetPortId = table.Column<Guid>(type: "uuid", nullable: false),
                    InElementGrafId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutElementGrafId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_ElementGrafs_InElementGrafId",
                        column: x => x.InElementGrafId,
                        principalTable: "ElementGrafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_ElementGrafs_OutElementGrafId",
                        column: x => x.OutElementGrafId,
                        principalTable: "ElementGrafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Ports_SourcePortId",
                        column: x => x.SourcePortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Ports_TargetPortId",
                        column: x => x.TargetPortId,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Индексы для оптимизации запросов
            migrationBuilder.CreateIndex(
                name: "IX_Grafs_ProjectId",
                table: "Grafs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementGrafs_NodeId",
                table: "ElementGrafs",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementGrafs_ParentGrafId",
                table: "ElementGrafs",
                column: "ParentGrafId");

            migrationBuilder.CreateIndex(
                name: "IX_Ports_NodeId",
                table: "Ports",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodePortConnections_NodeId",
                table: "NodePortConnections",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodePortConnections_PortId",
                table: "NodePortConnections",
                column: "PortId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_SourcePortId",
                table: "Connections",
                column: "SourcePortId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_TargetPortId",
                table: "Connections",
                column: "TargetPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_InElementGrafId",
                table: "Connections",
                column: "InElementGrafId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_OutElementGrafId",
                table: "Connections",
                column: "OutElementGrafId");

            // Дополнительный индекс для Modul, если нужно ускорить поиск по GrafOperationId
            migrationBuilder.CreateIndex(
                name: "IX_Nodes_GrafOperationId",
                table: "Nodes",
                column: "GrafOperationId");

            // Внешний ключ для Modul.GrafOperationId (добавляется отдельно, т.к. таблица Nodes уже создана)
            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Grafs_GrafOperationId",
                table: "Nodes",
                column: "GrafOperationId",
                principalTable: "Grafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаление внешнего ключа для Modul
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Grafs_GrafOperationId",
                table: "Nodes");

            // Удаление индексов
            migrationBuilder.DropIndex(
                name: "IX_Nodes_GrafOperationId",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Connections_OutElementGrafId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_InElementGrafId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_TargetPortId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_SourcePortId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_NodePortConnections_PortId",
                table: "NodePortConnections");

            migrationBuilder.DropIndex(
                name: "IX_NodePortConnections_NodeId",
                table: "NodePortConnections");

            migrationBuilder.DropIndex(
                name: "IX_Ports_NodeId",
                table: "Ports");

            migrationBuilder.DropIndex(
                name: "IX_ElementGrafs_ParentGrafId",
                table: "ElementGrafs");

            migrationBuilder.DropIndex(
                name: "IX_ElementGrafs_NodeId",
                table: "ElementGrafs");

            migrationBuilder.DropIndex(
                name: "IX_Grafs_ProjectId",
                table: "Grafs");

            // Удаление таблиц
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "NodePortConnections");

            migrationBuilder.DropTable(
                name: "ElementGrafs");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "Grafs");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}