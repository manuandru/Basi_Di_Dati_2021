CREATE FUNCTION DataInizioImpiantoDaCodice (@codImpianto INT)
RETURNS DATE
AS
BEGIN
	RETURN 
    (
        SELECT DataInizio
        FROM IMPIANTI_ELETTRICI 
        WHERE CodImpianto = @codImpianto
    )
END;

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