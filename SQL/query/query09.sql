/* Senza attributo ridondante */
SELECT T.CodTipologia, T.Nome, COUNT(T.CodTipologia) AS NumeroLavori
FROM TIPOLOGIE T, LAVORI L
WHERE T.CodTipologia = L.CodTipologia
GROUP BY T.CodTipologia, T.Nome
HAVING COUNT(T.CodTipologia) IN (
	SELECT TOP 1 WITH TIES COUNT(*)
	FROM LAVORI LL
	GROUP BY LL.CodTipologia
	ORDER BY COUNT(*) DESC
);

/* Con attributo ridondante */
SELECT TOP 1 WITH TIES CodTipologia, Nome, NumeroLavori
FROM TIPOLOGIE
GROUP BY CodTipologia, Nome, NumeroLavori
ORDER BY NumeroLavori DESC