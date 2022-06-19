ALTER TABLE [IdentityAuthDB].[dbo].[Prenotazione]
ALTER COLUMN idUser nvarchar(450);

ALTER TABLE [IdentityAuthDB].[dbo].[Prenotazione]
ADD FOREIGN KEY (idUser) REFERENCES [IdentityAuthDB].[dbo].[AspNetUsers](Id);