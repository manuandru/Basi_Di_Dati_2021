/* DDL for Tecnoimpianti Database */

CREATE TABLE CLIENTI
(
	CodCliente      INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY, 
    Telefono        VARCHAR(15)                     NOT NULL, 
    CodiceFiscale   CHAR(16)                        NULL /* UNIQUE */, 
    Nome            VARCHAR(32)                     NULL, 
    Cognome         VARCHAR(32)                     NULL, 
    PartitaIVA      VARCHAR(11)                     NULL /* UNIQUE */, 
    Denominazione   VARCHAR(50)                     NULL, 
    CONSTRAINT CodiceFiscaleXORPartitaIVA
	CHECK 
	(
		(CodiceFiscale IS NOT NULL AND NOME IS NOT NULL AND COGNOME IS NOT NULL) 
		OR
		(PartitaIVA IS NOT NULL AND Denominazione IS NOT NULL)
	),
    CONSTRAINT MIN_LEN_CODICE_FISCALE_CLIENTE	CHECK (LEN(CodiceFiscale) = 16),
    CONSTRAINT MIN_LEN_PARTITA_IVA				CHECK (LEN(PartitaIVA) = 11)
);


CREATE TABLE IMPIANTI_ELETTRICI
(
	CodImpianto     INT             IDENTITY(1,1)	NOT NULL PRIMARY KEY, 
    DataInizio      DATE							NOT NULL, 
    DataFine        DATE							NULL, 
    Regione         VARCHAR(32)						NOT NULL, 
    Città           VARCHAR(32)						NOT NULL, 
    Via             VARCHAR(32)						NOT NULL, 
    Numero          INT								NOT NULL, 
    Note            VARCHAR(256)					NULL,
    CodCliente      INT								NOT NULL,
    CONSTRAINT DataInizioMaggioreDataFine CHECK (DataInizio < DataFine),
    CONSTRAINT FK_COD_CLIENTE FOREIGN KEY (CodCliente) REFERENCES CLIENTI(CodCliente)
);


CREATE TABLE TIPOLOGIE
(
    CodTipologia INT           IDENTITY(1,1)	NOT NULL PRIMARY KEY,
    Nome         VARCHAR (32)					NOT NULL,
    Descrizione  VARCHAR (256)					NOT NULL,
    NumeroLavori INT							NOT NULL,
);


CREATE TABLE LAVORI
(
    Data       	 	DATE    NOT NULL, 
    CodImpianto 	INT     NOT NULL, 
    Costo       	MONEY   NOT NULL,
	CodTipologia	INT		NOT NULL,
    CONSTRAINT PK_LAVORI PRIMARY KEY (Data, CodImpianto),
    CONSTRAINT FK_LAVORO_IMPIANTO FOREIGN KEY (CodImpianto) REFERENCES IMPIANTI_ELETTRICI(CodImpianto),
    CONSTRAINT FK_LAVORO_TIPOLOGIA FOREIGN KEY (CodTipologia) REFERENCES TIPOLOGIE(CodTipologia),	
    CONSTRAINT DATALAVORO_MINORE_DATAIMPIANTO CHECK (dbo.DataInizioImpiantoDaCodice(CodImpianto) <= Data),
    CONSTRAINT IMPIANTO_FINITO CHECK (dbo.DataFineImpiantoDaCodice(CodImpianto) >= Data)
);


CREATE TABLE RUOLI
(
	CodRuolo	INT				IDENTITY(1,1)	NOT NULL PRIMARY KEY, 
    Descrizione VARCHAR(256)					NOT NULL
);


CREATE TABLE ELETTRICISTI
(
	CodiceFiscale   CHAR(16)	NOT NULL PRIMARY KEY, 
    Nome            VARCHAR(32) NULL, 
    Cognome         VARCHAR(32) NULL,
	CONSTRAINT MIN_LEN_CODICE_FISCALE_ELETTRICISTA CHECK (LEN(CodiceFiscale) = 16)
);


CREATE TABLE ELETTRICISTI_CON_RUOLI
(
	DataInizio 		DATE 		NOT NULL, 
    DataFine 		DATE 		NULL, 
    CodiceFiscale 	CHAR(16) 	NOT NULL,
	CodRuolo		INT			NOT NULL,
    CONSTRAINT PK_ELETTRICISTA_CON_RUOLO PRIMARY KEY (DataInizio, CodiceFiscale),
	CONSTRAINT FK_ELETTRICISTI_CON_RUOLO_ELETTRICISTI FOREIGN KEY (CodiceFiscale) REFERENCES ELETTRICISTI(CodiceFiscale),
	CONSTRAINT FK_ELETTRICISTI_CON_RUOLO_RUOLO FOREIGN KEY (CodRuolo) REFERENCES RUOLI(CodRuolo),
    CONSTRAINT CHECK_DATE_CORRETTE CHECK (DataInizio <= DataFine),
	CONSTRAINT CHECK_ELETTRICISTA_NO_RUOLO_DUPLICATO CHECK (dbo.ContaElettricistaDataFineNull(CodiceFiscale) <= 1)
);


