SELECT        ISNULL(dbo.FBA.sku, N'') AS sku, ISNULL(dbo.FBA.condition, N'') AS condition, ISNULL(dbo.FBA.remark, N'') AS f_remark, dbo.Mods.mod_num, dbo.Mods.asin, dbo.Mods.title, 
                         dbo.Mods.remark AS m_remark
FROM            dbo.FBA RIGHT OUTER JOIN
                         dbo.Mods ON dbo.FBA.mod_num = dbo.Mods.mod_num