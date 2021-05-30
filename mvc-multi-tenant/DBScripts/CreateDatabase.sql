CREATE DATABASE DEMO
GO

USE DEMO
GO

CREATE TABLE dbo.Tenant
(
	Id uniqueidentifier NOT NULL,
	ApiKey uniqueidentifier NOT NULL,
	TenantName nvarchar(200) NOT NULL,
	IsActive bit NOT NULL

	CONSTRAINT PK_Tenant PRIMARY KEY CLUSTERED (Id ASC)
)
GO

CREATE TABLE dbo.Customer
(
	Id uniqueidentifier NOT NULL,
	TenantId uniqueidentifier NOT NULL,
	CustomerName nvarchar(50) NOT NULL,
	IsActive bit NOT NULL

	CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT FK_Customer_Tenant FOREIGN KEY (TenantId) REFERENCES dbo.Tenant(Id)
)
GO