USE [MySGOO]
GO
SET IDENTITY_INSERT [dbo].[TipoOrdens] ON 

INSERT [dbo].[TipoOrdens] ([Id], [Nome]) VALUES (1, N'Estocagem')
INSERT [dbo].[TipoOrdens] ([Id], [Nome]) VALUES (2, N'Carregamento')
INSERT [dbo].[TipoOrdens] ([Id], [Nome]) VALUES (3, N'Descarga')
INSERT [dbo].[TipoOrdens] ([Id], [Nome]) VALUES (4, N'Embarque')
SET IDENTITY_INSERT [dbo].[TipoOrdens] OFF
GO
SET IDENTITY_INSERT [dbo].[Ordens] ON 

INSERT [dbo].[Ordens] ([Id], [Nome], [TipoOrdemId]) VALUES (1, N'ORDEM 1', 1)
INSERT [dbo].[Ordens] ([Id], [Nome], [TipoOrdemId]) VALUES (2, N'ORDEM 2', 2)
SET IDENTITY_INSERT [dbo].[Ordens] OFF
GO
SET IDENTITY_INSERT [dbo].[Equipamentos] ON 

INSERT [dbo].[Equipamentos] ([Id], [Nome]) VALUES (1, N'EP151K04')
INSERT [dbo].[Equipamentos] ([Id], [Nome]) VALUES (2, N'ER151K01')
INSERT [dbo].[Equipamentos] ([Id], [Nome]) VALUES (3, N'RP152K03')
SET IDENTITY_INSERT [dbo].[Equipamentos] OFF
GO
