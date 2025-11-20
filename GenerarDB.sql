USE
[master];
GO

-- 1. Crear base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'centro_acopio')
BEGIN
    CREATE
DATABASE centro_acopio;
END
GO

-- 2. Crear login si no existe
IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = N'centro_user')
BEGIN
    CREATE
LOGIN centro_user WITH PASSWORD = 'pHlUyjA4jn6QKA';
END
GO

-- 3. Crear usuario en la base de datos y asignar permisos
USE [centro_acopio];
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = N'centro_user')
BEGIN
    CREATE
USER centro_user FOR LOGIN centro_user;
END
GO

ALTER
ROLE db_datareader ADD MEMBER centro_user;
ALTER
ROLE db_datawriter ADD MEMBER centro_user;
GO

-- 4. Crear tablas con esquema dbo
CREATE TABLE dbo.proveedor
(
    id        INT IDENTITY PRIMARY KEY,
    nombre    NVARCHAR(100) NOT NULL,
    contacto  NVARCHAR(100) NOT NULL,
    telefono  VARCHAR(30)         NOT NULL,
    correo    VARCHAR(100),
    direccion NVARCHAR(100) NOT NULL,
    vigencia  CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.donacion
(
    id            INT IDENTITY PRIMARY KEY,
    proveedor_id  INT                 NOT NULL FOREIGN KEY REFERENCES dbo.proveedor(id),
    fecha         DATETIME2           NOT NULL,
    observaciones NVARCHAR(MAX),
    vigencia      CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.solicitud
(
    id            INT IDENTITY PRIMARY KEY,
    nombre        NVARCHAR(100) NOT NULL,
    contacto      NVARCHAR(100) NOT NULL,
    telefono      VARCHAR(30)         NOT NULL,
    correo        VARCHAR(100),
    direccion     NVARCHAR(100) NOT NULL,
    fecha         DATETIME2           NOT NULL,
    estado        VARCHAR(20)         NOT NULL,
    prioridad     VARCHAR(20)         NOT NULL,
    observaciones NVARCHAR(MAX),
    vigencia      CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.tipo_recurso
(
    id       INT IDENTITY PRIMARY KEY,
    nombre   NVARCHAR(100) NOT NULL,
    vigencia CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.recurso
(
    id            INT IDENTITY PRIMARY KEY,
    nombre        NVARCHAR(100) NOT NULL,
    tipo_id       INT                 NOT NULL FOREIGN KEY REFERENCES dbo.tipo_recurso(id),
    unidad_medida VARCHAR(20)         NOT NULL,
    vigencia      CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.detalle_solicitud
(
    id                  INT IDENTITY PRIMARY KEY,
    solicitud_id        INT                 NOT NULL FOREIGN KEY REFERENCES dbo.solicitud(id),
    recurso_id          INT                 NOT NULL FOREIGN KEY REFERENCES dbo.recurso(id),
    cantidad_solicitada DECIMAL(10, 2)      NOT NULL,
    cantidad_entregada  DECIMAL(10, 2)      NOT NULL,
    vigencia            CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.ubicacion
(
    id        INT IDENTITY PRIMARY KEY,
    nombre    NVARCHAR(100) NOT NULL,
    direccion NVARCHAR(100) NOT NULL,
    vigencia  CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.detalle_donacion
(
    id              INT IDENTITY PRIMARY KEY,
    donacion_id     INT                 NOT NULL FOREIGN KEY REFERENCES dbo.donacion(id),
    recurso_id      INT                 NOT NULL FOREIGN KEY REFERENCES dbo.recurso(id),
    cantidad_donada DECIMAL(10, 2)      NOT NULL,
    ubicacion_id    INT                 NOT NULL FOREIGN KEY REFERENCES dbo.ubicacion(id),
    vigencia        CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.recurso_ubicacion
(
    id           INT IDENTITY PRIMARY KEY,
    recurso_id   INT                 NOT NULL FOREIGN KEY REFERENCES dbo.recurso(id),
    ubicacion_id INT                 NOT NULL FOREIGN KEY REFERENCES dbo.ubicacion(id),
    cantidad     DECIMAL(10, 2)      NOT NULL,
    vigencia     CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.usuario
(
    id            INT IDENTITY PRIMARY KEY,
    nombre        NVARCHAR(100) NOT NULL,
    username      VARCHAR(50)         NOT NULL,
    password_hash VARCHAR(255)        NOT NULL,
    rol           VARCHAR(20)         NOT NULL,
    email         VARCHAR(100),
    vigencia      CHAR(1) DEFAULT 'A' NOT NULL
);
GO

CREATE TABLE dbo.historial
(
    id          INT IDENTITY PRIMARY KEY,
    usuario_id  INT                 NOT NULL FOREIGN KEY REFERENCES dbo.usuario(id),
    accion      VARCHAR(50)         NOT NULL,
    entidad     VARCHAR(50)         NOT NULL,
    entidad_id  INT                 NOT NULL,
    fecha_hora  DATETIME2           NOT NULL,
    descripcion NVARCHAR(255) NOT NULL,
    vigencia    CHAR(1) DEFAULT 'A' NOT NULL
);
GO

-- 5. Insertar registros a cada tabla

-- ================================
-- TABLA: proveedor (10 registros)
-- ================================
INSERT INTO proveedor (nombre, contacto, telefono, correo, direccion)
VALUES
('Fundación Vida Plena', 'Laura Pérez', '7010-1234', 'contacto@vidaplena.org', 'San Salvador, Col. Escalón'),
('Alimentos S.A.', 'Carlos Gómez', '7890-5678', 'cgomez@alimentos.com', 'Santa Tecla, La Libertad'),
('Donaciones Solidarias', 'María López', '7856-2233', 'info@solidarias.org', 'Soyapango, San Salvador'),
('ConstruSal', 'Jorge Ramos', '7788-9900', 'ventas@construsal.com', 'San Miguel, Centro'),
('Agua Pura El Manantial', 'Ana Torres', '7033-2200', 'ana@elmanantial.com', 'Santa Ana, Col. San Lorenzo'),
('Textiles El Mundo', 'Ricardo Díaz', '7456-9933', 'r.diaz@elmundo.com', 'San Salvador, Apopa'),
('Farmacia Esperanza', 'Lucía Hernández', '7622-1144', 'lucia@esperanza.com', 'Sonsonate, Centro'),
('Donatón SV', 'Ernesto Castillo', '7800-5566', 'info@donatonsv.org', 'San Vicente, Calle Central'),
('AgroCampo', 'Raúl Ayala', '7722-9988', 'r.ayala@agrocampo.com', 'Chalatenango, Barrio El Calvario'),
('Maderas López', 'Oscar López', '7555-1122', 'oscar@maderaslopez.com', 'Usulután, Centro');
GO

-- ================================
-- TABLA: usuario (10 registros)
-- ================================
INSERT INTO usuario (nombre, username, password_hash, rol, email)
VALUES
('Administrador General', 'sa', 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=', 'ADMIN', 'usuario.admin@ejemplo.com'),
('Usuario Operador', 'oper', 'FyUWXJoLNpij0BAW4NggUVWCC41/IYNcpkwPgcco2IA=', 'OPERADOR', 'usuario.operador@ejemplo.com');
GO

-- ================================
-- TABLA: ubicacion (10 registros)
-- ================================
INSERT INTO ubicacion (nombre, direccion)
VALUES
('Bodega Central', 'San Salvador, Col. Médica'),
('Bodega Occidente', 'Santa Ana, Calle Principal'),
('Bodega Oriente', 'San Miguel, Av. Roosevelt'),
('Centro Distribución Norte', 'Chalatenango, Barrio El Calvario'),
('Centro Distribución Sur', 'Usulután, Col. El Centro'),
('Punto de Apoyo Soyapango', 'Soyapango, Alameda Juan Pablo II'),
('Punto de Apoyo Santa Tecla', 'Santa Tecla, Blvd. Merliot'),
('Punto de Apoyo Sonsonate', 'Sonsonate, Calle Real'),
('Almacén Temporal A', 'San Vicente, Col. Guadalupe'),
('Almacén Temporal B', 'La Unión, Centro');
GO

-- ================================
-- TABLA: tipo_recurso (10 registros)
-- ================================
INSERT INTO tipo_recurso (nombre)
VALUES
('Alimentos'),
('Agua'),
('Medicamentos'),
('Ropa'),
('Material de Construcción'),
('Artículos de Limpieza'),
('Equipos Médicos'),
('Material Escolar'),
('Productos de Higiene'),
('Combustible');
GO

-- ================================
-- TABLA: recurso (20 registros)
-- ================================
INSERT INTO recurso (nombre, tipo_id, unidad_medida)
VALUES
('Arroz', 1, 'kg'),
('Frijoles', 1, 'kg'),
('Botellas de Agua', 2, 'lt'),
('Antibióticos', 3, 'caja'),
('Camisetas', 4, 'unidad'),
('Bloques de Concreto', 5, 'unidad'),
('Jabón', 6, 'unidad'),
('Guantes Médicos', 7, 'caja'),
('Cuadernos', 8, 'unidad'),
('Toallas Sanitarias', 9, 'paquete'),
('Combustible Diesel', 10, 'galón'),
('Aceite', 1, 'lt'),
('Azúcar', 1, 'kg'),
('Paracetamol', 3, 'caja'),
('Pantalones', 4, 'unidad'),
('Cepillos Dentales', 9, 'unidad'),
('Cloro', 6, 'lt'),
('Madera', 5, 'pieza'),
('Mascarillas', 7, 'caja'),
('Lapiceros', 8, 'unidad');
GO

-- ================================
-- TABLA: donacion (20 registros)
-- ================================
INSERT INTO donacion (proveedor_id, fecha, observaciones)
VALUES
(1, '2025-10-01', 'Entrega de alimentos básicos'),
(2, '2025-10-03', 'Donación de productos variados'),
(3, '2025-10-05', 'Medicinas y productos de limpieza'),
(4, '2025-10-06', 'Material de construcción'),
(5, '2025-10-08', 'Agua embotellada'),
(6, '2025-10-09', 'Ropa nueva'),
(7, '2025-10-10', 'Medicamentos varios'),
(8, '2025-10-11', 'Artículos de higiene personal'),
(9, '2025-10-12', 'Combustible para transporte'),
(10, '2025-10-13', 'Bloques y madera para reconstrucción'),
(1, '2025-10-14', 'Segunda entrega de alimentos'),
(2, '2025-10-15', 'Productos enlatados'),
(3, '2025-10-16', 'Medicamentos adicionales'),
(4, '2025-10-17', 'Cemento y herramientas'),
(5, '2025-10-18', 'Agua purificada extra'),
(6, '2025-10-19', 'Ropa para niños'),
(7, '2025-10-20', 'Farmacéuticos donados'),
(8, '2025-10-21', 'Artículos femeninos'),
(9, '2025-10-22', 'Gasolina y diésel'),
(10, '2025-10-23', 'Material estructural');
GO

-- ================================
-- TABLA: solicitud (20 registros)
-- ================================
INSERT INTO solicitud (nombre, contacto, telefono, correo, direccion, fecha, estado, prioridad, observaciones)
VALUES
('Cruz Roja SV', 'Pedro López', '7100-2233', 'pedro@cruzroja.org', 'San Salvador, Centro', '2025-10-02', 'Pendiente', 'Alta', 'Solicita alimentos y agua'),
('Iglesia La Esperanza', 'Juan Torres', '7155-3322', 'info@esperanza.org', 'Soyapango', '2025-10-03', 'Completada', 'Media', 'Apoyo a familias desplazadas'),
('Comité Comunal Norte', 'Rosa Pérez', '7899-9988', 'rosa@norte.org', 'Chalatenango', '2025-10-04', 'Pendiente', 'Alta', 'Requieren ropa y agua'),
('Escuela El Progreso', 'Marta Ramírez', '7000-1111', 'marta@escuelaprogreso.edu', 'Santa Ana', '2025-10-05', 'En proceso', 'Alta', 'Útiles escolares y agua'),
('Hospital Central', 'Carlos Gómez', '7200-4545', 'cgomez@hospitalcentral.org', 'San Salvador', '2025-10-06', 'Pendiente', 'Alta', 'Medicinas y guantes'),
('Asilo Santa María', 'Elena Díaz', '7333-5566', 'elena@asilo.org', 'San Miguel', '2025-10-07', 'Pendiente', 'Media', 'Ropa y artículos de limpieza'),
('Escuela La Paz', 'Julia Campos', '7444-8877', 'julia@lapaz.edu', 'Sonsonate', '2025-10-08', 'Completada', 'Media', 'Material escolar'),
('Iglesia Fe y Vida', 'Andrés Molina', '7555-9988', 'andres@fevida.org', 'Usulután', '2025-10-09', 'Pendiente', 'Baja', 'Alimentos'),
('Comunidad San José', 'Paola Herrera', '7666-7788', 'paola@sanjo.org', 'La Unión', '2025-10-10', 'Pendiente', 'Alta', 'Bloques y cemento'),
('Escuela Los Pinos', 'Mario Ramos', '7777-8899', 'mario@lospinos.edu', 'Santa Tecla', '2025-10-11', 'En proceso', 'Media', 'Cuadernos y lápices'),
-- agrega 10 más similares
('Hogar de Ancianos', 'Lucía Torres', '7111-2233', 'lucia@hogarancianos.org', 'Santa Ana', '2025-10-12', 'Pendiente', 'Alta', 'Ropa y alimentos'),
('Hospital Regional', 'Fernando Cruz', '7999-8877', 'fernando@hospitalregional.org', 'San Miguel', '2025-10-13', 'En proceso', 'Alta', 'Medicamentos'),
('Escuela Nueva Luz', 'Sandra López', '7888-4411', 'sandra@escuelaluz.edu', 'Chalatenango', '2025-10-14', 'Pendiente', 'Media', 'Útiles escolares'),
('Iglesia Amor y Fe', 'Rafael Castro', '7222-6677', 'rafael@amorfe.org', 'Usulután', '2025-10-15', 'Pendiente', 'Baja', 'Ropa y alimentos'),
('Cruz Verde SV', 'Erika Ramírez', '7990-1100', 'erika@cruzverde.org', 'San Salvador', '2025-10-16', 'Pendiente', 'Alta', 'Medicinas y guantes'),
('Escuela Santa Rosa', 'Beatriz Mejía', '7444-3344', 'beatriz@santarosa.edu', 'Santa Tecla', '2025-10-17', 'Pendiente', 'Media', 'Útiles escolares'),
('Comité Comunal Sur', 'Diego López', '7333-6655', 'diego@comsur.org', 'San Vicente', '2025-10-18', 'Pendiente', 'Alta', 'Alimentos'),
('Asilo Buen Pastor', 'Rosa García', '7555-8877', 'rosa@buenpastor.org', 'Santa Ana', '2025-10-19', 'Completada', 'Media', 'Ropa'),
('Hospital La Unión', 'Juan Ramos', '7666-2233', 'juan@hospitalunion.org', 'La Unión', '2025-10-20', 'Pendiente', 'Alta', 'Medicamentos'),
('Escuela El Porvenir', 'Claudia Castillo', '7777-9900', 'claudia@porvenir.edu', 'San Vicente', '2025-10-21', 'Pendiente', 'Media', 'Útiles escolares');
GO

-- =========================================
-- TABLA: detalle_solicitud (20 registros)
-- =========================================
INSERT INTO detalle_solicitud (solicitud_id, recurso_id, cantidad_solicitada, cantidad_entregada)
VALUES
(1, 1, 200.00, 150.00),
(1, 3, 300.00, 200.00),
(2, 5, 100.00, 100.00),
(3, 4, 20.00, 10.00),
(3, 2, 150.00, 100.00),
(4, 9, 200.00, 150.00),
(5, 8, 50.00, 40.00),
(5, 14, 60.00, 60.00),
(6, 6, 120.00, 80.00),
(7, 9, 200.00, 200.00),
(8, 1, 250.00, 200.00),
(9, 18, 50.00, 50.00),
(10, 19, 60.00, 60.00),
(11, 4, 40.00, 30.00),
(12, 7, 70.00, 70.00),
(13, 8, 100.00, 90.00),
(14, 10, 80.00, 70.00),
(15, 5, 120.00, 110.00),
(16, 11, 300.00, 250.00),
(17, 13, 150.00, 140.00);
GO

-- =========================================
-- TABLA: detalle_donacion (20 registros)
-- =========================================
INSERT INTO detalle_donacion (donacion_id, recurso_id, cantidad_donada, ubicacion_id)
VALUES
(1, 1, 500.00, 1),
(1, 2, 400.00, 1),
(2, 3, 600.00, 2),
(2, 12, 300.00, 2),
(3, 4, 200.00, 3),
(3, 7, 150.00, 3),
(4, 6, 800.00, 4),
(4, 18, 400.00, 4),
(5, 3, 1000.00, 5),
(6, 5, 200.00, 6),
(7, 14, 300.00, 3),
(8, 9, 250.00, 7),
(9, 11, 150.00, 8),
(10, 18, 500.00, 5),
(11, 1, 400.00, 2),
(12, 2, 350.00, 2),
(13, 4, 220.00, 3),
(14, 6, 700.00, 4),
(15, 3, 900.00, 5),
(16, 5, 180.00, 6);
GO

-- =========================================
-- TABLA: recurso_ubicacion (20 registros)
-- =========================================
INSERT INTO recurso_ubicacion (recurso_id, ubicacion_id, cantidad)
VALUES
(1, 1, 300.00),
(2, 1, 250.00),
(3, 2, 400.00),
(4, 3, 150.00),
(5, 4, 180.00),
(6, 4, 500.00),
(7, 3, 200.00),
(8, 3, 180.00),
(9, 7, 220.00),
(10, 8, 100.00),
(11, 8, 90.00),
(12, 2, 250.00),
(13, 2, 300.00),
(14, 3, 180.00),
(15, 6, 120.00),
(16, 7, 160.00),
(17, 5, 200.00),
(18, 4, 300.00),
(19, 3, 210.00),
(20, 9, 180.00);
GO




