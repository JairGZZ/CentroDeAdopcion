-- Crear base de datos
CREATE DATABASE CentroDeAdopcion;
GO

USE CentroDeAdopcion;
GO

-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    id_usuario INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    contrasena NVARCHAR(255) NOT NULL,
    direccion NVARCHAR(100),
    telefono NVARCHAR(15),
    rol NVARCHAR(20),
    FotoUrl NVARCHAR(255)
);

-- Crear tabla Mascotas
CREATE TABLE Mascotas (
    id_mascota INT PRIMARY KEY IDENTITY,
    nombre NVARCHAR(100) NOT NULL,
    raza NVARCHAR(100),
    tipo NVARCHAR(20),
    sexo NVARCHAR(10),
    tamaño NVARCHAR(10),
    edad INT,
    estado_salud NVARCHAR(100),
    foto NVARCHAR(255),
    id_propietario INT,
    estado NVARCHAR(20) DEFAULT 'Disponible',
    CONSTRAINT FK_Mascotas_Propietario FOREIGN KEY (id_propietario) REFERENCES Usuarios(id_usuario)
);

-- Crear tabla Adopciones
CREATE TABLE Adopciones (
    id_adopcion INT PRIMARY KEY IDENTITY,
    fecha_solicitud DATETIME DEFAULT GETDATE(),
    id_adoptante INT NOT NULL,
    id_mascota INT NOT NULL,
    CONSTRAINT FK_Adopciones_Adoptante FOREIGN KEY (id_adoptante) REFERENCES Usuarios(id_usuario),
    CONSTRAINT FK_Adopciones_Mascota FOREIGN KEY (id_mascota) REFERENCES Mascotas(id_mascota)
);

-- Índices únicos y constraints adicionales
CREATE UNIQUE INDEX UQ_Usuarios_Email ON Usuarios(email);

-- Finalización
PRINT 'Base de datos y tablas creadas exitosamente';
