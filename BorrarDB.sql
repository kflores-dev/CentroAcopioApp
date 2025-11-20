-- Verifica si la base de datos existe y la elimina por completo

USE master;
GO

-- Cambiar el nombre "centro_acopio" por el de tu base de datos
DECLARE @dbName NVARCHAR(100) = N'centro_acopio';
DECLARE @sql NVARCHAR(MAX);

-- Terminar conexiones activas
SET @sql = N'
ALTER DATABASE [' + @dbName + N'] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [' + @dbName + N'];
';
EXEC(@sql);
GO