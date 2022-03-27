USE master;
GO

--CRIAR banco de dados
CREATE DATABASE Cadastro;
GO
USE Cadastro;
GO

--Criar tabela PRODUTO 
Create Table USUARIO (
	IDUSUARIO  int primary key IDENTITY(1,1),
	NOME varchar(30),
	SENHA varchar(30),
	REGRA varchar(30),
	CODIGO uniqueidentifier
);
GO