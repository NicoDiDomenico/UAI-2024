use uai2022
GO
--VER ESPACIO DE UNA BD
sp_spaceused
GO
--VER ESPACION DE UN OBJETO DE LA BD
sp_spaceused @objname='productos'

use uai2022
select * from localidades

--1ERO HACEMOS UN BACKUP EN MODO COMPLETO
backup database [uai2022]
to disk =N'c:\test\dbuai2022copia.bak'

--2DO INSERTAMOS UN REGISTRO EN LA TABLA LOCALIDADES
insert into localidades
values(3333,'Maciel')

--3ERO HACEMOS UN BACKUP DEL LOG
backup log [uai2022]
to disk =N'c:\test\dbuai2022copia.trn'

--4TO BORRAR LA BD
use master
drop database uai2022

--5TO RECUPERAMOS LA BD
restore database [uai2022]
from disk =N'c:\test\dbuai2022copia.bak'
with norecovery

--6TO RECUPERO EL LOG
restore log [uai2022]
from disk =N'c:\test\dbuai2022copia.trn'






