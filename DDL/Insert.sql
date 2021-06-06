INSERT INTO CLIENTI VALUES
(1, '3331231231', NULL, NULL, NULL, '11111111111', 'Azienda 1'),
(2, '3333213213', 'CF12345678912345', 'Mario', 'Rossi', NULL, NULL),
(3, '3331112223', 'fc12345678912345', 'Manuel', 'Andruccioli', NULL, NULL),
(4, '+3331112223', 'fc22222678912345', 'Manuel2', 'Andruccioli', NULL, NULL),
(5, '-3331112223', 'fc11111678912345', 'Manuel3', 'Andruccioli', NULL, NULL);


INSERT INTO IMPIANTI_ELETTRICI VALUES
(1, '06/05/2021', '06/10/2021', 'Emilia-Romagna', 'Riccione', 'via', 123, 'Bella vista', 1),
(2, '06/10/2021', null, 'Emilia-Romagna', 'Riccione', 'via', 123, 'Bella vista', 2),
(3, '06/15/2021', '06/17/2021', 'Emilia-Romagna', 'Rimini', 'via', 123, 'Bella vista', 3),
(4, '06/20/2021', '06/30/2021', 'Emilia-Romagna', 'Riccione', 'via', 123, 'Bella vista', 1),
(5, '06/23/2021', null, 'Emilia-Romagna', 'Riccione', 'via', 123, 'Bella vista', 2);


INSERT INTO TIPOLOGIE VALUES
(1, 'Stesa cavi', 'Stendiamo cavi per terra', 0),
(2, 'Cambio Lampadine', 'Sostituzione lampadine', 0),
(3, 'Aggiunta punto luce', 'Aggiungiamo un punto luce', 0);


INSERT INTO LAVORI VALUES
('06/05/2021', 1, 0),
('06/07/2021', 1, 0),
('06/08/2021', 1, 0),
('06/10/2021', 1, 0),
('06/11/2021', 2, 0),
('06/15/2021', 3, 0),
('06/20/2021', 4, 0);


INSERT INTO RUOLI VALUES
(1, 'Socio'),
(2, 'Dipendente'),
(3, 'Stagista');


INSERT INTO ELETTRICISTI VALUES
('CF11122233344455', 'Elettricista1', '111'),
('CF11111111111111', 'Elettricista2', '222'),
('CF22222222222222', 'Elettricista3', '333');


INSERT INTO ELETTRICISTI_CON_RUOLI VALUES
('06/01/2021', '06/02/2021', 'CF11122233344455'),
('06/10/2021', NULL, 'CF11122233344455'),
('06/03/2021', NULL, 'CF11111111111111');


INSERT INTO TURNI_LAVORATIVI VALUES
('9:00', '13:00', 'Lavoro ben svolto', 'CF11122233344455', '06/01/2021', '06/05/2021', 1, 'AA000BB'),
('15:00', '18:00', 'Poteva andare meglio...', 'CF11122233344455', '06/01/2021', '06/05/2021', 1, 'AA000BB'),
('15:00', '18:00', 'Lavoro ben svolto', 'CF11111111111111', '06/03/2021', '06/05/2021', 1, 'AA000BB');


INSERT INTO FURGONI VALUES
('AA000BB', 'FORD', 3, 75000),
('BB000CC', 'FORD', 2, 100000),
('CC000DD', 'FORD', 3, 15000);


INSERT INTO PREVENTIVI VALUES
(1, '06/12/2021', 1, 'CF11122233344455', '06/10/2021'),
(2, '06/15/2021', 2, 'CF11111111111111', '06/03/2021'),
(3, '06/17/2021', 1, 'CF11111111111111', '06/03/2021');


INSERT INTO FORNITORI VALUES
(1, 'FORNITORE1', '12345'),
(2, 'FORNITORE2', '12345'),
(3, 'FORNITORE3', '12345');


INSERT INTO MATERIALI VALUES
(1, 'Lampadina', '10', 10, '220V', 150, 1),
(2, 'Scatola', '20', 20, '25x35', 50, 2),
(3, 'Interruttore', '0', 5, '12V', 250, 1);