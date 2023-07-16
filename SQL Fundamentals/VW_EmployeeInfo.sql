CREATE VIEW [dbo].[EmployeeInfo]
	AS SELECT 
	E.Id AS EmployeeId,
	ISNULL(E.EmployeeName, P.FirstName + ' ' + P.LastName) AS EmployeeFullName,
	A.ZipCode + '_' + ISNULL(A.State, '') + ', ' + A.City + '-' + A.Street as EmployeeFullAddress,
	C.Name + '(' + ISNULL(e.Position,'') + ')' AS EmployeeCompanyInfo 
	
	FROM Employee AS E
	INNER JOIN Person AS P ON E.PersonId = P.Id
	INNER JOIN dbo.Address AS A ON E.AddressId =A.Id
	INNER JOIN Company AS C ON E.CompanyName = C.Id;
