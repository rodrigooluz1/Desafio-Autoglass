
--DROP Table Produto
--DROP Table Fornecedor

-- Criar o banco de dados
CREATE DATABASE db_desafio_autoglass_1;
GO
-- Usar o banco de dados
USE db_desafio_autoglass_1;
GO

-- Criar a tabela Fornecedor
CREATE TABLE Fornecedor (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DescricaoFornecedor NVARCHAR(255) NOT NULL,
    CNPJFornecedor NVARCHAR(14) NOT NULL
);

-- Inserir valores iniciais na tabela Fornecedor
INSERT INTO Fornecedor (DescricaoFornecedor, CNPJFornecedor)
VALUES ('Adidas', '90660845000104'),
       ('Nike', '01410753000138'),
       ('Puma', '77504237000134');

-- Criar a tabela Produto
CREATE TABLE Produto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descricao NVARCHAR(255) NOT NULL,
    Situacao BIT,
    DataFabricacao DATETIME,
    DataValidade DATETIME,
    IdFornecedor INT,
    CONSTRAINT FK_Produto_Fornecedor FOREIGN KEY (IdFornecedor) REFERENCES Fornecedor(Id)
);
