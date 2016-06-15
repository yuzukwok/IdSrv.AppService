namespace IdSrv.Lib.Migrations.ScopeConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enabled = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DisplayName = c.String(maxLength: 200, storeType: "nvarchar"),
                        Description = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Required = c.Boolean(nullable: false),
                        Emphasize = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        IncludeAllClaimsForUser = c.Boolean(nullable: false),
                        ClaimsRule = c.String(maxLength: 200, storeType: "nvarchar"),
                        ShowInDiscoveryDocument = c.Boolean(nullable: false),
                        AllowUnrestrictedIntrospection = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScopeClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Description = c.String(maxLength: 1000, storeType: "nvarchar"),
                        AlwaysIncludeInIdToken = c.Boolean(nullable: false),
                        Scope_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scopes", t => t.Scope_Id, cascadeDelete: true)
                .Index(t => t.Scope_Id);
            
            CreateTable(
                "dbo.ScopeSecrets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Expiration = c.DateTime(precision: 0),
                        Type = c.String(maxLength: 250, storeType: "nvarchar"),
                        Value = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        Scope_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scopes", t => t.Scope_Id, cascadeDelete: true)
                .Index(t => t.Scope_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScopeSecrets", "Scope_Id", "dbo.Scopes");
            DropForeignKey("dbo.ScopeClaims", "Scope_Id", "dbo.Scopes");
            DropIndex("dbo.ScopeSecrets", new[] { "Scope_Id" });
            DropIndex("dbo.ScopeClaims", new[] { "Scope_Id" });
            DropTable("dbo.ScopeSecrets");
            DropTable("dbo.ScopeClaims");
            DropTable("dbo.Scopes");
        }
    }
}
