DROP FUNCTION СтоимостьПроданнойТехникиПоКатегории
GO
DROP FUNCTION ПолучитьОплатыРассрочкиПоПокупке
GO

CREATE FUNCTION СтоимостьПроданнойТехникиПоКатегории
(
  @id int
)
RETURNS int
AS
BEGIN
  RETURN (SELECT SUM(Цена) 
            FROM Покупка, Техника, Вид 
            WHERE Покупка.idТехники = Техника.id and Техника.idВида = Вид.id and Вид.idКатегории = @id)
END
GO

CREATE FUNCTION ПолучитьОплатыРассрочкиПоПокупке
(
  @id int
)
RETURNS @tmp TABLE(idПокупки int
				  ,Модель varchar(40)
				  ,Покупатель varchar(80)
				  ,СуммаОплаты money
				  ,ДатаОплаты datetime
				  )
AS
BEGIN
INSERT INTO @tmp
  SELECT idПокупки, Модель, Покупатель, СуммаОплаты, ДатаОплаты
    FROM ПолучитьОплатыРассрочкиПокупки
    WHERE idПокупки = @id
RETURN
END
GO