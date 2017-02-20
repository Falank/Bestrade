SELECT        dbo.Packs.purchase, dbo.Packs.sku, dbo.Purchases.date AS p_date, dbo.Purchases.supplier, dbo.Packs.unit_cost, dbo.Packs.qty AS p_qtq, dbo.Packs.remark AS p_remark, dbo.PackShipment.shipment, 
                         ISNULL(dbo.Shipments.date, CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AS s_date, dbo.Shipments.company, ISNULL(dbo.PackShipment.qty, 0) AS s_qty, ISNULL(dbo.Shipments.remark, N'') AS s_remark, 
                         ISNULL(dbo.Shipments.complete, 0) AS complete
FROM            dbo.Shipments RIGHT OUTER JOIN
                         dbo.PackShipment ON dbo.Shipments.shipment = dbo.PackShipment.shipment RIGHT OUTER JOIN
                         dbo.Packs ON dbo.PackShipment.purchase = dbo.Packs.purchase AND dbo.PackShipment.sku = dbo.Packs.sku LEFT OUTER JOIN
                         dbo.Purchases ON dbo.Packs.purchase = dbo.Purchases.purchase