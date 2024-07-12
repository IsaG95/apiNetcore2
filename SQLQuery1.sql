CREATE TABLE CanalVenta (
    IdCanalVta INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255) NOT NULL,
    Empresa VARCHAR(50) NOT NULL,
    Status INT NOT NULL
);

select *from CanalVenta

insert into CanalVenta(Descripcion,Empresa,Status)
values ('Escritorio', 'HP',1);

EXEC RED_M_CanalVenta @ACCION = 'D', @EMPRESA = 'HP', @idcanalvta = 6


CREATE OR ALTER PROCEDURE [dbo].[RED_M_CanalVenta]
(
    @accion varchar(1),
    @idcanalvta int = NULL,
    @descripcion varchar(30) = NULL,
    @empresa varchar(5) = NULL,
    @status int = 1 
)
AS
BEGIN TRY
    -- Verifica acciones válidas del SP
    IF (@accion != 'C' AND @accion != 'R' AND @accion != 'U' AND @accion != 'D' AND @accion != 'V')
    BEGIN
        RAISERROR('400: Acción no válida.', 16, 1);
        RETURN;
    END    
    
    -- Crea un nuevo registro
    IF (@accion = 'C')
    BEGIN
        IF (@descripcion IS NULL OR @empresa IS NULL)
        BEGIN
            RAISERROR('400: Los campos son requeridos.', 16, 1);
            RETURN;
        END
        ELSE
        BEGIN
            INSERT INTO CanalVenta (descripcion, empresa, status)
            VALUES (@descripcion, @empresa, @status);
            
            DECLARE @newId INT = SCOPE_IDENTITY();
            SELECT idcanalvta, descripcion, empresa, status
            FROM CanalVenta
            WHERE idcanalvta = @newId AND empresa = @empresa;
            
            RETURN;
        END
    END
    
    -- Obtiene todos los campos de la tabla por empresa
    IF (@accion = 'R')
    BEGIN
        IF (@empresa IS NULL)
        BEGIN
            RAISERROR('400: El campo empresa es requerido.', 16, 1);
            RETURN;
        END
        ELSE
        BEGIN
            SELECT idcanalvta, descripcion, empresa, status
            FROM CanalVenta
            WHERE empresa = @empresa;
            
            RETURN;
        END
    END
    
    -- Filtra registros activos por empresa
    IF (@accion = 'V')
    BEGIN
        IF (@empresa IS NULL)
        BEGIN
            RAISERROR('400: El campo empresa es requerido.', 16, 1);
            RETURN;
        END
        ELSE
        BEGIN
            SELECT idcanalvta, descripcion, empresa, status
            FROM CanalVenta
            WHERE empresa = @empresa AND status = 1;
            
            RETURN;
        END
    END
    
    -- Actualiza un registro
    IF (@accion = 'U')
    BEGIN
        IF (@idcanalvta IS NULL OR @empresa IS NULL)
        BEGIN
            RAISERROR('400: Los campos son requeridos.', 16, 1);
            RETURN;
        END
        ELSE
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM CanalVenta WHERE idcanalvta = @idcanalvta AND empresa = @empresa)
            BEGIN
                RAISERROR('404: No se encontró el registro.', 16, 1);
                RETURN;
            END
            ELSE
            BEGIN
                UPDATE CanalVenta
                SET descripcion = ISNULL(@descripcion, descripcion),
                    status = ISNULL(@status, status)
                WHERE idcanalvta = @idcanalvta AND empresa = @empresa;
                
                SELECT idcanalvta, descripcion, empresa, status
                FROM CanalVenta
                WHERE idcanalvta = @idcanalvta AND empresa = @empresa;
                
                RETURN;
            END
        END
    END
    
    -- Desactiva un registro
    IF (@accion = 'D')
    BEGIN
        IF (@idcanalvta IS NULL OR @empresa IS NULL)
        BEGIN
            RAISERROR('400: Los campos son requeridos.', 16, 1);
            RETURN;
        END
        ELSE
        BEGIN
            UPDATE CanalVenta
            SET status = 0
            WHERE idcanalvta = @idcanalvta AND empresa = @empresa;
            
            SELECT idcanalvta, descripcion, empresa, status
            FROM CanalVenta
            WHERE idcanalvta = @idcanalvta AND empresa = @empresa;
		   
		   RETURN;
        END
    END
END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
