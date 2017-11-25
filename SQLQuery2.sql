SET NOCOUNT ON

use Pharmacy

DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position INT,
		@i INT,
		@Number int,
		@RowCount int,
		@NameLimitOne INT,
		@medicamentId int,
		@receiptDate varchar(50),
		@count int,
		@deliverId int,
		@purchasePrice float
		
		
SET @Number=100

BEGIN TRAN

SELECT @i=0 FROM dbo.Arrival WITH (TABLOCKX) WHERE 1=0

	SET @RowCount=1
	
	WHILE @RowCount<=@Number
	BEGIN
		
		SET @receiptDate=''
		SET @NameLimitOne=5+RAND()*45 
		 
		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @receiptDate = @receiptDate + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		
		INSERT INTO  dbo.Arrival(medicamentId,receiptDate,count,deliverId,purchasePrice) SELECT RAND()*99,@receiptDate,RAND()*50, RAND()*50,RAND()*50
		

		SET @RowCount +=1
	END
COMMIT TRAN
