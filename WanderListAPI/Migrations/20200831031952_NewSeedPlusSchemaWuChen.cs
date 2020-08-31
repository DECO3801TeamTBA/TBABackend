using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderListAPI.Migrations
{
    public partial class NewSeedPlusSchemaWuChen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Lattitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.ContentId);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.RewardId);
                });

            migrationBuilder.CreateTable(
                name: "Shortlist",
                columns: table => new
                {
                    ShortListId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ListName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shortlist", x => x.ShortListId);
                });

            migrationBuilder.CreateTable(
                name: "ShortlistContent",
                columns: table => new
                {
                    ListId = table.Column<Guid>(nullable: false),
                    ContentId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortlistContent", x => new { x.ContentId, x.ListId });
                });

            migrationBuilder.CreateTable(
                name: "UserReward",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RewardId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReward", x => new { x.UserId, x.RewardId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activity_Content_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                columns: table => new
                {
                    DestinationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.DestinationId);
                    table.ForeignKey(
                        name: "FK_Destination_Content_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => new { x.ContentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_History_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceMeta",
                columns: table => new
                {
                    ResourceMetaId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    MimeType = table.Column<string>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OnDisk = table.Column<bool>(nullable: false),
                    ContentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceMeta", x => x.ResourceMetaId);
                    table.ForeignKey(
                        name: "FK_ResourceMeta_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResourceMeta_Resource_ResourceMetaId",
                        column: x => x.ResourceMetaId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af369cc6-1ec4-4eed-94a9-54f8798ab05c", "60c32101-a22c-43b3-9999-4835a962d031", "Admin", "Admin" },
                    { "78966968-c6e9-46e8-a277-972d45c381f2", "2522ed2f-e023-4b20-a9b0-82c860eb019c", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "59768b0c-fc94-4645-a27a-e79b0b7ae84e", 0, "f0087192-d389-48dc-a037-c8f279ce9bfa", "fake@fake.com", false, "JoeyJojo", "Shabadoo", false, null, null, null, "AQAAAAEAACcQAAAAEAYjOpoEKhZX96VzAk7lc+fBrAPrZFWDUAyMDnWxjewD+WpQtbcYyxZbqw/Q3WF8xQ==", null, false, 0, "a165f7be-522d-42b3-a937-ce4faa32192b", false, "wanderuser" },
                    { "4e5d808d-87f8-45b5-aae3-f8bb03d9a6b2", 0, "afe61de1-0ff2-4a00-8346-a93a268bc7fd", "surfer69@scoobydoo.com", false, "Norville", "Rogers", false, null, null, null, "AQAAAAEAACcQAAAAEM1G44oo51+TC2poqw+lJkrxhFsMleUOo6xjjt3YakrOfAG38nRurw+wMZRSofYBEg==", null, false, 0, "e21d68e2-1e8b-4903-a76d-dcbc10fe006e", false, "Shaggy" }
                });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "ContentId", "Address", "Capacity", "Description", "Lattitude", "Longitude", "Name", "Website" },
                values: new object[,]
                {
                    { new Guid("c7c89c9c-e5f6-419a-b569-a170b1334c2a"), "fake", 200, "fake", 15.51m, 45.15m, "Fakorama", "www.fake.com" },
                    { new Guid("32881ddb-21e1-4935-a06d-0c91e6014d28"), null, 0, null, 0m, 0m, null, null },
                    { new Guid("2dbbea42-abab-40f0-9eba-9236b740c724"), null, 0, null, 0m, 0m, null, null },
                    { new Guid("7b80434e-34f2-4227-afdf-1f8044f7043a"), null, 0, null, 0m, 0m, null, null }
                });

            migrationBuilder.InsertData(
                table: "Reward",
                columns: new[] { "RewardId", "Description", "ExpiryDate", "Name", "Value" },
                values: new object[] { new Guid("bcac4e97-646f-494d-a15e-66952fc53fe7"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Burger King Coupon", "15% OFF" });

            migrationBuilder.InsertData(
                table: "Shortlist",
                columns: new[] { "ShortListId", "ListName", "UserId" },
                values: new object[] { new Guid("751a3cb0-7c1c-4391-ad08-3db7e69c1ab0"), "Scooby Doo Vacation", new Guid("59768b0c-fc94-4645-a27a-e79b0b7ae84e") });

            migrationBuilder.InsertData(
                table: "ShortlistContent",
                columns: new[] { "ContentId", "ListId", "Number" },
                values: new object[,]
                {
                    { new Guid("32881ddb-21e1-4935-a06d-0c91e6014d28"), new Guid("751a3cb0-7c1c-4391-ad08-3db7e69c1ab0"), 1 },
                    { new Guid("2dbbea42-abab-40f0-9eba-9236b740c724"), new Guid("751a3cb0-7c1c-4391-ad08-3db7e69c1ab0"), 1 },
                    { new Guid("7b80434e-34f2-4227-afdf-1f8044f7043a"), new Guid("751a3cb0-7c1c-4391-ad08-3db7e69c1ab0"), 1 }
                });

            migrationBuilder.InsertData(
                table: "UserReward",
                columns: new[] { "UserId", "RewardId" },
                values: new object[] { new Guid("bcac4e97-646f-494d-a15e-66952fc53fe7"), new Guid("59768b0c-fc94-4645-a27a-e79b0b7ae84e") });

            migrationBuilder.InsertData(
                table: "Activity",
                column: "ActivityId",
                value: new Guid("32881ddb-21e1-4935-a06d-0c91e6014d28"));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "59768b0c-fc94-4645-a27a-e79b0b7ae84e", "af369cc6-1ec4-4eed-94a9-54f8798ab05c" },
                    { "4e5d808d-87f8-45b5-aae3-f8bb03d9a6b2", "78966968-c6e9-46e8-a277-972d45c381f2" }
                });

            migrationBuilder.InsertData(
                table: "Destination",
                column: "DestinationId",
                value: new Guid("2dbbea42-abab-40f0-9eba-9236b740c724"));

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "ContentId", "UserId", "Date" },
                values: new object[,]
                {
                    { new Guid("c7c89c9c-e5f6-419a-b569-a170b1334c2a"), "59768b0c-fc94-4645-a27a-e79b0b7ae84e", new DateTime(2020, 8, 31, 13, 19, 52, 159, DateTimeKind.Local).AddTicks(8205) },
                    { new Guid("32881ddb-21e1-4935-a06d-0c91e6014d28"), "59768b0c-fc94-4645-a27a-e79b0b7ae84e", new DateTime(2020, 8, 31, 13, 19, 52, 169, DateTimeKind.Local).AddTicks(5989) },
                    { new Guid("2dbbea42-abab-40f0-9eba-9236b740c724"), "59768b0c-fc94-4645-a27a-e79b0b7ae84e", new DateTime(2020, 8, 31, 13, 19, 52, 169, DateTimeKind.Local).AddTicks(6102) },
                    { new Guid("7b80434e-34f2-4227-afdf-1f8044f7043a"), "59768b0c-fc94-4645-a27a-e79b0b7ae84e", new DateTime(2020, 8, 31, 13, 19, 52, 169, DateTimeKind.Local).AddTicks(6130) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_UserId",
                table: "History",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceMeta_ContentId",
                table: "ResourceMeta",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "ResourceMeta");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "Shortlist");

            migrationBuilder.DropTable(
                name: "ShortlistContent");

            migrationBuilder.DropTable(
                name: "UserReward");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
