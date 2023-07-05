CREATE PROCEDURE InsertEmployeeInfo
    @EmployeeName NVARCHAR(100) = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @CompanyName NVARCHAR(20),
    @Position NVARCHAR(30) = NULL,
    @Street NVARCHAR(50),
    @City NVARCHAR(20) = NULL,
    @State NVARCHAR(50) = NULL,
    @ZipCode NVARCHAR(50) = NULL
AS
BEGIN
    -- Check conditions for at least one field with non-null, non-empty, and non-space value
    IF (@EmployeeName IS NULL OR LTRIM(RTRIM(@EmployeeName)) = '')
        AND (@FirstName IS NULL OR LTRIM(RTRIM(@FirstName)) = '')
        AND (@LastName IS NULL OR LTRIM(RTRIM(@LastName)) = '')
    BEGIN
        RAISERROR('At least one field (EmployeeName, FirstName, or LastName) must have a non-null and non-empty value.', 16, 1)
        RETURN
    END

    -- Truncate CompanyName if the length exceeds 20 symbols
    SET @CompanyName = LEFT(@CompanyName, 20)

    -- Insert the employee information into the respective tables
    BEGIN TRY
        -- Insert into Address table
        DECLARE @AddressId INT
        INSERT INTO Address (Street, City, State, ZipCode)
        VALUES (@Street, @City, @State, @ZipCode)
        SET @AddressId = SCOPE_IDENTITY()

        -- Insert into Person table
        INSERT INTO Person (FirstName, LastName)
        VALUES (@FirstName, @LastName)

        DECLARE @PersonId INT = SCOPE_IDENTITY()

        -- Insert into Employee table
        INSERT INTO Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
        VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName)

        -- Insert into Company table
        INSERT INTO Company (Name, AddressId)
        VALUES (@CompanyName, @AddressId)

        PRINT 'Employee information inserted successfully.'
    END TRY
    BEGIN CATCH
        PRINT 'Error occurred while inserting employee information: ' + ERROR_MESSAGE()
    END CATCH
END
