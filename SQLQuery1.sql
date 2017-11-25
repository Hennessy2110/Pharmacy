SET NOCOUNT ON

use Pharmacy

DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position INT,
		@i INT,
		@Number int,
		@RowCount int,
		@NameLimitOne INT,
		@surname varchar(50),
		@name varchar(50),
		@patronimyc varchar(50),
		@contactPhone int
		
		
SET @surname=100
SET @name=100
SET @Number=100
SET @patronimyc=100
SET @contactPhone=100

BEGIN TRAN

SELECT @i=0 FROM dbo.Delivers WITH (TABLOCKX) WHERE 1=0

	SET @RowCount=1
	
	WHILE @RowCount<=@Number
	BEGIN
		
		SET @surname=''
		SET @name=''
		SET @patronimyc=''
		SET @NameLimitOne=5+RAND()*45 
		 
		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @surname = @surname + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @name = @name+ SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @patronimyc = @patronimyc+ SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END
		
		INSERT INTO  dbo.Delivers(surname,name,patronymic,ContactPhone) SELECT @surname ,@name, @patronimyc, RAND()*50
		

		SET @RowCount +=1
	END
COMMIT TRAN
