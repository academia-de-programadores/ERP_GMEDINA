USE [master]
GO
/****** Object:  Database [ERP_GMEDINA]    Script Date: 09/12/2019 16:29:25 ******/
CREATE DATABASE [ERP_GMEDINA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ERP_GMEDINA', FILENAME = N'D:\rdsdbdata\DATA\ERP_GMEDINA.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ERP_GMEDINA_log', FILENAME = N'D:\rdsdbdata\DATA\ERP_GMEDINA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ERP_GMEDINA] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ERP_GMEDINA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ERP_GMEDINA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET ARITHABORT OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ERP_GMEDINA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ERP_GMEDINA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ERP_GMEDINA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ERP_GMEDINA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET RECOVERY FULL 
GO
ALTER DATABASE [ERP_GMEDINA] SET  MULTI_USER 
GO
ALTER DATABASE [ERP_GMEDINA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ERP_GMEDINA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ERP_GMEDINA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ERP_GMEDINA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ERP_GMEDINA] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ERP_GMEDINA] SET QUERY_STORE = OFF
GO
USE [ERP_GMEDINA]
GO
/****** Object:  User [ahm]    Script Date: 09/12/2019 16:29:30 ******/
CREATE USER [ahm] FOR LOGIN [ahm] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ahm]
GO
/****** Object:  Schema [Acce]    Script Date: 09/12/2019 16:29:31 ******/
CREATE SCHEMA [Acce]
GO
/****** Object:  Schema [Plani]    Script Date: 09/12/2019 16:29:31 ******/
CREATE SCHEMA [Plani]
GO
/****** Object:  Schema [rrhh]    Script Date: 09/12/2019 16:29:31 ******/
CREATE SCHEMA [rrhh]
GO
/****** Object:  Table [Plani].[tbCatalogoDePlanillas]    Script Date: 09/12/2019 16:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbCatalogoDePlanillas](
	[cpla_IdPlanilla] [int] NOT NULL,
	[cpla_DescripcionPlanilla] [nvarchar](50) NOT NULL,
	[cpla_FrecuenciaEnDias] [int] NOT NULL,
	[cpla_RecibeComision] [bit] NOT NULL,
	[cpla_UsuarioCrea] [int] NOT NULL,
	[cpla_FechaCrea] [datetime] NOT NULL,
	[cpla_UsuarioModifica] [int] NULL,
	[cpla_FechaModifica] [datetime] NULL,
	[cpla_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbCatalogoPlanillas_cpla_IdPlanilla	] PRIMARY KEY CLUSTERED 
(
	[cpla_IdPlanilla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbHistorialDeduccionPago]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbHistorialDeduccionPago](
	[hidp_IdHistorialdeDeduPago] [int] NOT NULL,
	[cde_IdDeducciones] [int] NOT NULL,
	[hipa_IdHistorialDePago] [int] NOT NULL,
	[hidp_Total] [decimal](16, 4) NULL,
	[hidp_UsuarioCrea] [int] NOT NULL,
	[hidp_FechaCrea] [datetime] NOT NULL,
	[hidp_UsuarioModifica] [int] NULL,
	[hidp_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_Plani_tbHistorialDeDeduccionPago_hidp_IdHistorialdeDeduPago ] PRIMARY KEY CLUSTERED 
(
	[hidp_IdHistorialdeDeduPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbHistorialDePago]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbHistorialDePago](
	[hipa_IdHistorialDePago] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[hipa_SueldoNeto] [decimal](16, 4) NULL,
	[hipa_FechaInicio] [datetime] NOT NULL,
	[hipa_FechaFin] [datetime] NOT NULL,
	[hipa_FechaPago] [datetime] NOT NULL,
	[hipa_Anio] [int] NOT NULL,
	[hipa_Mes] [int] NOT NULL,
	[peri_IdPeriodo] [int] NOT NULL,
	[hipa_UsuarioCrea] [int] NOT NULL,
	[hipa_FechaCrea] [datetime] NOT NULL,
	[hipa_UsuarioModifica] [int] NULL,
	[hipa_FechaModifica] [datetime] NULL,
	[hipa_TotalISR] [decimal](16, 4) NOT NULL,
	[hipa_ISRPendiente] [bit] NULL,
	[hipa_AFP] [decimal](16, 4) NOT NULL,
 CONSTRAINT [PK_Plani_tbHistorialDePago_hipa_IdHistorialdePago] PRIMARY KEY CLUSTERED 
(
	[hipa_IdHistorialDePago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbCatalogoDeDeducciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbCatalogoDeDeducciones](
	[cde_IdDeducciones] [int] NOT NULL,
	[cde_DescripcionDeduccion] [nvarchar](50) NOT NULL,
	[tde_IdTipoDedu] [int] NOT NULL,
	[cde_PorcentajeColaborador] [decimal](16, 4) NULL,
	[cde_PorcentajeEmpresa] [decimal](16, 4) NULL,
	[cde_UsuarioCrea] [int] NOT NULL,
	[cde_FechaCrea] [datetime] NOT NULL,
	[cde_UsuarioModifica] [int] NULL,
	[cde_FechaModifica] [datetime] NULL,
	[cde_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbCatalogoDeducciones_cde_IdDeducciones] PRIMARY KEY CLUSTERED 
(
	[cde_IdDeducciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbTipoPlanillaDetalleDeduccion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbTipoPlanillaDetalleDeduccion](
	[tpdd_IdPlanillaDetDeduccion] [int] NOT NULL,
	[cpla_IdPlanilla] [int] NOT NULL,
	[cde_IdDeducciones] [int] NOT NULL,
	[tpdd_UsuarioCrea] [int] NOT NULL,
	[tpdd_FechaCrea] [datetime] NOT NULL,
	[tpdd_UsuarioModifica] [int] NULL,
	[tpdd_FechaModifica] [datetime] NULL,
	[tpdd_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbTipoPlanillaDetalleDeduccion_tpdd_IdPlanDetDedu] PRIMARY KEY CLUSTERED 
(
	[tpdd_IdPlanillaDetDeduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_GeneralTotales_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_GeneralTotales_RPT]
AS
SELECT 
PHP.hipa_FechaPago,
PCP.cpla_IdPlanilla,
PCP.cpla_DescripcionPlanilla,
SUM(PHP.hipa_TotalISR)													AS cde_TotalISR, 
SUM(PHP.hipa_AFP)														AS cde_TotalAFP,
SUM(IIF(PTPDD.cde_IdDeducciones = 1, PHDP.hidp_Total, 0))				AS cde_TotalIHSS,
SUM(IIF(PTPDD.cde_IdDeducciones = 2, PHDP.hidp_Total, 0))				AS cde_TotalRAP,
SUM(IIF(PTPDD.cde_IdDeducciones = 3, PHDP.hidp_Total, 0))				AS cde_TotalINFOP,
SUM(IIF(PTPDD.cde_IdDeducciones = 4, PHDP.hidp_Total, 0))				AS cde_OtrasDeducciones
FROM [Plani].[tbHistorialDePago]										AS PHP
INNER JOIN [Plani].[tbHistorialDeduccionPago]							AS PHDP		ON	PHDP.hipa_IdHistorialDePago			=	PHP.hipa_IdHistorialDePago
INNER JOIN [Plani].[tbTipoPlanillaDetalleDeduccion]						AS PTPDD	ON	PTPDD.tpdd_IdPlanillaDetDeduccion	=	PHDP.tpdd_IdPlanillaDetDeduccion
INNER JOIN [Plani].[tbCatalogoDePlanillas]								AS PCP		ON	PCP.cpla_IdPlanilla					=	PTPDD.cpla_IdPlanilla
INNER JOIN [Plani].[tbCatalogoDeDeducciones]							AS PCD		ON	PCD.cde_IdDeducciones               =	PTPDD.cde_IdDeducciones
GROUP BY PHP.hipa_FechaPago, PCP.cpla_IdPlanilla, PCP.cpla_DescripcionPlanilla
GO
/****** Object:  Table [rrhh].[tbPersonas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbPersonas](
	[per_Id] [int] NOT NULL,
	[per_Identidad] [nvarchar](16) NOT NULL,
	[per_Nombres] [nvarchar](50) NOT NULL,
	[per_Apellidos] [nvarchar](50) NOT NULL,
	[per_FechaNacimiento] [date] NOT NULL,
	[per_Sexo] [char](1) NULL,
	[per_Edad] [int] NULL,
	[nac_Id] [int] NOT NULL,
	[per_Direccion] [nvarchar](50) NULL,
	[per_Telefono] [nvarchar](20) NULL,
	[per_CorreoElectronico] [nvarchar](50) NULL,
	[per_EstadoCivil] [char](1) NULL,
	[per_TipoSangre] [nvarchar](4) NULL,
	[per_Estado] [bit] NOT NULL,
	[per_RazonInactivo] [nvarchar](100) NULL,
	[per_UsuarioCrea] [int] NOT NULL,
	[per_FechaCrea] [datetime] NOT NULL,
	[per_UsuarioModifica] [int] NULL,
	[per_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbPersonas_per_Id] PRIMARY KEY CLUSTERED 
(
	[per_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbEmpleados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbEmpleados](
	[emp_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[car_Id] [int] NOT NULL,
	[area_Id] [int] NOT NULL,
	[depto_Id] [int] NOT NULL,
	[jor_Id] [int] NOT NULL,
	[cpla_IdPlanilla] [int] NOT NULL,
	[fpa_IdFormaPago] [int] NOT NULL,
	[emp_CuentaBancaria] [nvarchar](100) NULL,
	[emp_Reingreso] [bit] NOT NULL,
	[emp_Fechaingreso] [datetime] NOT NULL,
	[emp_RazonSalida] [nvarchar](50) NULL,
	[emp_CargoAnterior] [int] NULL,
	[emp_FechaDeSalida] [datetime] NULL,
	[emp_Estado] [bit] NOT NULL,
	[emp_RazonInactivo] [nvarchar](100) NULL,
	[emp_UsuarioCrea] [int] NOT NULL,
	[emp_FechaCrea] [datetime] NOT NULL,
	[emp_UsuarioModifica] [int] NULL,
	[emp_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbEmpleados_emp_Id] PRIMARY KEY CLUSTERED 
(
	[emp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbDepartamentos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbDepartamentos](
	[depto_Id] [int] NOT NULL,
	[area_Id] [int] NOT NULL,
	[car_Id] [int] NOT NULL,
	[depto_Descripcion] [nvarchar](100) NOT NULL,
	[depto_Estado] [bit] NOT NULL,
	[depto_RazonInactivo] [nvarchar](100) NULL,
	[depto_UsuarioCrea] [int] NOT NULL,
	[depto_Fechacrea] [datetime] NOT NULL,
	[depto_UsuarioModifica] [int] NULL,
	[depto_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbDepartamentos_depto_Id] PRIMARY KEY CLUSTERED 
(
	[depto_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbAreas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbAreas](
	[area_Id] [int] NOT NULL,
	[car_Id] [int] NOT NULL,
	[suc_Id] [int] NOT NULL,
	[area_Descripcion] [nvarchar](50) NOT NULL,
	[area_Estado] [bit] NOT NULL,
	[area_Razoninactivo] [nvarchar](100) NULL,
	[area_Usuariocrea] [int] NOT NULL,
	[area_Fechacrea] [datetime] NOT NULL,
	[area_Usuariomodifica] [int] NULL,
	[area_Fechamodifica] [datetime] NULL,
 CONSTRAINT [PK_tbAreas_area_Id] PRIMARY KEY CLUSTERED 
(
	[area_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_ISR_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_ISR_RPT]
AS
SELECT  emp.emp_Id,
		per.per_Identidad,
		per.per_Nombres + ' ' + per.per_Apellidos  AS [NombreCompleto],
		depto.depto_Id,
		depto.depto_descripcion,
		are.area_Id,
		are.area_Descripcion,
		cp.cpla_IdPlanilla,
		cp.cpla_DescripcionPlanilla,
		hp.hipa_TotalISR,
		hp.hipa_SueldoNeto,
		hp.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp 
INNER JOIN [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
INNER JOIN [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
INNER JOIN [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
INNER JOIN [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
GO
/****** Object:  Table [Plani].[tbTipoDeduccion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbTipoDeduccion](
	[tde_IdTipoDedu] [int] NOT NULL,
	[tde_Descripcion] [nvarchar](50) NOT NULL,
	[tde_UsuarioCrea] [int] NOT NULL,
	[tde_FechaCrea] [datetime] NOT NULL,
	[tde_UsuarioModifica] [int] NULL,
	[tde_FechaModifica] [datetime] NULL,
	[tde_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbTipoDeducciones_tde_IdTipoDedu] PRIMARY KEY CLUSTERED 
(
	[tde_IdTipoDedu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbDeduccionInstitucionFinanciera]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbDeduccionInstitucionFinanciera](
	[deif_IdDeduccionInstFinanciera] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[insf_IdInstitucionFinanciera] [int] NOT NULL,
	[deif_Monto] [decimal](16, 4) NULL,
	[deif_Comentarios] [nvarchar](100) NULL,
	[cde_IdDeducciones] [int] NOT NULL,
	[deif_UsuarioCrea] [int] NOT NULL,
	[deif_FechaCrea] [datetime] NOT NULL,
	[deif_UsuarioModifica] [int] NULL,
	[deif_FechaModifica] [datetime] NULL,
	[deif_Activo] [bit] NOT NULL,
	[deif_Pagado] [bit] NULL,
 CONSTRAINT [PK_Plani_tbDeduInstiFinan_tdif_IdDeduInstitFinan] PRIMARY KEY CLUSTERED 
(
	[deif_IdDeduccionInstFinanciera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbInstitucionesFinancieras]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbInstitucionesFinancieras](
	[insf_IdInstitucionFinanciera] [int] NOT NULL,
	[insf_DescInstitucionFinanc] [nvarchar](50) NOT NULL,
	[insf_Contacto] [nvarchar](50) NOT NULL,
	[insf_Telefono] [nvarchar](50) NOT NULL,
	[insf_Correo] [nvarchar](50) NOT NULL,
	[insf_UsuarioCrea] [int] NOT NULL,
	[insf_FechaCrea] [datetime] NOT NULL,
	[insf_UsuarioModifica] [int] NULL,
	[insf_FechaModifica] [datetime] NULL,
	[insf_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbInstitucionFinanciera_insf_IdInstitucionFinanciera] PRIMARY KEY CLUSTERED 
(
	[insf_IdInstitucionFinanciera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_InstitucionesFinancieras_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create VIEW [Plani].[V_InstitucionesFinancieras_RPT]
as
SELECT 
emp.emp_Id,
per.per_Identidad,
per.per_Nombres,
per.per_Apellidos,
depto.depto_Id,
depto.depto_descripcion,
are.area_Id,
are.area_Descripcion,
cp.cpla_IdPlanilla,
cp.cpla_DescripcionPlanilla,
cdd.cde_IdDeducciones,
instifinan.insf_DescInstitucionFinanc,
dp.hidp_Total,
HP.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp 
inner join [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
inner join [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
inner join [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
inner join [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
inner join [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
inner join [Plani].[tbHistorialDeduccionPago] as dp on  dp.hipa_IdHistorialDePago = hp.hipa_IdHistorialDePago
inner join [Plani].[tbTipoPlanillaDetalleDeduccion] tpdd on dp.tpdd_IdPlanillaDetDeduccion = tpdd.tpdd_IdPlanillaDetDeduccion
inner join [Plani].[tbCatalogoDeDeducciones] cdd on cdd.cde_IdDeducciones  = tpdd.cde_IdDeducciones
inner join [Plani].[tbDeduccionInstitucionFinanciera] deduinsti on deduinsti.emp_Id = emp.emp_Id
inner join [Plani].[tbInstitucionesFinancieras] instifinan on instifinan.insf_IdInstitucionFinanciera = deduinsti.insf_IdInstitucionFinanciera
inner join [Plani].[tbTipoDeduccion] as td on cdd.tde_IdTipoDedu = td.tde_IdTipoDedu
WHERE  td.tde_Descripcion= 'Institución Financiera'
GO
/****** Object:  View [Plani].[V_tbCatalogoDeDeducciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_tbCatalogoDeDeducciones]
AS
SELECT * FROM [Plani].[tbCatalogoDeDeducciones]
GO
/****** Object:  Table [Acce].[tbUsuario]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acce].[tbUsuario](
	[usu_Id] [int] NOT NULL,
	[usu_NombreUsuario] [varchar](100) NULL,
	[usu_Password] [varbinary](64) NULL,
	[usu_Nombres] [varchar](150) NULL,
	[usu_Apellidos] [varchar](150) NULL,
	[usu_Correos] [varchar](150) NULL,
	[usu_EsActivo] [bit] NOT NULL,
	[usu_RazonInactivo] [varchar](150) NULL,
	[usu_EsAdministrador] [bit] NOT NULL,
	[usu_SesionesValidas] [tinyint] NULL,
 CONSTRAINT [PK_usu_Id] PRIMARY KEY CLUSTERED 
(
	[usu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialContrataciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialContrataciones](
	[hcon_Id] [int] NOT NULL,
	[scan_Id] [int] NOT NULL,
	[depto_Id] [int] NOT NULL,
	[hcon_FechaContratado] [date] NOT NULL,
	[hcon_Estado] [bit] NOT NULL,
	[hcon_RazonInactivo] [nvarchar](100) NULL,
	[hcon_UsuarioCrea] [int] NOT NULL,
	[hcon_FechaCrea] [datetime] NOT NULL,
	[hcon_UsuarioModifica] [int] NULL,
	[hcon_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialContrataciones_hcon_Id] PRIMARY KEY CLUSTERED 
(
	[hcon_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbSeleccionCandidatos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbSeleccionCandidatos](
	[scan_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[fare_Id] [int] NOT NULL,
	[scan_Fecha] [datetime] NULL,
	[rper_Id] [int] NOT NULL,
	[scan_Estado] [bit] NOT NULL,
	[scan_RazonInactivo] [nvarchar](100) NULL,
	[scan_UsuarioCrea] [int] NOT NULL,
	[scan_FechaCrea] [datetime] NOT NULL,
	[scan_UsuarioModifica] [int] NULL,
	[scan_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbSeleccionCandidatos_scan_Id] PRIMARY KEY CLUSTERED 
(
	[scan_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbCargos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbCargos](
	[car_Id] [int] NOT NULL,
	[car_Descripcion] [nvarchar](50) NOT NULL,
	[car_Estado] [bit] NOT NULL,
	[car_RazonInactivo] [nvarchar](100) NULL,
	[car_UsuarioCrea] [int] NOT NULL,
	[car_FechaCrea] [datetime] NOT NULL,
	[car_UsuarioModifica] [int] NULL,
	[car_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbCargos_car_Id] PRIMARY KEY CLUSTERED 
(
	[car_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialContrataciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [rrhh].[V_HistorialContrataciones]
	AS
		SELECT  HContrataciones.hcon_Id As Id,				
				PERSONAS.[per_Nombres] + ' '+ PERSONAS.per_Apellidos AS [Nombre Completo],
				Departamento.depto_Descripcion AS Departamento,
				Areas.area_Descripcion AS Area,
				Cargo.car_Descripcion AS Cargo,
				SeleccionCandidato.scan_Fecha AS [Fecha Seleccion Candidato],
				HContrataciones.hcon_FechaContratado AS [Fecha Contrato],
				UsuariosC.usu_NombreUsuario AS [Usuario Crea],
				HContrataciones.hcon_FechaCrea AS [Fecha Crea],
				UsuariosM.usu_NombreUsuario AS [Usuario Modifica],
				HContrataciones.hcon_FechaModifica AS [Fecha Modifica]   
	FROM       [RRHH].[tbHistorialContrataciones] AS HContrataciones
	INNER JOIN [RRHH].[tbSeleccionCandidatos]  AS SeleccionCandidato ON HContrataciones.scan_Id = SeleccionCandidato.scan_Id
	INNER JOIN [RRHH].[tbDepartamentos] AS Departamento ON [HContrataciones].depto_Id = Departamento.depto_Id
	INNER JOIN [RRHH].[tbAreas] AS Areas ON Departamento.area_Id = Areas.area_Id
	INNER JOIN [RRHH].[tbCargos] AS Cargo ON Departamento.car_Id = Cargo.car_Id
	INNER JOIN [RRHH].[tbPersonas]  AS Personas ON Personas.per_Id = SeleccionCandidato.per_Id
	INNER JOIN [Acce].[tbUsuario] AS UsuariosC ON HContrataciones.hcon_UsuarioCrea = UsuariosC.usu_Id
	LEFT JOIN  [Acce].[tbUsuario] AS UsuariosM ON HContrataciones.hcon_UsuarioModifica = UsuariosM.usu_Id
GO
/****** Object:  View [Plani].[V_DecimoCuartoMes]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DecimoCuartoMes]
AS

    SELECT HP.emp_Id,
            P.[per_Nombres]						 AS [Nombre],
            P.[per_Apellidos]					 AS [Apellido],
            C.[car_Descripcion]					 AS [Cargo],
            CP.[cpla_DescripcionPlanilla]		 AS [Planilla],
			E.[emp_CuentaBancaria]				 AS [CuentaBancaria],
            SUM(HP.[hipa_SueldoNeto]) / 360 * 30 AS [Monto]
    FROM [Plani].[tbHistorialDePago] HP
    INNER JOIN [Rrhh].[tbPersonas] P ON HP.emp_Id = P.per_Id
    INNER JOIN [Rrhh].[tbEmpleados] E ON E.[emp_Id] = P.per_Id
    INNER JOIN [Rrhh].[tbCargos] C ON C.[car_Id] = E.[car_Id]
    INNER JOIN [Plani].[tbCatalogoDePlanillas] CP ON CP.cpla_IdPlanilla = E.cpla_IdPlanilla
    WHERE HP.hipa_FechaPago BETWEEN CONVERT(DATETIME, CONCAT((YEAR(GETDATE())-1),'-06-30')) 
								AND CONVERT(DATETIME, CONCAT((YEAR(GETDATE())),'-06-30'))
    AND CP.cpla_IdPlanilla != 1
    GROUP BY HP.emp_Id,
                P.per_Nombres,
                P.per_Apellidos,
                C.[car_Descripcion],
                CP.cpla_DescripcionPlanilla,
				E.[emp_CuentaBancaria]	
GO
/****** Object:  Table [rrhh].[tbTipoHoras]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoHoras](
	[tiho_Id] [int] NOT NULL,
	[tiho_Descripcion] [nvarchar](25) NOT NULL,
	[tiho_Recargo] [int] NOT NULL,
	[tiho_Estado] [bit] NOT NULL,
	[tiho_RazonInactivo] [nvarchar](100) NULL,
	[tiho_UsuarioCrea] [int] NOT NULL,
	[tiho_FechaCrea] [datetime] NOT NULL,
	[tiho_UsuarioModifica] [int] NULL,
	[tiho_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoHoras_tiho_Id] PRIMARY KEY CLUSTERED 
(
	[tiho_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbJornadas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbJornadas](
	[jor_Id] [int] NOT NULL,
	[jor_Descripcion] [nvarchar](30) NOT NULL,
	[jor_Estado] [bit] NOT NULL,
	[jor_RazonInactivo] [nvarchar](100) NULL,
	[jor_UsuarioCrea] [int] NOT NULL,
	[jor_FechaCrea] [datetime] NOT NULL,
	[jor_UsuarioModifica] [int] NULL,
	[jor_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbJornadas_jor_Id] PRIMARY KEY CLUSTERED 
(
	[jor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialHorasTrabajadas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialHorasTrabajadas](
	[htra_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[tiho_Id] [int] NOT NULL,
	[jor_Id] [int] NOT NULL,
	[htra_CantidadHoras] [int] NOT NULL,
	[htra_Fecha] [date] NULL,
	[htra_Estado] [bit] NOT NULL,
	[htra_RazonInactivo] [nvarchar](100) NULL,
	[htra_UsuarioCrea] [int] NOT NULL,
	[htra_FechaCrea] [datetime] NOT NULL,
	[htra_UsuarioModifica] [int] NULL,
	[htra_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialHorasTrabajadas_htra_Id] PRIMARY KEY CLUSTERED 
(
	[htra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialHorasTrabajadas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_HistorialHorasTrabajadas]
	AS
	SELECT  HHorasTrabajadas.[htra_Id] as Id,
			Personas.[per_Nombres] + ' '+ PERSONAS.per_Apellidos AS [Nombre Completo],
			Jornadas.jor_Descripcion AS Jornada,
			TipoHoras.tiho_Descripcion AS [Tipo Horas],
			TipoHoras.tiho_Recargo AS [Recargo],       
			UsuariosC.usu_NombreUsuario AS [Usuario Crea],
			HHorasTrabajadas.htra_FechaCrea AS [Fecha Crea],
			UsuariosM.usu_NombreUsuario AS [Usuario Modifica],
			HHorasTrabajadas.htra_FechaModifica AS [Fecha Modifica]
       
		FROM       [RRHH].[tbHistorialHorasTrabajadas] AS HHorasTrabajadas
		Inner Join [RRHH].[tbEmpleados] AS Empleados ON HHorasTrabajadas.emp_Id = Empleados.emp_Id
		Inner Join [RRHH].[tbPersonas]  AS Personas ON Personas.per_Id = Empleados.per_Id
		Inner Join [RRHH].[tbTipoHoras]  AS TipoHoras ON TipoHoras.tiho_Id = HHorasTrabajadas.tiho_Id
		Inner Join [RRHH].[tbJornadas]  AS Jornadas ON Jornadas.jor_Id = HHorasTrabajadas.jor_Id
		Inner Join [Acce].[tbUsuario] AS UsuariosC ON HHorasTrabajadas.htra_UsuarioCrea = UsuariosC.usu_Id
		Left Join [Acce].[tbUsuario] AS UsuariosM ON HHorasTrabajadas.htra_UsuarioModifica = UsuariosM.usu_Id
GO
/****** Object:  Table [rrhh].[tbEquipoEmpleados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbEquipoEmpleados](
	[eqem_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[eqtra_Id] [int] NOT NULL,
	[eqem_Fecha] [datetime] NOT NULL,
	[eqem_Estado] [bit] NOT NULL,
	[eqem_RazonInactivo] [nvarchar](100) NULL,
	[eqem_UsuarioCrea] [int] NOT NULL,
	[eqem_FechaCrea] [datetime] NOT NULL,
	[eqem_UsuarioModifica] [int] NULL,
	[eqem_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbEquipoEmpleados_eqem_Id] PRIMARY KEY CLUSTERED 
(
	[eqem_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbEquipoTrabajo]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbEquipoTrabajo](
	[eqtra_Id] [int] NOT NULL,
	[eqtra_Codigo] [nvarchar](25) NOT NULL,
	[eqtra_Descripcion] [nvarchar](50) NOT NULL,
	[eqtra_Observacion] [nvarchar](50) NULL,
	[eqtra_Estado] [bit] NOT NULL,
	[eqtra_RazonInactivo] [nvarchar](100) NULL,
	[eqtra_UsuarioCrea] [int] NOT NULL,
	[eqtra_FechaCrea] [datetime] NOT NULL,
	[eqtra_UsuarioModifica] [int] NULL,
	[eqtra_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbEquipoTrabajo_eqtra_Id] PRIMARY KEY CLUSTERED 
(
	[eqtra_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbDeduccionesExtraordinarias]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbDeduccionesExtraordinarias](
	[dex_IdDeduccionesExtra] [int] NOT NULL,
	[eqem_Id] [int] NOT NULL,
	[dex_MontoInicial] [decimal](16, 4) NULL,
	[dex_MontoRestante] [decimal](16, 4) NULL,
	[dex_ObservacionesComentarios] [nvarchar](100) NULL,
	[cde_IdDeducciones] [int] NOT NULL,
	[dex_Cuota] [decimal](16, 4) NULL,
	[dex_UsuarioCrea] [int] NOT NULL,
	[dex_FechaCrea] [datetime] NOT NULL,
	[dex_UsuarioModifica] [int] NULL,
	[dex_FechaModifica] [datetime] NULL,
	[dex_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbIDeduccionesExtraordinarias_dex_IdDeduccionesExtra] PRIMARY KEY CLUSTERED 
(
	[dex_IdDeduccionesExtra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_DeduccionesExtraordinarias_Detalles]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DeduccionesExtraordinarias_Detalles]
AS
SELECT
pde.dex_IdDeduccionesExtra,
pde.eqem_Id,
CONCAT(rhp.per_Nombres, ' ', rhp.per_Apellidos) AS per_Empleado,
rhc.car_Descripcion AS car_Cargo,
rhd.depto_Descripcion AS depto_Departamento,
rha.area_Descripcion AS area_Area,
pde.dex_ObservacionesComentarios,
rhet.eqtra_Id,
rhet.eqtra_Codigo,
rhet.eqtra_Descripcion,
pde.dex_MontoInicial,
pde.dex_MontoRestante,
pde.dex_Cuota,
pde.cde_IdDeducciones,
pcd.cde_DescripcionDeduccion,
pde.dex_UsuarioCrea,
au.usu_NombreUsuario AS usu_UsuarioCrea,
pde.dex_FechaCrea,
pde.dex_UsuarioModifica,
aus.usu_NombreUsuario AS usu_UsuarioModifica,
pde.dex_FechaModifica,
pde.dex_Activo
FROM [Plani].[tbDeduccionesExtraordinarias] AS pde
INNER JOIN [Plani].[tbCatalogoDeDeducciones] AS pcd ON pcd.cde_IdDeducciones = pde.cde_IdDeducciones --Relación de Deducciones Extraordinarias con el Catalogo de Deducciones
INNER JOIN [rrhh].[tbEquipoEmpleados] AS rhee ON rhee.eqem_Id = pde.eqem_Id							 --Relación de Deducciones Extraordinarias con el Equipo Empleado 
INNER JOIN [rrhh].[tbEquipoTrabajo] AS rhet ON rhet.eqtra_Id = rhee.eqtra_Id						 --Relación de Equipo Empleado con el Equipo Trabajo
INNER JOIN [Acce].[tbUsuario] AS au ON au.usu_Id = pde.dex_UsuarioCrea								 --Relación de Deducciones Extraordinarias con Usuarios
INNER JOIN [Acce].[tbUsuario] AS aus ON aus.usu_Id = pde.dex_UsuarioModifica						 --Relación de Deducciones Extraordinarias con Usuarios
INNER JOIN [rrhh].[tbEmpleados] AS rhe ON rhe.emp_Id = rhee.emp_Id									 --Relación de Equipo Empleado con Empleados
INNER JOIN [rrhh].[tbPersonas] AS rhp ON rhp.per_Id = rhe.per_Id									 --Relación de Empleados con Personas
INNER JOIN [rrhh].[tbCargos] AS rhc ON rhc.car_Id = rhe.car_Id										 --Relación de Empleados con Cargos
INNER JOIN [rrhh].[tbDepartamentos] AS rhd ON rhd.car_Id = rhc.car_Id                                --Relación de Cargos con Departamentos
INNER JOIN [rrhh].[tbAreas] AS rha ON rha.area_Id = rhd.area_Id                                      --Relación de Departamentos con Áreas
WHERE pde.dex_Activo = 1
GO
/****** Object:  Table [Plani].[tbDecimoTercerMes]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbDecimoTercerMes](
	[dtm_IdDecimoTercerMes] [int] IDENTITY(1,1) NOT NULL,
	[dtm_FechaPago] [date] NOT NULL,
	[dtm_UsuarioCrea] [int] NOT NULL,
	[dtm_FechaCrea] [datetime] NOT NULL,
	[dtm_UsuarioModifica] [int] NULL,
	[dtm_FechaModifica] [datetime] NULL,
	[emp_Id] [int] NULL,
	[dtm_Monto] [decimal](16, 4) NULL,
	[dtm_CodigoPago] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Plani_tbDecimoTercerMes_dtm_Id_DecimoTercerMes] PRIMARY KEY CLUSTERED 
(
	[dtm_IdDecimoTercerMes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_DecimoTercerMes_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DecimoTercerMes_RPT]
AS

	SELECT 
	dt.dtm_IdDecimoTercerMes, 
	dt.emp_Id,
	p.[per_Nombres],
	p.[per_Apellidos],
	dt.dtm_FechaPago, 	
	dt.dtm_Monto, 
	e.emp_CuentaBancaria,
	dt.dtm_CodigoPago,
	cp.cpla_IdPlanilla,
	cp.[cpla_DescripcionPlanilla]
	FROM [Plani].[tbDecimoTercerMes] dt
	INNER JOIN [rrhh].[tbPersonas] p ON dt.emp_Id = p.[per_Id]
	INNER JOIN [rrhh].[tbEmpleados] e ON e.emp_Id = dt.emp_Id
	INNER JOIN [Plani].[tbCatalogoDePlanillas] cp ON cp.[cpla_IdPlanilla] = e.[cpla_IdPlanilla]

GO
/****** Object:  Table [rrhh].[tbTitulos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTitulos](
	[titu_Id] [int] NOT NULL,
	[titu_Descripcion] [nvarchar](100) NOT NULL,
	[titu_Estado] [bit] NOT NULL,
	[titu_RazonInactivo] [nvarchar](100) NULL,
	[titu_UsuarioCrea] [int] NOT NULL,
	[titu_FechaCrea] [datetime] NOT NULL,
	[titu_UsuarioModifica] [int] NULL,
	[titu_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTitulos_titu_Id] PRIMARY KEY CLUSTERED 
(
	[titu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbTitulosPersona]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTitulosPersona](
	[tipe_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[titu_Id] [int] NOT NULL,
	[titu_Anio] [int] NOT NULL,
	[tipe_Estado] [bit] NOT NULL,
	[tipe_RazonInactivo] [nvarchar](100) NULL,
	[tipe_UsuarioCrea] [int] NOT NULL,
	[tipe_FechaCrea] [datetime] NULL,
	[tipe_UsuarioModifica] [int] NULL,
	[tipe_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTitulosPersona_tipe_Id] PRIMARY KEY CLUSTERED 
(
	[tipe_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHabilidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHabilidades](
	[habi_Id] [int] NOT NULL,
	[habi_Descripcion] [nvarchar](100) NULL,
	[habi_Estado] [bit] NOT NULL,
	[habi_RazonInactivo] [nvarchar](100) NULL,
	[habi_UsuarioCrea] [int] NOT NULL,
	[habi_FechaCrea] [datetime] NOT NULL,
	[habi_UsuarioModifica] [int] NULL,
	[habi_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHabilidades_habi_Id] PRIMARY KEY CLUSTERED 
(
	[habi_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbCompetencias]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbCompetencias](
	[comp_Id] [int] NOT NULL,
	[comp_Descripcion] [nvarchar](100) NOT NULL,
	[comp_Estado] [bit] NOT NULL,
	[comp_RazonInactivo] [nvarchar](100) NULL,
	[comp_UsuarioCrea] [int] NOT NULL,
	[comp_FechaCrea] [datetime] NOT NULL,
	[comp_UsuarioModifica] [int] NULL,
	[comp_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbCompetencias_comp_Id] PRIMARY KEY CLUSTERED 
(
	[comp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbCompetenciasPersona]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbCompetenciasPersona](
	[cope_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[comp_Id] [int] NOT NULL,
	[cope_Estado] [bit] NOT NULL,
	[cope_RazonInactivo] [nvarchar](100) NULL,
	[cope_UsuarioCrea] [int] NOT NULL,
	[cope_FechaCrea] [datetime] NOT NULL,
	[cope_UsuarioModifica] [int] NULL,
	[cope_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbcompetenciasPersona_cope_Id] PRIMARY KEY CLUSTERED 
(
	[cope_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHabilidadesPersona]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHabilidadesPersona](
	[hape_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[habi_Id] [int] NOT NULL,
	[hape_Estado] [bit] NOT NULL,
	[hape_RazonInactivo] [nvarchar](100) NULL,
	[hape_UsuarioCrea] [int] NOT NULL,
	[hape_FechaCrea] [datetime] NOT NULL,
	[hape_UsuarioModifica] [int] NULL,
	[hape_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHabilidadesPersona_hape_Id] PRIMARY KEY CLUSTERED 
(
	[hape_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbIdiomaPersona]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbIdiomaPersona](
	[idpe_Id] [int] NOT NULL,
	[per_Id] [int] NULL,
	[idi_Id] [int] NULL,
	[idpe_Estado] [bit] NOT NULL,
	[idpe_RazonInactivo] [nvarchar](100) NULL,
	[idpe_UsuarioCrea] [int] NOT NULL,
	[idpe_FechaCrea] [datetime] NOT NULL,
	[idpe_UsuarioModifica] [int] NULL,
	[idpe_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbIdiomaPersona_idpe_Id] PRIMARY KEY CLUSTERED 
(
	[idpe_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbIdiomas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbIdiomas](
	[idi_Id] [int] NOT NULL,
	[idi_Descripcion] [nvarchar](50) NULL,
	[idi_Estado] [bit] NOT NULL,
	[idi_RazonInactivo] [nvarchar](100) NULL,
	[idi_UsuarioCrea] [int] NOT NULL,
	[idi_FechaCrea] [datetime] NOT NULL,
	[idi_UsuarioModifica] [int] NULL,
	[idi_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbidiomas_idi_Id] PRIMARY KEY CLUSTERED 
(
	[idi_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbRequerimientosEspeciales]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbRequerimientosEspeciales](
	[resp_Id] [int] NOT NULL,
	[resp_Descripcion] [nvarchar](50) NULL,
	[resp_Estado] [bit] NOT NULL,
	[resp_RazonInactivo] [nvarchar](50) NULL,
	[resp_UsuarioCrea] [int] NOT NULL,
	[resp_FechaCrea] [datetime] NOT NULL,
	[resp_UsuarioModifica] [int] NULL,
	[resp_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbRequerimientosEspeciales_resp_Id] PRIMARY KEY CLUSTERED 
(
	[resp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbRequerimientosEspecialesPersona]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbRequerimientosEspecialesPersona](
	[rep_Id] [int] NOT NULL,
	[per_Id] [int] NOT NULL,
	[resp_Id] [int] NOT NULL,
	[rep_Estado] [bit] NOT NULL,
	[rep_RazonInactivo] [nvarchar](100) NULL,
	[rep_UsuarioCrea] [int] NOT NULL,
	[rep_FechaCrea] [datetime] NOT NULL,
	[rep_UsuarioModifica] [int] NULL,
	[rep_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbRequerimientosEspecialesPersona_rep_Id] PRIMARY KEY CLUSTERED 
(
	[rep_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_tbPersonas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_tbPersonas] 
AS
SELECT      CP.per_Id per_Id, CP.comp_Id Relacion_Id, C.comp_Descripcion Descripcion, CP.cope_Estado Estado, 1 Relacion
FROM        rrhh.tbCompetenciasPersona AS CP INNER JOIN
			rrhh.tbCompetencias AS C ON CP.comp_Id = C.comp_Id
WHERE		CP.cope_Estado = 1
UNION ALL
SELECT      HP.per_Id per_Id, HP.habi_Id Relacion_Id, H.habi_Descripcion Descripcion, HP.hape_Estado Estado, 2 Relacion
FROM        rrhh.tbHabilidadesPersona AS HP INNER JOIN
			rrhh.tbHabilidades AS H ON HP.habi_Id = H.habi_Id
WHERE		HP.hape_Estado = 1
UNION ALL
SELECT      IP.per_Id per_Id, IP.idi_Id Relacion_Id, I.idi_Descripcion Descripcion, IP.idpe_Estado Estado, 3 Relacion
FROM        rrhh.tbIdiomaPersona AS IP INNER JOIN
            rrhh.tbIdiomas AS I ON IP.idi_Id = I.idi_Id
WHERE		IP.idpe_Estado = 1
UNION ALL
SELECT      REP.per_Id per_Id, REP.resp_Id Relacion_Id, RE.resp_Descripcion Descripcion, REP.rep_Estado Estado, 4 Relacion
FROM        rrhh.tbRequerimientosEspecialesPersona AS REP INNER JOIN
            rrhh.tbRequerimientosEspeciales AS RE ON REP.resp_Id = RE.resp_Id
WHERE		REP.rep_Estado = 1
UNION ALL
SELECT		TP.per_Id per_Id, TP.titu_Id Relacion_Id, T.titu_Descripcion Descripcion, tp.tipe_Estado Estado, 5 Relacion
FROM        rrhh.tbTitulosPersona AS TP INNER JOIN
            rrhh.tbTitulos AS T ON TP.titu_Id = T.titu_Id
WHERE		TP.tipe_Estado = 1
GO
/****** Object:  View [rrhh].[V_Departamentos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [rrhh].[V_Departamentos]
            --WITH ENCRYPTION, SCHEMABINDING, VIEW_METADATA
            AS

SELECT 
	   d.depto_Id
      ,d.area_Id
      ,d.depto_Descripcion
      ,d.depto_UsuarioCrea
      ,d.depto_Fechacrea
      ,d.depto_Usuariomodifica
      ,d.depto_Fechamodifica
      ,c.car_Id
      ,c.car_Descripcion
      ,p.per_Id
      ,isnull(p.per_Nombres + ' ' + p.per_Apellidos,'No Asignado') AS per_NombreCompleto
      ,p.per_Telefono
      ,p.per_CorreoElectronico
  FROM rrhh.tbDepartamentos d
  INNER JOIN rrhh.tbCargos c ON d.car_Id = c.car_Id
  left JOIN rrhh.tbEmpleados e ON e.car_Id = c.car_Id
  left JOIN rrhh.tbPersonas p ON p.per_Id = e.per_Id
GO
/****** Object:  Table [Plani].[tbPeriodos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbPeriodos](
	[peri_IdPeriodo] [int] NOT NULL,
	[peri_DescripPeriodo] [nvarchar](100) NOT NULL,
	[peri_UsuarioCrea] [int] NOT NULL,
	[peri_FechaCrea] [datetime] NOT NULL,
	[peri_UsuarioModifica] [int] NULL,
	[peri_FechaModifica] [datetime] NULL,
	[peri_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbPeriodos_peri_IdPeriodo] PRIMARY KEY CLUSTERED 
(
	[peri_IdPeriodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbCatalogoDeIngresos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbCatalogoDeIngresos](
	[cin_IdIngreso] [int] NOT NULL,
	[cin_DescripcionIngreso] [nvarchar](50) NOT NULL,
	[cin_UsuarioCrea] [int] NOT NULL,
	[cin_FechaCrea] [datetime] NOT NULL,
	[cin_UsuarioModifica] [int] NULL,
	[cin_FechaModifica] [datetime] NULL,
	[cin_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbCatalogoIngresos_cin_IdIngreso] PRIMARY KEY CLUSTERED 
(
	[cin_IdIngreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbHistorialDeIngresosPago]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbHistorialDeIngresosPago](
	[hip_IdHistorialDeIngresosPago] [int] NOT NULL,
	[hipa_IdHistorialDePago] [int] NOT NULL,
	[hip_FechaInicio] [datetime] NOT NULL,
	[hip_FechaFinal] [datetime] NOT NULL,
	[hip_UnidadesPagar] [int] NOT NULL,
	[hip_MedidaUnitaria] [int] NOT NULL,
	[hip_TotalPagar] [decimal](16, 4) NULL,
	[cin_IdIngreso] [int] NOT NULL,
	[hip_UsuarioCrea] [int] NOT NULL,
	[hip_FechaCrea] [datetime] NOT NULL,
	[hip_UsuarioModifica] [int] NULL,
	[hip_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_Plani_tbHistorialDeIngresosPago_hip_IdHistorialdeIngresoPago ] PRIMARY KEY CLUSTERED 
(
	[hip_IdHistorialDeIngresosPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbFormaPago]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbFormaPago](
	[fpa_IdFormaPago] [int] NOT NULL,
	[fpa_Descripcion] [nvarchar](50) NOT NULL,
	[fpa_UsuarioCrea] [int] NOT NULL,
	[fpa_FechaCrea] [datetime] NOT NULL,
	[fpa_UsuarioModifica] [int] NULL,
	[fpa_FechaModifica] [datetime] NULL,
	[fpa_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbFormaPago_fpa_IdFormaPago] PRIMARY KEY CLUSTERED 
(
	[fpa_IdFormaPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbTipoPlanillaDetalleIngreso]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbTipoPlanillaDetalleIngreso](
	[tpdi_IdDetallePlanillaIngreso] [int] NOT NULL,
	[cin_IdIngreso] [int] NOT NULL,
	[cpla_IdPlanilla] [int] NOT NULL,
	[tpdi_UsuarioCrea] [int] NOT NULL,
	[tpdi_FechaCrea] [datetime] NOT NULL,
	[tpdi_UsuarioModifica] [int] NULL,
	[tpdi_FechaModifica] [datetime] NULL,
	[tpdi_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbTipoPlanillaDetalleIngreso_tpdi_IdPlanDetIng] PRIMARY KEY CLUSTERED 
(
	[tpdi_IdDetallePlanillaIngreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_Plani_EmpleadoPorPlanilla]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_Plani_EmpleadoPorPlanilla] AS
SELECT     EMP.emp_Id, 
	       CONCAT(PER.per_Nombres,' ',PER.per_Apellidos) AS NombreColaborador, 
	       CAR.car_Descripcion, 
	       ARE.area_Descripcion,
	       CDP.cpla_DescripcionPlanilla, 
	       FOP.fpa_Descripcion,
	       HIP.hipa_FechaPago,
	       HIP.hipa_SueldoNeto,
           PRI.peri_DescripPeriodo,
		   CDI.cin_DescripcionIngreso,
           HDI.hip_TotalPagar,
		   CDD.cde_DescripcionDeduccion,
		   HDD.hidp_Total
FROM       [rrhh].[tbEmpleados]                     AS EMP
INNER JOIN [rrhh].[tbPersonas]                      AS PER ON EMP.per_Id                        = PER.per_Id
INNER JOIN [rrhh].[tbCargos]		                AS CAR ON CAR.car_Id                        = EMP.car_Id
INNER JOIN [rrhh].[tbAreas]		                    AS ARE ON ARE.area_Id                       = EMP.area_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas]          AS CDP ON CDP.cpla_IdPlanilla               = EMP.cpla_IdPlanilla
INNER JOIN [Plani].[tbFormaPago]			        AS FOP ON FOP.fpa_IdFormaPago               = EMP.fpa_IdFormaPago
INNER JOIN [Plani].[tbHistorialDePago]              AS HIP ON HIP.emp_Id                        = EMP.emp_Id
INNER JOIN [Plani].[tbPeriodos]				        AS PRI ON PRI.peri_IdPeriodo	            = HIP.peri_IdPeriodo
LEFT JOIN  [Plani].[tbHistorialDeIngresosPago]	    AS HDI ON HDI.hipa_IdHistorialDePago        = HIP.hipa_IdHistorialDePago
LEFT JOIN  [Plani].[tbHistorialDeduccionPago]       AS HDD ON HDD.hipa_IdHistorialDePago        = HIP.hipa_IdHistorialDePago
LEFT JOIN  [Plani].[tbTipoPlanillaDetalleDeduccion] AS TDD ON TDD.tpdd_IdPlanillaDetDeduccion   = HDD.tpdd_IdPlanillaDetDeduccion
LEFT JOIN  [Plani].[tbTipoPlanillaDetalleIngreso]   AS TDI ON TDI.tpdi_IdDetallePlanillaIngreso = HDI.tpdi_IdDetallePlanillaIngreso
INNER JOIN [Plani].[tbCatalogoDeIngresos]			AS CDI ON CDI.cin_IdIngreso                 = TDI.cin_IdIngreso
INNER JOIN [Plani].[tbCatalogoDeDeducciones]        AS CDD ON CDD.cde_IdDeducciones			    = TDD.cde_IdDeducciones
GO
/****** Object:  View [Plani].[V_Plani_TipoPlani]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_Plani_TipoPlani] AS
SELECT  CATPLA.cpla_IdPlanilla,
        CATPLA.cpla_DescripcionPlanilla
FROM [Plani].[tbCatalogoDePlanillas]      AS CATPLA
INNER JOIN [rrhh].[tbEmpleados]           AS EMP        ON CATPLA.cpla_IdPlanilla  =    EMP.cpla_IdPlanilla
INNER JOIN [Plani].[tbHistorialDePago]    AS HP        ON EMP.emp_Id               =    HP.emp_Id
WHERE CATPLA.cpla_Activo = 1
GROUP BY CATPLA.cpla_IdPlanilla,CATPLA.cpla_DescripcionPlanilla
GO
/****** Object:  View [Plani].[V_Plani_FechaPlanilla]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_Plani_FechaPlanilla] AS
SELECT DISTINCT CATPLA.cpla_IdPlanilla,
        CATPLA.cpla_DescripcionPlanilla,
        CAST(HP.hipa_FechaPago AS DATE) AS hipa_FechaPago
FROM [Plani].[tbCatalogoDePlanillas]      AS CATPLA
INNER JOIN [rrhh].[tbEmpleados]           AS EMP        ON CATPLA.cpla_IdPlanilla  =    EMP.cpla_IdPlanilla
INNER JOIN [Plani].[tbHistorialDePago]    AS HP        ON EMP.emp_Id               =    HP.emp_Id
GROUP BY CATPLA.cpla_DescripcionPlanilla, HP.hipa_FechaPago, CATPLA.cpla_IdPlanilla
GO
/****** Object:  Table [Plani].[tbAdelantoSueldo]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbAdelantoSueldo](
	[adsu_IdAdelantoSueldo] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[adsu_FechaAdelanto] [datetime] NOT NULL,
	[adsu_RazonAdelanto] [nvarchar](50) NOT NULL,
	[adsu_Monto] [decimal](16, 4) NULL,
	[adsu_Deducido] [bit] NOT NULL,
	[adsu_UsuarioCrea] [int] NOT NULL,
	[adsu_FechaCrea] [datetime] NOT NULL,
	[adsu_UsuarioModifica] [int] NULL,
	[adsu_FechaModifica] [datetime] NULL,
	[adsu_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbAdelantoSueldo_IdAdelantoSueldo] PRIMARY KEY CLUSTERED 
(
	[adsu_IdAdelantoSueldo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_tbAdelantoSueldo]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Plani].[V_tbAdelantoSueldo]
AS
SELECT 
	ad.adsu_IdAdelantoSueldo, 
	e.emp_Id, 
	(p.per_Nombres + ' ' + p.per_Apellidos) AS per_Nombres,
	ad.adsu_FechaAdelanto,
	ad.adsu_RazonAdelanto, 
	ad.adsu_Monto,  
	ad.adsu_Deducido, 
	ad.adsu_UsuarioCrea,
	(crea.usu_Nombres + ' ' + crea.usu_Apellidos) AS NombreUsuarioCrea,
	crea.usu_NombreUsuario AS UsuarioCrea,
	ad.adsu_FechaCrea,
	ad.adsu_UsuarioModifica, 
	(modif.usu_Nombres + ' ' + modif.usu_Apellidos) AS NombreUsuarioModifica,
	modif.usu_NombreUsuario AS UsuarioModifica, 
	ad.adsu_FechaModifica,
	ad.adsu_Activo
FROM [Plani].[tbAdelantoSueldo] AS ad
INNER JOIN [rrhh].[tbEmpleados] AS e ON ad.emp_Id = e.emp_Id
INNER JOIN [rrhh].[tbPersonas] AS p ON p.per_Id = e.per_Id
INNER JOIN [Acce].[tbUsuario] AS crea ON ad.adsu_UsuarioCrea = crea.usu_Id
LEFT JOIN [Acce].[tbUsuario] AS modif ON ad.adsu_UsuarioModifica = modif.usu_Id
WHERE adsu_Activo = 1
GO
/****** Object:  View [rrhh].[V_Empleados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [rrhh].[V_Empleados]
as
select
emp.emp_Id, 
emp.per_Id, 
per.per_Nombres,
per.per_Apellidos,
concat(per.per_Nombres,' ',per.per_Apellidos) AS per_NombreCompleto,
emp_Estado
from [rrhh].[tbEmpleados] AS emp 
inner join [rrhh].[tbPersonas] AS per on emp.per_Id = per.per_Id
where emp_Estado = 1
GO
/****** Object:  View [Plani].[V_DeduccionesExtraordinarias_EquipoEmpleado]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DeduccionesExtraordinarias_EquipoEmpleado]
AS
SELECT
rhee.eqem_Id,
rhet.eqtra_Id,
rhet.eqtra_Codigo,
CONCAT(rhet.eqtra_Descripcion, ' - ', rhp.per_Nombres, ' ', rhp.per_Apellidos) AS per_EquipoEmpleado
FROM [rrhh].[tbEquipoEmpleados] AS rhee
INNER JOIN [rrhh].[tbEquipoTrabajo] AS rhet ON rhee.eqtra_Id = rhet.eqtra_Id
INNER JOIN [rrhh].[tbEmpleados] AS rhe ON rhee.emp_Id = rhe.emp_Id
INNER JOIN [rrhh].[tbPersonas] AS rhp ON rhe.per_Id = rhp.per_Id
WHERE rhee.eqem_Estado = 1 
GO
/****** Object:  Table [rrhh].[tbHistorialAmonestaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialAmonestaciones](
	[hamo_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[tamo_Id] [int] NOT NULL,
	[hamo_Fecha] [datetime] NOT NULL,
	[hamo_AmonestacionAnterior] [int] NOT NULL,
	[hamo_Observacion] [nvarchar](150) NULL,
	[hamo_Estado] [bit] NOT NULL,
	[hamo_RazonInactivo] [nvarchar](100) NULL,
	[hamo_UsuarioCrea] [int] NOT NULL,
	[hamo_FechaCrea] [datetime] NOT NULL,
	[hamo_UsuarioModifica] [int] NULL,
	[hamo_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialAmonestaciones_hamo_Id] PRIMARY KEY CLUSTERED 
(
	[hamo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbTipoAmonestaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoAmonestaciones](
	[tamo_Id] [int] NOT NULL,
	[tamo_Descripcion] [nvarchar](50) NOT NULL,
	[tamo_Estado] [bit] NOT NULL,
	[tamo_RazonInactivo] [nvarchar](100) NULL,
	[tamo_UsuarioCrea] [int] NOT NULL,
	[tamo_FechaCrea] [datetime] NOT NULL,
	[tamo_UsuarioModifica] [int] NULL,
	[tamo_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoAmonestaciones_tamo_Id] PRIMARY KEY CLUSTERED 
(
	[tamo_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialAmonestacion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_HistorialAmonestacion]
AS
SELECT	Empleados.emp_Id,
		Personas.per_Nombres +' '+ Personas.per_Apellidos AS emp_Nombre,
		Empleados.emp_Estado,
		TipoAmonestacion.tamo_Id,
		TipoAmonestacion.tamo_Descripcion,
		HistorialAmonestaciones.hamo_Id,
		CAST(HistorialAmonestaciones.hamo_Fecha AS DATE) AS hamo_Fecha,
		HistorialAmonestaciones.hamo_AmonestacionAnterior,
		HistorialAmonestaciones.hamo_Observacion,
		HistorialAmonestaciones.hamo_Estado,
		HistorialAmonestaciones.hamo_RazonInactivo,
		HistorialAmonestaciones.hamo_UsuarioCrea,
		HistorialAmonestaciones.hamo_FechaCrea,
		HistorialAmonestaciones.hamo_UsuarioModifica,
		HistorialAmonestaciones.hamo_FechaModifica
FROM    [ERP_GMEDINA].[rrhh].[tbPersonas] AS Personas 
INNER JOIN [ERP_GMEDINA].[rrhh].[tbEmpleados] AS Empleados 
ON	    Personas.per_Id = Empleados.per_Id 
INNER JOIN  [ERP_GMEDINA].[rrhh].[tbHistorialAmonestaciones] AS HistorialAmonestaciones
ON 	    HistorialAmonestaciones.emp_Id = Empleados.emp_Id
INNER JOIN [ERP_GMEDINA].[rrhh].[tbTipoAmonestaciones] AS TipoAmonestacion
ON      HistorialAmonestaciones.tamo_Id = TipoAmonestacion.tamo_Id
GO
/****** Object:  Table [rrhh].[tbHistorialAudienciaDescargo]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialAudienciaDescargo](
	[aude_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[aude_Descripcion] [nvarchar](25) NOT NULL,
	[aude_FechaAudiencia] [datetime] NOT NULL,
	[aude_Testigo] [bit] NOT NULL,
	[aude_DireccionArchivo] [nvarchar](25) NULL,
	[aude_Estado] [bit] NOT NULL,
	[aude_RazonInactivo] [nvarchar](100) NULL,
	[aude_UsuarioCrea] [int] NOT NULL,
	[aude_FechaCrea] [datetime] NOT NULL,
	[aude_UsuarioModifica] [int] NULL,
	[aude_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialAudienciaDescargo_aude_Id] PRIMARY KEY CLUSTERED 
(
	[aude_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialAudienciaDescargo]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [rrhh].[V_HistorialAudienciaDescargo]
AS
SELECT   HistorialAudienciaDescargo.aude_Id,
         Empleados.emp_Id,
		 Personas.per_Nombres +' '+ Personas.per_Apellidos AS emp_Nombre,
		 Empleados.emp_Estado,
		 HistorialAudienciaDescargo.aude_Descripcion,
		 HistorialAudienciaDescargo.aude_FechaAudiencia,
		 HistorialAudienciaDescargo.aude_Testigo,
		 HistorialAudienciaDescargo.aude_DireccionArchivo,
		 HistorialAudienciaDescargo.aude_Estado,
		 HistorialAudienciaDescargo.aude_RazonInactivo,
		 HistorialAudienciaDescargo.aude_UsuarioCrea,
		 HistorialAudienciaDescargo.aude_FechaCrea,
		 HistorialAudienciaDescargo.aude_UsuarioModifica,
		 HistorialAudienciaDescargo.aude_FechaModifica
FROM  [ERP_GMEDINA].[rrhh].[tbPersonas] AS Personas
INNER JOIN [ERP_GMEDINA].[rrhh].[tbEmpleados] AS Empleados
ON	Personas.per_Id = Empleados.per_Id 
INNER JOIN [ERP_GMEDINA].[rrhh].[tbHistorialAudienciaDescargo] AS HistorialAudienciaDescargo
ON HistorialAudienciaDescargo.emp_Id = Empleados.emp_Id
GO
/****** Object:  View [rrhh].[V_tbEmpleados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [rrhh].[V_tbEmpleados]
as
select concat(p.per_Nombres,' ',per_Apellidos) AS NombreCompleto
,emp.* 
from [rrhh].tbEmpleados emp
inner join rrhh.tbPersonas p on emp.per_Id = p.per_Id
where emp.emp_Estado = 1
GO
/****** Object:  Table [rrhh].[tbSueldos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbSueldos](
	[sue_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[tmon_Id] [int] NOT NULL,
	[sue_Cantidad] [decimal](8, 4) NOT NULL,
	[sue_SueldoAnterior] [int] NULL,
	[sue_Estado] [bit] NOT NULL,
	[sue_RazonInactivo] [nvarchar](100) NULL,
	[sue_UsuarioCrea] [int] NOT NULL,
	[sue_FechaCrea] [datetime] NULL,
	[sue_UsuarioModifica] [int] NULL,
	[sue_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbSueldos_hsue_Id] PRIMARY KEY CLUSTERED 
(
	[sue_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_InformacionColaborador]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_InformacionColaborador] AS
SELECT 
EMP.emp_Id,
EMP.emp_Estado,
ar.area_Id,
ar.area_Descripcion,
depto.depto_Id,
depto.depto_Descripcion,
jor.jor_Id,
jor.jor_Descripcion,
PER.per_Id,
PER.per_Nombres,
PER.per_Apellidos,
PER.per_Identidad,
PER.per_Sexo,
PER.per_Edad,
PER.per_Direccion,
PER.per_Telefono,
PER.per_CorreoElectronico,
PER.per_EstadoCivil,
SUE.sue_Cantidad AS SalarioBase,
EMP.cpla_IdPlanilla,
EMP.emp_CuentaBancaria,
FP.fpa_IdFormaPago,
FP.fpa_Descripcion,
ca.car_Id,
ca.car_Descripcion
FROM [rrhh].[tbEmpleados] EMP
LEFT JOIN [rrhh].[tbPersonas] PER ON EMP.per_Id = PER.per_Id
LEFT JOIN [rrhh].[tbSueldos] SUE ON EMP.emp_Id = SUE.emp_Id
LEFT JOIN Plani.tbFormaPago FP ON EMP.fpa_IdFormaPago = FP.fpa_IdFormaPago
LEFT JOIN [rrhh].[tbCargos] ca on EMP.car_Id = ca.car_Id
LEFT JOIN [rrhh].[tbAreas] ar on ar.area_Id = EMP.area_Id
LEFT JOIN [rrhh].[tbDepartamentos] depto on depto.depto_Id = EMP.depto_Id
LEFT JOIN [rrhh].[tbJornadas] jor on emp.jor_Id = jor.jor_Id
WHERE sue_SueldoAnterior IS NULL
GO
/****** Object:  View [Plani].[V_PlanillaDeducciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_PlanillaDeducciones] AS
SELECT 
CPLA.cpla_IdPlanilla,
CPLA.cpla_DescripcionPlanilla,
CPLA.cpla_FrecuenciaEnDias,
PLADE.tpdd_IdPlanillaDetDeduccion,
PLADE.cde_IdDeducciones,
CDE.cde_DescripcionDeduccion,
CDE.tde_IdTipoDedu,
TIPODE.tde_Descripcion,
CDE.cde_PorcentajeColaborador,
CDE.cde_PorcentajeEmpresa
FROM [Plani].[tbCatalogoDePlanillas] CPLA
INNER JOIN [Plani].[tbTipoPlanillaDetalleDeduccion] PLADE ON CPLA.cpla_IdPlanilla = PLADE.cpla_IdPlanilla
INNER JOIN [Plani].[tbCatalogoDeDeducciones] CDE ON PLADE.cde_IdDeducciones = CDE.cde_IdDeducciones
INNER JOIN [Plani].[tbTipoDeduccion] TIPODE ON CDE.tde_IdTipoDedu = TIPODE.tde_IdTipoDedu
-- WHERE CPLA.cpla_IdPlanilla = 2
GO
/****** Object:  View [Plani].[V_PlanillaIngresos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_PlanillaIngresos] AS
SELECT 
CPLA.cpla_IdPlanilla,
CPLA.cpla_DescripcionPlanilla,
cpla_FrecuenciaEnDias,
PLAIN.tpdi_IdDetallePlanillaIngreso,
CIN.cin_IdIngreso,
CIN.cin_DescripcionIngreso
FROM [Plani].[tbCatalogoDePlanillas] CPLA
INNER JOIN [Plani].[tbTipoPlanillaDetalleIngreso] PLAIN ON CPLA.cpla_IdPlanilla = PLAIN.cpla_IdPlanilla
INNER JOIN [Plani].[tbCatalogoDeIngresos] CIN ON PLAIN.cin_IdIngreso = CIN.cin_IdIngreso
--WHERE CPLA.cpla_IdPlanilla = 1
GO
/****** Object:  View [Plani].[V_DeduccionesExtrasColaboradores]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DeduccionesExtrasColaboradores] AS
SELECT 
E.eqtra_Id,
E.eqtra_codigo,
E.eqtra_Descripcion,
E.eqtra_observacion,
EE.emp_Id,
EE.eqem_Fecha,
DEX.dex_MontoInicial,
DEX.dex_MontoRestante,
DEX.dex_ObservacionesComentarios,
DE.cde_IdDeducciones,
DE.cde_DescripcionDeduccion,
DEX.dex_Cuota,
DEX.dex_UsuarioCrea,
DEX.dex_FechaCrea,
DEX.dex_UsuarioModifica,
DEX.dex_FechaModifica,
DEX.dex_Activo
FROM [rrhh].[tbEquipoTrabajo] E
INNER JOIN [rrhh].[tbEquipoEmpleados] EE ON E.eqtra_Id = EE.eqtra_Id
INNER JOIN [Plani].[tbDeduccionesExtraordinarias] DEX ON DEX.eqem_Id = EE.eqem_Id
INNER JOIN Plani.tbCatalogoDeDeducciones DE ON DEX.cde_IdDeducciones = DE.cde_IdDeducciones
GO
/****** Object:  Table [rrhh].[tbHistorialSalidas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialSalidas](
	[hsal_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[tsal_Id] [int] NOT NULL,
	[rsal_Id] [int] NOT NULL,
	[hsal_FechaSalida] [date] NOT NULL,
	[hsal_Observacion] [nvarchar](25) NULL,
	[hsal_Estado] [bit] NOT NULL,
	[hsal_RazonInactivo] [nvarchar](100) NULL,
	[hsal_UsuarioCrea] [int] NOT NULL,
	[hsal_FechaCrea] [datetime] NOT NULL,
	[hsal_UsuarioModifica] [int] NULL,
	[hsal_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialSalidas_hsal_Id] PRIMARY KEY CLUSTERED 
(
	[hsal_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_tbHistorialSalidas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [rrhh].[V_tbHistorialSalidas]
as
select 
* 
from rrhh.tbHistorialSalidas hsal
where hsal_Estado = 1
GO
/****** Object:  View [Plani].[V_DeduccionesInstitucionesFinancierasColaboradres]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DeduccionesInstitucionesFinancierasColaboradres] AS
SELECT 
DIF.deif_IdDeduccionInstFinanciera,
DIF.emp_Id,
DIF.deif_Monto,
DIF.deif_Comentarios,
DIF.deif_UsuarioCrea,
DIF.deif_FechaCrea,
DIF.deif_UsuarioModifica,
DIF.deif_FechaModifica,
DIF.deif_Activo,
INS.insf_IdInstitucionFinanciera,
INS.insf_DescInstitucionFinanc,
DE.cde_DescripcionDeduccion
FROM [Plani].[tbDeduccionInstitucionFinanciera]  DIF
INNER JOIN [Plani].[tbInstitucionesFinancieras] INS ON DIF.insf_IdInstitucionFinanciera = INS.insf_IdInstitucionFinanciera
INNER JOIN Plani.tbCatalogoDeDeducciones DE ON DE.cde_IdDeducciones = DIF.cde_IdDeducciones
--WHERE DIF.emp_Id = 1
GO
/****** Object:  View [Plani].[V_Plani_FechaPlani]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_Plani_FechaPlani]
AS
SELECT 
HistorialPago.hipa_FechaPago
FROM 
[Plani].[tbHistorialDePago] AS HistorialPago
GROUP BY HistorialPago.hipa_FechaPago
GO
/****** Object:  Table [Plani].[tbHistorialLiquidaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbHistorialLiquidaciones](
	[hliq_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[hliq_fechaIngreso] [datetime] NOT NULL,
	[hliq_fechaLiquidacion] [datetime] NOT NULL,
	[hliq_PorcentajeLiquidacion] [int] NOT NULL,
	[hliq_Observaciones] [nvarchar](25) NULL,
	[hliq_Estado] [bit] NOT NULL,
	[hliq_RazonInactivo] [nvarchar](100) NULL,
	[hliq_UsuarioCrea] [int] NOT NULL,
	[hliq_FechaCrea] [datetime] NOT NULL,
	[hliq_UsuarioModifica] [int] NULL,
	[hliq_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialLiquidaciones_hliq_Id] PRIMARY KEY CLUSTERED 
(
	[hliq_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_Liquidaciones_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_Liquidaciones_RPT]
AS
SELECT emp.emp_Id,
		per.per_Identidad,
		per.per_Nombres + ' ' + per.per_Apellidos  AS [NombreCompleto],
		depto.depto_descripcion,
		are.area_Descripcion,
		cp.cpla_IdPlanilla,
		cp.cpla_DescripcionPlanilla,
		hliq.hliq_fechaIngreso,
		hliq.hliq_fechaLiquidacion,
		hliq.hliq_PorcentajeLiquidacion,
		hliq_Observaciones
FROM [rrhh].[tbEmpleados] emp 
INNER JOIN [rrhh].[tbPersonas] per ON emp.per_Id = per.per_Id
INNER JOIN [rrhh].[tbAreas] are ON emp.area_Id = are.area_Id
INNER JOIN [rrhh].[tbDepartamentos] depto ON emp.depto_Id = depto.depto_Id
INNER JOIN [Plani].[tbHistorialLiquidaciones] AS hliq ON emp.emp_Id = hliq.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
GO
/****** Object:  Table [rrhh].[tbHorarios]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHorarios](
	[hor_Id] [int] NOT NULL,
	[jor_Id] [int] NOT NULL,
	[hor_Descripcion] [nvarchar](50) NULL,
	[hor_HoraInicio] [time](7) NOT NULL,
	[hor_HoraFin] [time](7) NOT NULL,
	[hor_CantidadHoras] [time](7) NOT NULL,
	[hor_Estado] [bit] NOT NULL,
	[hor_RazonInactivo] [nvarchar](100) NULL,
	[hor_UsuarioCrea] [int] NOT NULL,
	[hor_FechaCrea] [datetime] NOT NULL,
	[hor_UsuarioModifica] [int] NULL,
	[hor_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHorarios_hor_Id] PRIMARY KEY CLUSTERED 
(
	[hor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HorariosDetalles]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_HorariosDetalles]
  AS
SELECT h.hor_Id
      ,h.jor_Id
      ,h.hor_Descripcion
      ,CAST(h.hor_HoraInicio AS NVARCHAR(5)) AS hor_HoraInicio
      ,CAST(h.hor_HoraFin AS NVARCHAR(5)) AS hor_HoraFin
      ,CAST(h.hor_CantidadHoras AS NVARCHAR(5)) AS hor_CantidadHoras
      ,h.hor_Estado
      ,h.hor_RazonInactivo
      ,h.hor_UsuarioCrea
      ,h.hor_FechaCrea
      ,h.hor_UsuarioModifica
      ,h.hor_FechaModifica
  FROM rrhh.tbHorarios h 
  INNER JOIN rrhh.tbJornadas j ON j.jor_Id = h.jor_Id
GO
/****** Object:  Table [Plani].[tbEmpleadoBonos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbEmpleadoBonos](
	[cb_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[cin_IdIngreso] [int] NOT NULL,
	[cb_Monto] [decimal](16, 4) NULL,
	[cb_FechaRegistro] [datetime] NOT NULL,
	[cb_Pagado] [bit] NOT NULL,
	[cb_UsuarioCrea] [int] NOT NULL,
	[cb_FechaCrea] [datetime] NOT NULL,
	[cb_UsuarioModifica] [int] NULL,
	[cb_FechaModifica] [datetime] NULL,
	[cb_Activo] [bit] NOT NULL,
	[cb_FechaPagado] [datetime] NULL,
 CONSTRAINT [PK_Plani_tbEmpleadoBonos_cb_Id] PRIMARY KEY CLUSTERED 
(
	[cb_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_BonosColaborador]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_BonosColaborador] AS
SELECT 
EB.cb_Id,
EB.emp_Id,
EB.cb_Monto,
EB.cb_FechaRegistro,
EB.cb_Pagado,
EB.cb_UsuarioCrea,
EB.cb_FechaCrea,
EB.cb_UsuarioModifica,
EB.cb_FechaModifica,
EB.cb_Activo,
CPLA.cin_IdIngreso,
CPLA.cin_DescripcionIngreso
FROM [Plani].[tbEmpleadoBonos] EB
INNER JOIN [Plani].[tbCatalogoDeIngresos] CPLA ON EB.cin_IdIngreso = CPLA.cin_IdIngreso
-- WHERE EB.emp_Id = 1 AND EB.cb_Pagado = 0 AND EB.cb_Activo = 1

GO
/****** Object:  Table [Plani].[tbEmpleadoComisiones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbEmpleadoComisiones](
	[cc_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[cin_IdIngreso] [int] NOT NULL,
	[cc_FechaRegistro] [datetime] NOT NULL,
	[cc_Pagado] [bit] NOT NULL,
	[cc_UsuarioCrea] [int] NOT NULL,
	[cc_FechaCrea] [datetime] NOT NULL,
	[cc_UsuarioModifica] [int] NULL,
	[cc_FechaModifica] [datetime] NULL,
	[cc_Activo] [bit] NOT NULL,
	[cc_PorcentajeComision] [decimal](16, 4) NOT NULL,
	[cc_TotalVenta] [decimal](16, 4) NOT NULL,
	[cc_FechaPagado] [datetime] NULL,
 CONSTRAINT [PK_Plani_tbEmpleadoComisiones_cc_Id] PRIMARY KEY CLUSTERED 
(
	[cc_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_ComisionesColaborador]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Plani].[V_ComisionesColaborador] AS
SELECT 
EC.cc_Id,
EC.emp_Id,
(EC.cc_TotalVenta*EC.cc_PorcentajeComision)/100 AS cc_Monto,
EC.cc_FechaRegistro,
EC.cc_Pagado,
EC.cc_UsuarioCrea,
EC.cc_FechaCrea,
EC.cc_UsuarioModifica,
EC.cc_FechaModifica,
EC.cc_Activo,
CPLA.cin_IdIngreso,
CPLA.cin_DescripcionIngreso
FROM [Plani].[tbEmpleadoComisiones] EC
INNER JOIN [Plani].[tbCatalogoDeIngresos] CPLA ON EC.cin_IdIngreso = CPLA.cin_IdIngreso
--WHERE cc_Pagado = 0 AND emp_Id = 1
GO
/****** Object:  Table [rrhh].[tbTipoSalidas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoSalidas](
	[tsal_Id] [int] NOT NULL,
	[tsal_Descripcion] [nvarchar](50) NULL,
	[tsal_Estado] [bit] NOT NULL,
	[tsal_RazonInactivo] [nvarchar](100) NULL,
	[tsal_UsuarioCrea] [int] NOT NULL,
	[tsal_FechaCrea] [datetime] NOT NULL,
	[tsal_UsuarioModifica] [int] NULL,
	[tsal_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoSalidas_tsal_Id] PRIMARY KEY CLUSTERED 
(
	[tsal_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbRazonSalidas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbRazonSalidas](
	[rsal_Id] [int] NOT NULL,
	[rsal_Descripcion] [nvarchar](50) NOT NULL,
	[rsal_Estado] [bit] NOT NULL,
	[rsal_RazonInactivo] [nvarchar](100) NULL,
	[rsal_UsuarioCrea] [int] NOT NULL,
	[rsal_FechaCrea] [datetime] NOT NULL,
	[rsal_UsuarioModifica] [int] NULL,
	[rsal_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK__tbRazonSalidas_rsal_Id] PRIMARY KEY CLUSTERED 
(
	[rsal_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_tbHistorialSalidas_completa]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select * from [rrhh].[tbEmpleados]
select * from [rrhh].[tbPersonas]
select * from [rrhh].[tbTipoSalidas]
select * from [rrhh].[tbRazonSalidas]
*/
CREATE VIEW [rrhh].[V_tbHistorialSalidas_completa]
AS
select tsal.tsal_Descripcion,rsal.rsal_Descripcion,per.*,hsal.*
from [rrhh].[tbHistorialSalidas] hsal
Inner join rrhh.tbEmpleados AS emp on hsal.emp_Id = emp.emp_Id
Inner join rrhh.tbPersonas AS per on emp.per_Id = per.per_Id
Inner join rrhh.tbTipoSalidas as tsal on hsal.tsal_Id = tsal.tsal_Id
Inner join rrhh.tbRazonSalidas as rsal on hsal.rsal_Id = rsal.rsal_Id
GO
/****** Object:  View [rrhh].[V_tbtiposalidas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [rrhh].[V_tbtiposalidas]
as
select 
*
from rrhh.tbtiposalidas
where tsal_Estado = 1
GO
/****** Object:  View [rrhh].[V_EmpleadoAmonestaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [rrhh].[V_EmpleadoAmonestaciones]
as
Select Empleados.emp_Id,
Personas.per_Nombres +' '+Personas.per_Apellidos as [emp_NombreCompleto],
Cargos.car_Id,
Cargos.car_Descripcion,
Depto.depto_Id,
Depto.depto_Descripcion,
Empleados.emp_Estado
from [ERP_GMEDINA].[rrhh].[tbEmpleados] as Empleados
inner join  [ERP_GMEDINA].rrhh.tbPersonas as Personas
on Empleados.per_Id = Personas.per_Id inner join [ERP_GMEDINA].rrhh.tbCargos as Cargos
on Empleados.car_Id = Cargos.car_Id inner join [ERP_GMEDINA].rrhh.tbDepartamentos as Depto
on Empleados.depto_Id = Depto.depto_Id
GO
/****** Object:  Table [rrhh].[tbTipoMonedas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoMonedas](
	[tmon_Id] [int] NOT NULL,
	[tmon_Descripcion] [nvarchar](25) NOT NULL,
	[tmon_Estado] [bit] NOT NULL,
	[tmon_RazonInactivo] [nvarchar](100) NULL,
	[tmon_UsuarioCrea] [int] NOT NULL,
	[tmon_FechaCrea] [datetime] NOT NULL,
	[tmon_UsuarioModifica] [int] NULL,
	[tmon_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoMonedas_tmon_Id] PRIMARY KEY CLUSTERED 
(
	[tmon_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_PreviewPlanilla]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_PreviewPlanilla] AS
SELECT 
EMP.emp_Id,
(PER.per_Nombres+' '+PER.per_Apellidos) AS Nombres,
PER.per_Identidad,
PER.per_Sexo,
PER.per_Edad,
PER.per_Direccion,
PER.per_Telefono,
PER.per_CorreoElectronico,
PER.per_EstadoCivil,
SUE.sue_Cantidad AS salarioBase,
TMON.tmon_Id,
TMON.tmon_Descripcion,
CPLA.cpla_IdPlanilla,
CPLA.cpla_DescripcionPlanilla
FROM [rrhh].[tbEmpleados] EMP
INNER JOIN [rrhh].[tbPersonas] PER ON EMP.per_Id = PER.per_Id
INNER JOIN [rrhh].[tbSueldos] SUE ON EMP.emp_Id = SUE.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas] CPLA ON EMP.cpla_IdPlanilla = CPLA.cpla_IdPlanilla
INNER JOIN [rrhh].[tbTipoMonedas] TMON ON SUE.tmon_Id = TMON.tmon_Id
WHERE sue_SueldoAnterior IS NULL
GO
/****** Object:  View [Plani].[V_CatalogoDeIngresos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_CatalogoDeIngresos]
AS
SELECT  
        cin.cin_IdIngreso, 
		cin.cin_DescripcionIngreso , 
		cin.cin_UsuarioCrea ,
		cin.cin_FechaCrea ,
		cin.cin_UsuarioModifica ,
		cin.cin_FechaModifica ,
		cin.cin_Activo 
FROM [Plani].[tbCatalogoDeIngresos] AS cin 
WHERE cin.cin_Activo = 1
GO
/****** Object:  View [Plani].[V_DecimoTercerMes]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DecimoTercerMes]
AS
SELECT        
HP.emp_Id, 
P.per_Nombres, 
P.per_Apellidos, 
C.car_Descripcion, 
CP.cpla_DescripcionPlanilla, 
E.emp_CuentaBancaria,
SUM(HP.hipa_SueldoNeto) / 360 * 30 AS dtm_Monto
FROM            Plani.tbHistorialDePago AS HP INNER JOIN
                         Rrhh.tbPersonas AS P ON HP.emp_Id = P.per_Id INNER JOIN
                         Rrhh.tbEmpleados AS E ON E.emp_Id = P.per_Id INNER JOIN
                         Rrhh.tbCargos AS C ON C.car_Id = E.car_Id INNER JOIN
                         Plani.tbCatalogoDePlanillas AS CP ON CP.cpla_IdPlanilla = E.cpla_IdPlanilla LEFT JOIN
						 Plani.tbDecimoTercerMes AS DTM ON DTM.emp_Id = E.emp_Id
WHERE        (HP.hipa_Anio = YEAR(GETDATE())) AND (CP.cpla_IdPlanilla <> 2)
GROUP BY HP.emp_Id, P.per_Nombres, P.per_Apellidos, C.car_Descripcion, CP.cpla_DescripcionPlanilla, E.emp_CuentaBancaria
GO
/****** Object:  View [rrhh].[V_EmpleadoIncapacidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [rrhh].[V_EmpleadoIncapacidades]
as
Select Empleados.emp_Id,
Personas.per_Nombres +' '+Personas.per_Apellidos as [emp_NombreCompleto],
Cargos.car_Id,
Cargos.car_Descripcion,
Depto.depto_Id,
Depto.depto_Descripcion,
Empleados.emp_Estado
from [ERP_GMEDINA].[rrhh].[tbEmpleados] as Empleados
inner join  [ERP_GMEDINA].rrhh.tbPersonas as Personas
on Empleados.per_Id = Personas.per_Id inner join [ERP_GMEDINA].rrhh.tbCargos as Cargos
on Empleados.car_Id = Cargos.car_Id inner join [ERP_GMEDINA].rrhh.tbDepartamentos as Depto
on Empleados.depto_Id = Depto.depto_Id
GO
/****** Object:  Table [rrhh].[tbTipoIncapacidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoIncapacidades](
	[ticn_Id] [int] NOT NULL,
	[ticn_Descripcion] [nvarchar](25) NOT NULL,
	[ticn_Estado] [bit] NOT NULL,
	[ticn_RazonInactivo] [nvarchar](100) NULL,
	[ticn_UsuarioCrea] [int] NOT NULL,
	[ticn_FechaCrea] [datetime] NOT NULL,
	[ticn_UsuarioModifica] [int] NULL,
	[ticn_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoIncapacidades_ticn_Id] PRIMARY KEY CLUSTERED 
(
	[ticn_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialIncapacidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialIncapacidades](
	[hinc_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[ticn_Id] [int] NOT NULL,
	[hinc_Dias] [int] NOT NULL,
	[hinc_CentroMedico] [nvarchar](100) NOT NULL,
	[hinc_Doctor] [nvarchar](50) NOT NULL,
	[hinc_Diagnostico] [nvarchar](150) NULL,
	[hinc_FechaInicio] [datetime] NULL,
	[hinc_FechaFin] [datetime] NULL,
	[hinc_Estado] [bit] NOT NULL,
	[hinc_RazonInactivo] [nvarchar](100) NULL,
	[hinc_UsuarioCrea] [int] NOT NULL,
	[hinc_FechaCrea] [datetime] NOT NULL,
	[hinc_UsuarioModifica] [int] NULL,
	[hinc_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialincapacidades_hinc_Id] PRIMARY KEY CLUSTERED 
(
	[hinc_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialIncapacidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [rrhh].[V_HistorialIncapacidades]
as
select emp.emp_Id, hinc_Id,per.per_Nombres +' '+ per.per_Apellidos as [NombreCompleto],
	   tinc.ticn_Id,tinc.ticn_Descripcion,hinc.hinc_Dias,hinc.hinc_CentroMedico,hinc.hinc_Doctor,
	   hinc.hinc_Diagnostico,cast(hinc.hinc_FechaInicio as date) as hinc_FechaInicio , cast(hinc.hinc_FechaFin as date) as hinc_FechaFin ,hinc.hinc_Estado,hinc.hinc_RazonInactivo
	   from [rrhh].[tbHistorialIncapacidades] as hinc
	   inner join [rrhh].[tbEmpleados] emp on hinc.emp_Id=emp.emp_Id
	   inner join [rrhh].[tbPersonas] per on emp.per_Id=per.per_Id
	   inner join [rrhh].[tbTipoIncapacidades] tinc on hinc.ticn_Id=tinc.ticn_Id
GO
/****** Object:  View [rrhh].[V_Datos_Empleado]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [rrhh].[V_Datos_Empleado]
            --WITH ENCRYPTION, SCHEMABINDING, VIEW_METADATA
            AS
select EMP.[emp_Id],
a.area_Id,
a.area_Descripcion,
C.car_Id,
C.car_Descripcion,
D.depto_Id,
D.depto_Descripcion,
J.jor_Descripcion
from [rrhh].[tbEmpleados] as EMP
inner join [rrhh].[tbCargos] as C on c.car_Id=EMP.car_Id
inner join [rrhh].[tbAreas] as A on A.area_Id=EMP.area_Id
Inner join [rrhh].[tbDepartamentos] as D on D.depto_Id=EMP.depto_Id
inner join [rrhh].[tbJornadas] as J on J.jor_Id=EMP.jor_Id

GO
/****** Object:  View [Plani].[V_ColaboradoresPorPlanilla]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [Plani].[V_ColaboradoresPorPlanilla] as
SELECT 
CPLA.cpla_IdPlanilla,
CPLA.cpla_DescripcionPlanilla,
COUNT(E.emp_Id) AS CantidadColaboradores

FROM [Plani].[tbCatalogoDePlanillas] CPLA
LEFT JOIN [Rrhh].[tbEmpleados] E ON CPLA.cpla_IdPlanilla = E.cpla_IdPlanilla
GROUP BY CPLA.cpla_IdPlanilla,CPLA.cpla_DescripcionPlanilla
GO
/****** Object:  View [Plani].[V_FormaDePago]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_FormaDePago]
AS
SELECT [fpa_IdFormaPago]
      ,[fpa_Descripcion]
      ,[fpa_UsuarioCrea]
      ,[fpa_FechaCrea]
      ,[fpa_UsuarioModifica]
      ,[fpa_FechaModifica]
      ,[fpa_Activo]
  FROM [Plani].[tbFormaPago] 
GO
/****** Object:  View [Plani].[V_TipoDeduccion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_TipoDeduccion]
AS
SELECT 
tde_IdTipoDedu, 
tde_Descripcion, 
tde_UsuarioCrea, 
tde_FechaCrea, 
tde_UsuarioModifica, 
tde_FechaModifica, 
tde_Activo
FROM [Plani].[tbTipoDeduccion]
GO
/****** Object:  Table [Plani].[tbDecimoCuartoMes]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbDecimoCuartoMes](
	[dcm_IdDecimoCuartoMes] [int] NOT NULL,
	[dcm_FechaPago] [date] NOT NULL,
	[dcm_UsuarioCrea] [int] NOT NULL,
	[dcm_FechaCrea] [datetime] NOT NULL,
	[dcm_UsuarioModifica] [int] NULL,
	[dcm_FechaModifica] [datetime] NULL,
	[emp_Id] [int] NOT NULL,
	[dcm_Monto] [decimal](16, 4) NULL,
	[dcm_CodigoPago] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Plani_tbDecimoCuartoMes_dcm_IdDecimoCuartoMes] PRIMARY KEY CLUSTERED 
(
	[dcm_IdDecimoCuartoMes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [Plani].[V_DecimoCuartoMes_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DecimoCuartoMes_RPT]
AS
	SELECT 
	dt.dcm_IdDecimoCuartoMes, 
	dt.emp_Id,
	p.[per_Nombres],
	p.[per_Apellidos],
	dt.dcm_FechaPago, 	
	dt.dcm_Monto, 
	e.emp_CuentaBancaria,
	dt.dcm_CodigoPago,
	cp.cpla_IdPlanilla,
	cp.[cpla_DescripcionPlanilla]
	FROM [Plani].[tbDecimoCuartoMes] dt
	INNER JOIN [rrhh].[tbPersonas] p ON dt.emp_Id = p.[per_Id]
	INNER JOIN [rrhh].[tbEmpleados] e ON e.emp_Id = dt.emp_Id
	INNER JOIN [Plani].[tbCatalogoDePlanillas] cp ON cp.[cpla_IdPlanilla] = e.[cpla_IdPlanilla]
GO
/****** Object:  Table [rrhh].[tbRequisiciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbRequisiciones](
	[req_Id] [int] NOT NULL,
	[req_Experiencia] [nvarchar](100) NULL,
	[req_Sexo] [char](10) NULL,
	[req_Descripcion] [nvarchar](50) NOT NULL,
	[req_EdadMinima] [int] NOT NULL,
	[req_EdadMaxima] [int] NOT NULL,
	[req_EstadoCivil] [char](2) NOT NULL,
	[req_EducacionSuperior] [bit] NOT NULL,
	[req_Permanente] [bit] NOT NULL,
	[req_Duracion] [nvarchar](50) NULL,
	[req_Estado] [bit] NOT NULL,
	[req_RazonInactivo] [nvarchar](100) NULL,
	[req_Vacantes] [nvarchar](50) NOT NULL,
	[req_FechaRequisicion] [datetime] NULL,
	[req_FechaContratacion] [datetime] NULL,
	[req_UsuarioCrea] [int] NOT NULL,
	[req_FechaCrea] [datetime] NOT NULL,
	[req_UsuarioModifica] [int] NULL,
	[req_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbRequisiciones_req_Id] PRIMARY KEY CLUSTERED 
(
	[req_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbFasesReclutamiento]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbFasesReclutamiento](
	[fare_Id] [int] NOT NULL,
	[fare_Descripcion] [nvarchar](50) NOT NULL,
	[fare_Estado] [bit] NOT NULL,
	[fare_RazonInactivo] [nvarchar](100) NULL,
	[fare_UsuarioCrea] [int] NOT NULL,
	[fare_FechaCrea] [datetime] NOT NULL,
	[fare_UsuarioModifica] [int] NULL,
	[fare_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbFasesReclutamiento_fare_Id] PRIMARY KEY CLUSTERED 
(
	[fare_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_SeleccionCandidatos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_SeleccionCandidatos]
AS
SELECT 
SC.scan_Id AS Id,
P.per_Identidad AS Identidad,
P.per_Nombres + ' '+P.per_Apellidos AS Nombre,
FR.fare_Descripcion AS Fase,
R.req_Descripcion AS Plaza_Disponible,
CAST(SC.scan_Fecha AS DATE) AS Fecha
FROM [rrhh].[tbSeleccionCandidatos] AS SC
INNER JOIN [rrhh].[tbPersonas] AS P
ON P.per_Id = SC.per_Id
INNER JOIN [rrhh].[tbFasesReclutamiento] AS FR
ON FR.fare_Id = SC.fare_Id
INNER JOIN [rrhh].[tbRequisiciones] AS R
ON R.req_Id = SC.rper_Id
GO
/****** Object:  View [Plani].[V_DeduccionesExtraordinarias]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DeduccionesExtraordinarias]
AS
SELECT
CONCAT(rhp.per_Nombres, ' ', rhp.per_Apellidos) AS per_DeduccionEmpleado,
rhc.car_Descripcion,
pde.dex_ObservacionesComentarios,
pde.dex_MontoInicial,
pde.dex_MontoRestante,
pde.dex_Cuota,
pcd.cde_DescripcionDeduccion
FROM [Plani].[tbDeduccionesExtraordinarias] AS pde
INNER JOIN [Plani].[tbCatalogoDeDeducciones] AS pcd ON pcd.cde_IdDeducciones = pde.cde_IdDeducciones --Relación de Deducciones Extraordinarias con el Catalogo de Deducciones
INNER JOIN [rrhh].[tbEquipoEmpleados] AS rhee ON rhee.eqem_Id = pde.eqem_Id							 --Relación de Deducciones Extraordinarias con el Equipo Empleado 
INNER JOIN [rrhh].[tbEquipoTrabajo] AS rhet ON rhet.eqtra_Id = rhee.eqtra_Id						 --Relación de Equipo Empleado con el Equipo Trabajo
INNER JOIN [rrhh].[tbEmpleados] AS rhe ON rhe.emp_Id = rhee.emp_Id									 --Relación de Equipo Empleado con Empleados
INNER JOIN [rrhh].[tbPersonas] AS rhp ON rhp.per_Id = rhe.per_Id									 --Relación de Empleados con Personas
INNER JOIN [rrhh].[tbCargos] AS rhc ON rhc.car_Id = rhe.car_Id										 --Relación de Empleados con Cargos
WHERE pde.dex_Activo = 1
GO
/****** Object:  View [Plani].[V_tbEmpleadoComisiones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [Plani].[V_tbEmpleadoComisiones]
as 
SELECT
emp.emp_Id,
per.per_Identidad,
per.per_Nombres + ' ' + per.per_Apellidos as 'Nombre Completo',
Car.car_Descripcion,
Depto.depto_Descripcion,
Are.area_Descripcion,
Jor.jor_Descripcion,
Plani.cpla_DescripcionPlanilla
FROM [rrhh].[tbEmpleados] AS Emp
INNER JOIN  [rrhh].[tbPersonas] AS Per  on Emp.per_Id = Per.per_Id
INNER JOIN  [rrhh].[tbAreas] AS Are on emp.area_Id = Are.area_Id
INNER JOIN  [rrhh].[tbDepartamentos] AS Depto on emp.depto_Id =depto.depto_Id
INNER JOIN  [rrhh].[tbCargos] AS Car on emp.car_Id = Car.car_Id
INNER JOIN  [rrhh].[tbJornadas] AS Jor on Emp.jor_Id = Jor.Jor_Id
INNER JOIN   [Plani].[tbCatalogoDePlanillas] AS Plani  On Emp.cpla_IdPlanilla = plani.cpla_IdPlanilla
Where car.car_Descripcion = 'Vendedor' AND Emp.emp_Estado  = 1 
GO
/****** Object:  View [Plani].[V_tbCatalogoDeIngresos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_tbCatalogoDeIngresos]
AS
SELECT  
        cin.cin_IdIngreso, 
		cin.cin_DescripcionIngreso , 
		cin.cin_UsuarioCrea ,
		cin.cin_FechaCrea ,
		cin.cin_UsuarioModifica ,
		cin.cin_FechaModifica ,
		cin.cin_Activo 
FROM [Plani].[tbCatalogoDeIngresos] AS cin 
WHERE cin.cin_Activo = 1
GO
/****** Object:  View [Plani].[V_DecimoTercerMes_Pagados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_DecimoTercerMes_Pagados]
AS
SELECT 
DTP.emp_Id,
P.per_Nombres, 
P.per_Apellidos,
C.car_Descripcion,
CP.cpla_DescripcionPlanilla,
E.emp_CuentaBancaria,
DTP.dtm_Monto
FROM 
[Plani].[tbDecimoTercerMes] DTP INNER JOIN
Rrhh.tbPersonas AS P ON DTP.emp_Id = P.per_Id INNER JOIN 
Rrhh.tbEmpleados AS E ON E.emp_Id = P.per_Id INNER JOIN
Rrhh.tbCargos AS C ON C.car_Id = E.car_Id INNER JOIN
Plani.tbCatalogoDePlanillas AS CP ON CP.cpla_IdPlanilla = E.cpla_IdPlanilla
GO
/****** Object:  Table [rrhh].[tbHistorialCargos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialCargos](
	[hcar_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[car_IdAnterior] [int] NULL,
	[car_IdNuevo] [int] NOT NULL,
	[hcar_Fecha] [datetime] NULL,
	[hcar_Estado] [bit] NOT NULL,
	[hcar_RazonInactivo] [nvarchar](100) NULL,
	[hcar_UsuarioCrea] [int] NOT NULL,
	[hcar_FechaCrea] [datetime] NOT NULL,
	[hcar_UsuarioModifica] [int] NULL,
	[hcar_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialCargos_hcar_Id] PRIMARY KEY CLUSTERED 
(
	[hcar_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [rrhh].[V_HistorialCargos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [rrhh].[V_HistorialCargos]
	AS
	SELECT 	HCARGOS.[hcar_Id],
			PERSONAS.[per_Nombres] + ' '+ PERSONAS.per_Apellidos AS [Nombre Completo],
		    CARGOSA.[car_Descripcion] AS CargoAnterior,
			CARGOSN.[car_Descripcion] AS CargoNuevo,
			HCARGOS.hcar_Fecha AS [Fecha de Historial],
			USUARIOC.[usu_NombreUsuario] AS [Usuario Crea],
			HCARGOS.[hcar_FechaCrea] AS [Fecha Crea],
			USUARIOM.[usu_NombreUsuario] AS [Usuario Modifica],
			HCARGOS.hcar_FechaModifica AS [Fecha Modifica]		
	FROM    [rrhh].[tbHistorialCargos] AS HCARGOS 
			INNER JOIN  [RRHH].[tbCargos]   AS CARGOSA    ON HCARGOS.[car_IdAnterior]  = CARGOSA.[car_Id] 
			LEFT JOIN [rrhh].[tbCargos] AS CARGOSN ON HCARGOS.[car_IdNuevo]  = CARGOSN.[car_Id] 
			INNER JOIN [RRHH].[tbEmpleados] AS EMPLEADOS ON HCARGOS.[emp_Id] = EMPLEADOS.[emp_Id]
			INNER JOIN [rrhh].[tbPersonas] AS PERSONAS ON EMPLEADOS.[per_Id] = PERSONAS.[per_Id]
			INNER JOIN [Acce].[tbUsuario] AS USUARIOC ON HCARGOS.[hcar_UsuarioCrea] = USUARIOC.[usu_Id]
			LEFT JOIN  [Acce].[tbUsuario] AS USUARIOM ON HCARGOS.hcar_UsuarioModifica = USUARIOM.usu_Id
GO
/****** Object:  View [Plani].[V_CatalogoDePlanillasConIngresosYDeducciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_CatalogoDePlanillasConIngresosYDeducciones]
(
	idPlanilla,
	descripcionPlanilla,
	frecuenciaEnDias,
	idIngreso,
	descripcionIngreso,
	idDeducciones,
	descripcionDeduccion
)

AS
/****************************************************************************************************
 Seleccionar todos los catalogos de planillas unidos con los ingresos y deducciones que estas tienen.
****************************************************************************************************/

SELECT tcdp.cpla_IdPlanilla, 
       tcdp.cpla_DescripcionPlanilla, 
       tcdp.cpla_FrecuenciaEnDias, 
       tcdi.cin_IdIngreso, 
       tcdi.cin_DescripcionIngreso, 
       tcdd.cde_IdDeducciones, 
       tcdd.cde_DescripcionDeduccion
FROM Plani.tbCatalogoDePlanillas tcdp

/****************************************************************
Unir tipo de planilla detalle deduccion y filtrar que este activo
****************************************************************/

     OUTER APPLY
(
    SELECT ttpdd.tpdd_IdPlanillaDetDeduccion, 
           ttpdd.cpla_IdPlanilla, 
           ttpdd.cde_IdDeducciones, 
           ttpdd.tpdd_UsuarioCrea, 
           ttpdd.tpdd_FechaCrea, 
           ttpdd.tpdd_UsuarioModifica, 
           ttpdd.tpdd_FechaModifica, 
           ttpdd.tpdd_Activo
    FROM Plani.tbTipoPlanillaDetalleDeduccion ttpdd
    WHERE ttpdd.tpdd_Activo = 1
          AND ttpdd.cpla_IdPlanilla = tcdp.cpla_IdPlanilla
) AS ttpdd

/************************************************************************************
Unir la tabla Plani.tbTipoPlanillaDetalleIngreso filtrando solo los que estan activos
************************************************************************************/

     OUTER APPLY
(
    SELECT ttpdi.tpdi_IdDetallePlanillaIngreso, 
           ttpdi.cin_IdIngreso, 
           ttpdi.cpla_IdPlanilla, 
           ttpdi.tpdi_UsuarioCrea, 
           ttpdi.tpdi_FechaCrea, 
           ttpdi.tpdi_UsuarioModifica, 
           ttpdi.tpdi_FechaModifica, 
           ttpdi.tpdi_Activo
    FROM Plani.tbTipoPlanillaDetalleIngreso ttpdi
    WHERE ttpdi.tpdi_Activo = 1
          AND ttpdi.cpla_IdPlanilla = tcdp.cpla_IdPlanilla
) AS ttpdi

/*********************************************************************
Unir la tabla Plani.tbCatalogoDeDeducciones y filtrarlo si esta activo
*********************************************************************/

     OUTER APPLY
(
    SELECT tcdd.cde_IdDeducciones, 
           tcdd.cde_DescripcionDeduccion, 
           tcdd.tde_IdTipoDedu, 
           tcdd.cde_PorcentajeColaborador, 
           tcdd.cde_PorcentajeEmpresa, 
           tcdd.cde_UsuarioCrea, 
           tcdd.cde_FechaCrea, 
           tcdd.cde_UsuarioModifica, 
           tcdd.cde_FechaModifica, 
           tcdd.cde_Activo
    FROM Plani.tbCatalogoDeDeducciones tcdd
    WHERE tcdd.cde_Activo = 1
          AND tcdd.cde_IdDeducciones = ttpdd.cde_IdDeducciones
) AS tcdd

/*****************************************************************
Unir catalogo de ingresos y filtrar si estan activos los registros
*****************************************************************/

     OUTER APPLY
(
    SELECT tcdi.cin_IdIngreso, 
           tcdi.cin_DescripcionIngreso, 
           tcdi.cin_UsuarioCrea, 
           tcdi.cin_FechaCrea, 
           tcdi.cin_UsuarioModifica, 
           tcdi.cin_FechaModifica, 
           tcdi.cin_Activo
    FROM Plani.tbCatalogoDeIngresos tcdi
    WHERE tcdi.cin_Activo = 1
          AND ttpdi.cin_IdIngreso = tcdi.cin_IdIngreso
) AS tcdi
WHERE tcdp.cpla_Activo = 1;
GO
/****** Object:  View [Plani].[V_IHSS_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_IHSS_RPT]
AS
SELECT
emp.emp_Id,
per.per_Identidad,
per.per_Nombres + ' ' + per.per_Apellidos AS [NombreCompleto],
depto.depto_descripcion,
are.area_Descripcion,
cp.cpla_IdPlanilla,
cp.cpla_DescripcionPlanilla,
cdd.cde_IdDeducciones,
cdd.cde_DescripcionDeduccion,
dp.hidp_Total,
hp.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp inner join [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
INNER JOIN [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
INNER JOIN [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
INNER JOIN [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
INNER JOIN [Plani].[tbHistorialDeduccionPago] as dp on  dp.hipa_IdHistorialDePago = hp.hipa_IdHistorialDePago
INNER JOIN [Plani].[tbTipoPlanillaDetalleDeduccion] tpdd on dp.tpdd_IdPlanillaDetDeduccion = tpdd.tpdd_IdPlanillaDetDeduccion
INNER JOIN [Plani].[tbCatalogoDeDeducciones] cdd on cdd.cde_IdDeducciones  = tpdd.cde_IdDeducciones
INNER JOIN [Plani].[tbTipoDeduccion] as td on cdd.tde_IdTipoDedu = td.tde_IdTipoDedu
WHERE  td.tde_Descripcion= 'Normal' and cdd.cde_DescripcionDeduccion = 'IHSS'
GO
/****** Object:  View [Plani].[V_EmpleadoBonos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Plani].[V_EmpleadoBonos]
AS
SELECT 
	cb.cb_Id,
	cb.emp_Id,
	(p.per_Nombres + ' ' + p.per_Apellidos) AS per_Nombres,
	cb.cin_IdIngreso,
	ci.cin_DescripcionIngreso,
	cb.cb_Monto,
	cb.cb_FechaRegistro,
	cb.cb_Pagado,
	cb.cb_UsuarioCrea,
	cb.cb_FechaCrea, 
	cb.cb_UsuarioModifica, 
	cb.cb_FechaModifica
FROM [Plani].[tbEmpleadoBonos] AS cb
INNER JOIN [rrhh].[tbEmpleados] AS e ON cb.emp_Id = e.emp_Id
INNER JOIN [rrhh].[tbPersonas] AS p ON p.per_Id = e.per_Id
INNER JOIN [Plani].[tbCatalogoDeIngresos] AS ci ON ci.cin_IdIngreso =  cb.cin_IdIngreso
WHERE cb_Activo = 1
GO
/****** Object:  View [Plani].[V_DeduccionesExtraordinarias_Empleados]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [Plani].[V_DeduccionesExtraordinarias_Empleados]
AS
SELECT
pde.dex_IdDeduccionesExtra,
pde.eqem_Id,
CONCAT(rhp.per_Nombres, ' ', rhp.per_Apellidos) AS per_Empleado,
rhc.car_Descripcion AS car_Cargo,
pde.dex_MontoInicial,
pde.dex_MontoRestante,
pde.dex_ObservacionesComentarios,
pde.cde_IdDeducciones,
pde.dex_Cuota,
pde.dex_UsuarioCrea,
pde.dex_FechaCrea,
pde.dex_UsuarioModifica,
pde.dex_FechaModifica,
pde.dex_Activo
FROM [Plani].[tbDeduccionesExtraordinarias] AS pde
INNER JOIN [Plani].[tbCatalogoDeDeducciones] AS pcd ON pcd.cde_IdDeducciones = pde.cde_IdDeducciones --Relación de Deducciones Extraordinarias con el Catalogo de Deducciones
INNER JOIN [rrhh].[tbEquipoEmpleados] AS rhee ON rhee.eqem_Id = pde.eqem_Id							 --Relación de Deducciones Extraordinarias con el Equipo Empleado 
INNER JOIN [rrhh].[tbEquipoTrabajo] AS rhet ON rhet.eqtra_Id = rhee.eqtra_Id						 --Relación de Equipo Empleado con el Equipo Trabajo
INNER JOIN [rrhh].[tbEmpleados] AS rhe ON rhe.emp_Id = rhee.emp_Id									 --Relación de Equipo Empleado con Empleados
INNER JOIN [rrhh].[tbPersonas] AS rhp ON rhp.per_Id = rhe.per_Id									 --Relación de Empleados con Personas
INNER JOIN [rrhh].[tbCargos] AS rhc ON rhc.car_Id = rhe.car_Id

GO
/****** Object:  View [Plani].[V_INFOP_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
cREATE VIEW [Plani].[V_INFOP_RPT]
AS
SELECT 
emp.emp_Id,
per.per_Identidad,
per.per_Nombres,
per.per_Apellidos,
depto.depto_Id,
depto.depto_descripcion,
are.area_Id,
are.area_Descripcion,
cp.cpla_IdPlanilla,
cp.cpla_DescripcionPlanilla,
cdd.cde_IdDeducciones,
cdd.cde_DescripcionDeduccion,
dp.hidp_Total,
hp.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp inner join [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
inner join [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
inner join [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
inner join [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
inner join [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
inner join [Plani].[tbHistorialDeduccionPago] as dp on  dp.hipa_IdHistorialDePago = hp.hipa_IdHistorialDePago
inner join [Plani].[tbTipoPlanillaDetalleDeduccion] tpdd on dp.tpdd_IdPlanillaDetDeduccion = tpdd.tpdd_IdPlanillaDetDeduccion
inner join [Plani].[tbCatalogoDeDeducciones] cdd on cdd.cde_IdDeducciones  = tpdd.cde_IdDeducciones
inner join [Plani].[tbTipoDeduccion] as td on cdd.tde_IdTipoDedu = td.tde_IdTipoDedu
WHERE  td.tde_Descripcion= 'Normal' and cdd.cde_DescripcionDeduccion = 'INFOP'
GO
/****** Object:  View [Plani].[V_RAP_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_RAP_RPT]
AS
SELECT 
emp.emp_Id,
per.per_Identidad,
CONCAT(per.per_Nombres, ' ', per.per_Apellidos) AS per_Empleado,
depto.depto_Id,
depto.depto_descripcion,
are.area_Id,
are.area_Descripcion,
cp.cpla_IdPlanilla,
cp.cpla_DescripcionPlanilla,
cdd.cde_IdDeducciones,
cdd.cde_DescripcionDeduccion,
dp.hidp_Total,
hp.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp 
INNER JOIN [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
INNER JOIN [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
INNER JOIN [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
INNER JOIN [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas] cp on emp.cpla_IdPlanilla = cp.cpla_IdPlanilla
INNER JOIN [Plani].[tbHistorialDeduccionPago] as dp on  dp.hipa_IdHistorialDePago = hp.hipa_IdHistorialDePago
INNER JOIN [Plani].[tbTipoPlanillaDetalleDeduccion] tpdd on dp.tpdd_IdPlanillaDetDeduccion = tpdd.tpdd_IdPlanillaDetDeduccion
INNER JOIN [Plani].[tbCatalogoDeDeducciones] cdd on cdd.cde_IdDeducciones  = tpdd.cde_IdDeducciones
INNER JOIN [Plani].[tbTipoDeduccion] as td on cdd.tde_IdTipoDedu = td.tde_IdTipoDedu
WHERE cdd.cde_DescripcionDeduccion LIKE '%RAP%'
GO
/****** Object:  View [Plani].[V_AFP_RPT]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [Plani].[V_AFP_RPT]
AS
SELECT 
emp.emp_Id,
per.per_Identidad,
CONCAT(per.per_Nombres, ' ', per.per_Apellidos) AS per_Empleado,
depto.depto_Id,
depto.depto_descripcion,
are.area_Id,
are.area_Descripcion,
hp.hipa_AFP,
PCP.cpla_IdPlanilla,
PCP.cpla_DescripcionPlanilla,
hp.hipa_FechaPago
FROM [rrhh].[tbEmpleados] emp 
INNER JOIN [rrhh].[tbPersonas] per on emp.per_Id = per.per_Id
INNER JOIN [rrhh].[tbAreas] are on emp.area_Id = are.area_Id
INNER JOIN [rrhh].[tbDepartamentos] depto on emp.depto_Id = depto.depto_Id
INNER JOIN [Plani].[tbHistorialDePago] hp on hp.emp_Id = emp.emp_Id
INNER JOIN [Plani].[tbCatalogoDePlanillas]								AS	PCP		ON	PCP.cpla_IdPlanilla					=	emp.cpla_IdPlanilla
GO
/****** Object:  Table [Acce].[tbAccesoRol]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acce].[tbAccesoRol](
	[acrol_Id] [int] NOT NULL,
	[rol_Id] [int] NULL,
	[obj_Id] [int] NULL,
	[acrol_UsuarioCrea] [int] NULL,
	[acrol_FechaCrea] [datetime] NULL,
	[acrol_UsuarioModifica] [int] NULL,
	[acrol_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_ Acce _ tbAccesoRol _acrol_Id] PRIMARY KEY CLUSTERED 
(
	[acrol_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acce].[tbObjeto]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acce].[tbObjeto](
	[obj_Id] [int] NOT NULL,
	[obj_Pantalla] [nvarchar](50) NULL,
	[obj_Referencia] [varchar](100) NULL,
	[obj_UsuarioCrea] [int] NULL,
	[obj_FechaCrea] [datetime] NULL,
	[obj_UsuarioModifica] [int] NULL,
	[obj_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_Acce_ tbObjeto _obj_Id] PRIMARY KEY CLUSTERED 
(
	[obj_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acce].[tbRol]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acce].[tbRol](
	[rol_Id] [int] NOT NULL,
	[rol_Descripcion] [varchar](100) NOT NULL,
	[rol_UsuarioCrea] [int] NULL,
	[rol_FechaCrea] [datetime] NULL,
	[rol_UsuarioModifica] [int] NULL,
	[rol_FechaModifica] [datetime] NULL,
	[rol_Estado] [bit] NOT NULL,
 CONSTRAINT [PK_ Acce _ tbRol _rol_Id] PRIMARY KEY CLUSTERED 
(
	[rol_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acce].[tbRolesUsuario]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acce].[tbRolesUsuario](
	[rolu_Id] [int] NOT NULL,
	[rol_Id] [int] NULL,
	[usu_Id] [int] NULL,
	[rolu_UsuarioCrea] [int] NULL,
	[rolu_FechaCrea] [datetime] NULL,
	[rolu_UsuarioModifica] [int] NULL,
	[rolu_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_Acce_tbRolesUsuario_rolu_Id] PRIMARY KEY CLUSTERED 
(
	[rolu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbAcumuladosISR]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbAcumuladosISR](
	[aisr_Id] [int] NOT NULL,
	[aisr_Descripcion] [nvarchar](100) NOT NULL,
	[aisr_Monto] [decimal](16, 4) NOT NULL,
	[aisr_UsuarioCrea] [int] NOT NULL,
	[aisr_FechaCrea] [datetime] NOT NULL,
	[aisr_UsuarioModifica] [int] NULL,
	[aisr_FechaModifica] [datetime] NULL,
	[aisr_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbAcumluadosISR_aisr_Id] PRIMARY KEY CLUSTERED 
(
	[aisr_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbAFP]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbAFP](
	[afp_Id] [int] NOT NULL,
	[afp_Descripcion] [nvarchar](100) NOT NULL,
	[afp_AporteMinimoLps] [decimal](16, 4) NOT NULL,
	[afp_InteresAporte] [decimal](16, 4) NOT NULL,
	[afp_InteresAnual] [decimal](16, 4) NOT NULL,
	[tde_IdTipoDedu] [int] NOT NULL,
	[afp_UsuarioCrea] [int] NOT NULL,
	[afp_FechaCrea] [datetime] NOT NULL,
	[afp_UsuarioModifica] [int] NULL,
	[afp_FechaModifica] [datetime] NULL,
	[afp_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbAFP_afp_Id] PRIMARY KEY CLUSTERED 
(
	[afp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbAuxilioDeCesantias]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbAuxilioDeCesantias](
	[aces_IdAuxilioCesantia] [int] NOT NULL,
	[aces_RangoInicioMeses] [int] NOT NULL,
	[aces_RangoFinMeses] [int] NOT NULL,
	[aces_DiasAuxilioCesantia] [int] NOT NULL,
	[aces_UsuarioCrea] [int] NOT NULL,
	[aces_FechaCrea] [datetime] NOT NULL,
	[aces_UsuarioModifica] [int] NULL,
	[aces_FechaModifica] [datetime] NULL,
	[aces_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbAuxilioDeCesantias_aces_IdAuxilioCesantia] PRIMARY KEY CLUSTERED 
(
	[aces_IdAuxilioCesantia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbDeduccionAFP]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbDeduccionAFP](
	[dafp_Id] [int] NOT NULL,
	[dafp_AporteLps] [decimal](16, 4) NOT NULL,
	[afp_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[dafp_Pagado] [bit] NULL,
	[dafp_UsuarioCrea] [int] NOT NULL,
	[dafp_FechaCrea] [datetime] NOT NULL,
	[dafp_UsuarioModifica] [int] NULL,
	[dafp_FechaModifica] [datetime] NULL,
	[dafp_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbDeduccionAFP_dafp_Id] PRIMARY KEY CLUSTERED 
(
	[dafp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbISR]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbISR](
	[isr_Id] [int] NOT NULL,
	[isr_RangoInicial] [decimal](16, 4) NOT NULL,
	[isr_RangoFinal] [decimal](16, 4) NOT NULL,
	[isr_Porcentaje] [decimal](16, 4) NOT NULL,
	[tde_IdTipoDedu] [int] NOT NULL,
	[isr_UsuarioCrea] [int] NOT NULL,
	[isr_FechaCrea] [datetime] NOT NULL,
	[isr_UsuarioModifica] [int] NULL,
	[isr_FechaModifica] [datetime] NULL,
	[isr_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbISR_isr_Id] PRIMARY KEY CLUSTERED 
(
	[isr_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbLiquidaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbLiquidaciones](
	[liqu_IdLiquidacion] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[prea_IdPreaviso] [int] NOT NULL,
	[pvac_IdPagoVacaciones] [int] NOT NULL,
	[aces_IdAuxilioCesantia] [int] NOT NULL,
	[mliq_IdMotivoLiquidacion] [int] NOT NULL,
	[liqu_UsuarioCrea] [int] NOT NULL,
	[liqu_FechaCrea] [datetime] NOT NULL,
	[liqu_UsuarioModifica] [int] NULL,
	[liqu_FechaModifica] [datetime] NULL,
	[liqu_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_tbLiquidaciones] PRIMARY KEY CLUSTERED 
(
	[liqu_IdLiquidacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbLiquidacionVacaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbLiquidacionVacaciones](
	[pvac_IdPagoVacaciones] [int] NOT NULL,
	[pvac_Antiguedad] [int] NOT NULL,
	[pvac_dias] [int] NOT NULL,
	[pvac_UsuarioCrea] [int] NOT NULL,
	[pvac_FechaCrea] [datetime] NOT NULL,
	[pvac_UsuarioModifica] [int] NULL,
	[pvac_FechaModifica] [datetime] NULL,
	[pvac_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbLiquidacionesVacaciones_pvac_IdPagoVacaciones] PRIMARY KEY CLUSTERED 
(
	[pvac_IdPagoVacaciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbMotivoLiquidaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbMotivoLiquidaciones](
	[mliq_IdMotivoLiquidacion] [int] NOT NULL,
	[mliq_Descripcion] [nvarchar](50) NOT NULL,
	[mliq_EsJustificado] [bit] NOT NULL,
	[mliq_UsuarioCrea] [int] NOT NULL,
	[mliq_FechaCrea] [datetime] NOT NULL,
	[mliq_UsuarioModifica] [int] NULL,
	[mliq_FechaModifica] [datetime] NULL,
	[mliq_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbMotivoLiquidaciones_mliq_IdMotivoLiquidacion] PRIMARY KEY CLUSTERED 
(
	[mliq_IdMotivoLiquidacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbPreaviso]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbPreaviso](
	[prea_IdPreaviso] [int] NOT NULL,
	[prea_RangoInicioMeses] [int] NOT NULL,
	[prea_RangoFinMeses] [int] NOT NULL,
	[prea_DiasPreaviso] [int] NOT NULL,
	[prea_UsuarioCrea] [int] NOT NULL,
	[prea_FechaCrea] [datetime] NOT NULL,
	[prea_UsuarioModifica] [int] NULL,
	[prea_FechaModifica] [datetime] NULL,
	[prea_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbPreaviso_prea_IdPreaviso] PRIMARY KEY CLUSTERED 
(
	[prea_IdPreaviso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbRamaActividad]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbRamaActividad](
	[rama_Id] [int] NOT NULL,
	[rama_Descripcion] [nvarchar](100) NOT NULL,
	[rama_UsuarioCrea] [int] NOT NULL,
	[rama_FechaCrea] [datetime] NOT NULL,
	[rama_UsuarioModifica] [int] NULL,
	[rama_FechaModifica] [datetime] NULL,
	[rama_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbRamaActividad_rama_Id] PRIMARY KEY CLUSTERED 
(
	[rama_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbSalarioPorHora]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbSalarioPorHora](
	[saph_Idsaph] [int] NOT NULL,
	[rama_Id] [int] NOT NULL,
	[saph_SalarioMinimo] [decimal](16, 4) NULL,
	[jor_Id] [int] NOT NULL,
	[saph_Monto] [decimal](16, 4) NULL,
	[saph_TamanoEmpresaInicial] [int] NOT NULL,
	[saph_TamanoEmpresaFinal] [int] NOT NULL,
	[saph_UsuarioCrea] [int] NOT NULL,
	[saph_FechaCrea] [datetime] NOT NULL,
	[saph_UsuarioModifica] [int] NULL,
	[saph_FechaModifica] [datetime] NULL,
	[saph_Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Plani_tbSalarioPorHora_saph_Id] PRIMARY KEY CLUSTERED 
(
	[saph_Idsaph] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Plani].[tbTechosDeducciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Plani].[tbTechosDeducciones](
	[tddu_IdTechosDeducciones] [int] NOT NULL,
	[tddu_PorcentajeColaboradores] [decimal](16, 4) NOT NULL,
	[tddu_PorcentajeEmpresa] [decimal](16, 4) NOT NULL,
	[tddu_Techo] [decimal](16, 4) NOT NULL,
	[cde_IdDeducciones] [int] NOT NULL,
	[tddu_Activo] [bit] NOT NULL,
	[tddu_UsuarioCrea] [int] NOT NULL,
	[tddu_FechaCrea] [datetime] NOT NULL,
	[tddu_UsuarioModifica] [int] NULL,
	[tddu_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_Plani_tbTechosDeducciones_tddu_IdTechosDeducciones] PRIMARY KEY CLUSTERED 
(
	[tddu_IdTechosDeducciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbCompetenciasRequisicion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbCompetenciasRequisicion](
	[creq_Id] [int] NOT NULL,
	[req_Id] [int] NOT NULL,
	[comp_Id] [int] NOT NULL,
	[creq_Estado] [bit] NOT NULL,
	[creq_RazonInactivo] [nvarchar](100) NULL,
	[creq_UsuarioCrea] [int] NOT NULL,
	[creq_FechaCrea] [datetime] NOT NULL,
	[creq_UsuarioModifica] [int] NULL,
	[req_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbCompetenciasRequisicion_creq_Id] PRIMARY KEY CLUSTERED 
(
	[creq_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbEmpresas]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbEmpresas](
	[empr_Id] [int] NOT NULL,
	[empr_Nombre] [nvarchar](40) NOT NULL,
	[empr_Estado] [bit] NOT NULL,
	[empr_RazonInactivo] [nvarchar](100) NULL,
	[empr_UsuarioCrea] [int] NOT NULL,
	[empr_FechaCrea] [datetime] NOT NULL,
	[empr_UsuarioModifica] [int] NULL,
	[empr_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbEmpresas_empr_Id] PRIMARY KEY CLUSTERED 
(
	[empr_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbFaseSeleccion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbFaseSeleccion](
	[fsel_Id] [int] NOT NULL,
	[fare_Id] [int] NOT NULL,
	[fsel_Fecha] [datetime] NOT NULL,
	[fsel_Estado] [bit] NOT NULL,
	[fsel_RazonInactivo] [nvarchar](100) NULL,
	[fsel_UsuarioCrea] [int] NOT NULL,
	[fsel_FechaCrea] [datetime] NOT NULL,
	[fsel_UsuarioModifica] [int] NULL,
	[fsel_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbFaseSeleccion_fsel_Id] PRIMARY KEY CLUSTERED 
(
	[fsel_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHabilidadesRequisicion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHabilidadesRequisicion](
	[hreq_Id] [int] NOT NULL,
	[req_Id] [int] NOT NULL,
	[habi_Id] [int] NOT NULL,
	[hreq_Estado] [bit] NOT NULL,
	[hreq_RazonInactivo] [nvarchar](100) NULL,
	[hreq_UsuarioCrea] [int] NOT NULL,
	[hreq_FechaCrea] [datetime] NOT NULL,
	[hreq_UsuarioModifica] [int] NULL,
	[hreq_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHabilidadesRequisicion_hreq_Id] PRIMARY KEY CLUSTERED 
(
	[hreq_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialPermisos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialPermisos](
	[hper_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[tper_Id] [int] NOT NULL,
	[hper_fechaInicio] [datetime] NOT NULL,
	[hper_fechaFin] [datetime] NOT NULL,
	[hper_Duracion] [int] NOT NULL,
	[hper_Observacion] [nvarchar](25) NULL,
	[hper_PorcentajeIndemnizado] [int] NOT NULL,
	[hper_Estado] [bit] NOT NULL,
	[hper_RazonInactivo] [nvarchar](100) NULL,
	[hper_UsuarioCrea] [int] NOT NULL,
	[hper_FechaCrea] [datetime] NOT NULL,
	[hper_UsuarioModifica] [int] NULL,
	[hper_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialPermisos_hper_Id] PRIMARY KEY CLUSTERED 
(
	[hper_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialRefrendamientos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialRefrendamientos](
	[href_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[href_Archivo] [nvarchar](25) NULL,
	[href_FechaRefrendado] [date] NOT NULL,
	[href_Estado] [bit] NOT NULL,
	[href_RazonInactivo] [nvarchar](100) NULL,
	[href_UsuarioCrea] [int] NOT NULL,
	[href_FechaCrea] [datetime] NOT NULL,
	[href_UsuarioModifica] [int] NULL,
	[href_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialRefrendamientos_href_Id] PRIMARY KEY CLUSTERED 
(
	[href_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbHistorialVacaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbHistorialVacaciones](
	[hvac_Id] [int] NOT NULL,
	[emp_Id] [int] NOT NULL,
	[hvac_FechaInicio] [date] NOT NULL,
	[hvac_FechaFin] [date] NOT NULL,
	[hvac_DiasTomados] [bit] NOT NULL,
	[hvac_MesVacaciones] [int] NOT NULL,
	[hvac_AnioVacaciones] [int] NOT NULL,
	[hvac_Estado] [bit] NOT NULL,
	[hvac_RazonInactivo] [nvarchar](100) NULL,
	[hvac_UsuarioCrea] [int] NOT NULL,
	[hvac_FechaCrea] [datetime] NOT NULL,
	[hvac_UsuarioModifica] [int] NULL,
	[hvac_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHistorialVacaciones_hvac_Id] PRIMARY KEY CLUSTERED 
(
	[hvac_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbIdiomasRequisicion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbIdiomasRequisicion](
	[ireq_Id] [int] NOT NULL,
	[req_Id] [int] NOT NULL,
	[idi_Id] [int] NOT NULL,
	[ireq_Estado] [bit] NOT NULL,
	[ireq_RazonInactivo] [nvarchar](100) NULL,
	[ireq_UsuarioCrea] [int] NOT NULL,
	[ireq_FechaCrea] [datetime] NOT NULL,
	[ireq_UsuarioModifica] [int] NULL,
	[ireq_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbIdiomasRequisicion_ireq_Id] PRIMARY KEY CLUSTERED 
(
	[ireq_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbNacionalidades]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbNacionalidades](
	[nac_Id] [int] NOT NULL,
	[nac_Descripcion] [nvarchar](50) NOT NULL,
	[nac_Estado] [bit] NOT NULL,
	[nac_RazonInactivo] [nvarchar](100) NULL,
	[nac_UsuarioCrea] [int] NOT NULL,
	[nac_FechaCrea] [datetime] NOT NULL,
	[nac_UsuarioModifica] [int] NULL,
	[nac_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbNacionalidades_nac_Id] PRIMARY KEY CLUSTERED 
(
	[nac_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbPrestaciones]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbPrestaciones](
	[pres_Id] [int] NOT NULL,
	[pres_DerechoPreaviso] [bit] NOT NULL,
	[pres_Preaviso] [decimal](8, 4) NOT NULL,
	[pres_DecimoTercer] [decimal](8, 4) NOT NULL,
	[pres_Catorceavo] [decimal](8, 4) NOT NULL,
	[pres_Estado] [bit] NOT NULL,
	[pres_RazonInactivo] [nvarchar](100) NULL,
	[pres_UsuarioCrea] [int] NOT NULL,
	[pres_FechaCrea] [datetime] NOT NULL,
	[pres_UsuarioModifica] [int] NULL,
	[pres_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbPrestaciones_pres_Id] PRIMARY KEY CLUSTERED 
(
	[pres_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbRequerimientosEspecialesRequisicion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbRequerimientosEspecialesRequisicion](
	[rer_Id] [int] NOT NULL,
	[req_Id] [int] NOT NULL,
	[resp_Id] [int] NOT NULL,
	[rer_Estado] [bit] NOT NULL,
	[rer_RazonInactivo] [nvarchar](100) NULL,
	[rer_UsuarioCrea] [int] NOT NULL,
	[rer_FechaCrea] [datetime] NOT NULL,
	[rer_UsuarioModifica] [int] NULL,
	[rer_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbHabilidadesRequisicion_rer_Id] PRIMARY KEY CLUSTERED 
(
	[rer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbSucursales]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbSucursales](
	[suc_Id] [int] NOT NULL,
	[empr_Id] [int] NOT NULL,
	[mun_Codigo] [char](4) NOT NULL,
	[bod_Id] [int] NOT NULL,
	[pemi_Id] [int] NOT NULL,
	[suc_Descripcion] [nvarchar](50) NOT NULL,
	[suc_Correo] [nvarchar](50) NOT NULL,
	[suc_Direccion] [nvarchar](100) NOT NULL,
	[suc_Telefono] [nvarchar](9) NOT NULL,
	[suc_Estado] [bit] NOT NULL,
	[suc_RazonInactivo] [nvarchar](100) NULL,
	[suc_UsuarioCrea] [int] NOT NULL,
	[suc_FechaCrea] [datetime] NOT NULL,
	[suc_UsuarioModifica] [int] NULL,
	[suc_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbSucursales_suc_Id] PRIMARY KEY CLUSTERED 
(
	[suc_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbTipoPermisos]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTipoPermisos](
	[tper_Id] [int] NOT NULL,
	[tper_Descripcion] [nvarchar](25) NOT NULL,
	[tper_Estado] [bit] NOT NULL,
	[tper_RazonInactivo] [nvarchar](100) NULL,
	[tper_UsuarioCrea] [int] NOT NULL,
	[tper_FechaCrea] [datetime] NOT NULL,
	[tper_UsuarioModifica] [int] NULL,
	[tper_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTipoPermisos_tper_Id] PRIMARY KEY CLUSTERED 
(
	[tper_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [rrhh].[tbTitulosRequisicion]    Script Date: 09/12/2019 16:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [rrhh].[tbTitulosRequisicion](
	[treq_Id] [int] NOT NULL,
	[req_Id] [int] NOT NULL,
	[titu_Id] [int] NOT NULL,
	[treq_Estado] [bit] NOT NULL,
	[treq_RazonInactivo] [nvarchar](100) NULL,
	[treq_UsuarioCrea] [int] NOT NULL,
	[treq_FechaCrea] [datetime] NOT NULL,
	[treq_UsuarioModifica] [int] NULL,
	[treq_FechaModifica] [datetime] NULL,
 CONSTRAINT [PK_tbTitulosRequisicion_treq_Id] PRIMARY KEY CLUSTERED 
(
	[treq_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Acce].[tbUsuario] ([usu_Id], [usu_NombreUsuario], [usu_Password], [usu_Nombres], [usu_Apellidos], [usu_Correos], [usu_EsActivo], [usu_RazonInactivo], [usu_EsAdministrador], [usu_SesionesValidas]) VALUES (1, N'Admin', 0x0000007B, N'Selvin', N'Medina', N'selvinmedina@gmail.com', 1, N'No Aplica', 1, 1)
INSERT [Acce].[tbUsuario] ([usu_Id], [usu_NombreUsuario], [usu_Password], [usu_Nombres], [usu_Apellidos], [usu_Correos], [usu_EsActivo], [usu_RazonInactivo], [usu_EsAdministrador], [usu_SesionesValidas]) VALUES (2, N'Invitado', 0x0000007B, N'Onandy', N'Diaz', N'will.d98@gmail.com', 1, N'No Aplica', 0, 1)
INSERT [Plani].[tbAcumuladosISR] ([aisr_Id], [aisr_Descripcion], [aisr_Monto], [aisr_UsuarioCrea], [aisr_FechaCrea], [aisr_UsuarioModifica], [aisr_FechaModifica], [aisr_Activo]) VALUES (1, N'Gastos médicos', CAST(40000.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T13:43:40.687' AS DateTime), 1, CAST(N'2019-12-05T20:31:19.270' AS DateTime), 1)
INSERT [Plani].[tbAcumuladosISR] ([aisr_Id], [aisr_Descripcion], [aisr_Monto], [aisr_UsuarioCrea], [aisr_FechaCrea], [aisr_UsuarioModifica], [aisr_FechaModifica], [aisr_Activo]) VALUES (2, N'Testeo edicion', CAST(3000.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-05T20:47:57.887' AS DateTime), 1, CAST(N'2019-12-05T20:48:35.463' AS DateTime), 0)
INSERT [Plani].[tbAcumuladosISR] ([aisr_Id], [aisr_Descripcion], [aisr_Monto], [aisr_UsuarioCrea], [aisr_FechaCrea], [aisr_UsuarioModifica], [aisr_FechaModifica], [aisr_Activo]) VALUES (3, N'Testeo02', CAST(2000.5600 AS Decimal(16, 4)), 1, CAST(N'2019-12-05T20:50:04.003' AS DateTime), 1, CAST(N'2019-12-05T20:50:27.047' AS DateTime), 0)
INSERT [Plani].[tbAcumuladosISR] ([aisr_Id], [aisr_Descripcion], [aisr_Monto], [aisr_UsuarioCrea], [aisr_FechaCrea], [aisr_UsuarioModifica], [aisr_FechaModifica], [aisr_Activo]) VALUES (4, N'Testeo03', CAST(2500.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-05T20:51:50.210' AS DateTime), 1, CAST(N'2019-12-05T20:52:03.967' AS DateTime), 0)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (1, 7, CAST(N'2019-12-06T00:00:00.000' AS DateTime), N'Enfermedad', CAST(4500.0000 AS Decimal(16, 4)), 0, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-05T15:58:56.430' AS DateTime), 0)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (2, 12, CAST(N'2019-12-05T12:27:48.953' AS DateTime), N'Adelanto duplicado 33', CAST(1200.0000 AS Decimal(16, 4)), 0, 1, CAST(N'2019-12-05T12:27:48.953' AS DateTime), 1, CAST(N'2019-12-05T15:58:05.620' AS DateTime), 0)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (3, 12, CAST(N'2019-12-05T12:33:21.673' AS DateTime), N'Prueba123', CAST(6500.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-05T12:33:21.673' AS DateTime), 1, CAST(N'2019-12-06T15:23:06.917' AS DateTime), 1)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (4, 4, CAST(N'2019-12-05T15:43:08.693' AS DateTime), N'Pago de deudas atrasadas', CAST(1000.0000 AS Decimal(16, 4)), 0, 1, CAST(N'2019-12-05T15:43:08.693' AS DateTime), 1, CAST(N'2019-12-05T15:55:26.310' AS DateTime), 0)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (5, 2, CAST(N'2019-12-05T16:08:35.877' AS DateTime), N'Compra de vehículo', CAST(5000.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-05T16:08:35.877' AS DateTime), 1, CAST(N'2019-12-06T12:41:15.590' AS DateTime), 0)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (6, 6, CAST(N'2019-12-06T05:43:45.563' AS DateTime), N'Testing', CAST(1234.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T05:43:47.240' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (7, 6, CAST(N'2019-12-06T05:44:40.160' AS DateTime), N'Testing', CAST(1234.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T05:44:40.160' AS DateTime), 1, CAST(N'2019-12-09T12:43:53.160' AS DateTime), 1)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (8, 1, CAST(N'2019-12-06T05:46:09.787' AS DateTime), N'Robert', CAST(5500.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T05:46:09.787' AS DateTime), 1, CAST(N'2019-12-06T15:00:55.613' AS DateTime), 1)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (9, 5, CAST(N'2019-12-06T05:47:45.997' AS DateTime), N'Enfermedad', CAST(6500.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T05:47:45.997' AS DateTime), 1, CAST(N'2019-12-06T14:59:20.040' AS DateTime), 1)
INSERT [Plani].[tbAdelantoSueldo] ([adsu_IdAdelantoSueldo], [emp_Id], [adsu_FechaAdelanto], [adsu_RazonAdelanto], [adsu_Monto], [adsu_Deducido], [adsu_UsuarioCrea], [adsu_FechaCrea], [adsu_UsuarioModifica], [adsu_FechaModifica], [adsu_Activo]) VALUES (10, 13, CAST(N'2019-12-09T12:45:20.470' AS DateTime), N'Para editar', CAST(5000.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-09T12:45:20.470' AS DateTime), 1, CAST(N'2019-12-09T12:46:59.967' AS DateTime), 1)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (1, N'Bac Credomatic', CAST(250.0000 AS Decimal(16, 4)), CAST(10.0000 AS Decimal(16, 4)), CAST(3.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-02T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T12:47:44.003' AS DateTime), 1)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (2, N'Atlantida', CAST(250.0000 AS Decimal(16, 4)), CAST(1.0000 AS Decimal(16, 4)), CAST(9.5000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-02T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T12:47:39.207' AS DateTime), 1)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (3, N'RAP', CAST(250.0000 AS Decimal(16, 4)), CAST(8.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-04T23:34:59.130' AS DateTime), 1, CAST(N'2019-12-04T23:35:43.363' AS DateTime), 1)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (4, N'Ficohsa', CAST(250.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), CAST(9.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-04T23:38:10.847' AS DateTime), 1, CAST(N'2019-12-09T12:43:42.717' AS DateTime), 1)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (5, N'Pruebas', CAST(100.0000 AS Decimal(16, 4)), CAST(1.0000 AS Decimal(16, 4)), CAST(1.0000 AS Decimal(16, 4)), 3, 1, CAST(N'2019-12-09T07:44:12.447' AS DateTime), 1, CAST(N'2019-12-09T12:50:25.243' AS DateTime), 0)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (6, N'Prueba', CAST(500.0000 AS Decimal(16, 4)), CAST(5.0000 AS Decimal(16, 4)), CAST(1.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-09T08:31:48.770' AS DateTime), 1, CAST(N'2019-12-09T12:50:20.233' AS DateTime), 0)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (7, N'Test', CAST(300.0000 AS Decimal(16, 4)), CAST(10.0000 AS Decimal(16, 4)), CAST(3.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-09T08:48:03.880' AS DateTime), 1, CAST(N'2019-12-09T12:49:08.037' AS DateTime), 0)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (8, N'Pruebas', CAST(250.0000 AS Decimal(16, 4)), CAST(2.0000 AS Decimal(16, 4)), CAST(8.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-09T11:04:10.943' AS DateTime), 1, CAST(N'2019-12-09T12:50:00.753' AS DateTime), 0)
INSERT [Plani].[tbAFP] ([afp_Id], [afp_Descripcion], [afp_AporteMinimoLps], [afp_InteresAporte], [afp_InteresAnual], [tde_IdTipoDedu], [afp_UsuarioCrea], [afp_FechaCrea], [afp_UsuarioModifica], [afp_FechaModifica], [afp_Activo]) VALUES (9, N'Prueba', CAST(300.0000 AS Decimal(16, 4)), CAST(3.0000 AS Decimal(16, 4)), CAST(5.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-09T12:45:35.480' AS DateTime), 1, CAST(N'2019-12-09T12:50:15.947' AS DateTime), 0)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (1, 0, 0, 0, 1, CAST(N'2019-12-09T02:22:21.107' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (2, 1, 2, 3, 1, CAST(N'2019-12-09T02:22:38.393' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (3, 0, 0, 0, 1, CAST(N'2019-12-09T09:22:35.773' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (4, 0, 0, 0, 1, CAST(N'2019-12-09T09:29:46.480' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (5, 0, 0, 0, 1, CAST(N'2019-12-09T09:37:11.950' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (6, 1, 15, 30, 1, CAST(N'2019-12-09T09:43:09.643' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (7, 1, 5, 8, 1, CAST(N'2019-12-09T12:17:14.490' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (8, 1, 10, 17, 1, CAST(N'2019-12-09T12:20:37.123' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (9, 1, 15, 30, 1, CAST(N'2019-12-09T12:28:45.173' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (10, 1, 15, 30, 1, CAST(N'2019-12-09T12:37:24.313' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (11, 1, 15, 30, 1, CAST(N'2019-12-09T12:39:46.233' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (12, 1, 10, 15, 1, CAST(N'2019-12-09T12:57:59.890' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (13, 1, 12, 16, 1, CAST(N'2019-12-09T13:05:48.687' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (14, 1, 12, 16, 1, CAST(N'2019-12-09T13:07:27.013' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (15, 10, 20, 50, 1, CAST(N'2019-12-09T13:19:07.050' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (16, 1, 1, 1, 1, CAST(N'2019-12-09T13:30:58.770' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (17, 0, 0, 0, 1, CAST(N'2019-12-09T13:33:05.227' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (18, 0, 0, 0, 1, CAST(N'2019-12-09T13:39:42.770' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (19, 0, 0, 0, 1, CAST(N'2019-12-09T13:42:15.097' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (20, 0, 0, 0, 1, CAST(N'2019-12-09T13:44:38.300' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (21, 0, 0, 0, 1, CAST(N'2019-12-09T13:50:17.123' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (22, 0, 0, 0, 1, CAST(N'2019-12-09T13:52:13.847' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (23, 0, 0, 0, 1, CAST(N'2019-12-09T13:59:25.720' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (24, 0, 0, 0, 1, CAST(N'2019-12-09T14:00:23.353' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (25, 0, 0, 0, 1, CAST(N'2019-12-09T14:02:28.983' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (26, 0, 0, 0, 1, CAST(N'2019-12-09T14:04:32.917' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (27, 0, 0, 0, 1, CAST(N'2019-12-09T14:05:35.563' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (28, 0, 0, 0, 1, CAST(N'2019-12-09T14:15:50.947' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (29, 0, 0, 0, 1, CAST(N'2019-12-09T14:18:30.790' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (30, 0, 0, 0, 1, CAST(N'2019-12-09T14:19:09.770' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (31, 0, 0, 0, 1, CAST(N'2019-12-09T14:22:38.920' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (32, 0, 0, 0, 1, CAST(N'2019-12-09T14:30:22.057' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (33, 0, 0, 0, 1, CAST(N'2019-12-09T14:31:39.960' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (34, 0, 0, 0, 1, CAST(N'2019-12-09T14:39:55.030' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (35, 0, 0, 0, 1, CAST(N'2019-12-09T14:40:07.240' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (36, 0, 0, 0, 1, CAST(N'2019-12-09T14:41:04.893' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (37, 0, 0, 0, 1, CAST(N'2019-12-09T14:42:39.297' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia], [aces_RangoInicioMeses], [aces_RangoFinMeses], [aces_DiasAuxilioCesantia], [aces_UsuarioCrea], [aces_FechaCrea], [aces_UsuarioModifica], [aces_FechaModifica], [aces_Activo]) VALUES (38, 1, 10, 90, 1, CAST(N'2019-12-09T15:14:45.533' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (1, N'IHSS', 1, CAST(2.5000 AS Decimal(16, 4)), CAST(3.5000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (2, N'RAP', 1, CAST(1.5000 AS Decimal(16, 4)), CAST(1.5000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (3, N'INFOP', 1, CAST(1.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (4, N'Equipo Dañado', 3, CAST(2.5000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T21:01:16.177' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (5, N'Instituciones Financieras', 2, CAST(0.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (6, N'AFP', 1, CAST(0.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T11:32:20.450' AS DateTime), 1)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (7, N'Test', 1, CAST(25.0000 AS Decimal(16, 4)), CAST(24.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-05T19:59:00.387' AS DateTime), 1, CAST(N'2019-12-05T19:59:59.057' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones], [cde_DescripcionDeduccion], [tde_IdTipoDedu], [cde_PorcentajeColaborador], [cde_PorcentajeEmpresa], [cde_UsuarioCrea], [cde_FechaCrea], [cde_UsuarioModifica], [cde_FechaModifica], [cde_Activo]) VALUES (8, N'Deduccion nueva editada', 2, CAST(9.5000 AS Decimal(16, 4)), CAST(5.3300 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T03:17:25.337' AS DateTime), 1, CAST(N'2019-12-06T03:17:52.913' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (1, N'Septimo dia', 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (2, N'Bonos', 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (3, N'Horas extras', 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (4, N'Bono Navideño', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (5, N'Días compensatorios', 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (7, N'Salario Base', 1, CAST(N'2019-12-04T14:16:17.590' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (8, N'Comisiones', 1, CAST(N'2019-12-05T20:14:57.627' AS DateTime), 1, CAST(N'2019-12-05T20:15:58.357' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (9, N'Wifi Gratis', 1, CAST(N'2019-12-06T04:55:10.737' AS DateTime), 1, CAST(N'2019-12-06T04:55:36.153' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (10, N'Testeo', 1, CAST(N'2019-12-06T10:27:55.720' AS DateTime), 1, CAST(N'2019-12-06T10:30:19.687' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso], [cin_DescripcionIngreso], [cin_UsuarioCrea], [cin_FechaCrea], [cin_UsuarioModifica], [cin_FechaModifica], [cin_Activo]) VALUES (11, N'Testeos02', 1, CAST(N'2019-12-06T10:29:12.287' AS DateTime), 1, CAST(N'2019-12-06T10:29:42.160' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (1, N'Vendedores', 15, 1, 1, CAST(N'2019-05-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T13:13:13.577' AS DateTime), 1)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (2, N'Outsorcing', 30, 0, 1, CAST(N'2008-07-06T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-07T00:09:03.790' AS DateTime), 1)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (3, N'Normal', 30, 1, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T13:09:52.153' AS DateTime), 1)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (4, N'Vendedores', 30, 1, 1, CAST(N'2019-12-04T14:11:21.117' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (5, N'Mayores de 18 años', 11, 1, 1, CAST(N'2019-12-06T05:39:34.083' AS DateTime), 1, CAST(N'2019-12-09T13:07:22.360' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (6, N'Planilla 3x4', 3, 0, 1, CAST(N'2019-12-09T13:15:29.180' AS DateTime), 1, CAST(N'2019-12-09T13:31:15.030' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (7, N'Planilla discapacitados', 30, 1, 1, CAST(N'2019-12-09T13:25:50.750' AS DateTime), 1, CAST(N'2019-12-09T14:10:12.163' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (8, N'Planilla 4xr5', 5, 1, 1, CAST(N'2019-12-09T13:34:15.157' AS DateTime), 1, CAST(N'2019-12-09T13:37:00.327' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (9, N'Planilla Selvin', 5, 1, 1, CAST(N'2019-12-09T14:23:39.330' AS DateTime), 1, CAST(N'2019-12-09T14:26:01.943' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (10, N'Planilla Selvin-Abigail1411', 15, 0, 1, CAST(N'2019-12-09T14:35:18.803' AS DateTime), 1, CAST(N'2019-12-09T14:36:49.860' AS DateTime), 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (11, N'Planilla #1', 15, 1, 1, CAST(N'2019-12-09T14:38:26.857' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (12, N'Planilla #2', 30, 1, 1, CAST(N'2019-12-09T14:38:55.890' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (13, N'Planilla #3', 100, 0, 1, CAST(N'2019-12-09T14:39:51.740' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (14, N'Planilla #4', 35, 1, 1, CAST(N'2019-12-09T14:40:33.713' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (15, N'Planilla #5', 20, 1, 1, CAST(N'2019-12-09T14:41:13.563' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (16, N'Planilla #6', 50, 0, 1, CAST(N'2019-12-09T14:41:36.227' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (17, N'Planilla Selvin', 50, 1, 1, CAST(N'2019-12-09T14:42:18.450' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (18, N'Planilla Grupo Out soursing', 30, 1, 1, CAST(N'2019-12-09T14:43:20.480' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla], [cpla_DescripcionPlanilla], [cpla_FrecuenciaEnDias], [cpla_RecibeComision], [cpla_UsuarioCrea], [cpla_FechaCrea], [cpla_UsuarioModifica], [cpla_FechaModifica], [cpla_Activo]) VALUES (19, N'Planilla Selvin-Abigail1411', 15, 1, 1, CAST(N'2019-12-09T15:02:04.217' AS DateTime), NULL, NULL, 0)
INSERT [Plani].[tbDecimoCuartoMes] ([dcm_IdDecimoCuartoMes], [dcm_FechaPago], [dcm_UsuarioCrea], [dcm_FechaCrea], [dcm_UsuarioModifica], [dcm_FechaModifica], [emp_Id], [dcm_Monto], [dcm_CodigoPago]) VALUES (1, CAST(N'2019-12-05' AS Date), 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL, 1, CAST(13000.0000 AS Decimal(16, 4)), N'20191                                             ')
INSERT [Plani].[tbDecimoCuartoMes] ([dcm_IdDecimoCuartoMes], [dcm_FechaPago], [dcm_UsuarioCrea], [dcm_FechaCrea], [dcm_UsuarioModifica], [dcm_FechaModifica], [emp_Id], [dcm_Monto], [dcm_CodigoPago]) VALUES (2, CAST(N'2019-11-05' AS Date), 1, CAST(N'2019-11-05T00:00:00.000' AS DateTime), NULL, NULL, 4, CAST(14000.0000 AS Decimal(16, 4)), N'20194                                             ')
INSERT [Plani].[tbDecimoCuartoMes] ([dcm_IdDecimoCuartoMes], [dcm_FechaPago], [dcm_UsuarioCrea], [dcm_FechaCrea], [dcm_UsuarioModifica], [dcm_FechaModifica], [emp_Id], [dcm_Monto], [dcm_CodigoPago]) VALUES (3, CAST(N'2019-11-05' AS Date), 1, CAST(N'2019-11-05T00:00:00.000' AS DateTime), NULL, NULL, 7, CAST(16000.0000 AS Decimal(16, 4)), N'20197                                             ')
SET IDENTITY_INSERT [Plani].[tbDecimoTercerMes] ON 

INSERT [Plani].[tbDecimoTercerMes] ([dtm_IdDecimoTercerMes], [dtm_FechaPago], [dtm_UsuarioCrea], [dtm_FechaCrea], [dtm_UsuarioModifica], [dtm_FechaModifica], [emp_Id], [dtm_Monto], [dtm_CodigoPago]) VALUES (1, CAST(N'2019-12-09' AS Date), 1, CAST(N'2019-12-09T19:44:55.590' AS DateTime), NULL, NULL, 1, CAST(23176.0300 AS Decimal(16, 4)), N'20191')
INSERT [Plani].[tbDecimoTercerMes] ([dtm_IdDecimoTercerMes], [dtm_FechaPago], [dtm_UsuarioCrea], [dtm_FechaCrea], [dtm_UsuarioModifica], [dtm_FechaModifica], [emp_Id], [dtm_Monto], [dtm_CodigoPago]) VALUES (2, CAST(N'2019-12-09' AS Date), 1, CAST(N'2019-12-09T20:14:04.330' AS DateTime), NULL, NULL, 3, CAST(27848.1900 AS Decimal(16, 4)), N'20193')
SET IDENTITY_INSERT [Plani].[tbDecimoTercerMes] OFF
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (1, CAST(400.0000 AS Decimal(16, 4)), 4, 2, 0, 1, CAST(N'2019-12-04T15:29:55.947' AS DateTime), 1, CAST(N'2019-12-05T00:16:04.243' AS DateTime), 0)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (2, CAST(300.0000 AS Decimal(16, 4)), 1, 6, 1, 1, CAST(N'2019-12-04T15:30:47.937' AS DateTime), 1, CAST(N'2019-12-05T00:15:04.207' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (3, CAST(250.0000 AS Decimal(16, 4)), 2, 7, 1, 1, CAST(N'2019-12-04T15:31:16.657' AS DateTime), 1, CAST(N'2019-12-05T00:14:50.580' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (4, CAST(450.0000 AS Decimal(16, 4)), 3, 8, 0, 1, CAST(N'2019-12-05T00:12:09.807' AS DateTime), 1, CAST(N'2019-12-05T00:12:56.653' AS DateTime), 0)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (5, CAST(350.0000 AS Decimal(16, 4)), 2, 1, 1, 1, CAST(N'2019-12-05T00:35:18.697' AS DateTime), 1, CAST(N'2019-12-05T00:36:26.043' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (6, CAST(500.0000 AS Decimal(16, 4)), 4, 11, 1, 1, CAST(N'2019-12-05T00:37:22.393' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (7, CAST(400.0000 AS Decimal(16, 4)), 2, 4, 1, 1, CAST(N'2019-12-05T00:37:40.440' AS DateTime), 1, CAST(N'2019-12-09T12:31:20.157' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (8, CAST(500.0000 AS Decimal(16, 4)), 4, 4, 1, 1, CAST(N'2019-12-05T12:30:39.397' AS DateTime), 1, CAST(N'2019-12-05T12:31:14.337' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (9, CAST(700.0000 AS Decimal(16, 4)), 4, 10, 1, 1, CAST(N'2019-12-09T08:14:56.160' AS DateTime), 1, CAST(N'2019-12-09T08:15:22.113' AS DateTime), 1)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (10, CAST(900.0000 AS Decimal(16, 4)), 1, 5, 0, 1, CAST(N'2019-12-09T08:51:29.283' AS DateTime), 1, CAST(N'2019-12-09T08:52:41.283' AS DateTime), 0)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (11, CAST(200.0000 AS Decimal(16, 4)), 3, 2, 0, 1, CAST(N'2019-12-09T10:44:46.067' AS DateTime), 1, CAST(N'2019-12-09T10:46:24.263' AS DateTime), 0)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (12, CAST(0.0000 AS Decimal(16, 4)), 1, 1, 0, 1, CAST(N'2019-12-09T11:14:03.953' AS DateTime), 1, CAST(N'2019-12-09T11:16:55.467' AS DateTime), 0)
INSERT [Plani].[tbDeduccionAFP] ([dafp_Id], [dafp_AporteLps], [afp_Id], [emp_Id], [dafp_Pagado], [dafp_UsuarioCrea], [dafp_FechaCrea], [dafp_UsuarioModifica], [dafp_FechaModifica], [dafp_Activo]) VALUES (13, CAST(100.0000 AS Decimal(16, 4)), 3, 3, 1, 1, CAST(N'2019-12-09T12:31:41.763' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (1, 1, CAST(10000.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), N'CPU Inservible', 4, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-04T15:01:48.560' AS DateTime), 1, CAST(N'2019-12-06T09:46:06.313' AS DateTime), 1)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (2, 1, CAST(1.6000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), N'Se quebro una computadora y esta maaaaaaaas mala', 3, CAST(250.0400 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T03:14:50.020' AS DateTime), 1, CAST(N'2019-12-06T03:16:26.657' AS DateTime), 1)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (3, 1, CAST(1000.0000 AS Decimal(16, 4)), CAST(0.0000 AS Decimal(16, 4)), N'Mouse Extraviado', 4, CAST(100.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T09:30:31.880' AS DateTime), 1, CAST(N'2019-12-06T09:31:19.227' AS DateTime), 1)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (4, 1, CAST(5000.0000 AS Decimal(16, 4)), CAST(4000.0000 AS Decimal(16, 4)), N'Monitor Inservible', 4, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T09:44:14.477' AS DateTime), 1, CAST(N'2019-12-06T13:50:24.203' AS DateTime), 0)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (5, 1, CAST(1000.0000 AS Decimal(16, 4)), CAST(1000.0000 AS Decimal(16, 4)), N'Prueba', 7, CAST(100.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T12:54:54.687' AS DateTime), 1, CAST(N'2019-12-06T13:31:35.840' AS DateTime), 0)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (6, 1, CAST(1200.0000 AS Decimal(16, 4)), CAST(200.0000 AS Decimal(16, 4)), N'Test', 7, CAST(50.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T13:21:18.723' AS DateTime), 1, CAST(N'2019-12-06T13:30:40.463' AS DateTime), 0)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (7, 1, CAST(1500.0000 AS Decimal(16, 4)), CAST(1000.0000 AS Decimal(16, 4)), N'Tester', 7, CAST(100.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T13:27:00.010' AS DateTime), 1, CAST(N'2019-12-06T13:30:06.750' AS DateTime), 0)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (8, 1, CAST(500.0000 AS Decimal(16, 4)), CAST(300.0000 AS Decimal(16, 4)), N'Prueba', 7, CAST(50.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-09T08:08:36.497' AS DateTime), 1, CAST(N'2019-12-09T08:09:29.490' AS DateTime), 0)
INSERT [Plani].[tbDeduccionesExtraordinarias] ([dex_IdDeduccionesExtra], [eqem_Id], [dex_MontoInicial], [dex_MontoRestante], [dex_ObservacionesComentarios], [cde_IdDeducciones], [dex_Cuota], [dex_UsuarioCrea], [dex_FechaCrea], [dex_UsuarioModifica], [dex_FechaModifica], [dex_Activo]) VALUES (9, 1, CAST(1000.0000 AS Decimal(16, 4)), CAST(200.0000 AS Decimal(16, 4)), N'Pruebas', 1, CAST(100.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-09T15:53:58.640' AS DateTime), 2, CAST(N'2019-12-09T15:54:11.250' AS DateTime), 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (1, 1, 1, CAST(500.0000 AS Decimal(16, 4)), N'Cuota 1/24', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (2, 2, 2, CAST(400.0000 AS Decimal(16, 4)), N'Cuota 21/21', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 0)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (3, 3, 3, CAST(200.0000 AS Decimal(16, 4)), N'Ahorro', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (4, 4, 4, CAST(100.0000 AS Decimal(16, 4)), N'Ahorro', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (5, 5, 1, CAST(100.0000 AS Decimal(16, 4)), N'Cuota 23/36', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 0)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (6, 6, 2, CAST(600.0000 AS Decimal(16, 4)), N'Ahorro', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (7, 7, 3, CAST(150.0000 AS Decimal(16, 4)), N'Ahorro', 5, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (8, 1, 1, CAST(500.4700 AS Decimal(16, 4)), N'cuota 1/12 - prestamo personal', 5, 1, CAST(N'2019-12-05T14:32:04.333' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (9, 2, 1, CAST(750.5800 AS Decimal(16, 4)), N'cuota 12/12 prestamo hipotecario', 5, 1, CAST(N'2019-12-05T14:32:06.183' AS DateTime), NULL, NULL, 1, 0)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (10, 3, 1, CAST(985.2400 AS Decimal(16, 4)), N'Ahorro Personal', 5, 1, CAST(N'2019-12-05T14:32:09.127' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (11, 7, 2, CAST(500.4700 AS Decimal(16, 4)), N'cuota 1/12 - prestamo personal', 5, 1, CAST(N'2019-12-05T15:40:34.490' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (12, 11, 2, CAST(750.5800 AS Decimal(16, 4)), N'cuota 12/12 prestamo hipotecario', 5, 1, CAST(N'2019-12-05T15:41:05.683' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (13, 7, 1, CAST(500.4700 AS Decimal(16, 4)), N'cuota 1/12 - prestamo personal', 5, 1, CAST(N'2019-12-05T15:48:00.967' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (14, 11, 1, CAST(750.5800 AS Decimal(16, 4)), N'cuota 12/12 prestamo hipotecario', 5, 1, CAST(N'2019-12-05T15:48:11.167' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (15, 4, 1, CAST(985.2400 AS Decimal(16, 4)), N'Ahorro Personal', 5, 1, CAST(N'2019-12-05T15:48:26.260' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (16, 7, 2, CAST(500.4700 AS Decimal(16, 4)), N'cuota 1/12 - prestamo personal', 5, 1, CAST(N'2019-12-05T15:51:32.873' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (17, 11, 2, CAST(750.5800 AS Decimal(16, 4)), N'cuota 12/12 prestamo hipotecario', 5, 1, CAST(N'2019-12-05T15:51:34.177' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (18, 4, 2, CAST(985.2400 AS Decimal(16, 4)), N'Ahorro Personal', 5, 1, CAST(N'2019-12-05T15:51:35.510' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (19, 7, 4, CAST(0.0000 AS Decimal(16, 4)), N'cuota 1/12 - prestamo personal', 5, 1, CAST(N'2019-12-05T23:32:56.680' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (20, 10, 4, CAST(500.0000 AS Decimal(16, 4)), N'cuota 12/12 prestamo hipotecario', 5, 1, CAST(N'2019-12-05T23:32:57.547' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbDeduccionInstitucionFinanciera] ([deif_IdDeduccionInstFinanciera], [emp_Id], [insf_IdInstitucionFinanciera], [deif_Monto], [deif_Comentarios], [cde_IdDeducciones], [deif_UsuarioCrea], [deif_FechaCrea], [deif_UsuarioModifica], [deif_FechaModifica], [deif_Activo], [deif_Pagado]) VALUES (21, 12, 4, CAST(0.0000 AS Decimal(16, 4)), N'Ahorro Personal', 5, 1, CAST(N'2019-12-05T23:32:58.357' AS DateTime), NULL, NULL, 1, 1)
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (1, 1, 2, CAST(500.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), 1, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-05T23:39:22.693' AS DateTime), 1, CAST(N'2019-12-09T16:05:29.180' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (2, 4, 2, CAST(221.0000 AS Decimal(16, 4)), CAST(N'2019-12-05T23:38:43.000' AS DateTime), 1, 1, CAST(N'2019-12-05T23:38:43.040' AS DateTime), 1, CAST(N'2019-12-06T06:57:42.243' AS DateTime), 1, CAST(N'2019-12-09T16:05:45.433' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (3, 10, 2, CAST(8000.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T10:33:04.000' AS DateTime), 1, 1, CAST(N'2019-12-06T10:33:04.137' AS DateTime), 1, CAST(N'2019-12-06T10:36:08.477' AS DateTime), 1, CAST(N'2019-12-09T16:06:16.107' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (4, 1, 2, CAST(600.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T10:36:27.467' AS DateTime), 1, 1, CAST(N'2019-12-06T10:36:27.467' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T16:05:29.430' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (5, 3, 2, CAST(800.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T12:48:14.393' AS DateTime), 1, 1, CAST(N'2019-12-06T12:48:14.393' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T16:05:36.967' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (6, 13, 2, CAST(6000.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T14:53:14.217' AS DateTime), 0, 1, CAST(N'2019-12-06T14:53:14.217' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T14:30:23.903' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (7, 6, 2, CAST(600.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T18:55:59.323' AS DateTime), 1, 1, CAST(N'2019-12-06T18:55:59.323' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T16:05:55.100' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (8, 8, 2, CAST(67.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T18:56:09.017' AS DateTime), 0, 1, CAST(N'2019-12-06T18:56:09.017' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T14:30:16.783' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (9, 9, 2, CAST(1000.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T18:56:17.603' AS DateTime), 1, 1, CAST(N'2019-12-06T18:56:17.603' AS DateTime), NULL, NULL, 1, CAST(N'2019-12-09T16:06:09.403' AS DateTime))
INSERT [Plani].[tbEmpleadoBonos] ([cb_Id], [emp_Id], [cin_IdIngreso], [cb_Monto], [cb_FechaRegistro], [cb_Pagado], [cb_UsuarioCrea], [cb_FechaCrea], [cb_UsuarioModifica], [cb_FechaModifica], [cb_Activo], [cb_FechaPagado]) VALUES (10, 10, 2, CAST(10000.0000 AS Decimal(16, 4)), CAST(N'2019-12-06T18:56:25.000' AS DateTime), 1, 1, CAST(N'2019-12-06T18:56:25.240' AS DateTime), 1, CAST(N'2019-12-06T18:56:35.957' AS DateTime), 1, CAST(N'2019-12-09T16:06:16.107' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (1, 12, 8, CAST(N'2019-12-05T21:05:15.463' AS DateTime), 1, 1, CAST(N'2019-12-05T21:05:16.067' AS DateTime), 1, CAST(N'2019-12-05T21:07:06.643' AS DateTime), 1, CAST(10.0000 AS Decimal(16, 4)), CAST(900.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:29.427' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (2, 9, 8, CAST(N'2019-12-05T21:07:29.667' AS DateTime), 1, 1, CAST(N'2019-12-05T21:07:29.667' AS DateTime), 1, CAST(N'2019-12-05T21:07:45.180' AS DateTime), 1, CAST(90.0000 AS Decimal(16, 4)), CAST(67.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:07.803' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (3, 4, 8, CAST(N'2019-12-06T03:21:37.067' AS DateTime), 1, 1, CAST(N'2019-12-06T03:21:37.067' AS DateTime), 1, CAST(N'2019-12-06T03:23:56.137' AS DateTime), 1, CAST(1.3400 AS Decimal(16, 4)), CAST(20000.3300 AS Decimal(16, 4)), CAST(N'2019-12-09T16:05:43.927' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (4, 10, 8, CAST(N'2019-12-06T03:21:44.350' AS DateTime), 1, 1, CAST(N'2019-12-06T03:21:44.350' AS DateTime), 1, CAST(N'2019-12-06T03:23:49.223' AS DateTime), 1, CAST(1.5000 AS Decimal(16, 4)), CAST(10000.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:15.327' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (5, 11, 8, CAST(N'2019-12-06T10:39:59.897' AS DateTime), 1, 1, CAST(N'2019-12-06T10:39:59.897' AS DateTime), 1, CAST(N'2019-12-06T10:41:04.793' AS DateTime), 1, CAST(2.0000 AS Decimal(16, 4)), CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:22.293' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (6, 12, 8, CAST(N'2019-12-06T12:46:27.360' AS DateTime), 1, 1, CAST(N'2019-12-06T12:46:27.360' AS DateTime), NULL, NULL, 1, CAST(1.5000 AS Decimal(16, 4)), CAST(30000.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:29.960' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (7, 9, 8, CAST(N'2019-12-06T18:57:43.423' AS DateTime), 1, 1, CAST(N'2019-12-06T18:57:43.423' AS DateTime), NULL, NULL, 1, CAST(1.5000 AS Decimal(16, 4)), CAST(900.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:08.337' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (8, 12, 8, CAST(N'2019-12-06T18:58:04.170' AS DateTime), 1, 1, CAST(N'2019-12-06T18:58:04.170' AS DateTime), NULL, NULL, 1, CAST(50.0000 AS Decimal(16, 4)), CAST(900.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:30.497' AS DateTime))
INSERT [Plani].[tbEmpleadoComisiones] ([cc_Id], [emp_Id], [cin_IdIngreso], [cc_FechaRegistro], [cc_Pagado], [cc_UsuarioCrea], [cc_FechaCrea], [cc_UsuarioModifica], [cc_FechaModifica], [cc_Activo], [cc_PorcentajeComision], [cc_TotalVenta], [cc_FechaPagado]) VALUES (9, 7, 8, CAST(N'2019-12-06T18:58:16.207' AS DateTime), 1, 1, CAST(N'2019-12-06T18:58:16.207' AS DateTime), NULL, NULL, 1, CAST(2.0000 AS Decimal(16, 4)), CAST(300.0000 AS Decimal(16, 4)), CAST(N'2019-12-09T16:06:00.897' AS DateTime))
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (1, N'Transferencia Bancaria', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (2, N'Efectivo', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (3, N'Cheque', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-05T12:36:08.557' AS DateTime), 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (4, N'An', 1, CAST(N'2019-12-05T15:29:56.050' AS DateTime), 1, CAST(N'2019-12-06T14:37:10.700' AS DateTime), 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (5, N'FormaPago1', 1, CAST(N'2019-12-05T15:34:40.747' AS DateTime), 1, CAST(N'2019-12-06T14:19:00.877' AS DateTime), 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (6, N'A', 1, CAST(N'2019-12-05T15:34:44.490' AS DateTime), 1, CAST(N'2019-12-06T09:23:52.640' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (7, N'Anu', 1, CAST(N'2019-12-05T15:34:46.907' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (8, N'Anua', 1, CAST(N'2019-12-05T15:34:48.930' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (9, N'Anual', 1, CAST(N'2019-12-05T15:34:49.503' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (10, N'FormaPagoGenerica', 1, CAST(N'2019-12-05T15:45:39.710' AS DateTime), 1, CAST(N'2019-12-05T23:17:40.390' AS DateTime), 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (11, N'Semanal', 1, CAST(N'2019-12-05T15:50:48.233' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (12, N'Semanal', 1, CAST(N'2019-12-05T15:50:48.690' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (13, N'Anual', 1, CAST(N'2019-12-05T15:50:50.553' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (14, N'Anual', 1, CAST(N'2019-12-05T15:54:22.003' AS DateTime), 1, CAST(N'2019-12-06T14:37:16.827' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (15, N'Testeo01', 1, CAST(N'2019-12-06T10:42:23.940' AS DateTime), 1, CAST(N'2019-12-06T10:42:36.787' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (16, N'TESTEO22', 1, CAST(N'2019-12-06T10:44:34.603' AS DateTime), 1, CAST(N'2019-12-06T10:44:43.823' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (17, N'TESTEO33', 1, CAST(N'2019-12-06T10:44:59.787' AS DateTime), 1, CAST(N'2019-12-06T10:45:06.743' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (18, N'testeo44', 1, CAST(N'2019-12-06T10:45:37.593' AS DateTime), 1, CAST(N'2019-12-06T10:45:45.790' AS DateTime), 0)
INSERT [Plani].[tbFormaPago] ([fpa_IdFormaPago], [fpa_Descripcion], [fpa_UsuarioCrea], [fpa_FechaCrea], [fpa_UsuarioModifica], [fpa_FechaModifica], [fpa_Activo]) VALUES (19, N'Transacción Virtual', 1, CAST(N'2019-12-06T14:37:23.957' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbHistorialDeduccionPago] ([hidp_IdHistorialdeDeduPago], [cde_IdDeducciones], [hipa_IdHistorialDePago], [hidp_Total], [hidp_UsuarioCrea], [hidp_FechaCrea], [hidp_UsuarioModifica], [hidp_FechaModifica]) VALUES (1, 2, 1, CAST(200.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T20:02:02.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDeduccionPago] ([hidp_IdHistorialdeDeduPago], [cde_IdDeducciones], [hipa_IdHistorialDePago], [hidp_Total], [hidp_UsuarioCrea], [hidp_FechaCrea], [hidp_UsuarioModifica], [hidp_FechaModifica]) VALUES (2, 1, 2, CAST(2000.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T20:02:02.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDeduccionPago] ([hidp_IdHistorialdeDeduPago], [cde_IdDeducciones], [hipa_IdHistorialDePago], [hidp_Total], [hidp_UsuarioCrea], [hidp_FechaCrea], [hidp_UsuarioModifica], [hidp_FechaModifica]) VALUES (3, 5, 7, CAST(150.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-06T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDeduccionPago] ([hidp_IdHistorialdeDeduPago], [cde_IdDeducciones], [hipa_IdHistorialDePago], [hidp_Total], [hidp_UsuarioCrea], [hidp_FechaCrea], [hidp_UsuarioModifica], [hidp_FechaModifica]) VALUES (4, 2, 136, CAST(150.0000 AS Decimal(16, 4)), 1, CAST(N'2019-12-09T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDeIngresosPago] ([hip_IdHistorialDeIngresosPago], [hipa_IdHistorialDePago], [hip_FechaInicio], [hip_FechaFinal], [hip_UnidadesPagar], [hip_MedidaUnitaria], [hip_TotalPagar], [cin_IdIngreso], [hip_UsuarioCrea], [hip_FechaCrea], [hip_UsuarioModifica], [hip_FechaModifica]) VALUES (1, 1, CAST(N'2019-12-06T19:49:49.000' AS DateTime), CAST(N'2019-12-06T19:49:49.000' AS DateTime), 5, 1, CAST(100.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T19:49:49.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDeIngresosPago] ([hip_IdHistorialDeIngresosPago], [hipa_IdHistorialDePago], [hip_FechaInicio], [hip_FechaFinal], [hip_UnidadesPagar], [hip_MedidaUnitaria], [hip_TotalPagar], [cin_IdIngreso], [hip_UsuarioCrea], [hip_FechaCrea], [hip_UsuarioModifica], [hip_FechaModifica]) VALUES (2, 2, CAST(N'2019-12-06T19:49:49.000' AS DateTime), CAST(N'2019-12-06T19:49:49.000' AS DateTime), 6, 1, CAST(110.0000 AS Decimal(16, 4)), 2, 1, CAST(N'2019-12-06T19:49:49.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (1, 1, CAST(18000.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-01T00:00:00.000' AS DateTime), 2019, 11, 1, 1, CAST(N'2019-12-04T21:36:48.337' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (2, 1, CAST(25000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 1, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, CAST(3200.0000 AS Decimal(16, 4)), 0, CAST(100.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (3, 2, CAST(15000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 1, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, CAST(1500.0000 AS Decimal(16, 4)), 0, CAST(150.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (4, 3, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 1, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, CAST(1621.0000 AS Decimal(16, 4)), 0, CAST(1602.0100 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (5, 4, CAST(12000.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-16T00:00:00.000' AS DateTime), CAST(N'2019-03-15T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, CAST(1200.0000 AS Decimal(16, 4)), 0, CAST(120.3000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (6, 5, CAST(14000.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-16T00:00:00.000' AS DateTime), CAST(N'2019-03-15T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, CAST(2000.0000 AS Decimal(16, 4)), 0, CAST(135.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (7, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-02-01T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), 2019, 2, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (8, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (9, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (10, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), 2019, 5, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (11, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-06-01T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), 2019, 6, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (12, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-07-01T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), 2019, 7, 3, 1, CAST(N'2019-12-04T22:02:50.467' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (13, 7, CAST(18000.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), 2019, 1, 1, 1, CAST(N'2019-12-04T22:00:11.750' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (14, 7, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-02-01T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), 2019, 2, 1, 1, CAST(N'2019-12-04T22:01:43.420' AS DateTime), NULL, NULL, CAST(1100.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (15, 7, CAST(17500.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (16, 7, CAST(12000.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), 2019, 4, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (17, 7, CAST(11000.0000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), 2019, 5, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(800.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (18, 7, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-06-01T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), 2019, 6, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (19, 7, CAST(18781.0000 AS Decimal(16, 4)), CAST(N'2019-07-01T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), 2019, 7, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(1200.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (20, 7, CAST(65486.0000 AS Decimal(16, 4)), CAST(N'2019-08-01T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), 2019, 8, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (21, 7, CAST(14592.0000 AS Decimal(16, 4)), CAST(N'2019-09-01T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 9, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(600.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (22, 7, CAST(87535.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), 2019, 10, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(900.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (23, 7, CAST(15486.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), 2019, 11, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(1500.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (24, 7, CAST(54686.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-04T22:02:08.913' AS DateTime), NULL, NULL, CAST(1200.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (25, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-08-01T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), 2019, 8, 3, 1, CAST(N'2019-12-04T22:03:12.957' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (26, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-09-01T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 9, 3, 1, CAST(N'2019-12-04T22:03:12.957' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (27, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), 2019, 10, 3, 1, CAST(N'2019-12-04T22:03:12.957' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (28, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), 2019, 11, 3, 1, CAST(N'2019-12-04T22:03:12.957' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (29, 1, CAST(16000.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), 2019, 12, 3, 1, CAST(N'2019-12-04T22:03:12.957' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (30, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-01-15T00:00:00.000' AS DateTime), CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-02-15T00:00:00.000' AS DateTime), 2019, 2, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (31, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-03-15T00:00:00.000' AS DateTime), CAST(N'2019-03-15T00:00:00.000' AS DateTime), 2019, 3, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (32, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-03-15T00:00:00.000' AS DateTime), CAST(N'2019-04-15T00:00:00.000' AS DateTime), CAST(N'2019-04-15T00:00:00.000' AS DateTime), 2019, 4, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (33, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-04-15T00:00:00.000' AS DateTime), CAST(N'2019-05-15T00:00:00.000' AS DateTime), CAST(N'2019-05-15T00:00:00.000' AS DateTime), 2019, 5, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (34, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-05-15T00:00:00.000' AS DateTime), CAST(N'2019-06-15T00:00:00.000' AS DateTime), CAST(N'2019-06-15T00:00:00.000' AS DateTime), 2019, 6, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (35, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-06-15T00:00:00.000' AS DateTime), CAST(N'2019-07-15T00:00:00.000' AS DateTime), CAST(N'2019-07-15T00:00:00.000' AS DateTime), 2019, 7, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (36, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-07-15T00:00:00.000' AS DateTime), CAST(N'2019-08-15T00:00:00.000' AS DateTime), CAST(N'2019-08-15T00:00:00.000' AS DateTime), 2019, 8, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (37, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-08-15T00:00:00.000' AS DateTime), CAST(N'2019-09-15T00:00:00.000' AS DateTime), CAST(N'2019-09-15T00:00:00.000' AS DateTime), 2019, 9, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (38, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-09-15T00:00:00.000' AS DateTime), CAST(N'2019-10-15T00:00:00.000' AS DateTime), CAST(N'2019-10-15T00:00:00.000' AS DateTime), 2019, 10, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (39, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-10-15T00:00:00.000' AS DateTime), CAST(N'2019-11-15T00:00:00.000' AS DateTime), CAST(N'2019-11-15T00:00:00.000' AS DateTime), 2019, 11, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (40, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-11-15T00:00:00.000' AS DateTime), CAST(N'2019-12-15T00:00:00.000' AS DateTime), CAST(N'2019-12-15T00:00:00.000' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (41, 8, CAST(20000.0000 AS Decimal(16, 4)), CAST(N'2019-12-15T00:00:00.000' AS DateTime), CAST(N'2020-01-15T00:00:00.000' AS DateTime), CAST(N'2020-01-15T00:00:00.000' AS DateTime), 2020, 1, 1, 1, CAST(N'2019-12-04T22:11:44.920' AS DateTime), NULL, NULL, CAST(700.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (42, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-01-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 1, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (43, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-01-15T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 1, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (44, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-02-01T00:00:00.000' AS DateTime), CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 2, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (45, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-02-15T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 2, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (46, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 3, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (47, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-03-15T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 3, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (48, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (49, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-04-15T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (50, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-05-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 5, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (51, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-05-15T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 5, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (52, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-06-01T00:00:00.000' AS DateTime), CAST(N'2019-06-15T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 6, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (53, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-06-15T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 6, 3, 1, CAST(N'2019-12-04T22:20:14.853' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (54, 11, CAST(19000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), 2019, 1, 3, 1, CAST(N'2019-12-04T22:25:19.860' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (55, 11, CAST(19000.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-03-31T00:00:00.000' AS DateTime), 2019, 3, 3, 1, CAST(N'2019-12-04T22:25:54.920' AS DateTime), NULL, NULL, CAST(500.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (56, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), CAST(N'2019-01-30T00:00:00.000' AS DateTime), 2019, 1, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (57, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-02-01T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), CAST(N'2019-02-28T00:00:00.000' AS DateTime), 2019, 2, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (58, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-03-01T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), CAST(N'2019-03-30T00:00:00.000' AS DateTime), 2019, 3, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (59, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-04-01T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), CAST(N'2019-04-30T00:00:00.000' AS DateTime), 2019, 4, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (61, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), CAST(N'2019-05-30T00:00:00.000' AS DateTime), 2019, 5, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (62, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-06-01T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), CAST(N'2019-06-30T00:00:00.000' AS DateTime), 2019, 6, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (63, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-07-01T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), CAST(N'2019-07-30T00:00:00.000' AS DateTime), 2019, 7, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (64, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-08-01T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), CAST(N'2019-08-30T00:00:00.000' AS DateTime), 2019, 8, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (65, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-09-01T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), CAST(N'2019-09-30T00:00:00.000' AS DateTime), 2019, 9, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (66, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), CAST(N'2019-10-30T00:00:00.000' AS DateTime), 2019, 10, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (67, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), 2019, 11, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (68, 9, CAST(10500.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), 2019, 12, 3, 1, CAST(N'2019-12-04T22:27:34.360' AS DateTime), NULL, NULL, CAST(300.0000 AS Decimal(16, 4)), 0, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (69, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:27:05.297' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T00:27:05.297' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (74, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:58:39.637' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T00:58:39.637' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (79, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:01:43.183' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:01:43.183' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (84, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-10-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:03:19.973' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:03:19.973' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (89, 2, CAST(7309.9200 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-10-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:03:47.390' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:03:47.390' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (94, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:08:14.773' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:08:14.773' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (99, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:10:49.690' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:10:49.690' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (104, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:16:03.250' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:16:03.250' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (112, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:18:34.553' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:18:34.553' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (117, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T01:19:23.500' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T01:19:23.500' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (118, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:02.547' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:02.547' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (119, 4, CAST(-833.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:06.700' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:06.700' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (120, 7, CAST(-807.5000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:10.760' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:10.760' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (121, 11, CAST(-816.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:15.063' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:15.063' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (122, 12, CAST(-833.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:19.253' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:19.253' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (123, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:25.663' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:25.663' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (124, 5, CAST(-693.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:30.187' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:30.187' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (125, 8, CAST(-658.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:34.883' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:34.883' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (126, 10, CAST(-672.9800 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:40.367' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:40.367' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (127, 13, CAST(-665.0000 AS Decimal(16, 4)), CAST(N'2019-12-03T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T04:52:45.953' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T04:52:45.953' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (128, 1, CAST(3570.0500 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:35:34.430' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:35:34.430' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (129, 4, CAST(-833.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:35:39.423' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:35:39.423' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (130, 7, CAST(-807.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:35:44.913' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:35:44.913' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (131, 11, CAST(-816.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:35:50.763' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:35:50.763' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (132, 12, CAST(-833.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:35:56.180' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:35:56.180' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (133, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:36:04.663' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:36:04.663' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (134, 5, CAST(-693.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:36:10.777' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:36:10.777' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (135, 8, CAST(-658.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:36:16.253' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:36:16.253' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (136, 10, CAST(-672.9800 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:36:22.243' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:36:22.243' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (137, 13, CAST(-665.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T05:36:27.873' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T05:36:27.873' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (138, 1, CAST(3670.0500 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:04:58.247' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:04:58.247' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (139, 4, CAST(-433.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:05:05.190' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:05:05.190' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (140, 7, CAST(-807.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:05:11.123' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:05:11.123' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
GO
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (141, 11, CAST(-816.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:05:19.900' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:05:19.900' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (142, 12, CAST(7167.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:05:30.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:05:30.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (143, 3, CAST(-480.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:07:06.600' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:07:06.600' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (144, 6, CAST(-475.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:07:09.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:07:09.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (145, 9, CAST(-499.9400 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T11:07:12.980' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T11:07:12.980' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (146, 1, CAST(2919.6000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T14:59:31.280' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T14:59:31.280' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (147, 4, CAST(-833.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T14:59:36.993' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T14:59:36.993' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (148, 7, CAST(5992.5000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T14:59:42.900' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T14:59:42.900' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (149, 11, CAST(-816.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T14:59:48.557' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T14:59:48.557' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (150, 12, CAST(-383.0000 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-06T14:59:55.400' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T14:59:55.400' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (151, 1, CAST(2970.0500 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:00:30.283' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:00:30.283' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (152, 4, CAST(-219.5000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:00:39.393' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:00:39.393' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (153, 7, CAST(-807.5000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:00:47.547' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:00:47.547' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (154, 11, CAST(184.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:00:54.923' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:00:54.923' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (155, 12, CAST(-383.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:03.943' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:03.943' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (156, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:20.180' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:20.180' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (157, 5, CAST(-693.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:27.393' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:27.393' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (158, 8, CAST(-591.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:35.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:35.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (159, 10, CAST(9327.0200 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:43.850' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:43.850' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (160, 13, CAST(-665.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:01:52.057' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:01:52.057' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (161, 3, CAST(13926.0000 AS Decimal(16, 4)), CAST(N'2018-10-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:25:47.200' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:25:47.200' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (162, 6, CAST(10607.4000 AS Decimal(16, 4)), CAST(N'2018-10-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:25:54.090' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:25:54.090' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (163, 9, CAST(9240.1700 AS Decimal(16, 4)), CAST(N'2018-10-31T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:26:00.033' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:26:00.033' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (164, 3, CAST(13920.0000 AS Decimal(16, 4)), CAST(N'2018-06-20T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:27:22.203' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:27:22.203' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (165, 6, CAST(10607.4000 AS Decimal(16, 4)), CAST(N'2018-06-20T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:27:29.863' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:27:29.863' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (166, 9, CAST(9240.1700 AS Decimal(16, 4)), CAST(N'2018-06-20T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:27:42.680' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:27:42.680' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (167, 2, CAST(8240.5000 AS Decimal(16, 4)), CAST(N'2019-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:28:30.117' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:28:30.117' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (168, 5, CAST(22407.0000 AS Decimal(16, 4)), CAST(N'2019-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:28:38.647' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:28:38.647' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (169, 8, CAST(10505.4500 AS Decimal(16, 4)), CAST(N'2019-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:28:47.903' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:28:47.903' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (170, 10, CAST(3813.7400 AS Decimal(16, 4)), CAST(N'2019-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:28:57.053' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:28:57.053' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (171, 13, CAST(13465.0600 AS Decimal(16, 4)), CAST(N'2019-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:29:05.570' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:29:05.570' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (172, 3, CAST(13920.0000 AS Decimal(16, 4)), CAST(N'2019-04-17T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:47:13.513' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:47:13.513' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (173, 6, CAST(10607.4000 AS Decimal(16, 4)), CAST(N'2019-04-17T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:47:19.943' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:47:19.943' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (174, 9, CAST(9240.1700 AS Decimal(16, 4)), CAST(N'2019-04-17T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T19:47:27.093' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T19:47:27.093' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (175, 3, CAST(13920.0000 AS Decimal(16, 4)), CAST(N'2018-02-27T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T20:45:13.670' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T20:45:13.670' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (176, 6, CAST(10607.4000 AS Decimal(16, 4)), CAST(N'2018-02-27T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T20:45:22.607' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T20:45:22.607' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (177, 9, CAST(9240.1700 AS Decimal(16, 4)), CAST(N'2018-02-27T00:00:00.000' AS DateTime), CAST(N'2019-12-06T00:00:00.000' AS DateTime), CAST(N'2019-12-06T20:45:31.450' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-06T20:45:31.450' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (178, 4, CAST(13850.9200 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T08:58:26.580' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T08:58:26.580' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (179, 7, CAST(7163.9100 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T08:58:33.910' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T08:58:33.910' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (180, 11, CAST(13232.2600 AS Decimal(16, 4)), CAST(N'2019-11-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T08:58:41.440' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T08:58:41.440' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (181, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T09:01:17.353' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T09:01:17.353' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (182, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T09:01:24.917' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T09:01:24.917' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (183, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-10-01T00:00:00.000' AS DateTime), CAST(N'2019-11-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T09:01:33.707' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T09:01:33.707' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (184, 1, CAST(718.8500 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:31:50.480' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:31:50.480' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (185, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:32:18.890' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:32:18.890' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (186, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:32:31.177' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:32:31.177' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (187, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:32:48.367' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:32:48.367' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (188, 12, CAST(16315.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:33:39.560' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:33:39.560' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (189, 1, CAST(2219.3200 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:36:37.480' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:36:37.480' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (190, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:36:44.397' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:36:44.397' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (191, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:36:50.923' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:36:50.923' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (192, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:36:58.173' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:36:58.173' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (193, 12, CAST(16315.6000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:04.410' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:04.410' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (194, 2, CAST(3978.9200 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:13.817' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:13.817' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (195, 5, CAST(15558.5000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:20.167' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:20.167' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (196, 8, CAST(9989.5000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:26.347' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:26.347' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (197, 10, CAST(12387.6100 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:32.213' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:32.213' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (198, 13, CAST(15285.4000 AS Decimal(16, 4)), CAST(N'2019-01-08T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:37:38.903' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:37:38.903' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (199, 1, CAST(2219.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:40:11.510' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:40:11.510' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (200, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:40:18.517' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:40:18.517' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (201, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:40:25.240' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:40:25.240' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (202, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:40:32.133' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:40:32.133' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (203, 12, CAST(16315.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T12:40:39.167' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T12:40:39.167' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (204, 1, CAST(3319.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:11.920' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:11.920' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (205, 3, CAST(13198.7600 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:19.230' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:19.230' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (206, 4, CAST(15734.9000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:29.480' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:29.480' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (207, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:36.467' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:36.467' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (208, 7, CAST(8021.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:44.077' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:44.077' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (209, 9, CAST(9650.5300 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:51.310' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:51.310' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (210, 10, CAST(20387.6100 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:04:57.700' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:04:57.700' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (211, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:05:04.107' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:05:04.107' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (212, 12, CAST(17305.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:05:13.167' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:05:13.167' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (213, 1, CAST(2219.3200 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:11:44.597' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:11:44.597' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (214, 3, CAST(13584.0000 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:11:54.290' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:11:54.290' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (215, 4, CAST(14950.4000 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:05.160' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:05.160' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (216, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:12.940' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:12.940' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (217, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:21.033' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:21.033' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (218, 9, CAST(8650.5300 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:30.440' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:30.440' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (219, 10, CAST(2537.6100 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:39.143' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:39.143' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (220, 11, CAST(14884.0000 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:48.543' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:48.543' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (221, 12, CAST(20938.6000 AS Decimal(16, 4)), CAST(N'2019-11-24T00:00:00.000' AS DateTime), CAST(N'2019-12-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:12:59.177' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:12:59.177' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (222, 1, CAST(2219.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:15:40.573' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:15:40.573' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (223, 3, CAST(13584.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:15:50.020' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:15:50.020' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (224, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:15:59.783' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:15:59.783' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (225, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:06.950' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:06.950' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (226, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:14.803' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:14.803' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (227, 9, CAST(8590.2300 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:22.353' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:22.353' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (228, 10, CAST(2387.6100 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:29.837' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:29.837' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (229, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:37.503' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:37.503' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (230, 12, CAST(20398.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:16:45.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:16:45.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (231, 1, CAST(2219.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:21:17.867' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:21:17.867' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (232, 3, CAST(13584.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:21:26.043' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:21:26.043' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (233, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:21:34.077' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:21:34.077' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (234, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:21:42.140' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:21:42.140' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (235, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:21:50.023' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:21:50.023' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (236, 9, CAST(8603.7300 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:22:00.417' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:22:00.417' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (237, 10, CAST(2387.6100 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:22:08.593' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:22:08.593' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (238, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:22:16.200' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:22:16.200' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (239, 12, CAST(20848.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:22:25.207' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:22:25.207' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (240, 1, CAST(1719.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:26:49.270' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:26:49.270' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
GO
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (241, 3, CAST(13384.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:26:58.400' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:26:58.400' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (242, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:07.040' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:07.040' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (243, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:14.487' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:14.487' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (244, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:23.063' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:23.063' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (245, 9, CAST(8590.2300 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:30.740' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:30.740' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (246, 10, CAST(2387.6100 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:38.957' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:38.957' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (247, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:47.063' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:47.063' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (248, 12, CAST(20398.6000 AS Decimal(16, 4)), CAST(N'2019-12-01T00:00:00.000' AS DateTime), CAST(N'2019-12-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:27:56.547' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:27:56.547' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (249, 1, CAST(1818.8500 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:34:56.273' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:34:56.273' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (250, 3, CAST(12598.7600 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:04.857' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:04.857' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (251, 4, CAST(12611.9200 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:14.493' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:14.493' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (252, 6, CAST(9674.9000 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:22.193' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:22.193' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (253, 7, CAST(6363.9100 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:31.310' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:31.310' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (254, 9, CAST(8590.2300 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:40.027' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:40.027' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (255, 10, CAST(1887.6100 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:48.583' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:48.583' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (256, 11, CAST(12232.2600 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:35:56.637' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:35:56.637' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (257, 12, CAST(20398.6000 AS Decimal(16, 4)), CAST(N'2019-04-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:36:06.030' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:36:06.030' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (258, 1, CAST(2319.3200 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:02.983' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:02.983' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (259, 3, CAST(13584.0000 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:11.857' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:11.857' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (260, 4, CAST(14682.4000 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:20.770' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:20.770' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (261, 6, CAST(10274.9000 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:28.497' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:28.497' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (262, 7, CAST(8015.3200 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:36.547' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:36.547' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (263, 9, CAST(8590.2300 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:44.640' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:44.640' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (264, 10, CAST(2387.6100 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:37:52.757' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:37:52.757' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (265, 11, CAST(14484.0000 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:38:00.793' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:38:00.793' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (266, 12, CAST(20398.6000 AS Decimal(16, 4)), CAST(N'2018-06-25T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:38:09.347' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:38:09.347' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (267, 1, CAST(1749.3200 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:53:00.133' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:53:00.133' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (268, 3, CAST(13008.0000 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:53:23.057' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:53:23.057' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (269, 4, CAST(14094.4000 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:53:38.957' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:53:38.957' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (270, 6, CAST(9704.9000 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:53:51.950' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:53:51.950' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (271, 7, CAST(7415.3600 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:54:03.507' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:54:03.507' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (272, 9, CAST(7990.2700 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:54:18.647' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:54:18.647' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (273, 10, CAST(1810.7700 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:54:28.573' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:54:28.573' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (274, 11, CAST(13908.0000 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:55:23.673' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:55:23.673' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (275, 12, CAST(19810.6000 AS Decimal(16, 4)), CAST(N'2019-03-05T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T13:55:45.057' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T13:55:45.057' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (276, 1, CAST(2176.8200 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:05:37.623' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:05:37.623' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (277, 3, CAST(13440.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:05:44.550' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:05:44.550' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (278, 4, CAST(14535.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:05:50.697' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:05:50.697' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (279, 6, CAST(10132.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:05:57.203' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:05:57.203' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (280, 7, CAST(7865.3200 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:06:05.147' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:06:05.147' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (281, 9, CAST(8440.2300 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:06:12.940' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:06:12.940' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (282, 10, CAST(2243.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:06:19.700' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:06:19.700' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (283, 11, CAST(14340.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:06:27.077' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:06:27.077' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (284, 12, CAST(20251.6000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:06:34.813' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:06:34.813' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (285, 1, CAST(2271.8200 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:28.477' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:28.477' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (286, 3, CAST(13536.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:34.733' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:34.733' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (287, 4, CAST(14633.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:41.363' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:41.363' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (288, 6, CAST(10227.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:47.013' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:47.013' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (289, 7, CAST(8115.3000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:53.440' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:53.440' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (290, 9, CAST(8690.2100 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:07:59.213' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:07:59.213' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (291, 10, CAST(2339.5400 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:08:05.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:08:05.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (292, 11, CAST(14436.0000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:08:11.477' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:08:11.477' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (293, 12, CAST(20349.6000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:08:17.983' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:08:17.983' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (294, 1, CAST(1271.3500 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:07.940' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:07.940' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (295, 3, CAST(129470.7600 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:15.380' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:15.380' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (296, 4, CAST(249050.2800 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:22.607' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:22.607' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (297, 6, CAST(9627.4000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:28.140' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:28.140' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (298, 7, CAST(6463.8900 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:33.897' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:33.897' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (299, 9, CAST(8690.2100 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:39.823' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:39.823' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (300, 10, CAST(6967.2200 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:45.880' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:45.880' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (301, 11, CAST(12184.2600 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:51.983' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:51.983' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (302, 12, CAST(21982.8000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:12:58.757' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:12:58.757' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (303, 2, CAST(3693.9200 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:13:14.740' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:13:14.740' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (304, 5, CAST(15261.5000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:13:21.027' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:13:21.027' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (305, 8, CAST(9719.5000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:13:28.147' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:13:28.147' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (306, 13, CAST(15000.4000 AS Decimal(16, 4)), CAST(N'2019-05-01T00:00:00.000' AS DateTime), CAST(N'2019-12-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:13:34.600' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:13:34.600' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (307, 1, CAST(1271.3500 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:23:37.223' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:23:37.223' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (308, 3, CAST(129470.7600 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:23:44.750' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:23:44.750' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (309, 4, CAST(249050.2800 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:23:52.983' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:23:52.983' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (310, 6, CAST(9627.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:23:59.273' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:23:59.273' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (311, 7, CAST(6463.8900 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:05.300' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:05.300' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (312, 9, CAST(8690.2100 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:11.280' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:11.280' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (313, 10, CAST(6967.2200 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:18.083' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:18.083' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (314, 11, CAST(12184.2600 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:23.703' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:23.703' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (315, 12, CAST(21982.8000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:30.533' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:30.533' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (316, 2, CAST(3693.9200 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:46.690' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:46.690' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (317, 5, CAST(15261.5000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:24:53.010' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:24:53.010' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (318, 8, CAST(9652.5000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:25:00.107' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:25:00.107' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (319, 13, CAST(9000.4000 AS Decimal(16, 4)), CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:25:06.697' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:25:06.697' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (320, 1, CAST(3371.8200 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:28:40.770' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:28:40.770' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (321, 3, CAST(131456.0000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:28:48.680' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:28:48.680' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (322, 4, CAST(251609.7600 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:28:57.313' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:28:57.313' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (323, 6, CAST(10827.4000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:03.590' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:03.590' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (324, 7, CAST(8121.3000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:10.603' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:10.603' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (325, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:18.810' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:18.810' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (326, 10, CAST(25617.2200 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:26.303' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:26.303' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (327, 11, CAST(14836.0000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:37.547' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:37.547' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (328, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:29:48.083' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:29:48.083' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (329, 2, CAST(4844.5000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:06.180' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:06.180' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (330, 5, CAST(15261.5000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:14.570' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:14.570' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (331, 8, CAST(9719.5000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:21.983' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:21.983' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (332, 13, CAST(15000.4000 AS Decimal(16, 4)), CAST(N'2018-08-28T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:30.280' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:30.280' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (333, 1, CAST(2371.3500 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:36.373' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:36.373' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (334, 3, CAST(130270.7600 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:44.217' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:44.217' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (335, 4, CAST(249539.2800 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:52.283' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:52.283' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (336, 6, CAST(10227.4000 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:30:59.813' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:30:59.813' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (337, 7, CAST(6469.8900 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:31:06.963' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:31:06.963' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (338, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:31:14.890' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:31:14.890' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (339, 10, CAST(25117.2200 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:31:24.190' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:31:24.190' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (340, 11, CAST(12584.2600 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:31:31.380' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:31:31.380' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
GO
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (341, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2019-07-10T00:00:00.000' AS DateTime), CAST(N'2020-04-30T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:31:40.347' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:31:40.347' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (342, 1, CAST(2021.3500 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:02.487' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:02.487' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(350.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (343, 3, CAST(130170.7600 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:09.473' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:09.473' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(100.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (344, 4, CAST(248639.2800 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:17.930' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:17.930' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(900.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (345, 6, CAST(9927.4000 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:24.570' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:24.570' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(300.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (346, 7, CAST(6219.8900 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:30.803' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:30.803' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(250.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (347, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:37.637' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:37.637' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (348, 10, CAST(24417.2200 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:44.273' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:44.273' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(700.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (349, 11, CAST(12084.2600 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:50.660' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:50.660' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(500.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (350, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2019-01-07T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T14:35:59.250' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T14:35:59.250' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (351, 1, CAST(2021.3500 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:02.540' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:02.540' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(350.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (352, 3, CAST(130170.7600 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:10.043' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:10.043' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(100.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (353, 4, CAST(248639.2800 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:19.103' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:19.103' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(900.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (354, 6, CAST(9927.4000 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:25.110' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:25.110' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(300.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (355, 7, CAST(6219.8900 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:31.767' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:31.767' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(250.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (356, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:38.937' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:38.937' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (357, 10, CAST(24417.2200 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:46.120' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:46.120' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(700.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (358, 11, CAST(12084.2600 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:45:53.563' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:45:53.563' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(500.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (359, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:46:03.420' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:46:03.420' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (360, 1, CAST(1921.3500 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:57:22.633' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:57:22.633' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(350.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (361, 3, CAST(130170.7600 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:57:29.893' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:57:29.893' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(100.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (362, 4, CAST(248639.2800 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:57:38.190' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:57:38.190' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(900.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (363, 6, CAST(9927.4000 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:57:57.433' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:57:57.433' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(300.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (364, 7, CAST(6219.8900 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:58:04.860' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:58:04.860' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(250.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (365, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:58:12.567' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:58:12.567' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (366, 10, CAST(26219.9200 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:58:19.720' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:58:19.720' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(700.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (367, 11, CAST(12084.2600 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:58:26.227' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:58:26.227' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(500.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (368, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2018-11-27T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T15:58:34.157' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T15:58:34.157' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (369, 1, CAST(2171.8200 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:03:51.613' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:03:51.613' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (370, 3, CAST(130656.0000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:03:59.833' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:03:59.833' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (371, 4, CAST(251120.7600 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:06.803' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:06.803' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (372, 6, CAST(10227.4000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:13.573' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:13.573' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (373, 7, CAST(8115.3000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:19.943' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:19.943' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (374, 9, CAST(8690.2100 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:26.360' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:26.360' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (375, 10, CAST(8869.3200 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:33.003' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:33.003' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (376, 11, CAST(14436.0000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:40.477' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:40.477' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (377, 12, CAST(21982.8000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:04:47.353' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:04:47.353' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (378, 1, CAST(1921.3500 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:05:34.937' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:05:34.937' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(350.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (379, 3, CAST(130170.7600 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:05:42.537' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:05:42.537' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(100.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (380, 4, CAST(248639.2800 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:05:53.170' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:05:53.170' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(900.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (381, 6, CAST(9927.4000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:05:59.557' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:05:59.557' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(300.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (382, 7, CAST(6219.8900 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:06:06.477' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:06:06.477' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(250.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (383, 9, CAST(9764.0100 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:06:14.000' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:06:14.000' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (384, 10, CAST(25819.3200 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:06:21.043' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:06:21.043' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(700.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (385, 11, CAST(12084.2600 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:06:28.100' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:06:28.100' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(500.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago], [emp_Id], [hipa_SueldoNeto], [hipa_FechaInicio], [hipa_FechaFin], [hipa_FechaPago], [hipa_Anio], [hipa_Mes], [peri_IdPeriodo], [hipa_UsuarioCrea], [hipa_FechaCrea], [hipa_UsuarioModifica], [hipa_FechaModifica], [hipa_TotalISR], [hipa_ISRPendiente], [hipa_AFP]) VALUES (386, 12, CAST(22972.8000 AS Decimal(16, 4)), CAST(N'2018-01-02T00:00:00.000' AS DateTime), CAST(N'2019-12-09T00:00:00.000' AS DateTime), CAST(N'2019-12-09T16:06:37.013' AS DateTime), 2019, 12, 1, 1, CAST(N'2019-12-09T16:06:37.013' AS DateTime), NULL, NULL, CAST(0.0000 AS Decimal(16, 4)), 1, CAST(0.0000 AS Decimal(16, 4)))
INSERT [Plani].[tbHistorialLiquidaciones] ([hliq_Id], [emp_Id], [hliq_fechaIngreso], [hliq_fechaLiquidacion], [hliq_PorcentajeLiquidacion], [hliq_Observaciones], [hliq_Estado], [hliq_RazonInactivo], [hliq_UsuarioCrea], [hliq_FechaCrea], [hliq_UsuarioModifica], [hliq_FechaModifica]) VALUES (1, 1, CAST(N'2015-02-15T00:00:00.000' AS DateTime), CAST(N'2019-12-04T00:00:00.000' AS DateTime), 90000, N'N/A', 0, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [Plani].[tbInstitucionesFinancieras] ([insf_IdInstitucionFinanciera], [insf_DescInstitucionFinanc], [insf_Contacto], [insf_Telefono], [insf_Correo], [insf_UsuarioCrea], [insf_FechaCrea], [insf_UsuarioModifica], [insf_FechaModifica], [insf_Activo]) VALUES (1, N'Cooperativa Sagrada Familia', N'Manuel Dominguez', N'96857459', N'sagradafamilia@hotmail.com', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbInstitucionesFinancieras] ([insf_IdInstitucionFinanciera], [insf_DescInstitucionFinanc], [insf_Contacto], [insf_Telefono], [insf_Correo], [insf_UsuarioCrea], [insf_FechaCrea], [insf_UsuarioModifica], [insf_FechaModifica], [insf_Activo]) VALUES (2, N'Cooperativa Taulabe', N'Enrique Salomon', N'32165948', N'taulabe@gmail.com', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbInstitucionesFinancieras] ([insf_IdInstitucionFinanciera], [insf_DescInstitucionFinanc], [insf_Contacto], [insf_Telefono], [insf_Correo], [insf_UsuarioCrea], [insf_FechaCrea], [insf_UsuarioModifica], [insf_FechaModifica], [insf_Activo]) VALUES (3, N'Cooperativa Arsenaut', N'Monserrath Baldez', N'36521498', N'CooArsenault@gamail.com', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbInstitucionesFinancieras] ([insf_IdInstitucionFinanciera], [insf_DescInstitucionFinanc], [insf_Contacto], [insf_Telefono], [insf_Correo], [insf_UsuarioCrea], [insf_FechaCrea], [insf_UsuarioModifica], [insf_FechaModifica], [insf_Activo]) VALUES (4, N'Cooperativa Elga', N'Marvin Bañdez', N'9776777', N'CooElga@gmail.com', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-06T06:24:29.707' AS DateTime), 0)
INSERT [Plani].[tbISR] ([isr_Id], [isr_RangoInicial], [isr_RangoFinal], [isr_Porcentaje], [tde_IdTipoDedu], [isr_UsuarioCrea], [isr_FechaCrea], [isr_UsuarioModifica], [isr_FechaModifica], [isr_Activo]) VALUES (1, CAST(2.0000 AS Decimal(16, 4)), CAST(16000.0000 AS Decimal(16, 4)), CAST(15.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-04T14:08:25.000' AS DateTime), 1, CAST(N'2019-12-06T12:44:28.757' AS DateTime), 0)
INSERT [Plani].[tbISR] ([isr_Id], [isr_RangoInicial], [isr_RangoFinal], [isr_Porcentaje], [tde_IdTipoDedu], [isr_UsuarioCrea], [isr_FechaCrea], [isr_UsuarioModifica], [isr_FechaModifica], [isr_Activo]) VALUES (2, CAST(18000.0000 AS Decimal(16, 4)), CAST(25000.0000 AS Decimal(16, 4)), CAST(15.0000 AS Decimal(16, 4)), 3, 1, CAST(N'2019-12-04T14:11:54.000' AS DateTime), 1, CAST(N'2019-12-06T12:43:32.783' AS DateTime), 1)
INSERT [Plani].[tbISR] ([isr_Id], [isr_RangoInicial], [isr_RangoFinal], [isr_Porcentaje], [tde_IdTipoDedu], [isr_UsuarioCrea], [isr_FechaCrea], [isr_UsuarioModifica], [isr_FechaModifica], [isr_Activo]) VALUES (3, CAST(200.0000 AS Decimal(16, 4)), CAST(1201.0000 AS Decimal(16, 4)), CAST(15.0000 AS Decimal(16, 4)), 2, 1, CAST(N'2019-12-06T12:35:53.107' AS DateTime), 1, CAST(N'2019-12-06T14:26:29.753' AS DateTime), 1)
INSERT [Plani].[tbISR] ([isr_Id], [isr_RangoInicial], [isr_RangoFinal], [isr_Porcentaje], [tde_IdTipoDedu], [isr_UsuarioCrea], [isr_FechaCrea], [isr_UsuarioModifica], [isr_FechaModifica], [isr_Activo]) VALUES (4, CAST(158000.0000 AS Decimal(16, 4)), CAST(220000.0000 AS Decimal(16, 4)), CAST(25.0000 AS Decimal(16, 4)), 1, 1, CAST(N'2019-12-06T13:19:50.080' AS DateTime), 1, CAST(N'2019-12-06T13:30:07.790' AS DateTime), 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (1, N'Mensual', 1, CAST(N'2019-12-04T19:43:16.090' AS DateTime), 1, CAST(N'2019-12-06T13:17:31.443' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (2, N'Semanal', 1, CAST(N'2019-05-09T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-06T13:55:27.267' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (3, N'Quincenale', 1, CAST(N'2019-07-03T02:15:00.000' AS DateTime), 1, CAST(N'2019-12-06T14:08:07.277' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (4, N'Anuale', 1, CAST(N'2019-12-05T15:57:09.257' AS DateTime), 1, CAST(N'2019-12-06T14:05:53.287' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (5, N'Trimestral', 1, CAST(N'2019-12-05T16:00:49.010' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (6, N'Bimestral', 1, CAST(N'2019-12-05T16:01:06.667' AS DateTime), 1, CAST(N'2019-12-09T12:17:30.980' AS DateTime), 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (7, N'Semestral', 1, CAST(N'2019-12-05T16:01:32.540' AS DateTime), 1, CAST(N'2019-12-09T12:20:14.883' AS DateTime), 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (8, N'Prueba', 1, CAST(N'2019-12-05T16:03:27.710' AS DateTime), 1, CAST(N'2019-12-09T12:19:54.403' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (9, N'Prueba2', 1, CAST(N'2019-12-05T16:14:20.210' AS DateTime), 1, CAST(N'2019-12-09T12:19:43.020' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (10, N'Prueba3', 1, CAST(N'2019-12-05T16:14:37.177' AS DateTime), 1, CAST(N'2019-12-09T12:19:40.043' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (11, N'Prueba4', 1, CAST(N'2019-12-05T16:15:27.650' AS DateTime), 1, CAST(N'2019-12-09T12:19:36.150' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (12, N'Prueba5', 1, CAST(N'2019-12-05T16:16:00.770' AS DateTime), 1, CAST(N'2019-12-09T12:18:54.100' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (13, N'Prueba6', 1, CAST(N'2019-12-05T16:16:19.323' AS DateTime), 1, CAST(N'2019-12-09T12:18:48.573' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (14, N'Prueba7.1', 1, CAST(N'2019-12-05T16:17:20.763' AS DateTime), 1, CAST(N'2019-12-09T12:17:55.973' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (15, N'Prueba1', 1, CAST(N'2019-12-05T16:21:32.437' AS DateTime), 1, CAST(N'2019-12-06T13:55:21.010' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (16, N'Prueba1.', 1, CAST(N'2019-12-05T16:22:52.363' AS DateTime), 1, CAST(N'2019-12-06T14:08:00.470' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (17, N'prueba 1.1', 1, CAST(N'2019-12-05T21:33:52.450' AS DateTime), 1, CAST(N'2019-12-09T12:18:33.303' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (18, N'prueba 1.13', 1, CAST(N'2019-12-05T21:33:53.800' AS DateTime), 1, CAST(N'2019-12-06T14:08:35.403' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (19, N'prueba 1.1', 1, CAST(N'2019-12-05T21:33:55.587' AS DateTime), 1, CAST(N'2019-12-06T13:00:16.353' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (20, N'prueba 1.2', 1, CAST(N'2019-12-05T21:34:04.133' AS DateTime), 1, CAST(N'2019-12-06T13:00:11.117' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (21, N'Mensual', 1, CAST(N'2019-12-06T12:36:21.637' AS DateTime), 1, CAST(N'2019-12-06T13:00:23.570' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (22, N'Mensual', 1, CAST(N'2019-12-06T12:36:21.780' AS DateTime), 1, CAST(N'2019-12-06T13:00:19.970' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (23, N'Mensual', 1, CAST(N'2019-12-06T12:36:28.603' AS DateTime), 1, CAST(N'2019-12-09T12:19:31.327' AS DateTime), 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (24, N'Anual', 1, CAST(N'2019-12-06T13:46:50.373' AS DateTime), 1, CAST(N'2019-12-06T14:07:54.953' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (25, N'i', 1, CAST(N'2019-12-06T13:53:37.373' AS DateTime), 1, CAST(N'2019-12-06T14:01:48.537' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (26, N'1', 1, CAST(N'2019-12-06T13:53:39.587' AS DateTime), 1, CAST(N'2019-12-06T14:22:26.427' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (27, N'Prueba200', 1, CAST(N'2019-12-06T14:57:13.333' AS DateTime), 1, CAST(N'2019-12-09T12:18:44.597' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (28, N'r', 1, CAST(N'2019-12-06T15:01:25.610' AS DateTime), 1, CAST(N'2019-12-09T12:05:44.097' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (29, N's', 1, CAST(N'2019-12-06T15:05:22.300' AS DateTime), 1, CAST(N'2019-12-09T12:05:41.367' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (30, N'tt', 1, CAST(N'2019-12-06T15:08:07.607' AS DateTime), 1, CAST(N'2019-12-09T12:05:38.187' AS DateTime), 0)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (31, N'Anual', 1, CAST(N'2019-12-09T12:28:27.653' AS DateTime), 1, CAST(N'2019-12-09T14:30:41.260' AS DateTime), 1)
INSERT [Plani].[tbPeriodos] ([peri_IdPeriodo], [peri_DescripPeriodo], [peri_UsuarioCrea], [peri_FechaCrea], [peri_UsuarioModifica], [peri_FechaModifica], [peri_Activo]) VALUES (32, N'Semanal', 1, CAST(N'2019-12-09T12:28:54.437' AS DateTime), 1, CAST(N'2019-12-09T14:27:05.373' AS DateTime), 1)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (1, 2, 3, 7, 1, CAST(N'2019-12-08T23:27:10.263' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 1)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (2, 2, 3, 6, 1, CAST(N'2019-03-12T12:33:09.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 1)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (3, 4, 3, 2, 1, CAST(N'2019-12-03T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 1)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (4, 4, 3, 2, 1, CAST(N'2019-12-03T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (5, 4, 3, 2, 1, CAST(N'2019-09-12T03:50:29.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (6, 11, 2, 3, 1, CAST(N'2019-09-12T03:50:29.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (7, 4, 3, 2, 1, CAST(N'2019-09-12T03:50:29.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (8, 4, 3, 2, 1, CAST(N'2019-09-12T03:50:29.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbPreaviso] ([prea_IdPreaviso], [prea_RangoInicioMeses], [prea_RangoFinMeses], [prea_DiasPreaviso], [prea_UsuarioCrea], [prea_FechaCrea], [prea_UsuarioModifica], [prea_FechaModifica], [prea_Activo]) VALUES (9, 11, 2, 3, 1, CAST(N'2019-09-12T03:50:29.000' AS DateTime), 1, CAST(N'2019-12-09T03:58:49.000' AS DateTime), 0)
INSERT [Plani].[tbTechosDeducciones] ([tddu_IdTechosDeducciones], [tddu_PorcentajeColaboradores], [tddu_PorcentajeEmpresa], [tddu_Techo], [cde_IdDeducciones], [tddu_Activo], [tddu_UsuarioCrea], [tddu_FechaCrea], [tddu_UsuarioModifica], [tddu_FechaModifica]) VALUES (1, CAST(3.5000 AS Decimal(16, 4)), CAST(3.5000 AS Decimal(16, 4)), CAST(5600.0000 AS Decimal(16, 4)), 1, 1, 1, CAST(N'2019-12-04T20:15:32.923' AS DateTime), 1, CAST(N'2019-12-06T04:57:28.243' AS DateTime))
INSERT [Plani].[tbTechosDeducciones] ([tddu_IdTechosDeducciones], [tddu_PorcentajeColaboradores], [tddu_PorcentajeEmpresa], [tddu_Techo], [cde_IdDeducciones], [tddu_Activo], [tddu_UsuarioCrea], [tddu_FechaCrea], [tddu_UsuarioModifica], [tddu_FechaModifica]) VALUES (2, CAST(4.5000 AS Decimal(16, 4)), CAST(3.5000 AS Decimal(16, 4)), CAST(8000.0000 AS Decimal(16, 4)), 2, 1, 1, CAST(N'2019-12-05T20:19:05.723' AS DateTime), 1, CAST(N'2019-12-05T20:22:46.813' AS DateTime))
INSERT [Plani].[tbTechosDeducciones] ([tddu_IdTechosDeducciones], [tddu_PorcentajeColaboradores], [tddu_PorcentajeEmpresa], [tddu_Techo], [cde_IdDeducciones], [tddu_Activo], [tddu_UsuarioCrea], [tddu_FechaCrea], [tddu_UsuarioModifica], [tddu_FechaModifica]) VALUES (3, CAST(2.5000 AS Decimal(16, 4)), CAST(1.5000 AS Decimal(16, 4)), CAST(9900.0000 AS Decimal(16, 4)), 3, 1, 1, CAST(N'2019-12-06T10:46:30.220' AS DateTime), 1, CAST(N'2019-12-06T10:46:46.107' AS DateTime))
INSERT [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu], [tde_Descripcion], [tde_UsuarioCrea], [tde_FechaCrea], [tde_UsuarioModifica], [tde_FechaModifica], [tde_Activo]) VALUES (1, N'Normal', 1, CAST(N'2019-12-01T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu], [tde_Descripcion], [tde_UsuarioCrea], [tde_FechaCrea], [tde_UsuarioModifica], [tde_FechaModifica], [tde_Activo]) VALUES (2, N'Institución Financiera', 1, CAST(N'2019-04-22T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu], [tde_Descripcion], [tde_UsuarioCrea], [tde_FechaCrea], [tde_UsuarioModifica], [tde_FechaModifica], [tde_Activo]) VALUES (3, N'Deduccion Extraordinaria 1', 1, CAST(N'2018-10-19T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-06T10:29:44.940' AS DateTime), 1)
INSERT [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu], [tde_Descripcion], [tde_UsuarioCrea], [tde_FechaCrea], [tde_UsuarioModifica], [tde_FechaModifica], [tde_Activo]) VALUES (4, N'ok', 1, CAST(N'2019-12-06T10:29:55.553' AS DateTime), 1, CAST(N'2019-12-06T10:30:02.143' AS DateTime), 0)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (1, 4, 1, 1, CAST(N'2019-12-04T14:11:21.553' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (2, 4, 2, 1, CAST(N'2019-12-04T14:11:21.683' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (5, 1, 1, 1, CAST(N'2019-12-04T14:12:11.107' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (7, 3, 1, 1, CAST(N'2019-12-04T14:13:11.830' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (8, 3, 2, 1, CAST(N'2019-12-04T14:13:11.960' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (10, 4, 3, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (12, 1, 3, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (19, 4, 6, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (20, 1, 2, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (23, 5, 1, 1, CAST(N'2019-12-06T05:39:35.530' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (24, 5, 2, 1, CAST(N'2019-12-06T05:39:35.877' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (25, 5, 3, 1, CAST(N'2019-12-06T05:39:36.217' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (26, 5, 4, 1, CAST(N'2019-12-06T05:39:36.380' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (28, 5, 6, 1, CAST(N'2019-12-06T05:39:36.903' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (29, 2, 3, 1, CAST(N'2019-12-07T00:07:20.040' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (30, 2, 2, 1, CAST(N'2019-12-07T00:07:33.987' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (31, 2, 1, 1, CAST(N'2019-12-07T00:07:56.150' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (32, 2, 4, 1, CAST(N'2019-12-07T00:07:56.277' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (34, 2, 6, 1, CAST(N'2019-12-07T00:07:56.617' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (36, 6, 2, 1, CAST(N'2019-12-09T13:15:31.870' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (37, 6, 3, 1, CAST(N'2019-12-09T13:15:32.220' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (40, 6, 6, 1, CAST(N'2019-12-09T13:15:33.127' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (43, 7, 3, 1, CAST(N'2019-12-09T13:25:53.853' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (44, 8, 1, 1, CAST(N'2019-12-09T13:34:16.787' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (46, 8, 2, 1, CAST(N'2019-12-09T13:37:01.567' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (47, 8, 3, 1, CAST(N'2019-12-09T13:37:01.833' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (48, 8, 4, 1, CAST(N'2019-12-09T13:37:02.070' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (49, 8, 6, 1, CAST(N'2019-12-09T13:37:02.343' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (50, 7, 1, 1, CAST(N'2019-12-09T14:10:13.433' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (51, 7, 2, 1, CAST(N'2019-12-09T14:10:13.683' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (52, 7, 4, 1, CAST(N'2019-12-09T14:10:13.930' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (54, 7, 6, 1, CAST(N'2019-12-09T14:10:14.427' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (55, 9, 1, 1, CAST(N'2019-12-09T14:23:41.080' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (56, 9, 2, 1, CAST(N'2019-12-09T14:23:41.340' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (57, 9, 3, 1, CAST(N'2019-12-09T14:23:41.597' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (58, 9, 5, 1, CAST(N'2019-12-09T14:23:41.863' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (59, 9, 4, 1, CAST(N'2019-12-09T14:26:03.057' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (60, 10, 1, 1, CAST(N'2019-12-09T14:35:20.427' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (61, 10, 5, 1, CAST(N'2019-12-09T14:35:20.697' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (62, 10, 2, 1, CAST(N'2019-12-09T14:35:54.287' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (63, 10, 3, 1, CAST(N'2019-12-09T14:35:54.553' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (64, 10, 4, 1, CAST(N'2019-12-09T14:35:54.813' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (65, 10, 6, 1, CAST(N'2019-12-09T14:35:55.093' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (66, 11, 1, 1, CAST(N'2019-12-09T14:38:28.993' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (67, 11, 2, 1, CAST(N'2019-12-09T14:38:29.293' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (68, 11, 3, 1, CAST(N'2019-12-09T14:38:29.543' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (69, 11, 4, 1, CAST(N'2019-12-09T14:38:29.810' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (70, 11, 5, 1, CAST(N'2019-12-09T14:38:30.067' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (71, 11, 6, 1, CAST(N'2019-12-09T14:38:30.327' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (72, 12, 1, 1, CAST(N'2019-12-09T14:38:57.123' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (73, 12, 3, 1, CAST(N'2019-12-09T14:38:57.410' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (74, 12, 6, 1, CAST(N'2019-12-09T14:38:57.667' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (75, 13, 1, 1, CAST(N'2019-12-09T14:39:53.143' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (76, 13, 4, 1, CAST(N'2019-12-09T14:39:53.413' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (77, 14, 1, 1, CAST(N'2019-12-09T14:40:35.750' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (78, 14, 2, 1, CAST(N'2019-12-09T14:40:36.013' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (79, 14, 3, 1, CAST(N'2019-12-09T14:40:36.280' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (80, 15, 1, 1, CAST(N'2019-12-09T14:41:14.827' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (81, 15, 2, 1, CAST(N'2019-12-09T14:41:15.087' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (82, 15, 3, 1, CAST(N'2019-12-09T14:41:15.330' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (83, 15, 4, 1, CAST(N'2019-12-09T14:41:15.597' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (84, 15, 5, 1, CAST(N'2019-12-09T14:41:15.833' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (85, 15, 6, 1, CAST(N'2019-12-09T14:41:16.080' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (86, 16, 1, 1, CAST(N'2019-12-09T14:41:38.380' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (87, 16, 2, 1, CAST(N'2019-12-09T14:41:38.653' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (88, 16, 3, 1, CAST(N'2019-12-09T14:41:38.913' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (89, 16, 4, 1, CAST(N'2019-12-09T14:41:39.183' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (90, 16, 5, 1, CAST(N'2019-12-09T14:41:39.677' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (91, 16, 6, 1, CAST(N'2019-12-09T14:41:39.997' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (92, 17, 2, 1, CAST(N'2019-12-09T14:42:20.520' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (93, 17, 4, 1, CAST(N'2019-12-09T14:42:20.807' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (94, 17, 5, 1, CAST(N'2019-12-09T14:42:21.043' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (95, 17, 6, 1, CAST(N'2019-12-09T14:42:21.310' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (96, 18, 1, 1, CAST(N'2019-12-09T14:43:21.797' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (97, 18, 2, 1, CAST(N'2019-12-09T14:43:22.053' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (98, 18, 3, 1, CAST(N'2019-12-09T14:43:22.303' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (99, 18, 4, 1, CAST(N'2019-12-09T14:43:22.587' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (100, 18, 5, 1, CAST(N'2019-12-09T14:43:22.837' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (101, 18, 6, 1, CAST(N'2019-12-09T14:43:23.110' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (102, 19, 5, 1, CAST(N'2019-12-09T15:02:05.340' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleDeduccion] ([tpdd_IdPlanillaDetDeduccion], [cpla_IdPlanilla], [cde_IdDeducciones], [tpdd_UsuarioCrea], [tpdd_FechaCrea], [tpdd_UsuarioModifica], [tpdd_FechaModifica], [tpdd_Activo]) VALUES (103, 19, 6, 1, CAST(N'2019-12-09T15:02:05.613' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (5, 3, 5, 1, CAST(N'2019-12-07T00:01:33.797' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (6, 1, 5, 1, CAST(N'2019-12-07T00:02:46.013' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (8, 4, 5, 1, CAST(N'2019-12-07T00:03:03.343' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (9, 5, 5, 1, CAST(N'2019-12-07T00:03:03.560' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (10, 7, 5, 1, CAST(N'2019-12-07T00:03:03.687' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (11, 1, 2, 1, CAST(N'2019-12-07T00:03:35.077' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (12, 2, 2, 1, CAST(N'2019-12-07T00:09:04.183' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (13, 3, 2, 1, CAST(N'2019-12-07T00:09:04.310' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (14, 4, 2, 1, CAST(N'2019-12-07T00:09:04.437' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (15, 5, 2, 1, CAST(N'2019-12-07T00:09:04.737' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (16, 7, 2, 1, CAST(N'2019-12-07T00:09:04.860' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (17, 2, 5, 1, CAST(N'2019-12-09T12:54:04.790' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (18, 1, 3, 1, CAST(N'2019-12-09T13:09:53.180' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (19, 2, 3, 1, CAST(N'2019-12-09T13:09:53.477' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (20, 3, 3, 1, CAST(N'2019-12-09T13:09:53.813' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (21, 4, 3, 1, CAST(N'2019-12-09T13:09:54.280' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (22, 5, 3, 1, CAST(N'2019-12-09T13:09:54.500' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (23, 7, 3, 1, CAST(N'2019-12-09T13:09:54.843' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (24, 1, 1, 1, CAST(N'2019-12-09T13:13:14.483' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (25, 2, 1, 1, CAST(N'2019-12-09T13:13:14.880' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (26, 3, 1, 1, CAST(N'2019-12-09T13:13:15.237' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (27, 4, 1, 1, CAST(N'2019-12-09T13:13:15.407' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (28, 5, 1, 1, CAST(N'2019-12-09T13:13:15.790' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (29, 7, 1, 1, CAST(N'2019-12-09T13:13:16.073' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (31, 2, 6, 1, CAST(N'2019-12-09T13:15:29.793' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (32, 3, 6, 1, CAST(N'2019-12-09T13:15:30.140' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (33, 4, 6, 1, CAST(N'2019-12-09T13:15:30.473' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (35, 7, 6, 1, CAST(N'2019-12-09T13:15:31.177' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (38, 3, 7, 1, CAST(N'2019-12-09T13:25:52.077' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (39, 2, 8, 1, CAST(N'2019-12-09T13:34:15.797' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (40, 4, 8, 1, CAST(N'2019-12-09T13:34:16.207' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (41, 7, 8, 1, CAST(N'2019-12-09T13:34:16.463' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (42, 1, 8, 1, CAST(N'2019-12-09T13:37:02.733' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (43, 3, 8, 1, CAST(N'2019-12-09T13:37:03.183' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (44, 5, 8, 1, CAST(N'2019-12-09T13:37:03.430' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (45, 1, 7, 1, CAST(N'2019-12-09T14:10:14.680' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (46, 2, 7, 1, CAST(N'2019-12-09T14:10:14.933' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (47, 4, 7, 1, CAST(N'2019-12-09T14:10:15.183' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (48, 5, 7, 1, CAST(N'2019-12-09T14:10:15.430' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (49, 7, 7, 1, CAST(N'2019-12-09T14:10:15.677' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (50, 1, 9, 1, CAST(N'2019-12-09T14:23:39.747' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (51, 2, 9, 1, CAST(N'2019-12-09T14:23:39.993' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (52, 3, 9, 1, CAST(N'2019-12-09T14:23:40.293' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (53, 4, 9, 1, CAST(N'2019-12-09T14:23:40.570' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (54, 7, 9, 1, CAST(N'2019-12-09T14:23:40.803' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (55, 1, 10, 1, CAST(N'2019-12-09T14:35:19.393' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (56, 3, 10, 1, CAST(N'2019-12-09T14:35:19.650' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (57, 5, 10, 1, CAST(N'2019-12-09T14:35:19.900' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (58, 7, 10, 1, CAST(N'2019-12-09T14:35:20.167' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (59, 1, 11, 1, CAST(N'2019-12-09T14:38:27.447' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (60, 2, 11, 1, CAST(N'2019-12-09T14:38:27.700' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (61, 3, 11, 1, CAST(N'2019-12-09T14:38:27.950' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (62, 4, 11, 1, CAST(N'2019-12-09T14:38:28.250' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (63, 5, 11, 1, CAST(N'2019-12-09T14:38:28.497' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (64, 7, 11, 1, CAST(N'2019-12-09T14:38:28.723' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (65, 1, 12, 1, CAST(N'2019-12-09T14:38:56.307' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (66, 3, 12, 1, CAST(N'2019-12-09T14:38:56.590' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (67, 7, 12, 1, CAST(N'2019-12-09T14:38:56.857' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (68, 3, 13, 1, CAST(N'2019-12-09T14:39:52.333' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (69, 5, 13, 1, CAST(N'2019-12-09T14:39:52.587' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (70, 7, 13, 1, CAST(N'2019-12-09T14:39:52.853' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (71, 1, 14, 1, CAST(N'2019-12-09T14:40:34.143' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (72, 2, 14, 1, CAST(N'2019-12-09T14:40:34.410' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (73, 3, 14, 1, CAST(N'2019-12-09T14:40:34.687' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (74, 4, 14, 1, CAST(N'2019-12-09T14:40:34.950' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (75, 5, 14, 1, CAST(N'2019-12-09T14:40:35.207' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (76, 7, 14, 1, CAST(N'2019-12-09T14:40:35.463' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (77, 1, 15, 1, CAST(N'2019-12-09T14:41:14.080' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (78, 2, 15, 1, CAST(N'2019-12-09T14:41:14.327' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (79, 5, 15, 1, CAST(N'2019-12-09T14:41:14.580' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (80, 1, 16, 1, CAST(N'2019-12-09T14:41:36.777' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (81, 2, 16, 1, CAST(N'2019-12-09T14:41:37.083' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (82, 3, 16, 1, CAST(N'2019-12-09T14:41:37.330' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (83, 4, 16, 1, CAST(N'2019-12-09T14:41:37.573' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (84, 5, 16, 1, CAST(N'2019-12-09T14:41:37.857' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (85, 7, 16, 1, CAST(N'2019-12-09T14:41:38.113' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (86, 1, 17, 1, CAST(N'2019-12-09T14:42:18.960' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (87, 2, 17, 1, CAST(N'2019-12-09T14:42:19.223' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (88, 3, 17, 1, CAST(N'2019-12-09T14:42:19.457' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (89, 4, 17, 1, CAST(N'2019-12-09T14:42:19.733' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (90, 5, 17, 1, CAST(N'2019-12-09T14:42:19.983' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (91, 7, 17, 1, CAST(N'2019-12-09T14:42:20.250' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (92, 2, 18, 1, CAST(N'2019-12-09T14:43:21.083' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (93, 4, 18, 1, CAST(N'2019-12-09T14:43:21.340' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (94, 7, 18, 1, CAST(N'2019-12-09T14:43:21.537' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (95, 5, 19, 1, CAST(N'2019-12-09T15:02:04.840' AS DateTime), NULL, NULL, 1)
INSERT [Plani].[tbTipoPlanillaDetalleIngreso] ([tpdi_IdDetallePlanillaIngreso], [cin_IdIngreso], [cpla_IdPlanilla], [tpdi_UsuarioCrea], [tpdi_FechaCrea], [tpdi_UsuarioModifica], [tpdi_FechaModifica], [tpdi_Activo]) VALUES (96, 7, 19, 1, CAST(N'2019-12-09T15:02:05.083' AS DateTime), NULL, NULL, 1)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (1, 1, 1, N'IT', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (2, 2, 1, N'Logistica', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (3, 8, 1, N'RRHH', 1, NULL, 1, CAST(N'2019-12-06T09:16:02.373' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (4, 11, 1, N'Ventas', 1, NULL, 1, CAST(N'2019-12-09T07:46:53.367' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (5, 15, 2, N'xddd', 1, NULL, 1, CAST(N'2019-12-09T09:32:44.137' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbAreas] ([area_Id], [car_Id], [suc_Id], [area_Descripcion], [area_Estado], [area_Razoninactivo], [area_Usuariocrea], [area_Fechacrea], [area_Usuariomodifica], [area_Fechamodifica]) VALUES (6, 17, 1, N'x', 1, NULL, 1, CAST(N'2019-12-09T09:52:47.137' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (1, N'Encargado de Reclutamiento', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-06T10:03:30.710' AS DateTime))
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (2, N'Gerente de Desarrollo Organizacional', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (3, N'Gerente de Recursos Humanos', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (4, N'Jefe de Reclutamiento y Selección', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (5, N'Jefe de Capacitación', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (6, N'Vendedor', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (7, N'm', 1, NULL, 1, CAST(N'2019-12-06T09:04:20.653' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (8, N'Gerente de RRHH', 1, NULL, 1, CAST(N'2019-12-06T09:16:02.373' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (9, N'gerente administrativo de RRHH', 1, NULL, 1, CAST(N'2019-12-06T09:16:25.443' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (10, N'Gerente de IT', 1, NULL, 1, CAST(N'2019-12-06T10:19:46.303' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (11, N'gerente de experto en ventas', 1, NULL, 1, CAST(N'2019-12-09T07:46:53.367' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (12, N'gerente experto en publicidad', 1, NULL, 1, CAST(N'2019-12-09T07:46:53.583' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (13, N'gerente de ventas ', 1, NULL, 1, CAST(N'2019-12-09T07:46:56.157' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (14, N'jefe de producción', 1, NULL, 1, CAST(N'2019-12-09T07:46:56.373' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (15, N'xdddddddd', 1, NULL, 1, CAST(N'2019-12-09T09:32:44.137' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (16, N'xdddd', 1, NULL, 1, CAST(N'2019-12-09T09:32:44.587' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (17, N'h', 1, NULL, 1, CAST(N'2019-12-09T09:52:47.137' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (18, N'y', 1, NULL, 1, CAST(N'2019-12-09T09:52:49.303' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCargos] ([car_Id], [car_Descripcion], [car_Estado], [car_RazonInactivo], [car_UsuarioCrea], [car_FechaCrea], [car_UsuarioModifica], [car_FechaModifica]) VALUES (19, N'x', 1, NULL, 1, CAST(N'2019-12-09T09:52:49.603' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbCompetencias] ([comp_Id], [comp_Descripcion], [comp_Estado], [comp_RazonInactivo], [comp_UsuarioCrea], [comp_FechaCrea], [comp_UsuarioModifica], [comp_FechaModifica]) VALUES (1, N'Competencia 1', 1, NULL, 1, CAST(N'2019-12-06T09:28:13.013' AS DateTime), 1, CAST(N'2019-12-06T10:05:39.510' AS DateTime))
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (1, 1, 1, N'Dirección General', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (2, 2, 3, N'Administración y Recursos Humanos', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (3, 3, 9, N'administracion de RRHH', 1, NULL, 1, CAST(N'2019-12-06T09:16:25.443' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (4, 4, 12, N'comunicaciones y diseño', 1, NULL, 1, CAST(N'2019-12-09T07:46:53.583' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (5, 4, 13, N'marketing Digital', 1, NULL, 1, CAST(N'2019-12-09T07:46:56.157' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (6, 4, 14, N'produccion', 1, NULL, 1, CAST(N'2019-12-09T07:46:56.373' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (7, 5, 16, N'xdddd', 1, NULL, 1, CAST(N'2019-12-09T09:32:44.587' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (8, 6, 18, N'd', 1, NULL, 1, CAST(N'2019-12-09T09:52:49.303' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbDepartamentos] ([depto_Id], [area_Id], [car_Id], [depto_Descripcion], [depto_Estado], [depto_RazonInactivo], [depto_UsuarioCrea], [depto_Fechacrea], [depto_UsuarioModifica], [depto_FechaModifica]) VALUES (9, 6, 19, N'x', 1, NULL, 1, CAST(N'2019-12-09T09:52:49.603' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, N'2276 3359 61 5444571570', 0, CAST(N'2019-11-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (2, 2, 2, 2, 2, 2, 2, 2, N'9175 8480 03 1152252040', 0, CAST(N'2019-01-03T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-01-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (3, 3, 6, 1, 1, 1, 1, 3, N'2872 4617 33 5307771574', 0, CAST(N'2018-11-04T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (4, 4, 6, 1, 1, 1, 1, 1, N'4347 0116 07 2437411632', 0, CAST(N'2017-01-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (5, 5, 2, 2, 2, 2, 2, 2, N'7978 0980 60 3990150858', 0, CAST(N'2019-01-06T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (6, 6, 6, 1, 1, 1, 1, 3, N'4767 1554 91 9749731690', 0, CAST(N'2015-06-02T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-06T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (7, 7, 1, 1, 1, 1, 1, 1, N'2933 2490 04 7563021528', 0, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (8, 8, 2, 2, 1, 2, 2, 1, N'9476 1239 46 4962662847', 0, CAST(N'2017-05-06T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-11-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (9, 9, 6, 1, 1, 1, 1, 3, N'4767 1554 91 9749731690', 0, CAST(N'2018-05-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (10, 10, 4, 1, 2, 2, 1, 2, N'4128 1623 98 1770224859', 0, CAST(N'2016-09-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-10-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (11, 11, 5, 1, 1, 1, 1, 2, N'2684 0584 17 1921749851', 0, CAST(N'2015-01-09T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2018-05-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (12, 12, 6, 2, 1, 1, 1, 1, N'5335 2953 83 9398546409', 0, CAST(N'2001-06-03T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpleados] ([emp_Id], [per_Id], [car_Id], [area_Id], [depto_Id], [jor_Id], [cpla_IdPlanilla], [fpa_IdFormaPago], [emp_CuentaBancaria], [emp_Reingreso], [emp_Fechaingreso], [emp_RazonSalida], [emp_CargoAnterior], [emp_FechaDeSalida], [emp_Estado], [emp_RazonInactivo], [emp_UsuarioCrea], [emp_FechaCrea], [emp_UsuarioModifica], [emp_FechaModifica]) VALUES (13, 13, 2, 2, 2, 2, 2, 2, N'4327 4631 77 8912651868', 0, CAST(N'2019-02-15T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (1, N'AHM', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (2, N'BI-DSS (TGU Edit1)', 0, N'Deudas', 1, CAST(N'2019-12-05T07:56:22.907' AS DateTime), 1, CAST(N'2019-12-05T08:02:38.650' AS DateTime))
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (3, N'BI-DSS (SPS)', 1, NULL, 1, CAST(N'2019-12-05T08:02:54.397' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (4, N'BI-DSS (TGU)', 1, NULL, 1, CAST(N'2019-12-05T08:30:32.710' AS DateTime), 1, CAST(N'2019-12-05T15:10:14.540' AS DateTime))
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (5, N'nnnnn', 0, N'xx', 1, CAST(N'2019-12-06T08:54:58.707' AS DateTime), 1, CAST(N'2019-12-06T09:26:53.837' AS DateTime))
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (6, N'ffff', 0, N'ggg', 1, CAST(N'2019-12-06T09:00:42.373' AS DateTime), 1, CAST(N'2019-12-06T09:26:49.030' AS DateTime))
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (7, N'GILDAN', 1, NULL, 1, CAST(N'2019-12-06T09:01:20.137' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEmpresas] ([empr_Id], [empr_Nombre], [empr_Estado], [empr_RazonInactivo], [empr_UsuarioCrea], [empr_FechaCrea], [empr_UsuarioModifica], [empr_FechaModifica]) VALUES (8, N'Stark Industries', 1, NULL, 1, CAST(N'2019-12-06T09:03:04.463' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEquipoEmpleados] ([eqem_Id], [emp_Id], [eqtra_Id], [eqem_Fecha], [eqem_Estado], [eqem_RazonInactivo], [eqem_UsuarioCrea], [eqem_FechaCrea], [eqem_UsuarioModifica], [eqem_FechaModifica]) VALUES (1, 1, 1, CAST(N'2019-12-04T20:51:44.537' AS DateTime), 1, N'N/A', 1, CAST(N'2019-12-04T20:51:44.537' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbEquipoTrabajo] ([eqtra_Id], [eqtra_Codigo], [eqtra_Descripcion], [eqtra_Observacion], [eqtra_Estado], [eqtra_RazonInactivo], [eqtra_UsuarioCrea], [eqtra_FechaCrea], [eqtra_UsuarioModifica], [eqtra_FechaModifica]) VALUES (1, N'A1', N'Computadora', N'Ninguna', 1, N'N/A', 1, CAST(N'2019-12-04T20:34:27.317' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbFaseSeleccion] ([fsel_Id], [fare_Id], [fsel_Fecha], [fsel_Estado], [fsel_RazonInactivo], [fsel_UsuarioCrea], [fsel_FechaCrea], [fsel_UsuarioModifica], [fsel_FechaModifica]) VALUES (1, 1, CAST(N'2016-02-02T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbFasesReclutamiento] ([fare_Id], [fare_Descripcion], [fare_Estado], [fare_RazonInactivo], [fare_UsuarioCrea], [fare_FechaCrea], [fare_UsuarioModifica], [fare_FechaModifica]) VALUES (1, N'Fase A', 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHabilidades] ([habi_Id], [habi_Descripcion], [habi_Estado], [habi_RazonInactivo], [habi_UsuarioCrea], [habi_FechaCrea], [habi_UsuarioModifica], [habi_FechaModifica]) VALUES (1, N'A', 1, NULL, 1, CAST(N'2019-12-06T08:56:33.800' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialAmonestaciones] ([hamo_Id], [emp_Id], [tamo_Id], [hamo_Fecha], [hamo_AmonestacionAnterior], [hamo_Observacion], [hamo_Estado], [hamo_RazonInactivo], [hamo_UsuarioCrea], [hamo_FechaCrea], [hamo_UsuarioModifica], [hamo_FechaModifica]) VALUES (1, 3, 1, CAST(N'2019-12-05T14:16:06.200' AS DateTime), 1, N'Llegada Tarde', 1, NULL, 1, CAST(N'2019-12-05T14:16:06.200' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialAmonestaciones] ([hamo_Id], [emp_Id], [tamo_Id], [hamo_Fecha], [hamo_AmonestacionAnterior], [hamo_Observacion], [hamo_Estado], [hamo_RazonInactivo], [hamo_UsuarioCrea], [hamo_FechaCrea], [hamo_UsuarioModifica], [hamo_FechaModifica]) VALUES (2, 2, 2, CAST(N'2019-12-09T02:02:06.957' AS DateTime), 1, N'Por ya es constante las llegadas tarde', 1, NULL, 1, CAST(N'2019-12-09T02:02:06.957' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialAmonestaciones] ([hamo_Id], [emp_Id], [tamo_Id], [hamo_Fecha], [hamo_AmonestacionAnterior], [hamo_Observacion], [hamo_Estado], [hamo_RazonInactivo], [hamo_UsuarioCrea], [hamo_FechaCrea], [hamo_UsuarioModifica], [hamo_FechaModifica]) VALUES (3, 2, 1, CAST(N'2019-12-09T02:05:23.050' AS DateTime), 2, N'Por ya es constante las llegadas tarde', 1, NULL, 1, CAST(N'2019-12-09T02:05:23.050' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialCargos] ([hcar_Id], [emp_Id], [car_IdAnterior], [car_IdNuevo], [hcar_Fecha], [hcar_Estado], [hcar_RazonInactivo], [hcar_UsuarioCrea], [hcar_FechaCrea], [hcar_UsuarioModifica], [hcar_FechaModifica]) VALUES (1, 1, 1, 5, CAST(N'2019-06-22T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-11-27T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialCargos] ([hcar_Id], [emp_Id], [car_IdAnterior], [car_IdNuevo], [hcar_Fecha], [hcar_Estado], [hcar_RazonInactivo], [hcar_UsuarioCrea], [hcar_FechaCrea], [hcar_UsuarioModifica], [hcar_FechaModifica]) VALUES (2, 2, 2, 4, CAST(N'2019-05-12T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialContrataciones] ([hcon_Id], [scan_Id], [depto_Id], [hcon_FechaContratado], [hcon_Estado], [hcon_RazonInactivo], [hcon_UsuarioCrea], [hcon_FechaCrea], [hcon_UsuarioModifica], [hcon_FechaModifica]) VALUES (1, 1, 1, CAST(N'2019-12-09' AS Date), 1, N's', 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-09T15:31:28.340' AS DateTime))
INSERT [rrhh].[tbHistorialContrataciones] ([hcon_Id], [scan_Id], [depto_Id], [hcon_FechaContratado], [hcon_Estado], [hcon_RazonInactivo], [hcon_UsuarioCrea], [hcon_FechaCrea], [hcon_UsuarioModifica], [hcon_FechaModifica]) VALUES (2, 1, 1, CAST(N'2010-12-09' AS Date), 1, NULL, 1, CAST(N'2019-12-09T14:34:03.323' AS DateTime), 1, CAST(N'2019-12-09T15:31:28.340' AS DateTime))
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (1, 1, 3, 1, 79, CAST(N'2019-11-02' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (2, 2, 3, 2, 150, CAST(N'2019-10-10' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (3, 3, 2, 1, 240, CAST(N'2019-10-21' AS Date), 1, NULL, 1, CAST(N'2029-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (4, 4, 1, 2, 100, CAST(N'2019-10-21' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (5, 5, 3, 1, 400, CAST(N'2019-10-25' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (6, 6, 1, 2, 200, CAST(N'2019-06-23' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (7, 7, 3, 3, 150, CAST(N'2019-09-12' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (8, 8, 2, 2, 190, CAST(N'2019-12-05' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (9, 9, 1, 2, 167, CAST(N'2019-06-25' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (10, 10, 3, 2, 115, CAST(N'2019-12-01' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (11, 11, 2, 2, 255, CAST(N'2019-11-09' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (12, 12, 1, 1, 300, CAST(N'2019-10-09' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (13, 13, 3, 2, 255, CAST(N'2019-08-09' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (14, 7, 1, 2, 50, CAST(N'2019-08-09' AS Date), 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (15, 4, 3, 1, 240, CAST(N'2019-10-23' AS Date), 1, NULL, 1, CAST(N'2019-12-09T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialHorasTrabajadas] ([htra_Id], [emp_Id], [tiho_Id], [jor_Id], [htra_CantidadHoras], [htra_Fecha], [htra_Estado], [htra_RazonInactivo], [htra_UsuarioCrea], [htra_FechaCrea], [htra_UsuarioModifica], [htra_FechaModifica]) VALUES (16, 12, 3, 1, 100, CAST(N'2019-10-23' AS Date), 1, NULL, 1, CAST(N'2019-12-09T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (1, 1, 1, 2, N'Hospital y clinica bendana', N'rubilio castillo', N'tos y gripe', CAST(N'1900-01-01T00:00:00.000' AS DateTime), CAST(N'1900-01-01T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-08T18:56:35.533' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (2, 1, 1, 3, N'rssacsdc', N'sdcsdc', N'sdcds', CAST(N'2000-12-12T00:00:00.000' AS DateTime), CAST(N'2000-12-12T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2000-12-12T00:00:00.000' AS DateTime), 1, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (3, 1, 2, 2, N'paz barahona', N'flores', N'prueba', CAST(N'2001-12-12T00:00:00.000' AS DateTime), CAST(N'2001-12-10T00:00:00.000' AS DateTime), 0, NULL, 1, CAST(N'2001-12-12T00:00:00.000' AS DateTime), 1, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (4, 1, 1, 7, N'catarino', N'pablo cesar', N'gastritis', CAST(N'2001-12-12T00:00:00.000' AS DateTime), CAST(N'2001-12-12T00:00:00.000' AS DateTime), 0, NULL, 1, CAST(N'2001-12-12T00:00:00.000' AS DateTime), 1, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (5, 1, 2, 3, N'paz barahona', N'wilmer', N'conjutivitis', CAST(N'2001-02-09T00:00:00.000' AS DateTime), CAST(N'2001-03-10T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-09T01:36:06.360' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (6, 1, 2, 3, N'hospital del valle', N'dos santos', N'fractura', CAST(N'2001-02-09T00:00:00.000' AS DateTime), CAST(N'2001-03-10T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-09T01:37:08.483' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (7, 2, 2, 3, N'hospital del valle', N'dos santos', N'fractura', CAST(N'2001-02-09T00:00:00.000' AS DateTime), CAST(N'2001-03-10T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-09T01:37:49.580' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialIncapacidades] ([hinc_Id], [emp_Id], [ticn_Id], [hinc_Dias], [hinc_CentroMedico], [hinc_Doctor], [hinc_Diagnostico], [hinc_FechaInicio], [hinc_FechaFin], [hinc_Estado], [hinc_RazonInactivo], [hinc_UsuarioCrea], [hinc_FechaCrea], [hinc_UsuarioModifica], [hinc_FechaModifica]) VALUES (8, 2, 2, 3, N'paz barahona', N'wilmer', N'conjutivitis', CAST(N'2001-02-09T00:00:00.000' AS DateTime), CAST(N'2001-03-10T00:00:00.000' AS DateTime), 1, NULL, 1, CAST(N'2019-12-09T01:38:41.390' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialPermisos] ([hper_Id], [emp_Id], [tper_Id], [hper_fechaInicio], [hper_fechaFin], [hper_Duracion], [hper_Observacion], [hper_PorcentajeIndemnizado], [hper_Estado], [hper_RazonInactivo], [hper_UsuarioCrea], [hper_FechaCrea], [hper_UsuarioModifica], [hper_FechaModifica]) VALUES (1, 1, 1, CAST(N'2019-12-06T14:48:41.547' AS DateTime), CAST(N'2019-12-06T14:48:41.547' AS DateTime), 8, N'visita medica', 25, 1, NULL, 1, CAST(N'2019-12-06T14:48:41.547' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialPermisos] ([hper_Id], [emp_Id], [tper_Id], [hper_fechaInicio], [hper_fechaFin], [hper_Duracion], [hper_Observacion], [hper_PorcentajeIndemnizado], [hper_Estado], [hper_RazonInactivo], [hper_UsuarioCrea], [hper_FechaCrea], [hper_UsuarioModifica], [hper_FechaModifica]) VALUES (2, 2, 2, CAST(N'2019-12-06T14:49:34.377' AS DateTime), CAST(N'2019-12-06T14:49:34.377' AS DateTime), 5, N'asuntos legales', 50, 1, NULL, 1, CAST(N'2019-12-06T14:49:34.377' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialPermisos] ([hper_Id], [emp_Id], [tper_Id], [hper_fechaInicio], [hper_fechaFin], [hper_Duracion], [hper_Observacion], [hper_PorcentajeIndemnizado], [hper_Estado], [hper_RazonInactivo], [hper_UsuarioCrea], [hper_FechaCrea], [hper_UsuarioModifica], [hper_FechaModifica]) VALUES (3, 3, 3, CAST(N'2019-12-06T14:50:33.610' AS DateTime), CAST(N'2019-12-06T14:50:33.610' AS DateTime), 5, N'desconocido', 25, 1, NULL, 1, CAST(N'2019-12-06T14:50:33.610' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialSalidas] ([hsal_Id], [emp_Id], [tsal_Id], [rsal_Id], [hsal_FechaSalida], [hsal_Observacion], [hsal_Estado], [hsal_RazonInactivo], [hsal_UsuarioCrea], [hsal_FechaCrea], [hsal_UsuarioModifica], [hsal_FechaModifica]) VALUES (1, 5, 5, 1, CAST(N'2019-12-08' AS Date), NULL, 1, NULL, 1, CAST(N'2019-12-06T04:31:02.870' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialSalidas] ([hsal_Id], [emp_Id], [tsal_Id], [rsal_Id], [hsal_FechaSalida], [hsal_Observacion], [hsal_Estado], [hsal_RazonInactivo], [hsal_UsuarioCrea], [hsal_FechaCrea], [hsal_UsuarioModifica], [hsal_FechaModifica]) VALUES (2, 4, 5, 1, CAST(N'2019-12-08' AS Date), NULL, 1, NULL, 1, CAST(N'2019-12-06T04:31:18.963' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialSalidas] ([hsal_Id], [emp_Id], [tsal_Id], [rsal_Id], [hsal_FechaSalida], [hsal_Observacion], [hsal_Estado], [hsal_RazonInactivo], [hsal_UsuarioCrea], [hsal_FechaCrea], [hsal_UsuarioModifica], [hsal_FechaModifica]) VALUES (3, 3, 5, 1, CAST(N'2019-12-08' AS Date), NULL, 1, NULL, 1, CAST(N'2019-12-06T04:31:21.900' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialSalidas] ([hsal_Id], [emp_Id], [tsal_Id], [rsal_Id], [hsal_FechaSalida], [hsal_Observacion], [hsal_Estado], [hsal_RazonInactivo], [hsal_UsuarioCrea], [hsal_FechaCrea], [hsal_UsuarioModifica], [hsal_FechaModifica]) VALUES (4, 6, 5, 1, CAST(N'2019-12-08' AS Date), NULL, 1, NULL, 1, CAST(N'2019-12-06T04:31:24.230' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialVacaciones] ([hvac_Id], [emp_Id], [hvac_FechaInicio], [hvac_FechaFin], [hvac_DiasTomados], [hvac_MesVacaciones], [hvac_AnioVacaciones], [hvac_Estado], [hvac_RazonInactivo], [hvac_UsuarioCrea], [hvac_FechaCrea], [hvac_UsuarioModifica], [hvac_FechaModifica]) VALUES (1, 1, CAST(N'2019-12-07' AS Date), CAST(N'2019-12-07' AS Date), 0, 6, 2019, 1, NULL, 1, CAST(N'2019-12-07T21:07:21.330' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialVacaciones] ([hvac_Id], [emp_Id], [hvac_FechaInicio], [hvac_FechaFin], [hvac_DiasTomados], [hvac_MesVacaciones], [hvac_AnioVacaciones], [hvac_Estado], [hvac_RazonInactivo], [hvac_UsuarioCrea], [hvac_FechaCrea], [hvac_UsuarioModifica], [hvac_FechaModifica]) VALUES (2, 12, CAST(N'2019-12-09' AS Date), CAST(N'2019-12-14' AS Date), 0, 6, 2019, 1, NULL, 1, CAST(N'2019-12-07T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialVacaciones] ([hvac_Id], [emp_Id], [hvac_FechaInicio], [hvac_FechaFin], [hvac_DiasTomados], [hvac_MesVacaciones], [hvac_AnioVacaciones], [hvac_Estado], [hvac_RazonInactivo], [hvac_UsuarioCrea], [hvac_FechaCrea], [hvac_UsuarioModifica], [hvac_FechaModifica]) VALUES (3, 3, CAST(N'2018-12-03' AS Date), CAST(N'2019-12-04' AS Date), 0, 10, 2019, 1, NULL, 1, CAST(N'2019-12-09T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialVacaciones] ([hvac_Id], [emp_Id], [hvac_FechaInicio], [hvac_FechaFin], [hvac_DiasTomados], [hvac_MesVacaciones], [hvac_AnioVacaciones], [hvac_Estado], [hvac_RazonInactivo], [hvac_UsuarioCrea], [hvac_FechaCrea], [hvac_UsuarioModifica], [hvac_FechaModifica]) VALUES (4, 4, CAST(N'2017-12-10' AS Date), CAST(N'2019-12-04' AS Date), 0, 6, 20190, 1, NULL, 1, CAST(N'2019-12-03T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHistorialVacaciones] ([hvac_Id], [emp_Id], [hvac_FechaInicio], [hvac_FechaFin], [hvac_DiasTomados], [hvac_MesVacaciones], [hvac_AnioVacaciones], [hvac_Estado], [hvac_RazonInactivo], [hvac_UsuarioCrea], [hvac_FechaCrea], [hvac_UsuarioModifica], [hvac_FechaModifica]) VALUES (5, 10, CAST(N'2019-10-04' AS Date), CAST(N'2019-10-20' AS Date), 0, 10, 2019, 1, NULL, 1, CAST(N'2019-12-03T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHorarios] ([hor_Id], [jor_Id], [hor_Descripcion], [hor_HoraInicio], [hor_HoraFin], [hor_CantidadHoras], [hor_Estado], [hor_RazonInactivo], [hor_UsuarioCrea], [hor_FechaCrea], [hor_UsuarioModifica], [hor_FechaModifica]) VALUES (1, 1, N'Horario Normal', CAST(N'07:00:00' AS Time), CAST(N'04:00:00' AS Time), CAST(N'08:00:00' AS Time), 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHorarios] ([hor_Id], [jor_Id], [hor_Descripcion], [hor_HoraInicio], [hor_HoraFin], [hor_CantidadHoras], [hor_Estado], [hor_RazonInactivo], [hor_UsuarioCrea], [hor_FechaCrea], [hor_UsuarioModifica], [hor_FechaModifica]) VALUES (2, 1, N'Horario Vespertino', CAST(N'16:00:00' AS Time), CAST(N'00:00:00' AS Time), CAST(N'09:00:00' AS Time), 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbHorarios] ([hor_Id], [jor_Id], [hor_Descripcion], [hor_HoraInicio], [hor_HoraFin], [hor_CantidadHoras], [hor_Estado], [hor_RazonInactivo], [hor_UsuarioCrea], [hor_FechaCrea], [hor_UsuarioModifica], [hor_FechaModifica]) VALUES (3, 1, N'Horario Nocturno', CAST(N'22:00:00' AS Time), CAST(N'04:00:00' AS Time), CAST(N'07:00:00' AS Time), 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbIdiomas] ([idi_Id], [idi_Descripcion], [idi_Estado], [idi_RazonInactivo], [idi_UsuarioCrea], [idi_FechaCrea], [idi_UsuarioModifica], [idi_FechaModifica]) VALUES (1, N'Aleman', 1, NULL, 1, CAST(N'2019-12-06T08:50:41.633' AS DateTime), 1, CAST(N'2019-12-06T09:27:02.507' AS DateTime))
INSERT [rrhh].[tbIdiomas] ([idi_Id], [idi_Descripcion], [idi_Estado], [idi_RazonInactivo], [idi_UsuarioCrea], [idi_FechaCrea], [idi_UsuarioModifica], [idi_FechaModifica]) VALUES (2, N'Español', 1, NULL, 1, CAST(N'2019-12-06T08:53:11.197' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbJornadas] ([jor_Id], [jor_Descripcion], [jor_Estado], [jor_RazonInactivo], [jor_UsuarioCrea], [jor_FechaCrea], [jor_UsuarioModifica], [jor_FechaModifica]) VALUES (1, N'Matutina', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbJornadas] ([jor_Id], [jor_Descripcion], [jor_Estado], [jor_RazonInactivo], [jor_UsuarioCrea], [jor_FechaCrea], [jor_UsuarioModifica], [jor_FechaModifica]) VALUES (2, N'Vespertina', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbJornadas] ([jor_Id], [jor_Descripcion], [jor_Estado], [jor_RazonInactivo], [jor_UsuarioCrea], [jor_FechaCrea], [jor_UsuarioModifica], [jor_FechaModifica]) VALUES (3, N'Nocturna', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbNacionalidades] ([nac_Id], [nac_Descripcion], [nac_Estado], [nac_RazonInactivo], [nac_UsuarioCrea], [nac_FechaCrea], [nac_UsuarioModifica], [nac_FechaModifica]) VALUES (1, N'Hondureña', 1, NULL, 1, CAST(N'2019-12-04T19:59:46.907' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (1, N'0501200103569', N'Walter', N'Ortega', CAST(N'2001-01-22' AS Date), N'M', 18, 1, N'San Pedro Sula', N'996857412', N'willian1997.wd@gmail.com', N'S', N'o+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (2, N'1804200036521', N'Carlos', N'Flamenco', CAST(N'2000-11-22' AS Date), N'M', 19, 1, N'San Pedro Sula', N'96854789', N'cflamenco@gmail.com', N'S', N'A+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (3, N'0502200056984', N'Elvin', N'Murcia', CAST(N'2000-09-04' AS Date), N'M', 19, 1, N'Choloma', N'32659874', N'murcia14@gmail.com', N'S', N'o+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (4, N'0502-2000-02649', N'Darwin', N'Chavarilla', CAST(N'1998-09-03' AS Date), N'M', 21, 1, N'Col. 5 de Mayo', N'8832-1323', N'willian1997.wd@gmail.com', N'S', N'A-', 1, NULL, 1, CAST(N'2019-12-04T20:12:45.627' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (5, N'05012000569874', N'Selvin ', N'Medina', CAST(N'2000-06-16' AS Date), N'M', 19, 1, N'San Pedro Sula', N'96857412', N'Smedina19@hotmail.com', N'S', N'O+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (6, N'05012000968544', N'Malcon ', N'Medina', CAST(N'2000-04-16' AS Date), N'M', 19, 1, N'Choloma', N'96857412', N'Malcommendina@gmail.com', N'S', N'AB+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (7, N'08502200036521', N'William', N'Diaz', CAST(N'2000-09-15' AS Date), N'M', 19, 1, N'Choloma', N'96857412', N'willian1997.wd@gmail.com', N'S', N'A-', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (8, N'05011999658412', N'Roberto', N'Toro', CAST(N'2000-11-17' AS Date), N'M', 25, 1, N'San Pedro Sula', N'96854123', N'ramt@gmail.com', N'S', N'O+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (9, N'50031999658944', N'Mauricio', N'Diaz', CAST(N'2000-04-15' AS Date), N'M', 26, 1, N'San Pedro Sula', N'96854712', N'mauricio@gmail.com', N'S', N'O+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (10, N'05012001031654', N'Jennifer', N'Vasquez', CAST(N'2001-01-03' AS Date), N'F', 18, 1, N'Choloma', N'96857412', N'aracely_vasq@hotmail.com', N'S', N'O+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (11, N'05012000652374', N'Fabiola ', N'Hernandez', CAST(N'2000-06-16' AS Date), N'F', 19, 1, N'San Pedro Sula', N'96851236', N'willian1997.wd@gmail.com', N'S', N'O+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (12, N'05012003659447', N'Sindy', N'Lainez', CAST(N'2000-10-19' AS Date), N'F', 19, 1, N'San Pedro Sula', N'96857412', N'willian1997.wd@gmail.com', N'S', N'A+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbPersonas] ([per_Id], [per_Identidad], [per_Nombres], [per_Apellidos], [per_FechaNacimiento], [per_Sexo], [per_Edad], [nac_Id], [per_Direccion], [per_Telefono], [per_CorreoElectronico], [per_EstadoCivil], [per_TipoSangre], [per_Estado], [per_RazonInactivo], [per_UsuarioCrea], [per_FechaCrea], [per_UsuarioModifica], [per_FechaModifica]) VALUES (13, N'05036985471256', N'Mileydi', N'Solis', CAST(N'2000-12-24' AS Date), N'F', 18, 1, N'San Pedro Sula', N'96235417', N'mileydi@gmail.com', N'S', N'B+', 1, NULL, 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbRazonSalidas] ([rsal_Id], [rsal_Descripcion], [rsal_Estado], [rsal_RazonInactivo], [rsal_UsuarioCrea], [rsal_FechaCrea], [rsal_UsuarioModifica], [rsal_FechaModifica]) VALUES (1, N'Estado de las instalaciones', 1, NULL, 1, CAST(N'2019-12-06T00:25:14.767' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbRequisiciones] ([req_Id], [req_Experiencia], [req_Sexo], [req_Descripcion], [req_EdadMinima], [req_EdadMaxima], [req_EstadoCivil], [req_EducacionSuperior], [req_Permanente], [req_Duracion], [req_Estado], [req_RazonInactivo], [req_Vacantes], [req_FechaRequisicion], [req_FechaContratacion], [req_UsuarioCrea], [req_FechaCrea], [req_UsuarioModifica], [req_FechaModifica]) VALUES (1, N'3 Años', N'F         ', N'Descripción', 21, 55, N'C ', 1, 1, N'4 Años', 1, NULL, N'hds', CAST(N'2019-03-23T00:00:00.000' AS DateTime), CAST(N'2019-04-20T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbRequisiciones] ([req_Id], [req_Experiencia], [req_Sexo], [req_Descripcion], [req_EdadMinima], [req_EdadMaxima], [req_EstadoCivil], [req_EducacionSuperior], [req_Permanente], [req_Duracion], [req_Estado], [req_RazonInactivo], [req_Vacantes], [req_FechaRequisicion], [req_FechaContratacion], [req_UsuarioCrea], [req_FechaCrea], [req_UsuarioModifica], [req_FechaModifica]) VALUES (2, N'5 Años', N'M         ', N'Descripción', 20, 55, N'C ', 1, 1, N'4 Años', 1, NULL, N'hds', CAST(N'2019-03-23T00:00:00.000' AS DateTime), CAST(N'2019-04-20T00:00:00.000' AS DateTime), 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSeleccionCandidatos] ([scan_Id], [per_Id], [fare_Id], [scan_Fecha], [rper_Id], [scan_Estado], [scan_RazonInactivo], [scan_UsuarioCrea], [scan_FechaCrea], [scan_UsuarioModifica], [scan_FechaModifica]) VALUES (1, 1, 1, CAST(N'2017-05-11T00:00:00.000' AS DateTime), 1, 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSucursales] ([suc_Id], [empr_Id], [mun_Codigo], [bod_Id], [pemi_Id], [suc_Descripcion], [suc_Correo], [suc_Direccion], [suc_Telefono], [suc_Estado], [suc_RazonInactivo], [suc_UsuarioCrea], [suc_FechaCrea], [suc_UsuarioModifica], [suc_FechaModifica]) VALUES (1, 1, N'0501', 1, 1, N'AHM Barrio las Acacias', N'AHM@gmail.com', N'Col. San Juan', N'205605054', 1, N'1', 1, CAST(N'2019-12-04T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSucursales] ([suc_Id], [empr_Id], [mun_Codigo], [bod_Id], [pemi_Id], [suc_Descripcion], [suc_Correo], [suc_Direccion], [suc_Telefono], [suc_Estado], [suc_RazonInactivo], [suc_UsuarioCrea], [suc_FechaCrea], [suc_UsuarioModifica], [suc_FechaModifica]) VALUES (2, 1, N'501 ', 1, 1, N'AHM Altia', N'ahm2@gmail.com', N'Altia SPS', N'25500426', 1, N'1', 1, CAST(N'2019-12-09T14:21:34.667' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (1, 1, 1, CAST(9500.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (2, 2, 1, CAST(9500.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (3, 3, 1, CAST(9600.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (4, 4, 1, CAST(9800.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (5, 5, 1, CAST(9900.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (6, 6, 1, CAST(9500.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (7, 7, 1, CAST(9999.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (8, 8, 1, CAST(9000.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (9, 9, 1, CAST(9999.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (10, 10, 1, CAST(9614.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (11, 11, 1, CAST(9600.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (12, 12, 1, CAST(9800.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbSueldos] ([sue_Id], [emp_Id], [tmon_Id], [sue_Cantidad], [sue_SueldoAnterior], [sue_Estado], [sue_RazonInactivo], [sue_UsuarioCrea], [sue_FechaCrea], [sue_UsuarioModifica], [sue_FechaModifica]) VALUES (13, 13, 1, CAST(9500.0000 AS Decimal(8, 4)), NULL, 1, NULL, 1, CAST(N'2019-11-11T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoAmonestaciones] ([tamo_Id], [tamo_Descripcion], [tamo_Estado], [tamo_RazonInactivo], [tamo_UsuarioCrea], [tamo_FechaCrea], [tamo_UsuarioModifica], [tamo_FechaModifica]) VALUES (1, N'Escrita', 1, NULL, 1, CAST(N'2019-12-05T14:15:23.607' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoAmonestaciones] ([tamo_Id], [tamo_Descripcion], [tamo_Estado], [tamo_RazonInactivo], [tamo_UsuarioCrea], [tamo_FechaCrea], [tamo_UsuarioModifica], [tamo_FechaModifica]) VALUES (2, N'Verbal', 1, NULL, 1, CAST(N'2019-12-06T10:07:21.390' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoHoras] ([tiho_Id], [tiho_Descripcion], [tiho_Recargo], [tiho_Estado], [tiho_RazonInactivo], [tiho_UsuarioCrea], [tiho_FechaCrea], [tiho_UsuarioModifica], [tiho_FechaModifica]) VALUES (1, N'Hora Extra', 40, 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoHoras] ([tiho_Id], [tiho_Descripcion], [tiho_Recargo], [tiho_Estado], [tiho_RazonInactivo], [tiho_UsuarioCrea], [tiho_FechaCrea], [tiho_UsuarioModifica], [tiho_FechaModifica]) VALUES (2, N'Tipo B', 50, 1, NULL, 1, CAST(N'2019-12-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoHoras] ([tiho_Id], [tiho_Descripcion], [tiho_Recargo], [tiho_Estado], [tiho_RazonInactivo], [tiho_UsuarioCrea], [tiho_FechaCrea], [tiho_UsuarioModifica], [tiho_FechaModifica]) VALUES (3, N'Hora Normal ', 0, 1, NULL, 1, CAST(N'2019-12-06T08:56:01.900' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoHoras] ([tiho_Id], [tiho_Descripcion], [tiho_Recargo], [tiho_Estado], [tiho_RazonInactivo], [tiho_UsuarioCrea], [tiho_FechaCrea], [tiho_UsuarioModifica], [tiho_FechaModifica]) VALUES (4, N'Tipo C', 100, 1, NULL, 1, CAST(N'2019-12-06T10:05:03.503' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoIncapacidades] ([ticn_Id], [ticn_Descripcion], [ticn_Estado], [ticn_RazonInactivo], [ticn_UsuarioCrea], [ticn_FechaCrea], [ticn_UsuarioModifica], [ticn_FechaModifica]) VALUES (1, N'Por Maternidad', 1, NULL, 1, CAST(N'2019-12-06T09:27:58.897' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoIncapacidades] ([ticn_Id], [ticn_Descripcion], [ticn_Estado], [ticn_RazonInactivo], [ticn_UsuarioCrea], [ticn_FechaCrea], [ticn_UsuarioModifica], [ticn_FechaModifica]) VALUES (2, N'lesion', 1, NULL, 1, CAST(N'2019-12-08T22:46:45.060' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoMonedas] ([tmon_Id], [tmon_Descripcion], [tmon_Estado], [tmon_RazonInactivo], [tmon_UsuarioCrea], [tmon_FechaCrea], [tmon_UsuarioModifica], [tmon_FechaModifica]) VALUES (1, N'Lempiras', 1, NULL, 1, CAST(N'2019-12-04T21:54:10.340' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoMonedas] ([tmon_Id], [tmon_Descripcion], [tmon_Estado], [tmon_RazonInactivo], [tmon_UsuarioCrea], [tmon_FechaCrea], [tmon_UsuarioModifica], [tmon_FechaModifica]) VALUES (2, N'Quetzal', 1, NULL, 1, CAST(N'2019-12-05T15:03:39.273' AS DateTime), 1, CAST(N'2019-12-06T09:26:37.883' AS DateTime))
INSERT [rrhh].[tbTipoMonedas] ([tmon_Id], [tmon_Descripcion], [tmon_Estado], [tmon_RazonInactivo], [tmon_UsuarioCrea], [tmon_FechaCrea], [tmon_UsuarioModifica], [tmon_FechaModifica]) VALUES (3, N'Dolar', 1, NULL, 1, CAST(N'2019-12-05T15:44:49.447' AS DateTime), 1, CAST(N'2019-12-06T09:26:31.293' AS DateTime))
INSERT [rrhh].[tbTipoMonedas] ([tmon_Id], [tmon_Descripcion], [tmon_Estado], [tmon_RazonInactivo], [tmon_UsuarioCrea], [tmon_FechaCrea], [tmon_UsuarioModifica], [tmon_FechaModifica]) VALUES (4, N'Libra', 1, NULL, 1, CAST(N'2019-12-05T22:57:38.143' AS DateTime), 1, CAST(N'2019-12-06T09:26:25.773' AS DateTime))
INSERT [rrhh].[tbTipoMonedas] ([tmon_Id], [tmon_Descripcion], [tmon_Estado], [tmon_RazonInactivo], [tmon_UsuarioCrea], [tmon_FechaCrea], [tmon_UsuarioModifica], [tmon_FechaModifica]) VALUES (5, N'Bolivar', 1, NULL, 1, CAST(N'2019-12-06T08:55:27.903' AS DateTime), 1, CAST(N'2019-12-06T09:26:20.173' AS DateTime))
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (1, N'Por luto', 1, NULL, 1, CAST(N'2019-12-06T14:31:48.123' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (2, N'Por Enfermedad', 1, NULL, 1, CAST(N'2019-12-06T14:31:58.890' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (3, N'Por Muerte de un familiar', 1, NULL, 1, CAST(N'2019-12-06T14:32:09.080' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (4, N'Por Muerte de una mascota', 1, NULL, 1, CAST(N'2019-12-06T14:32:15.570' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (5, N'Por motivos externos', 1, NULL, 1, CAST(N'2019-12-06T14:32:27.623' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (6, N'Por boda', 1, NULL, 1, CAST(N'2019-12-06T14:32:53.140' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (7, N'Por nacimiento de un hijo', 1, NULL, 1, CAST(N'2019-12-06T14:33:02.437' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoPermisos] ([tper_Id], [tper_Descripcion], [tper_Estado], [tper_RazonInactivo], [tper_UsuarioCrea], [tper_FechaCrea], [tper_UsuarioModifica], [tper_FechaModifica]) VALUES (8, N'Por alguna emergencia', 1, NULL, 1, CAST(N'2019-12-06T14:34:14.980' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (1, N'Por enfermedad', 1, NULL, 1, CAST(N'2019-12-05T15:53:49.743' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (2, N'Por Luto', 1, NULL, 1, CAST(N'2019-12-05T15:54:02.960' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (3, N'Por Muerte del nucleo', 1, NULL, 1, CAST(N'2019-12-05T15:54:11.807' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (4, N'Por accidentes', 1, NULL, 1, CAST(N'2019-12-05T15:54:17.710' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (5, N'Por lesiones fisicas', 1, NULL, 1, CAST(N'2019-12-05T15:54:23.620' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (6, N'Por Cuestiones legales', 1, NULL, 1, CAST(N'2019-12-05T15:55:31.923' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (7, N'Por Juicio', 1, NULL, 1, CAST(N'2019-12-05T15:56:15.290' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTipoSalidas] ([tsal_Id], [tsal_Descripcion], [tsal_Estado], [tsal_RazonInactivo], [tsal_UsuarioCrea], [tsal_FechaCrea], [tsal_UsuarioModifica], [tsal_FechaModifica]) VALUES (8, N'Por Viaje', 1, NULL, 1, CAST(N'2019-12-05T15:58:31.570' AS DateTime), NULL, NULL)
INSERT [rrhh].[tbTitulos] ([titu_Id], [titu_Descripcion], [titu_Estado], [titu_RazonInactivo], [titu_UsuarioCrea], [titu_FechaCrea], [titu_UsuarioModifica], [titu_FechaModifica]) VALUES (1, N'Bachillerato en Ciencias y Letras', 1, NULL, 1, CAST(N'2019-12-06T09:36:04.880' AS DateTime), NULL, NULL)
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Plani_tbDecimoTercerMes_dcm_CodigoPago]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [Plani].[tbDecimoCuartoMes] ADD  CONSTRAINT [UQ_Plani_tbDecimoTercerMes_dcm_CodigoPago] UNIQUE NONCLUSTERED 
(
	[dcm_CodigoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Plani_tbDecimoTercerMes_dtm_CodigoPago]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [Plani].[tbDecimoTercerMes] ADD  CONSTRAINT [UQ_Plani_tbDecimoTercerMes_dtm_CodigoPago] UNIQUE NONCLUSTERED 
(
	[dtm_CodigoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbAreas_suc_Id_area_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbAreas] ADD  CONSTRAINT [UQ_rrhh_tbAreas_suc_Id_area_Descripcion] UNIQUE NONCLUSTERED 
(
	[suc_Id] ASC,
	[area_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbCargos_car_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbCargos] ADD  CONSTRAINT [UQ_rrhh_tbCargos_car_Descripcion] UNIQUE NONCLUSTERED 
(
	[car_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_RRHH_tbCompetencias_comp_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbCompetencias] ADD  CONSTRAINT [UQ_RRHH_tbCompetencias_comp_Descripcion] UNIQUE NONCLUSTERED 
(
	[comp_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_RRHH_tbcompetenciasPersona_per_Id_comp_Id]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbCompetenciasPersona] ADD  CONSTRAINT [UQ_RRHH_tbcompetenciasPersona_per_Id_comp_Id] UNIQUE NONCLUSTERED 
(
	[per_Id] ASC,
	[comp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_RRHH_tbCompetenciasRequisicion_comp_Id_req_Id]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] ADD  CONSTRAINT [UQ_RRHH_tbCompetenciasRequisicion_comp_Id_req_Id] UNIQUE NONCLUSTERED 
(
	[req_Id] ASC,
	[comp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_RRHH_tbDepartamentos_area_Id_depto_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbDepartamentos] ADD  CONSTRAINT [UQ_RRHH_tbDepartamentos_area_Id_depto_Descripcion] UNIQUE NONCLUSTERED 
(
	[area_Id] ASC,
	[depto_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_rrhh_tbEmpleados_per_Id]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbEmpleados] ADD  CONSTRAINT [UQ_rrhh_tbEmpleados_per_Id] UNIQUE NONCLUSTERED 
(
	[per_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbEquipoTrabajo_eqtra_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbEquipoTrabajo] ADD  CONSTRAINT [UQ_rrhh_tbEquipoTrabajo_eqtra_Descripcion] UNIQUE NONCLUSTERED 
(
	[eqtra_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbFasesReclutamiento_fare_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbFasesReclutamiento] ADD  CONSTRAINT [UQ_rrhh_tbFasesReclutamiento_fare_Descripcion] UNIQUE NONCLUSTERED 
(
	[fare_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbHabilidades_habi_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbHabilidades] ADD  CONSTRAINT [UQ_rrhh_tbHabilidades_habi_Descripcion] UNIQUE NONCLUSTERED 
(
	[habi_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbHorarios_hor_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbHorarios] ADD  CONSTRAINT [UQ_rrhh_tbHorarios_hor_Descripcion] UNIQUE NONCLUSTERED 
(
	[hor_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbIdiomas_idi_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbIdiomas] ADD  CONSTRAINT [UQ_rrhh_tbIdiomas_idi_Descripcion] UNIQUE NONCLUSTERED 
(
	[idi_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbJornadas_jor_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbJornadas] ADD  CONSTRAINT [UQ_rrhh_tbJornadas_jor_Descripcion] UNIQUE NONCLUSTERED 
(
	[jor_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbNacionalidades_nac_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbNacionalidades] ADD  CONSTRAINT [UQ_rrhh_tbNacionalidades_nac_Descripcion] UNIQUE NONCLUSTERED 
(
	[nac_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Personas_per_Identidad]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbPersonas] ADD  CONSTRAINT [UQ_Personas_per_Identidad] UNIQUE NONCLUSTERED 
(
	[per_Identidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbRequerimientosEspeciales_resp_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbRequerimientosEspeciales] ADD  CONSTRAINT [UQ_rrhh_tbRequerimientosEspeciales_resp_Descripcion] UNIQUE NONCLUSTERED 
(
	[resp_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbSucursales_empr_Id_suc_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbSucursales] ADD  CONSTRAINT [UQ_rrhh_tbSucursales_empr_Id_suc_Descripcion] UNIQUE NONCLUSTERED 
(
	[empr_Id] ASC,
	[suc_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoAmonestaciones_tamo_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoAmonestaciones] ADD  CONSTRAINT [UQ_rrhh_tbTipoAmonestaciones_tamo_Descripcion] UNIQUE NONCLUSTERED 
(
	[tamo_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoHoras_tiho_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoHoras] ADD  CONSTRAINT [UQ_rrhh_tbTipoHoras_tiho_Descripcion] UNIQUE NONCLUSTERED 
(
	[tiho_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoIncapacidades_ticn_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoIncapacidades] ADD  CONSTRAINT [UQ_rrhh_tbTipoIncapacidades_ticn_Descripcion] UNIQUE NONCLUSTERED 
(
	[ticn_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoMonedas_tmon_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoMonedas] ADD  CONSTRAINT [UQ_rrhh_tbTipoMonedas_tmon_Descripcion] UNIQUE NONCLUSTERED 
(
	[tmon_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoPermisos_tper_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoPermisos] ADD  CONSTRAINT [UQ_rrhh_tbTipoPermisos_tper_Descripcion] UNIQUE NONCLUSTERED 
(
	[tper_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTipoSalidas_tsal_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTipoSalidas] ADD  CONSTRAINT [UQ_rrhh_tbTipoSalidas_tsal_Descripcion] UNIQUE NONCLUSTERED 
(
	[tsal_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_rrhh_tbTitulos_titu_Descripcion]    Script Date: 09/12/2019 16:31:34 ******/
ALTER TABLE [rrhh].[tbTitulos] ADD  CONSTRAINT [UQ_rrhh_tbTitulos_titu_Descripcion] UNIQUE NONCLUSTERED 
(
	[titu_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones] ADD  CONSTRAINT [DF_tbHistorialLiquidaciones_hliq_Estado]  DEFAULT ((1)) FOR [hliq_Estado]
GO
ALTER TABLE [rrhh].[tbAreas] ADD  CONSTRAINT [DF_tbAreas_area_Estado]  DEFAULT ((1)) FOR [area_Estado]
GO
ALTER TABLE [rrhh].[tbCargos] ADD  CONSTRAINT [DF_tbCargos_car_Estado]  DEFAULT ((1)) FOR [car_Estado]
GO
ALTER TABLE [rrhh].[tbCompetencias] ADD  CONSTRAINT [DF_tbCompetencias_comp_Estado]  DEFAULT ((1)) FOR [comp_Estado]
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona] ADD  CONSTRAINT [DF_tbCompetenciasPersona_cope_Estado]  DEFAULT ((1)) FOR [cope_Estado]
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] ADD  CONSTRAINT [DF_tbCompetenciasRequisicion_creq_Estado]  DEFAULT ((1)) FOR [creq_Estado]
GO
ALTER TABLE [rrhh].[tbDepartamentos] ADD  CONSTRAINT [DF_tbDepartamentos_depto_Estado]  DEFAULT ((1)) FOR [depto_Estado]
GO
ALTER TABLE [rrhh].[tbEmpleados] ADD  CONSTRAINT [DF_tbEmpleados_emp_Estado]  DEFAULT ((1)) FOR [emp_Estado]
GO
ALTER TABLE [rrhh].[tbEmpresas] ADD  CONSTRAINT [DF_tbEmpresas_empr_Estado]  DEFAULT ((1)) FOR [empr_Estado]
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados] ADD  CONSTRAINT [DF_tbEquipoEmpleados_eqem_Estado]  DEFAULT ((1)) FOR [eqem_Estado]
GO
ALTER TABLE [rrhh].[tbEquipoTrabajo] ADD  CONSTRAINT [DF_tbEquipoTrabajo_eqtra_Estado]  DEFAULT ((1)) FOR [eqtra_Estado]
GO
ALTER TABLE [rrhh].[tbFaseSeleccion] ADD  CONSTRAINT [DF_tbFaseSeleccion_fsel_Estado]  DEFAULT ((1)) FOR [fsel_Estado]
GO
ALTER TABLE [rrhh].[tbFasesReclutamiento] ADD  CONSTRAINT [DF_tbFasesReclutamiento_fare_Estado]  DEFAULT ((1)) FOR [fare_Estado]
GO
ALTER TABLE [rrhh].[tbHabilidades] ADD  CONSTRAINT [DF_tbHabilidades_habi_Estado]  DEFAULT ((1)) FOR [habi_Estado]
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona] ADD  CONSTRAINT [DF_tbHabilidadesPersona_hape_Estado]  DEFAULT ((1)) FOR [hape_Estado]
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion] ADD  CONSTRAINT [DF_tbHabilidadesRequisicion_hreq_Estado]  DEFAULT ((1)) FOR [hreq_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] ADD  CONSTRAINT [DF_tbHistorialAmonestaciones_hamo_Estado]  DEFAULT ((1)) FOR [hamo_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo] ADD  CONSTRAINT [DF_tbHistorialAudienciaDescargo_aude_Estado]  DEFAULT ((1)) FOR [aude_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialCargos] ADD  CONSTRAINT [DF_tbHistorialCargos_hcar_Estado]  DEFAULT ((1)) FOR [hcar_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones] ADD  CONSTRAINT [DF_tbHistorialContrataciones_hcon_Estado]  DEFAULT ((1)) FOR [hcon_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] ADD  CONSTRAINT [DF_tbHistorialHorasTrabajadas_htra_Estado]  DEFAULT ((1)) FOR [htra_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades] ADD  CONSTRAINT [DF_tbHistorialIncapacidades_hinc_Estado]  DEFAULT ((1)) FOR [hinc_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialPermisos] ADD  CONSTRAINT [DF_tbHistorialPermisos_hper_Estado]  DEFAULT ((1)) FOR [hper_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos] ADD  CONSTRAINT [DF_tbHistorialRefrendamientos_href_Estado]  DEFAULT ((1)) FOR [href_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] ADD  CONSTRAINT [DF_tbHistorialSalidas_hsal_Estado]  DEFAULT ((1)) FOR [hsal_Estado]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] ADD  DEFAULT ((1)) FOR [hvac_DiasTomados]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] ADD  CONSTRAINT [DF_tbHistorialVacaciones_hvac_Estado]  DEFAULT ((1)) FOR [hvac_Estado]
GO
ALTER TABLE [rrhh].[tbHorarios] ADD  CONSTRAINT [DF_tbHorarios_hor_Estado]  DEFAULT ((1)) FOR [hor_Estado]
GO
ALTER TABLE [rrhh].[tbIdiomaPersona] ADD  CONSTRAINT [DF_tbIdiomaPersona_idpe_Estado]  DEFAULT ((1)) FOR [idpe_Estado]
GO
ALTER TABLE [rrhh].[tbIdiomas] ADD  CONSTRAINT [DF_tbIdiomas_idi_Estado]  DEFAULT ((1)) FOR [idi_Estado]
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion] ADD  CONSTRAINT [DF_tbIdiomasRequisicion_ireq_Estado]  DEFAULT ((1)) FOR [ireq_Estado]
GO
ALTER TABLE [rrhh].[tbJornadas] ADD  CONSTRAINT [DF_tbJornadas_jor_Estado]  DEFAULT ((1)) FOR [jor_Estado]
GO
ALTER TABLE [rrhh].[tbNacionalidades] ADD  CONSTRAINT [DF_tbNacionalidades_nac_Estado]  DEFAULT ((1)) FOR [nac_Estado]
GO
ALTER TABLE [rrhh].[tbPersonas] ADD  CONSTRAINT [DF_tbPersonas_per_Estado]  DEFAULT ((1)) FOR [per_Estado]
GO
ALTER TABLE [rrhh].[tbPrestaciones] ADD  CONSTRAINT [DF_tbPrestaciones_pres_Estado]  DEFAULT ((1)) FOR [pres_Estado]
GO
ALTER TABLE [rrhh].[tbRazonSalidas] ADD  CONSTRAINT [DF_tbRazonSalidas_rsal_Estado]  DEFAULT ((1)) FOR [rsal_Estado]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspeciales] ADD  CONSTRAINT [DF_tbRequerimientosEspeciales_resp_Estado]  DEFAULT ((1)) FOR [resp_Estado]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona] ADD  CONSTRAINT [DF_tbRequerimientosEspecialesPersona_rep_Estado]  DEFAULT ((1)) FOR [rep_Estado]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion] ADD  CONSTRAINT [DF_tbHabilidadesRequisicion_rer_Estado]  DEFAULT ((1)) FOR [rer_Estado]
GO
ALTER TABLE [rrhh].[tbRequisiciones] ADD  CONSTRAINT [DF_tbRequisiciones_req_Permanente]  DEFAULT ((1)) FOR [req_Permanente]
GO
ALTER TABLE [rrhh].[tbRequisiciones] ADD  CONSTRAINT [DF_tbRequisiciones_req_Estado]  DEFAULT ((1)) FOR [req_Estado]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] ADD  CONSTRAINT [DF_tbSeleccionCandidatos_scan_Estado]  DEFAULT ((1)) FOR [scan_Estado]
GO
ALTER TABLE [rrhh].[tbSucursales] ADD  CONSTRAINT [DF_tbSucursales_suc_RazonInactivo]  DEFAULT ((1)) FOR [suc_RazonInactivo]
GO
ALTER TABLE [rrhh].[tbSueldos] ADD  CONSTRAINT [DF_tbSueldos_hsue_Estado]  DEFAULT ((1)) FOR [sue_Estado]
GO
ALTER TABLE [rrhh].[tbTipoAmonestaciones] ADD  CONSTRAINT [DF_tbTipoAmonestaciones_tamo_Estado]  DEFAULT ((1)) FOR [tamo_Estado]
GO
ALTER TABLE [rrhh].[tbTipoHoras] ADD  CONSTRAINT [DF_tbTipoHoras_tiho_Estado]  DEFAULT ((1)) FOR [tiho_Estado]
GO
ALTER TABLE [rrhh].[tbTipoIncapacidades] ADD  CONSTRAINT [DF_tbTipoIncapacidades_ticn_Estado]  DEFAULT ((1)) FOR [ticn_Estado]
GO
ALTER TABLE [rrhh].[tbTipoMonedas] ADD  CONSTRAINT [DF_tbTipoMonedas_tmon_estado]  DEFAULT ((1)) FOR [tmon_Estado]
GO
ALTER TABLE [rrhh].[tbTipoPermisos] ADD  CONSTRAINT [DF_tbTipoPermisos_tper_Estado]  DEFAULT ((1)) FOR [tper_Estado]
GO
ALTER TABLE [rrhh].[tbTipoSalidas] ADD  CONSTRAINT [DF_tbTipoSalidas_tsal_Estado]  DEFAULT ((1)) FOR [tsal_Estado]
GO
ALTER TABLE [rrhh].[tbTitulos] ADD  CONSTRAINT [DF_tbTitulos_titu_Estado]  DEFAULT ((1)) FOR [titu_Estado]
GO
ALTER TABLE [rrhh].[tbTitulosPersona] ADD  CONSTRAINT [DF_tbTitulosPersona_tipe_Estado]  DEFAULT ((1)) FOR [tipe_Estado]
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion] ADD  CONSTRAINT [DF_tbTitulosRequisicion_treq_Estado]  DEFAULT ((1)) FOR [treq_Estado]
GO
ALTER TABLE [Acce].[tbAccesoRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbObjeto_obj_Id_Acce_tbAccesoRol_obj_Id] FOREIGN KEY([obj_Id])
REFERENCES [Acce].[tbObjeto] ([obj_Id])
GO
ALTER TABLE [Acce].[tbAccesoRol] CHECK CONSTRAINT [FK_Acce_tbObjeto_obj_Id_Acce_tbAccesoRol_obj_Id]
GO
ALTER TABLE [Acce].[tbAccesoRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbRol_rol_Id_Acce_tbAccesoRol_rol_Id] FOREIGN KEY([rol_Id])
REFERENCES [Acce].[tbRol] ([rol_Id])
GO
ALTER TABLE [Acce].[tbAccesoRol] CHECK CONSTRAINT [FK_Acce_tbRol_rol_Id_Acce_tbAccesoRol_rol_Id]
GO
ALTER TABLE [Acce].[tbAccesoRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbAccesoRol_acrol_UsuarioCrea] FOREIGN KEY([acrol_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbAccesoRol] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbAccesoRol_acrol_UsuarioCrea]
GO
ALTER TABLE [Acce].[tbAccesoRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbAccesoRol_acrol_UsuarioModifica] FOREIGN KEY([acrol_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbAccesoRol] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbAccesoRol_acrol_UsuarioModifica]
GO
ALTER TABLE [Acce].[tbObjeto]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbObjeto_obj_UsuarioCrea] FOREIGN KEY([obj_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbObjeto] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbObjeto_obj_UsuarioCrea]
GO
ALTER TABLE [Acce].[tbObjeto]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbObjeto_obj_UsuarioModifica] FOREIGN KEY([obj_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbObjeto] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbObjeto_obj_UsuarioModifica]
GO
ALTER TABLE [Acce].[tbRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRol_rol_UsuarioCrea] FOREIGN KEY([rol_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbRol] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRol_rol_UsuarioCrea]
GO
ALTER TABLE [Acce].[tbRol]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRol_rol_UsuarioModifica] FOREIGN KEY([rol_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbRol] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRol_rol_UsuarioModifica]
GO
ALTER TABLE [Acce].[tbRolesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbRol_rol_Id_Acce_tbRolesUsuario_rol_Id] FOREIGN KEY([rol_Id])
REFERENCES [Acce].[tbRol] ([rol_Id])
GO
ALTER TABLE [Acce].[tbRolesUsuario] CHECK CONSTRAINT [FK_Acce_tbRol_rol_Id_Acce_tbRolesUsuario_rol_Id]
GO
ALTER TABLE [Acce].[tbRolesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_rolu_UsuarioCrea] FOREIGN KEY([rolu_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbRolesUsuario] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_rolu_UsuarioCrea]
GO
ALTER TABLE [Acce].[tbRolesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_rolu_UsuarioModifica] FOREIGN KEY([rolu_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbRolesUsuario] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_rolu_UsuarioModifica]
GO
ALTER TABLE [Acce].[tbRolesUsuario]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_usu_Id] FOREIGN KEY([usu_Id])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Acce].[tbRolesUsuario] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Acce_tbRolesUsuario_usu_Id]
GO
ALTER TABLE [Plani].[tbAcumuladosISR]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAcumuladosISR_aisr_UsuarioCrea] FOREIGN KEY([aisr_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAcumuladosISR] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAcumuladosISR_aisr_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbAcumuladosISR]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAcumuladosISR_aisr_UsuarioModifica] FOREIGN KEY([aisr_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAcumuladosISR] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAcumuladosISR_aisr_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbAdelantoSueldo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAdelantoSueldo_adsu_UsuarioCrea] FOREIGN KEY([adsu_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAdelantoSueldo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAdelantoSueldo_adsu_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbAdelantoSueldo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAdelantoSueldo_adsu_UsuarioModifica] FOREIGN KEY([adsu_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAdelantoSueldo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAdelantoSueldo_adsu_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbAdelantoSueldo]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbAdelantoSueldo_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbAdelantoSueldo] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbAdelantoSueldo_emp_Id]
GO
ALTER TABLE [Plani].[tbAFP]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAFP_afp_UsuarioCrea] FOREIGN KEY([afp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAFP] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAFP_afp_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbAFP]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAFP_afp_UsuarioModifica] FOREIGN KEY([afp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAFP] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAFP_afp_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbAFP]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbAFP_tde_IdTipoDedu] FOREIGN KEY([tde_IdTipoDedu])
REFERENCES [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu])
GO
ALTER TABLE [Plani].[tbAFP] CHECK CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbAFP_tde_IdTipoDedu]
GO
ALTER TABLE [Plani].[tbAuxilioDeCesantias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAuxilioDeCesantias_aces_UsuarioCrea] FOREIGN KEY([aces_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAuxilioDeCesantias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAuxilioDeCesantias_aces_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbAuxilioDeCesantias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAuxilioDeCesantias_aces_UsuarioModifica] FOREIGN KEY([aces_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbAuxilioDeCesantias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbAuxilioDeCesantias_aces_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeDeducciones_cde_UsuarioCrea] FOREIGN KEY([cde_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeDeducciones_cde_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeDeducciones_cde_UsuarioModifica] FOREIGN KEY([cde_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeDeducciones_cde_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbCatalogoDeDeducciones_tde_IdTipoDedu] FOREIGN KEY([tde_IdTipoDedu])
REFERENCES [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu])
GO
ALTER TABLE [Plani].[tbCatalogoDeDeducciones] CHECK CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbCatalogoDeDeducciones_tde_IdTipoDedu]
GO
ALTER TABLE [Plani].[tbCatalogoDeIngresos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeIngresos_cin_UsuarioCrea] FOREIGN KEY([cin_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDeIngresos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeIngresos_cin_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbCatalogoDeIngresos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeIngresos_cin_UsuarioModifica] FOREIGN KEY([cin_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDeIngresos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDeIngresos_cin_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbCatalogoDePlanillas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDePlanillas_cpla_UsuarioCrea] FOREIGN KEY([cpla_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDePlanillas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDePlanillas_cpla_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbCatalogoDePlanillas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDePlanillas_cpla_UsuarioModifica] FOREIGN KEY([cpla_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbCatalogoDePlanillas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbCatalogoDePlanillas_cpla_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoCuartoMes_dcm_UsuarioCrea] FOREIGN KEY([dcm_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoCuartoMes_dcm_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoCuartoMes_dcm_UsuarioModifica] FOREIGN KEY([dcm_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoCuartoMes_dcm_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDecimoCuartoMes_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbDecimoCuartoMes] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDecimoCuartoMes_emp_Id]
GO
ALTER TABLE [Plani].[tbDecimoTercerMes]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoTercerMes_dtm_UsuarioCrea] FOREIGN KEY([dtm_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDecimoTercerMes] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoTercerMes_dtm_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbDecimoTercerMes]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoTercerMes_dtm_UsuarioModifica] FOREIGN KEY([dtm_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDecimoTercerMes] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDecimoTercerMes_dtm_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDecimoTercerMes]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDecimoTercerMes_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbDecimoTercerMes] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDecimoTercerMes_emp_Id]
GO
ALTER TABLE [Plani].[tbDeduccionAFP]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionAFP_dafp_UsuarioCrea] FOREIGN KEY([dafp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionAFP] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionAFP_dafp_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbDeduccionAFP]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionAFP_dafp_UsuarioModifica] FOREIGN KEY([dafp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionAFP] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionAFP_dafp_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDeduccionAFP]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbAFP_afp_Id_Plani_tbDeduccionAFP_afp_Id] FOREIGN KEY([afp_Id])
REFERENCES [Plani].[tbAFP] ([afp_Id])
GO
ALTER TABLE [Plani].[tbDeduccionAFP] CHECK CONSTRAINT [FK_Plani_tbAFP_afp_Id_Plani_tbDeduccionAFP_afp_Id]
GO
ALTER TABLE [Plani].[tbDeduccionAFP]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDeduccionAFP_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbDeduccionAFP] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDeduccionAFP_emp_Id]
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionesExtraordinarias_dex_UsuarioCrea] FOREIGN KEY([dex_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionesExtraordinarias_dex_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionesExtraordinarias_dex_UsuarioModifica] FOREIGN KEY([dex_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionesExtraordinarias_dex_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbDeduccionesExtraordinarias_cde_IdDeducciones] FOREIGN KEY([cde_IdDeducciones])
REFERENCES [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones])
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbDeduccionesExtraordinarias_cde_IdDeducciones]
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEquipoEmpleados_eqem_Id_Plani_tbDeduccionesExtraordinarias_eqem_Id] FOREIGN KEY([eqem_Id])
REFERENCES [rrhh].[tbEquipoEmpleados] ([eqem_Id])
GO
ALTER TABLE [Plani].[tbDeduccionesExtraordinarias] CHECK CONSTRAINT [FK_rrhh_tbEquipoEmpleados_eqem_Id_Plani_tbDeduccionesExtraordinarias_eqem_Id]
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionInstitucionFinanciera_deif_UsuarioCrea] FOREIGN KEY([deif_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionInstitucionFinanciera_deif_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionInstitucionFinanciera_deif_UsuarioModifica] FOREIGN KEY([deif_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbDeduccionInstitucionFinanciera_deif_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbDeduccionInstitucionFinanciera_cde_IdDeducciones] FOREIGN KEY([cde_IdDeducciones])
REFERENCES [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones])
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbDeduccionInstitucionFinanciera_cde_IdDeducciones]
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbInstitucionesFinancieras_insf_IdInstitucionFinanciera_Plani_tbDeduccionInstitucionFinanciera_insf_IdInstitucionFinanc] FOREIGN KEY([insf_IdInstitucionFinanciera])
REFERENCES [Plani].[tbInstitucionesFinancieras] ([insf_IdInstitucionFinanciera])
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera] CHECK CONSTRAINT [FK_Plani_tbInstitucionesFinancieras_insf_IdInstitucionFinanciera_Plani_tbDeduccionInstitucionFinanciera_insf_IdInstitucionFinanc]
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDeduccionInstitucionFinanciera_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbDeduccionInstitucionFinanciera] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbDeduccionInstitucionFinanciera_emp_Id]
GO
ALTER TABLE [Plani].[tbEmpleadoBonos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoBonos_cb_UsuarioCrea] FOREIGN KEY([cb_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoBonos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoBonos_cb_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbEmpleadoBonos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoBonos_cb_UsuarioModifica] FOREIGN KEY([cb_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoBonos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoBonos_cb_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbEmpleadoBonos]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbEmpleadoBonos_cin_IdIngreso] FOREIGN KEY([cin_IdIngreso])
REFERENCES [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso])
GO
ALTER TABLE [Plani].[tbEmpleadoBonos] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbEmpleadoBonos_cin_IdIngreso]
GO
ALTER TABLE [Plani].[tbEmpleadoBonos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbEmpleadoBonos_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoBonos] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbEmpleadoBonos_emp_Id]
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoComisiones_cc_UsuarioCrea] FOREIGN KEY([cc_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoComisiones_cc_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoComisiones_cc_UsuarioModifica] FOREIGN KEY([cc_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbEmpleadoComisiones_cc_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbEmpleadoComisiones_cin_IdIngreso] FOREIGN KEY([cin_IdIngreso])
REFERENCES [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso])
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbEmpleadoComisiones_cin_IdIngreso]
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbEmpleadoComisiones_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbEmpleadoComisiones] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbEmpleadoComisiones_emp_Id]
GO
ALTER TABLE [Plani].[tbFormaPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbFormaPago_fpa_UsuarioCrea] FOREIGN KEY([fpa_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbFormaPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbFormaPago_fpa_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbFormaPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbFormaPago_fpa_UsuarioModifica] FOREIGN KEY([fpa_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbFormaPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbFormaPago_fpa_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeduccionPago_hidp_UsuarioCrea] FOREIGN KEY([hidp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeduccionPago_hidp_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeduccionPago_hidp_UsuarioModifica] FOREIGN KEY([hidp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeduccionPago_hidp_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbHistorialDeduccionPago_cde_IdDeducciones] FOREIGN KEY([cde_IdDeducciones])
REFERENCES [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones])
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbHistorialDeduccionPago_cde_IdDeducciones]
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbHistorialDePago_hipa_IdHistorialDePago_Plani_tbHistorialDeduccionPago_hipa_IdHistorialDePago] FOREIGN KEY([hipa_IdHistorialDePago])
REFERENCES [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago])
GO
ALTER TABLE [Plani].[tbHistorialDeduccionPago] CHECK CONSTRAINT [FK_Plani_tbHistorialDePago_hipa_IdHistorialDePago_Plani_tbHistorialDeduccionPago_hipa_IdHistorialDePago]
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeIngresosPago_hip_UsuarioCrea] FOREIGN KEY([hip_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeIngresosPago_hip_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeIngresosPago_hip_UsuarioModifica] FOREIGN KEY([hip_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDeIngresosPago_hip_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbHistorialDeIngresosPago_cin_IdIngreso] FOREIGN KEY([cin_IdIngreso])
REFERENCES [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso])
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbHistorialDeIngresosPago_cin_IdIngreso]
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbHistorialDePago_hipa_IdHistorialDePago_Plani_tbHistorialDeIngresosPago_hipa_IdHistorialDePago] FOREIGN KEY([hipa_IdHistorialDePago])
REFERENCES [Plani].[tbHistorialDePago] ([hipa_IdHistorialDePago])
GO
ALTER TABLE [Plani].[tbHistorialDeIngresosPago] CHECK CONSTRAINT [FK_Plani_tbHistorialDePago_hipa_IdHistorialDePago_Plani_tbHistorialDeIngresosPago_hipa_IdHistorialDePago]
GO
ALTER TABLE [Plani].[tbHistorialDePago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDePago_hipa_UsuarioCrea] FOREIGN KEY([hipa_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDePago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDePago_hipa_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbHistorialDePago]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDePago_hipa_UsuarioModifica] FOREIGN KEY([hipa_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialDePago] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialDePago_hipa_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbHistorialDePago]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbPeriodos_peri_IdPeriodo_Plani_tbHistorialDePago_peri_IdPeriodo] FOREIGN KEY([peri_IdPeriodo])
REFERENCES [Plani].[tbPeriodos] ([peri_IdPeriodo])
GO
ALTER TABLE [Plani].[tbHistorialDePago] CHECK CONSTRAINT [FK_Plani_tbPeriodos_peri_IdPeriodo_Plani_tbHistorialDePago_peri_IdPeriodo]
GO
ALTER TABLE [Plani].[tbHistorialDePago]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbHistorialDePago_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbHistorialDePago] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbHistorialDePago_emp_Id]
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialLiquidaciones_hliq_UsuarioCrea] FOREIGN KEY([hliq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialLiquidaciones_hliq_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialLiquidaciones_hliq_UsuarioModifica] FOREIGN KEY([hliq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbHistorialLiquidaciones_hliq_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbHistorialLiquidaciones_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbHistorialLiquidaciones] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbHistorialLiquidaciones_emp_Id]
GO
ALTER TABLE [Plani].[tbInstitucionesFinancieras]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbInstitucionesFinancieras_insf_UsuarioCrea] FOREIGN KEY([insf_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbInstitucionesFinancieras] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbInstitucionesFinancieras_insf_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbInstitucionesFinancieras]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbInstitucionesFinancieras_insf_UsuarioModifica] FOREIGN KEY([insf_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbInstitucionesFinancieras] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbInstitucionesFinancieras_insf_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbISR]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbISR_isr_UsuarioCrea] FOREIGN KEY([isr_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbISR] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbISR_isr_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbISR]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbISR_isr_UsuarioModifica] FOREIGN KEY([isr_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbISR] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbISR_isr_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbISR]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbISR_tde_IdTipoDedu] FOREIGN KEY([tde_IdTipoDedu])
REFERENCES [Plani].[tbTipoDeduccion] ([tde_IdTipoDedu])
GO
ALTER TABLE [Plani].[tbISR] CHECK CONSTRAINT [FK_Plani_tbTipoDeduccion_tde_IdTipoDedu_Plani_tbISR_tde_IdTipoDedu]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidaciones_liqu_UsuarioCrea] FOREIGN KEY([liqu_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidaciones_liqu_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidaciones_liqu_UsuarioModifica] FOREIGN KEY([liqu_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidaciones_liqu_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbAuxilioDeCesantias_aces_IdAuxilioCesantia_Plani_tbLiquidaciones_aces_IdAuxilioCesantia] FOREIGN KEY([aces_IdAuxilioCesantia])
REFERENCES [Plani].[tbAuxilioDeCesantias] ([aces_IdAuxilioCesantia])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Plani_tbAuxilioDeCesantias_aces_IdAuxilioCesantia_Plani_tbLiquidaciones_aces_IdAuxilioCesantia]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbLiquidacionVacaciones_pvac_IdPagoVacaciones_Plani_tbLiquidaciones_pvac_IdPagoVacaciones] FOREIGN KEY([pvac_IdPagoVacaciones])
REFERENCES [Plani].[tbLiquidacionVacaciones] ([pvac_IdPagoVacaciones])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Plani_tbLiquidacionVacaciones_pvac_IdPagoVacaciones_Plani_tbLiquidaciones_pvac_IdPagoVacaciones]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbMotivoLiquidaciones_mliq_IdMotivoLiquidacion_Plani_tbLiquidaciones_mliq_IdMotivoLiquidacion] FOREIGN KEY([mliq_IdMotivoLiquidacion])
REFERENCES [Plani].[tbMotivoLiquidaciones] ([mliq_IdMotivoLiquidacion])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Plani_tbMotivoLiquidaciones_mliq_IdMotivoLiquidacion_Plani_tbLiquidaciones_mliq_IdMotivoLiquidacion]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbPreaviso_prea_IdPreaviso_Plani_tbLiquidaciones_prea_IdPreaviso] FOREIGN KEY([prea_IdPreaviso])
REFERENCES [Plani].[tbPreaviso] ([prea_IdPreaviso])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_Plani_tbPreaviso_prea_IdPreaviso_Plani_tbLiquidaciones_prea_IdPreaviso]
GO
ALTER TABLE [Plani].[tbLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbLiquidaciones_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [Plani].[tbLiquidaciones] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_Plani_tbLiquidaciones_emp_Id]
GO
ALTER TABLE [Plani].[tbLiquidacionVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidacionVacaciones_pvac_UsuarioCrea] FOREIGN KEY([pvac_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbLiquidacionVacaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidacionVacaciones_pvac_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbLiquidacionVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidacionVacaciones_pvac_UsuarioModifica] FOREIGN KEY([pvac_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbLiquidacionVacaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbLiquidacionVacaciones_pvac_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbMotivoLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbMotivoLiquidaciones_mliq_UsuarioCrea] FOREIGN KEY([mliq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbMotivoLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbMotivoLiquidaciones_mliq_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbMotivoLiquidaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbMotivoLiquidaciones_mliq_UsuarioModifica] FOREIGN KEY([mliq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbMotivoLiquidaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbMotivoLiquidaciones_mliq_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbPeriodos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPeriodos_peri_UsuarioCrea] FOREIGN KEY([peri_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbPeriodos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPeriodos_peri_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbPeriodos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPeriodos_peri_UsuarioModifica] FOREIGN KEY([peri_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbPeriodos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPeriodos_peri_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbPreaviso]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPreaviso_prea_UsuarioCrea] FOREIGN KEY([prea_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbPreaviso] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPreaviso_prea_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbPreaviso]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPreaviso_prea_UsuarioModifica] FOREIGN KEY([prea_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbPreaviso] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbPreaviso_prea_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbRamaActividad]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbRamaActividad_rama_UsuarioCrea] FOREIGN KEY([rama_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbRamaActividad] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbRamaActividad_rama_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbRamaActividad]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbRamaActividad_rama_UsuarioModifica] FOREIGN KEY([rama_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbRamaActividad] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbRamaActividad_rama_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbSalarioPorHora]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbSalarioPorHora_saph_UsuarioCrea] FOREIGN KEY([saph_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbSalarioPorHora] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbSalarioPorHora_saph_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbSalarioPorHora]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbSalarioPorHora_saph_UsuarioModifica] FOREIGN KEY([saph_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbSalarioPorHora] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbSalarioPorHora_saph_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbSalarioPorHora]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbRamaActividad_rama_Id_Plani_tbSalarioPorHora_rama_Id] FOREIGN KEY([rama_Id])
REFERENCES [Plani].[tbRamaActividad] ([rama_Id])
GO
ALTER TABLE [Plani].[tbSalarioPorHora] CHECK CONSTRAINT [FK_Plani_tbRamaActividad_rama_Id_Plani_tbSalarioPorHora_rama_Id]
GO
ALTER TABLE [Plani].[tbSalarioPorHora]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_Plani_tbSalarioPorHora_jor_Id] FOREIGN KEY([jor_Id])
REFERENCES [rrhh].[tbJornadas] ([jor_Id])
GO
ALTER TABLE [Plani].[tbSalarioPorHora] CHECK CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_Plani_tbSalarioPorHora_jor_Id]
GO
ALTER TABLE [Plani].[tbTechosDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTechosDeducciones_tddu_UsuarioCrea] FOREIGN KEY([tddu_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTechosDeducciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTechosDeducciones_tddu_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbTechosDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTechosDeducciones_tddu_UsuarioModifica] FOREIGN KEY([tddu_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTechosDeducciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTechosDeducciones_tddu_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbTechosDeducciones]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbTechosDeducciones_cde_IdDeducciones] FOREIGN KEY([cde_IdDeducciones])
REFERENCES [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones])
GO
ALTER TABLE [Plani].[tbTechosDeducciones] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbTechosDeducciones_cde_IdDeducciones]
GO
ALTER TABLE [Plani].[tbTipoDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoDeduccion_tde_UsuarioCrea] FOREIGN KEY([tde_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoDeduccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoDeduccion_tde_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbTipoDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoDeduccion_tde_UsuarioModifica] FOREIGN KEY([tde_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoDeduccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoDeduccion_tde_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleDeduccion_tpdd_UsuarioCrea] FOREIGN KEY([tpdd_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleDeduccion_tpdd_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleDeduccion_tpdd_UsuarioModifica] FOREIGN KEY([tpdd_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleDeduccion_tpdd_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbTipoPlanillaDetalleDeduccion_cde_IdDeducciones] FOREIGN KEY([cde_IdDeducciones])
REFERENCES [Plani].[tbCatalogoDeDeducciones] ([cde_IdDeducciones])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeDeducciones_cde_IdDeducciones_Plani_tbTipoPlanillaDetalleDeduccion_cde_IdDeducciones]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_Plani_tbTipoPlanillaDetalleDeduccion_cpla_IdPlanilla] FOREIGN KEY([cpla_IdPlanilla])
REFERENCES [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleDeduccion] CHECK CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_Plani_tbTipoPlanillaDetalleDeduccion_cpla_IdPlanilla]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleIngreso_tpdi_UsuarioCrea] FOREIGN KEY([tpdi_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleIngreso_tpdi_UsuarioCrea]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleIngreso_tpdi_UsuarioModifica] FOREIGN KEY([tpdi_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_Plani_tbTipoPlanillaDetalleIngreso_tpdi_UsuarioModifica]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbTipoPlanillaDetalleIngreso_cin_IdIngreso] FOREIGN KEY([cin_IdIngreso])
REFERENCES [Plani].[tbCatalogoDeIngresos] ([cin_IdIngreso])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso] CHECK CONSTRAINT [FK_Plani_tbCatalogoDeIngresos_cin_IdIngreso_Plani_tbTipoPlanillaDetalleIngreso_cin_IdIngreso]
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_Plani_tbTipoPlanillaDetalleIngreso_cpla_IdPlanilla] FOREIGN KEY([cpla_IdPlanilla])
REFERENCES [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla])
GO
ALTER TABLE [Plani].[tbTipoPlanillaDetalleIngreso] CHECK CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_Plani_tbTipoPlanillaDetalleIngreso_cpla_IdPlanilla]
GO
ALTER TABLE [rrhh].[tbAreas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbAreas_area_Usuariocrea] FOREIGN KEY([area_Usuariocrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbAreas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbAreas_area_Usuariocrea]
GO
ALTER TABLE [rrhh].[tbAreas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbAreas_area_Usuariomodifica] FOREIGN KEY([area_Usuariomodifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbAreas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbAreas_area_Usuariomodifica]
GO
ALTER TABLE [rrhh].[tbAreas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbAreas_car_Id] FOREIGN KEY([car_Id])
REFERENCES [rrhh].[tbCargos] ([car_Id])
GO
ALTER TABLE [rrhh].[tbAreas] CHECK CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbAreas_car_Id]
GO
ALTER TABLE [rrhh].[tbAreas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbSucursales_suc_Id_rrhh_tbAreas_suc_Id] FOREIGN KEY([suc_Id])
REFERENCES [rrhh].[tbSucursales] ([suc_Id])
GO
ALTER TABLE [rrhh].[tbAreas] CHECK CONSTRAINT [FK_rrhh_tbSucursales_suc_Id_rrhh_tbAreas_suc_Id]
GO
ALTER TABLE [rrhh].[tbCargos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCargos_car_UsuarioCrea] FOREIGN KEY([car_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCargos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCargos_car_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbCargos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCargos_car_UsuarioModifica] FOREIGN KEY([car_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCargos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCargos_car_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbCompetencias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetencias_comp_UsuarioCrea] FOREIGN KEY([comp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetencias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetencias_comp_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbCompetencias]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetencias_comp_UsuarioModifica] FOREIGN KEY([comp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetencias] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetencias_comp_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasPersona_cope_UsuarioCrea] FOREIGN KEY([cope_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasPersona_cope_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasPersona_cope_UsuarioModifica] FOREIGN KEY([cope_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasPersona_cope_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCompetencias_comp_Id_rrhh_tbCompetenciasPersona_comp_Id] FOREIGN KEY([comp_Id])
REFERENCES [rrhh].[tbCompetencias] ([comp_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona] CHECK CONSTRAINT [FK_rrhh_tbCompetencias_comp_Id_rrhh_tbCompetenciasPersona_comp_Id]
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbCompetenciasPersona_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasPersona] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbCompetenciasPersona_per_Id]
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasRequisicion_creq_UsuarioCrea] FOREIGN KEY([creq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasRequisicion_creq_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasRequisicion_creq_UsuarioModifica] FOREIGN KEY([creq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbCompetenciasRequisicion_creq_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCompetencias_comp_Id_rrhh_tbCompetenciasRequisicion_comp_Id] FOREIGN KEY([comp_Id])
REFERENCES [rrhh].[tbCompetencias] ([comp_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] CHECK CONSTRAINT [FK_rrhh_tbCompetencias_comp_Id_rrhh_tbCompetenciasRequisicion_comp_Id]
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbCompetenciasRequisicion_req_Id] FOREIGN KEY([req_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbCompetenciasRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbCompetenciasRequisicion_req_Id]
GO
ALTER TABLE [rrhh].[tbDepartamentos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbDepartamentos_depto_UsuarioCrea] FOREIGN KEY([depto_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbDepartamentos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbDepartamentos_depto_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbDepartamentos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbDepartamentos_depto_UsuarioModifica] FOREIGN KEY([depto_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbDepartamentos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbDepartamentos_depto_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbDepartamentos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbAreas_area_Id_rrhh_tbDepartamentos_area_Id] FOREIGN KEY([area_Id])
REFERENCES [rrhh].[tbAreas] ([area_Id])
GO
ALTER TABLE [rrhh].[tbDepartamentos] CHECK CONSTRAINT [FK_rrhh_tbAreas_area_Id_rrhh_tbDepartamentos_area_Id]
GO
ALTER TABLE [rrhh].[tbDepartamentos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbDepartamentos_car_Id] FOREIGN KEY([car_Id])
REFERENCES [rrhh].[tbCargos] ([car_Id])
GO
ALTER TABLE [rrhh].[tbDepartamentos] CHECK CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbDepartamentos_car_Id]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpleados_emp_UsuarioCrea] FOREIGN KEY([emp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpleados_emp_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpleados_emp_UsuarioModifica] FOREIGN KEY([emp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpleados_emp_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_rrhh_tbEmpleados_cpla_IdPlanilla] FOREIGN KEY([cpla_IdPlanilla])
REFERENCES [Plani].[tbCatalogoDePlanillas] ([cpla_IdPlanilla])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_Plani_tbCatalogoDePlanillas_cpla_IdPlanilla_rrhh_tbEmpleados_cpla_IdPlanilla]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Plani_tbFormaPago_fpa_IdFormaPago_rrhh_tbEmpleados_fpa_IdFormaPago] FOREIGN KEY([fpa_IdFormaPago])
REFERENCES [Plani].[tbFormaPago] ([fpa_IdFormaPago])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_Plani_tbFormaPago_fpa_IdFormaPago_rrhh_tbEmpleados_fpa_IdFormaPago]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbAreas_area_Id_rrhh_tbEmpleados_area_Id] FOREIGN KEY([area_Id])
REFERENCES [rrhh].[tbAreas] ([area_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_rrhh_tbAreas_area_Id_rrhh_tbEmpleados_area_Id]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbEmpleados_car_Id] FOREIGN KEY([car_Id])
REFERENCES [rrhh].[tbCargos] ([car_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbEmpleados_car_Id]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbDepartamentos_depto_Id_rrhh_tbEmpleados_depto_Id] FOREIGN KEY([depto_Id])
REFERENCES [rrhh].[tbDepartamentos] ([depto_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_rrhh_tbDepartamentos_depto_Id_rrhh_tbEmpleados_depto_Id]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbEmpleados_jor_Id] FOREIGN KEY([jor_Id])
REFERENCES [rrhh].[tbJornadas] ([jor_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbEmpleados_jor_Id]
GO
ALTER TABLE [rrhh].[tbEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbEmpleados_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbEmpleados] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbEmpleados_per_Id]
GO
ALTER TABLE [rrhh].[tbEmpresas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpresas_empr_UsuarioCrea] FOREIGN KEY([empr_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEmpresas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpresas_empr_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbEmpresas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpresas_empr_UsuarioModifica] FOREIGN KEY([empr_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEmpresas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEmpresas_empr_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoEmpleados_eqem_UsuarioCrea] FOREIGN KEY([eqem_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoEmpleados_eqem_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoEmpleados_eqem_UsuarioModifica] FOREIGN KEY([eqem_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoEmpleados_eqem_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbEquipoEmpleados_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbEquipoEmpleados_emp_Id]
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEquipoTrabajo_eqtra_Id_rrhh_tbEquipoEmpleados_eqtra_Id] FOREIGN KEY([eqtra_Id])
REFERENCES [rrhh].[tbEquipoTrabajo] ([eqtra_Id])
GO
ALTER TABLE [rrhh].[tbEquipoEmpleados] CHECK CONSTRAINT [FK_rrhh_tbEquipoTrabajo_eqtra_Id_rrhh_tbEquipoEmpleados_eqtra_Id]
GO
ALTER TABLE [rrhh].[tbEquipoTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoTrabajo_eqtra_UsuarioCrea] FOREIGN KEY([eqtra_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEquipoTrabajo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoTrabajo_eqtra_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbEquipoTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoTrabajo_eqtra_UsuarioModifica] FOREIGN KEY([eqtra_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbEquipoTrabajo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbEquipoTrabajo_eqtra_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbFaseSeleccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFaseSeleccion_fsel_UsuarioCrea] FOREIGN KEY([fsel_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbFaseSeleccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFaseSeleccion_fsel_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbFaseSeleccion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFaseSeleccion_fsel_UsuarioModifica] FOREIGN KEY([fsel_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbFaseSeleccion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFaseSeleccion_fsel_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbFaseSeleccion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbFasesReclutamiento_fare_Id_rrhh_tbFaseSeleccion_fare_Id] FOREIGN KEY([fare_Id])
REFERENCES [rrhh].[tbFasesReclutamiento] ([fare_Id])
GO
ALTER TABLE [rrhh].[tbFaseSeleccion] CHECK CONSTRAINT [FK_rrhh_tbFasesReclutamiento_fare_Id_rrhh_tbFaseSeleccion_fare_Id]
GO
ALTER TABLE [rrhh].[tbFasesReclutamiento]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFasesReclutamiento_fare_UsuarioCrea] FOREIGN KEY([fare_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbFasesReclutamiento] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFasesReclutamiento_fare_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbFasesReclutamiento]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFasesReclutamiento_fare_UsuarioModifica] FOREIGN KEY([fare_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbFasesReclutamiento] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbFasesReclutamiento_fare_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHabilidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidades_habi_UsuarioCrea] FOREIGN KEY([habi_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidades_habi_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHabilidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidades_habi_UsuarioModifica] FOREIGN KEY([habi_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidades_habi_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesPersona_hape_UsuarioCrea] FOREIGN KEY([hape_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesPersona_hape_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesPersona_hape_UsuarioModifica] FOREIGN KEY([hape_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesPersona_hape_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbHabilidades_habi_Id_rrhh_tbHabilidadesPersona_habi_Id] FOREIGN KEY([habi_Id])
REFERENCES [rrhh].[tbHabilidades] ([habi_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona] CHECK CONSTRAINT [FK_rrhh_tbHabilidades_habi_Id_rrhh_tbHabilidadesPersona_habi_Id]
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbHabilidadesPersona_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesPersona] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbHabilidadesPersona_per_Id]
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesRequisicion_hreq_UsuarioCrea] FOREIGN KEY([hreq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesRequisicion_hreq_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesRequisicion_hreq_UsuarioModifica] FOREIGN KEY([hreq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHabilidadesRequisicion_hreq_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbHabilidades_habi_Id_rrhh_tbHabilidadesRequisicion_habi_Id] FOREIGN KEY([habi_Id])
REFERENCES [rrhh].[tbHabilidades] ([habi_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion] CHECK CONSTRAINT [FK_rrhh_tbHabilidades_habi_Id_rrhh_tbHabilidadesRequisicion_habi_Id]
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbHabilidadesRequisicion_req_Id] FOREIGN KEY([req_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbHabilidadesRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbHabilidadesRequisicion_req_Id]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAmonestaciones_hamo_UsuarioCrea] FOREIGN KEY([hamo_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAmonestaciones_hamo_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAmonestaciones_hamo_UsuarioModifica] FOREIGN KEY([hamo_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAmonestaciones_hamo_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialAmonestaciones_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialAmonestaciones_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbHistorialAmonestaciones_hamo_Id_rrhh_tbHistorialAmonestaciones_hamo_AmonestacionAnterior] FOREIGN KEY([hamo_AmonestacionAnterior])
REFERENCES [rrhh].[tbHistorialAmonestaciones] ([hamo_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] CHECK CONSTRAINT [FK_rrhh_tbHistorialAmonestaciones_hamo_Id_rrhh_tbHistorialAmonestaciones_hamo_AmonestacionAnterior]
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTipoAmonestaciones_tamo_Id_rrhh_tbHistorialAmonestaciones_tamo_Id] FOREIGN KEY([tamo_Id])
REFERENCES [rrhh].[tbTipoAmonestaciones] ([tamo_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAmonestaciones] CHECK CONSTRAINT [FK_rrhh_tbTipoAmonestaciones_tamo_Id_rrhh_tbHistorialAmonestaciones_tamo_Id]
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAudienciaDescargo_aude_UsuarioCrea] FOREIGN KEY([aude_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAudienciaDescargo_aude_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAudienciaDescargo_aude_UsuarioModifica] FOREIGN KEY([aude_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialAudienciaDescargo_aude_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialAudienciaDescargo_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialAudienciaDescargo] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialAudienciaDescargo_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialCargos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialCargos_hcar_UsuarioCrea] FOREIGN KEY([hcar_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialCargos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialCargos_hcar_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialCargos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialCargos_hcar_UsuarioModifica] FOREIGN KEY([hcar_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialCargos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialCargos_hcar_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialCargos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbHistorialCargos_car_IdNuevo] FOREIGN KEY([car_IdNuevo])
REFERENCES [rrhh].[tbCargos] ([car_Id])
GO
ALTER TABLE [rrhh].[tbHistorialCargos] CHECK CONSTRAINT [FK_rrhh_tbCargos_car_Id_rrhh_tbHistorialCargos_car_IdNuevo]
GO
ALTER TABLE [rrhh].[tbHistorialCargos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialCargos_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialCargos] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialCargos_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialContrataciones_hcon_UsuarioCrea] FOREIGN KEY([hcon_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialContrataciones_hcon_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialContrataciones_hcon_UsuarioModifica] FOREIGN KEY([hcon_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialContrataciones_hcon_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbDepartamentos_depto_Id_rrhh_tbHistorialContrataciones_depto_Id] FOREIGN KEY([depto_Id])
REFERENCES [rrhh].[tbDepartamentos] ([depto_Id])
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones] CHECK CONSTRAINT [FK_rrhh_tbDepartamentos_depto_Id_rrhh_tbHistorialContrataciones_depto_Id]
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbSeleccionCandidatos_scan_Id_rrhh_tbHistorialContrataciones_scan_Id] FOREIGN KEY([scan_Id])
REFERENCES [rrhh].[tbSeleccionCandidatos] ([scan_Id])
GO
ALTER TABLE [rrhh].[tbHistorialContrataciones] CHECK CONSTRAINT [FK_rrhh_tbSeleccionCandidatos_scan_Id_rrhh_tbHistorialContrataciones_scan_Id]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialHorasTrabajadas_htra_UsuarioCrea] FOREIGN KEY([htra_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialHorasTrabajadas_htra_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialHorasTrabajadas_htra_UsuarioModifica] FOREIGN KEY([htra_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialHorasTrabajadas_htra_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialHorasTrabajadas_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialHorasTrabajadas_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbHistorialHorasTrabajadas_jor_Id] FOREIGN KEY([jor_Id])
REFERENCES [rrhh].[tbJornadas] ([jor_Id])
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] CHECK CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbHistorialHorasTrabajadas_jor_Id]
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTipoHoras_tiho_Id_rrhh_tbHistorialHorasTrabajadas_tiho_Id] FOREIGN KEY([tiho_Id])
REFERENCES [rrhh].[tbTipoHoras] ([tiho_Id])
GO
ALTER TABLE [rrhh].[tbHistorialHorasTrabajadas] CHECK CONSTRAINT [FK_rrhh_tbTipoHoras_tiho_Id_rrhh_tbHistorialHorasTrabajadas_tiho_Id]
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialIncapacidades_hinc_UsuarioCrea] FOREIGN KEY([hinc_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialIncapacidades_hinc_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialIncapacidades_hinc_UsuarioModifica] FOREIGN KEY([hinc_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialIncapacidades_hinc_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialIncapacidades_Emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialIncapacidades_Emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTipoIncapacidades_ticn_Id_rrhh_tbHistorialIncapacidades_ticn_Id] FOREIGN KEY([ticn_Id])
REFERENCES [rrhh].[tbTipoIncapacidades] ([ticn_Id])
GO
ALTER TABLE [rrhh].[tbHistorialIncapacidades] CHECK CONSTRAINT [FK_rrhh_tbTipoIncapacidades_ticn_Id_rrhh_tbHistorialIncapacidades_ticn_Id]
GO
ALTER TABLE [rrhh].[tbHistorialPermisos]  WITH CHECK ADD  CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialPermisos_hper_UsuarioCrea] FOREIGN KEY([hper_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialPermisos] CHECK CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialPermisos_hper_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialPermisos]  WITH CHECK ADD  CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialPermisos_hper_UsuarioModifica] FOREIGN KEY([hper_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialPermisos] CHECK CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialPermisos_hper_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialPermisos]  WITH CHECK ADD  CONSTRAINT ['FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialPermisos_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialPermisos] CHECK CONSTRAINT ['FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialPermisos_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialPermisos]  WITH CHECK ADD  CONSTRAINT ['FK_rrhh_tbTipoPermisos_tper_Id_rrhh_tbHistorialPermisos_tper_Id] FOREIGN KEY([tper_Id])
REFERENCES [rrhh].[tbTipoPermisos] ([tper_Id])
GO
ALTER TABLE [rrhh].[tbHistorialPermisos] CHECK CONSTRAINT ['FK_rrhh_tbTipoPermisos_tper_Id_rrhh_tbHistorialPermisos_tper_Id]
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialRefrendamientos_href_UsuarioCrea] FOREIGN KEY([href_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialRefrendamientos_href_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialRefrendamientos_href_UsuarioModifica] FOREIGN KEY([href_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialRefrendamientos_href_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialRefrendamientos_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialRefrendamientos] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialRefrendamientos_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialSalidas_hsal_UsuarioCrea] FOREIGN KEY([hsal_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialSalidas_hsal_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialSalidas_hsal_UsuarioModifica] FOREIGN KEY([hsal_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialSalidas_hsal_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialSalidas_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialSalidas_emp_Id]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRazonSalidas_rsal_Id_rrhh_tbHistorialSalidas_rsal_Id] FOREIGN KEY([rsal_Id])
REFERENCES [rrhh].[tbRazonSalidas] ([rsal_Id])
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] CHECK CONSTRAINT [FK_rrhh_tbRazonSalidas_rsal_Id_rrhh_tbHistorialSalidas_rsal_Id]
GO
ALTER TABLE [rrhh].[tbHistorialSalidas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTipoSalidas_tsal_Id_rrhh_tbHistorialSalidas_tsal_Id] FOREIGN KEY([tsal_Id])
REFERENCES [rrhh].[tbTipoSalidas] ([tsal_Id])
GO
ALTER TABLE [rrhh].[tbHistorialSalidas] CHECK CONSTRAINT [FK_rrhh_tbTipoSalidas_tsal_Id_rrhh_tbHistorialSalidas_tsal_Id]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialVacaciones_hvac_UsuarioCrea] FOREIGN KEY([hvac_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialVacaciones_hvac_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialVacaciones_hvac_UsuarioModifica] FOREIGN KEY([hvac_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHistorialVacaciones_hvac_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialVacaciones_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbHistorialVacaciones_emp_Id]
GO
ALTER TABLE [rrhh].[tbHorarios]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHorarios_hor_UsuarioCrea] FOREIGN KEY([hor_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHorarios] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHorarios_hor_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbHorarios]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHorarios_hor_UsuarioModifica] FOREIGN KEY([hor_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbHorarios] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbHorarios_hor_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbHorarios]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbHorarios_jor_Id] FOREIGN KEY([jor_Id])
REFERENCES [rrhh].[tbJornadas] ([jor_Id])
GO
ALTER TABLE [rrhh].[tbHorarios] CHECK CONSTRAINT [FK_rrhh_tbJornadas_jor_Id_rrhh_tbHorarios_jor_Id]
GO
ALTER TABLE [rrhh].[tbIdiomaPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomaPersona_idpe_UsuarioCrea] FOREIGN KEY([idpe_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomaPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomaPersona_idpe_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbIdiomaPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomaPersona_idpe_UsuarioModifica] FOREIGN KEY([idpe_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomaPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomaPersona_idpe_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbIdiomaPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbIdiomas_idi_Id_rrhh_tbIdiomaPersona_idi_Id] FOREIGN KEY([idi_Id])
REFERENCES [rrhh].[tbIdiomas] ([idi_Id])
GO
ALTER TABLE [rrhh].[tbIdiomaPersona] CHECK CONSTRAINT [FK_rrhh_tbIdiomas_idi_Id_rrhh_tbIdiomaPersona_idi_Id]
GO
ALTER TABLE [rrhh].[tbIdiomaPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbIdiomaPersona_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbIdiomaPersona] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbIdiomaPersona_per_Id]
GO
ALTER TABLE [rrhh].[tbIdiomas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomas_idi_UsuarioCrea] FOREIGN KEY([idi_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomas_idi_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbIdiomas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomas_idi_UsuarioModifica] FOREIGN KEY([idi_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomas_idi_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomasRequisicion_ireq_UsuarioCrea] FOREIGN KEY([ireq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomasRequisicion_ireq_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomasRequisicion_ireq_UsuarioModifica] FOREIGN KEY([ireq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbIdiomasRequisicion_ireq_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbIdiomas_idi_Id_rrhh_tbIdiomasRequisicion_idi_Id] FOREIGN KEY([idi_Id])
REFERENCES [rrhh].[tbIdiomas] ([idi_Id])
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion] CHECK CONSTRAINT [FK_rrhh_tbIdiomas_idi_Id_rrhh_tbIdiomasRequisicion_idi_Id]
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbIdiomasRequisicion_req_Id] FOREIGN KEY([req_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbIdiomasRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbIdiomasRequisicion_req_Id]
GO
ALTER TABLE [rrhh].[tbJornadas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbJornadas_jor_UsuarioCrea] FOREIGN KEY([jor_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbJornadas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbJornadas_jor_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbJornadas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbJornadas_jor_UsuarioModifica] FOREIGN KEY([jor_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbJornadas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbJornadas_jor_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbNacionalidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioCrea] FOREIGN KEY([nac_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbNacionalidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbNacionalidades]  WITH CHECK ADD  CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioCrea] FOREIGN KEY([nac_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbNacionalidades] CHECK CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbNacionalidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioModifica] FOREIGN KEY([nac_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbNacionalidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbNacionalidades]  WITH CHECK ADD  CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioModifica] FOREIGN KEY([nac_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbNacionalidades] CHECK CONSTRAINT ['FK_Acce_tbUsuario_usu_Id_rrhh_tbNacionalidades_nac_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPersonas_per_UsuarioCrea] FOREIGN KEY([per_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPersonas_per_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPersonas_per_UsuarioModifica] FOREIGN KEY([per_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPersonas_per_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbNacionalidades_nac_Id_rrhh_tbPersonas_nac_Id] FOREIGN KEY([nac_Id])
REFERENCES [rrhh].[tbNacionalidades] ([nac_Id])
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [FK_rrhh_tbNacionalidades_nac_Id_rrhh_tbPersonas_nac_Id]
GO
ALTER TABLE [rrhh].[tbPrestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPrestaciones_pres_UsuarioCrea] FOREIGN KEY([pres_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbPrestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPrestaciones_pres_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbPrestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPrestaciones_pres_UsuarioModifica] FOREIGN KEY([pres_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbPrestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbPrestaciones_pres_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbRazonSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRazonSalidas_rsal_UsuarioCrea] FOREIGN KEY([rsal_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRazonSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRazonSalidas_rsal_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbRazonSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRazonSalidas_rsal_UsuarioModifica] FOREIGN KEY([rsal_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRazonSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRazonSalidas_rsal_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspeciales]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspeciales_resp_UsuarioCrea] FOREIGN KEY([resp_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspeciales] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspeciales_resp_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspeciales]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspeciales_resp_UsuarioModifica] FOREIGN KEY([resp_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspeciales] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspeciales_resp_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesPersona_rep_UsuarioCrea] FOREIGN KEY([rep_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesPersona_rep_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesPersona_rep_UsuarioModifica] FOREIGN KEY([rep_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesPersona_rep_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbRequerimientosEspecialesPersona_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbRequerimientosEspecialesPersona_per_Id]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequerimientosEspeciales_resp_Id_rrhh_tbRequerimientosEspecialesPersona_resp_Id] FOREIGN KEY([resp_Id])
REFERENCES [rrhh].[tbRequerimientosEspeciales] ([resp_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesPersona] CHECK CONSTRAINT [FK_rrhh_tbRequerimientosEspeciales_resp_Id_rrhh_tbRequerimientosEspecialesPersona_resp_Id]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesRequisicion_rer_UsuarioCrea] FOREIGN KEY([rer_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesRequisicion_rer_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesRequisicion_rer_UsuarioModifica] FOREIGN KEY([rer_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequerimientosEspecialesRequisicion_rer_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequerimientosEspeciales_resp_Id_rrhh_tbRequerimientosEspecialesRequisicion_resp_Id] FOREIGN KEY([resp_Id])
REFERENCES [rrhh].[tbRequerimientosEspeciales] ([resp_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequerimientosEspeciales_resp_Id_rrhh_tbRequerimientosEspecialesRequisicion_resp_Id]
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbRequerimientosEspecialesRequisicion_req_Id] FOREIGN KEY([req_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbRequerimientosEspecialesRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbRequerimientosEspecialesRequisicion_req_Id]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequisiciones_req_UsuarioCrea] FOREIGN KEY([req_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequisiciones_req_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequisiciones_req_UsuarioModifica] FOREIGN KEY([req_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbRequisiciones_req_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSeleccionCandidatos_scan_UsuarioCrea] FOREIGN KEY([scan_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSeleccionCandidatos_scan_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSeleccionCandidatos_scan_UsuarioModifica] FOREIGN KEY([scan_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSeleccionCandidatos_scan_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbFaseSeleccion_fsel_Id_rrhh_tbSeleccionCandidatos_fare_Id] FOREIGN KEY([fare_Id])
REFERENCES [rrhh].[tbFaseSeleccion] ([fsel_Id])
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] CHECK CONSTRAINT [FK_rrhh_tbFaseSeleccion_fsel_Id_rrhh_tbSeleccionCandidatos_fare_Id]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbSeleccionCandidatos_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbSeleccionCandidatos_per_Id]
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbSeleccionCandidatos_rper_Id] FOREIGN KEY([rper_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbSeleccionCandidatos] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbSeleccionCandidatos_rper_Id]
GO
ALTER TABLE [rrhh].[tbSucursales]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSucursales_suc_UsuarioCrea] FOREIGN KEY([suc_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSucursales] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSucursales_suc_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbSucursales]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSucursales_suc_UsuarioModifica] FOREIGN KEY([suc_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSucursales] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSucursales_suc_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbSucursales]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpresas_empr_Id_rrhh_tbSucursales_empr_Id] FOREIGN KEY([empr_Id])
REFERENCES [rrhh].[tbEmpresas] ([empr_Id])
GO
ALTER TABLE [rrhh].[tbSucursales] CHECK CONSTRAINT [FK_rrhh_tbEmpresas_empr_Id_rrhh_tbSucursales_empr_Id]
GO
ALTER TABLE [rrhh].[tbSueldos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSueldos_sue_UsuarioCrea] FOREIGN KEY([sue_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSueldos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSueldos_sue_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbSueldos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSueldos_sue_UsuarioModifica] FOREIGN KEY([sue_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbSueldos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbSueldos_sue_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbSueldos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbSueldos_emp_Id] FOREIGN KEY([emp_Id])
REFERENCES [rrhh].[tbEmpleados] ([emp_Id])
GO
ALTER TABLE [rrhh].[tbSueldos] CHECK CONSTRAINT [FK_rrhh_tbEmpleados_emp_Id_rrhh_tbSueldos_emp_Id]
GO
ALTER TABLE [rrhh].[tbSueldos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbSueldos_sue_Id_rrhh_tbSueldos_sue_SueldoAnterior] FOREIGN KEY([sue_SueldoAnterior])
REFERENCES [rrhh].[tbSueldos] ([sue_Id])
GO
ALTER TABLE [rrhh].[tbSueldos] CHECK CONSTRAINT [FK_rrhh_tbSueldos_sue_Id_rrhh_tbSueldos_sue_SueldoAnterior]
GO
ALTER TABLE [rrhh].[tbSueldos]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTipoMonedas_tmon_Id_rrhh_tbSueldos_tmon_Id] FOREIGN KEY([tmon_Id])
REFERENCES [rrhh].[tbTipoMonedas] ([tmon_Id])
GO
ALTER TABLE [rrhh].[tbSueldos] CHECK CONSTRAINT [FK_rrhh_tbTipoMonedas_tmon_Id_rrhh_tbSueldos_tmon_Id]
GO
ALTER TABLE [rrhh].[tbTipoAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoAmonestaciones_tamo_UsuarioCrea] FOREIGN KEY([tamo_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoAmonestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoAmonestaciones_tamo_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoAmonestaciones]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoAmonestaciones_tamo_UsuarioModifica] FOREIGN KEY([tamo_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoAmonestaciones] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoAmonestaciones_tamo_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTipoHoras]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoHoras_tiho_UsuarioCrea] FOREIGN KEY([tiho_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoHoras] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoHoras_tiho_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoHoras]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoHoras_tiho_UsuarioModifica] FOREIGN KEY([tiho_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoHoras] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoHoras_tiho_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTipoIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoIncapacidades_ticn_UsuarioCrea] FOREIGN KEY([ticn_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoIncapacidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoIncapacidades_ticn_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoIncapacidades]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoIncapacidades_ticn_UsuarioModifica] FOREIGN KEY([ticn_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoIncapacidades] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoIncapacidades_ticn_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTipoMonedas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoMonedas_tmon_UsuarioCrea] FOREIGN KEY([tmon_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoMonedas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoMonedas_tmon_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoMonedas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoMonedas_tmon_UsuarioModifica] FOREIGN KEY([tmon_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoMonedas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoMonedas_tmon_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTipoPermisos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoPermisos_tper_UsuarioCrea] FOREIGN KEY([tper_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoPermisos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoPermisos_tper_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoPermisos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoPermisos_tper_UsuarioModifica] FOREIGN KEY([tper_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoPermisos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoPermisos_tper_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTipoSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoSalidas_tsal_UsuarioCrea] FOREIGN KEY([tsal_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoSalidas_tsal_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTipoSalidas]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoSalidas_tsal_UsuarioModifica] FOREIGN KEY([tsal_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTipoSalidas] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTipoSalidas_tsal_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTitulos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulos_titu_UsuarioCrea] FOREIGN KEY([titu_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulos_titu_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTitulos]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulos_titu_UsuarioModifica] FOREIGN KEY([titu_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulos] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulos_titu_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTitulosPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosPersona_tipe_UsuarioCrea] FOREIGN KEY([tipe_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosPersona_tipe_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTitulosPersona]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosPersona_tipe_UsuarioModifica] FOREIGN KEY([tipe_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosPersona] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosPersona_tipe_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTitulosPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbTitulosPersona_per_Id] FOREIGN KEY([per_Id])
REFERENCES [rrhh].[tbPersonas] ([per_Id])
GO
ALTER TABLE [rrhh].[tbTitulosPersona] CHECK CONSTRAINT [FK_rrhh_tbPersonas_per_Id_rrhh_tbTitulosPersona_per_Id]
GO
ALTER TABLE [rrhh].[tbTitulosPersona]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTitulos_titu_Id_rrhh_tbTitulosPersona_titu_Id] FOREIGN KEY([titu_Id])
REFERENCES [rrhh].[tbTitulos] ([titu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosPersona] CHECK CONSTRAINT [FK_rrhh_tbTitulos_titu_Id_rrhh_tbTitulosPersona_titu_Id]
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosRequisicion_treq_UsuarioCrea] FOREIGN KEY([treq_UsuarioCrea])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosRequisicion_treq_UsuarioCrea]
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosRequisicion_treq_UsuarioModifica] FOREIGN KEY([treq_UsuarioModifica])
REFERENCES [Acce].[tbUsuario] ([usu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion] CHECK CONSTRAINT [FK_Acce_tbUsuario_usu_Id_rrhh_tbTitulosRequisicion_treq_UsuarioModifica]
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbTitulosRequisicion_req_Id] FOREIGN KEY([req_Id])
REFERENCES [rrhh].[tbRequisiciones] ([req_Id])
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion] CHECK CONSTRAINT [FK_rrhh_tbRequisiciones_req_Id_rrhh_tbTitulosRequisicion_req_Id]
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion]  WITH CHECK ADD  CONSTRAINT [FK_rrhh_tbTitulos_titu_Id_rrhh_tbTitulosRequisicion_titu_Id] FOREIGN KEY([titu_Id])
REFERENCES [rrhh].[tbTitulos] ([titu_Id])
GO
ALTER TABLE [rrhh].[tbTitulosRequisicion] CHECK CONSTRAINT [FK_rrhh_tbTitulos_titu_Id_rrhh_tbTitulosRequisicion_titu_Id]
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones]  WITH CHECK ADD  CONSTRAINT [CK_tbHistorialVacaciones_hvac_MesVacaciones] CHECK  (([hvac_MesVacaciones]>=(1) AND [hvac_MesVacaciones]<=(12)))
GO
ALTER TABLE [rrhh].[tbHistorialVacaciones] CHECK CONSTRAINT [CK_tbHistorialVacaciones_hvac_MesVacaciones]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [CK_tbPersonas_per_CorreoElectronico] CHECK  (([per_CorreoElectronico] like '%@%'))
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [CK_tbPersonas_per_CorreoElectronico]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [CK_tbPersonas_per_Edad] CHECK  (([per_Edad]>=(18)))
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [CK_tbPersonas_per_Edad]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [CK_tbPersonas_per_EstadoCivil] CHECK  (([per_EstadoCivil] like '%S%' OR [per_EstadoCivil] like '%C%' OR [per_EstadoCivil] like '%D%' OR [per_EstadoCivil] like '%U%' OR [per_EstadoCivil] like '%V%'))
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [CK_tbPersonas_per_EstadoCivil]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [CK_tbPersonas_per_Sexo] CHECK  (([per_Sexo]='M' OR [per_Sexo]='F'))
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [CK_tbPersonas_per_Sexo]
GO
ALTER TABLE [rrhh].[tbPersonas]  WITH CHECK ADD  CONSTRAINT [CK_tbPersonas_per_TipoSangre] CHECK  (([per_TipoSangre] like '%A+%' OR [per_TipoSangre] like '%A-%' OR [per_TipoSangre] like '%B+%' OR [per_TipoSangre] like '%B-%' OR [per_TipoSangre] like '%AB+%' OR [per_TipoSangre] like '%AB-%' OR [per_TipoSangre] like '%O+%' OR [per_TipoSangre] like '%O-%'))
GO
ALTER TABLE [rrhh].[tbPersonas] CHECK CONSTRAINT [CK_tbPersonas_per_TipoSangre]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [CK_EdadMaxima] CHECK  (([req_EdadMaxima]>(18)))
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [CK_EdadMaxima]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [CK_EdadMinima] CHECK  (([req_EdadMinima]>(18)))
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [CK_EdadMinima]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [CK_EstadoCivil] CHECK  (([req_EstadoCivil]='S' OR [req_EstadoCivil]='C' OR [req_EstadoCivil]='V' OR [req_EstadoCivil]='D' OR [req_EstadoCivil]='U'))
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [CK_EstadoCivil]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [CK_RRHH_tbRequisiones_EstadoCivil] CHECK  (([req_EstadoCivil]='S' OR [req_EstadoCivil]='C' OR [req_EstadoCivil]='V' OR [req_EstadoCivil]='D' OR [req_EstadoCivil]='U'))
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [CK_RRHH_tbRequisiones_EstadoCivil]
GO
ALTER TABLE [rrhh].[tbRequisiciones]  WITH CHECK ADD  CONSTRAINT [CK_tbRequisiciones_req_Sexo] CHECK  (([req_Sexo]='M' OR [req_Sexo]='F'))
GO
ALTER TABLE [rrhh].[tbRequisiciones] CHECK CONSTRAINT [CK_tbRequisiciones_req_Sexo]
GO
/****** Object:  StoredProcedure [dbo].[UDP_dbo_tbFasesReclutamiento_DELETE]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UDP_dbo_tbFasesReclutamiento_DELETE]
(
@fare_id int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
UPDATE dbo.tbFasesReclutamiento
SET  [fare_Estado]=1
WHERE [fare_Id]=@fare_id

SELECT CAST(SCOPE_IDENTITY() AS varchar) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UDP_dbo_tbFasesReclutamiento_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UDP_dbo_tbFasesReclutamiento_Insert]
(
@fare_id int,
@fare_Descripcion nvarchar(50),
@fare_Estado bit,
@usuario int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
INSERT INTO dbo.tbFasesReclutamiento(
[fare_Id],
[fare_Descripcion],
[fare_Estado],
[fare_UsuarioCrea],
[fare_FechaCrea],
[fare_UsuarioModifica],
[fare_FechaModifica]
)
VALUES(
(SELECT ISNULL((SELECT MAX([fare_Id]) FROM dbo.tbFasesReclutamiento),0) + 1),
@fare_id,
@fare_Descripcion,
@fare_Estado,
@usuario,
(SELECT GETDATE())
)
SELECT CAST(SCOPE_IDENTITY() AS varchar) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UDP_dbo_tbFasesReclutamiento_SELECT]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UDP_dbo_tbFasesReclutamiento_SELECT]
AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
SELECT fare_Id, fare_Descripcion, fare_Estado, fare_UsuarioCrea, fare_FechaCrea, fare_UsuarioModifica, fare_FechaModifica
FROM dbo.tbFasesReclutamiento

SELECT CAST(SCOPE_IDENTITY() AS varchar) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UDP_dbo_tbFasesReclutamiento_UPDATE]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UDP_dbo_tbFasesReclutamiento_UPDATE]
(
@fare_id int,
@fare_Descripcion nvarchar(50),
@fare_UsuarioModifica int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
UPDATE dbo.tbFasesReclutamiento
SET [fare_Descripcion]=@fare_Descripcion, [fare_UsuarioModifica]=@fare_UsuarioModifica, [fare_FechaModifica]=GETDATE()
WHERE [fare_Id]=@fare_id

SELECT CAST(SCOPE_IDENTITY() AS varchar) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani.tbAuxilioDeCesantias_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani.tbAuxilioDeCesantias_Delete]
    (
        @aces_IdAuxilioCesantia INT
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
          DELETE FROM [Plani].[tbAuxilioDeCesantias]
      WHERE [aces_IdAuxilioCesantia] = @aces_IdAuxilioCesantia
          COMMIT TRAN
          SET  @msj  = @aces_IdAuxilioCesantia
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani.tbAuxilioDeCesantias_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani.tbAuxilioDeCesantias_Insert]
    (
        @aces_RangoInicioMeses INT,
		@aces_RangoFinMeses INT,
		@aces_DiasAuxilioCesantia INT,
		@aces_UsuarioCrea INT,
		@aces_FechaCrea DATETIME,
		@aces_Activo BIT)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
            DECLARE @aces_IdAuxilioCesantia INT
            SET @aces_IdAuxilioCesantia = (SELECT ISNULL(MAX(aces_IdAuxilioCesantia) + 1, 1) FROM [Plani].[tbAuxilioDeCesantias]);
           INSERT INTO [Plani].[tbAuxilioDeCesantias]
           ([aces_IdAuxilioCesantia],[aces_RangoInicioMeses],[aces_RangoFinMeses],
            [aces_DiasAuxilioCesantia],[aces_UsuarioCrea],[aces_FechaCrea],[aces_Activo])
	    VALUES(
           @aces_IdAuxilioCesantia,
		   @aces_RangoInicioMeses,
		   @aces_RangoFinMeses,
		   @aces_DiasAuxilioCesantia,
		   @aces_UsuarioCrea ,
		   @aces_FechaCrea ,
		   @aces_Activo)
          COMMIT TRAN
          SET  @msj  = @aces_IdAuxilioCesantia
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani.tbAuxilioDeCesantias_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani.tbAuxilioDeCesantias_Update]
    (
	    @aces_IdAuxilioCesantia INT,
        @aces_RangoInicioMeses INT,
		@aces_RangoFinMeses INT,
		@aces_DiasAuxilioCesantia INT,
		@aces_UsuarioModifica INT,
		@aces_FechaModifica DATETIME)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
           UPDATE [Plani].[tbAuxilioDeCesantias]
			  SET 
			     [aces_RangoInicioMeses] = @aces_RangoInicioMeses,
			     [aces_RangoFinMeses] = @aces_RangoFinMeses,
			     [aces_DiasAuxilioCesantia] = @aces_DiasAuxilioCesantia,
			     [aces_UsuarioModifica] = @aces_UsuarioModifica,
			     [aces_FechaModifica] = @aces_FechaModifica
			WHERE [aces_IdAuxilioCesantia] = @aces_IdAuxilioCesantia
          COMMIT TRAN
          SET  @msj  = @aces_IdAuxilioCesantia
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_CatalogoDeduccionesEdit_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_CatalogoDeduccionesEdit_Select]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla INT
)
AS
    BEGIN
	SET NOCOUNT ON
        SELECT ttpdd.cde_IdDeducciones, 
               tcdd.cde_DescripcionDeduccion, 
               1 AS 'checked'
        FROM Plani.tbTipoPlanillaDetalleDeduccion ttpdd
             LEFT JOIN Plani.tbCatalogoDeDeducciones tcdd ON ttpdd.cde_IdDeducciones = tcdd.cde_IdDeducciones
        WHERE ttpdd.cpla_IdPlanilla = @cpla_IdPlanilla
              AND tcdd.cde_Activo = 1
        UNION ALL
        SELECT tcdd.cde_IdDeducciones, 
               tcdd.cde_DescripcionDeduccion, 
               0 AS 'checked'
        FROM Plani.tbCatalogoDeDeducciones tcdd
        WHERE tcdd.cde_IdDeducciones NOT IN
        (
            SELECT ttpdd.cde_IdDeducciones
            FROM Plani.tbTipoPlanillaDetalleDeduccion ttpdd
            WHERE ttpdd.cpla_IdPlanilla = @cpla_IdPlanilla
                  AND ttpdd.tpdd_Activo = 1
        )
              AND tcdd.cde_Activo = 1
        ORDER BY 1;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_CatalogoDeIngresosEdit_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_CatalogoDeIngresosEdit_Select]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla INT
)
AS
    BEGIN
        SELECT ttpdi.cin_IdIngreso, 
               tcdi.cin_DescripcionIngreso, 
               1 AS 'checked'
        FROM Plani.tbTipoPlanillaDetalleIngreso ttpdi
             LEFT JOIN Plani.tbCatalogoDeIngresos tcdi ON ttpdi.cin_IdIngreso = tcdi.cin_IdIngreso
        WHERE ttpdi.cpla_IdPlanilla = @cpla_IdPlanilla
              AND tcdi.cin_Activo = 1
        UNION ALL
        SELECT tcdi.cin_IdIngreso, 
               tcdi.cin_DescripcionIngreso, 
               0 AS 'checked'
        FROM Plani.tbCatalogoDeIngresos tcdi
        WHERE tcdi.cin_IdIngreso NOT IN
        (
            SELECT cin_IdIngreso
            FROM Plani.tbTipoPlanillaDetalleIngreso
            WHERE cpla_IdPlanilla = @cpla_IdPlanilla
                  AND cin_Activo = 1
        )
              AND tcdi.cin_Activo = 1
        ORDER BY 1;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_DecimoTercerMes_RPT]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_DecimoTercerMes_RPT]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
	 @FechaInicio datetime
)
AS
    BEGIN

	SELECT 
	dtm_IdDecimoTercerMes, 
	dtm_FechaPago, 
	dtm_UsuarioCrea, 
	dtm_FechaCrea, 
	dtm_UsuarioModifica, 
	dtm_FechaModifica, 
	emp_Id, dtm_Monto, 
	dtm_CodigoPago	
	FROM [Plani].[tbDecimoTercerMes]

WHERE   dtm_FechaPago = @FechaInicio

    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_EmpleadoComisiones_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_EmpleadoComisiones_Activar]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cc_Id INT,
		@cc_UsuarioModifica Int,
		@cc_FechaModifcia DateTime
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100); 
        BEGIN TRY
           UPDATE [Plani].[tbEmpleadoComisiones]
			SET [cc_Activo] = 1,
			[cc_UsuarioModifica] = @cc_UsuarioModifica,
			[cc_FechaModifica] =  @cc_FechaModifcia
			WHERE [cc_Id]       = @cc_Id

		   SELECT CAST(@cc_Id AS varchar) AS MensajeError
          COMMIT TRAN
          SET  @msj  = @cc_Id
		 SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_EmpleadoComisiones_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_EmpleadoComisiones_Inactivar]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cc_Id INT,
		@cc_UsuarioModifica Int,
		@cc_FechaModifcia DateTime
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100); 
        BEGIN TRY
           UPDATE [Plani].[tbEmpleadoComisiones]
			SET [cc_Activo] = 0,
			[cc_UsuarioModifica] = @cc_UsuarioModifica,
			[cc_FechaModifica] =  @cc_FechaModifcia
			WHERE [cc_Id]       = @cc_Id

		   SELECT CAST(@cc_Id AS varchar) AS MensajeError
          COMMIT TRAN
          SET  @msj  = @cc_Id
		 SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_EmpleadoComisiones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_EmpleadoComisiones_Insert]
(
-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
    @emp_Id INT,
	@cin_IdIngreso INT ,
	@cc_FechaRegistro DATETIME,
	@cc_Pagado BIT ,
	@cc_UsuarioCrea INT ,
	@cc_FechaCrea DATETIME,
	@cc_PorcentajeComision Decimal(16, 4),
	@cc_TotalVenta  Decimal(16, 4) 
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
            DECLARE @IdEmpleadoComisiones INT
            SET @IdEmpleadoComisiones = (SELECT ISNULL(MAX(cc_Id) + 1, 1) FROM[Plani].[tbEmpleadoComisiones]);
                INSERT INTO [Plani].[tbEmpleadoComisiones]
			(
				cc_Id, 
				emp_Id, 
				cin_IdIngreso, 
				cc_FechaRegistro, 
				cc_Pagado, 
				cc_UsuarioCrea, 
				cc_FechaCrea,
				cc_Activo,
				cc_PorcentajeComision,
				cc_TotalVenta
			)
			VALUES
				(
			    @IdEmpleadoComisiones,
				@emp_Id,
				@cin_IdIngreso,
				@cc_FechaRegistro,
				@cc_Pagado,
				@cc_UsuarioCrea,
				@cc_FechaCrea,
				1,
				@cc_PorcentajeComision,
				@cc_TotalVenta
		    )
                COMMIT TRAN
                SET @msj = @IdEmpleadoComisiones
			SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_EmpleadoComisiones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_EmpleadoComisiones_Update]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cc_Id INT,
		@EMP_Id int,
        @cc_UsuarioModifica  INT,
        @cc_FechaModifica DATETIME,
		@cc_PorcentajeComision Decimal(16, 4),
	    @cc_TotalVenta  Decimal(16, 4) 
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100); 
        BEGIN TRY
             UPDATE   [Plani].[tbEmpleadoComisiones]
			SET [cc_Id]               =@cc_Id,
			    [emp_Id]              =@EMP_Id,
				[cc_UsuarioModifica]  =@cc_UsuarioModifica,
				[cc_FechaModifica]    =@cc_FechaModifica,
                [cc_PorcentajeComision] = @cc_PorcentajeComision,
				[cc_TotalVenta]    =  @cc_TotalVenta
				WHERE [cc_Id]       = @cc_Id
		   SELECT CAST(@cc_Id AS varchar) AS MensajeError
          COMMIT TRAN
          SET  @msj  = @cc_Id
		 SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_EmpleadosPorAreas_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_EmpleadosPorAreas_Select]
AS
    BEGIN

	/*
	Estructura deseada en este JSON:

{
  "results": [
    { 
      "text": "Group 1", 
      "children" : [
        {
            "id": 1,
            "text": "Option 1.1"
        },
        {
            "id": 2,
            "text": "Option 1.2"
        }
      ]
    },
    { 
      "text": "Group 2", 
      "children" : [
        {
            "id": 3,
            "text": "Option 2.1"
        },
        {
            "id": 4,
            "text": "Option 2.2"
        }
      ]
    }
  ],
  "pagination": {
    "more": true
  }
}
	
	Para mas informacion visitar este enlace: https://select2.org/data-sources/formats
	
	*/

        SELECT
        (
            SELECT
            (
                SELECT ta.area_Descripcion AS 'text', 
                (
                    SELECT te.emp_Id AS 'id', 
                           (tp.per_Nombres + ' ' + tp.per_Apellidos) AS 'text'
                    FROM rrhh.tbEmpleados te
                         INNER JOIN rrhh.tbPersonas tp ON te.per_Id = tp.per_Id
						 INNER JOIN rrhh.tbDepartamentos td ON td.depto_Id = te.depto_Id
						 INNER JOIN rrhh.tbCargos tc ON tc.car_Id = te.car_Id
						 INNER JOIN rrhh.tbSueldos ts ON ts.emp_Id = te.emp_Id
						 INNER JOIN rrhh.tbTipoMonedas ttp ON ttp.tmon_Id = ts.tmon_Id
                    WHERE te.emp_Estado = 1
						  AND tp.per_Estado = 1
						  AND td.depto_Estado = 1
						  AND tc.car_Estado = 1
						  AND ts.sue_Estado = 1
						  AND ttp.tmon_Estado = 1
                          AND ta.area_Id = te.area_Id
                    ORDER BY tp.per_Nombres FOR JSON AUTO
                ) AS 'children'
                FROM rrhh.tbAreas ta
                WHERE ta.area_Estado = 1
                      AND TA.area_Id IN
                (
                    SELECT DISTINCT 
                           te.area_Id
                    FROM rrhh.tbEmpleados te
                         INNER JOIN rrhh.tbPersonas tp ON te.per_Id = tp.per_Id
						 INNER JOIN rrhh.tbDepartamentos td ON td.depto_Id = te.depto_Id
						 INNER JOIN rrhh.tbCargos tc ON tc.car_Id = te.car_Id
						 INNER JOIN rrhh.tbSueldos ts ON ts.emp_Id = te.emp_Id
						 INNER JOIN rrhh.tbTipoMonedas ttp ON ttp.tmon_Id = ts.tmon_Id
                    WHERE te.emp_Estado = 1
						  AND tp.per_Estado = 1
						  AND td.depto_Estado = 1
						  AND tc.car_Estado = 1
						  AND ts.sue_Estado = 1
						  AND ttp.tmon_Estado = 1
                ) FOR JSON AUTO
            ) AS 'results', 
            (
                SELECT JSON_QUERY(
                (
                    SELECT 'true' AS 'more' FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
                ))
            ) AS 'pagination' FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        ) AS 'json';
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAcumuladosISR_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAcumuladosISR_Activar] 
(
@aisr_Id int,
@aisr_UsuarioModifica int,
@aisr_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbAcumuladosISR]
               SET 			    
			   [aisr_Activo] = 1,
			   [aisr_UsuarioModifica] = @aisr_UsuarioModifica,
			   [aisr_FechaModifica] = @aisr_FechaModifica
			   WHERE [aisr_Id] = @aisr_Id
                COMMIT TRAN
                SET @msj = @aisr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAcumuladosISR_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAcumuladosISR_Inactivar] 
(
@aisr_Id int,
@aisr_UsuarioModifica int,
@aisr_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbAcumuladosISR]
               SET 			    
			   [aisr_Activo] = 0,
			   [aisr_UsuarioModifica] = @aisr_UsuarioModifica,
			   [aisr_FechaModifica] = @aisr_FechaModifica
			   WHERE [aisr_Id] = @aisr_Id
                COMMIT TRAN
                SET @msj = @aisr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAcumuladosISR_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAcumuladosISR_Insert]
(
@aisr_Descripcion nvarchar(100),
@aisr_Monto decimal (16,4),
@aisr_UsuarioCrea int,
@aisr_FechaCrea DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
            DECLARE @aisr_Id INT
            SET @aisr_Id = (SELECT ISNULL(MAX(aisr_Id) + 1, 1) FROM [Plani].[tbAcumuladosISR]);
                INSERT INTO Plani.tbAcumuladosISR
                (
                    [aisr_Id], [aisr_Descripcion], [aisr_Monto], [aisr_UsuarioCrea], [aisr_FechaCrea],[aisr_Activo]
                )
                VALUES
                (
                    @aisr_Id,
                    @aisr_Descripcion,
                    @aisr_Monto,
                    @aisr_UsuarioCrea,
                    @aisr_FechaCrea,
					1				
                )
                COMMIT TRAN
                SET @msj = @aisr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAcumuladosISR_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Plani].[UDP_Plani_tbAcumuladosISR_Update] 
(
@aisr_Id int,
@aisr_Descripcion nvarchar(100),
@aisr_Monto decimal (16,2),
@aisr_UsuarioModifica int,
@aisr_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbAcumuladosISR]
               SET 			    
			   [aisr_Descripcion]		= @aisr_Descripcion, 
			   [aisr_Monto]			= @aisr_Monto, 		
			   [aisr_UsuarioModifica]				= @aisr_UsuarioModifica, 
			   [aisr_FechaModifica]				= @aisr_FechaModifica
			   WHERE [aisr_Id]	= @aisr_Id
                COMMIT TRAN
                SET @msj = @aisr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAdelantoSueldo_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbAdelantoSueldo_Activar]
(
	@adsu_IdAdelantoSueldo int,	
	@adsu_UsuarioModifica int,
	@adsu_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbAdelantoSueldo]
				SET
				adsu_Activo = 1,
				adsu_UsuarioModifica = @adsu_UsuarioModifica,
				adsu_FechaModifica = @adsu_FechaModifica
			WHERE adsu_IdAdelantoSueldo = @adsu_IdAdelantoSueldo

			COMMIT TRAN
			SET @msj = @adsu_IdAdelantoSueldo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAdelantoSueldo_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbAdelantoSueldo_Inactivar]
(
	@adsu_IdAdelantoSueldo int,	
	@adsu_UsuarioModifica int,
	@adsu_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbAdelantoSueldo]
				SET
				adsu_Activo = 0,
				adsu_UsuarioModifica = @adsu_UsuarioModifica,
				adsu_FechaModifica = @adsu_FechaModifica
			WHERE adsu_IdAdelantoSueldo = @adsu_IdAdelantoSueldo

			COMMIT TRAN
			SET @msj = @adsu_IdAdelantoSueldo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAdelantoSueldo_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbAdelantoSueldo_Insert]
(
	@emp_Id					int,
	@adsu_FechaAdelanto		datetime,
	@adsu_RazonAdelanto		nvarchar(50),
	@adsu_Monto				decimal(16,2),
	@adsu_UsuarioCrea		int,
	@adsu_FechaCrea			datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			DECLARE @adsu_IdAdelantoSueldo INT
            SET @adsu_IdAdelantoSueldo = (SELECT ISNULL(MAX(adsu_IdAdelantoSueldo) + 1, 1) FROM [Plani].[tbAdelantoSueldo])

			INSERT INTO [Plani].[tbAdelantoSueldo]
			(adsu_IdAdelantoSueldo, emp_Id, adsu_FechaAdelanto, adsu_RazonAdelanto, adsu_Monto, adsu_Deducido, adsu_UsuarioCrea, adsu_FechaCrea, adsu_Activo)
			VALUES
			(
			@adsu_IdAdelantoSueldo,
			@emp_Id,	
			@adsu_FechaAdelanto,	
			@adsu_RazonAdelanto,
			@adsu_Monto,
			0,
			@adsu_UsuarioCrea,	
			@adsu_FechaCrea,
			1
			)

			COMMIT TRAN
			SET @msj = @adsu_IdAdelantoSueldo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAdelantoSueldo_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbAdelantoSueldo_Update]
(
	@adsu_IdAdelantoSueldo	int,
	@emp_Id					int,
	@adsu_RazonAdelanto		nvarchar(50),
	@adsu_Monto				decimal(16,2),
	@adsu_UsuarioModifica	int,
	@adsu_FechaModifica		datetime
)
AS
BEGIN
	 BEGIN TRAN
	 DECLARE @msj nvarchar(100)
		BEGIN TRY
			
			UPDATE [Plani].[tbAdelantoSueldo] 
				SET
				emp_Id					=		@emp_Id,
				adsu_RazonAdelanto		=		@adsu_RazonAdelanto,
				adsu_Monto				=		@adsu_Monto,
				adsu_UsuarioModifica	=		@adsu_UsuarioModifica,
				adsu_FechaModifica		=		@adsu_FechaModifica
			WHERE adsu_IdAdelantoSueldo = @adsu_IdAdelantoSueldo

			COMMIT TRAN
			SET @msj = @adsu_IdAdelantoSueldo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAFP_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAFP_Activar]
(
	@afp_Id INT,
	@afp_UsuarioModifica INT,
	@afp_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbAFP]
				SET afp_Activo           =  1,
					afp_UsuarioModifica  =  @afp_UsuarioModifica,
					afp_FechaModifica    =  @afp_FechaModifica
					WHERE afp_Id = @afp_Id
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAFP_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAFP_Inactivar]
(
	@afp_Id INT,
	@afp_UsuarioModifica INT,
	@afp_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbAFP]
				SET afp_Activo           =  0,
					afp_UsuarioModifica  =  @afp_UsuarioModifica,
					afp_FechaModifica    =  @afp_FechaModifica
					WHERE afp_Id = @afp_Id
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAFP_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbAFP_Insert]
(
	@afp_Descripcion		NVARCHAR(100),
	@afp_AporteMinimoLps	DECIMAL(16,4),
	@afp_InteresAporte		DECIMAL(16,4),
	@afp_InteresAnual		DECIMAL(16,4),
	@tde_IdTipoDedu			INT,
	@afp_UsuarioCrea		INT,
	@afp_FechaCrea			DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
				DECLARE @afp_Id INT
				SET @afp_Id = (SELECT ISNULL(MAX(afp_Id) + 1, 1) FROM [Plani].[tbAFP]);
                INSERT INTO [Plani].[tbAFP]
                (
					afp_Id,
					afp_Descripcion, 
					afp_AporteMinimoLps, 
					afp_InteresAporte, 
					afp_InteresAnual, 
					tde_IdTipoDedu, 
					afp_UsuarioCrea, 
					afp_FechaCrea, 
					afp_Activo
                )
                VALUES
                (	
					@afp_Id,
					@afp_Descripcion,	
					@afp_AporteMinimoLps,
					@afp_InteresAporte,	
					@afp_InteresAnual,	
					@tde_IdTipoDedu,	
					@afp_UsuarioCrea,	
					@afp_FechaCrea,
					1
                )
                COMMIT TRAN
                SET @msj = @afp_Id
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbAFP_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbAFP_Update]
(
	@afp_Id					INT,
	@afp_Descripcion		NVARCHAR(100),
	@afp_AporteMinimoLps	DECIMAL(16,4),
	@afp_InteresAporte		DECIMAL(16,4),
	@afp_InteresAnual		DECIMAL(16,4),
	@tde_IdTipoDedu			INT,
	@afp_UsuarioModifica	INT,
	@afp_FechaModifica		DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbAFP]
				SET afp_Descripcion      =  @afp_Descripcion,	   
					afp_AporteMinimoLps  =  @afp_AporteMinimoLps,   
					afp_InteresAporte 	 =  @afp_InteresAporte,	  
					afp_InteresAnual 	 =  @afp_InteresAnual,	  
					tde_IdTipoDedu 		 =  @tde_IdTipoDedu,	  
					afp_UsuarioModifica  =  @afp_UsuarioModifica,	  
					afp_FechaModifica	 =  @afp_FechaModifica
				WHERE afp_Id = @afp_Id
                COMMIT TRAN
                SET @msj = @afp_Id
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Activar]
    (
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cde_IdDeduccion INT,
		@cde_UsuarioModifica INT,
		@cde_FechaModifica DATETIME		
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
            UPDATE [Plani].[tbCatalogoDeDeducciones]
			SET
			cde_Activo = 1,
			cde_UsuarioModifica = @cde_UsuarioModifica,
			cde_FechaModifica = @cde_FechaModifica
			WHERE cde_IdDeducciones = @cde_IdDeduccion
          COMMIT TRAN
          SET  @msj  = @cde_IdDeduccion
          SELECT @msj AS MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Inactivar]
    (
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cde_IdDeduccion INT,
		@cde_UsuarioModifica INT,
		@cde_FechaModifica DATETIME		
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
            UPDATE [Plani].[tbCatalogoDeDeducciones]
			SET
			cde_Activo = 0,
			cde_UsuarioModifica = @cde_UsuarioModifica,
			cde_FechaModifica = @cde_FechaModifica
			WHERE cde_IdDeducciones = @cde_IdDeduccion
          COMMIT TRAN
          SET  @msj  = @cde_IdDeduccion
          SELECT @msj AS MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Insert]
    (
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cde_DescripcionDedu NVARCHAR(50),
		@tde_IdTipoDedu INT,
		@cde_PorcentajeColaborador DECIMAL(16,2),
		@cde_PorcentajeEmpresa DECIMAL(16,2),
		@cde_UsuarioCrea INT,
		@cde_FechaCrea DATETIME
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
			DECLARE @IdCatalogoDeDedu INT
            SET @IdCatalogoDeDedu = (SELECT ISNULL(MAX(cde_IdDeducciones) + 1, 1) FROM [Plani].[tbCatalogoDeDeducciones]);
            INSERT INTO [Plani].[tbCatalogoDeDeducciones]
            (
                cde_IdDeducciones, cde_DescripcionDeduccion, tde_IdTipoDedu, cde_PorcentajeColaborador, 
				cde_PorcentajeEmpresa, cde_UsuarioCrea, cde_FechaCrea, cde_Activo
            )
            VALUES
            (
                (SELECT ISNULL(MAX(cde_IdDeducciones) + 1, 1) FROM [Plani].[tbCatalogoDeDeducciones]),
                @cde_DescripcionDedu,
				@tde_IdTipoDedu,
				@cde_PorcentajeColaborador,
				@cde_PorcentajeEmpresa,
				@cde_UsuarioCrea,
				@cde_FechaCrea,
				1
             )
          COMMIT TRAN
          SET  @msj  = @IdCatalogoDeDedu
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
    SELECT @msj AS	MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeDeducciones_Update]
    (
        -- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
        @cde_IdDeduccion INT,
		@cde_DescripcionDedu NVARCHAR(50),
		@tde_IdTipoDedu INT,
		@cde_PorcentajeColaborador DECIMAL(16,2),
		@cde_PorcentajeEmpresa DECIMAL(16,2),
		@cde_UsuarioModifica INT,
		@cde_FechaModifica DATETIME
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
            UPDATE [Plani].[tbCatalogoDeDeducciones]
			SET
			cde_DescripcionDeduccion = @cde_DescripcionDedu,
			tde_IdTipoDedu = @tde_IdTipoDedu,
			cde_PorcentajeColaborador = @cde_PorcentajeColaborador,
			cde_PorcentajeEmpresa = @cde_PorcentajeEmpresa,
			cde_UsuarioModifica = @cde_UsuarioModifica,
			cde_FechaModifica = @cde_FechaModifica
			WHERE cde_IdDeducciones = @cde_IdDeduccion     
          COMMIT TRAN
          SET  @msj  = @cde_IdDeduccion
          SELECT @msj AS MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeIngresos_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeIngresos_Activar]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cin_IdIngreso          INT,
		@cin_UsuarioModifica    INT,
		@cin_FechaModifica      DATETIME
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
            UPDATE [Plani].[tbCatalogoDeIngresos]
			SET 
             [cin_Activo] = 1,
             [cin_UsuarioModifica]= @cin_UsuarioModifica,
             [cin_FechaModifica]= @cin_FechaModifica

			WHERE cin_IdIngreso  = @cin_IdIngreso
          COMMIT TRAN
          SET  @msj  = @cin_IdIngreso
		   SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeIngresos_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeIngresos_Inactivar]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cin_IdIngreso          INT,
		@cin_UsuarioModifica    INT,
		@cin_FechaModifica      DATETIME
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
            UPDATE [Plani].[tbCatalogoDeIngresos]
			SET 
             [cin_Activo] = 0,
             [cin_UsuarioModifica]= @cin_UsuarioModifica,
             [cin_FechaModifica]= @cin_FechaModifica

			WHERE cin_IdIngreso  = @cin_IdIngreso
          COMMIT TRAN
          SET  @msj  = @cin_IdIngreso
		   SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeIngresos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeIngresos_Insert]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cin_DescripcionIngreso NVARCHAR(50), 
		@cin_UsuarioCrea        INT,
		@cin_FechaCrea          DATETIME
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
		DECLARE @cin_IdIngreso INT
            SET @cin_IdIngreso = (SELECT ISNULL(MAX(cin_IdIngreso) + 1, 1) 
			FROM [Plani].[tbCatalogoDeIngresos]);
            INSERT INTO [Plani].[tbCatalogoDeIngresos]
			(
				cin_IdIngreso, 
				cin_DescripcionIngreso, 
				cin_UsuarioCrea, 
				cin_FechaCrea,
				cin_Activo
			)
			VALUES
			(
			    @cin_IdIngreso,
				@cin_DescripcionIngreso,
				@cin_UsuarioCrea       ,
			    @cin_FechaCrea  ,
				1         
			)	
          COMMIT TRAN
          SET  @msj  = @cin_IdIngreso
		   SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDeIngresos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDeIngresos_Update]
	(
		-- LOS PARAMETROS QUE RECIBIRÁ EL PROCEDIMIENTO ALMACENADO
		@cin_IdIngreso          INT,
		@cin_DescripcionIngreso NVARCHAR(50), 
		@cin_UsuarioModifica    INT,
		@cin_FechaModifica      DATETIME
	)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
		
            UPDATE [Plani].[tbCatalogoDeIngresos]
			SET 
			    cin_DescripcionIngreso = @cin_DescripcionIngreso ,
			    cin_UsuarioModifica	   = @cin_UsuarioModifica,
			    cin_FechaModifica	   = @cin_FechaModifica
			WHERE cin_IdIngreso          = @cin_IdIngreso
		   
          COMMIT TRAN
          SET  @msj  = @cin_IdIngreso
		   SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDePlanillas_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDePlanillas_Inactivar]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla INT
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            -- Declarar la variable @msj, es la que retornara el procedimiento como MensajeError
            DECLARE @msj NVARCHAR(100);

            -- Inicio del Update de la tabla [Plani].[tbCatalogoDePlanillas]
            UPDATE [Plani].[tbCatalogoDePlanillas]
              SET 
                  [cpla_Activo] = 0
            WHERE cpla_IdPlanilla = @cpla_IdPlanilla;

            -- @cpla_IdPlanilla, era el Id a retornar por el procedimiento almacenado
            SET @msj = @cpla_IdPlanilla;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SET @msj = '-1' + ERROR_MESSAGE();
        END CATCH;
        SELECT @msj AS MensajeError;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDePlanillas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDePlanillas_Insert]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_DescripcionPlanilla NVARCHAR(50)
   , @cpla_FrecuenciaEnDias    INT
   , @cpla_UsuarioCrea         INT
   , @cpla_FechaCrea           DATETIME
   , @cpla_RecibeComision       BIT
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            -- Declarar la variable @cpla_IdPlanilla, es la que retornara el procedimiento como MensajeError
            DECLARE @cpla_IdPlanilla INT;
            SET @cpla_IdPlanilla =
            (
                SELECT ISNULL(MAX([cpla_IdPlanilla]) + 1, 1)
                FROM [Plani].[tbCatalogoDePlanillas]
            );

            -- Inicio del Insert de la tabla [Plani].[tbCatalogoDePlanillas]
            INSERT INTO [Plani].[tbCatalogoDePlanillas]
            (
			[cpla_IdPlanilla], 
             [cpla_DescripcionPlanilla], 
             [cpla_FrecuenciaEnDias], 
             [cpla_UsuarioCrea], 
             [cpla_FechaCrea], 
             [cpla_Activo],
			 [cpla_RecibeComision]
            )
            VALUES
            (
			 @cpla_IdPlanilla, 
             @cpla_DescripcionPlanilla, 
             @cpla_FrecuenciaEnDias, 
             @cpla_UsuarioCrea, 
             @cpla_FechaCrea, 
             1,
			 @cpla_RecibeComision
            );

            -- @cpla_IdPlanilla, era el Id a retornar por el procedimiento almacenado
            SELECT CAST(@cpla_IdPlanilla AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbCatalogoDePlanillas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbCatalogoDePlanillas_Update]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla          INT
   , @cpla_DescripcionPlanilla NVARCHAR(50)
   , @cpla_FrecuenciaEnDias    INT
   , @cpla_UsuarioModifica     INT
   , @cpla_FechaModifica       DATETIME
   , @cpla_RecibeComision       BIT
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            -- Declarar la variable @msj, es la que retornara el procedimiento como MensajeError
            DECLARE @msj NVARCHAR(100);

            -- Inicio del Update de la tabla [Plani].[tbCatalogoDePlanillas]
            UPDATE [Plani].[tbCatalogoDePlanillas]
              SET 
                  [cpla_DescripcionPlanilla] = @cpla_DescripcionPlanilla, 
                  [cpla_FrecuenciaEnDias] = @cpla_FrecuenciaEnDias, 
                  [cpla_UsuarioModifica] = @cpla_UsuarioModifica, 
                  [cpla_FechaModifica] = @cpla_FechaModifica,
				  [cpla_RecibeComision] = @cpla_RecibeComision
            WHERE cpla_IdPlanilla = @cpla_IdPlanilla;

            -- @cpla_IdPlanilla, era el Id a retornar por el procedimiento almacenado
            SELECT CAST(@cpla_IdPlanilla AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SET @msj = '-1' + ERROR_MESSAGE();
            SELECT @msj AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDecimoCuartoMes_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDecimoCuartoMes_Insert]
(
	@emp_Id int,
	@dtm_DecimoCuartoMonto decimal(18,3)
)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY

            DECLARE @dtm_IdDecimoCuartoMes INT
            SET @dtm_IdDecimoCuartoMes = (SELECT ISNULL(MAX(@dtm_IdDecimoCuartoMes) + 1, 1) FROM Plani.tbDecimoCuartoMes);

			INSERT INTO [Plani].[tbDecimoCuartoMes]
			           ([dcm_FechaPago]
			           ,[dcm_UsuarioCrea]
			           ,[dcm_FechaCrea]			           
			           ,[emp_Id]
			           ,[dcm_Monto]
					   ,[dcm_CodigoPago]
					   )
			     VALUES
			           (GETDATE(), 1, GETDATE(), @emp_Id, @dtm_DecimoCuartoMonto, CONCAT(YEAR(GETDATE()), @emp_Id))

          COMMIT TRAN
          SET  @msj  = @dtm_IdDecimoCuartoMes
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDecimoTercerMes_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDecimoTercerMes_Insert]
    (
		@emp_Id int,
		@dtm_DecimoTercer decimal(18,3)
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY

            DECLARE @dtm_IdDecimoTercerMes INT
            SET @dtm_IdDecimoTercerMes = (SELECT ISNULL(MAX(@dtm_IdDecimoTercerMes) + 1, 1) FROM Plani.tbDecimoTercerMes);

			INSERT INTO [Plani].[tbDecimoTercerMes]
			           ([dtm_FechaPago]
			           ,[dtm_UsuarioCrea]
			           ,[dtm_FechaCrea]			           
			           ,[emp_Id]
			           ,[dtm_Monto]
					   ,[dtm_CodigoPago]
					   )
			     VALUES
			           (GETDATE(),1,GETDATE(),@emp_Id,@dtm_DecimoTercer,CONCAT(YEAR(GETDATE()),@emp_Id))


          COMMIT TRAN
          SET  @msj  = @dtm_IdDecimoTercerMes
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionAFP_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionAFP_Activar]
(
	@dafp_Id INT,
	@dafp_UsuarioModifica INT,
	@dafp_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionAFP]
				SET dafp_Activo           =  1,
					dafp_UsuarioModifica  =  @dafp_UsuarioModifica,
					dafp_FechaModifica    =  @dafp_FechaModifica
					WHERE dafp_Id = @dafp_Id
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionAFP_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionAFP_Inactivar]
(
	@dafp_Id INT,
	@dafp_UsuarioModifica INT,
	@dafp_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionAFP]
				SET dafp_Activo           =  0,
					dafp_UsuarioModifica  =  @dafp_UsuarioModifica,
					dafp_FechaModifica    =  @dafp_FechaModifica
					WHERE dafp_Id = @dafp_Id
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionAFP_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionAFP_Insert]
(
	@dafp_AporteMinimoLps	DECIMAL(16,4),
	@afp_Id					INT,
	@emp_Id					INT,
	@dafp_UsuarioCrea		INT,
	@dafp_FechaCrea			DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
				DECLARE @dafp_Id INT
				SET @dafp_Id = (SELECT ISNULL(MAX(dafp_Id) + 1, 1) FROM [Plani].[tbDeduccionAFP]);
                INSERT INTO [Plani].[tbDeduccionAFP]
                (
					dafp_Id,
					dafp_AporteLps, 
					afp_Id,
					emp_Id,
					dafp_UsuarioCrea, 
					dafp_FechaCrea, 
					dafp_Activo
                )
                VALUES
                (	
					@dafp_Id,
					@dafp_AporteMinimoLps,
					@afp_Id,					
					@emp_Id,	
					@dafp_UsuarioCrea,	
					@dafp_FechaCrea,					
					1
                )
                COMMIT TRAN
                SET @msj = @dafp_Id
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionAFP_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionAFP_Update]
(
	@dafp_Id                INT,
	@dafp_AporteLps	        DECIMAL(16,4),
	@afp_Id					INT,	
	@emp_Id					INT,
	@dafp_UsuarioModifica	INT,
	@dafp_FechaModifica 	DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionAFP]
				SET dafp_AporteLps		  =  @dafp_AporteLps,  
					afp_Id				  =  @afp_Id,					  	
					emp_Id				  =  @emp_Id,				
					dafp_UsuarioModifica  =  @dafp_UsuarioModifica,	  
					dafp_FechaModifica    =  @dafp_FechaModifica 
				WHERE dafp_Id = @dafp_Id
                COMMIT TRAN
                SET @msj = @dafp_Id
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Activar]
(
	@dex_IdDeduccionesExtra INT,
	@dex_UsuarioModifica INT,
	@dex_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionesExtraordinarias]
				SET dex_Activo = 1,
					dex_UsuarioModifica = @dex_UsuarioModifica,
					dex_FechaModifica = @dex_FechaModifica
					WHERE dex_IdDeduccionesExtra = @dex_IdDeduccionesExtra
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Inactivar]
(
	@dex_IdDeduccionesExtra INT,
	@dex_UsuarioModifica INT,
	@dex_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionesExtraordinarias]
				SET dex_Activo = 0,
					dex_UsuarioModifica = @dex_UsuarioModifica,
					dex_FechaModifica = @dex_FechaModifica
					WHERE dex_IdDeduccionesExtra = @dex_IdDeduccionesExtra
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Insert]
(
	@eqem_Id INT,
	@dex_MontoInicial DECIMAL(10,2),
	@dex_MontoRestante DECIMAL(10,2),
	@dex_ObservacionesComentarios NVARCHAR(100),
	@cde_IdDeducciones INT,
	@dex_Cuota DECIMAL(16,2),
	@dex_UsuarioCrea INT,
	@dex_FechaCrea DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
				DECLARE @dex_IdDeduccionesExtra INT
				SET @dex_IdDeduccionesExtra = (SELECT ISNULL(MAX(dex_IdDeduccionesExtra) + 1, 1) FROM [Plani].[tbDeduccionesExtraordinarias]);
                INSERT INTO [Plani].[tbDeduccionesExtraordinarias]
                (
                    dex_IdDeduccionesExtra, 
					eqem_Id, 
					dex_MontoInicial, 
					dex_MontoRestante, 
					dex_ObservacionesComentarios, 
					cde_IdDeducciones, 
					dex_Cuota, 
					dex_UsuarioCrea, 
					dex_FechaCrea, 
					dex_Activo
                )
                VALUES
                (	
					@dex_IdDeduccionesExtra,
                    @eqem_Id,
					@dex_MontoInicial,
					@dex_MontoRestante,
					@dex_ObservacionesComentarios,
					@cde_IdDeducciones,
					@dex_Cuota,
					@dex_UsuarioCrea,
					@dex_FechaCrea,
					1
                )
                COMMIT TRAN
                SET @msj = @dex_IdDeduccionesExtra
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbDeduccionesExtraordinarias_Update]
(
	@dex_IdDeduccionesExtra INT,
	@eqem_Id INT,
	@dex_MontoInicial DECIMAL(10,2),
	@dex_MontoRestante DECIMAL(10,2),
	@dex_ObservacionesComentarios NVARCHAR(100),
	@cde_IdDeducciones INT,
	@dex_Cuota DECIMAL(16,2),
	@dex_UsuarioModifica INT,
	@dex_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
                UPDATE [Plani].[tbDeduccionesExtraordinarias]
				SET eqem_Id = @eqem_Id, 
					dex_MontoInicial = @dex_MontoInicial,
					dex_MontoRestante = @dex_MontoRestante,
					dex_ObservacionesComentarios = @dex_ObservacionesComentarios,
					cde_IdDeducciones = @cde_IdDeducciones,
					dex_Cuota = @dex_Cuota,
					dex_UsuarioModifica = @dex_UsuarioModifica,
					dex_FechaModifica = @dex_FechaModifica
					WHERE dex_IdDeduccionesExtra = @dex_IdDeduccionesExtra
					SELECT CAST(@dex_IdDeduccionesExtra AS VARCHAR) AS MensajeError
                COMMIT TRAN
                SET @msj = '1'
            END TRY
            BEGIN CATCH
				ROLLBACK TRAN
				SET @msj = '-1' + ERROR_MESSAGE()
            END CATCH
				SELECT @msj AS MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbEmpleadoBonos_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbEmpleadoBonos_Activar]
(
	@cb_Id int,	
	@cb_UsuarioModifica int,
	@cb_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbEmpleadoBonos] 
				SET
				cb_Activo = 1,
				cb_UsuarioModifica = @cb_UsuarioModifica,
				cb_FechaModifica = @cb_FechaModifica
			WHERE cb_Id = @cb_Id

			COMMIT TRAN
			SET @msj = @cb_Id
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbEmpleadoBonos_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbEmpleadoBonos_Inactivar]
(
	@cb_Id int,	
	@cb_UsuarioModifica int,
	@cb_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbEmpleadoBonos] 
				SET
				cb_Activo = 0,
				cb_UsuarioModifica = @cb_UsuarioModifica,
				cb_FechaModifica = @cb_FechaModifica
			WHERE cb_Id = @cb_Id

			COMMIT TRAN
			SET @msj = @cb_Id
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbEmpleadoBonos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbEmpleadoBonos_Insert]
(
	@emp_Id int,
	@cin_IdIngreso int,
	@cb_Monto decimal(10,2),
	@cb_FechaRegistro datetime,
	@cb_Pagado bit,
	@cb_UsuarioCrea int,
	@cb_FechaCrea datetime
	
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			DECLARE @cb_Id INT
            SET @cb_Id = (SELECT ISNULL(MAX(cb_Id) + 1, 1) FROM [Plani].[tbEmpleadoBonos])

			INSERT INTO [Plani].[tbEmpleadoBonos]
			(cb_Id, emp_Id, cin_IdIngreso, cb_Monto, cb_FechaRegistro, cb_Pagado, cb_UsuarioCrea, cb_FechaCrea, cb_Activo)
			VALUES
			(
			@cb_Id,
			@emp_Id,
			@cin_IdIngreso,
			@cb_Monto,
			@cb_FechaRegistro,
			@cb_Pagado,
			@cb_UsuarioCrea,
			@cb_FechaCrea,
			1
			)

			COMMIT TRAN
			SET @msj = @cb_Id
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbEmpleadoBonos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbEmpleadoBonos_Update]
(
	@cb_Id int,
	@emp_Id int, --depende si se va a dejar la opcion de modificar el empleado que recibe el bono
	@cin_IdIngreso int,
	@cb_Monto decimal(10,2),
	@cb_FechaRegistro datetime,
	@cb_Pagado bit,
	@cb_UsuarioModifica int,
	@cb_FechaModifica datetime
)
AS
BEGIN
	 BEGIN TRAN
	 DECLARE @msj nvarchar(100)
		BEGIN TRY
			
			UPDATE [Plani].[tbEmpleadoBonos] 
				SET
				emp_Id				= @emp_Id,
				cin_IdIngreso		= @cin_IdIngreso,
				cb_Monto			= @cb_Monto,
				cb_FechaRegistro	= @cb_FechaRegistro,
				cb_Pagado			= @cb_Pagado,
				cb_UsuarioModifica	= @cb_UsuarioModifica,
				cb_FechaModifica	= @cb_FechaModifica
			WHERE cb_Id = @cb_Id

			COMMIT TRAN
			SET @msj = @cb_Id
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbFormaPago_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbFormaPago_Activar]
(
	@fpa_IdFormaPago int,	
	@fpa_UsuarioModifica int,
	@fpa_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbFormaPago]
				SET
				fpa_Activo = 1,
				fpa_UsuarioModifica = @fpa_UsuarioModifica,
				fpa_FechaModifica = @fpa_FechaModifica
			WHERE fpa_IdFormaPago = @fpa_IdFormaPago

			COMMIT TRAN
			SET @msj = @fpa_IdFormaPago
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbFormaPago_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbFormaPago_Inactivar]
(
	@fpa_IdFormaPago int,	
	@fpa_UsuarioModifica int,
	@fpa_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbFormaPago]
				SET
				fpa_Activo = 0,
				fpa_UsuarioModifica = @fpa_UsuarioModifica,
				fpa_FechaModifica = @fpa_FechaModifica
			WHERE fpa_IdFormaPago = @fpa_IdFormaPago

			COMMIT TRAN
			SET @msj = @fpa_IdFormaPago
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbFormaPago_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbFormaPago_Insert]
(
	@fpa_Descripcion nvarchar(50),
	@fpa_UsuarioCrea int,
	@fpa_FechaCrea   datetime
	
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			DECLARE @fpa_IdFormaPago INT
            SET @fpa_IdFormaPago = (SELECT ISNULL(MAX(fpa_IdFormaPago) + 1, 1) FROM [Plani].[tbFormaPago])

			INSERT INTO [Plani].[tbFormaPago]
			(fpa_IdFormaPago, fpa_Descripcion, fpa_UsuarioCrea, fpa_FechaCrea, fpa_Activo)
			VALUES
			(
			@fpa_IdFormaPago,
			@fpa_Descripcion,
			@fpa_UsuarioCrea,
			@fpa_FechaCrea,
			1
			)

			COMMIT TRAN
			SET @msj = @fpa_IdFormaPago
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbFormaPago_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbFormaPago_Update] 
(
	@fpa_IdFormaPago INT,
	@fpa_Descripcion nvarchar(50),
	@fpa_UsuarioModifica int,
	@fpa_FechaModifica  datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbFormaPago]
               SET 			    
			   fpa_Descripcion = @fpa_Descripcion, 
			   fpa_UsuarioModifica = @fpa_UsuarioModifica, 
			   fpa_FechaModifica = @fpa_FechaModifica
			   WHERE fpa_IdFormaPago = @fpa_IdFormaPago
                COMMIT TRAN
                SET @msj = @fpa_IdFormaPago
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END

GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbHistorialDePago_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbHistorialDePago_Insert]
(

@emp_Id int,
@hipa_SueldoNeto decimal(16,4),
@hipa_FechaInicio datetime,
@hipa_FechaFin datetime,
@hipa_FechaPago datetime,
@hipa_Anio int,
@hipa_Mes int,
@peri_IdPeriodo int,
@hipa_UsuarioCrea int,
@hipa_FechaCrea datetime,
@hipa_TotalISR decimal(16,4),
@hipa_ISRPendiente bit,
@hipa_AFP decimal(16,4)
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
            DECLARE @hipa_IdHistorialDePago INT
            SET @hipa_IdHistorialDePago = (SELECT ISNULL(MAX([hipa_IdHistorialDePago]) + 1, 1) FROM [Plani].[tbHistorialDePago]);
                INSERT INTO [Plani].[tbHistorialDePago]
                (
					[hipa_IdHistorialDePago],
					[emp_Id],
					[hipa_SueldoNeto],
					[hipa_FechaInicio],
					[hipa_FechaFin],
					[hipa_FechaPago],
					[hipa_Anio],
					[hipa_Mes],
					[peri_IdPeriodo],
					[hipa_UsuarioCrea],
					[hipa_FechaCrea],
					[hipa_TotalISR],
					[hipa_ISRPendiente],
					[hipa_AFP]

                )
                VALUES
                (
					@hipa_IdHistorialDePago,
                    @emp_Id,
					@hipa_SueldoNeto,
					@hipa_FechaInicio,
					@hipa_FechaFin,
					@hipa_FechaPago,
					@hipa_Anio,
					@hipa_Mes,
					@peri_IdPeriodo,
					@hipa_UsuarioCrea,
					@hipa_FechaCrea,
					@hipa_TotalISR,
					@hipa_ISRPendiente,
					@hipa_AFP			
                )
                COMMIT TRAN
                SET @msj = @hipa_IdHistorialDePago
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbInstitucionesFinancieras_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbInstitucionesFinancieras_Insert]
    (
        @insf_DescInstitucionFinanc NVARCHAR(50),
		@insf_Contacto NVARCHAR(50),
		@insf_Telefono NVARCHAR(50),
		@insf_Correo NVARCHAR(50),
		@insf_UsuarioCrea INT,
		@insf_FechaCrea DATETIME,
		@insf_Activo BIT)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
            DECLARE @insf_IdInstitucionFinanciera INT
            SET @insf_IdInstitucionFinanciera = (SELECT ISNULL(MAX(insf_IdInstitucionFinanciera) + 1, 1) FROM [Plani].[tbInstitucionesFinancieras]);
           INSERT INTO [Plani].[tbInstitucionesFinancieras]
           ([insf_IdInstitucionFinanciera],[insf_DescInstitucionFinanc],[insf_Contacto],[insf_Telefono],[insf_Correo],
		   [insf_UsuarioCrea],[insf_FechaCrea],[insf_Activo])
	    VALUES(
           @insf_IdInstitucionFinanciera,
		   @insf_DescInstitucionFinanc,
		   @insf_Contacto,
		   @insf_Telefono,
		   @insf_Correo ,
		   @insf_UsuarioCrea ,
		   @insf_FechaCrea ,
		   @insf_Activo)
          COMMIT TRAN
          SET  @msj  = @insf_IdInstitucionFinanciera
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
 


GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbInstitucionesFinancieras_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [Plani].[UDP_Plani_tbInstitucionesFinancieras_Update]
    (
	    @insf_IdInstitucionFinanciera INT,
        @insf_DescInstitucionFinanc NVARCHAR(50),
		@insf_Contacto NVARCHAR(50),
		@insf_Telefono NVARCHAR(50),
		@insf_Correo NVARCHAR(50),
		@insf_UsuarioModifica INT,
		@insf_FechaModifica DATETIME,
		@insf_Activo BIT)
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)
        BEGIN TRY
            --DECLARE @insf_IdInstitucionFinanciera INT
            --SET @insf_IdInstitucionFinanciera = (SELECT ISNULL(MAX(insf_IdInstitucionFinanciera) + 1, 1) FROM [Plani].[tbInstitucionesFinancieras]);
     		UPDATE [Plani].[tbInstitucionesFinancieras]
			   SET [insf_DescInstitucionFinanc] = @insf_DescInstitucionFinanc,
				   [insf_Contacto] = @insf_Contacto,
				   [insf_Telefono] = @insf_Telefono,
				   [insf_Correo] = @insf_Correo,
				   [insf_UsuarioModifica] = @insf_UsuarioModifica,
				   [insf_FechaModifica] = @insf_FechaModifica,
				   [insf_Activo] = @insf_Activo
			 WHERE  [insf_IdInstitucionFinanciera] = @insf_IdInstitucionFinanciera
			
          COMMIT TRAN
          SET  @msj  = @insf_IdInstitucionFinanciera
          SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbISR_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbISR_Activar] 
(
@isr_Id int,
@isr_UsuarioModifica int,
@isr_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbISR]
               SET 			    
			   isr_Activo = 1,
			   isr_UsuarioModifica = @isr_UsuarioModifica,
			   isr_FechaModifica = @isr_FechaModifica
			   WHERE isr_Id = @isr_Id
                COMMIT TRAN
                SET @msj = @isr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbISR_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbISR_Inactivar] 
(
@isr_Id int,
@isr_UsuarioModifica int,
@isr_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbISR]
               SET 			    
			   isr_Activo = 0,
			   isr_UsuarioModifica = @isr_UsuarioModifica,
			   isr_FechaModifica = @isr_FechaModifica
			   WHERE isr_Id = @isr_Id
                COMMIT TRAN
                SET @msj = @isr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbISR_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbISR_Insert]
(
@isr_RangoInicial decimal (16,4),
@isr_RangoFinal decimal (16,4),
@isr_Porcentaje decimal (16,4),
@tde_IdTipoDedu int,
@isr_UsuarioCrea int,
@isr_FechaCrea DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
            DECLARE @IdISR INT
            SET @IdISR = (SELECT ISNULL(MAX(isr_Id) + 1, 1) FROM [Plani].[tbISR]);
                INSERT INTO [Plani].[tbISR]
                (
                    isr_Id, isr_RangoInicial, isr_RangoFinal, isr_Porcentaje, tde_IdTipoDedu, isr_UsuarioCrea, isr_FechaCrea, isr_Activo
                )
                VALUES
                (
                    @IdISR,
                    @isr_RangoInicial,
                    @isr_RangoFinal,
                    @isr_Porcentaje,
                    @tde_IdTipoDedu,
                    @isr_UsuarioCrea,
                    @isr_FechaCrea,
					1
                )
                COMMIT TRAN
                SET @msj = @IdISR
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END

GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbISR_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Plani].[UDP_Plani_tbISR_Update] 
(
@isr_Id int,
@isr_RangoInicial decimal (10,2),
@isr_RangoFinal decimal (10,2),
@isr_Porcentaje decimal (10,2),
@tde_IdTipoDedu int,
@isr_UsuarioModifica int,
@isr_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbISR]
               SET 			    
			   isr_RangoInicial = @isr_RangoInicial, 
			   isr_RangoFinal = @isr_RangoFinal, 
			   isr_Porcentaje = @isr_Porcentaje, 
			   tde_IdTipoDedu = @tde_IdTipoDedu, 
			   isr_UsuarioModifica = @isr_UsuarioModifica, 
			   isr_FechaModifica = @isr_FechaModifica
			   WHERE isr_Id = @isr_Id
                COMMIT TRAN
                SET @msj = @isr_Id
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END

GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPeriodos_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbPeriodos_Activar]
(
	@peri_IdPeriodo int,	
	@peri_UsuarioModifica int,
	@peri_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbPeriodos]
				SET
				peri_Activo = 1,
				peri_UsuarioModifica = @peri_UsuarioModifica,
				peri_FechaModifica = @peri_FechaModifica
			WHERE peri_IdPeriodo = @peri_IdPeriodo

			COMMIT TRAN
			SET @msj = @peri_IdPeriodo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPeriodos_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbPeriodos_Inactivar]
(
	@peri_IdPeriodo int,	
	@peri_UsuarioModifica int,
	@peri_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbPeriodos]
				SET
				peri_Activo = 0,
				peri_UsuarioModifica = @peri_UsuarioModifica,
				peri_FechaModifica = @peri_FechaModifica
			WHERE peri_IdPeriodo = @peri_IdPeriodo

			COMMIT TRAN
			SET @msj = @peri_IdPeriodo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPeriodos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Plani].[UDP_Plani_tbPeriodos_Insert]
(
	@peri_DescripPeriodo nvarchar(50),
	@peri_UsuarioCrea int,
	@peri_FechaCrea   datetime
	
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			DECLARE @peri_IdPeriodo INT
            SET @peri_IdPeriodo = (SELECT ISNULL(MAX(peri_IdPeriodo) + 1, 1) FROM [Plani].[tbPeriodos])

			INSERT INTO [Plani].[tbPeriodos]
			(peri_IdPeriodo, peri_DescripPeriodo, peri_UsuarioCrea, peri_FechaCrea, peri_Activo)
			VALUES
			(
			@peri_IdPeriodo,
			@peri_DescripPeriodo,
			@peri_UsuarioCrea,
			@peri_FechaCrea,
			1
			)

			COMMIT TRAN
			SET @msj = @peri_IdPeriodo
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END


GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPeriodos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Plani].[UDP_Plani_tbPeriodos_Update] 
(
	@peri_IdPeriodo INT,
	@peri_DescripPeriodo nvarchar(50),
	@peri_UsuarioModifica int,
	@peri_FechaModifica   datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbPeriodos]
               SET 			    
			   peri_DescripPeriodo = @peri_DescripPeriodo, 
			   peri_UsuarioModifica = @peri_UsuarioModifica, 
			   peri_FechaModifica = @peri_FechaModifica
			   WHERE peri_IdPeriodo = @peri_IdPeriodo
                COMMIT TRAN
                SET @msj = @peri_IdPeriodo
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END



GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPreaviso_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbPreaviso_Activar]
(
	@prea_IdPreaviso int,	
	@prea_UsuarioModifica int,
	@prea_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbPreaviso]
				SET
				prea_Activo = 1,
				prea_UsuarioModifica = @prea_UsuarioModifica,
				prea_FechaModifica = @prea_FechaModifica
			WHERE prea_IdPreaviso = @prea_IdPreaviso

			COMMIT TRAN
			SET @msj = @prea_IdPreaviso
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPreaviso_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbPreaviso_Inactivar]
(
	@prea_IdPreaviso int,	
	@prea_UsuarioModifica int,
	@prea_FechaModifica datetime
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			UPDATE [Plani].[tbPreaviso]
				SET
				prea_Activo = 0,
				prea_UsuarioModifica = @prea_UsuarioModifica,
				prea_FechaModifica = @prea_FechaModifica
			WHERE prea_IdPreaviso = @prea_IdPreaviso

			COMMIT TRAN
			SET @msj = @prea_IdPreaviso
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPreaviso_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbPreaviso_Insert]
(
	@prea_RangoInicio int,
	@prea_RangoFin int,
	@prea_DiasPreaviso int,
	@prea_UsuarioCrea int,
	@prea_FechaCrea   datetime
	
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
		BEGIN TRY
			DECLARE @prea_IdPreaviso INT
            SET @prea_IdPreaviso = (SELECT ISNULL(MAX(prea_IdPreaviso) + 1, 1) FROM [Plani].[tbPreaviso])

			INSERT INTO [Plani].[tbPreaviso]
			(prea_IdPreaviso, prea_RangoInicioMeses, prea_RangoFinMeses, prea_DiasPreaviso, prea_UsuarioCrea, prea_FechaCrea, prea_Activo)
			VALUES
			(
			@prea_IdPreaviso,
			@prea_RangoInicio,
			@prea_RangoFin,
			@prea_DiasPreaviso,
			@prea_UsuarioCrea,
			@prea_FechaCrea,
			1
			)

			COMMIT TRAN
			SET @msj = @prea_IdPreaviso
			SELECT @msj AS MensajeError
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
				SELECT '-1' + ERROR_MESSAGE() AS MensajeError
		END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbPreaviso_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Plani].[UDP_Plani_tbPreaviso_Update]
(
	@prea_IdPreaviso int,
	@prea_RangoInicio int,
	@prea_RangoFin int,
	@prea_DiasPreaviso int,
	@prea_UsuarioModifica int,
	@prea_FechaModifica   datetime
	
)
AS
BEGIN
	BEGIN TRAN
	DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbPreaviso]
               SET 			    
			   prea_RangoInicioMeses = @prea_RangoInicio, 
			   prea_RangoFinMeses = @prea_RangoFin, 
			   prea_DiasPreaviso = @prea_DiasPreaviso, 
			   prea_UsuarioModifica = @prea_UsuarioModifica, 
			   prea_FechaModifica = @prea_FechaModifica
			   WHERE prea_IdPreaviso = @prea_IdPreaviso
                COMMIT TRAN
                SET @msj = @prea_IdPreaviso
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTechosDeducciones_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTechosDeducciones_Activar] 
(
@tddu_IdTechosDeducciones int,
@tddu_UsuarioModifica int,
@tddu_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbTechosDeducciones]
               SET 			    
			   tddu_Activo = 1,
			   tddu_UsuarioModifica = @tddu_UsuarioModifica,
			   tddu_FechaModifica = @tddu_FechaModifica
			   WHERE tddu_IdTechosDeducciones = @tddu_IdTechosDeducciones
                COMMIT TRAN
                SET @msj = @tddu_IdTechosDeducciones
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTechosDeducciones_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTechosDeducciones_Inactivar] 
(
@tddu_IdTechosDeducciones int,
@tddu_UsuarioModifica int,
@tddu_FechaModifica datetime
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbTechosDeducciones]
               SET 			    
			   tddu_Activo = 0,
			   tddu_UsuarioModifica = @tddu_UsuarioModifica,
			   tddu_FechaModifica = @tddu_FechaModifica
			   WHERE tddu_IdTechosDeducciones = @tddu_IdTechosDeducciones
                COMMIT TRAN
                SET @msj = @tddu_IdTechosDeducciones
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTechosDeducciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTechosDeducciones_Insert]
(
@tddu_PorcentajeColaboradores decimal (16,4),
@tddu_PorcentajeEmpresa decimal (16,4),
@tddu_Techo decimal (16,4),
@cde_IdDeducciones int,
@tddu_UsuarioCrea int,
@tddu_FechaCrea DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY
            DECLARE @tddu_IdTechosDeducciones INT
            SET @tddu_IdTechosDeducciones = (SELECT ISNULL(MAX(tddu_IdTechosDeducciones) + 1, 1) FROM [Plani].[tbTechosDeducciones]);
                INSERT INTO [Plani].[tbTechosDeducciones]
                (
                    tddu_IdTechosDeducciones, tddu_PorcentajeColaboradores, tddu_PorcentajeEmpresa, tddu_Techo, cde_IdDeducciones, tddu_Activo, tddu_UsuarioCrea, tddu_FechaCrea
                )
                VALUES
                (
                    @tddu_IdTechosDeducciones,
                    @tddu_PorcentajeColaboradores,
                    @tddu_PorcentajeEmpresa,
                    @tddu_Techo,
                    @cde_IdDeducciones,
					1,
                    @tddu_UsuarioCrea,
                    @tddu_FechaCrea					
                )
                COMMIT TRAN
                SET @msj = @tddu_IdTechosDeducciones
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN
                SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTechosDeducciones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Plani].[UDP_Plani_tbTechosDeducciones_Update] 
(
@tddu_IdTechosDeducciones int,
@tddu_PorcentajeColaboradores decimal (16,2),
@tddu_PorcentajeEmpresa decimal (16,2),
@tddu_Techo decimal (16,2),
@cde_IdDeducciones int,
@tddu_UsuarioModifica int,
@tddu_FechaModifica DATETIME
)
AS
BEGIN
    BEGIN TRAN
        DECLARE @msj nvarchar(100)
            BEGIN TRY           
                UPDATE [Plani].[tbTechosDeducciones]
               SET 			    
			   tddu_PorcentajeColaboradores		= @tddu_PorcentajeColaboradores, 
			   tddu_PorcentajeEmpresa			= @tddu_PorcentajeEmpresa, 			   
			   tddu_Techo						= @tddu_Techo,
			   cde_IdDeducciones				= @cde_IdDeducciones, 
			   tddu_UsuarioModifica				= @tddu_UsuarioModifica, 
			   tddu_FechaModifica				= @tddu_FechaModifica
			   WHERE tddu_IdTechosDeducciones	= @tddu_IdTechosDeducciones
                COMMIT TRAN
                SET @msj = @tddu_IdTechosDeducciones
				SELECT @msj AS MensajeError
            END TRY
            BEGIN CATCH
            ROLLBACK TRAN                
				SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
            END CATCH
        END

GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTipoDeduccion_Activar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTipoDeduccion_Activar]
    (
	   @tde_IdTipoDedu int,
	   @tde_UsuarioModifica int,
	   @tde_FechaModifica datetime
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
		SET NOCOUNT ON
           UPDATE  [Plani].[tbTipoDeduccion] 
		   SET    tde_Activo =  1,
				  tde_UsuarioModifica =   @tde_UsuarioModifica,
				  tde_FechaModifica = @tde_FechaModifica
            WHERE tde_IdTipoDedu = @tde_IdTipoDedu
			   
          COMMIT TRAN
		  SET  @msj  =CAST( @tde_IdTipoDedu AS nvarchar)		  
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1' + ERROR_MESSAGE() AS MensajeError
    END CATCH
	SELECT @msj as MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTipoDeduccion_Inactivar]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTipoDeduccion_Inactivar]
    (
	   @tde_IdTipoDedu int,
	   @tde_UsuarioModifica int,
	   @tde_FechaModifica datetime
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
		SET NOCOUNT ON
           UPDATE  [Plani].[tbTipoDeduccion] 
		   SET    tde_Activo = 0 ,
				  tde_UsuarioModifica =   @tde_UsuarioModifica,
				  tde_FechaModifica = @tde_FechaModifica
            WHERE tde_IdTipoDedu = @tde_IdTipoDedu
			   
          COMMIT TRAN
		  SET  @msj  =CAST( @tde_IdTipoDedu AS nvarchar)		  
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SELECT '-1' + ERROR_MESSAGE() AS MensajeError
    END CATCH
	SELECT @msj as MensajeError
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTipoDeduccion_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---PROCEDIMIENTO ALMACENADO DE LA TABLA TIPODEDUCCION (INSERT).
CREATE PROCEDURE [Plani].[UDP_Plani_tbTipoDeduccion_Insert]
    (
       @tde_Descripcion nvarchar(50),
	   @tde_UsuarioCrea int,
	   @tde_FechaCrea datetime
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
		DECLARE @tde_IdTipoDedu int
		SET @tde_IdTipoDedu = (SELECT ISNULL(MAX(tde_IdTipoDedu) + 1, 1) FROM [Plani].[tbTipoDeduccion]);
            INSERT INTO [Plani].[tbTipoDeduccion]
            (
              tde_IdTipoDedu, 
			  tde_Descripcion, 
			  tde_Activo, 
			  tde_UsuarioCrea, 
			  tde_FechaCrea
            )
            VALUES
            (
                (SELECT ISNULL(MAX(tde_IdTipoDedu) + 1, 1) FROM [Plani].[tbTipoDeduccion]),
                @tde_Descripcion,
				1,
                @tde_UsuarioCrea,
				@tde_FechaCrea
             )
          COMMIT TRAN
          SET  @msj  = @tde_IdTipoDedu
		  SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SET  @msj  = '-1' + ERROR_MESSAGE()
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_Plani_tbTipoDeduccion_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_Plani_tbTipoDeduccion_Update]
    (
	   @tde_IdTipoDedu int,
       @tde_Descripcion nvarchar(50),
	   @tde_UsuarioModifica int,
	   @tde_FechaModifica datetime
    )
AS
BEGIN
    BEGIN TRAN
       DECLARE @msj nvarchar(100)  
        BEGIN TRY
            UPDATE [Plani].[tbTipoDeduccion] 
		    SET tde_Descripcion = @tde_Descripcion,
				tde_UsuarioModifica = @tde_UsuarioModifica,
				tde_FechaModifica =  @tde_FechaModifica
           WHERE tde_IdTipoDedu = @tde_IdTipoDedu
		     	
          COMMIT TRAN
		  SET  @msj  = @tde_IdTipoDedu
		  SELECT @msj as MensajeError
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
             SET  @msj  = '-1' + ERROR_MESSAGE()
    END CATCH
END
GO
/****** Object:  StoredProcedure [Plani].[UDP_tbTipoPlanillaDetalleDeduccion_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_tbTipoPlanillaDetalleDeduccion_Insert]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cde_IdDeducciones INT
   , @cpla_IdPlanilla   INT
   , @tpdd_UsuarioCrea  INT
   , @tpdd_FechaCrea    DATETIME
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            -- Declarar la variable @tpdd_IdPlanillaDetDeduccion, es la que retornara el procedimiento como MensajeError
            DECLARE @tpdd_IdPlanillaDetDeduccion INT;
            SET @tpdd_IdPlanillaDetDeduccion =
            (
                SELECT ISNULL(MAX([tpdd_IdPlanillaDetDeduccion]) + 1, 1)
                FROM [Plani].[tbTipoPlanillaDetalleDeduccion]
            );

            -- Inicio del Insert de la tabla [Plani].[tbTipoPlanillaDetalleDeduccion]
            INSERT INTO [Plani].[tbTipoPlanillaDetalleDeduccion]
            ([tpdd_IdPlanillaDetDeduccion], 
             [cpla_IdPlanilla], 
             [cde_IdDeducciones], 
             [tpdd_UsuarioCrea], 
             [tpdd_FechaCrea], 
             [tpdd_Activo]
            )
            VALUES
            (@tpdd_IdPlanillaDetDeduccion, 
             @cpla_IdPlanilla, 
             @cde_IdDeducciones, 
             @tpdd_UsuarioCrea, 
             @tpdd_FechaCrea, 
             1
            );

            -- @tpdd_IdPlanillaDetDeduccion, era el Id a retornar por el procedimiento almacenado
            SELECT CAST(@tpdd_IdPlanillaDetDeduccion AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_tbTipoPlanillaDetalleDeduccion_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_tbTipoPlanillaDetalleDeduccion_Update]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla   INT
   , @cde_IdDeducciones INT
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            DELETE FROM [Plani].[tbTipoPlanillaDetalleDeduccion]
            WHERE [cpla_IdPlanilla] = @cpla_IdPlanilla
                  AND [cde_IdDeducciones] = @cde_IdDeducciones;
            SELECT CAST(1 AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_tbTipoPlanillaDetalleIngreso_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_tbTipoPlanillaDetalleIngreso_Insert]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cin_IdIngreso    INT
   , @cpla_IdPlanilla  INT
   , @tpdi_UsuarioCrea INT
   , @tpdi_FechaCrea   DATETIME
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            -- Declarar la variable @tpdi_IdDetallePlanillaIngreso, es la que retornara el procedimiento como MensajeError
            DECLARE @tpdi_IdDetallePlanillaIngreso INT;
            SET @tpdi_IdDetallePlanillaIngreso =
            (
                SELECT ISNULL(MAX([tpdi_IdDetallePlanillaIngreso]) + 1, 1)
                FROM [Plani].[tbTipoPlanillaDetalleIngreso]
            );

            -- Inicio del Insert de la tabla [Plani].[tbTipoPlanillaDetalleIngreso]
            INSERT INTO [Plani].[tbTipoPlanillaDetalleIngreso]
            (
			 [tpdi_IdDetallePlanillaIngreso], 
             [cin_IdIngreso], 
             [cpla_IdPlanilla], 
             [tpdi_UsuarioCrea], 
             [tpdi_FechaCrea], 
             [tpdi_Activo]
            )
            VALUES
            (
			 @tpdi_IdDetallePlanillaIngreso, 
             @cin_IdIngreso, 
             @cpla_IdPlanilla, 
             @tpdi_UsuarioCrea, 
             @tpdi_FechaCrea, 
             1
            );

            -- @tpdi_IdDetallePlanillaIngreso, era el Id a retornar por el procedimiento almacenado
            SELECT CAST(@tpdi_IdDetallePlanillaIngreso AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [Plani].[UDP_tbTipoPlanillaDetalleIngreso_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Plani].[UDP_tbTipoPlanillaDetalleIngreso_Update]
(
     -- Declarar los parametros que recibe el procedimiento almacenado
     @cpla_IdPlanilla INT
   , @cin_IdIngreso   INT
)
AS
    BEGIN
        -- Inicio del try
        BEGIN TRY
            --INICIO TRANSACCION
            BEGIN TRAN;
            DELETE FROM [Plani].[tbTipoPlanillaDetalleIngreso]
            WHERE [cpla_IdPlanilla] = @cpla_IdPlanilla
                  AND [cin_IdIngreso] = @cin_IdIngreso;

            -- @tpdi_IdDetallePlanillaIngreso, era el Id a retornar por el procedimiento almacenado
            SELECT CAST(1 AS VARCHAR) AS MensajeError;
            COMMIT TRAN;
        END TRY
        -- Inicio del catch
        BEGIN CATCH
            ROLLBACK TRAN;
            -- Retornar -1 para detectar que hubo un error al guardar el registro, y que lo vuelva a intentar
            SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError;
        END CATCH;
    END;
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbAreas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rrhh].[UDP_RRHH_tbAreas_Delete]
  (
  @area_Id int,
  @area_Razoninactivo nvarchar(100),
  @area_Usuariomodifica int,
  @area_Fechamodifica datetime
  )
  AS
   SET NOCOUNT ON;
  BEGIN
    BEGIN TRY
      BEGIN TRAN
      DECLARE @Id INT
      SET @Id = @area_Id
      UPDATE [RRHH].tbAreas
      SET
      area_Estado = 0,
      area_Razoninactivo = @area_Razoninactivo,
      area_Usuariomodifica = @area_Usuariomodifica,
      area_Fechamodifica = @area_Fechamodifica
      WHERE area_Id = @area_Id
      AND area_Estado = 1
      SELECT
        CAST(@Id AS VARCHAR) AS MensajeError
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
      SELECT
        '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbAreas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbAreas_Insert]
  (
    @suc_Id int,
	@car_descripcion nvarchar(50),
    @area_Descripcion nvarchar(50),
    @area_Usuariocrea int,
    @area_Fechacrea datetime
  )
   AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = (SELECT ISNULL((SELECT MAX(area_Id) FROM [RRHH].[tbAreas]),0) + 1)
declare @car_Id int
set @car_Id = (SELECT ISNULL((SELECT MAX(car_Id) FROM [RRHH].[tbCargos]),0) + 1)

insert into rrhh.tbcargos
(
car_Id,
car_Descripcion,
car_UsuarioCrea,
car_FechaCrea
)
values
(
@car_Id,
@car_descripcion,
@area_Usuariocrea,
@area_Fechacrea)

INSERT INTO [RRHH].tbAreas(
area_Id,
car_Id,
suc_Id,
area_Descripcion,
area_Usuariocrea,
area_Fechacrea
)
VALUES(
@Id,
@car_Id,
@suc_Id,
@area_Descripcion,
@area_Usuariocrea,
@area_Fechacrea
)
SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbAreas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE [rrhh].[UDP_RRHH_tbAreas_Restore]
	(
		@area_Id int,
		@area_Usuariomodifica int,
		@area_Fechamodifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @area_Id 
	UPDATE [rrhh].[tbAreas]
	SET   [area_Estado]=1,
		  [area_Usuariomodifica]= @area_Usuariomodifica,
		  [area_Fechamodifica]= @area_Fechamodifica,
		  [area_Razoninactivo] = NULL
	WHERE area_Id =@area_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbAreas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rrhh].[UDP_RRHH_tbAreas_Update]
  (
    @area_Id int,
    @car_Id int,
    @suc_Id int,
    @area_Descripcion nvarchar(50),
    @area_Usuariomodifica int,
    @area_Fechamodifica datetime
  )
  AS 
  SET NOCOUNT ON;
  BEGIN
    BEGIN TRY
      BEGIN TRAN
      DECLARE @Id INT
      SET @Id = @area_Id
      UPDATE [RRHH].tbAreas
      SET
      car_Id = @car_Id,
      suc_Id = @suc_Id,
      area_Descripcion = @area_Descripcion,
      area_Usuariomodifica = @area_Usuariomodifica,
      area_Fechamodifica = @area_Fechamodifica
      WHERE area_Id = @area_Id
      AND area_Estado = 1
      SELECT
        CAST(@Id AS VARCHAR) AS MensajeError
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
      SELECT
        '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCargos_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCargos_Delete]
	(
		@car_Id int,
		@car_razon_Inactivo nvarchar(100),
		@car_UsuarioModifica int,
		@car_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @car_id 
	UPDATE[RRHH].[tbCargos]
	SET   [car_Estado]=0,
		  [car_RazonInactivo] = @car_razon_Inactivo,
		  [car_UsuarioModifica]= @car_UsuarioModifica,
		  [car_FechaModifica]= @car_FechaModifica
	WHERE car_id =@car_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCargos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCargos_Insert]
	(
		@car_Descripcion nvarchar(100),
		@car_UsuarioCrea int,
		@car_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([car_Id]) FROM [RRHH].tbCargos),0) + 1)
	INSERT INTO [RRHH].tbCargos(
				car_Id, 
				car_Descripcion,
				car_Usuariocrea, 
				car_Fechacrea
	)
	VALUES(
				@Id,
				@car_Descripcion,
				@car_UsuarioCrea,
				@car_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCargos_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCargos_Restore]
	(
		@car_Id int,

		@car_UsuarioModifica int,
		@car_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @car_id 
	UPDATE[RRHH].[tbCargos]
	SET   [car_Estado]=1,

		  [car_UsuarioModifica]= @car_UsuarioModifica,
		  [car_FechaModifica]= @car_FechaModifica
	WHERE car_id =@car_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCargos_tbEmpleados_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCargos_tbEmpleados_Select]
 AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
SELECT 
[car_Descripcion]
FROM
[rrhh].[tbCargos]
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCargos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCargos_Update]
	(
		@car_Id int,
		@car_Descripcion nvarchar(100),
		@car_UsuarioModifica int,
		@car_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @car_Id
	UPDATE [RRHH].tbCargos
	SET    [car_Descripcion] = @car_Descripcion,
		   [car_UsuarioModifica] = @car_UsuarioModifica,
		   [car_FechaModifica] = @car_FechaModifica
	WHERE  Car_Id  = @car_Id AND Car_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCompetencias_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCompetencias_Delete]
	(
		@comp_Id int,
		@comp_razon_Inactivo nvarchar(100),
		@comp_UsuarioModifica int,
		@comp_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @comp_Id 
	UPDATE [RRHH].[tbCompetencias]
	SET   [comp_Estado]=0,
		  [comp_RazonInactivo] = @comp_razon_Inactivo,
		  [comp_UsuarioModifica]= @comp_UsuarioModifica,
		  [comp_FechaModifica]= @comp_FechaModifica
	WHERE comp_Id =@comp_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCompetencias_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCompetencias_Insert]
	(
		@comp_Descripcion nvarchar (100),
		@comp_UsuarioCrea int,
		@comp_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([comp_Id]) FROM [RRHH].[tbCompetencias]),0) + 1)
	INSERT INTO [RRHH].[tbCompetencias](
				comp_Id, 
				comp_Descripcion, 
				comp_UsuarioCrea, 
				comp_FechaCrea
	)
	VALUES(
				@Id,
				@comp_Descripcion,
				@comp_UsuarioCrea,
				@comp_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCompetencias_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCompetencias_Restore]
	(
		@comp_Id int,

		@comp_UsuarioModifica int,
		@comp_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @comp_Id 
	UPDATE [RRHH].[tbCompetencias]
	SET   [comp_Estado]=1,

		  [comp_UsuarioModifica]= @comp_UsuarioModifica,
		  [comp_FechaModifica]= @comp_FechaModifica
	WHERE comp_Id =@comp_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbCompetencias_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbCompetencias_Update]
	(
		@comp_Id int,
		@comp_Descripcion nvarchar(100),
		@comp_UsuarioModifica int,
		@comp_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @comp_Id
	UPDATE [RRHH].tbCompetencias
	SET   [comp_Descripcion] = @comp_Descripcion,
		  [comp_UsuarioModifica] = @comp_UsuarioModifica,
		  [comp_FechaModifica] = @comp_FechaModifica
	WHERE comp_Id  = @comp_Id AND comp_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbDepartamentos_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbDepartamentos_Delete]
	(
		@depto_Id int,
		@depto_razon_Inactivo nvarchar(100),
		@depto_UsuarioModifica int,
		@depto_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @depto_Id 
	UPDATE [rrhh].[tbDepartamentos]
	SET   [depto_Estado]=0,
		  [depto_RazonInactivo] = @depto_razon_Inactivo,
		  [depto_UsuarioModifica] = @depto_UsuarioModifica,
		  [depto_FechaModifica] = @depto_FechaModifica
	WHERE depto_Id = @depto_Id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbDepartamentos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbDepartamentos_Insert]
	(
		@area_Id int,
		@car_descripcion nvarchar(50),
		@depto_Descripcion nvarchar(100),
		@depto_Usuariocrea int,
		@depto_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([depto_Id]) FROM [rrhh].[tbDepartamentos] ),0) + 1)	
	declare @car_Id int
	set @car_Id = (SELECT ISNULL((SELECT MAX(car_Id) FROM [RRHH].[tbCargos]),0) + 1)
	insert into rrhh.tbcargos
	(
		car_Id,
		car_Descripcion,
		car_UsuarioCrea,
		car_FechaCrea
	)
	values
	(
		@car_Id,
		@car_descripcion,
		@depto_Usuariocrea,
		@depto_FechaCrea)
	INSERT INTO [rrhh].[tbDepartamentos](
				[depto_id], 
				[area_Id],
				[car_Id],
				[depto_descripcion],
				[depto_usuariocrea], 
				[depto_fechacrea]
	)
	VALUES(
				@Id,
				@area_Id,
				@car_Id,
				@depto_Descripcion,
				@depto_Usuariocrea,
				@depto_FechaCrea
	)
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbDepartamentos_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbDepartamentos_Restore]
    (
        @depto_Id int,
        @depto_Usuariomodifica int,
        @depto_Fechamodifica datetime
    ) 
    AS
    set nocount on;
    BEGIN
    BEGIN TRY
    BEGIN TRAN
    declare @Id int
    set @Id = @depto_Id 
    UPDATE [rrhh].[tbDepartamentos]
    SET   [depto_Estado]=1,
          [depto_Usuariomodifica]= @depto_Usuariomodifica,
          [depto_Fechamodifica]= @depto_Fechamodifica,
		  [depto_RazonInactivo] = NULL 
    WHERE depto_Id =@depto_Id
    SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
    COMMIT TRAN
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
    SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
    END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbDepartamentos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [rrhh].[UDP_RRHH_tbDepartamentos_Update]
	(
		@depto_Id int,
		@area_Id int,
		@car_Id int,
		@depto_Descripcion nvarchar(100),
		@depto_UsuarioModifica int,
		@depto_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @depto_Id
	UPDATE [rrhh].[tbDepartamentos]
	SET area_Id = @area_Id,
		car_Id = @car_Id,
	    depto_Descripcion = @depto_Descripcion,		
		depto_UsuarioModifica = @depto_UsuarioModifica,
		depto_FechaModifica = @depto_FechaModifica
	WHERE depto_Id  = @depto_Id AND depto_Estado = 1
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEmpresas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEmpresas_Delete]
	(
		@empr_Id int,
		@empr_razon_Inactivo nvarchar(100),
		@empr_UsuarioModifica int,
		@empr_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @empr_id 
	UPDATE [RRHH].[tbEmpresas]
	SET    [empr_Estado]=0,
		   [empr_RazonInactivo] = @empr_razon_Inactivo,
		   [empr_UsuarioModifica]=@empr_UsuarioModifica,
		   [empr_FechaModifica]= @empr_FechaModifica
	WHERE  empr_id =@empr_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEmpresas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEmpresas_Insert]
	(
		@empr_Nombre nvarchar (100),
		@empr_usuarioCrea int,
		@empr_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([empr_Id]) FROM [RRHH].[tbEmpresas]),0) + 1)
	INSERT INTO [RRHH].[tbEmpresas](
				empr_Id,
				empr_Nombre,
				empr_UsuarioCrea,
				empr_FechaCrea
	)
	VALUES(
				@Id,
				@empr_Nombre,
				@empr_usuarioCrea,
				@empr_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEmpresas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEmpresas_Restore]
	(
		@empr_Id int,

		@empr_UsuarioModifica int,
		@empr_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @empr_id 
	UPDATE [RRHH].[tbEmpresas]
	SET    [empr_Estado]=1,

		   [empr_UsuarioModifica]=@empr_UsuarioModifica,
		   [empr_FechaModifica]= @empr_FechaModifica
	WHERE  empr_id =@empr_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEmpresas_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEmpresas_Select]
(
@empr_Id int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = @empr_Id
SELECT 
[empr_Nombre], 
[empr_Estado],
[empr_RazonInactivo]
FROM
[ERP_GMEDINA].[RRHH].[tbEmpresas]
WHERE [empr_Id]=@empr_Id AND [empr_Estado] = 1

SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEmpresas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEmpresas_Update]
	(
		@empr_Id int,
		@empr_Nombre nvarchar (100),
		@empr_usuarioModifica int,
		@empr_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @empr_Id
	UPDATE [RRHH].tbEmpresas
	SET    [empr_Nombre] = @empr_Nombre,
		   [empr_UsuarioModifica] = @empr_UsuarioModifica,
		   [empr_FechaModifica] = @empr_FechaModifica
	WHERE  empr_Id  = @empr_Id AND empr_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEquipoTrabajo_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------------------------DELETE Equipo Trabajo----
	CREATE PROCEDURE [rrhh].[UDP_RRHH_tbEquipoTrabajo_Delete]
	(
		@eqtra_Id int,
		@eqtra_RazonInactivo nvarchar(100),
		@eqtra_UsuarioModifica int,
		@eqtra_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @eqtra_Id  
	UPDATE [RRHH].[tbEquipoTrabajo]
	SET   [eqtra_Estado]=0,
		  eqtra_RazonInactivo = @eqtra_RazonInactivo,
		  eqtra_UsuarioModifica = @eqtra_UsuarioModifica,
		  eqtra_FechaModifica = @eqtra_FechaModifica
	WHERE eqtra_Id = @eqtra_Id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEquipoTrabajo_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	CREATE PROCEDURE [rrhh].[UDP_RRHH_tbEquipoTrabajo_Insert]
	(
		@eqtra_Codigo nvarchar(25), 
		@eqtra_Descripcion nvarchar(50), 
		@eqtra_Observacion nvarchar(50),  
		@eqtra_UsuarioCrea int, 
		@eqtra_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX(eqtra_Id) FROM  [rrhh].[tbEquipoTrabajo]),0) + 1)
	INSERT INTO [rrhh].[tbEquipoTrabajo](
				eqtra_Id, 
				eqtra_Codigo,
				eqtra_Descripcion,
				eqtra_Observacion,
				eqtra_UsuarioCrea,
				eqtra_FechaCrea		    
	)
	VALUES(
				@Id,
				@eqtra_Codigo,
				@eqtra_Descripcion,
				@eqtra_Observacion,
				@eqtra_UsuarioCrea,
				@eqtra_FechaCrea	

	)
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEquipoTrabajo_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------------------------Restore Equipo Trabajo----
	CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbEquipoTrabajo_Restore]
	(
		@eqtra_Id int,

		@eqtra_UsuarioModifica int,
		@eqtra_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @eqtra_Id  
	UPDATE [RRHH].[tbEquipoTrabajo]
	SET   [eqtra_Estado]=1,

		  eqtra_UsuarioModifica = @eqtra_UsuarioModifica,
		  eqtra_FechaModifica = @eqtra_FechaModifica
	WHERE eqtra_Id = @eqtra_Id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbEquipoTrabajo_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbEquipoTrabajo_Update]
	(   
	    @eqtra_Id INT,
		@eqtra_Codigo nvarchar(25), 
		@eqtra_Descripcion nvarchar(50), 
		@eqtra_Observacion nvarchar(50),  
		@eqtra_UsuarioModifica int, 
		@eqtra_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @eqtra_Id
	UPDATE [RRHH].tbEquipoTrabajo	    
	SET	   eqtra_Codigo = @eqtra_Codigo,
	       eqtra_Descripcion = @eqtra_Descripcion,
		   eqtra_Observacion = @eqtra_Observacion,
		   eqtra_UsuarioModifica = @eqtra_UsuarioModifica,
		   eqtra_FechaModifica = @eqtra_FechaModifica
	WHERE  eqtra_Id  = @eqtra_Id AND  eqtra_Estado= 1
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbfasesReclutamiento_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbfasesReclutamiento_Delete]
	(
		@fare_Id int,
		@fare_razon_Inactivo nvarchar(100),
		@fare_UsuarioModifica int,
		@fare_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @fare_id 
	UPDATE [RRHH].tbfasesReclutamiento
	SET    [fare_Estado]=0,
		   [fare_RazonInactivo] = @fare_razon_Inactivo,
		   [fare_UsuarioModifica] = @fare_UsuarioModifica,
		   [fare_FechaModifica]= @fare_FechaModifica
	WHERE  fare_id =@fare_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbFasesReclutamiento_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbFasesReclutamiento_Insert]
	(
		@fare_Descripcion nvarchar(100),
		@fare_UsuarioCrea int,
		@fare_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([fare_Id]) FROM [RRHH].tbFasesReclutamiento),0) + 1)
	INSERT INTO [RRHH].tbFasesReclutamiento(
				fare_Id,
				fare_Descripcion,
				fare_UsuarioCrea, 
				fare_FechaCrea
	)
	VALUES(
				@Id,
				@fare_Descripcion,
				@fare_UsuarioCrea,
				@fare_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbfasesReclutamiento_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbfasesReclutamiento_Restore]
	(
		@fare_Id int,

		@fare_UsuarioModifica int,
		@fare_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @fare_id 
	UPDATE [RRHH].tbfasesReclutamiento
	SET    [fare_Estado]=1,

		   [fare_UsuarioModifica] = @fare_UsuarioModifica,
		   [fare_FechaModifica]= @fare_FechaModifica
	WHERE  fare_id =@fare_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbFasesReclutamiento_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbFasesReclutamiento_Update]
	(
		@fare_Id int,
		@fare_Descripcion nvarchar(50),
		@fare_UsuarioModifica int,
		@fare_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @fare_Id
	UPDATE [RRHH].[tbFasesReclutamiento]
	SET    [fare_Descripcion] = @fare_Descripcion,
		   [fare_UsuarioModifica] = @fare_UsuarioModifica,
		   [fare_FechaModifica] = @fare_FechaModifica
	WHERE  fare_Id  = @fare_Id AND fare_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHabilidades_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbHabilidades_Delete]
	(
		@habi_id int,
		@habi_razon_Inactivo nvarchar(100),
		@habi_UsuarioModifica int,
		@habi_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @habi_id 
	UPDATE [RRHH].tbHabilidades
	SET    [habi_Estado]=0,
		   [habi_RazonInactivo] = @habi_razon_Inactivo,
		   [habi_UsuarioModifica]=@habi_UsuarioModifica,
		   [habi_FechaModifica]= @habi_FechaModifica
	WHERE  habi_Id =@habi_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHabilidades_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbHabilidades_Insert]
	(
		@habi_Descripcion nvarchar(100),
		@habi_UsuarioCrea  int,
		@habi_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([habi_Id]) FROM [RRHH].tbHabilidades),0) + 1)
	INSERT INTO [RRHH].tbHabilidades(
				habi_Id,
				habi_Descripcion,
				habi_UsuarioCrea, 
				habi_FechaCrea
	)
	VALUES(
				@Id,
				@habi_Descripcion,
				@habi_UsuarioCrea,
				@habi_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHabilidades_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbHabilidades_Restore]
	(
		@habi_id int,

		@habi_UsuarioModifica int,
		@habi_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @habi_id 
	UPDATE [RRHH].tbHabilidades
	SET    [habi_Estado]=1,

		   [habi_UsuarioModifica]=@habi_UsuarioModifica,
		   [habi_FechaModifica]= @habi_FechaModifica
	WHERE  habi_Id =@habi_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHabilidades_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbHabilidades_Update]
	(
		@habi_Id int,
		@habi_Descripcion nvarchar(50),
		@habi_UsuarioModifica int,
		@habi_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @habi_Id
	UPDATE [RRHH].[tbHabilidades]
	SET    [habi_Descripcion] = @habi_Descripcion,
		   [habi_UsuarioModifica] = @habi_UsuarioModifica,
		   [habi_FechaModifica] = @habi_FechaModifica
	WHERE  habi_Id  = @habi_Id  AND habi_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Delete]
	(
		@hamo_Id int,
		@hamo_RazonInactivo nvarchar(100),
		@hamo_UsuarioModifica int,
		@hamo_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hamo_Id 
	UPDATE[RRHH].[tbHistorialAmonestaciones]
	SET   [hamo_Estado]=0,
		  [hamo_RazonInactivo] = @hamo_RazonInactivo,
		  [hamo_UsuarioModifica]= @hamo_UsuarioModifica,
		  [hamo_FechaModifica]= @hamo_FechaModifica
	WHERE hamo_Id =@hamo_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Insert]
	(
	    @emp_Id int,
		@tamo_Id int,
		@hamo_Fecha datetime,
		@hamo_Observacion nvarchar(25),	
		@hamo_UsuarioCrea int,
		@hamo_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @hamo_AmonestacionAnterior int
	set 	@hamo_AmonestacionAnterior =  (Select top 1 ISNULL(hamo_Id,NULL) from rrhh.tbHistorialAmonestaciones where hamo_Estado = 1 and tamo_Id = @tamo_Id Order By hamo_Id desc)
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([hamo_Id]) FROM [RRHH].tbHistorialAmonestaciones),0) + 1)
	INSERT INTO [RRHH].tbHistorialAmonestaciones(
				hamo_Id, 
				emp_Id,
				tamo_Id,
				hamo_Fecha,
				hamo_AmonestacionAnterior,
				hamo_Observacion,
				hamo_UsuarioCrea, 
				hamo_FechaCrea
	)
	VALUES(
				@Id,
				@emp_Id,
				@tamo_Id,
				@hamo_Fecha,
				@hamo_AmonestacionAnterior,
				@hamo_Observacion,
				@hamo_UsuarioCrea,
				@hamo_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAmonestaciones_Restore]
	(
		@hamo_Id int,
		@hamo_UsuarioModifica int,
		@hamo_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hamo_id 
	UPDATE[RRHH].[tbHistorialAmonestaciones]
	SET   [hamo_Estado]=1,
		  [hamo_UsuarioModifica]= @hamo_UsuarioModifica,
		  [hamo_FechaModifica]= @hamo_FechaModifica
	WHERE hamo_id =@hamo_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Delete]
	(
		@aude_Id int,
		@aude_RazonInactivo nvarchar(100),
		@aude_UsuarioModifica int,
		@aude_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @aude_id 
	UPDATE[RRHH].[tbHistorialAudienciaDescargo]
	SET   [aude_Estado]=0,
		  [aude_RazonInactivo] = @aude_RazonInactivo,
		  [aude_UsuarioModifica]= @aude_UsuarioModifica,
		  [aude_FechaModifica]= @aude_FechaModifica
	WHERE aude_id =@aude_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Insert]
	(
	    @emp_Id int,
		@aude_Descripcion nvarchar(25),
		@aude_FechaAudiencia datetime,
		@aude_Testigo bit,
		@aude_DireccionArchivo nvarchar(25),
		@aude_UsuarioCrea int,
		@aude_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([aude_Id]) FROM [RRHH].tbHistorialAudienciaDescargo),0) + 1)
	INSERT INTO [RRHH].tbHistorialAudienciaDescargo(
				aude_Id, 
				emp_Id,
				aude_Descripcion,
				aude_FechaAudiencia,
				aude_Testigo,
				aude_DireccionArchivo,
				aude_UsuarioCrea, 
				aude_FechaCrea
	)
	VALUES(
				@Id,
				@emp_Id,
				@aude_Descripcion,
				@aude_FechaAudiencia,
				@aude_Testigo,
				@aude_DireccionArchivo,
				@aude_UsuarioCrea,
				@aude_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Restore]
	(
		@aude_Id int,
		@aude_UsuarioModifica int,
		@aude_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @aude_Id 
	UPDATE[RRHH].[tbHistorialAudienciaDescargo]
	SET   [aude_Estado]=1,
		  [aude_UsuarioModifica]= @aude_UsuarioModifica,
		  [aude_FechaModifica]= @aude_FechaModifica
	WHERE aude_Id =@aude_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialAudienciaDescargo_Update]
	(
		@aude_Id int,
		@aude_FechaAudiencia datetime,
		@aude_UsuarioModifica int,
		@aude_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @aude_Id
	UPDATE [RRHH].tbtbHistorialAudienciaDescargo
	SET    [aude_FechaAudiencia] = @aude_FechaAudiencia,
		   [aude_UsuarioModifica] = @aude_UsuarioModifica,
		   [aude_FechaModifica] = @aude_FechaModifica
	WHERE  aude_Id  = @aude_Id AND aude_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialContrataciones_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [rrhh].[UDP_RRHH_tbHistorialContrataciones_Delete]
	(	@hcon_Id int,		
		@hcon_RazonInactivo nvarchar(100),
		@hcon_UsuarioModifica int,
		@hcon_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hcon_id 
	UPDATE[RRHH].[tbHistorialContrataciones]
	SET   [hcon_Estado]=0,
		  [hcon_RazonInactivo] = @hcon_RazonInactivo,
		  [hcon_UsuarioModifica]= @hcon_UsuarioModifica,
		  [hcon_FechaModifica]= @hcon_FechaModifica
	WHERE hcon_id =@hcon_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialContrataciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialContrataciones_Insert]
	(	
        @scan_Id int,
        @depto_Id int,
        @hcon_FechaContratado date,
        @hcon_UsuarioCrea int,
        @hcon_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([hcon_Id]) FROM [RRHH].tbHistorialContrataciones),0) + 1)
INSERT INTO [rrhh].[tbHistorialContrataciones]
           ([hcon_Id]
			,[scan_Id]
			,[depto_Id]
			,hcon_FechaContratado
			,[hcon_UsuarioCrea]
			,[hcon_FechaCrea]
			)
     VALUES
           (@Id,
			@scan_Id,
			@depto_Id,
			@hcon_FechaContratado,
			@hcon_UsuarioCrea,
			@hcon_FechaCrea
			)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END

	exec [rrhh].[UDP_RRHH_tbHistorialContrataciones_Insert] 1,1,'2019-12-09 14:34:03.323',1,'2019-12-09 14:34:03.323'

select * from [rrhh].[tbHistorialContrataciones]




GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialContrataciones_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [rrhh].[UDP_RRHH_tbHistorialContrataciones_Restore]
	(
		@hcon_Id int,
		@hcon_UsuarioModifica int,
		@hcon_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hcon_Id 
	UPDATE[RRHH].[tbHistorialContrataciones]
	SET   [hcon_Estado]=1,
		  [hcon_UsuarioModifica]= @hcon_UsuarioModifica,
		  [hcon_FechaModifica]= @hcon_FechaModifica
	WHERE hcon_Id =@hcon_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialContrataciones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialContrataciones_Update]
	(
		@hcon_Id int,
		@scan_Id int,
		@depto_Id int,
		@hcon_FechaContratado datetime,
		@hcon_UsuarioModifica int,
		@hcon_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hcon_Id
	UPDATE [rrhh].[tbHistorialContrataciones]
	SET		[scan_Id] = @scan_Id,
			[depto_Id] = @depto_Id,
			[hcon_FechaContratado] = @hcon_FechaContratado,
			[hcon_UsuarioModifica] = @hcon_UsuarioModifica,
			[hcon_FechaModifica] = @hcon_FechaModifica
	WHERE  hcon_Id  = @hcon_Id AND hcon_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Delete]
    (
        @hinc_Id int,
        @hinc_RazonInactivo nvarchar(100),
        @hinc_UsuarioModifica int,
        @hinc_FechaModifica datetime
    ) 
    AS
    set nocount on;
    BEGIN
    BEGIN TRY
    BEGIN TRAN
    declare @Id int
    set @Id = @hinc_id 
    UPDATE[RRHH].tbHistorialIncapacidades
    SET   [hinc_UsuarioModifica]= @hinc_UsuarioModifica,
          [hinc_FechaModifica]= @hinc_FechaModifica
    WHERE hinc_id =@hinc_id
    SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
    COMMIT TRAN
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
    SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
    END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Insert]
	(
		@Emp_Id int , 
		@ticn_Id int, 
		@hinc_Dias int,
		@hinc_CentroMedico nvarchar(100),
		@hinc_Doctor nvarchar(50),
		@hinc_Diagnostico nvarchar(150), 
		@hinc_FechaInicio datetime, 
		@hinc_FechaFin datetime, 
		@hinc_UsuarioCrea int, 
		@hinc_FechaCrea datetime
		
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([hinc_Id]) FROM [RRHH].tbHistorialIncapacidades),0) + 1)
	INSERT INTO [RRHH].tbHistorialIncapacidades
				(
				hinc_Id, 
				Emp_Id, 
				ticn_Id, 
				[hinc_Dias], 
				[hinc_CentroMedico], 
                [hinc_Doctor],
                [hinc_Diagnostico],
				hinc_FechaInicio, 
				hinc_FechaFin, 
				hinc_UsuarioCrea, 
				hinc_FechaCrea
				
				)
	VALUES	(
			@Id,
			@Emp_Id , 
			@ticn_Id, 
			@hinc_Dias,
			@hinc_CentroMedico , 
			@hinc_Doctor,
			@hinc_Diagnostico,
			@hinc_FechaInicio , 
			@hinc_FechaFin , 
			@hinc_UsuarioCrea , 
			@hinc_FechaCrea 
			)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialIncapacidades_Restore]
	(
		@hinc_Id int,
		@hinc_UsuarioModifica int,
		@hinc_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @hinc_Id 
	UPDATE[RRHH].[tbHistorialIncapacidades]
	SET   [hinc_Estado]=1,
		  [hinc_UsuarioModifica]= @hinc_UsuarioModifica,
		  [hinc_FechaModifica]= @hinc_FechaModifica
	WHERE hinc_Id =@hinc_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialPermisos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialPermisos_Insert]
(
		@hper_Id INT
		,@emp_Id INT
		,@tper_Id INT
		,@hper_fechaInicio DATETIME
		,@hper_fechaFin DATETIME
		,@hper_Duracion INT
		,@hper_Observacion NVARCHAR(25)
		,@hper_PorcentajeIndemnizado INT
		,@hper_Estado BIT
		,@hper_RazonInactivo NVARCHAR(100)
		,@hper_UsuarioCrea INT
		,@hper_FechaCrea DATETIME
		)
   AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = (SELECT ISNULL((SELECT MAX(hper_Id) FROM [RRHH].tbHistorialPermisos),0) + 1)


INSERT INTO [RRHH].tbHistorialPermisos(
	hper_Id
	,emp_Id
	,tper_Id
	,hper_fechaInicio
	,hper_fechaFin
	,hper_Duracion
	,hper_Observacion
	,hper_PorcentajeIndemnizado
	,hper_Estado
	,hper_RazonInactivo
	,hper_UsuarioCrea
	,hper_FechaCrea
)
VALUES(
@Id
,@emp_Id
	,@tper_Id
	,@hper_fechaInicio
	,@hper_fechaFin
	,@hper_Duracion
	,@hper_Observacion
	,@hper_PorcentajeIndemnizado
	,@hper_Estado
	,@hper_RazonInactivo
	,@hper_UsuarioCrea
	,@hper_FechaCrea
)
SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialSalidas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


	CREATE    PROCEDURE [rrhh].[UDP_RRHH_tbHistorialSalidas_Delete]
	(
		@Hsal_Id int,
		@hsal_RazonInactivo nvarchar(25),
		@hsal_UsuarioModifica int,
		@hsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX(hs.hsal_Id) FROM rrhh.tbHistorialSalidas AS hs),0)+1)

Update rrhh.tbHistorialSalidas
set
	[hsal_Estado]			=	0,
	[hsal_RazonInactivo]	=	@hsal_RazonInactivo,
	[hsal_UsuarioModifica]	=	@hsal_UsuarioModifica,
	[hsal_FechaModifica]	=	@hsal_FechaModifica
where [hsal_Id] = @Hsal_Id
    SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
    COMMIT TRAN
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
    SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
    END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialSalidas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialSalidas_Insert]
	(
		@emp_Id int,
		@tsal_Id int,
		@rsal_Id int,
		@hsal_FechaSalida datetime,
		@hsal_Observacion nvarchar(25),
		@hsal_UsuarioCrea int,
		@hsal_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX(hs.hsal_Id) FROM rrhh.tbHistorialSalidas AS hs),0)+1)

INSERT INTO rrhh.tbHistorialSalidas
(
	 hsal_Id
	 ,emp_Id
	 ,tsal_Id
	 ,rsal_Id
	 ,hsal_FechaSalida
	 ,hsal_Observacion
	 ,hsal_UsuarioCrea
	 ,hsal_FechaCrea
)
VALUES
(
	@Id,
	@emp_Id,
	@tsal_Id,
	@rsal_Id,
	@hsal_FechaSalida,
	@hsal_Observacion,
	@hsal_UsuarioCrea,
	@hsal_FechaCrea
);

UPDATE ERP_GMEDINA.rrhh.tbEmpleados 
       SET emp_Estado =0
       WHERE emp_Id = @emp_Id


    SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
    COMMIT TRAN
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
    SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
    END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialSalidas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


	CREATE       PROCEDURE [rrhh].[UDP_RRHH_tbHistorialSalidas_Restore]
	(
		@Hsal_Id int,
		@hsal_UsuarioModifica int,
		@hsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX(hs.hsal_Id) FROM rrhh.tbHistorialSalidas AS hs),0)+1)

Update rrhh.tbHistorialSalidas
set
	[hsal_Estado]			=	1,
	[hsal_RazonInactivo]	=	null,
	[hsal_UsuarioModifica]	=	@hsal_UsuarioModifica,
	[hsal_FechaModifica]	=	@hsal_FechaModifica
where [hsal_Id] = @Hsal_Id
    SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
    COMMIT TRAN
    END TRY
    BEGIN CATCH
    ROLLBACK TRAN
    SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
    END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialVacaciones_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialVacaciones_Delete] (@hvac_Id INT,
@hvac_RazonInactivo NVARCHAR(50),
@hvac_UsuarioModifica INT,
@hvac_FechaModifica DATETIME)
AS
  SET NOCOUNT ON;
  BEGIN
    BEGIN TRY
      BEGIN TRAN
      DECLARE @Id INT
      SET @Id = (SELECT
          ISNULL((SELECT
              MAX(hv.hvac_Id)
            FROM rrhh.tbHistorialVacaciones as hv )
          , 0) + 1)

      UPDATE rrhh.tbHistorialVacaciones
      SET [hvac_Estado] = 0
         ,[hvac_RazonInactivo] = @hvac_RazonInactivo
         ,[hvac_UsuarioModifica] = @hvac_UsuarioModifica
         ,[hvac_FechaModifica] = @hvac_FechaModifica
      WHERE [hvac_Id] = @hvac_Id
      SELECT
        CAST(@Id AS VARCHAR(10)) AS MensajeError
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
      SELECT
        '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialVacaciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialVacaciones_Insert] (
@emp_Id int,
@hvac_FechaInicio DATETIME, 
@hvac_FechaFin DATETIME,
@hvac_MesVacaciones int,
@hvac_AnioVacaciones int,
@hvac_UsuarioCrea INT,
@hvac_FechaCrea DATETIME)
AS
  SET NOCOUNT ON;
  BEGIN
    BEGIN TRY
      BEGIN TRAN
      DECLARE @Id INT
      SET @Id = (SELECT
          ISNULL((SELECT
              MAX(hv.hvac_Id)
            FROM rrhh.tbHistorialVacaciones AS hv)
          , 0) + 1)

      INSERT INTO rrhh.tbHistorialVacaciones (hvac_Id
      , emp_Id
      , hvac_FechaInicio
      , hvac_FechaFin
      , hvac_MesVacaciones
      , hvac_AnioVacaciones
      , hvac_UsuarioCrea
      , hvac_FechaCrea)
        VALUES (@Id,@emp_Id ,
@hvac_FechaInicio, 
@hvac_FechaFin,
@hvac_MesVacaciones,
@hvac_AnioVacaciones,
@hvac_UsuarioCrea,
@hvac_FechaCrea);
      SELECT
        CAST(@Id AS VARCHAR(10)) AS MensajeError
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
      SELECT
        '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHistorialVacaciones_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHistorialVacaciones_Restore] (@hvac_Id INT,
@hvac_UsuarioModifica INT,
@hvac_FechaModifica DATETIME)
AS
  SET NOCOUNT ON;
  BEGIN
    BEGIN TRY
      BEGIN TRAN
      DECLARE @Id INT
      SET @Id = (SELECT
          ISNULL((SELECT
              MAX(hv.hvac_Id)
            FROM rrhh.tbHistorialVacaciones as hv)
          , 0) + 1)

      UPDATE rrhh.tbHistorialVacaciones
      SET [hvac_Estado] = 1
         ,[hvac_RazonInactivo] = NULL
         ,[hvac_UsuarioModifica] = @hvac_UsuarioModifica
         ,[hvac_FechaModifica] = @hvac_FechaModifica
      WHERE [hvac_Id] = @hvac_Id
      SELECT
        CAST(@Id AS VARCHAR(10)) AS MensajeError
      COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
      SELECT
        '-1 ' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHorarios_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHorarios_Delete]
  (
    @hor_Id int,
    @hor_RazonInactivo nvarchar(100),
    @hor_UsuarioModifica int,
    @hor_FechaModifica datetime
  )
  AS
  SET NOCOUNT ON
  BEGIN
  BEGIN TRY
    BEGIN TRAN
  DECLARE @Id INT
  SET @Id = @hor_Id
    UPDATE ERP_GMEDINA.rrhh.tbHorarios 
     SET 
      hor_Estado = 0
      ,hor_RazonInactivo = @hor_RazonInactivo
      ,hor_UsuarioModifica = @hor_UsuarioModifica
      ,hor_FechaModifica = @hor_FechaModifica
     WHERE hor_Id = @hor_Id
  SELECT
    CAST(@Id AS VARCHAR(10)) AS MensajeError
  COMMIT TRAN
  END TRY
  BEGIN CATCH
    ROLLBACK TRAN
  SELECT '-1' + ERROR_MESSAGE() AS MensajeError
  END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHorarios_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHorarios_Insert]
  (
    @jor_Id int,
    @hor_Descripcion nvarchar(50),
    @hor_HoraInicio time,
    @hor_HoraFin time,
    @hor_CantidadHoras time,
    @hor_UsuarioCrea int,
    @hor_FechaCrea datetime
  )
  AS
  SET NOCOUNT ON
  BEGIN
  BEGIN TRY
    BEGIN TRAN
  DECLARE @Id INT
  SET @Id = (SELECT ISNULL((SELECT MAX(hor_Id) FROM ERP_GMEDINA.rrhh.tbHorarios), 0) + 1)
  INSERT INTO rrhh.tbHorarios (hor_Id, jor_Id, hor_Descripcion, hor_HoraInicio, hor_HoraFin, hor_CantidadHoras, hor_UsuarioCrea, hor_FechaCrea)
  VALUES (@Id, @jor_Id, @hor_Descripcion, @hor_HoraInicio, @hor_HoraFin, @hor_CantidadHoras , @hor_UsuarioCrea, @hor_FechaCrea)
  SELECT
    CAST(@Id AS VARCHAR(10)) AS MensajeError
  COMMIT TRAN
  END TRY
  BEGIN CATCH
    ROLLBACK TRAN
  SELECT '-1' + ERROR_MESSAGE() AS MensajeError
  END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHorarios_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbHorarios_Restore]
  (
    @hor_Id int,
    @hor_RazonInactivo nvarchar(100),
    @hor_UsuarioModifica int,
    @hor_FechaModifica datetime
  )
  AS
  SET NOCOUNT ON
  BEGIN
  BEGIN TRY
    BEGIN TRAN
  DECLARE @Id INT
  SET @Id = @hor_Id
    UPDATE ERP_GMEDINA.rrhh.tbHorarios 
     SET 
      hor_Estado = 1
      ,hor_RazonInactivo = ''
      ,hor_UsuarioModifica = @hor_UsuarioModifica
      ,hor_FechaModifica = @hor_FechaModifica
     WHERE hor_Id = @hor_Id
  SELECT
    CAST(@Id AS VARCHAR(10)) AS MensajeError
  COMMIT TRAN
  END TRY
  BEGIN CATCH
    ROLLBACK TRAN
  SELECT '-1' + ERROR_MESSAGE() AS MensajeError
  END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbHorarios_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [rrhh].[UDP_RRHH_tbHorarios_Update]
  (
    @hor_Id int,
    @hor_Descripcion nvarchar(50),
    @hor_HoraInicio time,
    @hor_HoraFin time,
    @hor_CantidadHoras time,
    @hor_UsuarioModifica int,
    @hor_FechaModifica datetime
  )
  AS
  SET NOCOUNT ON
  BEGIN
  BEGIN TRY
    BEGIN TRAN
  DECLARE @Id INT
  SET @Id = @hor_Id
    UPDATE ERP_GMEDINA.rrhh.tbHorarios 
     SET 
    hor_Descripcion = @hor_Descripcion
    ,hor_HoraInicio = @hor_HoraInicio
    ,hor_HoraFin = @hor_HoraFin
    ,hor_CantidadHoras = @hor_CantidadHoras
    ,hor_UsuarioModifica = @hor_UsuarioModifica
    ,hor_FechaModifica = @hor_FechaModifica
     WHERE hor_Id = @hor_Id
  SELECT
    CAST(@Id AS VARCHAR(10)) AS MensajeError
  COMMIT TRAN
  END TRY
  BEGIN CATCH
    ROLLBACK TRAN
  SELECT '-1' + ERROR_MESSAGE() AS MensajeError
  END CATCH
  END
  
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbIdiomas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbIdiomas_Delete]
	(
		@idi_Id int,
		@idi_razon_Inactivo nvarchar(100),
		@idi_UsuarioModifica int,
		@idi_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @idi_id 
	UPDATE[RRHH].[tbIdiomas]
	SET   [idi_Estado]=0,
		  [idi_RazonInactivo] = @idi_razon_Inactivo,
		  [idi_UsuarioModifica]=@idi_UsuarioModifica,
		  [idi_FechaModifica]=@idi_FechaModifica
	WHERE idi_id =@idi_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbIdiomas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbIdiomas_Insert]
	(
		@idi_Descripcion nvarchar(100),
		@idi_UsuarioCrea int,
		@idi_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([idi_Id]) FROM [RRHH].[tbIdiomas]),0) + 1)
	INSERT INTO [RRHH].[tbIdiomas](
				idi_Id,
				idi_Descripcion,
				idi_UsuarioCrea,
				idi_FechaCrea
	)
	VALUES(
				@Id,
				@idi_Descripcion,
				@idi_UsuarioCrea,
				@idi_FechaCrea 
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbIdiomas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbIdiomas_Restore]
	(
		@idi_Id int,

		@idi_UsuarioModifica int,
		@idi_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @idi_id 
	UPDATE[RRHH].[tbIdiomas]
	SET   [idi_Estado]=1,

		  [idi_UsuarioModifica]=@idi_UsuarioModifica,
		  [idi_FechaModifica]=@idi_FechaModifica
	WHERE idi_id =@idi_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbIdiomas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbIdiomas_Update]
	(
		@idi_Id int,
		@idi_Descripcion nvarchar(100),
		@idi_UsuarioModifica int,
		@idi_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @idi_Id
	UPDATE [RRHH].tbIdiomas
	SET    [idi_Descripcion] = @idi_Descripcion,
		   [idi_UsuarioModifica] = @idi_UsuarioModifica,
		   [idi_FechaModifica] = @idi_FechaModifica
	WHERE  idi_Id  = @idi_Id  AND idi_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbJornadas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbJornadas_Delete]
	(
		@jor_Id int,
		@jor_razon_Inactivo nvarchar(100),
		@jor_UsuarioModifica int,
		@jor_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @jor_Id 
	UPDATE [rrhh].[tbJornadas]
	SET   [jor_Estado]=0,
		  [jor_RazonInactivo] = @jor_razon_Inactivo,
		  [jor_UsuarioModifica] = @jor_UsuarioModifica,
		  [jor_FechaModifica] = @jor_FechaModifica
	WHERE jor_Id =@jor_Id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbJornadas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbJornadas_Insert]
  (
  @jor_Id int,
  @jor_Descripcion nvarchar(30),
  @jor_Estado bit,
  @jor_RazonInactivo nvarchar(100),
  @jor_UsuarioCrea int,
  @jor_FechaCrea datetime
  )
  AS
  BEGIN
    BEGIN TRY
      BEGIN TRAN
          DECLARE @Id INT
          SET @Id = (SELECT ISNULL((SELECT MAX(jor_Id) FROM ERP_GMEDINA.rrhh.tbJornadas), 0) + 1)
          INSERT INTO rrhh.tbJornadas 
            (jor_Id, jor_Descripcion, jor_UsuarioCrea, jor_FechaCrea)
            VALUES (@Id, @jor_Descripcion, @jor_UsuarioCrea, @jor_FechaCrea)
          SELECT
          CAST(@Id AS VARCHAR(10)) AS MensajeError
        COMMIT TRAN
    END TRY
    BEGIN CATCH
      ROLLBACK TRAN
    SELECT '-1' + ERROR_MESSAGE() AS MensajeError
    END CATCH
  END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbJornadas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbJornadas_Restore]
	(
		@jor_Id int,

		@jor_UsuarioModifica int,
		@jor_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @jor_Id 
	UPDATE [rrhh].[tbJornadas]
	SET   [jor_Estado]=1,

		  [jor_UsuarioModifica] = @jor_UsuarioModifica,
		  [jor_FechaModifica] = @jor_FechaModifica
	WHERE jor_Id =@jor_Id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbJornadas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbJornadas_Update]
	(
		@jor_Id int,
		@jor_Descripcion nvarchar(30),
		@jor_UsuarioModifica int,
		@jor_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @jor_Id
	UPDATE [rrhh].[tbJornadas]
	SET jor_Descripcion = @jor_Descripcion,
		jor_UsuarioModifica = @jor_UsuarioModifica,
		jor_FechaModifica = @jor_FechaModifica
	WHERE jor_Id  = @jor_Id AND jor_Estado = 1
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbNacionalidades_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbNacionalidades_Delete]
	(
		@nac_id int,
		@nac_razon_Inactivo nvarchar(100),
		@nac_UsuarioModifica int,
		@nac_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @nac_id 
	UPDATE[RRHH].[tbNacionalidades]
	SET   [nac_Estado]=0,
		  [nac_RazonInactivo] = @nac_razon_Inactivo,
		  [nac_UsuarioModifica]= @nac_UsuarioModifica,
		  [nac_FechaModifica]= @nac_FechaModifica
	WHERE nac_Id =@nac_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbNacionalidades_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbNacionalidades_Insert]
	(
		@nac_Descripcion nvarchar(100),
		@nac_UsuarioCrea int,
		@nac_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([nac_Id]) FROM [RRHH].[tbNacionalidades]),0) + 1)
	INSERT INTO [RRHH].[tbNacionalidades](
				[nac_Id],
				[nac_Descripcion],
				[nac_UsuarioCrea],
				[nac_FechaCrea]
	)
	VALUES(
				@Id,
				@nac_Descripcion,
				@nac_UsuarioCrea,
				@nac_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbNacionalidades_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbNacionalidades_Restore]
	(
		@nac_id int,

		@nac_UsuarioModifica int,
		@nac_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @nac_id 
	UPDATE[RRHH].[tbNacionalidades]
	SET   [nac_Estado]=1,

		  [nac_UsuarioModifica]= @nac_UsuarioModifica,
		  [nac_FechaModifica]= @nac_FechaModifica
	WHERE nac_Id =@nac_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbNacionalidades_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbNacionalidades_Update]
	(
		@nac_Id int,
		@nac_Descripcion nvarchar(100),
		@nac_UsuarioModifica int,
		@nac_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @nac_Id
	UPDATE [RRHH].[tbNacionalidades]
	SET nac_Descripcion = @nac_Descripcion,
		nac_UsuarioModifica = @nac_UsuarioModifica,
		nac_FechaModifica = @nac_FechaModifica
	WHERE nac_Id  = @nac_Id AND nac_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbPermisos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbPermisos_Update]
	(
		@tper_Id int,
		@tper_Descripcion nvarchar(100),
		@tper_UsuarioModifica int,
		@tper_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tper_Id
	UPDATE [RRHH].tbTipoPermisos
	SET    [tper_Descripcion] = @tper_Descripcion,
		   [tper_UsuarioModifica] = @tper_UsuarioModifica,
		   [tper_FechaModifica] = @tper_FechaModifica
	WHERE  tper_Id  = @tper_Id  AND tper_Estado = 1
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRazonSalida_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRazonSalida_Update]
	(
		@rsal_Id int,
		@rsal_Descripcion nvarchar(100),
		@rsal_UsuarioModifica int,
		@rsal_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @rsal_Id
	UPDATE [RRHH].tbRazonSalidas
	SET    [rsal_Descripcion] = @rsal_Descripcion,
		   [rsal_UsuarioModifica] = @rsal_UsuarioModifica,
		   [rsal_FechaModifica] = @rsal_FechaModifica
	WHERE  rsal_Id  = @rsal_Id AND rsal_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRazonSalidas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRazonSalidas_Delete]
	(
		@rsal_Id int,
		@rsal_razon_Inactivo nvarchar(100),
		@rsal_UsuarioModifica int,
		@rsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @rsal_Id 
	UPDATE [RRHH].[tbRazonSalidas]
	SET   [rsal_Estado]=0,
		  [rsal_RazonInactivo] = @rsal_razon_Inactivo,
		  [rsal_UsuarioModifica] = @rsal_UsuarioModifica,
		  [rsal_FechaModifica] = @rsal_FechaModifica
	WHERE rsal_Id =@rsal_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRazonSalidas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRazonSalidas_Insert]
	(
		@rsal_Descripcion nvarchar(100),
		@rsal_Usuariocrea int,
		@rsal_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([rsal_Id]) FROM [RRHH].[tbRazonSalidas] ),0) + 1)
	INSERT INTO [RRHH].[tbRazonSalidas](
				[rsal_id], 
				[rsal_descripcion],
				[rsal_usuariocrea], 
				[rsal_fechacrea]
	)
	VALUES(
				@Id,
				@rsal_Descripcion,
				@rsal_Usuariocrea,
				@rsal_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRazonSalidas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRazonSalidas_Restore]
	(
		@rsal_Id int,

		@rsal_UsuarioModifica int,
		@rsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @rsal_Id 
	UPDATE [RRHH].[tbRazonSalidas]
	SET   [rsal_Estado]=1,

		  [rsal_UsuarioModifica] = @rsal_UsuarioModifica,
		  [rsal_FechaModifica] = @rsal_FechaModifica
	WHERE rsal_Id =@rsal_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Delete]
	(
		@resp_id int,
		@resp_razon_Inactivo nvarchar(100),
		@resp_UsuarioModifica int,
		@resp_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @resp_id 
	UPDATE[RRHH].tbRequerimientosEspeciales
	SET   [resp_Estado]=0,
		  [resp_RazonInactivo] = @resp_razon_Inactivo,
		  [resp_UsuarioModifica]= @resp_UsuarioModifica,
		  [resp_FechaModifica]= @resp_FechaModifica
	WHERE resp_Id =@resp_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Insert]
	(
		@resp_Descripcion nvarchar(100),
		@resp_UsuarioCrea  int,
		@resp_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([resp_Id]) FROM [RRHH].[tbRequerimientosEspeciales]),0) + 1)
	INSERT INTO [RRHH].[tbRequerimientosEspeciales](
				[resp_Id],
				[resp_Descripcion],
				[resp_UsuarioCrea],
				[resp_Fechacrea]
	)
	VALUES(
				@Id,
				@resp_Descripcion,
				@resp_UsuarioCrea, 
				@resp_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Restore]
	(
		@resp_id int,
		@resp_UsuarioModifica int,
		@resp_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @resp_id 
	UPDATE[RRHH].tbRequerimientosEspeciales
	SET   [resp_Estado]=1,
		  [resp_RazonInactivo] = NULL,
		  [resp_UsuarioModifica]= @resp_UsuarioModifica,
		  [resp_FechaModifica]= @resp_FechaModifica
	WHERE resp_Id =@resp_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequerimientosEspeciales_Update]
	(
		@resp_Id int,
		@resp_Descripcion nvarchar(100),
		@resp_UsuarioModifica int,
		@resp_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @resp_Id
	UPDATE [RRHH].[tbRequerimientosEspeciales]
	SET   [resp_Descripcion] = @resp_Descripcion,
		  [resp_Usuariomodifica] = @resp_Usuariomodifica,
		  [resp_FechaModifica] = @resp_FechaModifica
	WHERE [resp_Id]  = @resp_Id AND resp_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequisiciones_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequisiciones_Delete]
	(
		@req_id int,
		@req_razonInactivo nvarchar(100),
		@req_UsuarioModifica int,
		@req_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @req_id 
	UPDATE[RRHH].tbRequisiciones
	SET   [req_Estado]=0,
		  [req_RazonInactivo] = @req_razonInactivo,
		  [req_UsuarioModifica]= @req_UsuarioModifica,
		  [req_FechaModifica]= @req_FechaModifica
	WHERE req_Id =@req_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequisiciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequisiciones_Insert]
	(
    @req_Experiencia nvarchar(100),
    @req_Sexo char(10),
    @req_Descripcion nvarchar(50),
    @req_EdadMinima int,
    @req_EdadMaxima int,
    @req_EstadoCivil char(2),
    @req_EducacionSuperior bit,
    @req_Permanente bit,
    @req_Duracion nvarchar(50),
    @req_Vacantes nvarchar(50),
    @req_FechaRequisicion datetime,
    @req_FechaContratacion datetime,
    @req_UsuarioCrea int,
    @req_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([req_Id]) FROM [RRHH].[tbRequisiciones]),0) + 1)
	INSERT INTO [RRHH].[tbRequisiciones](
				req_Id,
        req_Experiencia,
        req_Sexo,
        req_Descripcion,
        req_EdadMinima,
        req_EdadMaxima,
        req_EstadoCivil,
        req_EducacionSuperior,
        req_Permanente,
        req_Duracion,
        req_Vacantes,
        req_FechaRequisicion,
        req_FechaContratacion,
        req_UsuarioCrea,
        req_FechaCrea
	)
	VALUES(
				@Id,
        @req_Experiencia,
        @req_Sexo,
        @req_Descripcion,
        @req_EdadMinima,
        @req_EdadMaxima,
        @req_EstadoCivil,
        @req_EducacionSuperior,
        @req_Permanente,
        @req_Duracion,
        @req_Vacantes,
        @req_FechaRequisicion,
        @req_FechaContratacion,
        @req_UsuarioCrea,
        @req_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequisiciones_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequisiciones_Restore]
	(
		@req_id int,
		@req_UsuarioModifica int,
		@req_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @req_id 
	UPDATE[RRHH].tbRequisiciones
	SET   [req_Estado]=1,
      req_RazonInactivo = '',
		  [req_UsuarioModifica]= @req_UsuarioModifica,
		  [req_FechaModifica]= @req_FechaModifica
	WHERE req_Id =@req_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbRequisiciones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbRequisiciones_Update]
	(
		@req_Id int,
    @req_Experiencia nvarchar(100),
    @req_Sexo char(10),
    @req_Descripcion nvarchar(50),
    @req_EdadMinima int,
    @req_EdadMaxima int,
    @req_EstadoCivil char(2),
    @req_EducacionSuperior bit,
    @req_Permanente bit,
    @req_Duracion nvarchar(50),
    @req_Vacantes nvarchar(50),
    @req_FechaRequisicion datetime,
    @req_FechaContratacion datetime,
    @req_UsuarioModifica int,
    @req_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @req_Id
	UPDATE [RRHH].[tbRequisiciones]
	SET   [req_Descripcion] = @req_Descripcion,
		  [req_Usuariomodifica] = @req_Usuariomodifica,
		  [req_FechaModifica] = @req_FechaModifica
	WHERE [req_Id]  = @req_Id AND req_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Delete]
	(
		@tamo_Id int,
		@tamo_razon_Inactivo nvarchar(100),
		@tamo_UsuarioModifica int,
		@tamo_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tamo_Id 
	UPDATE [RRHH].[tbTipoAmonestaciones]
	SET   [tamo_Estado]=0,
		  [tamo_RazonInactivo] = @tamo_razon_Inactivo,
		  [tamo_UsuarioModifica] = @tamo_UsuarioModifica,
		  [tamo_FechaModifica] =@tamo_FechaModifica
	WHERE tamo_Id =@tamo_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Insert]
	(
		@tamo_Descripcion nvarchar(100),
		@tamo_UsuarioCrea int,
		@tamo_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([tamo_Id]) FROM [RRHH].tbTipoAmonestaciones),0) + 1)
	INSERT INTO [RRHH].tbTipoAmonestaciones(
				[tamo_Id],
				[tamo_Descripcion],
				[tamo_UsuarioCrea],
				tamo_FechaCrea
	)
	VALUES(
				@Id,
				@tamo_Descripcion,
				@tamo_UsuarioCrea,
				@tamo_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Restore]
	(
		@tamo_Id int,

		@tamo_UsuarioModifica int,
		@tamo_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tamo_Id 
	UPDATE [RRHH].[tbTipoAmonestaciones]
	SET   [tamo_Estado]=1,

		  [tamo_UsuarioModifica] = @tamo_UsuarioModifica,
		  [tamo_FechaModifica] =@tamo_FechaModifica
	WHERE tamo_Id =@tamo_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoAmonestaciones_Update]
	(
		@tamo_Id int,
		@tamo_Descripcion nvarchar(100),
		@tamo_UsuarioModifica int,
		@tamo_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tamo_Id
	UPDATE [RRHH].tbTipoAmonestaciones
	SET    [tamo_Descripcion] = @tamo_Descripcion,
		   [tamo_UsuarioModifica] = @tamo_UsuarioModifica,
		   [tamo_FechaModifica] = @tamo_FechaModifica
	WHERE  tamo_Id  = @tamo_Id AND tamo_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoHora_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoHora_Update]
	(
		@tiho_Id int,
		@tiho_Descripcion nvarchar(100),
		@tiho_Recargo int,
		@tiho_UsuarioModifica int,
		@tiho_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tiho_Id
	UPDATE [RRHH].tbTipoHoras
	SET    [tiho_Descripcion] = @tiho_Descripcion,
		   [tiho_Recargo] = @tiho_Recargo,
		   [tiho_UsuarioModifica] = @tiho_UsuarioModifica,
		   [tiho_FechaModifica] = @tiho_FechaModifica
	WHERE  tiho_Id  = @tiho_Id  AND tiho_Estado = 1
	SELECT CAST(@Id AS VARCHAR) AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoHoras_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoHoras_Delete]
	(
		@tiho_Id int,
		@tiho_razon_Inactivo nvarchar(100),
		@tiho_UsuarioModifica int,
		@tiho_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tiho_id 
	UPDATE [RRHH].[tbTipoHoras]
	SET	  [tiho_Estado]=0,
		  [tiho_RazonInactivo] = @tiho_razon_Inactivo,
		  [tiho_UsuarioModifica]= @tiho_UsuarioModifica,
		  [tiho_FechaModifica]= @tiho_FechaModifica
	WHERE tiho_id =@tiho_id
	SELECT CAST(@Id AS VARCHAR) AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoHoras_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoHoras_Insert]
(
@tiho_Descripcion nvarchar(100),
@tiho_Recargo int,
@tiho_UsuarioCrea int,
@tiho_FechaCrea datetime
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = (SELECT ISNULL((SELECT MAX([tiho_Id]) FROM [RRHH].[tbTipoHoras]),0) + 1)
INSERT INTO [RRHH].[tbTipoHoras](
tiho_Id, 
tiho_Descripcion,
tiho_Recargo,  
tiho_UsuarioCrea, 
tiho_FechaCrea
)
VALUES(
@Id,
@tiho_Descripcion,
@tiho_Recargo,
@tiho_UsuarioCrea,
@tiho_FechaCrea 
)
SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoHoras_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoHoras_Restore]
	(
		@tiho_Id int,

		@tiho_UsuarioModifica int,
		@tiho_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tiho_id 
	UPDATE [RRHH].[tbTipoHoras]
	SET	  [tiho_Estado]=1,

		  [tiho_UsuarioModifica]= @tiho_UsuarioModifica,
		  [tiho_FechaModifica]= @tiho_FechaModifica
	WHERE tiho_id =@tiho_id
	SELECT CAST(@Id AS VARCHAR) AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoHoras_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoHoras_Select]
(
@tiho_Id int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = @tiho_Id
SELECT 
tiho_Id, 
tiho_Descripcion,
tiho_Recargo
FROM
[RRHH].[tbTipoHoras]
WHERE tiho_Id=@tiho_Id

SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoIncapacidades_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoIncapacidades_Delete]
	(
		@ticn_Id int,
		@ticn_razon_Inactivo nvarchar(100),
		@ticn_UsuarioModifica int,
		@ticn_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @ticn_Id 
	UPDATE[RRHH].[tbTipoIncapacidades]
	SET   [ticn_Estado]=0,
		  [ticn_RazonInactivo] = @ticn_razon_Inactivo,
		  [ticn_UsuarioModifica] =@ticn_UsuarioModifica,
		  [ticn_FechaModifica]= @ticn_FechaModifica
	WHERE ticn_Id =@ticn_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoIncapacidades_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoIncapacidades_Insert]
	(
		@ticn_Descripcion nvarchar(100),
		@ticn_Usuariocrea int,
		@ticn_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([ticn_Id]) FROM [RRHH].[tbTipoIncapacidades] ),0) + 1)
	INSERT INTO [RRHH].[tbTipoIncapacidades](
				[ticn_Id],
				[ticn_Descripcion],
				[ticn_Usuariocrea],
				[ticn_Fechacrea]
	)
	VALUES(
				@Id,
				@ticn_Descripcion,
				@ticn_Usuariocrea,
				@ticn_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoIncapacidades_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoIncapacidades_Restore]
	(
		@ticn_Id int,

		@ticn_UsuarioModifica int,
		@ticn_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @ticn_Id 
	UPDATE[RRHH].[tbTipoIncapacidades]
	SET   [ticn_Estado]=1,

		  [ticn_UsuarioModifica] =@ticn_UsuarioModifica,
		  [ticn_FechaModifica]= @ticn_FechaModifica
	WHERE ticn_Id =@ticn_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoIncapacidades_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoIncapacidades_Update]
	(
		@ticn_Id int,
		@ticn_Descripcion nvarchar(100),
		@ticn_UsuarioModifica int,
		@ticn_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @ticn_Id 
	UPDATE [RRHH].tbTipoIncapacidades
	SET    [ticn_Descripcion] = @ticn_Descripcion,
		   [ticn_UsuarioModifica] = @ticn_UsuarioModifica,
		   [ticn_FechaModifica] = @ticn_FechaModifica
	WHERE ticn_Id  = @ticn_Id AND ticn_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoMoneda_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoMoneda_Update]
	(
		@tmon_Id int,
		@tmon_Descripcion nvarchar(100),
		@tmon_UsuarioModifica int,
		@tmon_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tmon_Id
	UPDATE [RRHH].tbTipoMonedas
	SET    [tmon_Descripcion] = @tmon_Descripcion,
		   [tmon_UsuarioModifica] = @tmon_UsuarioModifica,
		   [tmon_FechaModifica] = @tmon_FechaModifica
	WHERE  tmon_Id  = @tmon_Id  AND tmon_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoMonedas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoMonedas_Delete]
	(
		@tmon_Id int,
		@tmon_razon_Inactivo nvarchar(100),
		@tmon_UsuarioModifica int,
		@tmon_FechaModifica datetime
	)
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tmon_id 
	UPDATE[RRHH].[tbTipoMonedas]
	SET   [tmon_Estado]=0,
		  [tmon_RazonInactivo] = @tmon_razon_Inactivo,
		  [tmon_UsuarioModifica]= @tmon_UsuarioModifica,
		  [tmon_FechaModifica]= @tmon_FechaModifica
	WHERE tmon_id =@tmon_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoMonedas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoMonedas_Insert]
	(
		@tmon_Descripcion nvarchar(100),
		@tmon_UsuarioCrea int,
		@tmon_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([tmon_Id]) FROM [RRHH].tbTipoMonedas),0) + 1)
	INSERT INTO [RRHH].tbTipoMonedas(
				[tmon_Id],
				[tmon_Descripcion],
				[tmon_UsuarioCrea],
				[tmon_FechaCrea]
	)
	VALUES(
				@Id,
				@tmon_Descripcion,
				@tmon_UsuarioCrea,
				@tmon_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoMonedas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoMonedas_Restore]
	(
		@tmon_Id int,

		@tmon_UsuarioModifica int,
		@tmon_FechaModifica datetime
	)
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tmon_id 
	UPDATE[RRHH].[tbTipoMonedas]
	SET   [tmon_Estado]=1,

		  [tmon_UsuarioModifica]= @tmon_UsuarioModifica,
		  [tmon_FechaModifica]= @tmon_FechaModifica
	WHERE tmon_id =@tmon_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoMonedas_Select]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoMonedas_Select]
(
@tmon_Id int
) AS
set nocount on;
BEGIN
BEGIN TRY
BEGIN TRAN
declare @Id int
set @Id = @tmon_Id
SELECT 
[tmon_Descripcion], 
[tmon_Estado],
[tmon_RazonInactivo]
FROM
[ERP_GMEDINA].[RRHH].[tbTipoMonedas]
WHERE [tmon_Id]=@tmon_Id AND [tmon_Estado] = 1

SELECT CAST(@Id AS VARCHAR) AS MensajeError
COMMIT TRAN
END TRY
BEGIN CATCH
ROLLBACK TRAN
SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
END CATCH
END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoPermisos_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoPermisos_Delete]
	(
		@tper_Id int,
		@tper_razon_Inactivo nvarchar(100),
		@tper_UsuarioModifica int,
		@tper_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tper_Id 
	UPDATE[RRHH].[tbTipoPermisos]
	SET   [tper_Estado]=0,
		  [tper_RazonInactivo] = @tper_razon_Inactivo,
		  [tper_UsuarioModifica]= @tper_UsuarioModifica
	WHERE tper_Id =@tper_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoPermisos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoPermisos_Insert]
	(
		@tper_Descripcion nvarchar(100),
		@tper_UsuarioCrea int,
		@tper_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([tper_Id]) FROM [RRHH].tbTipoPermisos),0) + 1)
	INSERT INTO [RRHH].tbTipoPermisos(
				[tper_Id],
				[tper_Descripcion],
				[tper_UsuarioCrea],
				tper_FechaCrea
	)
	VALUES(
				@Id,
				@tper_Descripcion,
				@tper_UsuarioCrea,
				@tper_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoPermisos_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoPermisos_Restore]
	(
		@tper_Id int,

		@tper_UsuarioModifica int,
		@tper_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tper_Id 
	UPDATE[RRHH].[tbTipoPermisos]
	SET   [tper_Estado]=1,

		  [tper_UsuarioModifica]= @tper_UsuarioModifica
	WHERE tper_Id =@tper_Id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoPermisos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [rrhh].[UDP_RRHH_tbTipoPermisos_Update]
	(
		@tper_Id int,
		@tper_Descripcion nvarchar(100),
		@tper_UsuarioModifica int,
		@tper_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tper_Id
	UPDATE [RRHH].tbTipoPermisos
	SET    [tper_Descripcion] = @tper_Descripcion,
		   [tper_UsuarioModifica] = @tper_UsuarioModifica,
		   [tper_FechaModifica] = @tper_FechaModifica
	WHERE  tper_Id  = @tper_Id  AND tper_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoSalidas_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbTipoSalidas_Delete]
	(
		@tsal_id int,
		@tsal_razon_Inactivo nvarchar(100),
		@tsal_UsuarioModifica int,
		@tsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tsal_id 
	UPDATE [RRHH].[tbTipoSalidas]
	SET   [tsal_Estado]=0,
		  [tsal_RazonInactivo] = @tsal_razon_Inactivo,
		  [tsal_UsuarioModifica]= @tsal_UsuarioModifica,
		  [tsal_FechaModifica]= @tsal_FechaModifica
	WHERE tsal_Id =@tsal_id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoSalidas_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbTipoSalidas_Insert]
	(
		@tsal_Descripcion nvarchar(100),
		@tsal_UsuarioCrea int,
		@tsal_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([tsal_Id]) FROM [RRHH].[tbTipoSalidas]),0) + 1)
	INSERT INTO [RRHH].[tbTipoSalidas](
				[tsal_Id],
				[tsal_Descripcion],
				[tsal_UsuarioCrea],
				[tsal_FechaCrea]
	)
	VALUES(
				@Id,
				@tsal_Descripcion,
				@tsal_UsuarioCrea,
				@tsal_FechaCrea
	)
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoSalidas_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTipoSalidas_Restore]
	(
		@tsal_id int,

		@tsal_UsuarioModifica int,
		@tsal_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tsal_id 
	UPDATE [RRHH].[tbTipoSalidas]
	SET   [tsal_Estado]=1,

		  [tsal_UsuarioModifica]= @tsal_UsuarioModifica,
		  [tsal_FechaModifica]= @tsal_FechaModifica
	WHERE tsal_Id =@tsal_id
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTipoSalidas_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [rrhh].[UDP_RRHH_tbTipoSalidas_Update]
	(
		@tsal_Id int,
		@tsal_Descripcion nvarchar(100),
		@tsal_UsuarioModifica int,
		@tsal_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @tsal_Id
	UPDATE [RRHH].[tbTipoSalidas]
	SET tsal_Descripcion = @tsal_Descripcion,
		tsal_UsuarioModifica = @tsal_UsuarioModifica,
		tsal_FechaModifica = @tsal_FechaModifica
	WHERE tsal_Id  = @tsal_Id AND tsal_Estado = 1
	SELECT @Id AS MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTitulos_Delete]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTitulos_Delete]
	(
		@titu_id int,
		@titu_razon_Inactivo nvarchar(100),
		@titu_UsuarioModifica int,
		@titu_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @titu_id 
	UPDATE[RRHH].tbTitulos
	SET   [titu_Estado]=0,
		  [titu_RazonInactivo] = @titu_razon_Inactivo,
		  [titu_UsuarioModifica]= @titu_UsuarioModifica,
		  [titu_FechaModifica]= @titu_FechaModifica
	WHERE titu_Id =@titu_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTitulos_Insert]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTitulos_Insert]
	(
		@titu_Descripcion nvarchar(100),
		@titu_UsuarioCrea  int,
		@titu_FechaCrea datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = (SELECT ISNULL((SELECT MAX([titu_Id]) FROM [RRHH].[tbTitulos]),0) + 1)
	INSERT INTO [RRHH].[tbTitulos](
				[titu_Id],
				[titu_Descripcion],
				[titu_UsuarioCrea],
				[titu_Fechacrea]
	)
	VALUES(
				@Id,
				@titu_Descripcion,
				@titu_UsuarioCrea, 
				@titu_FechaCrea
	)
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTitulos_Restore]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTitulos_Restore]
	(
		@titu_id int,

		@titu_UsuarioModifica int,
		@titu_FechaModifica datetime
	) 
	AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @titu_id 
	UPDATE[RRHH].tbTitulos
	SET   [titu_Estado]=1,

		  [titu_UsuarioModifica]= @titu_UsuarioModifica,
		  [titu_FechaModifica]= @titu_FechaModifica
	WHERE titu_Id =@titu_id
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
/****** Object:  StoredProcedure [rrhh].[UDP_RRHH_tbTitulos_Update]    Script Date: 09/12/2019 16:31:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [rrhh].[UDP_RRHH_tbTitulos_Update]
	(
		@titu_Id int,
		@titu_Descripcion nvarchar(100),
		@titu_UsuarioModifica int,
		@titu_FechaModifica datetime
	) AS
	set nocount on;
	BEGIN
	BEGIN TRY
	BEGIN TRAN
	declare @Id int
	set @Id = @titu_Id
	UPDATE [RRHH].[tbTitulos]
	SET   [titu_Descripcion] = @titu_Descripcion,
		  [titu_Usuariomodifica] = @titu_Usuariomodifica,
		  [titu_FechaModifica] = @titu_FechaModifica
	WHERE [titu_Id]  = @titu_Id AND titu_Estado = 1
	SELECT CAST(@Id AS VARCHAR(10)) AS  MensajeError
	COMMIT TRAN
	END TRY
	BEGIN CATCH
	ROLLBACK TRAN
	SELECT '-1 ' + ERROR_MESSAGE() AS MensajeError
	END CATCH
	END
GO
USE [master]
GO
ALTER DATABASE [ERP_GMEDINA] SET  READ_WRITE 
GO
