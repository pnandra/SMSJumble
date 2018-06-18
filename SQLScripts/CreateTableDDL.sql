DROP TABLE IF EXISTS dbo.SMSRecord;

Create Table dbo.SMSRecord
(
	ID	INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	vchMessageIn	VARCHAR(250),
	vchMessageOut	VARCHAR(250),
	dttmTimeStamp	DateTime default getdate()
);