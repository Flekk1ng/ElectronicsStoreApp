DROP PROC ПолучитьПокупкиСотрудникаДисконтКарты
GO
DROP PROC restoreПокупка
GO

CREATE PROC ПолучитьПокупкиСотрудникаДисконтКарты 
	@idСотрудника int,          
	@idДисконтКарты int
AS
IF @idСотрудника IS NULL and @idДисконтКарты IS NULL
  SELECT * FROM Покупка
ELSE
  IF @idСотрудника IS NULL
	SELECT * FROM Покупка WHERE idДисконтКарты = @idДисконтКарты
  ELSE
    IF @idДисконтКарты IS NULL
      SELECT * FROM Покупка WHERE idСотрудника = @idСотрудника
    ELSE
      SELECT * FROM Покупка WHERE idСотрудника = @idСотрудника and idДисконтКарты = @idДисконтКарты
GO

CREATE PROC restoreПокупка @lastid int
AS
DECLARE ПокупLog CURSOR FOR
  SELECT typelog, idПокупки, idТехники, idСотрудника, idДисконтКарты, ДатаПокупки
    FROM ПокупкаLog WHERE id >= @lastid ORDER BY id DESC
DECLARE @type char, @idПокупки int, @idТехники int, @idСотрудника int, @idДисконтКарты int, @ДатаПокупки datetime
SET IDENTITY_INSERT Покупка ON
OPEN ПокупLog
FETCH ПокупLog INTO @type, @idПокупки, @idТехники, @idСотрудника, @idДисконтКарты, @ДатаПокупки
WHILE @@FETCH_STATUS = 0
BEGIN
  IF @type = 'I'
    DELETE FROM Покупка WHERE id = @idПокупки
  ELSE
    INSERT INTO Покупка(id, idТехники, idСотрудника, idДисконтКарты, Дата)
      VALUES(@idПокупки, @idТехники, @idСотрудника, @idДисконтКарты, @ДатаПокупки)
  FETCH ПокупLog INTO @type, @idПокупки, @idТехники, @idСотрудника, @idДисконтКарты, @ДатаПокупки
END
SET IDENTITY_INSERT Покупка OFF
CLOSE ПокупLog
DEALLOCATE ПокупLog
DELETE FROM ПокупкаLog WHERE id >= @lastid
GO

DROP PROC ПолучитьОбщееСтоимостьКоличествоПроданнойТехники
GO
CREATE PROC ПолучитьОбщееСтоимостьКоличествоПроданнойТехники
AS
CREATE TABLE #tmp(ОбщееКоличество int
                 ,ОбщаяСтоимость money
                 )
DECLARE @ОбщееКоличество int, @ОбщаяСтоимость money
DECLARE myCursor CURSOR FOR
  SELECT COUNT(*) AS TotalSold, SUM(Цена) AS TotalPrice
    FROM Покупка JOIN Техника ON Покупка.idТехники = Техника.id
OPEN myCursor

FETCH myCursor INTO @ОбщееКоличество, @ОбщаяСтоимость
WHILE @@FETCH_STATUS = 0
BEGIN
  INSERT INTO #tmp
    VALUES (@ОбщееКоличество, @ОбщаяСтоимость)
  FETCH myCursor INTO @ОбщееКоличество, @ОбщаяСтоимость
END
CLOSE myCursor
DEALLOCATE myCursor
SELECT * FROM #tmp
GO