CREATE TABLE FURGONI
(
	Targa   CHAR(7)		NOT NULL PRIMARY KEY, 
    Marca   VARCHAR(32) NOT NULL, 
    Posti   TINYINT     NOT NULL, 
    KM      INT         NOT NULL
);


CREATE TABLE TURNI_LAVORATIVI
(
	OraInizio					TIME			NOT NULL, 
    OraFine						TIME			NOT NULL,
	Nota						VARCHAR(256)	NULL,
    CodiceFiscale				CHAR(16)		NOT NULL,
	DataInizioElettricista		DATE			NOT NULL,
	DataLavoro					DATE			NOT NULL,
    CodImpianto					INT				NOT NULL,
	Targa						CHAR(7)		NOT NULL,
    CONSTRAINT PK_TURNI_LAVORATIVI PRIMARY KEY (OraInizio, CodiceFiscale, DataInizioElettricista, DataLavoro, CodImpianto),
	CONSTRAINT CHECK_ORE_CORRETTE CHECK (OraInizio < OraFine),
    CONSTRAINT LAVORATORE_NON_HA_RUOLO CHECK (dbo.ContaElettricistaDataFineNull(CodiceFiscale) = 1),
	CONSTRAINT CHECK_ELETTRICISTA_LAVORA_DA_ASSUNTO CHECK (DataLavoro >= DataInizioElettricista),
    CONSTRAINT FK_TURNI_LAVORATIVI_ELETTRICISTA_CF FOREIGN KEY (DataInizioElettricista, CodiceFiscale) REFERENCES ELETTRICISTI_CON_RUOLI(DataInizio, CodiceFiscale),
	CONSTRAINT FK_TURNI_LAVORATIVI_LAVORO FOREIGN KEY (DataLavoro, CodImpianto) REFERENCES LAVORI(Data, CodImpianto),
	CONSTRAINT FK_TURNI_LAVORATIVI_FURGONE FOREIGN KEY (Targa) REFERENCES FURGONI(Targa),
);



CREATE TABLE PREVENTIVI
(
	CodPreventivo			INT				NOT NULL PRIMARY KEY,
	Data					DATE			NOT NULL,
	CodCliente				INT				NOT NULL,
	CodiceFiscale			CHAR(16)		NOT NULL,
	DataInizioElettricista	DATE			NOT NULL,
	CONSTRAINT FK_PREVENTIVO_CLIENTE FOREIGN KEY (CodCliente) REFERENCES CLIENTI(CodCliente),
	CONSTRAINT FK_PREVENTIVO_ELETTRICISTA FOREIGN KEY (DataInizioElettricista, CodiceFiscale) REFERENCES ELETTRICISTI_CON_RUOLI(DataInizio, CodiceFiscale),
	CONSTRAINT CHECK_DATE CHECK (DataInizioElettricista <= Data),
);


CREATE TABLE FORNITORI
(
	CodFornitore			INT				IDENTITY(1,1)	NOT NULL PRIMARY KEY,
	Nome					VARCHAR(32)						NOT NULL,
	Telefono				VARCHAR(15)						NOT NULL,
);


CREATE TABLE MATERIALI
(
	CodMateriale			INT				IDENTITY(1,1)	NOT NULL PRIMARY KEY,
	Nome					VARCHAR(32)						NOT NULL,
	Quantità				INT								NOT NULL,
	Prezzo					MONEY							NOT NULL,
	Descrizione				VARCHAR(256)					NOT NULL,
	QuantitàVenduta			INT								NOT NULL,
	CodFornitore			INT								NOT NULL,
	CONSTRAINT FK_MATERIALE_FORNITORE FOREIGN KEY (CodFornitore) REFERENCES FORNITORI(CodFornitore),
);


CREATE TABLE MATERIALI_IN_PREVENTIVI
(
	CodMateriale			INT				NOT NULL,
	CodPreventivo			INT				NOT NULL,
	Quantità				INT				NOT NULL,
	Nota					VARCHAR(256)	NULL,
	CONSTRAINT PK_MATERIALI_IN_PREVENTIVI PRIMARY KEY (CodMateriale, CodPreventivo),
	CONSTRAINT FK_MATERIALE FOREIGN KEY (CodMateriale) REFERENCES MATERIALI(CodMateriale),
	CONSTRAINT FK_PREVENTIVO FOREIGN KEY (CodPreventivo) REFERENCES PREVENTIVI(CodPreventivo),
);


CREATE TABLE DETTAGLI_MATERIALI
(
	CodMateriale			INT				NOT NULL,
	CodImpianto				INT				NOT NULL,
	DataLavoro				DATE			NOT NULL,
	Quantità				INT				NOT NULL,
	Prezzo					MONEY			NOT NULL,
	Sconto					REAL			NULL,
	Nota					VARCHAR(256)	NULL,
	CONSTRAINT PK_MATERIALI_IN_LAVORI PRIMARY KEY (CodMateriale, CodImpianto, DataLavoro),
	CONSTRAINT FK_MATERIALE_LAVORO FOREIGN KEY (CodMateriale) REFERENCES MATERIALI(CodMateriale),
	CONSTRAINT FK_LAVORO FOREIGN KEY (DataLavoro, CodImpianto) REFERENCES LAVORI(Data, CodImpianto),
);