CREATE TRIGGER CHANGE_QUANTITA_MATERIALI
ON DETTAGLI_MATERIALI 
AFTER INSERT
AS  
BEGIN 
    DECLARE @CodMateriale int;
    DECLARE @QuantitaUsata int;
    DECLARE @Quantita int;
    DECLARE @QuantitaVenduta int;

    SELECT @CodMateriale = CodMateriale FROM inserted;
    SELECT @QuantitaUsata = Quantità FROM inserted;
    SELECT @Quantita = Quantità FROM MATERIALI WHERE CodMateriale = @CodMateriale;
    SELECT @QuantitaVenduta = QuantitàVenduta FROM MATERIALI WHERE CodMateriale = @CodMateriale;

    /* UPDATE QUANTITA' IN MAGAZZINO */
    UPDATE MATERIALI
    SET Quantità = @Quantita - @QuantitaUsata
    WHERE CodMateriale = @CodMateriale

    /* UPDATE QUANTITA' VENDUTA */
    UPDATE MATERIALI
    SET QuantitàVenduta = QuantitàVenduta + @QuantitaUsata
    WHERE CodMateriale = @CodMateriale

END  
GO