# Progetto di Basi di Dati 2021 - Gestionale Tecnoimpianti

## Tecnologie Utilizzate
Per la realizzazione di questo progetto sono state utilizzate le seguenti tecnologie:

- .NET Framework 4.7.2
- SQL Server
- Linq to SQL

## Procedura di installazione
Per installare correttamente il Dababase bisogna seguire i passi illustrati successivamente. Per il corretto funzionamente, è necessario seguire meticolosamente la procedura, altrimenti potrebbero verificarsi malfunzionamenti. Se non si vuole ricreare il Database, è possibile utilizzare l'applicazione già pronta nella sezione Release.

### Download (via GIT)
- Clonare il codice presente nella repository
- Aprire il progetto in Visual Studio tramine la [Soluzione](GestionaleTecnoimpianti)

### Creazione del Database
- Tasto destro sul nome della soluzione > Aggiungi > Nuovo Elemento
- Selezionare "Database basato su servizi"
- Rinominarlo "TecnoimpiantiDB.mdf

### Definizione Schema Database
- Esplora Server > Tasto destro su TecnoimpiantiDB.mdf > Nuova query
- Eseguire le [queries](SQL/DDL) nel seguente ordine:
  - [Functions](SQL/DDL/Functions):
    - ContaElettricistaDataFineNull_Function.sql
    - DataFineImpiantoDaCodice_Function.sql
    - DataInizioImpiantoDaCodice_Function.sql
  - [Triggers](SQL/DDL/Triggers):
    - UpdateQuantita_Triggers.sql
  - Tecnoimpianti_Create_DDL.sql
 
 ### Avvio Applicazione
 Ora l'applicazione è pronta per essere eseguita tramite il bottone di debug di Visual Studio
