using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Palladin.Data.EntityFramework.Migrations
{
    public partial class initialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Path = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Archive = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MethodProtocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    ProjectType = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodProtocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenusItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    MenuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenusItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenusItem_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    Login = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<short>(type: "smallint", nullable: false),
                    MenuId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InitialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectType = table.Column<short>(type: "smallint", nullable: false),
                    Subsidiary = table.Column<string>(type: "varchar(200)", nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenus",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMenus", x => new { x.MenuId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenus_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vulnerabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", nullable: false),
                    References = table.Column<string>(type: "text", nullable: false),
                    CVSS = table.Column<string>(type: "text", nullable: true),
                    RiskFactor = table.Column<short>(type: "smallint", nullable: false),
                    ProjectType = table.Column<short>(type: "smallint", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vulnerabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vulnerabilities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsVulnerability",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Environment = table.Column<string>(type: "varchar(200)", nullable: false),
                    Port = table.Column<string>(type: "varchar(50)", nullable: true),
                    FiledOrCookieName = table.Column<string>(nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    MethodProtocolId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    VulnerabilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsVulnerability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectsVulnerability_MethodProtocols_MethodProtocolId",
                        column: x => x.MethodProtocolId,
                        principalTable: "MethodProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsVulnerability_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsVulnerability_Vulnerabilities_VulnerabilityId",
                        column: x => x.VulnerabilityId,
                        principalTable: "Vulnerabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VulnerabilityLangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LanguageType = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Remediation = table.Column<string>(type: "text", nullable: false),
                    VulnerabilityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VulnerabilityLangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VulnerabilityLangs_Vulnerabilities_VulnerabilityId",
                        column: x => x.VulnerabilityId,
                        principalTable: "Vulnerabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaProjectVultis",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(nullable: false),
                    ProjectVulnerabilityId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaProjectVultis", x => new { x.MediaId, x.ProjectVulnerabilityId });
                    table.ForeignKey(
                        name: "FK_MediaProjectVultis_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaProjectVultis_ProjectsVulnerability_ProjectVulnerabilityId",
                        column: x => x.ProjectVulnerabilityId,
                        principalTable: "ProjectsVulnerability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ceac4f1e-8749-46a7-9ffb-a4996cc2f4f8"), "Client" },
                    { new Guid("aa9a13d4-ac37-49bc-b821-6658aac46df3"), "eSecurity" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Login", "MenuId", "Password", "UserType" },
                values: new object[] { new Guid("747cbe55-8f74-474b-af7f-cd3585eff2aa"), new DateTime(2019, 5, 31, 18, 30, 48, 914, DateTimeKind.Local).AddTicks(5764), "cliente", null, "5ivWjl+ZjGohSxB1pb/U+w==", (short)1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Login", "MenuId", "Password", "UserType" },
                values: new object[] { new Guid("b43a265f-9638-495e-b589-91cb5a51c0ca"), new DateTime(2019, 5, 28, 18, 30, 48, 915, DateTimeKind.Local).AddTicks(6958), "esec", null, "TKrBGmPmSUdIcUBSa1x0+g==", (short)0 });

            migrationBuilder.InsertData(
                table: "MenusItem",
                columns: new[] { "Id", "MenuId", "Name", "Path" },
                values: new object[,]
                {
                    { new Guid("871ccebc-fd54-4194-9358-9ca96c34d882"), new Guid("ceac4f1e-8749-46a7-9ffb-a4996cc2f4f8"), "Projetos", "/project" },
                    { new Guid("734aaa33-b704-4d2c-858b-8069ec6c4898"), new Guid("ceac4f1e-8749-46a7-9ffb-a4996cc2f4f8"), "Comparar Projetos", "/compare" },
                    { new Guid("7bb67908-a4e6-47a9-99a6-c8649051aa29"), new Guid("aa9a13d4-ac37-49bc-b821-6658aac46df3"), "Projetos", "/admin/project" },
                    { new Guid("743c15b3-323e-4766-a970-fb2f3cbc7597"), new Guid("aa9a13d4-ac37-49bc-b821-6658aac46df3"), "Vulnerabilidades", "/admin/vulnerability" },
                    { new Guid("4439216d-fa62-4e1a-a36e-f5e4d2aca22e"), new Guid("aa9a13d4-ac37-49bc-b821-6658aac46df3"), "Vinculação", "/admin/join-project" }
                });

            migrationBuilder.InsertData(
                table: "UserMenus",
                columns: new[] { "MenuId", "UserId" },
                values: new object[,]
                {
                    { new Guid("ceac4f1e-8749-46a7-9ffb-a4996cc2f4f8"), new Guid("747cbe55-8f74-474b-af7f-cd3585eff2aa") },
                    { new Guid("aa9a13d4-ac37-49bc-b821-6658aac46df3"), new Guid("b43a265f-9638-495e-b589-91cb5a51c0ca") }
                });

            migrationBuilder.InsertData(
                table: "Vulnerabilities",
                columns: new[] { "Id", "CVSS", "CreatedDate", "IsDeleted", "Name", "ProjectType", "References", "RiskFactor", "Tags", "UserId" },
                values: new object[] { new Guid("96dcd4b6-2676-4947-90a4-d5005c8e6b77"), "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H", new DateTime(2019, 8, 29, 18, 30, 48, 916, DateTimeKind.Local).AddTicks(5341), false, "Weak Password Policy", (short)0, "http://www.owasp.org/index.php/Testing_for_Weak_password_policy", (short)3, "password,weak-password,weak-password-policy,password-policy", new Guid("b43a265f-9638-495e-b589-91cb5a51c0ca") });

            migrationBuilder.InsertData(
                table: "VulnerabilityLangs",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "LanguageType", "Remediation", "VulnerabilityId" },
                values: new object[] { new Guid("0732a356-08a7-4854-9b66-275f292430f2"), new DateTime(2019, 8, 29, 18, 30, 48, 916, DateTimeKind.Local).AddTicks(9134), @"A aplicação não exige que os usuários tenham senhas fortes. A falta de complexidade de senha reduz significamente o espaço de busca ao tentar adivinhar as senhas dos usuários, facilitando ataques de força bruta.
                    Dessa forma,
                    foi possível obter acesso ao sistema utilizando uma conta de usuário que possui senha fraca e de fácil adivinhação.A partir da conta acessada,
                    uma nova conta foi criada.", false, (short)0, @"Introduza uma política de senha forte (que garanta o tamanho, a complexidade, a reutilização e o envelhecimento da senha) e/ou
                    controles de autenticação adicionais (duplo fator de autenticação).", new Guid("96dcd4b6-2676-4947-90a4-d5005c8e6b77") });

            migrationBuilder.CreateIndex(
                name: "IX_MediaProjectVultis_ProjectVulnerabilityId",
                table: "MediaProjectVultis",
                column: "ProjectVulnerabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MenusItem_MenuId",
                table: "MenusItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_MethodProtocolId",
                table: "ProjectsVulnerability",
                column: "MethodProtocolId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_ProjectId",
                table: "ProjectsVulnerability",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_VulnerabilityId",
                table: "ProjectsVulnerability",
                column: "VulnerabilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMenus_UserId",
                table: "UserMenus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MenuId",
                table: "Users",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Vulnerabilities_UserId",
                table: "Vulnerabilities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VulnerabilityLangs_VulnerabilityId",
                table: "VulnerabilityLangs",
                column: "VulnerabilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaProjectVultis");

            migrationBuilder.DropTable(
                name: "MenusItem");

            migrationBuilder.DropTable(
                name: "UserMenus");

            migrationBuilder.DropTable(
                name: "VulnerabilityLangs");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "ProjectsVulnerability");

            migrationBuilder.DropTable(
                name: "MethodProtocols");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Vulnerabilities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
