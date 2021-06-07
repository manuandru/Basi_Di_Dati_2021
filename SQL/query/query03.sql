/* Recupero il codice del Cliente (si puo' omettere se gia' si conosce) */
SELECT CodCliente
FROM CLIENTI
WHERE <condizioni>;

INSERT INTO IMPIANTI_ELETTRICI VALUES
(<CodImpianto>, <DataInizio>, <DataFine>, 
<Regione>, <Citta>, <Via>, <Numero>, <Note>, <CodCliente>);