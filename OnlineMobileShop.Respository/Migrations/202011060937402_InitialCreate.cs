namespace OnlineMobileShop.Respository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PhoneNumber = c.Long(nullable: false),
                        Gender = c.String(nullable: false),
                        MailID = c.String(nullable: false, maxLength: 35),
                        Password = c.String(nullable: false, maxLength: 15),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Mobiles",
                c => new
                    {
                        MobileID = c.Int(nullable: false, identity: true),
                        BrandID = c.Int(nullable: false),
                        Model = c.String(),
                        Battery = c.Int(nullable: false),
                        RAM = c.Int(nullable: false),
                        ROM = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MobileID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .Index(t => t.BrandID);
            
            CreateStoredProcedure(
                "dbo.Brand_Insert",
                p => new
                    {
                        BrandName = p.String(maxLength: 15),
                    },
                body:
                    @"INSERT [dbo].[Brands]([BrandName])
                      VALUES (@BrandName)
                      
                      DECLARE @BrandID int
                      SELECT @BrandID = [BrandID]
                      FROM [dbo].[Brands]
                      WHERE @@ROWCOUNT > 0 AND [BrandID] = scope_identity()
                      
                      SELECT t0.[BrandID]
                      FROM [dbo].[Brands] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[BrandID] = @BrandID"
            );
            
            CreateStoredProcedure(
                "dbo.Brand_Update",
                p => new
                    {
                        BrandID = p.Int(),
                        BrandName = p.String(maxLength: 15),
                    },
                body:
                    @"UPDATE [dbo].[Brands]
                      SET [BrandName] = @BrandName
                      WHERE ([BrandID] = @BrandID)"
            );
            
            CreateStoredProcedure(
                "dbo.Brand_Delete",
                p => new
                    {
                        BrandID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Brands]
                      WHERE ([BrandID] = @BrandID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Brand_Delete");
            DropStoredProcedure("dbo.Brand_Update");
            DropStoredProcedure("dbo.Brand_Insert");
            DropForeignKey("dbo.Mobiles", "BrandID", "dbo.Brands");
            DropIndex("dbo.Mobiles", new[] { "BrandID" });
            DropTable("dbo.Mobiles");
            DropTable("dbo.Brands");
            DropTable("dbo.Accounts");
        }
    }
}
