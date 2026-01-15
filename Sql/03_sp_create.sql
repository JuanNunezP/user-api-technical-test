CREATE OR REPLACE PROCEDURE sp_create_user(
    p_fullname VARCHAR,
    p_phone VARCHAR,
    p_address VARCHAR,
    p_cityid INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO AppUser(FullName, Phone, Address, CityId)
    VALUES (p_fullname, p_phone, p_address, p_cityid);
END;
$$;




CREATE OR REPLACE FUNCTION sp_get_countries()
RETURNS TABLE (CountryId INT, Name VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT c.CountryId, c.Name
    FROM Country c
    ORDER BY c.Name;
END;
$$;


CREATE OR REPLACE FUNCTION sp_get_departments_by_country(p_countryid INT)
RETURNS TABLE (DepartmentId INT, Name VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT d.DepartmentId, d.Name
    FROM Department d
    WHERE d.CountryId = p_countryid
    ORDER BY d.Name;
END;
$$;


CREATE OR REPLACE FUNCTION sp_get_cities_by_department(p_departmentid INT)
RETURNS TABLE (CityId INT, Name VARCHAR)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT c.CityId, c.Name
    FROM City c
    WHERE c.DepartmentId = p_departmentid
    ORDER BY c.Name;
END;
$$;

