/* DDL for Tecnoimpianti Database */

CREATE TABLE CLIENTI
(
	CodCliente      INT             NOT NULL PRIMARY KEY, 
    Telefono        VARCHAR(15)     NOT NULL, 
    CodiceFiscale   NCHAR(16)       NULL /* UNIQUE */, 
    Nome            VARCHAR(32)     NULL, 
    Cognome         VARCHAR(32)     NULL, 
    PartitaIVA      NCHAR(11)       NULL /* UNIQUE */, 
    Denominazione   VARCHAR(50)     NULL, 
    CONSTRAINT CodiceFiscaleXORPartitaIVA
	CHECK 
	(
		(CodiceFiscale IS NOT NULL AND NOME IS NOT NULL AND COGNOME IS NOT NULL) 
		OR
		(PartitaIVA IS NOT NULL AND Denominazione IS NOT NULL)
	)
);


CREATE TABLE IMPIANTI_ELETTRICI
(
	CodImpianto     INT             NOT NULL PRIMARY KEY, 
    DataInizio      DATE            NOT NULL, 
    DataFine        DATE            NULL, 
    Regione         VARCHAR(32)     NOT NULL, 
    Città           VARCHAR(32)     NOT NULL, 
    Via             VARCHAR(32)     NOT NULL, 
    Numero          INT             NOT NULL, 
    Note            VARCHAR(256)    NULL,
    CodCliente      INT             NOT NULL,
    CONSTRAINT DataInizioMaggioreDataFine CHECK (DataInizio < DataFine),
    CONSTRAINT FK_COD_CLIENTE FOREIGN KEY (CodCliente) REFERENCES CLIENTI(CodCliente)
);


CREATE TABLE TIPOLOGIE
(
    CodTipologia INT           NOT NULL,
    Nome         VARCHAR (32)  NOT NULL,
    Descrizione  VARCHAR (256) NOT NULL,
    NumeroLavori INT           NOT NULL,
);


CREATE TABLE LAVORI
(
    Data        DATE    NOT NULL, 
    CodImpianto INT     NOT NULL, 
    Costo       MONEY   NOT NULL,
    CONSTRAINT PK_LAVORI PRIMARY KEY (Data, CodImpianto),
    CONSTRAINT FK_LAVORO_IMPIANTO FOREIGN KEY (CodImpianto) REFERENCES IMPIANTI_ELETTRICI(CodImpianto),
    CONSTRAINT DATALAVORO_MINORE_DATAIMPIANTO CHECK (dbo.DataInizioImpiantoDaCodice(CodImpianto) <= Data),
    CONSTRAINT IMPIANTO_FINITO CHECK (dbo.DataFineImpiantoDaCodice(CodImpianto) >= Data)
);