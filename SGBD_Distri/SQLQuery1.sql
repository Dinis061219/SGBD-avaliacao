


CREATE TABLE Produtos(
ProdutoID INT IDENTITY(1,1) PRIMARY KEY,
Codigo VARCHAR(50) UNIQUE,
Nome VARCHAR(150) NOT NULL,
PrecoCompra Decimal(10,2),
PrecoVenda DECIMAL(10,2)Not Null,
Estoque int DEFAULT 0,
EstoqueMinimo int DEFAULT 5
);

CREATE TABLE Clientes(
ClienteID INT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(159) NOT NULL,
Telefone VARCHAR(29),
Email VARCHAR(100)

);

CREATE TABLE Vendadores(
VendedorID INT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(30) NOT NULL
);

CREATE TABLE Pedidos(
PedidoID INT IDENTITY(1,1) PRIMARY KEY,
ClienteID INT,
VendedorID	INT,
Data DATETIME DEFAULT GETDATE(),
Status VARCHAR(50),
FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
FOREIGN KEY (VendedorID) REFERENCES Vendadores(VendedorID)
);

CREATE TABLE PedidoItens(
ItensID INT IDENTITY(1,1) PRIMARY KEY,
PedidoID INT,
ProdutoID INT,
Quantidade INT,
Preco DECIMAL(10,2),
FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID),
FOREIGN KEY (ProdutoID) REFERENCES Produtos(ProdutoID)

);

CREATE TABLE Usuarios(
UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
Nome VARCHAR(50),
Email VARCHAR(100) UNIQUE,
Senha VARCHAR(100),
Perfil VARCHAR(50)
);

INSERT INTO Usuarios VALUES ('Admin','samuelantofelix@gmail.com','2211','Admin');
select * from Usuarios;