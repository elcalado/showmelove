CREATE TABLE [dbo].[Eval]
(
	[id] INT NOT NULL , 
    [capturetime] DATETIME2 NOT NULL, 
    [attendeeid] INT NOT NULL, 
    [sessionid] INT NOT NULL, 
    [anger] DECIMAL NULL, 
    [contempt] DECIMAL NULL, 
    [disgust] DECIMAL NULL, 
    [fear] DECIMAL NULL, 
    [happiness] DECIMAL NULL, 
    [neutral] DECIMAL NULL, 
    [sadness] DECIMAL NULL, 
    [surprise] DECIMAL NULL, 
    [rectangleleft] INT NULL, 
    [rectangleright] INT NULL, 
    [rectangleup] INT NULL, 
    PRIMARY KEY ([capturetime], [id]), 
    CONSTRAINT [FK_Eval_ToAttendee] FOREIGN KEY ([attendeeid]) REFERENCES [Attendee]([id]), 
    CONSTRAINT [FK_Eval_ToSession] FOREIGN KEY ([sessionid]) REFERENCES [Session]([id])
)
