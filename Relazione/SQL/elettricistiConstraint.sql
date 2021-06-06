CREATE FUNCTION noIllegalElettricista (@codice INT)
RETURNS INT
AS
BEGIN
	RETURN 
    (
        SELECT count(*) 
        FROM ELETTRICISTI_CON_RUOLI 
        WHERE CodElettricista = @codice
        AND DataFine IS NULL
    )
END;

CHECK(noIllegalElettricista(CodElettricista) <= 1);