/* Recupero il codice del Cliente (si può omettere se già si conosce) */
SELECT CodCliente
FROM CLIENTI
WHERE <condizioni>;

INSERT INTO IMPIANTI_ELETTRICI VALUES
(<CodImpianto>, <DataInizio>, <DataFine>, 
<Regione>, <Città>, <Via>, <Numero>, <Note>, <CodCliente>);