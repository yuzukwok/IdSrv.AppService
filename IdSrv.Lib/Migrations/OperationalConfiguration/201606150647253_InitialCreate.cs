namespace IdSrv.Lib.Migrations.OperationalConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consents",
                c => new
                    {
                        Subject = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        ClientId = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Scopes = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.Subject, t.ClientId });
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        TokenType = c.Short(nullable: false),
                        SubjectId = c.String(maxLength: 200, storeType: "nvarchar"),
                        ClientId = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        JsonCode = c.String(nullable: false, unicode: false),
                        Expiry = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => new { t.Key, t.TokenType });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tokens");
            DropTable("dbo.Consents");
        }
    }
}
