using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Palladin.Data.EntityFramework.Migrations
{
    public partial class RealInitialCatalog : Migration
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
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 200, nullable: false),
                    Archive = table.Column<string>(type: "text", maxLength: 2500, nullable: false)
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
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    Path = table.Column<string>(type: "varchar(50)", nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Menus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(80)", nullable: true),
                    InitialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectType = table.Column<short>(type: "smallint", nullable: false),
                    Subsidiary = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenEntity",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    JwtId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsInvalided = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenEntity", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    Level = table.Column<short>(nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserType = table.Column<short>(type: "smallint", nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Users_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMenuEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FK_Menu_User", x => new { x.MenuId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserMenuEntity_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMenuEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
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
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    References = table.Column<string>(type: "text", nullable: false),
                    CVSS = table.Column<string>(type: "varchar(50)", nullable: true),
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
                    VulnerabilityId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
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
                        name: "FK_ProjectsVulnerability_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsVulnerability_Vulnerabilities_VulnerabilityId",
                        column: x => x.VulnerabilityId,
                        principalTable: "Vulnerabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.PrimaryKey("FK_Media_PV", x => new { x.MediaId, x.ProjectVulnerabilityId });
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
                columns: new[] { "Id", "Name", "Order", "ParentId", "Path" },
                values: new object[,]
                {
                    { new Guid("1d9ded52-dc8b-4276-ad0e-4a5bccae93e0"), "Client", (short)0, null, "/projects" },
                    { new Guid("0192ec60-ea5c-4ea3-8ab9-8e6c0be3df20"), "eSecurity", (short)0, null, "/projects" },
                    { new Guid("3f836f67-da2a-4a4a-a557-b0e8aacb59da"), "eSecurity", (short)1, null, "/vulnerabilities" }
                });

            migrationBuilder.InsertData(
                table: "MethodProtocols",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "ProjectType" },
                values: new object[,]
                {
                    { new Guid("33c0801d-6189-4ee9-9211-008519a57914"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SIP", (short)1 },
                    { new Guid("8da0aaee-a4e0-440c-a3f6-e25579a15a92"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HTTPS", (short)1 },
                    { new Guid("c8e292b7-5959-412c-9e49-dc5dfad55588"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "HTTP", (short)1 },
                    { new Guid("88deec05-1e71-4bf8-abfa-630fd61d25af"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "IMAP4", (short)1 },
                    { new Guid("71695098-3236-43f8-a754-88daa7f0daea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "POP3", (short)1 },
                    { new Guid("a8b627b8-3f6c-4ea1-8987-8e08ee77f610"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SMTP", (short)1 },
                    { new Guid("d3b25a96-5731-46a3-b895-6e39d23c0564"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "TELNET", (short)1 },
                    { new Guid("4c8cf2f5-c68d-441c-9a9f-19dfadd08fd2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SSL", (short)1 },
                    { new Guid("7a38bff6-291d-468a-b44a-0710bb5077db"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "SSH", (short)1 },
                    { new Guid("0b45ff60-bc86-4317-a8e2-bd0c0718f04d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "FTP", (short)1 },
                    { new Guid("b63a4d3b-5152-4c4f-9614-77587c064a9b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "IP", (short)1 },
                    { new Guid("d1054e36-4d06-4e3a-b052-502a70f41a8c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "DELETE", (short)0 },
                    { new Guid("f09efb98-df65-430e-bcc7-a73f8403789b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "PUT", (short)0 },
                    { new Guid("09ef5a8a-6a55-4370-ad90-3f745a866d3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "POST", (short)0 },
                    { new Guid("b01f2cab-82aa-443a-ab48-61707438145e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GET", (short)0 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedDate", "EndDate", "InitialDate", "IsDeleted", "Name", "ProjectType", "Subsidiary" },
                values: new object[] { new Guid("f60da305-2561-47b9-aebf-aecb785b0bd3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 24, 22, 4, 10, 266, DateTimeKind.Utc).AddTicks(6187), new DateTime(2019, 11, 17, 22, 4, 10, 266, DateTimeKind.Utc).AddTicks(5792), false, "Projeto 01 [Web]: <br />Domínio: http://www.siteinseguro.com.br/", (short)0, "Subsidiária" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "Login", "MenuId", "Password", "ProjectId", "UserName", "UserType" },
                values: new object[] { new Guid("c410975d-9187-413f-9dd9-423531ea1968"), new DateTime(2019, 6, 20, 19, 4, 10, 262, DateTimeKind.Local).AddTicks(2025), "diego@cliente.com", "201909182204", null, "5ivWjl+ZjGohSxB1pb/U+w==", null, "Diego Sanches", (short)1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "Login", "MenuId", "Password", "ProjectId", "UserName", "UserType" },
                values: new object[] { new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3"), new DateTime(2019, 6, 20, 19, 4, 10, 263, DateTimeKind.Local).AddTicks(8695), "adm@esecurity.com", "201909182204", null, "5ivWjl+ZjGohSxB1pb/U+w==", null, "Administrador [eSecurity]", (short)0 });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "Order", "ParentId", "Path" },
                values: new object[,]
                {
                    { new Guid("b6a6c83a-0382-4ae2-99ed-37cc98baf2a4"), "Client", (short)0, new Guid("1d9ded52-dc8b-4276-ad0e-4a5bccae93e0"), "/projects/view-details" },
                    { new Guid("6035459a-38c2-47da-896e-c7444a129f1e"), "eSecurity", (short)0, new Guid("0192ec60-ea5c-4ea3-8ab9-8e6c0be3df20"), "/projects/create" },
                    { new Guid("c7f75934-40c3-4f2f-9005-67503ab34e3d"), "eSecurity", (short)1, new Guid("0192ec60-ea5c-4ea3-8ab9-8e6c0be3df20"), "/projects/join" },
                    { new Guid("1746e741-d9e3-4574-b1f5-4e5888057488"), "eSecurity", (short)0, new Guid("3f836f67-da2a-4a4a-a557-b0e8aacb59da"), "/vulnerabilities/create" }
                });

            migrationBuilder.InsertData(
                table: "UserMenuEntity",
                columns: new[] { "MenuId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1d9ded52-dc8b-4276-ad0e-4a5bccae93e0"), new Guid("c410975d-9187-413f-9dd9-423531ea1968") },
                    { new Guid("0192ec60-ea5c-4ea3-8ab9-8e6c0be3df20"), new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3") },
                    { new Guid("3f836f67-da2a-4a4a-a557-b0e8aacb59da"), new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3") }
                });

            migrationBuilder.InsertData(
                table: "Vulnerabilities",
                columns: new[] { "Id", "CVSS", "CreatedDate", "IsDeleted", "Name", "ProjectType", "References", "RiskFactor", "Tags", "UserId" },
                values: new object[,]
                {
                    { new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc"), "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H", new DateTime(2019, 9, 18, 19, 4, 10, 265, DateTimeKind.Local).AddTicks(6388), false, "Weak Password Policy", (short)0, "http://www.owasp.org/index.php/Testing_for_Weak_password_policy", (short)3, "password,weak-password,weak-password-policy,password-policy", new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3") },
                    { new Guid("74b45d1f-e6e2-474d-b7e3-5cecefe825e5"), "AV:N/AV:H/PR:N/UI:N/S:U/C:H/I:H/A:H", new DateTime(2019, 9, 18, 19, 4, 10, 265, DateTimeKind.Local).AddTicks(9878), false, "Buffer overflow", (short)0, "http://www.owasp.org/index.php/buffer_overflow_field", (short)3, "field-validation,field-weak", new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3") }
                });

            migrationBuilder.InsertData(
                table: "ProjectsVulnerability",
                columns: new[] { "Id", "CreatedDate", "Environment", "FiledOrCookieName", "IsDeleted", "MethodProtocolId", "Observation", "Port", "ProjectId", "Status", "UserId", "VulnerabilityId" },
                values: new object[,]
                {
                    { new Guid("4b61176e-1154-4cab-863c-efd0172f1afc"), new DateTime(2019, 11, 7, 22, 4, 10, 266, DateTimeKind.Utc).AddTicks(8907), "register.php", "password", false, new Guid("09ef5a8a-6a55-4370-ad90-3f745a866d3b"), null, "80", new Guid("f60da305-2561-47b9-aebf-aecb785b0bd3"), (short)1, new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3"), new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc") },
                    { new Guid("f37254b1-8969-461e-8983-dad6c6d13707"), new DateTime(2019, 11, 7, 22, 4, 10, 267, DateTimeKind.Utc).AddTicks(1871), "register.php", "retype-password", false, new Guid("09ef5a8a-6a55-4370-ad90-3f745a866d3b"), null, "80", new Guid("f60da305-2561-47b9-aebf-aecb785b0bd3"), (short)1, new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3"), new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc") },
                    { new Guid("95ffea42-01b2-4b44-a21b-753ce5af0213"), new DateTime(2019, 11, 7, 22, 4, 10, 267, DateTimeKind.Utc).AddTicks(1895), "contato.php", "name", false, new Guid("09ef5a8a-6a55-4370-ad90-3f745a866d3b"), null, "80", new Guid("f60da305-2561-47b9-aebf-aecb785b0bd3"), (short)1, new Guid("332e1405-b3f7-4c44-8fff-6fda45fa13c3"), new Guid("74b45d1f-e6e2-474d-b7e3-5cecefe825e5") }
                });

            migrationBuilder.InsertData(
                table: "VulnerabilityLangs",
                columns: new[] { "Id", "CreatedDate", "Description", "IsDeleted", "LanguageType", "Remediation", "VulnerabilityId" },
                values: new object[,]
                {
                    { new Guid("551588d2-dadf-498a-be10-2cebfa2c3efc"), new DateTime(2019, 9, 18, 19, 4, 10, 266, DateTimeKind.Local).AddTicks(881), @"The application does not require users to have strong passwords. The lack of password complexity significantly reduces search space by trying to guess user passwords, facilitating brute force attacks.
                                    Thus,
                                    it was possible to gain access to the system using a user account that has weak password and easy guessing. From the accessed account,
                                    A new account has been created.", false, (short)1, @"Enter a strong password policy (which ensures password length, complexity, reuse and aging) and / or
                                    additional authentication controls (double factor authentication).", new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc") },
                    { new Guid("f089facc-9c97-4835-afc7-bd36e48bfe3c"), new DateTime(2019, 9, 18, 19, 4, 10, 266, DateTimeKind.Local).AddTicks(2402), @"A aplicação não exige que os usuários tenham senhas fortes. A falta de complexidade de senha reduz significamente o espaço de busca ao tentar adivinhar as senhas dos usuários, facilitando ataques de força bruta.
                                    Dessa forma,
                                    foi possível obter acesso ao sistema utilizando uma conta de usuário que possui senha fraca e de fácil adivinhação.A partir da conta acessada,
                                    uma nova conta foi criada.", false, (short)0, @"Introduza uma política de senha forte (que garanta o tamanho, a complexidade, a reutilização e o envelhecimento da senha) e/ou
                                    controles de autenticação adicionais (duplo fator de autenticação).", new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc") },
                    { new Guid("e161d574-6ac8-47d2-9abf-3f72d46131b8"), new DateTime(2019, 9, 18, 19, 4, 10, 266, DateTimeKind.Local).AddTicks(2420), @"La aplicación no requiere que los usuarios tengan contraseñas seguras. La falta de complejidad de la contraseña reduce significativamente el espacio de búsqueda al tratar de adivinar las contraseñas de los usuarios, lo que facilita los ataques de fuerza bruta.
                                    De esa forma,
                                    fue posible obtener acceso al sistema utilizando una cuenta de usuario que tiene una contraseña débil y fácil de adivinar.
                                    Se ha creado una nueva cuenta.", false, (short)2, @"Ingrese una política de contraseña segura (que asegure la longitud, complejidad, reutilización y antigüedad de la contraseña) y / o
                                    controles de autenticación adicionales (autenticación de doble factor).", new Guid("21536a2a-6b90-4cf0-ae88-ab09da264cfc") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaProjectVultis_ProjectVulnerabilityId",
                table: "MediaProjectVultis",
                column: "ProjectVulnerabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_MethodProtocolId",
                table: "ProjectsVulnerability",
                column: "MethodProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_ProjectId",
                table: "ProjectsVulnerability",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_UserId",
                table: "ProjectsVulnerability",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsVulnerability_VulnerabilityId",
                table: "ProjectsVulnerability",
                column: "VulnerabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuEntity_UserId",
                table: "UserMenuEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MenuId",
                table: "Users",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");

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
                name: "RefreshTokenEntity");

            migrationBuilder.DropTable(
                name: "UserMenuEntity");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "VulnerabilityLangs");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "ProjectsVulnerability");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "MethodProtocols");

            migrationBuilder.DropTable(
                name: "Vulnerabilities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
