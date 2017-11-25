SET NOCOUNT ON

use Pharmacy

DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position INT,
		@i INT,
		@number int,
		@RowCount int,
		@NameLimitOne INT,
		@medicamentId int,
		@dateOfSale varchar(50),
		@count int,
		@sellingPrice float
		
		
SET @medicamentId=100
SET @dateOfSale=100
SET @number=100
SET @count=100
SET @sellingPrice=100

BEGIN TRAN

SELECT @i=0 FROM dbo.Expence WITH (TABLOCKX) WHERE 1=0

	SET @RowCount=1
	
	WHILE @RowCount<=@number
	BEGIN
		
		SET @dateOfSale=''
		SET @NameLimitOne=5+RAND()*45 
		 
		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @dateOfSale = @dateOfSale + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		
		INSERT INTO  dbo.Expence(medicamentId,dateOfSale,count,sellingPrice) SELECT RAND()*100, @dateOfSale	,RAND()*50, RAND()*50
		

		SET @RowCount +=1
	END
COMMIT TRAN
