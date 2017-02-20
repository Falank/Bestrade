namespace Bestrade.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        company = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.company);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        shipment = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        company = c.String(maxLength: 128),
                        cost = c.Double(nullable: false),
                        remark = c.String(),
                        complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.shipment)
                .ForeignKey("dbo.Companies", t => t.company)
                .Index(t => t.company);
            
            CreateTable(
                "dbo.FBA",
                c => new
                    {
                        sku = c.String(nullable: false, maxLength: 128),
                        mod_num = c.String(maxLength: 128),
                        condition = c.String(),
                        remark = c.String(),
                    })
                .PrimaryKey(t => t.sku)
                .ForeignKey("dbo.Mods", t => t.mod_num)
                .Index(t => t.mod_num);
            
            CreateTable(
                "dbo.Mods",
                c => new
                    {
                        mod_num = c.String(nullable: false, maxLength: 128),
                        asin = c.String(),
                        title = c.String(),
                        remark = c.String(),
                    })
                .PrimaryKey(t => t.mod_num);
            
            CreateTable(
                "dbo.Packs",
                c => new
                    {
                        purchase = c.String(nullable: false, maxLength: 128),
                        sku = c.String(nullable: false, maxLength: 128),
                        unit_cost = c.Double(nullable: false),
                        qty = c.Int(nullable: false),
                        remark = c.String(),
                    })
                .PrimaryKey(t => new { t.purchase, t.sku })
                .ForeignKey("dbo.FBA", t => t.sku, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.purchase, cascadeDelete: true)
                .Index(t => t.purchase)
                .Index(t => t.sku);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        purchase = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false, storeType: "date"),
                        supplier = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.purchase)
                .ForeignKey("dbo.Suppliers", t => t.supplier)
                .Index(t => t.supplier);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        supplier = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.supplier);
            
            CreateTable(
                "dbo.PackShipment",
                c => new
                    {
                        purchase = c.String(nullable: false, maxLength: 128),
                        sku = c.String(nullable: false, maxLength: 128),
                        shipment = c.String(nullable: false, maxLength: 128),
                        qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.purchase, t.sku, t.shipment })
                .ForeignKey("dbo.Packs", t => new { t.purchase, t.sku }, cascadeDelete: true)
                .ForeignKey("dbo.Shipments", t => t.shipment, cascadeDelete: true)
                .Index(t => new { t.purchase, t.sku })
                .Index(t => t.shipment);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PackShipment", "shipment", "dbo.Shipments");
            DropForeignKey("dbo.PackShipment", new[] { "purchase", "sku" }, "dbo.Packs");
            DropForeignKey("dbo.Packs", "purchase", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "supplier", "dbo.Suppliers");
            DropForeignKey("dbo.Packs", "sku", "dbo.FBA");
            DropForeignKey("dbo.FBA", "mod_num", "dbo.Mods");
            DropForeignKey("dbo.Shipments", "company", "dbo.Companies");
            DropIndex("dbo.PackShipment", new[] { "shipment" });
            DropIndex("dbo.PackShipment", new[] { "purchase", "sku" });
            DropIndex("dbo.Purchases", new[] { "supplier" });
            DropIndex("dbo.Packs", new[] { "sku" });
            DropIndex("dbo.Packs", new[] { "purchase" });
            DropIndex("dbo.FBA", new[] { "mod_num" });
            DropIndex("dbo.Shipments", new[] { "company" });
            DropTable("dbo.PackShipment");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.Packs");
            DropTable("dbo.Mods");
            DropTable("dbo.FBA");
            DropTable("dbo.Shipments");
            DropTable("dbo.Companies");
        }
    }
}
