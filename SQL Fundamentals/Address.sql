﻿CREATE TABLE [dbo].[Address]
(
	Id INT NOT NULL PRIMARY KEY,
	Street NVARCHAR(50) NOT NULL,
	City NVARCHAR(50) NULL,
	State NVARCHAR(100) NULL,
	ZipCode NVARCHAR(50) NULL
)
