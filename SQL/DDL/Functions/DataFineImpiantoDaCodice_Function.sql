CREATE FUNCTION DataFineImpiantoDaCodice (@codImpianto INT)
RETURNS DATE
AS
BEGIN
	RETURN 
    (
        SELECT DataFine
        FROM IMPIANTI_ELETTRICI 
        WHERE CodImpianto = @codImpianto
    )
END;