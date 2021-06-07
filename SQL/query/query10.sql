/* Senza attributo ridondante */
SELECT TOP 1 WITH TIES T.CodTipologia, T.Nome, COUNT(T.CodTipologia) AS NumeroLavori
FROM TIPOLOGIE T, LAVORI L
WHERE T.CodTipologia = L.CodTipologia
GROUP BY T.CodTipologia, T.Nome
ORDER BY COUNT(T.CodTipologia) DESC

/* Con attributo ridondante */
SELECT TOP 1 WITH TIES CodTipologia, Nome, NumeroLavori
FROM TIPOLOGIE
GROUP BY CodTipologia, Nome, NumeroLavori
ORDER BY NumeroLavori DESC