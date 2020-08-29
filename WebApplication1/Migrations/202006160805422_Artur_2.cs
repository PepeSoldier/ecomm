namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artur_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FakturaPozycjas", "VAT", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FakturaPozycjas", "VAT", c => c.String());
        }
    }
}
