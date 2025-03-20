
CREATE TRIGGER trg_Adquisiciones_Auditoria
ON dbo.Adquisiciones
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Manejar INSERT (Registra todos los campos)
    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        INSERT INTO Historial (IdAdquisicion, Operacion, FechaCambio, Usuario, Cambios)
        SELECT 
            i.Id, 
            'INSERT', 
            GETDATE(), 
            SUSER_NAME(),
			JSON_QUERY('{' + STRING_AGG(
				'"' + c.campo + '": { "antiguo": "' + ISNULL(c.antiguo, 'NULL') + '", "nuevo": "' + ISNULL(c.nuevo, 'NULL') + '" }'
			, ', ') + '}'
			)
        FROM inserted i
        CROSS APPLY (VALUES
            ('Presupuesto', NULL, CAST(i.Presupuesto AS NVARCHAR(50))),
            ('Unidad', NULL, i.Unidad),
            ('TipoBienServicio', NULL, i.TipoDeBien),
            ('Cantidad', NULL, CAST(i.Cantidad AS NVARCHAR(50))),
            ('ValorUnitario', NULL, CAST(i.ValorUnitario AS NVARCHAR(50))),
            ('ValorTotal', NULL, CAST(i.ValorTotal AS NVARCHAR(50))),
            ('FechaAdquisicion', NULL, CONVERT(NVARCHAR, i.FechaAdquisicion, 120)), -- Formato ISO
            ('Proveedor', NULL, i.Proveedor),
            ('Documentacion', NULL, i.Documentacion),
            ('Activo', NULL, CAST(i.Activo AS NVARCHAR(5))) -- Convertir bit a string ('0' o '1')
        ) AS c(campo, antiguo, nuevo)
        GROUP BY i.Id;
    END

    -- Manejar UPDATE (Solo registrar campos que cambiaron)
    ELSE
    BEGIN
        INSERT INTO Historial (IdAdquisicion, Operacion, FechaCambio, Usuario, Cambios)
        SELECT 
            i.Id, 
            'UPDATE', 
            GETDATE(), 
            SUSER_NAME(),
			JSON_QUERY('{' + STRING_AGG(
				'"' + c.campo + '": { "antiguo": "' + ISNULL(c.antiguo, 'NULL') + '", "nuevo": "' + ISNULL(c.nuevo, 'NULL') + '" }'
			, ', ') + '}')
        FROM inserted i
        JOIN deleted d ON i.Id = d.Id
        CROSS APPLY (VALUES
            ('Presupuesto', CAST(d.Presupuesto AS NVARCHAR(50)), CAST(i.Presupuesto AS NVARCHAR(50))),
            ('Unidad', d.Unidad, i.Unidad),
            ('TipoBienServicio', d.TipoDeBien, i.TipoDeBien),
            ('Cantidad', CAST(d.Cantidad AS NVARCHAR(50)), CAST(i.Cantidad AS NVARCHAR(50))),
            ('ValorUnitario', CAST(d.ValorUnitario AS NVARCHAR(50)), CAST(i.ValorUnitario AS NVARCHAR(50))),
            ('ValorTotal', CAST(d.ValorTotal AS NVARCHAR(50)), CAST(i.ValorTotal AS NVARCHAR(50))),
            ('FechaAdquisicion', CONVERT(NVARCHAR, d.FechaAdquisicion, 120), CONVERT(NVARCHAR, i.FechaAdquisicion, 120)), 
            ('Proveedor', d.Proveedor, i.Proveedor),
            ('Documentacion', d.Documentacion, i.Documentacion),
            ('Activo', CAST(d.Activo AS NVARCHAR(5)), CAST(i.Activo AS NVARCHAR(5))) -- Convertir bit a string ('0' o '1')
        ) AS c(campo, antiguo, nuevo)
        WHERE c.antiguo <> c.nuevo -- solo guardar si hubo un cambio
        GROUP BY i.Id;
    END
END;