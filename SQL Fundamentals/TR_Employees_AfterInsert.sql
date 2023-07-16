CREATE TRIGGER CreateCompanyOnEmployeeInsert
ON Employee
AFTER INSERT
AS
BEGIN
    -- Insert a new Company with an Address based on the newly inserted employee's address
    INSERT INTO Company (Name, AddressId)
    SELECT i.CompanyName, i.AddressId
    FROM inserted i
    INNER JOIN Address a ON i.AddressId = a.Id;

    PRINT 'New Company created based on the inserted Employee information.'
END
