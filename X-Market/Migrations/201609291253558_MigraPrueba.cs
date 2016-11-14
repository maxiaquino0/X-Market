namespace X_Market.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigraPrueba : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DocumentTypes", "Description", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DocumentTypes", "Description", c => c.String(maxLength: 30));
        }
    }
}
