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