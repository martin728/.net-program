INSERT INTO Person (Id, FirstName, LastName) VALUES
(1, 'John', 'Doe'),
(2, 'Jane', 'Smith'),
(3, 'Michael', 'Johnson');

INSERT INTO Address (Id, Street, City, State, ZipCode) VALUES
(1, '123 Main St', 'New York', 'NY', '10001'),
(2, '456 Elm St', 'Los Angeles', 'CA', '90001'),
(3, '789 Oak St', 'Chicago', 'IL', '60601');

INSERT INTO Employee (Id, AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES
(1, 1, 1, 'ABC Company', 'Manager', NULL),
(2, 2, 2, 'XYZ Corporation', 'Developer', 'Jane Smith'),
(3, 3, 3, '123 Industries', NULL, NULL);

INSERT INTO Company (Id, Name, AddressId) VALUES
(1, 'ABC Company', 1),
(2, 'XYZ Corporation', 2),
(3, '123 Industries', 3);
