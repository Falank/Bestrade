namespace Bestrade.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        shipping_company = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.shipping_company);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        shipment_id = c.String(nullable: false, maxLength: 128),
                        shipping_date = c.DateTime(nullable: false, storeType: "date"),
                        shipping_company = c.String(maxLength: 128),
                        shipment_cost = c.Double(nullable: false),
                        shipment_remark = c.String(),
                        complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.shipment_id)
                .ForeignKey("dbo.Company", t => t.shipping_company)
                .Index(t => t.shipping_company);
            
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
                        purchase_id = c.String(nullable: false, maxLength: 128),
                        sku = c.String(nullable: false, maxLength: 128),
                        unit_cost = c.Double(nullable: false),
                        qty = c.Int(nullable: false),
                        pack_remark = c.String(),
                    })
                .PrimaryKey(t => new { t.purchase_id, t.sku })
                .ForeignKey("dbo.FBA", t => t.sku, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.purchase_id, cascadeDelete: true)
                .Index(t => t.purchase_id)
                .Index(t => t.sku);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        purchase_id = c.String(nullable: false, maxLength: 128),
                        purchase_date = c.DateTime(nullable: false, storeType: "date"),
                        supplier_name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.purchase_id)
                .ForeignKey("dbo.Suppliers", t => t.supplier_name)
                .Index(t => t.supplier_name);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        supplier_name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.supplier_name);
            
            CreateTable(
                "dbo.PackShipment",
                c => new
                    {
                        purchase_id = c.String(maxLength: 128),
                        sku = c.String(maxLength: 128),
                        id = c.Int(nullable: false, identity: true),
                        shipment_id = c.String(maxLength: 128),
                        qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Packs", t => new { t.purchase_id, t.sku })
                .ForeignKey("dbo.Shipments", t => t.shipment_id)
                .Index(t => new { t.purchase_id, t.sku })
                .Index(t => t.shipment_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PackShipment", "shipment_id", "dbo.Shipments");
            DropForeignKey("dbo.PackShipment", new[] { "purchase_id", "sku" }, "dbo.Packs");
            DropForeignKey("dbo.Packs", "purchase_id", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "supplier_name", "dbo.Suppliers");
            DropForeignKey("dbo.Packs", "sku", "dbo.FBA");
            DropForeignKey("dbo.FBA", "mod_num", "dbo.Mods");
            DropForeignKey("dbo.Shipments", "shipping_company", "dbo.Company");
            DropIndex("dbo.PackShipment", new[] { "shipment_id" });
            DropIndex("dbo.PackShipment", new[] { "purchase_id", "sku" });
            DropIndex("dbo.Purchases", new[] { "supplier_name" });
            DropIndex("dbo.Packs", new[] { "sku" });
            DropIndex("dbo.Packs", new[] { "purchase_id" });
            DropIndex("dbo.FBA", new[] { "mod_num" });
            DropIndex("dbo.Shipments", new[] { "shipping_company" });
            DropTable("dbo.PackShipment");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.Packs");
            DropTable("dbo.Mods");
            DropTable("dbo.FBA");
            DropTable("dbo.Shipments");
            DropTable("dbo.Company");
        }
    }
}
