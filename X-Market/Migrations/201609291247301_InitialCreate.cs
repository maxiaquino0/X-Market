namespace X_Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentTypes",
                c => new
                    {
                        DocumentTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.DocumentTypeID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        Adress = c.String(),
                        Document = c.String(),
                        DocumentTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeID, cascadeDelete: true)
                .Index(t => t.DocumentTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "DocumentTypeID", "dbo.DocumentTypes");
            DropIndex("dbo.Customers", new[] { "DocumentTypeID" });
            DropTable("dbo.Customers");
            DropTable("dbo.DocumentTypes");
        }
    }
}
