SELECT   dbo.Purchases.purchase, dbo.Purchases.date, dbo.Purchases.supplier, ISNULL(dbo.Packs.sku, N'') AS sku, 
                ISNULL(dbo.Packs.unit_cost, 0) AS unit_cost, ISNULL(dbo.Packs.qty, 0) AS qty, ISNULL(dbo.Packs.remark, N'') 
                AS remark
FROM      dbo.Purchases LEFT OUTER JOIN
                dbo.Packs ON dbo.Purchases.purchase = dbo.Packs.purchase