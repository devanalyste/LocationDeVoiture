CREATE DATABASE MultiLocations
GO
USE [MultiLocations]
GO
/****** Object:  Table [dbo].[Couleur]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Couleur](
	[idCouleur] [int] IDENTITY(1,1) NOT NULL,
	[couleur] [varchar](50) NULL,
 CONSTRAINT [PK_Couleur] PRIMARY KEY CLUSTERED 
(
	[idCouleur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Couleur] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Modele]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modele](
	[idModel] [int] IDENTITY(1,1) NOT NULL,
	[model] [nchar](15) NULL,
 CONSTRAINT [PK_Modele] PRIMARY KEY CLUSTERED 
(
	[idModel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Modele] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Vehicule]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicule](
	[niv] [varchar](50) NOT NULL,
	[annee] [int] NULL,
	[valeur] [decimal](19, 2) NULL,
	[transmission] [char](11) NULL,
	[climatiseur] [bit] NULL,
	[antiDemarreur] [bit] NULL,
	[idCouleur] [int] NULL,
	[idModel] [int] NULL,
	[idType] [int] NULL,
 CONSTRAINT [PK_Vehicule] PRIMARY KEY CLUSTERED 
(
	[niv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Vehicule] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Type]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[idType] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[idType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Type] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Location]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[idLocation] [int] IDENTITY(1,1) NOT NULL,
	[debutLoc] [datetime] NULL,
	[contratNbrMois] [int] NULL,
	[finContrat]  AS (dateadd(month,[contratNbrMois],[debutLoc])),
	[premPaiement] [datetime] NULL,
	[qte.paie/annee] [int] NULL,
	[mntPaieMens] [decimal](19, 2) NULL,
	[nbrKmDebut] [decimal](19, 2) NULL,
	[nbrKmFin] [decimal](18, 2) NULL,
	[idClient] [int] NULL,
	[niv] [varchar](50) NULL,
	[idTerme] [int] NULL,
	[idPaiement] [int] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[idLocation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Location] TO  SCHEMA OWNER 
GO
/****** Object:  View [dbo].[PasEnLocation]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW  [dbo].[PasEnLocation]  AS
SELECT V.niv AS 'Niv véhicule',
	V.annee,
	V.valeur,
	V.transmission,
	V.climatiseur,
	V.antiDemarreur,
	M.model,
	T.type,
	C.couleur,
	L.niv AS 'Niv en location'
FROM dbo.Vehicule AS V
	INNER JOIN dbo.Type AS T ON T.idType=V.idType
	INNER JOIN dbo.Couleur AS C ON C.idCouleur=V.idCouleur
	LEFT JOIN dbo.Location AS L ON L.niv=V.niv
	INNER JOIN dbo.Modele AS M ON M.idModel=V.idModel
	WHERE L.niv IS NULL
GO
ALTER AUTHORIZATION ON [dbo].[PasEnLocation] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Audit]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[idAudit] [int] IDENTITY(1,1) NOT NULL,
	[dateChangement] [datetime] NULL,
	[idLocation] [int] NULL,
	[debutLoc] [date] NULL,
	[contratNbrMois] [int] NULL,
	[finContrat] [date] NULL,
	[premPaiement] [date] NULL,
	[mntPaieMens] [decimal](5, 2) NULL,
	[nbrKmDebut] [decimal](18, 0) NULL,
	[nbrKmFin] [decimal](18, 0) NULL,
	[niv] [nchar](23) NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[idAudit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Audit] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Client]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[idClient] [int] IDENTITY(1,1) NOT NULL,
	[prenom] [varchar](50) NULL,
	[nom] [varchar](50) NULL,
	[adresse] [varchar](100) NULL,
	[ville] [varchar](50) NULL,
	[province] [char](3) NULL,
	[codePostal] [char](7) NULL,
	[telephone] [numeric](18, 0) NULL,
	[date_naissance] [date] NULL,
	[idLocation] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[idClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Client] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Paiement]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paiement](
	[idPaiement] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[montant] [decimal](6, 2) NULL,
	[idLocation] [int] NULL,
	[idClient] [int] NULL,
 CONSTRAINT [PK_Paiement] PRIMARY KEY CLUSTERED 
(
	[idPaiement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Paiement] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[TermesLocation]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermesLocation](
	[idTerme] [int] IDENTITY(1,1) NOT NULL,
	[nbAnnee] [int] NULL,
	[kmMax] [decimal](18, 0) NULL,
	[surprime] [decimal](2, 2) NULL,
	[idLocation] [int] NULL,
	[idPaiement] [int] NULL,
 CONSTRAINT [PK_TermesLocation] PRIMARY KEY CLUSTERED 
(
	[idTerme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[TermesLocation] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Utilisateurs]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateurs](
	[idUtilisateur] [char](5) NOT NULL,
	[dateConnexion] [datetime] NULL,
	[Prenom] [varchar](20) NULL,
	[Nom] [varchar](20) NULL,
	[Courriel] [varchar](50) NOT NULL,
	[MotPasse] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED 
(
	[idUtilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER AUTHORIZATION ON [dbo].[Utilisateurs] TO  SCHEMA OWNER 
GO
SET IDENTITY_INSERT [dbo].[Audit] ON 
GO
SET IDENTITY_INSERT [dbo].[Client] ON 
GO
INSERT [dbo].[Client] ([idClient], [prenom], [nom], [adresse], [ville], [province], [codePostal], [telephone], [date_naissance], [idLocation]) VALUES (1, N'Claudine', N'Latreille', N'1000 chemin des Pruches', N'St-Clinclin', N'QC ', N'X1X 1X1', CAST(4501236567 AS Numeric(18, 0)), CAST(N'1984-11-07' AS Date), NULL)
GO
INSERT [dbo].[Client] ([idClient], [prenom], [nom], [adresse], [ville], [province], [codePostal], [telephone], [date_naissance], [idLocation]) VALUES (2, N'Armand', N'Guindon', N'728 rue Vorien', N'St Benit', N'QC ', N'G1X 1H1', CAST(4504768276 AS Numeric(18, 0)), CAST(N'1968-05-30' AS Date), NULL)
GO
INSERT [dbo].[Client] ([idClient], [prenom], [nom], [adresse], [ville], [province], [codePostal], [telephone], [date_naissance], [idLocation]) VALUES (3, N'Pierre', N'Monfils', N'248 Garneau', N'Tenaga', N'QC ', N'J6C 8B2', CAST(8193378219 AS Numeric(18, 0)), CAST(N'1980-11-10' AS Date), NULL)
GO
INSERT [dbo].[Client] ([idClient], [prenom], [nom], [adresse], [ville], [province], [codePostal], [telephone], [date_naissance], [idLocation]) VALUES (4, N'Alfred', N'Léon', N'1269 chemin des Anges', N'St-Clinclin', N'QC ', N'X1X 1X1', CAST(4508871690 AS Numeric(18, 0)), CAST(N'1992-02-03' AS Date), NULL)
GO
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Couleur] ON 
GO
INSERT [dbo].[Couleur] ([idCouleur], [couleur]) VALUES (1, N'Bleu foncé')
GO
INSERT [dbo].[Couleur] ([idCouleur], [couleur]) VALUES (2, N'Rouge vin')
GO
INSERT [dbo].[Couleur] ([idCouleur], [couleur]) VALUES (3, N'Jaune citron')
GO
INSERT [dbo].[Couleur] ([idCouleur], [couleur]) VALUES (4, N'Vert lime')
GO
INSERT [dbo].[Couleur] ([idCouleur], [couleur]) VALUES (5, N'Gris argenté')
GO
SET IDENTITY_INSERT [dbo].[Couleur] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([idLocation], [debutLoc], [contratNbrMois], [premPaiement], [qte.paie/annee], [mntPaieMens], [nbrKmDebut], [nbrKmFin], [idClient], [niv], [idTerme], [idPaiement]) VALUES (1, CAST(N'2004-01-12T00:00:00.000' AS DateTime), 48, CAST(N'2004-02-12T00:00:00.000' AS DateTime), 25, CAST(150.00 AS Decimal(19, 2)), CAST(100.00 AS Decimal(19, 2)), NULL, 3, N'3W9T1-2Q10D-12D0P-2E1R2', 3, NULL)
GO
INSERT [dbo].[Location] ([idLocation], [debutLoc], [contratNbrMois], [premPaiement], [qte.paie/annee], [mntPaieMens], [nbrKmDebut], [nbrKmFin], [idClient], [niv], [idTerme], [idPaiement]) VALUES (2, CAST(N'2012-02-28T00:00:00.000' AS DateTime), 48, CAST(N'2012-02-28T00:00:00.000' AS DateTime), 25, CAST(270.00 AS Decimal(19, 2)), CAST(10.00 AS Decimal(19, 2)), NULL, 2, N'7D901-9W120-Z0029-021P2', 1, NULL)
GO
INSERT [dbo].[Location] ([idLocation], [debutLoc], [contratNbrMois], [premPaiement], [qte.paie/annee], [mntPaieMens], [nbrKmDebut], [nbrKmFin], [idClient], [niv], [idTerme], [idPaiement]) VALUES (4, CAST(N'2018-01-12T00:00:00.000' AS DateTime), 48, CAST(N'2004-02-12T00:00:00.000' AS DateTime), 25, CAST(350.00 AS Decimal(19, 2)), CAST(10.00 AS Decimal(19, 2)), NULL, 4, N'L219M-K129P-V12BP-210G3', 5, NULL)
GO
INSERT [dbo].[Location] ([idLocation], [debutLoc], [contratNbrMois], [premPaiement], [qte.paie/annee], [mntPaieMens], [nbrKmDebut], [nbrKmFin], [idClient], [niv], [idTerme], [idPaiement]) VALUES (5, CAST(N'2022-01-12T00:00:00.000' AS DateTime), 12, CAST(N'2004-02-12T00:00:00.000' AS DateTime), 12, CAST(750.00 AS Decimal(19, 2)), CAST(0.00 AS Decimal(19, 2)), NULL, 1, N'M21L1-3129S-V1292-LI2X1', 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Modele] ON 
GO
INSERT [dbo].[Modele] ([idModel], [model]) VALUES (1, N'SC 430         ')
GO
INSERT [dbo].[Modele] ([idModel], [model]) VALUES (2, N'Pirate         ')
GO
INSERT [dbo].[Modele] ([idModel], [model]) VALUES (3, N'Rainier        ')
GO
INSERT [dbo].[Modele] ([idModel], [model]) VALUES (4, N'Rock           ')
GO
INSERT [dbo].[Modele] ([idModel], [model]) VALUES (5, N'Speedy         ')
GO
SET IDENTITY_INSERT [dbo].[Modele] OFF
GO
SET IDENTITY_INSERT [dbo].[Paiement] ON 
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (1, CAST(N'2004-02-15T00:00:00.000' AS DateTime), CAST(650.00 AS Decimal(6, 2)), 1, 1)
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (2, CAST(N'2004-03-15T00:00:00.000' AS DateTime), CAST(650.00 AS Decimal(6, 2)), 1, 1)
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (3, CAST(N'2004-04-16T00:00:00.000' AS DateTime), CAST(350.00 AS Decimal(6, 2)), 2, 2)
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (4, CAST(N'2004-05-15T00:00:00.000' AS DateTime), CAST(600.00 AS Decimal(6, 2)), 3, 3)
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (5, CAST(N'2005-02-15T00:00:00.000' AS DateTime), CAST(300.00 AS Decimal(6, 2)), 4, 4)
GO
INSERT [dbo].[Paiement] ([idPaiement], [date], [montant], [idLocation], [idClient]) VALUES (6, CAST(N'2005-03-15T00:00:00.000' AS DateTime), CAST(300.00 AS Decimal(6, 2)), 4, 4)
GO
SET IDENTITY_INSERT [dbo].[Paiement] OFF
GO
SET IDENTITY_INSERT [dbo].[TermesLocation] ON 
GO
INSERT [dbo].[TermesLocation] ([idTerme], [nbAnnee], [kmMax], [surprime], [idLocation], [idPaiement]) VALUES (1, 3, CAST(120 AS Decimal(18, 0)), CAST(0.25 AS Decimal(2, 2)), 2, 1)
GO
INSERT [dbo].[TermesLocation] ([idTerme], [nbAnnee], [kmMax], [surprime], [idLocation], [idPaiement]) VALUES (2, 1, CAST(85 AS Decimal(18, 0)), CAST(0.20 AS Decimal(2, 2)), 5, 3)
GO
INSERT [dbo].[TermesLocation] ([idTerme], [nbAnnee], [kmMax], [surprime], [idLocation], [idPaiement]) VALUES (3, 2, CAST(150 AS Decimal(18, 0)), CAST(0.20 AS Decimal(2, 2)), 1, 3)
GO
INSERT [dbo].[TermesLocation] ([idTerme], [nbAnnee], [kmMax], [surprime], [idLocation], [idPaiement]) VALUES (4, 4, CAST(130 AS Decimal(18, 0)), CAST(0.15 AS Decimal(2, 2)), 2, 4)
GO
INSERT [dbo].[TermesLocation] ([idTerme], [nbAnnee], [kmMax], [surprime], [idLocation], [idPaiement]) VALUES (5, 1, CAST(150 AS Decimal(18, 0)), CAST(0.35 AS Decimal(2, 2)), 4, 3)
GO
SET IDENTITY_INSERT [dbo].[TermesLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 
GO
INSERT [dbo].[Type] ([idType], [type]) VALUES (1, N'Coupé 2 portes')
GO
INSERT [dbo].[Type] ([idType], [type]) VALUES (2, N'Sedan 4 portes')
GO
INSERT [dbo].[Type] ([idType], [type]) VALUES (3, N'Camion')
GO
INSERT [dbo].[Type] ([idType], [type]) VALUES (4, N'VUS')
GO
INSERT [dbo].[Type] ([idType], [type]) VALUES (5, N'Van')
GO
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
INSERT [dbo].[Utilisateurs] ([idUtilisateur], [dateConnexion], [Prenom], [Nom], [Courriel], [MotPasse]) VALUES (N'1    ', CAST(N'2022-10-05T07:47:49.700' AS DateTime), N'Nadine', N'Turpin', N'nadineturpin29@gmail.com', N'1234allo')
GO
INSERT [dbo].[Utilisateurs] ([idUtilisateur], [dateConnexion], [Prenom], [Nom], [Courriel], [MotPasse]) VALUES (N'2    ', CAST(N'2022-10-05T07:47:49.700' AS DateTime), N'Admin', N'Admin', N'admin@gmail.com', N'Admin1234')
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'3W9T1-2Q10D-12D0P-2E1R2', 2003, CAST(90.00 AS Decimal(19, 2)), N'automatique', 0, 0, 4, 1, 4)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'4W9w1-2Q10D-12D0P-2E1R9', 2019, CAST(18000.00 AS Decimal(19, 2)), N'manuelle   ', 1, 1, 3, 4, 3)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'7D901-9W120-Z0029-021P2', 2003, CAST(45.00 AS Decimal(19, 2)), N'manuelle   ', 1, 0, 2, 2, 3)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'K219M-K129P-V12BP-210G4', 2022, CAST(185.00 AS Decimal(19, 2)), N'manuelle   ', 1, 1, 3, 4, 1)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'L219M-K129P-V12BP-210G3', 2021, CAST(105.00 AS Decimal(19, 2)), N'manuelle   ', 1, 1, 1, 1, 1)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'M21L1-3129S-V1292-LI2X1', 2003, CAST(85.00 AS Decimal(19, 2)), N'manuelle   ', 1, 0, 5, 5, 2)
GO
INSERT [dbo].[Vehicule] ([niv], [annee], [valeur], [transmission], [climatiseur], [antiDemarreur], [idCouleur], [idModel], [idType]) VALUES (N'Z1221-X129A-KO212-9021J', 2003, CAST(70.00 AS Decimal(19, 2)), N'automatique', 0, 0, 5, 3, 5)
GO
/****** Object:  Index [IX_TelephoneClient]    Script Date: 2022-11-02 17:21:00 ******/
CREATE NONCLUSTERED INDEX [IX_TelephoneClient] ON [dbo].[Client]
(
	[telephone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_Constraint]    Script Date: 2022-11-02 17:21:00 ******/
ALTER TABLE [dbo].[Utilisateurs] ADD  CONSTRAINT [UX_Constraint] UNIQUE NONCLUSTERED 
(
	[Courriel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_Check_Unique_Niv]    Script Date: 2022-11-02 17:21:00 ******/
ALTER TABLE [dbo].[Vehicule] ADD  CONSTRAINT [UX_Check_Unique_Niv] UNIQUE NONCLUSTERED 
(
	[niv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF_date_getDate]  DEFAULT (getdate()) FOR [debutLoc]
GO
ALTER TABLE [dbo].[Utilisateurs] ADD  CONSTRAINT [DF_Utilisateur_dateConnexion]  DEFAULT (getdate()) FOR [dateConnexion]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Client] FOREIGN KEY([idClient])
REFERENCES [dbo].[Client] ([idClient])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Client]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Paiement] FOREIGN KEY([idPaiement])
REFERENCES [dbo].[Paiement] ([idPaiement])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Paiement]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_TermesLocation] FOREIGN KEY([idTerme])
REFERENCES [dbo].[TermesLocation] ([idTerme])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_TermesLocation]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Vehicule] FOREIGN KEY([niv])
REFERENCES [dbo].[Vehicule] ([niv])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Vehicule]
GO
ALTER TABLE [dbo].[Paiement]  WITH CHECK ADD  CONSTRAINT [FK_Paiement_Client] FOREIGN KEY([idClient])
REFERENCES [dbo].[Client] ([idClient])
GO
ALTER TABLE [dbo].[Paiement] CHECK CONSTRAINT [FK_Paiement_Client]
GO
ALTER TABLE [dbo].[TermesLocation]  WITH CHECK ADD  CONSTRAINT [FK_TermesLocation_Paiement] FOREIGN KEY([idPaiement])
REFERENCES [dbo].[Paiement] ([idPaiement])
GO
ALTER TABLE [dbo].[TermesLocation] CHECK CONSTRAINT [FK_TermesLocation_Paiement]
GO
ALTER TABLE [dbo].[Vehicule]  WITH CHECK ADD  CONSTRAINT [FK_Vehicule_Couleur] FOREIGN KEY([idCouleur])
REFERENCES [dbo].[Couleur] ([idCouleur])
GO
ALTER TABLE [dbo].[Vehicule] CHECK CONSTRAINT [FK_Vehicule_Couleur]
GO
ALTER TABLE [dbo].[Vehicule]  WITH CHECK ADD  CONSTRAINT [FK_Vehicule_Modele] FOREIGN KEY([idModel])
REFERENCES [dbo].[Modele] ([idModel])
GO
ALTER TABLE [dbo].[Vehicule] CHECK CONSTRAINT [FK_Vehicule_Modele]
GO
ALTER TABLE [dbo].[Vehicule]  WITH CHECK ADD  CONSTRAINT [FK_Vehicule_Type] FOREIGN KEY([idType])
REFERENCES [dbo].[Type] ([idType])
GO
ALTER TABLE [dbo].[Vehicule] CHECK CONSTRAINT [FK_Vehicule_Type]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [check_nbrMoisLocation] CHECK  (([contratNbrMois]=(48) OR [contratNbrMois]=(36) OR [contratNbrMois]=(24) OR [contratNbrMois]=(12)))
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [check_nbrMoisLocation]
GO
/****** Object:  Trigger [dbo].[audit_location]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[audit_location]
ON [dbo].[Location]
FOR UPDATE, insert
AS 

 BEGIN
 SET NOCOUNT ON
 ----------update------------
	INSERT INTO [Audit]
	(
		dateChangement,idLocation,debutLoc,finContrat,contratNbrMois,
		premPaiement,mntPaieMens,nbrKmDebut,nbrKmFin
	)
	SELECT 
		GETDATE(),D.idLocation,debutLoc,D.finContrat,D.contratNbrMois,
		D.premPaiement,D.mntPaieMens,D.nbrKmDebut,D.nbrKmFin
	FROM deleted D
 ----------insert------------
	INSERT INTO [Audit]
	(
	dateChangement,idLocation,debutLoc,finContrat,contratNbrMois,
		premPaiement,mntPaieMens,nbrKmDebut,nbrKmFin
	)
	SELECT 
		GETDATE(),I.idLocation,I.debutLoc,I.finContrat,I.contratNbrMois,
		I.premPaiement,I.mntPaieMens,I.nbrKmDebut,I.nbrKmFin
	FROM inserted AS I
END;
GO
ALTER TABLE [dbo].[Location] ENABLE TRIGGER [audit_location]
GO
/****** Object:  Trigger [dbo].[interdictionDeSuppression]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[interdictionDeSuppression]
ON [dbo].[Location]
AFTER DELETE
AS BEGIN
IF EXISTS (SELECT * FROM DELETED)
BEGIN
	THROW 50000,'INTERDICTION DE SUPPRIMER CES INFORMATIONS',1;
	ROLLBACK
END
END

--OUTILS POUR LE PROGRAMMEUR
--enable trigger [dbo].[interdictionDeSuppression] on locations
GO
ALTER TABLE [dbo].[Location] ENABLE TRIGGER [interdictionDeSuppression]
GO
/****** Object:  Trigger [dbo].[VerificationMoisDeLocation]    Script Date: 2022-11-02 17:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[VerificationMoisDeLocation]
ON [dbo].[Location]
AFTER INSERT, UPDATE
AS

DECLARE @moisDeLocation INT;
	SET @moisDeLocation = (SELECT 1 FROM inserted as i WHERE i.contratNbrMois = @moisDeLocation)
DECLARE @contratNbrMois INT;
	SET @contratNbrMois = (SELECT contratNbrMois FROM Location)


BEGIN
IF 
(@contratNbrMois = 12)
 
insert into Location (contratNbrMois)
	VALUES(@moisDeLocation) ;
IF 
(@moisDeLocation = 24)
insert into Location (contratNbrMois)
	VALUES(@moisDeLocation);
IF 
(@moisDeLocation = (36))
insert into Location (contratNbrMois)
	VALUES(@moisDeLocation);
IF 
(@moisDeLocation = 48)
insert into Location (contratNbrMois)
	VALUES(@moisDeLocation);
ELSE
	BEGIN
		BEGIN TRANSACTION
			RAISERROR ('Traitement impossible: Vous devez inserer une date
			qui est conforme aux normes de locations: 12, 24, 36 ou 48 mois',10,1)
		ROLLBACK TRANSACTION
		RETURN
	END
END
GO
ALTER TABLE [dbo].[Location] DISABLE TRIGGER [VerificationMoisDeLocation]
GO
