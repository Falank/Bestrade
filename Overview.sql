SELECT        dbo.Packs.purchase_id, dbo.Packs.sku, dbo.Purchases.purchase_date, dbo.Purchases.supplier_name, dbo.Packs.unit_cost, dbo.Packs.qty AS pq, dbo.Packs.pack_remark, dbo.PackShipment.shipment_id, 
                         ISNULL(dbo.Shipments.shipping_date, CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AS shipping_date, dbo.Shipments.shipping_company, ISNULL(dbo.PackShipment.qty, 0) AS sq, 
                         ISNULL(dbo.Shipments.shipment_remark, N'') AS shipment_remark, ISNULL(dbo.Shipments.complete, 0) AS complete
FROM            dbo.Shipments RIGHT OUTER JOIN
                         dbo.PackShipment ON dbo.Shipments.shipment_id = dbo.PackShipment.shipment_id RIGHT OUTER JOIN
                         dbo.Packs ON dbo.PackShipment.purchase_id = dbo.Packs.purchase_id AND dbo.PackShipment.sku = dbo.Packs.sku LEFT OUTER JOIN
                         dbo.Purchases ON dbo.Packs.purchase_id = dbo.Purchases.purchase_id