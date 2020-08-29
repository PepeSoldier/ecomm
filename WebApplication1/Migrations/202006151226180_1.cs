namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FakturaPozycjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FakturaId = c.Int(nullable: false),
                        Lp = c.Int(nullable: false),
                        Symbol = c.Int(nullable: false),
                        Nazwa = c.String(),
                        Jm = c.String(),
                        Ilosc = c.Int(nullable: false),
                        CenaJednostkowa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VAT = c.String(),
                        WartoscNetto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WartoscVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WartoscBrutto = c.Decimal(nullable: false, precision: 18, scale: 3),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fakturas", t => t.FakturaId, cascadeDelete: true)
                .Index(t => t.FakturaId);
            
            CreateTable(
                "dbo.Fakturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numer = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FakturaPozycjas", "FakturaId", "dbo.Fakturas");
            DropIndex("dbo.FakturaPozycjas", new[] { "FakturaId" });
            DropTable("dbo.Fakturas");
            DropTable("dbo.FakturaPozycjas");
        }
    }
}
