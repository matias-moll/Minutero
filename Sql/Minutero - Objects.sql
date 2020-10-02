------------------------------------------------------------------------------------------------ 
-- FUNCTIONS
------------------------------------------------------------------------------------------------

if exists (select * from sysobjects where id = object_id('esUsuarioAutorizado'))
begin  
	drop function esUsuarioAutorizado  
end  
go

create function esUsuarioAutorizado(@usuario char(20))
returns int
as
begin
	if (@usuario in 
					(
						select Usuario 
						from Usuario
					)
		)
		return 1;
		
	return 0;
end

go

if exists (select * from sysobjects where id = object_id('getEncargado'))
begin  
	drop function getEncargado  
end  
go

create function getEncargado()
returns char(50)
begin
	return	(select	top 1 u.NombreAMostrar 
			 from	Usuario u
			 where	u.EsEncargado = 1)
end

go


if exists (select * from sysobjects where id = object_id('getCodigoEncargado'))
begin  
	drop function getCodigoEncargado  
end  
go

create function getCodigoEncargado()
returns char(50)
begin
	return	(select	top 1 u.Usuario 
			 from	Usuario u
			 where	u.EsEncargado = 1)
end

go




------------------------------------------------------------------------------------------------ 
-- SPROCS
------------------------------------------------------------------------------------------------

if exists (select * from sysobjects where id = object_id('grabarItemMinuta'))
begin 
	drop procedure grabarItemMinuta 
end 
go

create procedure grabarItemMinuta
(
	@idMinuta int,
	@descripcion varchar(100),
	@tipo char(15)
)
as
begin
	insert into ItemMinuta (IdMinuta, Descripcion, Tipo)
	values (@idMinuta, @descripcion, @tipo)
end
go

if exists (select * from sysobjects where id = object_id('grabarMinuta'))
begin 
	drop procedure grabarMinuta 
end 
go

create procedure grabarMinuta
(
	@usuario char(20),
	@idMinuta int output
)
as
begin

	declare @CurrentDate Date
	select @CurrentDate = GETDATE()

	-- Borramos todos los items que podrian existir (carga dos veces la minuta en el dia)
	delete	from ItemMinuta
	where	IdMinuta = (
						select	Id 
						from	Minuta 
						where	Fecha = @CurrentDate
						and		Usuario = @usuario
						)

	-- Borramos la minuta que podria existir
	delete	from Minuta
	where	Usuario = @usuario
	and		Fecha = @CurrentDate

	-- Grabamos la nueva minuta
	insert into 
	Minuta (Fecha, Usuario)
	values (@CurrentDate, @usuario)

	select @idMinuta = SCOPE_IDENTITY()
end
go


if exists (select * from sysobjects where id = object_id('getUsuarioFromCodigo'))
begin 
	drop procedure getUsuarioFromCodigo 
end 
go

create procedure getUsuarioFromCodigo
(
	@codigoUsuario varchar(20)
)
as
begin
	select	us.Usuario, us.NombreAMostrar, us.Mail, us.EsEncargado
	from	Usuario us
	where	us.Usuario = @codigoUsuario
end
go


if exists (select * from sysobjects where id = object_id('getItemsMinutaDeAyerPorUsuario'))
begin 
	drop procedure getItemsMinutaDeAyerPorUsuario 
end 
go

create procedure getItemsMinutaDeAyerPorUsuario
(
	@usuario char(20)
)
as
begin
	select * from 
	(
		select	us.Usuario, us.NombreAMostrar, im.Tipo, im.Descripcion, im.IdMinuta, MAX(im.IdMinuta) over (partition by us.NombreAMostrar) MaxIdMinuta
		from	ItemMinuta im
		join	Minuta m 
		on		m.Id = im.IdMinuta
		join	Usuario us
		on		us.Usuario = m.Usuario
		where	m.Usuario = @usuario
	) ultimaMinuta
	where IdMinuta = MaxIdMinuta
end
go


if exists (select * from sysobjects where id = object_id('getValorParametro'))
begin 
	drop procedure getValorParametro 
end 
go

create procedure getValorParametro
(
	@descripcionParametro char(50)
)
as
begin
	select	c.Valor 
	from	Configuracion c
	where	Descripcion = @descripcionParametro
end
go


if exists (select * from sysobjects where id = object_id('getLastValidItemsMinutaPorUsuario'))
begin 
	drop procedure getLastValidItemsMinutaPorUsuario 
end 
go

create procedure getLastValidItemsMinutaPorUsuario
(
	@usuario char(20)
)
as
begin
	select * from 
	(
		select	us.Usuario, us.NombreAMostrar, im.Tipo, im.Descripcion, im.IdMinuta, MAX(im.IdMinuta) over (partition by us.NombreAMostrar) MaxIdMinuta
		from	ItemMinuta im
		join	Minuta m 
		on		m.Id = im.IdMinuta
		join	Usuario us
		on		us.Usuario = m.Usuario
		where	m.Usuario = @usuario
		and		descripcion <> 'Will send it later'
	) ultimaMinuta
	where IdMinuta = MaxIdMinuta
end
go


if exists (select * from sysobjects where id = object_id('getValorParametro'))
begin 
	drop procedure getValorParametro 
end 
go



------------------------------------------------------------------------------------------------ 
-- VIEWS
------------------------------------------------------------------------------------------------

if exists (select * from sysobjects where id = object_id('itemsMinutasDeHoy'))
begin 
	drop view itemsMinutasDeHoy 
end 
go

create view itemsMinutasDeHoy
as
	-- El top 1000 es un hack para que me permita hacer un order by en una view.
	select	top(1000) 
			us.Usuario, us.NombreAMostrar, im.Tipo, im.Descripcion 
	from	ItemMinuta im

	join	Minuta m 
	on		m.Id = im.IdMinuta

	join	Usuario us
	on		us.Usuario = m.Usuario

	where	convert(date, m.Fecha) = convert(date, GetDate())

	order by us.OrdenEnMinuta
go

if exists (select * from sysobjects where id = object_id('usuariosQueEnviaronSuMinutaHoy'))
begin 
	drop view usuariosQueEnviaronSuMinutaHoy 
end 
go

create view usuariosQueEnviaronSuMinutaHoy
as

	select	u.Usuario, u.NombreAMostrar, u.Mail
	from	Usuario u

	join	Minuta m 
	on		m.Usuario = u.Usuario

	and		m.Fecha = convert(date, GetDate())

	where	u.Activo = 1

go

if exists (select * from sysobjects where id = object_id('usuariosQueNoEnviaronSuMinutaHoy'))
begin 
	drop view usuariosQueNoEnviaronSuMinutaHoy 
end 
go

create view usuariosQueNoEnviaronSuMinutaHoy
as

	select	u.Usuario, u.NombreAMostrar, u.Mail
	from	Usuario u
	where	u.Usuario not in ( 
								select	vwUsuarios.Usuario 
								from	usuariosQueEnviaronSuMinutaHoy vwUsuarios
							 )
	and		u.Activo = 1

go


if exists (select * from sysobjects where id = object_id('usuariosDelSistema'))
begin 
	drop view usuariosDelSistema 
end 
go

create view usuariosDelSistema
as

	select	u.Usuario, u.NombreAMostrar
	from	Usuario u
	where	u.Activo = 1
go


if exists (select * from sysobjects where id = object_id('suscriptoresMinuta'))
begin 
	drop view suscriptoresMinuta 
end 
go

create view suscriptoresMinuta
as

	select	s.Mail
	from	Suscriptor s

go