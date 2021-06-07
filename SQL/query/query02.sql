/* Recupero il codice del Cliente (si puo' omettere se gia' si conosce) */
SELECT CodCliente
FROM CLIENTI
WHERE <condizioni>;

INSERT INTO PREVENTIVI VALUES
(<CodPreventivo>, <DataPreventivo>, <CodCliente>, 
<CodiceFiscaleElettricista>, <DataInizioElettricista>);

/* Recupero il codice del Materiale (si puo' omettere se gia' si conosce) */
SELECT CodMateriale
FROM MATERIALI
WHERE <condizioni>

INSERT INTO MATERIALI_IN_PREVENTIVI VALUES
(<CodMateriale>, <CodPreventivo>, <Quantita>, <Nota>);