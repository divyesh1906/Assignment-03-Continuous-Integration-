using Microsoft.EntityFrameworkCore.Migrations;

namespace Company_List.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TickerSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    TransactionTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionFee = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SharePrice = table.Column<double>(type: "float", nullable: false),
                    TransactionTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyID", "Address", "CompanyName", "TickerSymbol" },
                values: new object[,]
                {
                    { 1, "41 Hamilton Street", "Alexa", "AlX" },
                    { 2, "141 Toronto Street", "Google", "GG" },
                    { 3, "321 Microsoft Street", "Microsoft", "MSFT" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "TransactionTypeId", "CommissionFee", "TransactionName" },
                values: new object[,]
                {
                    { "S", 5.9900000000000002, "Sell" },
                    { "B", 5.2000000000000002, "Buy" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CompanyID", "Quantity", "SharePrice", "TransactionTypeId" },
                values: new object[] { 1, 1, 100, 500.0, "S" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CompanyID", "Quantity", "SharePrice", "TransactionTypeId" },
                values: new object[] { 2, 2, 100, 500.0, "B" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CompanyID", "Quantity", "SharePrice", "TransactionTypeId" },
                values: new object[] { 3, 3, 100, 500.0, "B" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompanyID",
                table: "Transactions",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionTypeId",
                table: "Transactions",
                column: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
