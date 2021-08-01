USE [testTecnico]
GO
/****** Object:  Table [dbo].[Adelantos]    Script Date: 01/08/2021 15:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adelantos](
	[idAdelanto] [varchar](10) NOT NULL,
	[idEmpleado] [int] NULL,
	[monto] [float] NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaCancelacion] [datetime] NULL,
	[cancelado] [bit] NOT NULL,
 CONSTRAINT [PK_Adelantos] PRIMARY KEY CLUSTERED 
(
	[idAdelanto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 01/08/2021 15:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[legajo] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[dni] [varchar](20) NULL,
	[sueldo] [float] NULL,
	[idTipoEmpleado] [int] NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[legajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 01/08/2021 15:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[idPago] [int] IDENTITY(1,1) NOT NULL,
	[idAdelanto] [varchar](10) NOT NULL,
	[montoPago] [float] NOT NULL,
	[fechaPago] [datetime] NOT NULL,
 CONSTRAINT [PK_Pagos] PRIMARY KEY CLUSTERED 
(
	[idPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoEmpleados]    Script Date: 01/08/2021 15:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoEmpleados](
	[idTipoEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[porcentajeAdelanto] [float] NULL,
	[maximoAdelanto] [int] NULL,
 CONSTRAINT [PK_TipoEmpleados] PRIMARY KEY CLUSTERED 
(
	[idTipoEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Adelantos] ADD  CONSTRAINT [DF_Adelantos_fechaAlta]  DEFAULT (getdate()) FOR [fechaAlta]
GO
ALTER TABLE [dbo].[Adelantos] ADD  CONSTRAINT [DF_Adelantos_cancelado]  DEFAULT ((0)) FOR [cancelado]
GO
ALTER TABLE [dbo].[Pagos] ADD  CONSTRAINT [DF_Pagos_fechaPago]  DEFAULT (getdate()) FOR [fechaPago]
GO
ALTER TABLE [dbo].[Adelantos]  WITH CHECK ADD  CONSTRAINT [FK_Adelantos_Empleados] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[Empleados] ([legajo])
GO
ALTER TABLE [dbo].[Adelantos] CHECK CONSTRAINT [FK_Adelantos_Empleados]
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD  CONSTRAINT [FK_Empleados_TipoEmpleados] FOREIGN KEY([idTipoEmpleado])
REFERENCES [dbo].[TipoEmpleados] ([idTipoEmpleado])
GO
ALTER TABLE [dbo].[Empleados] CHECK CONSTRAINT [FK_Empleados_TipoEmpleados]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Adelantos] FOREIGN KEY([idAdelanto])
REFERENCES [dbo].[Adelantos] ([idAdelanto])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Adelantos]
GO
