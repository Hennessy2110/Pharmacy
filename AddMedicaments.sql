SET NOCOUNT ON

use Pharmacy

DECLARE @Symbol CHAR(52)= 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz',
		@Position INT,
		@positons nvarchar(50),
		@i INT,
		@Number int,
		@NameLimitOne INT,
		@NameLimitTwo INT,
		@RowCount int,
		@name varchar(50),
		@annotation varchar(50),
		@producer varchar(50),
		@units varchar(50),
		@storage varchar(50)
		
SET @name=100
SET @annotation=100
SET @producer=100
SET @Number=100
SET @units=100
SET @storage=100



SELECT @i=0 FROM dbo.Medicaments WITH (TABLOCKX) WHERE 1=0
	
	set @RowCount=1
	
	WHILE @RowCount<=@Number
	BEGIN
	
			
		SET @name=''
		SET @annotation=''
		SET @producer=''
		SET @units=''
		SET @storage=''
		
		SET @NameLimitOne=5+RAND()*45 
		
		SET @i=1
		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*22
			SET @name = @name + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @annotation = @annotation + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @producer = @producer + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @units = @units + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		SET @i=1

		WHILE @i<=@NameLimitOne
		BEGIN
			SET @Position=RAND()*52
			SET @storage = @storage + SUBSTRING(@Symbol, @Position, 1)
			SET @i=@i+1
		END

		INSERT INTO  dbo.Medicaments(name, annotation, producer, units, storage) SELECT @Name, @annotation, @producer,@units,@storage
		SET @RowCount +=1
	END
