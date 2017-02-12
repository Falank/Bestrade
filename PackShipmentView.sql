SELECT        dbo.Packs.purchase_id, dbo.Packs.sku, dbo.Packs.qty AS p_qty, ISNULL(SUM(dbo.PackShipment.qty), 0) AS s_qty, dbo.Packs.pack_remark
FROM            dbo.PackShipment FULL OUTER JOIN
                         dbo.Packs ON dbo.PackShipment.purchase_id = dbo.Packs.purchase_id AND dbo.PackShipment.sku = dbo.Packs.sku
GROUP BY dbo.Packs.qty, dbo.Packs.pack_remark, dbo.Packs.purchase_id, dbo.Packs.sku