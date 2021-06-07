SELECT CodImpianto
FROM IMPIANTI_ELETTRICI
where <condizioni>

SELECT CodTipologia, NumeroLavori
FROM TIPOLOGIE
WHERE <condizioni>

/* Aumento l'attributo ridondante */
UPDATE TIPOLOGIE
SET NumeroLavori = <NuovaQuantita> + 1
WHERE CodTipologia = <CodTipologia>;

INSERT INTO LAVORI VALUES
(<DataLavoro>, <CodImpianto>, <Costo>, <CodTipologia>),

/* Cerco i materiali necessari */
SELECT CodMateriale, QuantitaVenduta
FROM MATERIALI
WHERE <condizioni>

/* Aggiorno l'attributo ridondante */
UPDATE MATERIALI
SET QuantitaVenduta = <QuantitaVenduta> + <QuantitaUtilizzata>
WHERE CodMateriale = <CodMateriale>

INSERT INTO DETTAGLI_MATERIALI VALUES
(<CodMateriale>, <CodImpianto>, <DataLavoro>, 
<QuantitaUtilizzata>, <Prezzo>, <Sconto>, <Nota>);



