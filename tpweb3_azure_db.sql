create database tpweb3_azure;
use tpweb3_azure;

/*
CREATE TABLE TipoPlan(
	id int not null identity(1,1),
	descripcion varchar(100),
	CONSTRAINT pk_tipoPlan PRIMARY KEY(id),
);

CREATE TABLE Plan(
	id int not null identity(1,1),
	idTipoPlan int not null,
	precio decimal (10,2),
	duracion int,
	CONSTRAINT pk_plan PRIMARY KEY(id),
	CONSTRAINT fk_tipoPlan FOREIGN KEY (idTipoPlan) REFERENCES TipoPlan(id)
);
*/


CREATE TABLE [Plan](
	id int not null identity(1,1),
	tipoPlan varchar(100) not null,
	precio decimal (10,2),
	duracion int,
	CONSTRAINT pk_plan PRIMARY KEY(id)
);


CREATE TABLE Usuario(
	id int not null identity(1,1),
	nombre varchar(100) not null,
	apellido varchar(100) not null,
	email varchar(255) not null,
	nombreUsuario varchar(255) not null,
	contrasenia varchar(255) not null,
	idPlan int not null,
	CONSTRAINT pk_usuario PRIMARY KEY(id),
	CONSTRAINT fk_idPlan FOREIGN KEY(idPlan) REFERENCES [Plan](id)
);

CREATE TABLE ListaReproduccion(
	id int not null identity(1,1),
	idUsuario int not null,
	nombre varchar(255) not null,
	fechaCreacion date,
	CONSTRAINT pk_listaReproduccion PRIMARY KEY(id),
	CONSTRAINT fk_idUsuario FOREIGN KEY(idUsuario) REFERENCES Usuario(id)
);

CREATE TABLE Cancion(
	id int not null identity (1,1),
	titulo varchar(255) not null,
	artista varchar(255) not null,
	album varchar(255) not null,
	creador int not null,
	duracion int not null,
	CONSTRAINT pk_cancion PRIMARY KEY (id),
	CONSTRAINT fk_creador FOREIGN KEY (creador) REFERENCES Usuario(id),
);

CREATE TABLE ListaCanciones(
	id int not null identity(1,1),
	idListaReproduccion int not null,
	idCancion int not null,
	CONSTRAINT pk_lista_canciones PRIMARY KEY(id),
	CONSTRAINT fk_listaReproduccion FOREIGN KEY (idListaReproduccion) REFERENCES ListaReproduccion(id),
	CONSTRAINT fk_cancion FOREIGN KEY (idCancion) REFERENCES Cancion(id)
);

