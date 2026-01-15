-- COUNTRIES
INSERT INTO Country (Name) VALUES ('Colombia');

-- DEPARTMENTS
INSERT INTO Department (Name, CountryId)
VALUES 
('Cundinamarca', 1),
('Antioquia', 1);

-- CITIES
INSERT INTO City (Name, DepartmentId)
VALUES
('Bogotá', 1),
('Soacha', 1),
('Medellín', 2),
('Envigado', 2);
