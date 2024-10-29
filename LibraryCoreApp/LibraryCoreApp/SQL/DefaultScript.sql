USE [Library]
GO
Insert into Users(Name,Contact,Gender,Email,Password,IsAdmin) values('Admin','9813450622','Male','admin@gmail.com','123',1);
GO
/****** Object:  StoredProcedure [dbo].[GetAllYourEntities]    Script Date: 2024-10-29 1:52:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [dbo].[UserLogin] 'admin@gmail.com','123'
CREATE PROCEDURE [dbo].[UserLogin]
   @Email nvarchar(max),
   @Password nvarchar(15)
AS
  BEGIN
    select * from Users where Email = @Email and Password = @Password
  END
        
GO
