/* Dato che ogni Dettaglio materiale fa riferimento al proprio impianto,
 * non e' necessario passare dalla tabella lavori.
 * Il join e' necessario se non si conosce il CodImpianto
 */
SELECT SUM(DM.Quantita*DM.Prezzo*(1 - DM.Sconto))
FROM IMPIANTI_ELETTRICI IE JOIN DETTAGLI_MATERIALI DM ON (IE.CodImpianto = DM.CodImpianto)
WHERE IE.CodCliente = <CodCliente>

/* Versione dove si ottengono i costi di tutti gli Impianti */
SELECT DM.CodImpianto, SUM(DM.Quantita*DM.Prezzo*(1 - DM.Sconto))
FROM DETTAGLI_MATERIALI DM
GROUP BY DM.CodImpianto;

/* Versione utilizzando l'attributo ridondante */
SELECT SUM(Costo)
FROM LAVORI
WHERE CodImpianto = <CodImpianto>