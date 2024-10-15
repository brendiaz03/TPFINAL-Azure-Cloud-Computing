create database tpweb3_azure;
use tpweb3_azure;

CREATE TABLE [Plan](
	id int not null identity(1,1),
	tipoPlan varchar(100),
	precio decimal (10,2),
	duracion int,
	CONSTRAINT pk_plan PRIMARY KEY(id)
);


CREATE TABLE Usuario(
	id int not null identity(1,1),
	nombre varchar(100),
	apellido varchar(100),
	email varchar(255),
	nombreUsuario varchar(255),
	contrasenia varchar(255),
	CONSTRAINT pk_usuario PRIMARY KEY(id),
);

CREATE TABLE Pago(
	id int not null identity(1,1),
	idUsuario int,
	idPlan int,
	CONSTRAINT pk_pago PRIMARY KEY(id),
	CONSTRAINT fk_usuario FOREIGN KEY (idUsuario) REFERENCES Usuario(id),
	CONSTRAINT fk_plan FOREIGN KEY (idPlan) REFERENCES [Plan](id),
)

CREATE TABLE ListaReproduccion(
	id int not null identity(1,1),
	idUsuario int,
	nombre varchar(255),
	fechaCreacion date,
	CONSTRAINT pk_listaReproduccion PRIMARY KEY(id),
	CONSTRAINT fk_idUsuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id)
);

CREATE TABLE Cancion(
	id int not null identity (1,1),
	titulo varchar(255),
	artista varchar(255),
	album varchar(255),
	creador int,
	duracion int,
	CONSTRAINT pk_cancion PRIMARY KEY (id),
	CONSTRAINT fk_creador FOREIGN KEY (creador) REFERENCES Usuario(id),
);

CREATE TABLE ListaCanciones(
	id int not null identity(1,1),
	idListaReproduccion int,
	idCancion int,
	CONSTRAINT pk_lista_canciones PRIMARY KEY(id),
	CONSTRAINT fk_listaReproduccion FOREIGN KEY (idListaReproduccion) REFERENCES ListaReproduccion(id),
	CONSTRAINT fk_cancion FOREIGN KEY (idCancion) REFERENCES Cancion(id)
);

