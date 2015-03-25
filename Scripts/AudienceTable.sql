CREATE TABLE [dbo].[Audience](
	[ClientID] [varchar](32) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Secret] [varchar](80) NOT NULL,
	[Issuer] [varchar](150) NULL,
 CONSTRAINT [PK_Audience] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO Audience( ClientID, Name, Secret, Issuer ) VALUES ('bc852fdb311d4adb9d22dfa8226b391a','DemoAudience','MzA1MDI4MThlZjUxNDVmNTgzNDM2YWI4MzQ4Nzg3Njg=','http://authz.com')
