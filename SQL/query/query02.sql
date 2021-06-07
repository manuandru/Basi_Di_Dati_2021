/* Recupero il codice del Cliente (si può omettere se già si conosce) */
SELECT CodCliente
FROM CLIENTI
WHERE <condizioni>;

INSERT INTO PREVENTIVI VALUES
(<CodPreventivo>, <DataPreventivo>, 
<CodCliente>, <CodiceFiscaleElettricista>, <DataInizioElettricista>);

/* Recupero il codice del Materiale (si può omettere se già si conosce) */
SELECT CodMateriale
FROM MATERIALI
WHERE <condizioni>

INSERT INTO MATERIALI_IN_PREVENTIVI VALUES
(<CodMateriale>, <CodPreventivo>, <Quantità>, <Nota>);