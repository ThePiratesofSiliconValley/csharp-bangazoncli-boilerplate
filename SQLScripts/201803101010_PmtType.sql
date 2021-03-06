/*
   Saturday, March 10, 201810:12:11 AM
   User: 
   Server: (local)
   Database: BangazonCLI
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.PmtType
	DROP CONSTRAINT FK_PmtType_CustomerId
GO
ALTER TABLE dbo.Customer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Customer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Customer', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_PmtType
	(
	PmtType nvarchar(50) NOT NULL,
	PmtTypeId int NOT NULL IDENTITY (1, 1),
	CustomerId int NOT NULL,
	AcctNumber bigint NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PmtType SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_PmtType ON
GO
IF EXISTS(SELECT * FROM dbo.PmtType)
	 EXEC('INSERT INTO dbo.Tmp_PmtType (PmtType, PmtTypeId, CustomerId, AcctNumber)
		SELECT PmtType, PmtTypeId, CustomerId, CONVERT(bigint, AcctNumber) FROM dbo.PmtType WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_PmtType OFF
GO
DROP TABLE dbo.PmtType
GO
EXECUTE sp_rename N'dbo.Tmp_PmtType', N'PmtType', 'OBJECT' 
GO
ALTER TABLE dbo.PmtType ADD CONSTRAINT
	PK_PmtType PRIMARY KEY CLUSTERED 
	(
	PmtTypeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PmtType ADD CONSTRAINT
	FK_PmtType_CustomerId FOREIGN KEY
	(
	CustomerId
	) REFERENCES dbo.Customer
	(
	CustomerId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PmtType', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PmtType', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PmtType', 'Object', 'CONTROL') as Contr_Per 