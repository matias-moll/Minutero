-- Datos Usuarios
insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Gustavo', 'Gustavo Patrich', 'gpatrich@hexacta.com', 0, 1)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Esteban', 'Esteban Ferrero', 'eferrero@hexacta.com', 0, 2)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Manuel', 'Manuel Funes', 'mfunes@hexacta.com', 0, 3)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('NicoLete', 'Nicolas Lete', 'nlete@hexacta.com', 0, 4)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Charly', 'Carlos Cervio', 'ccervio@hexacta.com', 0, 5)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Paola', 'Paola Moguillansky', 'pmoguillansky@hexacta.com', 0, 6)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Cristian', 'Cristian Crulcich', 'ccrulcich@hexacta.com', 0, 7)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Ignacio', 'Ignacio Ortega', 'iortega@hexacta.com', 0, 8)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Nicolas', 'Nicolas Pisapio', 'npisapio@hexacta.com', 0, 9)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('David', 'David Wasinger', 'dwasinger@hexacta.com', 0, 10)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Victor', 'Victor Catamo', 'vcatamo@hexacta.com', 0, 11)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Axel', 'Axel Magagnini', 'amagagnini@hexacta.com', 0, 16)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Luciana', 'Luciana Garcia', 'lgarciarichter@hexacta.com', 0, 17)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Lorena', 'Lorena Muzzio', 'lmuzzio@hexacta.com', 0, 18)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Dario', 'Dario Spasaro', 'dspasaro@hexacta.com', 0, 19)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Juan', 'Juan Pablo Sanchez', 'jsanchez@hexacta.com', 0, 20)

insert into Usuario (Usuario, NombreAMostrar, Mail, EsEncargado, OrdenEnMinuta)
values	('Facundo', 'Facundo Farias', 'ffarias@hexacta.com', 0, 21)



select * from Usuario


-- Datos Configuracion
insert into Configuracion (Descripcion, Valor)
values ('PathToSharepoint', 'http://teams.corvel.com/sites/sd/mccore/ECM/Shared%20Documents/Claims%20System%20Projects/Project%20Management/Daily%20meeting%20minutes/2016/Daily_Meeting_Minutes_-_July_2016.docx')

select * from Configuracion


-- Datos Suscriptores

insert into Suscriptor (Descripcion, Mail)
values	('Maxim', 'Maxim_Shishin@corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('MattAnderson', 'Matt_Anderson@Corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('Robert', 'Robert_Belcher@Corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('Mustakimov, Timur', 'Timur_Mustakimov@Corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('Andy', 'Andy_Babbitt@corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('MattCappelli', 'Matt_Cappelli@Corvel.com')

insert into Suscriptor (Descripcion, Mail)
values ('Jerry', 'Jerry_Chu@Corvel.com')

insert into Suscriptor (Descripcion, Mail)
values	('CorVelHX', 'CorVel@hexacta.com')

insert into Suscriptor (Descripcion, Mail) 
values ('CareMCSystems', 'DL-IRCA-CareMCSystems@Corvel.com')

insert into Suscriptor (Descripcion, Mail) 
values ('Alexander', 'Alexander_Myachin@Corvel.com')

insert into Suscriptor (Descripcion, Mail) 
values ('Shomali, Charlie', 'Charlie_Shomali@corvel.com')

insert into Suscriptor (Descripcion, Mail) 
values ('Bohm, Marty', 'Marty_Bohm@CORVEL.COM')

insert into Suscriptor (Descripcion, Mail) 
values ('Dudrey, Paul', 'Paul_Dudrey@Corvel.com')

insert into Suscriptor (Descripcion, Mail) 
values ('Hensley, Amanda', 'DLAmanda_Hensley@CORVEL.com')

select * from Suscriptor