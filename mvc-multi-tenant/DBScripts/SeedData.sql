USE DEMO
GO

INSERT INTO dbo.Tenant(Id, ApiKey, TenantName, IsActive)
VALUES ('5563f9df-470d-43b3-937d-345e7d3c3115', 'a0bfb55d-9fb6-4271-9f72-cfb11a2c754c', 'Tenant A', 1),
('3a56bb10-8421-4e82-a5a5-1ae7527087dc', 'eb375824-b1b4-4eff-9346-ad0e36580b90', 'Tenant B', 1),
('74193104-e6fa-4ebc-a81f-3b2f99895eca', '8393a43b-12d5-4b58-9823-76e4f47ef3f8', 'Tenant C', 1)

INSERT INTO dbo.Customer(Id, TenantId, CustomerName, IsActive)
VALUES ('5018e4b3-e1d3-4ab9-9c1a-33afbd784ee8', '5563f9df-470d-43b3-937d-345e7d3c3115', 'Customer 1', 1),
('1386f84e-4da5-4db6-8626-941c03f9aa60', '3a56bb10-8421-4e82-a5a5-1ae7527087dc', 'Customer 2', 1),
('2a40e268-54ac-4a5e-a04c-4f8f609f2195', '74193104-e6fa-4ebc-a81f-3b2f99895eca', 'Customer 3', 1),
('381db8ad-f195-477e-b717-f69c08794bbb', '5563f9df-470d-43b3-937d-345e7d3c3115', 'Customer 4', 1),
('40e37c59-4d2f-4399-ba5f-674d154abd27', '74193104-e6fa-4ebc-a81f-3b2f99895eca', 'Customer 5', 1)