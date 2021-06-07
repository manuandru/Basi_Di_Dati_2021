/* Ricavo i dati dell'Elettricista */
SELECT CodiceFiscale, DataInizio
FROM ELETTRICISTI E, ELETTRICISTI_CON_RUOLI ER
WHERE E.CodiceFiscale = ER.CodiceFiscale
AND <condizioni>

/* Ricavo i dati del Furgone */
SELECT Targa
FROM FURGONI
WHERE <condizioni>

INSERT INTO TURNI_LAVORATIVI VALUES
(<OraInizio>, <OraFine>, <Nota>, <CodiceFiscaleElettricista>, 
<DataInizioElettricista>, <DataLavoro>, <CodImpianto>, <Targa>);