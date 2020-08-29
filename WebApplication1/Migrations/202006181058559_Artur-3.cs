namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artur3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FakturaPozycjas", "PKWiU", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FakturaPozycjas", "PKWiU");
        }
    }
}
