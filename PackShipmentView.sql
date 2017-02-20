SELECT   dbo.Packs.purchase, dbo.Packs.sku, dbo.Packs.qty AS p_qty, ISNULL(SUM(dbo.PackShipment.qty), 0) AS s_qty, 
                dbo.Packs.remark
FROM      dbo.PackShipment FULL OUTER JOIN
                dbo.Packs ON dbo.PackShipment.purchase = dbo.Packs.purchase AND dbo.PackShipment.sku = dbo.Packs.sku
GROUP BY dbo.Packs.qty, dbo.Packs.remark, dbo.Packs.purchase, dbo.Packs.sku