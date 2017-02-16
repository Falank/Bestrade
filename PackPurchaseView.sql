SELECT        dbo.Purchases.purchase_id, dbo.Purchases.purchase_date, dbo.Purchases.supplier_name, ISNULL(dbo.Packs.sku, N'') AS sku, ISNULL(dbo.Packs.unit_cost, 0) AS unit_cost, ISNULL(dbo.Packs.qty, 0) AS qty, 
                         ISNULL(dbo.Packs.pack_remark, N'') AS pack_remark
FROM            dbo.Purchases LEFT OUTER JOIN
                         dbo.Packs ON dbo.Purchases.purchase_id = dbo.Packs.purchase_id