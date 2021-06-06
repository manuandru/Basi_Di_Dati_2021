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


CREATE TABLE RUOLI
(
	CodRuolo	INT				NOT NULL PRIMARY KEY, 
    Descrizione VARCHAR(256)	NOT NULL
)


CREATE TABLE ELETTRICISTI
(
	CodiceFiscale   NCHAR(16)   NOT NULL PRIMARY KEY, 
    Nome            VARCHAR(32) NULL, 
    Cognome         VARCHAR(32) NULL
)


CREATE TABLE ELETTRICISTI_CON_RUOLI
(
	DataInizio DATE NOT NULL, 
    DataFine DATE NULL, 
    CodiceFiscale NCHAR(16) NOT NULL, 
    CONSTRAINT PK_ELETTRICISTA_CON_RUOLO PRIMARY KEY (DataInizio, CodiceFiscale),
    CONSTRAINT CHECK_DATE_CORRETTE CHECK (DataInizio <= DataFine),
	CONSTRAINT CHECK_ELETTRICISTA_NO_RUOLO_DUPLICATO CHECK (dbo.ContaElettricistaDataFineNull(CodiceFiscale) <= 1)
)


CREATE TABLE TURNI_LAVORATIVI
(
	OraInizio					TIME			NOT NULL, 
    OraFine						TIME			NOT NULL,
	Nota						VARCHAR(256)	NULL,
    CodiceFiscale				NCHAR(16)		NOT NULL,
	DataInizioElettricista		DATE			NOT NULL,
	DataLavoro					DATE			NOT NULL,
    CodImpianto					INT				NOT NULL,
	Targa						NCHAR(7)		NOT NULL,
    CONSTRAINT PK_TURNI_LAVORATIVI PRIMARY KEY (OraInizio, CodiceFiscale, DataInizioElettricista, DataLavoro, CodImpianto),
	CONSTRAINT CHECK_ORE_CORRETTE CHECK (OraInizio < OraFine),
    CONSTRAINT LAVORATORE_NON_HA_RUOLO CHECK (dbo.ContaElettricistaDataFineNull(CodiceFiscale) = 1),
	CONSTRAINT CHECK_ELETTRICISTA_LAVORA_DA_ASSUNTO CHECK (DataLavoro >= DataInizioElettricista),
    CONSTRAINT FK_TURNI_LAVORATIVI_ELETTRICISTA_CF FOREIGN KEY (DataInizioElettricista, CodiceFiscale) REFERENCES ELETTRICISTI_CON_RUOLI(DataInizio, CodiceFiscale),
	CONSTRAINT FK_TURNI_LAVORATIVI_LAVORO FOREIGN KEY (DataLavoro, CodImpianto) REFERENCES LAVORI(Data, CodImpianto),
	CONSTRAINT FK_TURNI_LAVORATIVI_FURGONE FOREIGN KEY (Targa) REFERENCES FURGONI(Targa),
)


CREATE TABLE FURGONI
(
	Targa   NCHAR(7)    NOT NULL PRIMARY KEY, 
    Marca   VARCHAR(32) NOT NULL, 
    Posti   TINYINT     NOT NULL, 
    KM      INT         NOT NULL
)