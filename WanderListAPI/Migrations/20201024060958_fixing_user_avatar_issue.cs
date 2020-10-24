using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderListAPI.Migrations
{
    public partial class fixing_user_avatar_issue : Migration
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
                    OnDisk = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceMeta", x => x.ResourceMetaId);
                    table.ForeignKey(
                        name: "FK_ResourceMeta_Resource_ResourceMetaId",
                        column: x => x.ResourceMetaId,
                        principalTable: "Resource",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
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
                    Points = table.Column<int>(nullable: false),
                    ProfilePicResourceMetaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ResourceMeta_ProfilePicResourceMetaId",
                        column: x => x.ProfilePicResourceMetaId,
                        principalTable: "ResourceMeta",
                        principalColumn: "ResourceMetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Lattitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    CoverImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_ResourceMeta_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "ResourceMeta",
                        principalColumn: "ResourceMetaId",
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
                name: "Shortlist",
                columns: table => new
                {
                    ShortlistId = table.Column<Guid>(nullable: false),
                    ListName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shortlist", x => x.ShortlistId);
                    table.ForeignKey(
                        name: "FK_Shortlist_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<Guid>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Video = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_Item_CityId",
                        column: x => x.CityId,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityUser",
                columns: table => new
                {
                    CityId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityUser", x => new { x.CityId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CityUser_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Featured = table.Column<bool>(nullable: false),
                    EnvironmentalRating = table.Column<int>(nullable: false),
                    SocialRating = table.Column<int>(nullable: false),
                    EconomicRating = table.Column<int>(nullable: false),
                    CityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Content_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Content_Item_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    CountThreshold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.RewardId);
                    table.ForeignKey(
                        name: "FK_Reward_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
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
                name: "ContentResourceMeta",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    ResourceMetaId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentResourceMeta", x => new { x.ContentId, x.ResourceMetaId });
                    table.ForeignKey(
                        name: "FK_ContentResourceMeta_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentResourceMeta_ResourceMeta_ResourceMetaId",
                        column: x => x.ResourceMetaId,
                        principalTable: "ResourceMeta",
                        principalColumn: "ResourceMetaId",
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
                name: "QR",
                columns: table => new
                {
                    QRId = table.Column<Guid>(nullable: false),
                    Expiry = table.Column<DateTime>(nullable: false),
                    ContentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QR", x => x.QRId);
                    table.ForeignKey(
                        name: "FK_QR_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortlistContent",
                columns: table => new
                {
                    ShortlistId = table.Column<Guid>(nullable: false),
                    ContentId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortlistContent", x => new { x.ContentId, x.ShortlistId });
                    table.ForeignKey(
                        name: "FK_ShortlistContent_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShortlistContent_Shortlist_ShortlistId",
                        column: x => x.ShortlistId,
                        principalTable: "Shortlist",
                        principalColumn: "ShortlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserReward",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RewardId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReward", x => new { x.UserId, x.RewardId });
                    table.ForeignKey(
                        name: "FK_UserReward_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserReward_Reward_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Reward",
                        principalColumn: "RewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53357811-244f-474c-9cdb-9e9ad1d3dc00", "9aa0a0c0-1b03-4bc4-a6e3-6e27df2899ae", "User", "USER" },
                    { "f04ae5a9-25d2-4fd6-bbc6-64ef93bb1585", "ed1bc63d-9838-4900-a4d5-abea58381cad", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ResourceId", "Data", "FilePath" },
                values: new object[,]
                {
                    { new Guid("57c84676-98cb-48f2-bef3-65e22e7139a4"), null, "./Resources/Images/SouthBrisbaneCemetery - 3.jpg" },
                    { new Guid("d5056f70-0b06-43b8-a531-7fb03125a02b"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("7385025a-3cad-4c06-a128-63faba227ade"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("48fa6cca-e61e-4ee7-ac08-9a950e98fb0b"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("eea05605-3272-402e-821a-227b657f38ee"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("256c7770-71bd-4261-9b26-b47d89d61b82"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("867540eb-577d-4cf3-bdef-14fdbf8ed2ea"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("c2df0405-fb07-495b-afed-c3ea9a94c948"), null, "./Resources/Images/Aquarium.jpg" },
                    { new Guid("2571b013-aa2a-455f-b759-e1dab4849895"), null, "./Resources/Images/Aquarium - 2.jpg" },
                    { new Guid("cd431ce8-2fd2-4a11-b20e-bb39160f7d13"), null, "./Resources/Images/Aquarium - 3.jpg" },
                    { new Guid("b984fc53-6449-4570-842b-ea4df77dcf15"), null, "./Resources/Images/Chinatown.jpg" },
                    { new Guid("4312f7ed-3e9d-4c61-94a0-cbbb82672ced"), null, "./Resources/Images/Chinatown - 2.jpg" },
                    { new Guid("a303d0eb-a9eb-4634-a69c-106edcb9f4e6"), null, "./Resources/Images/Chinatown - 3.jpg" },
                    { new Guid("74b38bc8-db1e-48a0-8ac2-dfbb0ca23d9c"), null, "./Resources/Images/Harold.jfif" },
                    { new Guid("a552b281-498d-4577-b102-83df5b6c8e0d"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("a3865d3e-d89f-499e-89d9-342937feccb4"), null, "./Resources/Images/Velma.jfif" },
                    { new Guid("83c45b65-bed3-4968-9452-b164c52346d3"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("2517a391-99c3-478a-8c13-aa8dfec383ba"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("6b614456-11db-4f1a-812f-3f3154c8b5e8"), null, "./Resources/Images/Brisbane.jfif" },
                    { new Guid("3830a757-d155-4163-8f83-b90efe45c53c"), null, "./Resources/Images/SouthBrisbaneCemetery - 2.jpg" },
                    { new Guid("4dc3cebb-e223-4aaf-badb-8811b1f0bc31"), null, "./Resources/Images/SouthBrisbaneCemetery.jpg" },
                    { new Guid("8a49a0a6-983a-4cb8-9829-3665182a6cbe"), null, "./Resources/Images/UQ - 3.jpg" },
                    { new Guid("6d5eea37-8e93-43ba-b82f-3b6b9e38076c"), null, "./Resources/Images/UQ - 2.jpg" },
                    { new Guid("f2924ae3-f6f1-4251-b4e3-9acaf9bafd8b"), null, "./Resources/Images/PubCrawl.jpg" },
                    { new Guid("39d1c250-bebe-4192-af7a-05187d2784f5"), null, "./Resources/Images/PubCrawl - 2.jpg" },
                    { new Guid("883efb56-479a-47a9-8e2c-625553187ca7"), null, "./Resources/Images/PubCrawl - 3.jpg" },
                    { new Guid("599ff097-5da6-432a-a649-11c070126c25"), null, "./Resources/Images/UniTour.jpg" },
                    { new Guid("b80708d5-376d-4368-b7e1-5f71d5d36d3f"), null, "./Resources/Images/UniTour - 2.jpg" },
                    { new Guid("92ea3611-cc81-475e-9fd3-3ccbbe8c7b2a"), null, "./Resources/Images/UniTour - 3.jpg" },
                    { new Guid("06b1b197-5873-45dd-ac38-e346b67c4dc5"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("1bdfd98d-f1f4-4647-8de7-774a9750e016"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("d8409179-d5e0-46dc-80a3-0d17f2e098f2"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("8bccccc6-c9cf-4b1c-ad1c-c0c854afb5af"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("80125e79-bd6a-4ca6-9ee2-a22edac2d6c7"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("6123d448-4955-4308-af5d-cd35cd95b7f7"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("9aaeb44e-f597-448c-8c04-fff025028863"), null, "./Resources/Images/Art Gallery.jpg" },
                    { new Guid("6c41e2a0-1fd2-4d9f-94c4-4610b0f352ab"), null, "./Resources/Images/Art Gallery - 2.jpg" },
                    { new Guid("43bbf6d7-db73-418a-8953-947394985bf4"), null, "./Resources/Images/Art Gallery - 3.jpg" },
                    { new Guid("c0efe95f-3f46-4370-92e7-30f2d95e6e2e"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("49f9e196-8f6f-4cc0-a7a4-2b7a5da73cee"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("e806ce2a-9fe3-4e18-b5fd-79dda216ffd1"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("f4c432cd-ac81-47b5-8210-ce661e06e815"), null, "./Resources/Images/UQ.jpg" },
                    { new Guid("b266e840-3f79-4f8a-aceb-0585af953cdc"), null, "./Resources/Images/Sydney.jfif" },
                    { new Guid("96e8fe2c-6a5d-408e-833c-49a5187cbd93"), null, "./Resources/Images/Melbourne.jfif" }
                });

            migrationBuilder.InsertData(
                table: "ResourceMeta",
                columns: new[] { "ResourceMetaId", "AddedOn", "Description", "Extension", "FileName", "Length", "MimeType", "OnDisk" },
                values: new object[,]
                {
                    { new Guid("f2924ae3-f6f1-4251-b4e3-9acaf9bafd8b"), new DateTime(2020, 10, 24, 16, 9, 52, 147, DateTimeKind.Local).AddTicks(2671), "PubCrawl", ".jpg", "PubCrawl.jpg", 0L, "image/jpeg", true },
                    { new Guid("d5056f70-0b06-43b8-a531-7fb03125a02b"), new DateTime(2020, 10, 24, 16, 9, 52, 155, DateTimeKind.Local).AddTicks(8648), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("7385025a-3cad-4c06-a128-63faba227ade"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(2886), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("48fa6cca-e61e-4ee7-ac08-9a950e98fb0b"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(4453), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("eea05605-3272-402e-821a-227b657f38ee"), new DateTime(2020, 10, 24, 16, 9, 52, 156, DateTimeKind.Local).AddTicks(381), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("256c7770-71bd-4261-9b26-b47d89d61b82"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(6816), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("867540eb-577d-4cf3-bdef-14fdbf8ed2ea"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(8082), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("c2df0405-fb07-495b-afed-c3ea9a94c948"), new DateTime(2020, 10, 24, 16, 9, 52, 156, DateTimeKind.Local).AddTicks(2209), "Aquarium", ".jpg", "Aquarium.jpg", 0L, "image/jpeg", true },
                    { new Guid("2571b013-aa2a-455f-b759-e1dab4849895"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(9600), "Aquarium - 2", ".jpg", "Aquarium - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("57c84676-98cb-48f2-bef3-65e22e7139a4"), new DateTime(2020, 10, 24, 16, 9, 52, 159, DateTimeKind.Local).AddTicks(187), "SouthBrisbaneCemetery - 3", ".jpg", "SouthBrisbaneCemetery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("cd431ce8-2fd2-4a11-b20e-bb39160f7d13"), new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(1348), "Aquarium - 3", ".jpg", "Aquarium - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("4312f7ed-3e9d-4c61-94a0-cbbb82672ced"), new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(3090), "Chinatown - 2", ".jpg", "Chinatown - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("a303d0eb-a9eb-4634-a69c-106edcb9f4e6"), new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(6645), "Chinatown - 3", ".jpg", "Chinatown - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("74b38bc8-db1e-48a0-8ac2-dfbb0ca23d9c"), new DateTime(2020, 10, 24, 16, 9, 52, 144, DateTimeKind.Local).AddTicks(6846), "Harold", ".jfif", "Harold.jfif", 0L, "image/jpeg", true },
                    { new Guid("a552b281-498d-4577-b102-83df5b6c8e0d"), new DateTime(2020, 10, 24, 16, 9, 52, 138, DateTimeKind.Local).AddTicks(5487), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("a3865d3e-d89f-499e-89d9-342937feccb4"), new DateTime(2020, 10, 24, 16, 9, 52, 144, DateTimeKind.Local).AddTicks(9977), "Velma", ".jfif", "Velma.jfif", 0L, "image/jpeg", true },
                    { new Guid("83c45b65-bed3-4968-9452-b164c52346d3"), new DateTime(2020, 10, 24, 16, 9, 52, 141, DateTimeKind.Local).AddTicks(4160), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("2517a391-99c3-478a-8c13-aa8dfec383ba"), new DateTime(2020, 10, 24, 16, 9, 52, 142, DateTimeKind.Local).AddTicks(8705), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("6b614456-11db-4f1a-812f-3f3154c8b5e8"), new DateTime(2020, 10, 24, 16, 9, 52, 120, DateTimeKind.Local).AddTicks(4042), "Brisbane", ".jfif", "Brisbane.jfif", 0L, "image/jpeg", true },
                    { new Guid("b984fc53-6449-4570-842b-ea4df77dcf15"), new DateTime(2020, 10, 24, 16, 9, 52, 156, DateTimeKind.Local).AddTicks(5722), "Chinatown", ".jpg", "Chinatown.jpg", 0L, "image/jpeg", true },
                    { new Guid("3830a757-d155-4163-8f83-b90efe45c53c"), new DateTime(2020, 10, 24, 16, 9, 52, 158, DateTimeKind.Local).AddTicks(8599), "SouthBrisbaneCemetery - 2", ".jpg", "SouthBrisbaneCemetery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("4dc3cebb-e223-4aaf-badb-8811b1f0bc31"), new DateTime(2020, 10, 24, 16, 9, 52, 155, DateTimeKind.Local).AddTicks(6312), "SouthBrisbaneCemetery", ".jpg", "SouthBrisbaneCemetery.jpg", 0L, "image/jpeg", true },
                    { new Guid("8a49a0a6-983a-4cb8-9829-3665182a6cbe"), new DateTime(2020, 10, 24, 16, 9, 52, 158, DateTimeKind.Local).AddTicks(170), "UQ - 3", ".jpg", "UQ - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("39d1c250-bebe-4192-af7a-05187d2784f5"), new DateTime(2020, 10, 24, 16, 9, 52, 149, DateTimeKind.Local).AddTicks(3646), "PubCrawl - 2", ".jpg", "PubCrawl - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("883efb56-479a-47a9-8e2c-625553187ca7"), new DateTime(2020, 10, 24, 16, 9, 52, 149, DateTimeKind.Local).AddTicks(6592), "PubCrawl - 3", ".jpg", "PubCrawl - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("599ff097-5da6-432a-a649-11c070126c25"), new DateTime(2020, 10, 24, 16, 9, 52, 148, DateTimeKind.Local).AddTicks(2948), "UniTour", ".jpg", "UniTour.jpg", 0L, "image/jpeg", true },
                    { new Guid("b80708d5-376d-4368-b7e1-5f71d5d36d3f"), new DateTime(2020, 10, 24, 16, 9, 52, 150, DateTimeKind.Local).AddTicks(2245), "UniTour - 2", ".jpg", "UniTour - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("92ea3611-cc81-475e-9fd3-3ccbbe8c7b2a"), new DateTime(2020, 10, 24, 16, 9, 52, 150, DateTimeKind.Local).AddTicks(4098), "UniTour - 3", ".jpg", "UniTour - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("06b1b197-5873-45dd-ac38-e346b67c4dc5"), new DateTime(2020, 10, 24, 16, 9, 52, 148, DateTimeKind.Local).AddTicks(4513), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("1bdfd98d-f1f4-4647-8de7-774a9750e016"), new DateTime(2020, 10, 24, 16, 9, 52, 150, DateTimeKind.Local).AddTicks(6018), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("d8409179-d5e0-46dc-80a3-0d17f2e098f2"), new DateTime(2020, 10, 24, 16, 9, 52, 150, DateTimeKind.Local).AddTicks(7836), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("8bccccc6-c9cf-4b1c-ad1c-c0c854afb5af"), new DateTime(2020, 10, 24, 16, 9, 52, 148, DateTimeKind.Local).AddTicks(5632), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("80125e79-bd6a-4ca6-9ee2-a22edac2d6c7"), new DateTime(2020, 10, 24, 16, 9, 52, 150, DateTimeKind.Local).AddTicks(9181), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("6123d448-4955-4308-af5d-cd35cd95b7f7"), new DateTime(2020, 10, 24, 16, 9, 52, 151, DateTimeKind.Local).AddTicks(635), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("9aaeb44e-f597-448c-8c04-fff025028863"), new DateTime(2020, 10, 24, 16, 9, 52, 148, DateTimeKind.Local).AddTicks(7406), "Art Gallery", ".jpg", "Art Gallery.jpg", 0L, "image/jpeg", true },
                    { new Guid("6c41e2a0-1fd2-4d9f-94c4-4610b0f352ab"), new DateTime(2020, 10, 24, 16, 9, 52, 151, DateTimeKind.Local).AddTicks(2193), "Art Gallery - 2", ".jpg", "Art Gallery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("43bbf6d7-db73-418a-8953-947394985bf4"), new DateTime(2020, 10, 24, 16, 9, 52, 151, DateTimeKind.Local).AddTicks(4403), "Art Gallery - 3", ".jpg", "Art Gallery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("c0efe95f-3f46-4370-92e7-30f2d95e6e2e"), new DateTime(2020, 10, 24, 16, 9, 52, 148, DateTimeKind.Local).AddTicks(8484), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("49f9e196-8f6f-4cc0-a7a4-2b7a5da73cee"), new DateTime(2020, 10, 24, 16, 9, 52, 151, DateTimeKind.Local).AddTicks(5984), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("e806ce2a-9fe3-4e18-b5fd-79dda216ffd1"), new DateTime(2020, 10, 24, 16, 9, 52, 151, DateTimeKind.Local).AddTicks(7695), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("f4c432cd-ac81-47b5-8210-ce661e06e815"), new DateTime(2020, 10, 24, 16, 9, 52, 155, DateTimeKind.Local).AddTicks(669), "UQ", ".jpg", "UQ.jpg", 0L, "image/jpeg", true },
                    { new Guid("6d5eea37-8e93-43ba-b82f-3b6b9e38076c"), new DateTime(2020, 10, 24, 16, 9, 52, 157, DateTimeKind.Local).AddTicks(6339), "UQ - 2", ".jpg", "UQ - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("b266e840-3f79-4f8a-aceb-0585af953cdc"), new DateTime(2020, 10, 24, 16, 9, 52, 131, DateTimeKind.Local).AddTicks(3269), "Sydney", ".jfif", "Sydney.jfif", 0L, "image/jpeg", true },
                    { new Guid("96e8fe2c-6a5d-408e-833c-49a5187cbd93"), new DateTime(2020, 10, 24, 16, 9, 52, 131, DateTimeKind.Local).AddTicks(5588), "Melbourne", ".jfif", "Melbourne.jfif", 0L, "image/jpeg", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "ProfilePicResourceMetaId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "730f6a71-49c9-445d-8e71-868f497ffffa", 0, "4b351e7a-bc02-4ac4-97a3-b1a52d3fe5c7", "Daphne.Blakeo@pretend.com", false, "Daphne", "Blakeo", false, null, "DAPHNE.BLAKEO@PRETEND.COM", "DAPHNE", "AQAAAAEAACcQAAAAEFyceip7yN0yTgcUUgMijIkwayayj3BMiusf6Ep33U1vN5QF54x8hMU0mOJ98CZbfQ==", null, false, 400, new Guid("2517a391-99c3-478a-8c13-aa8dfec383ba"), "31e03685-0e2a-406d-8b6a-09bfa117ae27", false, "Daphne" },
                    { "7b26062c-2243-474e-9254-a158f8491b1f", 0, "10e69ce4-1270-4dcb-b912-f6515df110f8", "Fred.Jones@pretend.com", false, "Fred", "Jones", false, null, "FRED.JONES@PRETEND.COM", "FRED", "AQAAAAEAACcQAAAAEHb7TslhAN/kEUP6tga4enSYjMaM5tQsFtKN7JzjC3oxwFRQIR/jpxPq+HZhFRGYrA==", null, false, 375, new Guid("83c45b65-bed3-4968-9452-b164c52346d3"), "4ec28e7d-006c-44cc-a140-f62a7c0e7193", false, "Fred" },
                    { "14dae795-570a-4ddd-95df-603b19b16e53", 0, "7d122b16-c972-44b4-a999-a6b4a5017e39", "Velma.Dinkley@pretend.com", false, "Velma", "Dinkley", false, null, "VELMA.DINKLEY@PRETEND.COM", "VELMA", "AQAAAAEAACcQAAAAEOYbK40kNP9RkpTkQNrnfl403Fi8pNs+ebPNCd6WbBsUmkF9/twiAQLWly2mMNHiiw==", null, false, 400, new Guid("a3865d3e-d89f-499e-89d9-342937feccb4"), "f4cc5202-aafc-43f6-8475-9d3bc23622d6", false, "Velma" },
                    { "e865cca8-85ca-4f56-9e77-8c0394e48207", 0, "7312100b-bf1c-4812-994b-3d84d386499d", "Scoobert.Doo@pretend.com", false, "Scoobert", "Doo", false, null, "SCOOBERT.DOO@PRETEND.COM", "SCOOBY", "AQAAAAEAACcQAAAAELTUBkMfl5uReSMiSPZXtRs9E9xcAJw/JfZV/ClU4oMtyKYQnGfIT4Ad/REsJicOWw==", null, false, 500, new Guid("a552b281-498d-4577-b102-83df5b6c8e0d"), "bd3b3fb6-2f3c-45b9-8db9-8d19f3bde0af", false, "Scooby" },
                    { "9eeac19b-9df1-4bab-9e70-38bc4af917bd", 0, "93127eca-4252-40c1-b93f-7f99785c8aa5", "Norville.Rogers@pretend.com", false, "Norville", "Rogers", false, null, "NORVILLE.ROGERS@PRETEND.COM", "SHAGGY", "AQAAAAEAACcQAAAAEBq4m/N4x/iWsDqeGYliVgEXWhwhBwaxGnhbI6flRpBcesKLSqUD0GeayUPI/9a2ng==", null, false, 100, new Guid("74b38bc8-db1e-48a0-8ac2-dfbb0ca23d9c"), "844ece07-8b4a-4fad-80e3-3543fe23d3d4", false, "Shaggy" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "CoverImageId", "Description", "Lattitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new Guid("f2924ae3-f6f1-4251-b4e3-9acaf9bafd8b"), "Tour Brisbanes best bars and clubs in a night of fun", -27.470568, 153.024866, "Pub Crawl" },
                    { new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), new Guid("6b614456-11db-4f1a-812f-3f3154c8b5e8"), "Brisbane is the capital of Queensland, and the third most populous city in Australia.", -27.467939999999999, 153.02808999999999, "Brisbane" },
                    { new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), new Guid("b984fc53-6449-4570-842b-ea4df77dcf15"), "China Town’s great for Yum Cha, Chinese Food and a visit to Dessert Story, they have the best Taiwanese desserts!", -37.811279999999996, 144.96880899999999, "Melbourne Chinatown" },
                    { new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), new Guid("c2df0405-fb07-495b-afed-c3ea9a94c948"), "See the more than 13,000 aquatic life forms in the 14 themed areas.", -33.869349999999997, 151.202192, "SEA LIFE Sydney Aquarium" },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new Guid("eea05605-3272-402e-821a-227b657f38ee"), "Explore the lush plant life, hidden pagodas, and colorful statues at your own speed, or join one of three informative tours that run during the day at no extra cost.", -33.876274000000002, 151.20280199999999, "Chinese Garden of Friendship" },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new Guid("d5056f70-0b06-43b8-a531-7fb03125a02b"), "Australia's most famouse landmark", -33.856650999999999, 151.21527599999999, "Sydney Opera House" },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("4dc3cebb-e223-4aaf-badb-8811b1f0bc31"), "Super spooooky at night", -27.498972999999999, 153.02712, "South Brisbane Cemetery" },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new Guid("f4c432cd-ac81-47b5-8210-ce661e06e815"), "The best uni in brisbane", -27.497408, 153.01367999999999, "UQ" },
                    { new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), new Guid("c0efe95f-3f46-4370-92e7-30f2d95e6e2e"), "Explore Melbourne from the river at your own pace and without anyone getting in the way on this afternoon kayaking tour.", -37.820380999999998, 144.95828700000001, "Melbourne City Afternoon Kayak Tour" },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), new Guid("9aaeb44e-f597-448c-8c04-fff025028863"), "ArtVo is an art gallery with a difference—this immersive art space encourages people to touch, play, and interact with the art, and there are 11 themed zones to explore.", -37.812648000000003, 144.93767099999999, "ArtVo Immersive Gallery Experience" },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new Guid("8bccccc6-c9cf-4b1c-ad1c-c0c854afb5af"), "In an intimate group limited to 16 people, float over Yarra Valley vineyards at sunrise, when the landscapes look most magical.", -37.631934999999999, 145.400453, "Yarra Valley Balloon Flight at Sunrise" },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new Guid("06b1b197-5873-45dd-ac38-e346b67c4dc5"), "Get the chance to spot humpback whales right outside of Sydney on this speed boat tour from Circular Quay or Manly Wharf.", -33.856788999999999, 151.20925199999999, "Sydney Whale-Watching by Speed Boat" },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new Guid("599ff097-5da6-432a-a649-11c070126c25"), "Visit Brisbanes best universities", -27.477118999999998, 153.02837199999999, "Uni tour" },
                    { new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), new Guid("b266e840-3f79-4f8a-aceb-0585af953cdc"), "Sydney is the state capital of New South Wales and the most populous city in Australia and Oceania.", -33.867849999999997, 151.20732000000001, "Sydney" },
                    { new Guid("5a136b8f-9e27-4621-abef-504456d347de"), new Guid("96e8fe2c-6a5d-408e-833c-49a5187cbd93"), "Melbourne is the capital of Victoria, and the second-most populous city in Australia and Oceania.", -37.814, 144.96332000000001, "Melbourne" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "9eeac19b-9df1-4bab-9e70-38bc4af917bd", "53357811-244f-474c-9cdb-9e9ad1d3dc00" },
                    { "e865cca8-85ca-4f56-9e77-8c0394e48207", "53357811-244f-474c-9cdb-9e9ad1d3dc00" },
                    { "14dae795-570a-4ddd-95df-603b19b16e53", "53357811-244f-474c-9cdb-9e9ad1d3dc00" },
                    { "7b26062c-2243-474e-9254-a158f8491b1f", "53357811-244f-474c-9cdb-9e9ad1d3dc00" },
                    { "730f6a71-49c9-445d-8e71-868f497ffffa", "53357811-244f-474c-9cdb-9e9ad1d3dc00" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Country", "Video" },
                values: new object[,]
                {
                    { new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), "Australia", "nDHlEG48b-M" },
                    { new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), "Australia", "Yc7r_bbt00M" },
                    { new Guid("5a136b8f-9e27-4621-abef-504456d347de"), "Australia", "Rzn5WGnS350" }
                });

            migrationBuilder.InsertData(
                table: "Shortlist",
                columns: new[] { "ShortlistId", "ListName", "UserId" },
                values: new object[,]
                {
                    { new Guid("fef5d4b0-4607-4813-88d7-679b9666cd26"), "Shag Spots", "9eeac19b-9df1-4bab-9e70-38bc4af917bd" },
                    { new Guid("2ee3842d-9d38-47e5-81a6-0bd57df50e6b"), "Ghost Sightings", "9eeac19b-9df1-4bab-9e70-38bc4af917bd" },
                    { new Guid("8d590836-f6c0-400e-90bc-b1825699c0b7"), "Brisbane Holiday", "14dae795-570a-4ddd-95df-603b19b16e53" },
                    { new Guid("2c27bee8-7cfc-4ac3-b5ad-debe0f874a6f"), "Bucket List", "14dae795-570a-4ddd-95df-603b19b16e53" }
                });

            migrationBuilder.InsertData(
                table: "CityUser",
                columns: new[] { "CityId", "UserId", "Count" },
                values: new object[,]
                {
                    { new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), "9eeac19b-9df1-4bab-9e70-38bc4af917bd", 3 },
                    { new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), "e865cca8-85ca-4f56-9e77-8c0394e48207", 2 },
                    { new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), "14dae795-570a-4ddd-95df-603b19b16e53", 3 },
                    { new Guid("5a136b8f-9e27-4621-abef-504456d347de"), "14dae795-570a-4ddd-95df-603b19b16e53", 1 }
                });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "ContentId", "Address", "Capacity", "CityId", "EconomicRating", "EnvironmentalRating", "Featured", "SocialRating", "Website" },
                values: new object[,]
                {
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), null, 5, new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 5, 3, false, 5, null },
                    { new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), null, 5, new Guid("5a136b8f-9e27-4621-abef-504456d347de"), 5, 4, false, 5, null },
                    { new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), null, 5, new Guid("5a136b8f-9e27-4621-abef-504456d347de"), 5, 5, false, 4, null },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), null, 5, new Guid("5a136b8f-9e27-4621-abef-504456d347de"), 3, 5, false, 5, null },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), null, 5, new Guid("5a136b8f-9e27-4621-abef-504456d347de"), 5, 3, false, 5, null },
                    { new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), null, 5, new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 3, 2, false, 5, null },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), null, 5, new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 4, 5, false, 5, null },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), null, 5, new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 3, 3, false, 5, null },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), null, 5, new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 5, 4, false, 3, null },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), null, 5, new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 5, 5, false, 5, null },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), null, 5, new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 5, 3, false, 5, null },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), null, 5, new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 5, 5, false, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Reward",
                columns: new[] { "RewardId", "CityId", "CountThreshold", "Description", "ExpiryDate", "Name", "Value" },
                values: new object[,]
                {
                    { new Guid("73e161f2-5967-4a04-b81e-cc1ef7b18922"), new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drink Discount", "$5 OFF a jug of beer with any meal purchase" },
                    { new Guid("23fe1cc4-2db1-44f7-a8cf-20b8853d9bf9"), new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sydney Aquarium Voucher", "5% off your next ticket" },
                    { new Guid("9a374c7e-f91c-4fdf-9da0-c84a8127c62b"), new Guid("58dab70e-c8fd-49a9-a67a-3fd6dd0f4210"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free tour of Chinese Garden of Friendship", "Free tour with any ticket purchase" },
                    { new Guid("afbba952-bf50-499d-bda7-69370952c3c1"), new Guid("26e08eeb-c653-44a4-a6c0-43c91fe825b4"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uni Tour Discount", "15% Off your next tour" },
                    { new Guid("4ed94d72-dbea-4bdb-aa79-510308974ffb"), new Guid("5a136b8f-9e27-4621-abef-504456d347de"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Save when you bring a Friend", "1/2 price for the scond person for your Melbourne City Afternoon Kayak Tour" }
                });

            migrationBuilder.InsertData(
                table: "Activity",
                column: "ActivityId",
                values: new object[]
                {
                    new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"),
                    new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"),
                    new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"),
                    new Guid("2192d829-54d6-4290-b230-638304f60ae5"),
                    new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"),
                    new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc")
                });

            migrationBuilder.InsertData(
                table: "ContentResourceMeta",
                columns: new[] { "ContentId", "ResourceMetaId", "Number" },
                values: new object[,]
                {
                    { new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), new Guid("c0efe95f-3f46-4370-92e7-30f2d95e6e2e"), 0 },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new Guid("1bdfd98d-f1f4-4647-8de7-774a9750e016"), 1 },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new Guid("d5056f70-0b06-43b8-a531-7fb03125a02b"), 0 },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new Guid("7385025a-3cad-4c06-a128-63faba227ade"), 1 },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new Guid("48fa6cca-e61e-4ee7-ac08-9a950e98fb0b"), 2 },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new Guid("eea05605-3272-402e-821a-227b657f38ee"), 0 },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new Guid("256c7770-71bd-4261-9b26-b47d89d61b82"), 1 },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new Guid("867540eb-577d-4cf3-bdef-14fdbf8ed2ea"), 2 },
                    { new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), new Guid("c2df0405-fb07-495b-afed-c3ea9a94c948"), 0 },
                    { new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), new Guid("2571b013-aa2a-455f-b759-e1dab4849895"), 1 },
                    { new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), new Guid("49f9e196-8f6f-4cc0-a7a4-2b7a5da73cee"), 1 },
                    { new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), new Guid("cd431ce8-2fd2-4a11-b20e-bb39160f7d13"), 2 },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new Guid("8bccccc6-c9cf-4b1c-ad1c-c0c854afb5af"), 0 },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new Guid("06b1b197-5873-45dd-ac38-e346b67c4dc5"), 0 },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new Guid("6123d448-4955-4308-af5d-cd35cd95b7f7"), 2 },
                    { new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), new Guid("4312f7ed-3e9d-4c61-94a0-cbbb82672ced"), 1 },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), new Guid("9aaeb44e-f597-448c-8c04-fff025028863"), 0 },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), new Guid("6c41e2a0-1fd2-4d9f-94c4-4610b0f352ab"), 1 },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), new Guid("43bbf6d7-db73-418a-8953-947394985bf4"), 2 },
                    { new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), new Guid("b984fc53-6449-4570-842b-ea4df77dcf15"), 0 },
                    { new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), new Guid("e806ce2a-9fe3-4e18-b5fd-79dda216ffd1"), 2 },
                    { new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), new Guid("a303d0eb-a9eb-4634-a69c-106edcb9f4e6"), 2 },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new Guid("80125e79-bd6a-4ca6-9ee2-a22edac2d6c7"), 1 },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new Guid("d8409179-d5e0-46dc-80a3-0d17f2e098f2"), 2 },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new Guid("f4c432cd-ac81-47b5-8210-ce661e06e815"), 0 },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new Guid("6d5eea37-8e93-43ba-b82f-3b6b9e38076c"), 1 },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new Guid("f2924ae3-f6f1-4251-b4e3-9acaf9bafd8b"), 0 },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("57c84676-98cb-48f2-bef3-65e22e7139a4"), 2 },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new Guid("92ea3611-cc81-475e-9fd3-3ccbbe8c7b2a"), 2 },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new Guid("39d1c250-bebe-4192-af7a-05187d2784f5"), 1 },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new Guid("883efb56-479a-47a9-8e2c-625553187ca7"), 2 },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new Guid("8a49a0a6-983a-4cb8-9829-3665182a6cbe"), 2 },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("3830a757-d155-4163-8f83-b90efe45c53c"), 1 },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("4dc3cebb-e223-4aaf-badb-8811b1f0bc31"), 0 },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new Guid("b80708d5-376d-4368-b7e1-5f71d5d36d3f"), 1 },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new Guid("599ff097-5da6-432a-a649-11c070126c25"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Destination",
                column: "DestinationId",
                values: new object[]
                {
                    new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"),
                    new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"),
                    new Guid("06591e55-45a8-412c-9cab-105c9843a116"),
                    new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"),
                    new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"),
                    new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508")
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "ContentId", "UserId", "Date" },
                values: new object[,]
                {
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), "e865cca8-85ca-4f56-9e77-8c0394e48207", new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(7039) },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), "9eeac19b-9df1-4bab-9e70-38bc4af917bd", new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(6969) },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), "14dae795-570a-4ddd-95df-603b19b16e53", new DateTime(2020, 10, 24, 16, 9, 52, 153, DateTimeKind.Local).AddTicks(6508) },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), "9eeac19b-9df1-4bab-9e70-38bc4af917bd", new DateTime(2020, 10, 24, 16, 9, 52, 153, DateTimeKind.Local).AddTicks(3228) },
                    { new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), "14dae795-570a-4ddd-95df-603b19b16e53", new DateTime(2020, 10, 24, 16, 9, 52, 153, DateTimeKind.Local).AddTicks(6501) },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), "14dae795-570a-4ddd-95df-603b19b16e53", new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(7059) },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), "e865cca8-85ca-4f56-9e77-8c0394e48207", new DateTime(2020, 10, 24, 16, 9, 52, 153, DateTimeKind.Local).AddTicks(6428) },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), "9eeac19b-9df1-4bab-9e70-38bc4af917bd", new DateTime(2020, 10, 24, 16, 9, 52, 152, DateTimeKind.Local).AddTicks(9217) },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), "14dae795-570a-4ddd-95df-603b19b16e53", new DateTime(2020, 10, 24, 16, 9, 52, 160, DateTimeKind.Local).AddTicks(7052) }
                });

            migrationBuilder.InsertData(
                table: "QR",
                columns: new[] { "QRId", "ContentId", "Expiry" },
                values: new object[,]
                {
                    { new Guid("98e571f9-7a3b-4c0d-a8ae-32d4b06f737f"), new Guid("86eaa7cd-22c3-4b23-9d7a-a6a40a13df2b"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4793) },
                    { new Guid("729d6d26-1c96-4087-abad-6c56f7de4e89"), new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4788) },
                    { new Guid("e94fb825-69a5-4616-987e-5ba923553221"), new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(3575) },
                    { new Guid("9ca62dd6-3172-426e-87fd-900663197629"), new Guid("9a69e9d4-75e5-42db-92f7-c984aa8728ab"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4802) },
                    { new Guid("d1429357-fa0d-48c2-b0a5-d3b03741e9e9"), new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4747) },
                    { new Guid("6ed07c3e-12b5-48ac-a912-f0c3036482d7"), new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4852) },
                    { new Guid("a2546b7d-cf5e-4193-b82a-84a2ad024a57"), new Guid("9a29bc1d-8c33-4f00-b953-50f2fee9b657"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4872) },
                    { new Guid("28273ddb-7ab5-49d8-916e-f16fab359b3b"), new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4864) },
                    { new Guid("75425683-5be2-4a2f-9760-7598053cc712"), new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4846) },
                    { new Guid("49b44a90-b25b-4ef2-a0cb-bd3df96e158e"), new Guid("a4d180f9-d4a0-4962-a623-adcf5bea3faf"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4868) },
                    { new Guid("e00c546e-eef0-442e-b4b3-66324d2f696c"), new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4857) },
                    { new Guid("00e899a5-2c96-48c7-a3af-baa8c4c074cf"), new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new DateTime(2021, 1, 24, 16, 9, 52, 161, DateTimeKind.Local).AddTicks(4779) }
                });

            migrationBuilder.InsertData(
                table: "ShortlistContent",
                columns: new[] { "ContentId", "ShortlistId", "Number" },
                values: new object[,]
                {
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("2ee3842d-9d38-47e5-81a6-0bd57df50e6b"), 1 },
                    { new Guid("06591e55-45a8-412c-9cab-105c9843a116"), new Guid("fef5d4b0-4607-4813-88d7-679b9666cd26"), 3 },
                    { new Guid("bc0f26b5-3341-425f-8626-f79de9563bea"), new Guid("8d590836-f6c0-400e-90bc-b1825699c0b7"), 1 },
                    { new Guid("2192d829-54d6-4290-b230-638304f60ae5"), new Guid("8d590836-f6c0-400e-90bc-b1825699c0b7"), 2 },
                    { new Guid("93b4ccec-711e-4461-afc7-7f15c33d1017"), new Guid("2c27bee8-7cfc-4ac3-b5ad-debe0f874a6f"), 2 },
                    { new Guid("d3bcb61c-e5f5-4497-9bf8-3cf1ded1f508"), new Guid("8d590836-f6c0-400e-90bc-b1825699c0b7"), 3 },
                    { new Guid("72d6b13e-79af-4c56-b115-8fc616051ccd"), new Guid("fef5d4b0-4607-4813-88d7-679b9666cd26"), 2 },
                    { new Guid("fd39f7ef-5fe4-4670-ab7a-11e63b494aa4"), new Guid("fef5d4b0-4607-4813-88d7-679b9666cd26"), 2 },
                    { new Guid("17f3d026-e975-4a0b-ab5c-223a682400bc"), new Guid("2c27bee8-7cfc-4ac3-b5ad-debe0f874a6f"), 1 }
                });

            migrationBuilder.InsertData(
                table: "UserReward",
                columns: new[] { "UserId", "RewardId", "AppUserId" },
                values: new object[,]
                {
                    { "14dae795-570a-4ddd-95df-603b19b16e53", new Guid("73e161f2-5967-4a04-b81e-cc1ef7b18922"), null },
                    { "e865cca8-85ca-4f56-9e77-8c0394e48207", new Guid("73e161f2-5967-4a04-b81e-cc1ef7b18922"), null },
                    { "9eeac19b-9df1-4bab-9e70-38bc4af917bd", new Guid("73e161f2-5967-4a04-b81e-cc1ef7b18922"), null },
                    { "14dae795-570a-4ddd-95df-603b19b16e53", new Guid("afbba952-bf50-499d-bda7-69370952c3c1"), null },
                    { "e865cca8-85ca-4f56-9e77-8c0394e48207", new Guid("afbba952-bf50-499d-bda7-69370952c3c1"), null },
                    { "9eeac19b-9df1-4bab-9e70-38bc4af917bd", new Guid("afbba952-bf50-499d-bda7-69370952c3c1"), null },
                    { "14dae795-570a-4ddd-95df-603b19b16e53", new Guid("4ed94d72-dbea-4bdb-aa79-510308974ffb"), null }
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
                name: "IX_AspNetUsers_ProfilePicResourceMetaId",
                table: "AspNetUsers",
                column: "ProfilePicResourceMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_CityUser_UserId",
                table: "CityUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CityId",
                table: "Content",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentResourceMeta_ResourceMetaId",
                table: "ContentResourceMeta",
                column: "ResourceMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_History_UserId",
                table: "History",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CoverImageId",
                table: "Item",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_QR_ContentId",
                table: "QR",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reward_CityId",
                table: "Reward",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlist_UserId",
                table: "Shortlist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortlistContent_ShortlistId",
                table: "ShortlistContent",
                column: "ShortlistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReward_AppUserId",
                table: "UserReward",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReward_RewardId",
                table: "UserReward",
                column: "RewardId");
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
                name: "CityUser");

            migrationBuilder.DropTable(
                name: "ContentResourceMeta");

            migrationBuilder.DropTable(
                name: "Destination");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "QR");

            migrationBuilder.DropTable(
                name: "ShortlistContent");

            migrationBuilder.DropTable(
                name: "UserReward");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Shortlist");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ResourceMeta");

            migrationBuilder.DropTable(
                name: "Resource");
        }
    }
}
