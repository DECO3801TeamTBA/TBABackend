using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderListAPI.Migrations
{
    public partial class testing_static_qrs : Migration
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
                    CountThreshold = table.Column<int>(nullable: false),
                    CoverImageId = table.Column<Guid>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Reward_ResourceMeta_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "ResourceMeta",
                        principalColumn: "ResourceMetaId",
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
                    { "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c", "61991c17-03b2-425b-a3a8-8e5e5aa825cb", "User", "USER" },
                    { "931cecb3-5789-43be-9e1a-c4523c730b1f", "821f8353-37fa-4cb6-9c07-5081f699f06f", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ResourceId", "Data", "FilePath" },
                values: new object[,]
                {
                    { new Guid("1d0f2cf6-e3d2-4e15-8ec2-6881657f748a"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("f65e0db3-2d9c-41d2-8927-73a73b73265c"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("b709f89a-f5e0-4ec7-9c76-751f7a6d4945"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("b83c1b23-4398-4fb5-8d1c-3acd152295e2"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("883f88fc-b84e-47bb-b98d-6c56672195c8"), null, "./Resources/Images/Lone Pine - 3.jpg" },
                    { new Guid("8fdbfb57-5cbb-458d-a169-2bd168fc7b77"), null, "./Resources/Images/Lone Pine - 2.jpg" },
                    { new Guid("82128831-4ff9-422e-98bb-6b32a467cf1e"), null, "./Resources/Images/Lone Pine.jpg" },
                    { new Guid("0a69255d-ec07-4b33-ad36-8db326b4f0bc"), null, "./Resources/Images/Stradbroke - 3.jpg" },
                    { new Guid("1c1ff2a3-d840-4d33-850e-245894fc0840"), null, "./Resources/Images/Stradbroke - 2.jpg" },
                    { new Guid("6dfdc65e-d8e2-44ba-947b-70f2a06890ba"), null, "./Resources/Images/Stradbroke.jpg" },
                    { new Guid("3d6fb5da-35f4-45e8-913b-4f1031dfc3a8"), null, "./Resources/Images/Australia Zoo - 3.jpg" },
                    { new Guid("e6928025-5f3f-4373-82e4-7358e6fa981d"), null, "./Resources/Images/Australia Zoo - 2.jpg" },
                    { new Guid("a0ed18dc-2ea2-43e3-af53-26c35bd57c97"), null, "./Resources/Images/Australia Zoo.jpg" },
                    { new Guid("f24684a2-a0e3-4386-a51c-0faf060bb527"), null, "./Resources/Images/Rainforest - 3.jpg" },
                    { new Guid("f9d0493a-80e7-48cc-a4a4-5816240e42e1"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("44790094-a4f1-489d-8e52-2949ff4e8722"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("bbb053eb-e39c-45c3-906b-a4d5740a72ee"), null, "./Resources/Images/Aquarium.jpg" },
                    { new Guid("d1b9e5c0-5de8-4f85-bb9e-158646cea872"), null, "./Resources/Images/Aquarium - 2.jpg" },
                    { new Guid("546b2b8a-4049-41c5-91f4-a0b52723b7cd"), null, "./Resources/Images/Sydney Aquarium.jfif" },
                    { new Guid("8cb41680-4b0c-4123-b973-a2161ca40549"), null, "./Resources/Images/Beer.jfif" },
                    { new Guid("81a456da-326e-4fcd-a1aa-781771f6d471"), null, "./Resources/Images/Uni Tour.jfif" },
                    { new Guid("684f0bb7-60b0-42b2-bdf0-dd7bb7f93388"), null, "./Resources/Images/Melbourne.jfif" },
                    { new Guid("2a3b8bf8-3b89-472f-b14c-281638d33ede"), null, "./Resources/Images/Sydney.jfif" },
                    { new Guid("97c9293a-0782-4493-9f5f-3a522fedbbf8"), null, "./Resources/Images/Brisbane.jfif" },
                    { new Guid("96009bac-acf0-4075-8636-b3eb34e759c5"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("86dc2975-8a56-4bc5-83a1-0fe53e0b7271"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("5f5a5317-e775-48ae-8207-c3fdc46bf15b"), null, "./Resources/Images/Velma.jfif" },
                    { new Guid("e90aa38e-cec2-4426-a120-942531fddad6"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("77c5b1d5-aff1-4790-942a-adb25e144763"), null, "./Resources/Images/harold.jfif" },
                    { new Guid("e46d4c0c-c8c4-4ee2-a571-097e5818b1a3"), null, "./Resources/Images/Chinatown - 3.jpg" },
                    { new Guid("d3cba90e-e23e-4149-a2f9-1c64aff63c30"), null, "./Resources/Images/Chinatown - 2.jpg" },
                    { new Guid("05f7cd29-8d10-49f0-a5eb-e9c7f66cd3fc"), null, "./Resources/Images/Chinatown.jpg" },
                    { new Guid("594022e7-2551-477c-a06e-39cfcc25c59b"), null, "./Resources/Images/Aquarium - 3.jpg" },
                    { new Guid("8d633412-ec63-40b3-ae4b-b1dceee96cb3"), null, "./Resources/Images/Rainforest - 2.jpg" },
                    { new Guid("1784a2e6-4829-4d5b-b04c-37558c1e2bd6"), null, "./Resources/Images/Rainforest.jpg" },
                    { new Guid("10c6416d-fd9a-45be-aa78-4388c3e0cdea"), null, "./Resources/Images/SouthBrisbaneCemetery - 3.jpg" },
                    { new Guid("c3732c52-67e2-4581-8727-16f648ff7e05"), null, "./Resources/Images/SouthBrisbaneCemetery - 2.jpg" },
                    { new Guid("369ed4bd-0b46-44e3-a79d-b23575ec97ae"), null, "./Resources/Images/Rock Climbing - 3.jpg" },
                    { new Guid("6319e57f-e415-4ed4-8694-136d3b5b9bee"), null, "./Resources/Images/Rock Climbing - 2.jpg" },
                    { new Guid("7453da0d-3417-4bad-9ab7-65672fed228d"), null, "./Resources/Images/Rock Climbing.jpg" },
                    { new Guid("19155dc4-933a-42fd-8764-0b8c1253b21e"), null, "./Resources/Images/Scavenger - 3.jpg" },
                    { new Guid("d153eda8-4c32-4b31-b31a-3aa322b3d1c0"), null, "./Resources/Images/Scavenger - 2.jpg" },
                    { new Guid("541b83fb-0794-4f29-aeb0-8b43ada570c2"), null, "./Resources/Images/Scavenger.jpg" },
                    { new Guid("eb7d6e82-95e7-47d1-82b6-da7d0bfd87ac"), null, "./Resources/Images/Segway - 3.jpg" },
                    { new Guid("5dc5fd8e-ac89-421b-9b91-975b07ce719b"), null, "./Resources/Images/Segway - 2.jpg" },
                    { new Guid("a2b0e0a9-7e39-4593-9cbe-7faada91b3c1"), null, "./Resources/Images/Segway.jpg" },
                    { new Guid("a11bf867-424a-4a99-861f-dbec0d697686"), null, "./Resources/Images/UniTour - 3.jpg" },
                    { new Guid("8f994356-b6ff-4c50-9f84-a3d378ebbb14"), null, "./Resources/Images/UniTour - 2.jpg" },
                    { new Guid("74f3d463-d15b-4f9c-acbd-f2c967f7d1fb"), null, "./Resources/Images/UniTour.jpg" },
                    { new Guid("5c4e8732-4c22-4cae-a80b-73b57fb8f999"), null, "./Resources/Images/PubCrawl - 3.jpg" },
                    { new Guid("8be760e7-2fe3-478c-9af1-7c8eeee2d302"), null, "./Resources/Images/PubCrawl - 2.jpg" },
                    { new Guid("75c22588-8485-4928-950c-c9c6aead147b"), null, "./Resources/Images/PubCrawl.jpg" },
                    { new Guid("144f0e2d-d473-4e1d-8609-d4ff7c33dcf0"), null, "./Resources/Images/River Cruise.jpg" },
                    { new Guid("2f3369c7-eab4-4a68-aacd-a33bf65de6ee"), null, "./Resources/Images/Chinese Garden.jfif" },
                    { new Guid("297e21ad-73fc-445b-bc1b-4a7f33bd6178"), null, "./Resources/Images/River Cruise - 2.jpg" },
                    { new Guid("9113948c-96ee-40e1-a8d5-a1e02f157f61"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("ded79971-b64c-4586-a95c-20ef14a93b05"), null, "./Resources/Images/SouthBrisbaneCemetery.jpg" },
                    { new Guid("89c627d1-f983-4e51-8923-dc114959bb58"), null, "./Resources/Images/UQ - 3.jpg" },
                    { new Guid("6eec68ec-029f-422e-bdea-6315cc3b1b1a"), null, "./Resources/Images/UQ - 2.jpg" },
                    { new Guid("f434a5fd-9a49-4d97-a550-75131aff32d1"), null, "./Resources/Images/UQ.jpg" },
                    { new Guid("c566499a-9798-45a6-91f1-5e19b8c71837"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("e3e562d4-e23b-4bed-bbd0-a64c0766ab8e"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("9e678a98-7b51-47f0-9e48-cb4011f1f55a"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("402389e2-1aa6-499c-b856-c115c7ff07d5"), null, "./Resources/Images/Art Gallery - 3.jpg" },
                    { new Guid("bb2673dc-e390-4c80-b254-4cff61c6233c"), null, "./Resources/Images/Art Gallery - 2.jpg" },
                    { new Guid("82e97ea6-4842-4cbe-a27c-c1e9bf194825"), null, "./Resources/Images/Art Gallery.jpg" },
                    { new Guid("686d6f2c-99fe-429e-a90d-e32b5be773c5"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("fac58468-507d-4c9b-a003-b680a35d83d0"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("4c7bab6c-62c5-4d61-9aa2-b8a8b4a4d35c"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("91e38c43-e111-452e-890d-983c8d47212a"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("7c4ca2da-0962-458a-925e-4c3a7d0a1281"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("c699cabf-e325-43aa-b4f2-b00abfda9347"), null, "./Resources/Images/River Cruise - 3.jpg" },
                    { new Guid("98a0e55f-1d0b-43bd-aa58-abb13abbd8c0"), null, "./Resources/Images/Kayak.jfif" }
                });

            migrationBuilder.InsertData(
                table: "ResourceMeta",
                columns: new[] { "ResourceMetaId", "AddedOn", "Description", "Extension", "FileName", "Length", "MimeType", "OnDisk" },
                values: new object[,]
                {
                    { new Guid("75c22588-8485-4928-950c-c9c6aead147b"), new DateTime(2020, 10, 26, 18, 49, 43, 522, DateTimeKind.Local).AddTicks(7073), "PubCrawl", ".jpg", "PubCrawl.jpg", 0L, "image/jpeg", true },
                    { new Guid("1d0f2cf6-e3d2-4e15-8ec2-6881657f748a"), new DateTime(2020, 10, 26, 18, 49, 43, 533, DateTimeKind.Local).AddTicks(1087), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("f65e0db3-2d9c-41d2-8927-73a73b73265c"), new DateTime(2020, 10, 26, 18, 49, 43, 535, DateTimeKind.Local).AddTicks(3746), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("b709f89a-f5e0-4ec7-9c76-751f7a6d4945"), new DateTime(2020, 10, 26, 18, 49, 43, 535, DateTimeKind.Local).AddTicks(1966), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("b83c1b23-4398-4fb5-8d1c-3acd152295e2"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(9402), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("883f88fc-b84e-47bb-b98d-6c56672195c8"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(9760), "Lone Pine - 3", ".jpg", "Lone Pine - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("8fdbfb57-5cbb-458d-a169-2bd168fc7b77"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(8953), "Lone Pine - 2", ".jpg", "Lone Pine - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("f9d0493a-80e7-48cc-a4a4-5816240e42e1"), new DateTime(2020, 10, 26, 18, 49, 43, 535, DateTimeKind.Local).AddTicks(5467), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("82128831-4ff9-422e-98bb-6b32a467cf1e"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(7705), "Lone Pine", ".jpg", "Lone Pine.jpg", 0L, "image/jpeg", true },
                    { new Guid("1c1ff2a3-d840-4d33-850e-245894fc0840"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(7524), "Stradbroke - 2", ".jpg", "Stradbroke - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("6dfdc65e-d8e2-44ba-947b-70f2a06890ba"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(7177), "Stradbroke", ".jpg", "Stradbroke.jpg", 0L, "image/jpeg", true },
                    { new Guid("3d6fb5da-35f4-45e8-913b-4f1031dfc3a8"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(6785), "Australia Zoo - 3", ".jpg", "Australia Zoo - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("e6928025-5f3f-4373-82e4-7358e6fa981d"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(6043), "Australia Zoo - 2", ".jpg", "Australia Zoo - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("a0ed18dc-2ea2-43e3-af53-26c35bd57c97"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(6568), "Australia Zoo", ".jpg", "Australia Zoo.jpg", 0L, "image/jpeg", true },
                    { new Guid("f24684a2-a0e3-4386-a51c-0faf060bb527"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(5208), "Rainforest - 3", ".jpg", "Rainforest - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("0a69255d-ec07-4b33-ad36-8db326b4f0bc"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(8233), "Stradbroke - 3", ".jpg", "Stradbroke - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("44790094-a4f1-489d-8e52-2949ff4e8722"), new DateTime(2020, 10, 26, 18, 49, 43, 535, DateTimeKind.Local).AddTicks(7104), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("bbb053eb-e39c-45c3-906b-a4d5740a72ee"), new DateTime(2020, 10, 26, 18, 49, 43, 533, DateTimeKind.Local).AddTicks(2799), "Aquarium", ".jpg", "Aquarium.jpg", 0L, "image/jpeg", true },
                    { new Guid("d1b9e5c0-5de8-4f85-bb9e-158646cea872"), new DateTime(2020, 10, 26, 18, 49, 43, 535, DateTimeKind.Local).AddTicks(8783), "Aquarium - 2", ".jpg", "Aquarium - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("546b2b8a-4049-41c5-91f4-a0b52723b7cd"), new DateTime(2020, 10, 26, 18, 49, 43, 509, DateTimeKind.Local).AddTicks(7918), "Sydney Aquarium", ".jfif", "Sydney Aquarium.jfif", 0L, "image/jpeg", true },
                    { new Guid("8cb41680-4b0c-4123-b973-a2161ca40549"), new DateTime(2020, 10, 26, 18, 49, 43, 509, DateTimeKind.Local).AddTicks(7134), "Beer", ".jfif", "Beer.jfif", 0L, "image/jpeg", true },
                    { new Guid("81a456da-326e-4fcd-a1aa-781771f6d471"), new DateTime(2020, 10, 26, 18, 49, 43, 509, DateTimeKind.Local).AddTicks(295), "Uni Tour", ".jfif", "Uni Tour.jfif", 0L, "image/jpeg", true },
                    { new Guid("684f0bb7-60b0-42b2-bdf0-dd7bb7f93388"), new DateTime(2020, 10, 26, 18, 49, 43, 508, DateTimeKind.Local).AddTicks(6429), "Melbourne", ".jfif", "Melbourne.jfif", 0L, "image/jpeg", true },
                    { new Guid("2a3b8bf8-3b89-472f-b14c-281638d33ede"), new DateTime(2020, 10, 26, 18, 49, 43, 508, DateTimeKind.Local).AddTicks(5526), "Sydney", ".jfif", "Sydney.jfif", 0L, "image/jpeg", true },
                    { new Guid("97c9293a-0782-4493-9f5f-3a522fedbbf8"), new DateTime(2020, 10, 26, 18, 49, 43, 500, DateTimeKind.Local).AddTicks(1296), "Brisbane", ".jfif", "Brisbane.jfif", 0L, "image/jpeg", true },
                    { new Guid("96009bac-acf0-4075-8636-b3eb34e759c5"), new DateTime(2020, 10, 26, 18, 49, 43, 519, DateTimeKind.Local).AddTicks(3459), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("86dc2975-8a56-4bc5-83a1-0fe53e0b7271"), new DateTime(2020, 10, 26, 18, 49, 43, 518, DateTimeKind.Local).AddTicks(558), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("5f5a5317-e775-48ae-8207-c3fdc46bf15b"), new DateTime(2020, 10, 26, 18, 49, 43, 520, DateTimeKind.Local).AddTicks(6891), "Velma", ".jfif", "Velma.jfif", 0L, "image/jpeg", true },
                    { new Guid("e90aa38e-cec2-4426-a120-942531fddad6"), new DateTime(2020, 10, 26, 18, 49, 43, 515, DateTimeKind.Local).AddTicks(4063), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("77c5b1d5-aff1-4790-942a-adb25e144763"), new DateTime(2020, 10, 26, 18, 49, 43, 520, DateTimeKind.Local).AddTicks(6375), "harold", ".jfif", "harold.jfif", 0L, "image/jpeg", true },
                    { new Guid("e46d4c0c-c8c4-4ee2-a571-097e5818b1a3"), new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(4005), "Chinatown - 3", ".jpg", "Chinatown - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("d3cba90e-e23e-4149-a2f9-1c64aff63c30"), new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(2342), "Chinatown - 2", ".jpg", "Chinatown - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("05f7cd29-8d10-49f0-a5eb-e9c7f66cd3fc"), new DateTime(2020, 10, 26, 18, 49, 43, 533, DateTimeKind.Local).AddTicks(4479), "Chinatown", ".jpg", "Chinatown.jpg", 0L, "image/jpeg", true },
                    { new Guid("594022e7-2551-477c-a06e-39cfcc25c59b"), new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(637), "Aquarium - 3", ".jpg", "Aquarium - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("8d633412-ec63-40b3-ae4b-b1dceee96cb3"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(4472), "Rainforest - 2", ".jpg", "Rainforest - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("2f3369c7-eab4-4a68-aacd-a33bf65de6ee"), new DateTime(2020, 10, 26, 18, 49, 43, 509, DateTimeKind.Local).AddTicks(8487), "Chinese Garden", ".jfif", "Chinese Garden.jfif", 0L, "image/jpeg", true },
                    { new Guid("1784a2e6-4829-4d5b-b04c-37558c1e2bd6"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(6017), "Rainforest", ".jpg", "Rainforest.jpg", 0L, "image/jpeg", true },
                    { new Guid("c3732c52-67e2-4581-8727-16f648ff7e05"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(824), "SouthBrisbaneCemetery - 2", ".jpg", "SouthBrisbaneCemetery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("369ed4bd-0b46-44e3-a79d-b23575ec97ae"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(5860), "Rock Climbing - 3", ".jpg", "Rock Climbing - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("6319e57f-e415-4ed4-8694-136d3b5b9bee"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(5168), "Rock Climbing - 2", ".jpg", "Rock Climbing - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("7453da0d-3417-4bad-9ab7-65672fed228d"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(3931), "Rock Climbing", ".jpg", "Rock Climbing.jpg", 0L, "image/jpeg", true },
                    { new Guid("19155dc4-933a-42fd-8764-0b8c1253b21e"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(4360), "Scavenger - 3", ".jpg", "Scavenger - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("d153eda8-4c32-4b31-b31a-3aa322b3d1c0"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(3679), "Scavenger - 2", ".jpg", "Scavenger - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("541b83fb-0794-4f29-aeb0-8b43ada570c2"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(3094), "Scavenger", ".jpg", "Scavenger.jpg", 0L, "image/jpeg", true },
                    { new Guid("144f0e2d-d473-4e1d-8609-d4ff7c33dcf0"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(4674), "River Cruise", ".jpg", "River Cruise.jpg", 0L, "image/jpeg", true },
                    { new Guid("eb7d6e82-95e7-47d1-82b6-da7d0bfd87ac"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(2956), "Segway - 3", ".jpg", "Segway - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("a2b0e0a9-7e39-4593-9cbe-7faada91b3c1"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(2342), "Segway", ".jpg", "Segway.jpg", 0L, "image/jpeg", true },
                    { new Guid("a11bf867-424a-4a99-861f-dbec0d697686"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(1436), "UniTour - 3", ".jpg", "UniTour - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("8f994356-b6ff-4c50-9f84-a3d378ebbb14"), new DateTime(2020, 10, 26, 18, 49, 43, 526, DateTimeKind.Local).AddTicks(9050), "UniTour - 2", ".jpg", "UniTour - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("74f3d463-d15b-4f9c-acbd-f2c967f7d1fb"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(1292), "UniTour", ".jpg", "UniTour.jpg", 0L, "image/jpeg", true },
                    { new Guid("5c4e8732-4c22-4cae-a80b-73b57fb8f999"), new DateTime(2020, 10, 26, 18, 49, 43, 526, DateTimeKind.Local).AddTicks(1396), "PubCrawl - 3", ".jpg", "PubCrawl - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("8be760e7-2fe3-478c-9af1-7c8eeee2d302"), new DateTime(2020, 10, 26, 18, 49, 43, 525, DateTimeKind.Local).AddTicks(9261), "PubCrawl - 2", ".jpg", "PubCrawl - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("5dc5fd8e-ac89-421b-9b91-975b07ce719b"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(2257), "Segway - 2", ".jpg", "Segway - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("297e21ad-73fc-445b-bc1b-4a7f33bd6178"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(6583), "River Cruise - 2", ".jpg", "River Cruise - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("c699cabf-e325-43aa-b4f2-b00abfda9347"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(7275), "River Cruise - 3", ".jpg", "River Cruise - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("9113948c-96ee-40e1-a8d5-a1e02f157f61"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(6860), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("ded79971-b64c-4586-a95c-20ef14a93b05"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(5354), "SouthBrisbaneCemetery", ".jpg", "SouthBrisbaneCemetery.jpg", 0L, "image/jpeg", true },
                    { new Guid("89c627d1-f983-4e51-8923-dc114959bb58"), new DateTime(2020, 10, 26, 18, 49, 43, 533, DateTimeKind.Local).AddTicks(8517), "UQ - 3", ".jpg", "UQ - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("6eec68ec-029f-422e-bdea-6315cc3b1b1a"), new DateTime(2020, 10, 26, 18, 49, 43, 533, DateTimeKind.Local).AddTicks(6849), "UQ - 2", ".jpg", "UQ - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("f434a5fd-9a49-4d97-a550-75131aff32d1"), new DateTime(2020, 10, 26, 18, 49, 43, 532, DateTimeKind.Local).AddTicks(954), "UQ", ".jpg", "UQ.jpg", 0L, "image/jpeg", true },
                    { new Guid("c566499a-9798-45a6-91f1-5e19b8c71837"), new DateTime(2020, 10, 26, 18, 49, 43, 529, DateTimeKind.Local).AddTicks(3050), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("e3e562d4-e23b-4bed-bbd0-a64c0766ab8e"), new DateTime(2020, 10, 26, 18, 49, 43, 529, DateTimeKind.Local).AddTicks(1124), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("9e678a98-7b51-47f0-9e48-cb4011f1f55a"), new DateTime(2020, 10, 26, 18, 49, 43, 525, DateTimeKind.Local).AddTicks(2753), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("402389e2-1aa6-499c-b856-c115c7ff07d5"), new DateTime(2020, 10, 26, 18, 49, 43, 528, DateTimeKind.Local).AddTicks(9009), "Art Gallery - 3", ".jpg", "Art Gallery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("bb2673dc-e390-4c80-b254-4cff61c6233c"), new DateTime(2020, 10, 26, 18, 49, 43, 528, DateTimeKind.Local).AddTicks(7185), "Art Gallery - 2", ".jpg", "Art Gallery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("82e97ea6-4842-4cbe-a27c-c1e9bf194825"), new DateTime(2020, 10, 26, 18, 49, 43, 525, DateTimeKind.Local).AddTicks(866), "Art Gallery", ".jpg", "Art Gallery.jpg", 0L, "image/jpeg", true },
                    { new Guid("686d6f2c-99fe-429e-a90d-e32b5be773c5"), new DateTime(2020, 10, 26, 18, 49, 43, 528, DateTimeKind.Local).AddTicks(5317), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("fac58468-507d-4c9b-a003-b680a35d83d0"), new DateTime(2020, 10, 26, 18, 49, 43, 528, DateTimeKind.Local).AddTicks(3495), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("4c7bab6c-62c5-4d61-9aa2-b8a8b4a4d35c"), new DateTime(2020, 10, 26, 18, 49, 43, 524, DateTimeKind.Local).AddTicks(8732), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("91e38c43-e111-452e-890d-983c8d47212a"), new DateTime(2020, 10, 26, 18, 49, 43, 528, DateTimeKind.Local).AddTicks(1552), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("7c4ca2da-0962-458a-925e-4c3a7d0a1281"), new DateTime(2020, 10, 26, 18, 49, 43, 527, DateTimeKind.Local).AddTicks(9644), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("10c6416d-fd9a-45be-aa78-4388c3e0cdea"), new DateTime(2020, 10, 26, 18, 49, 43, 534, DateTimeKind.Local).AddTicks(3554), "SouthBrisbaneCemetery - 3", ".jpg", "SouthBrisbaneCemetery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("98a0e55f-1d0b-43bd-aa58-abb13abbd8c0"), new DateTime(2020, 10, 26, 18, 49, 43, 509, DateTimeKind.Local).AddTicks(9047), "Kayak", ".jfif", "Kayak.jfif", 0L, "image/jpeg", true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "ProfilePicResourceMetaId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "83038430-bb33-4025-8831-cc35ae9c4897", 0, "9b02cc9e-0c39-4026-a6e9-9c726faeb505", "Daphne.Blakeo@pretend.com", false, "Daphne", "Blakeo", false, null, "DAPHNE.BLAKEO@PRETEND.COM", "DAPHNE", "AQAAAAEAACcQAAAAEK2ydI/YcxodSyqn5G35J8qD/Qau7QVT6wEX4gTe/JYnVMnsvh5PIE6iFwHdZg0qxw==", null, false, 400, new Guid("96009bac-acf0-4075-8636-b3eb34e759c5"), "8ca26124-44c1-4222-9262-77c8387931c5", false, "Daphne" },
                    { "ebad14c7-b201-40f1-bcda-22139ab920db", 0, "61c7b0f6-a547-4945-b973-0b38f991d981", "Fred.Jones@pretend.com", false, "Fred", "Jones", false, null, "FRED.JONES@PRETEND.COM", "FRED", "AQAAAAEAACcQAAAAEAqpXZj6Kt2TGlgBD8Gdi6jV44/iB29Nk4pNCT9EwsryFXadg0ANFwjdL/O/vVI8Sw==", null, false, 375, new Guid("86dc2975-8a56-4bc5-83a1-0fe53e0b7271"), "437d6fa6-3a83-41fe-80e5-2168e7f23b7a", false, "Fred" },
                    { "f0f54405-89bf-48ff-8508-56dcc05607a7", 0, "00003355-c100-4cc0-a350-7599353f2b16", "Velma.Dinkley@pretend.com", false, "Velma", "Dinkley", false, null, "VELMA.DINKLEY@PRETEND.COM", "VELMA", "AQAAAAEAACcQAAAAEMgrn7C+02r7MwH3/CQ4tcHGQUTePuj+4Q9c6O0Zoo/ds1CwAP211vtWoBqmzbs32w==", null, false, 400, new Guid("5f5a5317-e775-48ae-8207-c3fdc46bf15b"), "bdaf0958-3464-4d71-99ec-17d899dd0b8f", false, "Velma" },
                    { "dd86d61d-004f-45de-a0b3-21ed54e0c250", 0, "e623ac84-5f80-4f13-8f1e-8f6a89435e94", "Scoobert.Doo@pretend.com", false, "Scoobert", "Doo", false, null, "SCOOBERT.DOO@PRETEND.COM", "SCOOBY", "AQAAAAEAACcQAAAAEMfZKvL7M4JjMu0m7+6gmYqI47lmVaCf7fZWAz0GU8ehGZUh7vCyBHmeclqgAhqBdw==", null, false, 500, new Guid("e90aa38e-cec2-4426-a120-942531fddad6"), "bfe5032d-7dc3-44e2-8c3d-d199d26df51f", false, "Scooby" },
                    { "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", 0, "55f43b85-215d-46a4-b9fb-017e3b47d5be", "Norville.Rogers@pretend.com", false, "Norville", "Rogers", false, null, "NORVILLE.ROGERS@PRETEND.COM", "SHAGGY", "AQAAAAEAACcQAAAAEJZjbUtZr6PpHxp1F7kXVa+zpKVsdd4ZtPznkPR04CgvcS71fC7Jld4tLkmgqkruLg==", null, false, 100, new Guid("77c5b1d5-aff1-4790-942a-adb25e144763"), "fa38b714-5799-45be-aa36-557c98ebd27f", false, "Shaggy" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "CoverImageId", "Description", "Lattitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new Guid("75c22588-8485-4928-950c-c9c6aead147b"), "Tour Brisbanes best bars and clubs in a night of fun", -27.470568, 153.024866, "Pub Crawl" },
                    { new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), new Guid("97c9293a-0782-4493-9f5f-3a522fedbbf8"), "Brisbane is the capital of Queensland, and the third most populous city in Australia.", -27.467939999999999, 153.02808999999999, "Brisbane" },
                    { new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), new Guid("05f7cd29-8d10-49f0-a5eb-e9c7f66cd3fc"), "China Town’s great for Yum Cha, Chinese Food and a visit to Dessert Story, they have the best Taiwanese desserts!", -37.811279999999996, 144.96880899999999, "Melbourne Chinatown" },
                    { new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), new Guid("bbb053eb-e39c-45c3-906b-a4d5740a72ee"), "See the more than 13,000 aquatic life forms in the 14 themed areas.", -33.869349999999997, 151.202192, "SEA LIFE Sydney Aquarium" },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new Guid("1d0f2cf6-e3d2-4e15-8ec2-6881657f748a"), "Explore the lush plant life, hidden pagodas, and colorful statues at your own speed, or join one of three informative tours that run during the day at no extra cost.", -33.876274000000002, 151.20280199999999, "Chinese Garden of Friendship" },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new Guid("b83c1b23-4398-4fb5-8d1c-3acd152295e2"), "Australia's most famouse landmark", -33.856650999999999, 151.21527599999999, "Sydney Opera House" },
                    { new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), new Guid("82128831-4ff9-422e-98bb-6b32a467cf1e"), " Meet a koala, hand-feed kangaroos and engage with a large variety of Australian wildlife in Lone Pine's beautiful, natural settings. Guests experience happy, healthy animals and engaged staff, as well as the opportunity to support conservation and enjoy educational opportunities.", -27.533553000000001, 152.968783, "Lone Pine Koala Sanctuary" },
                    { new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), new Guid("6dfdc65e-d8e2-44ba-947b-70f2a06890ba"), "A beautiful tropical island located west of brisbane, with many beautiful white beaches and sea views. Dolphins, turtles and many other marine and coastal life are frequently sighted here.", -27.509374999999999, 153.46847099999999, "North Stradbroke Island" },
                    { new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), new Guid("a0ed18dc-2ea2-43e3-af53-26c35bd57c97"), "This is the perfect pick for animal lovers! Visit the world-renowned Australia Zoo—also known as “The Home of the Crocodile Hunter” and owned by Steve Irwin’s widow Terri Irwin.", -26.835488000000002, 152.963134, "Australia Zoo" },
                    { new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), new Guid("1784a2e6-4829-4d5b-b04c-37558c1e2bd6"), "Explore the mountains, caves, and waterfalls of the Gold Coast Hinterlands. Admire the Natural Bridge and trek to Cave Creek waterfall in the Springbrook National Park.", -28.20928, 153.27017499999999, "Springbrook and Tamborine Rainforest" },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("ded79971-b64c-4586-a95c-20ef14a93b05"), "Super spooooky at night", -27.498972999999999, 153.02712, "South Brisbane Cemetery" },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new Guid("f434a5fd-9a49-4d97-a550-75131aff32d1"), "The best uni in brisbane", -27.497408, 153.01367999999999, "UQ" },
                    { new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), new Guid("9e678a98-7b51-47f0-9e48-cb4011f1f55a"), "Explore Melbourne from the river at your own pace and without anyone getting in the way on this afternoon kayaking tour.", -37.820380999999998, 144.95828700000001, "Melbourne City Afternoon Kayak Tour" },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), new Guid("82e97ea6-4842-4cbe-a27c-c1e9bf194825"), "ArtVo is an art gallery with a difference—this immersive art space encourages people to touch, play, and interact with the art, and there are 11 themed zones to explore.", -37.812648000000003, 144.93767099999999, "ArtVo Immersive Gallery Experience" },
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new Guid("4c7bab6c-62c5-4d61-9aa2-b8a8b4a4d35c"), "In an intimate group limited to 16 people, float over Yarra Valley vineyards at sunrise, when the landscapes look most magical.", -37.631934999999999, 145.400453, "Yarra Valley Balloon Flight at Sunrise" },
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new Guid("9113948c-96ee-40e1-a8d5-a1e02f157f61"), "Get the chance to spot humpback whales right outside of Sydney on this speed boat tour from Circular Quay or Manly Wharf.", -33.856788999999999, 151.20925199999999, "Sydney Whale-Watching by Speed Boat" },
                    { new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), new Guid("144f0e2d-d473-4e1d-8609-d4ff7c33dcf0"), "The only way to see the Brisbane River. The back drop of the city underlights with these reflecting off the river create an amazing ambiance.", -27.470407999999999, 153.01887199999999, "Twilight River Cruise" },
                    { new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), new Guid("7453da0d-3417-4bad-9ab7-65672fed228d"), "Kangaroo Point cliff face is a unique sight in the heart of Brisbane, climb the cliffs whilst they are lit up in the evening. The urban cliff offers a unique rock climbing experience, allowing beginners and the experienced to be challenged by its various routes.", -27.477733000000001, 153.034482, "Brisbane Rock Climbing" },
                    { new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), new Guid("541b83fb-0794-4f29-aeb0-8b43ada570c2"), "Walk to all the best landmarks and hidden gems, answering trivia questions and solving challenges. Work with your team or compete against them, as you learn new facts and create memorable experiences on this 2h activity. ", -27.465717999999999, 153.024058, "Brisbane Scavenger Hunt" },
                    { new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), new Guid("a2b0e0a9-7e39-4593-9cbe-7faada91b3c1"), "Make the most of your vacation time in Brisbane by embarking on a Segway tour of the riverside city. Zooming around on this two-wheeled, self-balancing, electric scooter allows you to cover much more ground", -27.476327999999999, 153.009019, "Brisbane Segway Sightseeing Tour" },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new Guid("74f3d463-d15b-4f9c-acbd-f2c967f7d1fb"), "Visit Brisbanes best universities", -27.477118999999998, 153.02837199999999, "Uni tour" },
                    { new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), new Guid("2a3b8bf8-3b89-472f-b14c-281638d33ede"), "Sydney is the state capital of New South Wales and the most populous city in Australia and Oceania.", -33.867849999999997, 151.20732000000001, "Sydney" },
                    { new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), new Guid("684f0bb7-60b0-42b2-bdf0-dd7bb7f93388"), "Melbourne is the capital of Victoria, and the second-most populous city in Australia and Oceania.", -37.814, 144.96332000000001, "Melbourne" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c" },
                    { "dd86d61d-004f-45de-a0b3-21ed54e0c250", "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c" },
                    { "f0f54405-89bf-48ff-8508-56dcc05607a7", "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c" },
                    { "ebad14c7-b201-40f1-bcda-22139ab920db", "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c" },
                    { "83038430-bb33-4025-8831-cc35ae9c4897", "ce9b0fa1-3b79-4a3f-87d0-9e6ee6c2668c" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Country", "Video" },
                values: new object[,]
                {
                    { new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), "Australia", "nDHlEG48b-M" },
                    { new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), "Australia", "Yc7r_bbt00M" },
                    { new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), "Australia", "Rzn5WGnS350" }
                });

            migrationBuilder.InsertData(
                table: "Shortlist",
                columns: new[] { "ShortlistId", "ListName", "UserId" },
                values: new object[,]
                {
                    { new Guid("e23024b3-ae0d-4e96-a3df-51e9273c6640"), "Shag Spots", "92ebc31c-1b2c-4f3b-809a-deb931cafcf5" },
                    { new Guid("dfcc4256-5044-4e28-a2df-2c661799299a"), "Ghost Sightings", "92ebc31c-1b2c-4f3b-809a-deb931cafcf5" },
                    { new Guid("2a147563-87eb-4a73-ac43-dde2961f9351"), "Brisbane Holiday", "f0f54405-89bf-48ff-8508-56dcc05607a7" },
                    { new Guid("5b519114-c16f-4a65-8cdb-e1aeabec256f"), "Bucket List", "f0f54405-89bf-48ff-8508-56dcc05607a7" }
                });

            migrationBuilder.InsertData(
                table: "CityUser",
                columns: new[] { "CityId", "UserId", "Count" },
                values: new object[,]
                {
                    { new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), "f0f54405-89bf-48ff-8508-56dcc05607a7", 3 },
                    { new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), "f0f54405-89bf-48ff-8508-56dcc05607a7", 1 },
                    { new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), "dd86d61d-004f-45de-a0b3-21ed54e0c250", 2 },
                    { new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", 3 }
                });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "ContentId", "Address", "Capacity", "CityId", "EconomicRating", "EnvironmentalRating", "Featured", "SocialRating", "Website" },
                values: new object[,]
                {
                    { new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), null, 5, new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), 5, 4, false, 5, null },
                    { new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), null, 5, new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), 5, 5, false, 4, null },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), null, 5, new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), 3, 5, false, 5, null },
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), null, 5, new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), 5, 3, false, 5, null },
                    { new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), null, 5, new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 3, 2, false, 5, null },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), null, 5, new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 4, 5, false, 5, null },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), null, 5, new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 3, 3, false, 5, null },
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), null, 5, new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 5, 5, false, 5, null },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 3, false, 5, null },
                    { new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 4, false, 4, null },
                    { new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 3, 4, false, 5, null },
                    { new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 5, false, 5, null },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 4, false, 3, null },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 5, false, 5, null },
                    { new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 3, false, 5, null },
                    { new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 3, false, 5, null },
                    { new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 3, false, 5, null },
                    { new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 4, 3, false, 5, null },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 3, false, 5, null },
                    { new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), null, 5, new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 5, 5, false, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Reward",
                columns: new[] { "RewardId", "CityId", "CountThreshold", "CoverImageId", "Description", "ExpiryDate", "Name", "Value" },
                values: new object[,]
                {
                    { new Guid("7c2c854b-c7f0-43e1-9a0c-30d4a5e8c75b"), new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 1, new Guid("81a456da-326e-4fcd-a1aa-781771f6d471"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uni Tour Discount", "15% Off your next tour" },
                    { new Guid("98c0a91a-47fa-4d14-8796-4e7fd07d605f"), new Guid("4f3eaffa-fe79-4376-a666-cc32a77102cc"), 4, new Guid("8cb41680-4b0c-4123-b973-a2161ca40549"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drink Discount", "$5 OFF a jug of beer with any meal purchase" },
                    { new Guid("bce60943-30d6-4791-8e53-4e382c1aa787"), new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 1, new Guid("546b2b8a-4049-41c5-91f4-a0b52723b7cd"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sydney Aquarium Voucher", "5% off your next ticket" },
                    { new Guid("10045a6d-838f-4ad5-b69b-910c24a8d94b"), new Guid("5d0a295e-51dc-4940-88f3-6067d74432d9"), 0, new Guid("2f3369c7-eab4-4a68-aacd-a33bf65de6ee"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free tour of Chinese Garden of Friendship", "Free tour with any ticket purchase" },
                    { new Guid("756c43fd-4b09-4e8e-9a54-f84dd69333a2"), new Guid("7128040d-cbe9-4259-8e0e-59baeb9a2e48"), 1, new Guid("98a0e55f-1d0b-43bd-aa58-abb13abbd8c0"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Save when you bring a Friend", "1/2 price for the scond person for your Melbourne City Afternoon Kayak Tour" }
                });

            migrationBuilder.InsertData(
                table: "Activity",
                column: "ActivityId",
                values: new object[]
                {
                    new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"),
                    new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"),
                    new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"),
                    new Guid("c460d346-0628-44ce-8846-222a24f72dcd"),
                    new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"),
                    new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"),
                    new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"),
                    new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"),
                    new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"),
                    new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524")
                });

            migrationBuilder.InsertData(
                table: "ContentResourceMeta",
                columns: new[] { "ContentId", "ResourceMetaId", "Number" },
                values: new object[,]
                {
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new Guid("9113948c-96ee-40e1-a8d5-a1e02f157f61"), 0 },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("ded79971-b64c-4586-a95c-20ef14a93b05"), 0 },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("c3732c52-67e2-4581-8727-16f648ff7e05"), 1 },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("10c6416d-fd9a-45be-aa78-4388c3e0cdea"), 2 },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), new Guid("402389e2-1aa6-499c-b856-c115c7ff07d5"), 2 },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), new Guid("bb2673dc-e390-4c80-b254-4cff61c6233c"), 1 },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), new Guid("82e97ea6-4842-4cbe-a27c-c1e9bf194825"), 0 },
                    { new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), new Guid("1784a2e6-4829-4d5b-b04c-37558c1e2bd6"), 0 },
                    { new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), new Guid("8d633412-ec63-40b3-ae4b-b1dceee96cb3"), 1 },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new Guid("44790094-a4f1-489d-8e52-2949ff4e8722"), 2 },
                    { new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), new Guid("f24684a2-a0e3-4386-a51c-0faf060bb527"), 2 },
                    { new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), new Guid("a0ed18dc-2ea2-43e3-af53-26c35bd57c97"), 0 },
                    { new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), new Guid("e6928025-5f3f-4373-82e4-7358e6fa981d"), 1 },
                    { new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), new Guid("3d6fb5da-35f4-45e8-913b-4f1031dfc3a8"), 2 },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new Guid("f65e0db3-2d9c-41d2-8927-73a73b73265c"), 2 },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new Guid("b709f89a-f5e0-4ec7-9c76-751f7a6d4945"), 1 },
                    { new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), new Guid("1c1ff2a3-d840-4d33-850e-245894fc0840"), 1 },
                    { new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), new Guid("0a69255d-ec07-4b33-ad36-8db326b4f0bc"), 2 },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new Guid("b83c1b23-4398-4fb5-8d1c-3acd152295e2"), 0 },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new Guid("1d0f2cf6-e3d2-4e15-8ec2-6881657f748a"), 0 },
                    { new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), new Guid("8fdbfb57-5cbb-458d-a169-2bd168fc7b77"), 1 },
                    { new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), new Guid("883f88fc-b84e-47bb-b98d-6c56672195c8"), 2 },
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new Guid("686d6f2c-99fe-429e-a90d-e32b5be773c5"), 2 },
                    { new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), new Guid("bbb053eb-e39c-45c3-906b-a4d5740a72ee"), 0 },
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new Guid("fac58468-507d-4c9b-a003-b680a35d83d0"), 1 },
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new Guid("4c7bab6c-62c5-4d61-9aa2-b8a8b4a4d35c"), 0 },
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new Guid("91e38c43-e111-452e-890d-983c8d47212a"), 2 },
                    { new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), new Guid("594022e7-2551-477c-a06e-39cfcc25c59b"), 2 },
                    { new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), new Guid("d1b9e5c0-5de8-4f85-bb9e-158646cea872"), 1 },
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new Guid("7c4ca2da-0962-458a-925e-4c3a7d0a1281"), 1 },
                    { new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), new Guid("6dfdc65e-d8e2-44ba-947b-70f2a06890ba"), 0 },
                    { new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), new Guid("82128831-4ff9-422e-98bb-6b32a467cf1e"), 0 },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new Guid("f9d0493a-80e7-48cc-a4a4-5816240e42e1"), 1 },
                    { new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), new Guid("e46d4c0c-c8c4-4ee2-a571-097e5818b1a3"), 2 },
                    { new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), new Guid("5dc5fd8e-ac89-421b-9b91-975b07ce719b"), 1 },
                    { new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), new Guid("eb7d6e82-95e7-47d1-82b6-da7d0bfd87ac"), 2 },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new Guid("a11bf867-424a-4a99-861f-dbec0d697686"), 2 },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new Guid("8f994356-b6ff-4c50-9f84-a3d378ebbb14"), 1 },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new Guid("74f3d463-d15b-4f9c-acbd-f2c967f7d1fb"), 0 },
                    { new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), new Guid("541b83fb-0794-4f29-aeb0-8b43ada570c2"), 0 },
                    { new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), new Guid("d153eda8-4c32-4b31-b31a-3aa322b3d1c0"), 1 },
                    { new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), new Guid("19155dc4-933a-42fd-8764-0b8c1253b21e"), 2 },
                    { new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), new Guid("d3cba90e-e23e-4149-a2f9-1c64aff63c30"), 1 },
                    { new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), new Guid("c566499a-9798-45a6-91f1-5e19b8c71837"), 2 },
                    { new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), new Guid("7453da0d-3417-4bad-9ab7-65672fed228d"), 0 },
                    { new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), new Guid("6319e57f-e415-4ed4-8694-136d3b5b9bee"), 1 },
                    { new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), new Guid("369ed4bd-0b46-44e3-a79d-b23575ec97ae"), 2 },
                    { new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), new Guid("a2b0e0a9-7e39-4593-9cbe-7faada91b3c1"), 0 },
                    { new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), new Guid("05f7cd29-8d10-49f0-a5eb-e9c7f66cd3fc"), 0 },
                    { new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), new Guid("e3e562d4-e23b-4bed-bbd0-a64c0766ab8e"), 1 },
                    { new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), new Guid("144f0e2d-d473-4e1d-8609-d4ff7c33dcf0"), 0 },
                    { new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), new Guid("297e21ad-73fc-445b-bc1b-4a7f33bd6178"), 1 },
                    { new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), new Guid("c699cabf-e325-43aa-b4f2-b00abfda9347"), 2 },
                    { new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), new Guid("9e678a98-7b51-47f0-9e48-cb4011f1f55a"), 0 },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new Guid("8be760e7-2fe3-478c-9af1-7c8eeee2d302"), 1 },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new Guid("f434a5fd-9a49-4d97-a550-75131aff32d1"), 0 },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new Guid("6eec68ec-029f-422e-bdea-6315cc3b1b1a"), 1 },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new Guid("89c627d1-f983-4e51-8923-dc114959bb58"), 2 },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new Guid("75c22588-8485-4928-950c-c9c6aead147b"), 0 },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new Guid("5c4e8732-4c22-4cae-a80b-73b57fb8f999"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Destination",
                column: "DestinationId",
                values: new object[]
                {
                    new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"),
                    new Guid("52f1c504-1511-4725-ad45-cc8221be5182"),
                    new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"),
                    new Guid("50c46513-7068-4448-b4be-6ec4102931eb"),
                    new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"),
                    new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"),
                    new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"),
                    new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"),
                    new Guid("195eed8e-e575-4860-9ca8-9be147816287"),
                    new Guid("9f68887a-103c-4b54-a057-0e939bdab61a")
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "ContentId", "UserId", "Date" },
                values: new object[,]
                {
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(4138) },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", new DateTime(2020, 10, 26, 18, 49, 43, 530, DateTimeKind.Local).AddTicks(2301) },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), "dd86d61d-004f-45de-a0b3-21ed54e0c250", new DateTime(2020, 10, 26, 18, 49, 43, 530, DateTimeKind.Local).AddTicks(8986) },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), "dd86d61d-004f-45de-a0b3-21ed54e0c250", new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(4175) },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), "f0f54405-89bf-48ff-8508-56dcc05607a7", new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(4182) },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), "f0f54405-89bf-48ff-8508-56dcc05607a7", new DateTime(2020, 10, 26, 18, 49, 43, 536, DateTimeKind.Local).AddTicks(4189) },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", new DateTime(2020, 10, 26, 18, 49, 43, 530, DateTimeKind.Local).AddTicks(5981) },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), "f0f54405-89bf-48ff-8508-56dcc05607a7", new DateTime(2020, 10, 26, 18, 49, 43, 530, DateTimeKind.Local).AddTicks(9069) },
                    { new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), "f0f54405-89bf-48ff-8508-56dcc05607a7", new DateTime(2020, 10, 26, 18, 49, 43, 530, DateTimeKind.Local).AddTicks(9063) }
                });

            migrationBuilder.InsertData(
                table: "QR",
                columns: new[] { "QRId", "ContentId", "Expiry" },
                values: new object[,]
                {
                    { new Guid("419118f9-c599-45cc-af39-e5c2a4cd42ad"), new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3844) },
                    { new Guid("ad323d73-bc68-4a50-a671-9caaec358363"), new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3872) },
                    { new Guid("35706d34-ba8e-4d9a-b462-56d6dc77442a"), new Guid("c460d346-0628-44ce-8846-222a24f72dcd"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3848) },
                    { new Guid("1f893175-6e3f-43a9-8423-7a3786782f47"), new Guid("ea625e98-329a-40b2-9dbf-52195ce70686"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3874) },
                    { new Guid("0cc9bd7b-8168-4f3c-bc17-3506fc6162d0"), new Guid("3ca8202a-a751-4959-8e29-292bdd3eeeca"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3852) },
                    { new Guid("9bfa91ed-db90-421d-b801-7fdcd1d77c21"), new Guid("1d0d9e39-0901-4373-a038-a36cb3693f25"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3863) },
                    { new Guid("396d1fc0-5f4b-4210-8ef9-a8da6af71653"), new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3868) },
                    { new Guid("427409c5-6835-422e-80cd-440fa43fbc5b"), new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(2672) },
                    { new Guid("34fc6cf4-a1c6-450c-b042-cf63439b32eb"), new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3792) },
                    { new Guid("2fd3044a-4399-470f-b4e7-caaaf4778052"), new Guid("597b2f2a-4482-4ac3-84b8-644832ae1203"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3826) },
                    { new Guid("cd39311d-270b-4a65-91e3-7f017b7d3e1d"), new Guid("0dad5e05-4fc6-4da4-a33a-3e23875609d7"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3830) },
                    { new Guid("48dc9302-ab3f-4c5c-ae99-ad26890e2b67"), new Guid("aea7974a-6ac2-4f7c-bf76-4917110aea08"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3833) },
                    { new Guid("10acf718-78a0-4942-af03-d0b0967d8fb8"), new Guid("f36bbf85-1722-4cee-9ba0-440f20bb7059"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3838) },
                    { new Guid("ac6078f5-b650-40af-989d-ca283f2953d3"), new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3857) },
                    { new Guid("3a79f425-9b29-4e56-a7e8-2a19763a851e"), new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3859) },
                    { new Guid("62198a08-23f2-427d-b237-23c853ee888e"), new Guid("9f68887a-103c-4b54-a057-0e939bdab61a"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3876) },
                    { new Guid("79b70237-ee27-4b33-a4c1-e4c7a979a196"), new Guid("8c73105f-d5d5-4750-bd70-41cb420e8542"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3865) },
                    { new Guid("fa3016e5-e381-4763-9f6e-7c3c5dbfe8b6"), new Guid("26ff627a-fc10-4566-a01c-bf96098a3568"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3861) },
                    { new Guid("8e24ae3d-e5dd-44e4-9020-7a78197fe252"), new Guid("9afe9a8c-d5a8-430b-9ce9-fb7bb91cdbee"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3866) },
                    { new Guid("cf4720b2-a19b-4192-b058-e57833e3a136"), new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new DateTime(2021, 1, 26, 18, 49, 43, 537, DateTimeKind.Local).AddTicks(3841) }
                });

            migrationBuilder.InsertData(
                table: "ShortlistContent",
                columns: new[] { "ContentId", "ShortlistId", "Number" },
                values: new object[,]
                {
                    { new Guid("7fe540e3-f820-4eda-9c3f-ca5cbf8dfd8b"), new Guid("e23024b3-ae0d-4e96-a3df-51e9273c6640"), 2 },
                    { new Guid("0848d69e-79a4-4c8e-a7bb-e1ea7b84c5ef"), new Guid("e23024b3-ae0d-4e96-a3df-51e9273c6640"), 2 },
                    { new Guid("52f1c504-1511-4725-ad45-cc8221be5182"), new Guid("5b519114-c16f-4a65-8cdb-e1aeabec256f"), 2 },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("e23024b3-ae0d-4e96-a3df-51e9273c6640"), 3 },
                    { new Guid("50c46513-7068-4448-b4be-6ec4102931eb"), new Guid("dfcc4256-5044-4e28-a2df-2c661799299a"), 1 },
                    { new Guid("2af61612-427c-4bd1-9aee-44bc2976609a"), new Guid("2a147563-87eb-4a73-ac43-dde2961f9351"), 1 },
                    { new Guid("195eed8e-e575-4860-9ca8-9be147816287"), new Guid("2a147563-87eb-4a73-ac43-dde2961f9351"), 3 },
                    { new Guid("05dcebb3-ba3b-429c-80ed-a8a7f4eb65c9"), new Guid("5b519114-c16f-4a65-8cdb-e1aeabec256f"), 1 },
                    { new Guid("9506fb4d-2ab6-4b42-aef9-106092c27524"), new Guid("2a147563-87eb-4a73-ac43-dde2961f9351"), 2 }
                });

            migrationBuilder.InsertData(
                table: "UserReward",
                columns: new[] { "UserId", "RewardId", "AppUserId" },
                values: new object[,]
                {
                    { "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", new Guid("98c0a91a-47fa-4d14-8796-4e7fd07d605f"), null },
                    { "dd86d61d-004f-45de-a0b3-21ed54e0c250", new Guid("98c0a91a-47fa-4d14-8796-4e7fd07d605f"), null },
                    { "f0f54405-89bf-48ff-8508-56dcc05607a7", new Guid("7c2c854b-c7f0-43e1-9a0c-30d4a5e8c75b"), null },
                    { "dd86d61d-004f-45de-a0b3-21ed54e0c250", new Guid("7c2c854b-c7f0-43e1-9a0c-30d4a5e8c75b"), null },
                    { "92ebc31c-1b2c-4f3b-809a-deb931cafcf5", new Guid("7c2c854b-c7f0-43e1-9a0c-30d4a5e8c75b"), null },
                    { "f0f54405-89bf-48ff-8508-56dcc05607a7", new Guid("98c0a91a-47fa-4d14-8796-4e7fd07d605f"), null },
                    { "f0f54405-89bf-48ff-8508-56dcc05607a7", new Guid("756c43fd-4b09-4e8e-9a54-f84dd69333a2"), null }
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
                name: "IX_Reward_CoverImageId",
                table: "Reward",
                column: "CoverImageId");

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
