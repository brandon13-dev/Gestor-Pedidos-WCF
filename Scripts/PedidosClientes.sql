create database GestionPedidos;

use GestionPedidos;

create table Clientes(
	ClientesId int identity(1,1) primary key,
	Nombre varchar(100),
	Ciudad varchar(100),
	Estatus bit,
);

create table Pedidos(
	PedidoId int identity(1,1) primary key,
	Fecha smalldatetime,
	Total decimal(16,2),
	Descripcion varchar(500),
	ClientesId int,

	constraint FK_Pedidos_Clientes
		foreign key (ClientesId) references Clientes(ClientesId)
);