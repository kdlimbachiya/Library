USE [Library]
CREATE PROCEDURE [dbo].[UserLogin]
   @Email nvarchar(max),
   @Password nvarchar(15)
AS
  BEGIN
    select * from Users where Email = @Email and Password = @Password
  END