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


CREATE FUNCTION ContaElettricistaDataFineNull (@codice NCHAR(16))
RETURNS INT
AS
BEGIN
	RETURN 
    (
        SELECT count(*) 
        FROM ELETTRICISTI_CON_RUOLI 
        WHERE CodiceFiscale = @codice
        AND DataFine IS NULL
    )
END;