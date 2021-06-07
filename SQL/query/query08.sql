/* Dato che ogni Dettaglio materiale fa riferimento al proprio impianto,
 * non è necessario passare dalla tabella lavori
 */
SELECT SUM(DM.Quantità*DM.Prezzo*(1 - DM.Sconto))
FROM IMPIANTI_ELETTRICI IE	JOIN DETTAGLI_MATERIALI DM ON (IE.CodImpianto = DM.CodImpianto)
WHERE IE.CodImpianto = 1

/* Versione dove si ottengono i costi di tutti gli Impianti */
SELECT DM.CodImpianto, SUM(DM.Quantità*DM.Prezzo*(1 - DM.Sconto))
FROM IMPIANTI_ELETTRICI IE JOIN DETTAGLI_MATERIALI DM ON (IE.CodImpianto = DM.CodImpianto)
GROUP BY DM.CodImpianto;