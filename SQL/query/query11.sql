SELECT ER.CodiceFiscale, E.Nome, SUM(DATEDIFF(HOUR, T.OraInizio, T.OraFine))
FROM ELETTRICISTI E, ELETTRICISTI_CON_RUOLI ER, TURNI_LAVORATIVI T
WHERE E.CodiceFiscale = ER.CodiceFiscale
AND ER.CodiceFiscale = T.CodiceFiscale
AND ER.DataInizio = T.DataInizioElettricista
AND DATEDIFF(DAY, T.DataLavoro, GETDATE()) < 30 
AND ER.CodiceFiscale = <CodiceFiscale>
GROUP BY ER.CodiceFiscale, E.Nome