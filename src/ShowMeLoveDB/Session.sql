CREATE TABLE [dbo].[Session]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [title] NVARCHAR(100) NOT NULL, 
    [speakerid] INT NOT NULL, 
    CONSTRAINT [FK_Session_ToSpeaker] FOREIGN KEY ([speakerid]) REFERENCES [Speaker]([id])
)
