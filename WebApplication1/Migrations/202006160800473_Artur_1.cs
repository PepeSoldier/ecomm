namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artur_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FakturaPozycjas", "Symbol", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FakturaPozycjas", "Symbol", c => c.Int(nullable: false));
        }
    }
}
