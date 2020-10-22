using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderListAPI.Migrations
{
    public partial class testingPhysicalFile : Migration
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
                    CoverImageId = table.Column<Guid>(nullable: false),
                    ProfilePicResourceMetaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ResourceMeta_ProfilePicResourceMetaId",
                        column: x => x.ProfilePicResourceMetaId,
                        principalTable: "ResourceMeta",
                        principalColumn: "ResourceMetaId",
                        onDelete: ReferentialAction.Restrict);
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
                    { "9ff49cf1-81a4-430e-bd38-3b9c229b4b18", "03563a7f-6530-4cc4-ace3-239867460c9d", "User", "USER" },
                    { "d098a91f-0510-42cf-b3fc-13f7ce5a0fac", "60cc499d-9476-46a1-a9ad-18cf15c039d5", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CoverImageId", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Points", "ProfilePicResourceMetaId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", 0, "2c6cdd71-dcf5-4940-afa2-db856ac86a78", new Guid("1831a3bd-3b3c-478c-b08d-defdf2962ae8"), "Norville.Rogers@pretend.com", false, "Norville", "Rogers", false, null, "NORVILLE.ROGERS@PRETEND.COM", "SHAGGY", "AQAAAAEAACcQAAAAEF3qt0uZcmmFTy1u45CGgyLw0MtwiUIQiIYC84Vq1lO0TfS/D2gp6USVgY9I1mCIrA==", null, false, 100, null, "37ede683-e669-445d-9ab5-39c0732453ea", false, "Shaggy" },
                    { "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", 0, "2d0a63f2-7eb5-43a4-a967-23c9dd3564dc", new Guid("3412cbe5-ce64-420e-983f-296a6c1ccb15"), "Scoobert.Doo@pretend.com", false, "Scoobert", "Doo", false, null, "SCOOBERT.DOO@PRETEND.COM", "SCOOBY", "AQAAAAEAACcQAAAAEHNMZI02vUY3MtxMjBS3rtU/dcoQxX1i/uzrOmFnJ+wEuoOO3xnYE227vWpvXO9TCw==", null, false, 500, null, "85eb5410-ef84-4d0c-9c1b-f59d35e66835", false, "Scooby" },
                    { "4f2759c5-9dac-40a2-8d0d-e97e136581ac", 0, "aac1c66c-67a2-48b3-90ad-652d93557d25", new Guid("20ec2ac8-718d-48d3-8021-ffdebf4904d0"), "Velma.Dinkley@pretend.com", false, "Velma", "Dinkley", false, null, "VELMA.DINKLEY@PRETEND.COM", "VELMA", "AQAAAAEAACcQAAAAEJk15FOjwDVDHsjaLI1aDh/EdP4PM0MP5UvQjF03csf49XXxzyLBeaeZCCu9W/osDw==", null, false, 400, null, "b0283e2a-cfe2-432d-a4d5-b6cdef7022bf", false, "Velma" },
                    { "ba044904-1bb9-49f8-8ac5-a2c29efaabba", 0, "d6795f31-dce6-4700-9030-3a0764f57d7a", new Guid("8e04f2d4-60e3-4c6a-91c8-dbb25e5ddcae"), "Fred.Jones@pretend.com", false, "Fred", "Jones", false, null, "FRED.JONES@PRETEND.COM", "FRED", "AQAAAAEAACcQAAAAEJVBGwrfxgGFRLga10VkYuXtRcntYI0zRc7Ck/vfACnU2jl6t/8/lou7xSUDb3USTw==", null, false, 375, null, "7a9247e7-fa96-493c-8b23-864ebd7568b6", false, "Fred" },
                    { "3dc25e29-820b-4972-a7c5-6fc3362e6c9c", 0, "a99ddde4-eca5-4f0a-93cc-d9d13f297b88", new Guid("fe487884-2a5f-4936-9ed7-9b98756b755b"), "Daphne.Blakeo@pretend.com", false, "Daphne", "Blakeo", false, null, "DAPHNE.BLAKEO@PRETEND.COM", "DAPHNE", "AQAAAAEAACcQAAAAEN3tyw9g0sIUmT5M1RdhmfvrP5AMlrUlxAo/CXHT9rNoRXltaZHq5oOicaCiBbuq/w==", null, false, 400, null, "b8f5e7da-3496-45c3-a0be-eca48503d7a4", false, "Daphne" }
                });

            migrationBuilder.InsertData(
                table: "Resource",
                columns: new[] { "ResourceId", "Data", "FilePath" },
                values: new object[,]
                {
                    { new Guid("1afe8e59-f39f-4882-a4c6-5e4e48ba111c"), null, "./Resources/Images/SouthBrisbaneCemetery - 3.jpg" },
                    { new Guid("1b533d07-aa98-4efc-b301-61c84297a193"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("f3b539ee-038b-4cce-8854-411107430195"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("37df0390-a4b0-4cce-9e89-0ae704764791"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("f526a386-539e-4b89-a25c-d02530c4917c"), null, "./Resources/Images/OperaHouse.jpg" },
                    { new Guid("058be29f-19e9-4192-b4a6-4687c9143a72"), null, "./Resources/Images/OperaHouse - 2.jpg" },
                    { new Guid("54b4aad0-b9d7-4e6a-8dbb-bbbc64a0f8d5"), null, "./Resources/Images/OperaHouse - 3.jpg" },
                    { new Guid("7453b934-85bd-4d07-9857-11d7b03b64ad"), null, "./Resources/Images/Aquarium.jpg" },
                    { new Guid("3cd55a01-3eb1-4d04-a815-021dfab88205"), null, "./Resources/Images/Aquarium - 2.jpg" },
                    { new Guid("ce23a3d1-4b37-49a1-a8e7-1af5aa9002f9"), null, "./Resources/Images/Chinatown - 3.jpg" },
                    { new Guid("b99f2dd2-09ab-4219-9a7a-c00c64a011e3"), null, "./Resources/Images/Chinatown.jpg" },
                    { new Guid("b732cde8-93a8-474e-a94c-3aa85d41a626"), null, "./Resources/Images/Chinatown - 2.jpg" },
                    { new Guid("be959719-d2ff-4223-af1b-0b42c876be37"), null, "./Resources/Images/SouthBrisbaneCemetery - 2.jpg" },
                    { new Guid("3de69d8c-1320-4d8a-ad0a-89787815cf14"), null, "./Resources/Images/harold.jfif" },
                    { new Guid("e07913cf-3a41-47bb-87f6-b923c87a8e0f"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("2b5a184d-73f5-46c2-accf-4b8382752bea"), null, "./Resources/Images/Velma.jfif" },
                    { new Guid("780bcb01-f687-4c5d-a48a-546a8fdc6cde"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("ad2f5827-eabf-4445-bed1-fa9ab488cf2d"), null, "./Resources/Images/DefaultUser.jfif" },
                    { new Guid("92e3cb68-51de-417c-96c8-e60b7bc42cb5"), null, "./Resources/Images/Brisbane.jfif" },
                    { new Guid("4123ff6f-d457-438e-afc6-c5ad8e5ef788"), null, "./Resources/Images/Aquarium - 3.jpg" },
                    { new Guid("51c085b5-d7e7-44ca-8c07-50abe8d4e974"), null, "./Resources/Images/SouthBrisbaneCemetery.jpg" },
                    { new Guid("b63e6bdd-def3-4170-a338-7b557266169d"), null, "./Resources/Images/UQ.jpg" },
                    { new Guid("72be0ec5-4b92-4021-84d9-61cf8766e5f8"), null, "./Resources/Images/UQ - 2.jpg" },
                    { new Guid("3d3fe775-f8d7-4961-9416-6ce026069acb"), null, "./Resources/Images/PubCrawl.jpg" },
                    { new Guid("a4ef40ed-ba0d-455c-8835-bc171a33d66f"), null, "./Resources/Images/PubCrawl - 2.jpg" },
                    { new Guid("f11036f7-4cfc-486f-a7f8-971e259c1dec"), null, "./Resources/Images/PubCrawl - 3.jpg" },
                    { new Guid("90966de5-403b-4b66-ae2c-35a9ad22ad62"), null, "./Resources/Images/UniTour.jpg" },
                    { new Guid("5686c190-65b9-4d21-a876-e6ca20f19173"), null, "./Resources/Images/UniTour - 2.jpg" },
                    { new Guid("03a72a14-6ad1-4885-9349-441490edc007"), null, "./Resources/Images/UniTour - 3.jpg" },
                    { new Guid("cbcf83b1-d1ba-4c88-b365-eb227610dd97"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("df6fe27d-af5a-4f21-be80-992c190c7643"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("bbba1559-2c3b-4c1e-a722-a7ac293e1443"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("6b23dfc5-fe1a-4f6e-af0d-267cc0582fdd"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("2c5938de-9a09-40eb-94a3-52a102edac85"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("fd12e6a0-0717-4ff8-b84b-cf26125323d7"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("a58bdc56-3385-4920-a800-71753b202c61"), null, "./Resources/Images/Art Gallery.jpg" },
                    { new Guid("4615dbae-e74b-4582-b5ff-a13b2d257ba1"), null, "./Resources/Images/Art Gallery - 2.jpg" },
                    { new Guid("2b656ed4-bed7-4472-9b0d-faf09f40fe9d"), null, "./Resources/Images/Art Gallery - 3.jpg" },
                    { new Guid("c23b9bc8-07d3-4a59-a093-7cb73f3e93f3"), null, "./Resources/Images/Balloon Flight.jpg" },
                    { new Guid("b2ef3fea-efc8-49d9-98bc-34cbe4a3c00a"), null, "./Resources/Images/Balloon Flight - 2.jpg" },
                    { new Guid("270266af-04c3-4afd-963b-d2bf3b21b2ba"), null, "./Resources/Images/Balloon Flight - 3.jpg" },
                    { new Guid("15bb6143-62f9-422d-b746-afdc116fa582"), null, "./Resources/Images/Sydney.jfif" },
                    { new Guid("df78444d-517e-48ca-bcd0-f4d211657744"), null, "./Resources/Images/UQ - 3.jpg" },
                    { new Guid("6acb6efa-c82f-4590-b2d8-fb11cc3d8b58"), null, "./Resources/Images/Melbourne.jfif" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", "9ff49cf1-81a4-430e-bd38-3b9c229b4b18" },
                    { "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", "9ff49cf1-81a4-430e-bd38-3b9c229b4b18" },
                    { "4f2759c5-9dac-40a2-8d0d-e97e136581ac", "9ff49cf1-81a4-430e-bd38-3b9c229b4b18" },
                    { "ba044904-1bb9-49f8-8ac5-a2c29efaabba", "9ff49cf1-81a4-430e-bd38-3b9c229b4b18" },
                    { "3dc25e29-820b-4972-a7c5-6fc3362e6c9c", "9ff49cf1-81a4-430e-bd38-3b9c229b4b18" }
                });

            migrationBuilder.InsertData(
                table: "ResourceMeta",
                columns: new[] { "ResourceMetaId", "AddedOn", "Description", "Extension", "FileName", "Length", "MimeType", "OnDisk" },
                values: new object[,]
                {
                    { new Guid("51c085b5-d7e7-44ca-8c07-50abe8d4e974"), new DateTime(2020, 10, 22, 16, 46, 24, 49, DateTimeKind.Local).AddTicks(1815), "SouthBrisbaneCemetery", ".jpg", "SouthBrisbaneCemetery.jpg", 0L, "image/jpeg", true },
                    { new Guid("be959719-d2ff-4223-af1b-0b42c876be37"), new DateTime(2020, 10, 22, 16, 46, 24, 51, DateTimeKind.Local).AddTicks(7534), "SouthBrisbaneCemetery - 2", ".jpg", "SouthBrisbaneCemetery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("1afe8e59-f39f-4882-a4c6-5e4e48ba111c"), new DateTime(2020, 10, 22, 16, 46, 24, 51, DateTimeKind.Local).AddTicks(8821), "SouthBrisbaneCemetery - 3", ".jpg", "SouthBrisbaneCemetery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("1b533d07-aa98-4efc-b301-61c84297a193"), new DateTime(2020, 10, 22, 16, 46, 24, 49, DateTimeKind.Local).AddTicks(3550), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("f3b539ee-038b-4cce-8854-411107430195"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(1543), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("37df0390-a4b0-4cce-9e89-0ae704764791"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(3161), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("f526a386-539e-4b89-a25c-d02530c4917c"), new DateTime(2020, 10, 22, 16, 46, 24, 49, DateTimeKind.Local).AddTicks(4894), "OperaHouse", ".jpg", "OperaHouse.jpg", 0L, "image/jpeg", true },
                    { new Guid("058be29f-19e9-4192-b4a6-4687c9143a72"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(5455), "OperaHouse - 2", ".jpg", "OperaHouse - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("54b4aad0-b9d7-4e6a-8dbb-bbbc64a0f8d5"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(6598), "OperaHouse - 3", ".jpg", "OperaHouse - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("7453b934-85bd-4d07-9857-11d7b03b64ad"), new DateTime(2020, 10, 22, 16, 46, 24, 49, DateTimeKind.Local).AddTicks(6489), "Aquarium", ".jpg", "Aquarium.jpg", 0L, "image/jpeg", true },
                    { new Guid("b99f2dd2-09ab-4219-9a7a-c00c64a011e3"), new DateTime(2020, 10, 22, 16, 46, 24, 49, DateTimeKind.Local).AddTicks(9253), "Chinatown", ".jpg", "Chinatown.jpg", 0L, "image/jpeg", true },
                    { new Guid("4123ff6f-d457-438e-afc6-c5ad8e5ef788"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(9528), "Aquarium - 3", ".jpg", "Aquarium - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("df78444d-517e-48ca-bcd0-f4d211657744"), new DateTime(2020, 10, 22, 16, 46, 24, 51, DateTimeKind.Local).AddTicks(24), "UQ - 3", ".jpg", "UQ - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("b732cde8-93a8-474e-a94c-3aa85d41a626"), new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(1115), "Chinatown - 2", ".jpg", "Chinatown - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("ce23a3d1-4b37-49a1-a8e7-1af5aa9002f9"), new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(3749), "Chinatown - 3", ".jpg", "Chinatown - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("3de69d8c-1320-4d8a-ad0a-89787815cf14"), new DateTime(2020, 10, 22, 16, 46, 24, 40, DateTimeKind.Local).AddTicks(110), "harold", ".jfif", "harold.jfif", 0L, "image/jpeg", true },
                    { new Guid("e07913cf-3a41-47bb-87f6-b923c87a8e0f"), new DateTime(2020, 10, 22, 16, 46, 24, 34, DateTimeKind.Local).AddTicks(3547), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("2b5a184d-73f5-46c2-accf-4b8382752bea"), new DateTime(2020, 10, 22, 16, 46, 24, 40, DateTimeKind.Local).AddTicks(1494), "Velma", ".jfif", "Velma.jfif", 0L, "image/jpeg", true },
                    { new Guid("780bcb01-f687-4c5d-a48a-546a8fdc6cde"), new DateTime(2020, 10, 22, 16, 46, 24, 37, DateTimeKind.Local).AddTicks(1214), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("ad2f5827-eabf-4445-bed1-fa9ab488cf2d"), new DateTime(2020, 10, 22, 16, 46, 24, 38, DateTimeKind.Local).AddTicks(5255), "DefaultUser", ".jfif", "DefaultUser.jfif", 0L, "image/jpeg", true },
                    { new Guid("92e3cb68-51de-417c-96c8-e60b7bc42cb5"), new DateTime(2020, 10, 22, 16, 46, 24, 18, DateTimeKind.Local).AddTicks(7215), "Brisbane", ".jfif", "Brisbane.jfif", 0L, "image/jpeg", true },
                    { new Guid("3cd55a01-3eb1-4d04-a815-021dfab88205"), new DateTime(2020, 10, 22, 16, 46, 24, 52, DateTimeKind.Local).AddTicks(8054), "Aquarium - 2", ".jpg", "Aquarium - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("72be0ec5-4b92-4021-84d9-61cf8766e5f8"), new DateTime(2020, 10, 22, 16, 46, 24, 50, DateTimeKind.Local).AddTicks(7072), "UQ - 2", ".jpg", "UQ - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("270266af-04c3-4afd-963b-d2bf3b21b2ba"), new DateTime(2020, 10, 22, 16, 46, 24, 46, DateTimeKind.Local).AddTicks(555), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("15bb6143-62f9-422d-b746-afdc116fa582"), new DateTime(2020, 10, 22, 16, 46, 24, 27, DateTimeKind.Local).AddTicks(5988), "Sydney", ".jfif", "Sydney.jfif", 0L, "image/jpeg", true },
                    { new Guid("3d3fe775-f8d7-4961-9416-6ce026069acb"), new DateTime(2020, 10, 22, 16, 46, 24, 42, DateTimeKind.Local).AddTicks(531), "PubCrawl", ".jpg", "PubCrawl.jpg", 0L, "image/jpeg", true },
                    { new Guid("a4ef40ed-ba0d-455c-8835-bc171a33d66f"), new DateTime(2020, 10, 22, 16, 46, 24, 44, DateTimeKind.Local).AddTicks(720), "PubCrawl - 2", ".jpg", "PubCrawl - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("f11036f7-4cfc-486f-a7f8-971e259c1dec"), new DateTime(2020, 10, 22, 16, 46, 24, 44, DateTimeKind.Local).AddTicks(3099), "PubCrawl - 3", ".jpg", "PubCrawl - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("90966de5-403b-4b66-ae2c-35a9ad22ad62"), new DateTime(2020, 10, 22, 16, 46, 24, 43, DateTimeKind.Local).AddTicks(658), "UniTour", ".jpg", "UniTour.jpg", 0L, "image/jpeg", true },
                    { new Guid("5686c190-65b9-4d21-a876-e6ca20f19173"), new DateTime(2020, 10, 22, 16, 46, 24, 44, DateTimeKind.Local).AddTicks(8479), "UniTour - 2", ".jpg", "UniTour - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("03a72a14-6ad1-4885-9349-441490edc007"), new DateTime(2020, 10, 22, 16, 46, 24, 44, DateTimeKind.Local).AddTicks(9869), "UniTour - 3", ".jpg", "UniTour - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("b63e6bdd-def3-4170-a338-7b557266169d"), new DateTime(2020, 10, 22, 16, 46, 24, 48, DateTimeKind.Local).AddTicks(7668), "UQ", ".jpg", "UQ.jpg", 0L, "image/jpeg", true },
                    { new Guid("df6fe27d-af5a-4f21-be80-992c190c7643"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(1281), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("cbcf83b1-d1ba-4c88-b365-eb227610dd97"), new DateTime(2020, 10, 22, 16, 46, 24, 43, DateTimeKind.Local).AddTicks(2128), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("6b23dfc5-fe1a-4f6e-af0d-267cc0582fdd"), new DateTime(2020, 10, 22, 16, 46, 24, 43, DateTimeKind.Local).AddTicks(3198), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("2c5938de-9a09-40eb-94a3-52a102edac85"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(3986), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("fd12e6a0-0717-4ff8-b84b-cf26125323d7"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(5255), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("a58bdc56-3385-4920-a800-71753b202c61"), new DateTime(2020, 10, 22, 16, 46, 24, 43, DateTimeKind.Local).AddTicks(4838), "Art Gallery", ".jpg", "Art Gallery.jpg", 0L, "image/jpeg", true },
                    { new Guid("4615dbae-e74b-4582-b5ff-a13b2d257ba1"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(6666), "Art Gallery - 2", ".jpg", "Art Gallery - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("2b656ed4-bed7-4472-9b0d-faf09f40fe9d"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(8261), "Art Gallery - 3", ".jpg", "Art Gallery - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("c23b9bc8-07d3-4a59-a093-7cb73f3e93f3"), new DateTime(2020, 10, 22, 16, 46, 24, 43, DateTimeKind.Local).AddTicks(5878), "Balloon Flight", ".jpg", "Balloon Flight.jpg", 0L, "image/jpeg", true },
                    { new Guid("b2ef3fea-efc8-49d9-98bc-34cbe4a3c00a"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(9339), "Balloon Flight - 2", ".jpg", "Balloon Flight - 2.jpg", 0L, "image/jpeg", true },
                    { new Guid("bbba1559-2c3b-4c1e-a722-a7ac293e1443"), new DateTime(2020, 10, 22, 16, 46, 24, 45, DateTimeKind.Local).AddTicks(2827), "Balloon Flight - 3", ".jpg", "Balloon Flight - 3.jpg", 0L, "image/jpeg", true },
                    { new Guid("6acb6efa-c82f-4590-b2d8-fb11cc3d8b58"), new DateTime(2020, 10, 22, 16, 46, 24, 27, DateTimeKind.Local).AddTicks(7994), "Melbourne", ".jfif", "Melbourne.jfif", 0L, "image/jpeg", true }
                });

            migrationBuilder.InsertData(
                table: "Shortlist",
                columns: new[] { "ShortlistId", "ListName", "UserId" },
                values: new object[,]
                {
                    { new Guid("40031af5-bf1b-4572-9c07-133f3673529f"), "Bucket List", "4f2759c5-9dac-40a2-8d0d-e97e136581ac" },
                    { new Guid("de987f5b-a15e-4942-bdd4-933b12b0e337"), "Brisbane Holiday", "4f2759c5-9dac-40a2-8d0d-e97e136581ac" },
                    { new Guid("7f9ba63f-ee42-4b45-a075-9b23898f06b1"), "Ghost Sightings", "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea" },
                    { new Guid("d92b3c04-9d55-43ac-bfaf-064b9a2e59b1"), "Shag Spots", "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "CoverImageId", "Description", "Lattitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new Guid("3d3fe775-f8d7-4961-9416-6ce026069acb"), "Tour Brisbanes best bars and clubs in a night of fun", -27.470568, 153.024866, "Pub Crawl" },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new Guid("90966de5-403b-4b66-ae2c-35a9ad22ad62"), "Visit Brisbanes best universities", -27.477118999999998, 153.02837199999999, "Uni tour" },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new Guid("cbcf83b1-d1ba-4c88-b365-eb227610dd97"), "Get the chance to spot humpback whales right outside of Sydney on this speed boat tour from Circular Quay or Manly Wharf.", -33.856788999999999, 151.20925199999999, "Sydney Whale-Watching by Speed Boat" },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new Guid("6b23dfc5-fe1a-4f6e-af0d-267cc0582fdd"), "In an intimate group limited to 16 people, float over Yarra Valley vineyards at sunrise, when the landscapes look most magical.", -37.631934999999999, 145.400453, "Yarra Valley Balloon Flight at Sunrise" },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), new Guid("a58bdc56-3385-4920-a800-71753b202c61"), "ArtVo is an art gallery with a difference—this immersive art space encourages people to touch, play, and interact with the art, and there are 11 themed zones to explore.", -37.812648000000003, 144.93767099999999, "ArtVo Immersive Gallery Experience" },
                    { new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), new Guid("c23b9bc8-07d3-4a59-a093-7cb73f3e93f3"), "Explore Melbourne from the river at your own pace and without anyone getting in the way on this afternoon kayaking tour.", -37.820380999999998, 144.95828700000001, "Melbourne City Afternoon Kayak Tour" },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new Guid("b63e6bdd-def3-4170-a338-7b557266169d"), "The best uni in brisbane", -27.497408, 153.01367999999999, "UQ" },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("51c085b5-d7e7-44ca-8c07-50abe8d4e974"), "Super spooooky at night", -27.498972999999999, 153.02712, "South Brisbane Cemetery" },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new Guid("1b533d07-aa98-4efc-b301-61c84297a193"), "Australia's most famouse landmark", -33.856650999999999, 151.21527599999999, "Sydney Opera House" },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new Guid("f526a386-539e-4b89-a25c-d02530c4917c"), "Explore the lush plant life, hidden pagodas, and colorful statues at your own speed, or join one of three informative tours that run during the day at no extra cost.", -33.876274000000002, 151.20280199999999, "Chinese Garden of Friendship" },
                    { new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), new Guid("7453b934-85bd-4d07-9857-11d7b03b64ad"), "See the more than 13,000 aquatic life forms in the 14 themed areas.", -33.869349999999997, 151.202192, "SEA LIFE Sydney Aquarium" },
                    { new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), new Guid("b99f2dd2-09ab-4219-9a7a-c00c64a011e3"), "China Town’s great for Yum Cha, Chinese Food and a visit to Dessert Story, they have the best Taiwanese desserts!", -37.811279999999996, 144.96880899999999, "Melbourne Chinatown" },
                    { new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), new Guid("92e3cb68-51de-417c-96c8-e60b7bc42cb5"), "Brisbane is the capital of Queensland, and the third most populous city in Australia.", -27.467939999999999, 153.02808999999999, "Brisbane" },
                    { new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), new Guid("15bb6143-62f9-422d-b746-afdc116fa582"), "Sydney is the state capital of New South Wales and the most populous city in Australia and Oceania.", -33.867849999999997, 151.20732000000001, "Sydney" },
                    { new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), new Guid("6acb6efa-c82f-4590-b2d8-fb11cc3d8b58"), "Melbourne is the capital of Victoria, and the second-most populous city in Australia and Oceania.", -37.814, 144.96332000000001, "Melbourne" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Country", "Video" },
                values: new object[] { new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), "Australia", "nDHlEG48b-M" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Country", "Video" },
                values: new object[] { new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), "Australia", "Yc7r_bbt00M" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "CityId", "Country", "Video" },
                values: new object[] { new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), "Australia", "Rzn5WGnS350" });

            migrationBuilder.InsertData(
                table: "CityUser",
                columns: new[] { "CityId", "UserId", "Count" },
                values: new object[,]
                {
                    { new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", 3 },
                    { new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", 2 },
                    { new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", 3 },
                    { new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", 1 }
                });

            migrationBuilder.InsertData(
                table: "Content",
                columns: new[] { "ContentId", "Address", "Capacity", "CityId", "EconomicRating", "EnvironmentalRating", "Featured", "SocialRating", "Website" },
                values: new object[,]
                {
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), null, 5, new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 5, 3, false, 5, null },
                    { new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), null, 5, new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), 5, 4, false, 5, null },
                    { new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), null, 5, new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), 5, 5, false, 4, null },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), null, 5, new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), 3, 5, false, 5, null },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), null, 5, new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), 5, 3, false, 5, null },
                    { new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), null, 5, new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 3, 2, false, 5, null },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), null, 5, new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 4, 5, false, 5, null },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), null, 5, new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 3, 3, false, 5, null },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), null, 5, new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 5, 4, false, 3, null },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), null, 5, new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 5, 5, false, 5, null },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), null, 5, new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 5, 3, false, 5, null },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), null, 5, new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 5, 5, false, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Reward",
                columns: new[] { "RewardId", "CityId", "CountThreshold", "Description", "ExpiryDate", "Name", "Value" },
                values: new object[,]
                {
                    { new Guid("5f3ec24a-daae-4945-8f47-ae5f91ac35a8"), new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drink Discount", "$5 OFF a jug of beer with any meal purchase" },
                    { new Guid("b59fcd99-c4f2-485b-a9dc-bb1a46e6bb62"), new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sydney Aquarium Voucher", "5% off your next ticket" },
                    { new Guid("17ed77c8-91ac-4b40-ac0b-dbe077364edd"), new Guid("c46703bc-fad3-45d5-9dc7-3003b6b4e4df"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free tour of Chinese Garden of Friendship", "Free tour with any ticket purchase" },
                    { new Guid("ffc970f1-da66-4d34-bc8a-477f01736a8a"), new Guid("1db30674-7fbd-4f87-8575-fc16066cfb45"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uni Tour Discount", "15% Off your next tour" },
                    { new Guid("5bd90a76-b34d-44a9-8efb-0096da28acee"), new Guid("05b2effc-2073-4b52-a61f-810f0d57f191"), 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Save when you bring a Friend", "1/2 price for the scond person for your Melbourne City Afternoon Kayak Tour" }
                });

            migrationBuilder.InsertData(
                table: "Activity",
                column: "ActivityId",
                values: new object[]
                {
                    new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"),
                    new Guid("a72ea357-728b-4b47-976e-1f49d298c082"),
                    new Guid("2342d266-f702-4256-aa64-395f5d072a61"),
                    new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"),
                    new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"),
                    new Guid("135131d9-011d-486d-b511-c43af3dd21e2")
                });

            migrationBuilder.InsertData(
                table: "ContentResourceMeta",
                columns: new[] { "ContentId", "ResourceMetaId", "Number" },
                values: new object[,]
                {
                    { new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), new Guid("c23b9bc8-07d3-4a59-a093-7cb73f3e93f3"), 0 },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new Guid("df6fe27d-af5a-4f21-be80-992c190c7643"), 1 },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new Guid("1b533d07-aa98-4efc-b301-61c84297a193"), 0 },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new Guid("f3b539ee-038b-4cce-8854-411107430195"), 1 },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new Guid("37df0390-a4b0-4cce-9e89-0ae704764791"), 2 },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new Guid("f526a386-539e-4b89-a25c-d02530c4917c"), 0 },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new Guid("058be29f-19e9-4192-b4a6-4687c9143a72"), 1 },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new Guid("54b4aad0-b9d7-4e6a-8dbb-bbbc64a0f8d5"), 2 },
                    { new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), new Guid("7453b934-85bd-4d07-9857-11d7b03b64ad"), 0 },
                    { new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), new Guid("3cd55a01-3eb1-4d04-a815-021dfab88205"), 1 },
                    { new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), new Guid("b2ef3fea-efc8-49d9-98bc-34cbe4a3c00a"), 1 },
                    { new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), new Guid("4123ff6f-d457-438e-afc6-c5ad8e5ef788"), 2 },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new Guid("6b23dfc5-fe1a-4f6e-af0d-267cc0582fdd"), 0 },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new Guid("cbcf83b1-d1ba-4c88-b365-eb227610dd97"), 0 },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new Guid("fd12e6a0-0717-4ff8-b84b-cf26125323d7"), 2 },
                    { new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), new Guid("b732cde8-93a8-474e-a94c-3aa85d41a626"), 1 },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), new Guid("a58bdc56-3385-4920-a800-71753b202c61"), 0 },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), new Guid("4615dbae-e74b-4582-b5ff-a13b2d257ba1"), 1 },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), new Guid("2b656ed4-bed7-4472-9b0d-faf09f40fe9d"), 2 },
                    { new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), new Guid("b99f2dd2-09ab-4219-9a7a-c00c64a011e3"), 0 },
                    { new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), new Guid("270266af-04c3-4afd-963b-d2bf3b21b2ba"), 2 },
                    { new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), new Guid("ce23a3d1-4b37-49a1-a8e7-1af5aa9002f9"), 2 },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new Guid("2c5938de-9a09-40eb-94a3-52a102edac85"), 1 },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new Guid("bbba1559-2c3b-4c1e-a722-a7ac293e1443"), 2 },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new Guid("b63e6bdd-def3-4170-a338-7b557266169d"), 0 },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new Guid("72be0ec5-4b92-4021-84d9-61cf8766e5f8"), 1 },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new Guid("3d3fe775-f8d7-4961-9416-6ce026069acb"), 0 },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("1afe8e59-f39f-4882-a4c6-5e4e48ba111c"), 2 },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new Guid("03a72a14-6ad1-4885-9349-441490edc007"), 2 },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new Guid("a4ef40ed-ba0d-455c-8835-bc171a33d66f"), 1 },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new Guid("f11036f7-4cfc-486f-a7f8-971e259c1dec"), 2 },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new Guid("df78444d-517e-48ca-bcd0-f4d211657744"), 2 },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("be959719-d2ff-4223-af1b-0b42c876be37"), 1 },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("51c085b5-d7e7-44ca-8c07-50abe8d4e974"), 0 },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new Guid("5686c190-65b9-4d21-a876-e6ca20f19173"), 1 },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new Guid("90966de5-403b-4b66-ae2c-35a9ad22ad62"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Destination",
                column: "DestinationId",
                values: new object[]
                {
                    new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"),
                    new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"),
                    new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"),
                    new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"),
                    new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"),
                    new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0")
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "ContentId", "UserId", "Date" },
                values: new object[,]
                {
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(4051) },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(3995) },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new DateTime(2020, 10, 22, 16, 46, 24, 47, DateTimeKind.Local).AddTicks(6551) },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", new DateTime(2020, 10, 22, 16, 46, 24, 47, DateTimeKind.Local).AddTicks(3417) },
                    { new Guid("2342d266-f702-4256-aa64-395f5d072a61"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new DateTime(2020, 10, 22, 16, 46, 24, 47, DateTimeKind.Local).AddTicks(6485) },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(4065) },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", new DateTime(2020, 10, 22, 16, 46, 24, 47, DateTimeKind.Local).AddTicks(6421) },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", new DateTime(2020, 10, 22, 16, 46, 24, 46, DateTimeKind.Local).AddTicks(9724) },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new DateTime(2020, 10, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(4059) }
                });

            migrationBuilder.InsertData(
                table: "QR",
                columns: new[] { "QRId", "ContentId", "Expiry" },
                values: new object[,]
                {
                    { new Guid("e3bfb998-927d-4fcd-8983-4ff18d75c05a"), new Guid("2342d266-f702-4256-aa64-395f5d072a61"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1115) },
                    { new Guid("45bfdafe-e8ca-41ae-9e6a-8d4b2718ed1a"), new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1111) },
                    { new Guid("09626616-7c04-46ba-b8ac-7ce587765289"), new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new DateTime(2021, 1, 22, 16, 46, 24, 53, DateTimeKind.Local).AddTicks(9936) },
                    { new Guid("3db4a3ef-8619-4db5-9be5-2974bca42a37"), new Guid("2b134a25-09e5-49e9-a7c6-2030cbf091f3"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1121) },
                    { new Guid("9225f341-7f60-4183-b0d9-1c419c8baeeb"), new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1077) },
                    { new Guid("46c0e9d4-982a-4599-b747-f66704d869ba"), new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1129) },
                    { new Guid("9ad3b65e-7297-4c92-aadf-4cfa4921c9b9"), new Guid("8ad16df5-87db-448f-b979-33dfff6b3f06"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1143) },
                    { new Guid("3c7ec676-ae47-415a-950f-4b46e06f72d5"), new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1138) },
                    { new Guid("0c824642-4513-4f8c-ba65-e2b0a5ba5358"), new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1126) },
                    { new Guid("8e50038b-3ba5-48ac-96ca-5b95bd37495f"), new Guid("943a43a2-3d47-47e7-b2e9-80acd90f337a"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1141) },
                    { new Guid("8e39d7d1-4b7d-49a2-adcd-2811356ede6c"), new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1131) },
                    { new Guid("d985d95d-b853-4a69-b246-86e81daba2fa"), new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new DateTime(2021, 1, 22, 16, 46, 24, 54, DateTimeKind.Local).AddTicks(1107) }
                });

            migrationBuilder.InsertData(
                table: "ShortlistContent",
                columns: new[] { "ContentId", "ShortlistId", "Number" },
                values: new object[,]
                {
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("7f9ba63f-ee42-4b45-a075-9b23898f06b1"), 1 },
                    { new Guid("4427d44d-f996-45d8-bff8-5697a51cf1ec"), new Guid("d92b3c04-9d55-43ac-bfaf-064b9a2e59b1"), 3 },
                    { new Guid("b448551f-148f-4227-91af-73a2bdc05a6d"), new Guid("de987f5b-a15e-4942-bdd4-933b12b0e337"), 1 },
                    { new Guid("35fc42d9-0e6a-4a9e-9cee-684f06350f51"), new Guid("de987f5b-a15e-4942-bdd4-933b12b0e337"), 2 },
                    { new Guid("eb127d9e-4622-4a70-a5e7-62e4fa2f7250"), new Guid("40031af5-bf1b-4572-9c07-133f3673529f"), 2 },
                    { new Guid("bc604b49-b75e-4bdb-baf1-cdaa1a21c2d0"), new Guid("de987f5b-a15e-4942-bdd4-933b12b0e337"), 3 },
                    { new Guid("a72ea357-728b-4b47-976e-1f49d298c082"), new Guid("d92b3c04-9d55-43ac-bfaf-064b9a2e59b1"), 2 },
                    { new Guid("48dd94ba-60cf-429d-8399-1ce19bffbc2b"), new Guid("d92b3c04-9d55-43ac-bfaf-064b9a2e59b1"), 2 },
                    { new Guid("135131d9-011d-486d-b511-c43af3dd21e2"), new Guid("40031af5-bf1b-4572-9c07-133f3673529f"), 1 }
                });

            migrationBuilder.InsertData(
                table: "UserReward",
                columns: new[] { "UserId", "RewardId", "AppUserId" },
                values: new object[,]
                {
                    { "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new Guid("5f3ec24a-daae-4945-8f47-ae5f91ac35a8"), null },
                    { "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", new Guid("5f3ec24a-daae-4945-8f47-ae5f91ac35a8"), null },
                    { "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", new Guid("5f3ec24a-daae-4945-8f47-ae5f91ac35a8"), null },
                    { "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new Guid("ffc970f1-da66-4d34-bc8a-477f01736a8a"), null },
                    { "f888d8ec-1103-4a30-bae2-bdf8cdbd9b67", new Guid("ffc970f1-da66-4d34-bc8a-477f01736a8a"), null },
                    { "49b3b6ec-ce67-46ff-a4e5-9c89754f9fea", new Guid("ffc970f1-da66-4d34-bc8a-477f01736a8a"), null },
                    { "4f2759c5-9dac-40a2-8d0d-e97e136581ac", new Guid("5bd90a76-b34d-44a9-8efb-0096da28acee"), null }
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
