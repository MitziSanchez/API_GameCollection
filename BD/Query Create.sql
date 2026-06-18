-- Creación de BD
CREATE DATABASE GameCollection;

-- Creación de Tablas
CREATE TABLE Usuario (
	UsuarioId INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(150) NOT NULL,
	Correo NVARCHAR(255) NOT NULL UNIQUE,
	Contrasena NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Coleccion (
	ColeccionId INT PRIMARY KEY IDENTITY(1,1),
	UsuarioId INT NOT NULL UNIQUE,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE DetalleColeccion (
	DetalleColeccionId INT PRIMARY KEY IDENTITY(1,1),
	ColeccionId INT NOT NULL,
	VideojuegoId INT NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	Calificacion DECIMAL(3,1) NULL CHECK (Calificacion >= 0 AND Calificacion <= 10),
	FechaCalificacion DATETIME NULL,
	EstadoJuegoId INT NOT NULL
);

EXEC sp_addextendedproperty
	@name = N'MS_Description',
	@value = N'Calificación considera valores de 0 a 10',
	@level0type = N'SCHEMA', @level0name = 'dbo',
	@level1type = N'TABLE', @level1name = 'DetalleColeccion',
	@level2type = N'COLUMN', @level2name = 'Calificacion';

CREATE TABLE EstadoJuego (
	EstadoJuegoId INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(15) NOT NULL
);

CREATE TABLE Videojuego (
	VideojuegoId INT PRIMARY KEY IDENTITY(1,1),
	GeneroId INT NOT NULL,
	PlataformaId INT NOT NULL,
	Titulo NVARCHAR(250) NOT NULL
);

CREATE TABLE Genero (
	GeneroId INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(60) NOT NULL
);

CREATE TABLE Plataforma (
	PlataformaId INT PRIMARY KEY IDENTITY(1,1),
	Nombre NVARCHAR(20) NOT NULL
);


-- Creacion de claves foraneas
ALTER TABLE Coleccion
ADD CONSTRAINT FK_Coleccion_Usuario
FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId); 

ALTER TABLE DetalleColeccion
ADD CONSTRAINT FK_DetalleColeccion_Coleccion
FOREIGN KEY (ColeccionId) REFERENCES Coleccion(ColeccionId); 

ALTER TABLE DetalleColeccion
ADD CONSTRAINT FK_DetalleColeccion_EstadoJuego
FOREIGN KEY (EstadoJuegoId) REFERENCES EstadoJuego(EstadoJuegoId);

ALTER TABLE DetalleColeccion
ADD CONSTRAINT FK_DetalleColeccion_Videojuego
FOREIGN KEY (VideojuegoId) REFERENCES Videojuego(VideojuegoId);

ALTER TABLE Videojuego
ADD CONSTRAINT FK_Videojuego_Genero
FOREIGN KEY (GeneroId) REFERENCES Genero(GeneroId);

ALTER TABLE Videojuego
ADD CONSTRAINT FK_Videojuego_Plataforma
FOREIGN KEY (PlataformaId) REFERENCES Plataforma(PlataformaId);