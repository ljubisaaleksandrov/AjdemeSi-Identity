MERGE INTO AspNetRoles AS TARGET USING (VALUES 
('e6e0d6ae-ee09-4b5f-a46a-c91a021ec148', N'User'),
('ccfa943c-ee4d-48df-b9c8-a3941e576dd9', N'Driver'),
('c81fd2e2-756b-4197-9ad0-9061d30e952f', N'Admin')) AS Source (Id, Name)
ON TARGET.Id = Source.Id
--	update matched rows 
WHEN MATCHED THEN 
UPDATE SET Name = Source.Name 
--	insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Name) 
VALUES (Id, Name);