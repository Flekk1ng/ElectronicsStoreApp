CREATE TRIGGER TRGПокупкаLog ON Покупка
  AFTER INSERT, UPDATE, DELETE
AS
DECLARE @datelog datetime
SET @datelog = GetDate()
INSERT INTO ПокупкаLog
  SELECT 'D', @datelog, CONVERT(varchar(128), CONTEXT_INFO()), 
     id, idТехники, idСотрудника, idДисконтКарты, Дата
    FROM deleted
INSERT INTO ПокупкаLog
  SELECT 'I', @datelog, CONVERT(varchar(128), CONTEXT_INFO()), 
     id, idТехники, idСотрудника, idДисконтКарты, Дата
    FROM inserted
GO

CREATE TRIGGER TRGДисконтКартаDEL ON ДисконтКарта
  AFTER DELETE 
AS
  IF EXISTS(SELECT * FROM Покупка
              WHERE idДисконтКарты IN (SELECT id FROM deleted))
  BEGIN
    RAISERROR('Нельзя удалить, есть покупкки!', 16, 2)
    ROLLBACK TRAN
    RETURN
  END
GO

CREATE TRIGGER TRGРассрочкаINS ON Рассрочка
  AFTER INSERT
AS
  IF EXISTS(SELECT * FROM inserted
              WHERE idПокупки NOT IN (SELECT id FROM Покупка))
  BEGIN
    RAISERROR('Неверный id покупки!', 16, 2)
    ROLLBACK TRAN
  END
GO

CREATE TRIGGER TRGРассрочкаUP ON Рассрочка
  AFTER UPDATE
AS
  IF UPDATE(idПокупки)
    IF EXISTS(SELECT * FROM inserted
                WHERE idПокупки NOT IN (SELECT id FROM Покупка))
    BEGIN
      RAISERROR('Неверный id покупки!', 16, 2)
      ROLLBACK TRAN
    END
GO