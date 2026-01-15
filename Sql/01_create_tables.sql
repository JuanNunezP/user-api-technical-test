CREATE TABLE Country (
    CountryId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Department (
    DepartmentId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    CountryId INT NOT NULL,
    CONSTRAINT fk_department_country
        FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
);

CREATE TABLE City (
    CityId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    DepartmentId INT NOT NULL,
    CONSTRAINT fk_city_department
        FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

CREATE TABLE AppUser (
    UserId SERIAL PRIMARY KEY,
    FullName VARCHAR(150) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Address VARCHAR(200) NOT NULL,
    CityId INT NOT NULL,
    CONSTRAINT fk_user_city
        FOREIGN KEY (CityId) REFERENCES City(CityId)
);
