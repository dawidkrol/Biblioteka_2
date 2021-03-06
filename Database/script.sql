USE [master]
GO
/****** Object:  Database [biblioteka_2]    Script Date: 14.02.2021 23:46:42 ******/
CREATE DATABASE [biblioteka_2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'biblioteka_2', FILENAME = N'/var/opt/mssql/data/biblioteka_2.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'biblioteka_2_log', FILENAME = N'/var/opt/mssql/data/biblioteka_2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [biblioteka_2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [biblioteka_2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [biblioteka_2] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [biblioteka_2] SET ANSI_NULLS ON 
GO
ALTER DATABASE [biblioteka_2] SET ANSI_PADDING ON 
GO
ALTER DATABASE [biblioteka_2] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [biblioteka_2] SET ARITHABORT ON 
GO
ALTER DATABASE [biblioteka_2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [biblioteka_2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [biblioteka_2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [biblioteka_2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [biblioteka_2] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [biblioteka_2] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [biblioteka_2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [biblioteka_2] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [biblioteka_2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [biblioteka_2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [biblioteka_2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [biblioteka_2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [biblioteka_2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [biblioteka_2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [biblioteka_2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [biblioteka_2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [biblioteka_2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [biblioteka_2] SET RECOVERY FULL 
GO
ALTER DATABASE [biblioteka_2] SET  MULTI_USER 
GO
ALTER DATABASE [biblioteka_2] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [biblioteka_2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [biblioteka_2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [biblioteka_2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [biblioteka_2] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'biblioteka_2', N'ON'
GO
ALTER DATABASE [biblioteka_2] SET QUERY_STORE = OFF
GO
USE [biblioteka_2]
GO
/****** Object:  UserDefinedTableType [dbo].[handingUDT]    Script Date: 14.02.2021 23:46:42 ******/
CREATE TYPE [dbo].[handingUDT] AS TABLE(
	[isbn] [varchar](20) NULL,
	[nr_czytelnika] [int] NULL,
	[data_Wypozyczenia] [datetime] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[numberOfRentalsfromTime]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[numberOfRentalsfromTime](@t datetime)
returns int 
as
begin
declare @return as int
set @return = (select COUNT(*) from dbo.Wypożyczenia as w where w.Data_wypożyczenia >= @t)
return @return
END
GO
/****** Object:  Table [dbo].[Wypożyczenia]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wypożyczenia](
	[ISBN] [nvarchar](20) NOT NULL,
	[Nr_czytelnika] [int] NOT NULL,
	[Data_wypożyczenia] [datetime] NOT NULL,
	[Spodziewana_data_zwrotu] [datetime] NULL,
	[Faktyczna_data_zwrotu] [datetime] NULL,
	[Opłata] [money] NULL,
 CONSTRAINT [PK_Wypożyczenia] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC,
	[Nr_czytelnika] ASC,
	[Data_wypożyczenia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[RentalsStats]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[RentalsStats]()
returns table
as
return SELECT month(w.Data_wypożyczenia) as 'miesiac', YEAR(w.Data_wypożyczenia) as 'rok', COUNT(*) as 'ilosc' from Wypożyczenia as w group by month(w.Data_wypożyczenia),YEAR(w.Data_wypożyczenia)
GO
/****** Object:  Table [dbo].[Czytelnicy]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Czytelnicy](
	[Nr_czytelnika] [int] IDENTITY(1,1) NOT NULL,
	[Tytuł] [nvarchar](10) NULL,
	[Imię] [nvarchar](20) NOT NULL,
	[Nazwisko] [nvarchar](20) NOT NULL,
	[Adres] [nvarchar](45) NULL,
	[Miasto] [nvarchar](40) NULL,
	[Telefon] [nvarchar](20) NULL,
	[Email] [nvarchar](max) NULL,
	[Opłata] [money] NULL,
	[Wartość] [money] NULL,
	[Profil] [nvarchar](15) NULL,
 CONSTRAINT [PK_Czytelnicy] PRIMARY KEY CLUSTERED 
(
	[Nr_czytelnika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Książki]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Książki](
	[ISBN] [nvarchar](20) NOT NULL,
	[Tytuł] [nvarchar](50) NOT NULL,
	[Rok_wydania] [int] NULL,
	[Wydawca] [nvarchar](20) NULL,
	[Kategoria] [int] NOT NULL,
	[Aktualna] [int] NOT NULL,
 CONSTRAINT [PK_Książki] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetRentalsFromTime]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[GetRentalsFromTime](@t datetime)
returns table
as
return SELECT k.Tytuł, k.Wydawca, w.[Data_wypożyczenia], w.[Spodziewana_data_zwrotu], c.Imię, c.Nazwisko, c.Email
FROM dbo.Wypożyczenia AS w INNER JOIN
	 dbo.Czytelnicy AS c ON w.Nr_czytelnika = c.Nr_czytelnika INNER JOIN
	 dbo.Książki AS k ON k.ISBN = w.ISBN
WHERE w.[Faktyczna_data_zwrotu] >= @t
GO
/****** Object:  Table [dbo].[KsiążkiAutorzy]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KsiążkiAutorzy](
	[ISBN] [nvarchar](20) NOT NULL,
	[Nr_autora] [int] NOT NULL,
 CONSTRAINT [PK_KsiążkiAutorzy] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC,
	[Nr_autora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Autorzy]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autorzy](
	[Nr_autora] [int] IDENTITY(1,1) NOT NULL,
	[Imię] [nvarchar](50) NULL,
	[Nazwisko] [nvarchar](50) NULL,
 CONSTRAINT [PK_Autorzy] PRIMARY KEY CLUSTERED 
(
	[Nr_autora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[delayedDonations]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[delayedDonations]()
returns table
as
return SELECT k.ISBN, a.Imię as 'Imie_autora', a.Nazwisko as 'Nazwisko_autora', k.Tytuł, k.Wydawca, w.[Data_wypożyczenia], w.[Spodziewana_data_zwrotu], c.Imię, c.Nazwisko, c.Email
FROM dbo.Wypożyczenia AS w 
	 INNER JOIN dbo.Czytelnicy AS c ON w.Nr_czytelnika = c.Nr_czytelnika 
	 INNER JOIN dbo.Książki AS k ON k.ISBN = w.ISBN
	 Inner Join dbo.KsiążkiAutorzy as ks on ks.ISBN = k.ISBN
	 Inner Join dbo.Autorzy as a on a.Nr_autora = ks.Nr_autora
WHERE w.[Spodziewana_data_zwrotu] <= GetDate() and w.[Faktyczna_data_zwrotu] is null 
GO
/****** Object:  UserDefinedFunction [dbo].[notDeliveredYet]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[notDeliveredYet]()
returns table
as
return SELECT k.ISBN,k.Tytuł, k.Wydawca,a.Imię as 'Imie_autora', a.Nazwisko as 'Nazwisko_autora', w.[Data_wypożyczenia], w.[Spodziewana_data_zwrotu], c.Imię, c.Nazwisko, c.Email
FROM dbo.Wypożyczenia AS w 
	 INNER JOIN dbo.Czytelnicy AS c ON w.Nr_czytelnika = c.Nr_czytelnika 
	 INNER JOIN dbo.Książki AS k ON k.ISBN = w.ISBN
	 Inner Join dbo.KsiążkiAutorzy as ks on ks.ISBN = k.ISBN
	 Inner Join dbo.Autorzy as a on a.Nr_autora = ks.Nr_autora
WHERE w.[Faktyczna_data_zwrotu] is null 
GO
/****** Object:  Table [dbo].[Kategorie]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategorie](
	[id_kategorii] [int] NOT NULL,
	[nazwa_kategorii] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Kategorie] PRIMARY KEY CLUSTERED 
(
	[id_kategorii] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[availableBooks]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[availableBooks]()
returns table
as
return SELECT k.ISBN,k.Tytuł,k.Wydawca,ka.nazwa_kategorii as 'Kategoria',k.Rok_wydania
FROM dbo.Książki AS k full outer JOIN
	 [dbo].[notDeliveredYet]() AS w ON k.ISBN = w.ISBN
	 Inner join Kategorie as ka on ka.id_kategorii = k.Kategoria
WHERE w.ISBN is null and k.Aktualna = 1
GO
/****** Object:  UserDefinedFunction [dbo].[ListOfAuthors]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[ListOfAuthors]()
returns table
as
return SELECT a.Nr_autora, CONCAT(a.Imię,' ',a.Nazwisko) AS imie_nazwisko
FROM dbo.Autorzy AS a
GO
/****** Object:  View [dbo].[GetCzytelnicy]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetCzytelnicy]
AS
SELECT        dbo.Czytelnicy.*
FROM            dbo.Czytelnicy
GO
/****** Object:  View [dbo].[GetCategories]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetCategories] AS
SELECT *
FROM Kategorie; 
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[firstName] [varchar](50) NOT NULL,
	[lastName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Książki]  WITH CHECK ADD  CONSTRAINT [FK_Książki_Kategorie] FOREIGN KEY([Kategoria])
REFERENCES [dbo].[Kategorie] ([id_kategorii])
GO
ALTER TABLE [dbo].[Książki] CHECK CONSTRAINT [FK_Książki_Kategorie]
GO
ALTER TABLE [dbo].[KsiążkiAutorzy]  WITH CHECK ADD  CONSTRAINT [FK_KsiążkiAutorzy_Autorzy] FOREIGN KEY([Nr_autora])
REFERENCES [dbo].[Autorzy] ([Nr_autora])
GO
ALTER TABLE [dbo].[KsiążkiAutorzy] CHECK CONSTRAINT [FK_KsiążkiAutorzy_Autorzy]
GO
ALTER TABLE [dbo].[KsiążkiAutorzy]  WITH CHECK ADD  CONSTRAINT [FK_KsiążkiAutorzy_Książki] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Książki] ([ISBN])
GO
ALTER TABLE [dbo].[KsiążkiAutorzy] CHECK CONSTRAINT [FK_KsiążkiAutorzy_Książki]
GO
ALTER TABLE [dbo].[Wypożyczenia]  WITH CHECK ADD  CONSTRAINT [FK_Wypożyczenia_Czytelnicy] FOREIGN KEY([Nr_czytelnika])
REFERENCES [dbo].[Czytelnicy] ([Nr_czytelnika])
GO
ALTER TABLE [dbo].[Wypożyczenia] CHECK CONSTRAINT [FK_Wypożyczenia_Czytelnicy]
GO
ALTER TABLE [dbo].[Wypożyczenia]  WITH CHECK ADD  CONSTRAINT [FK_Wypożyczenia_Książki] FOREIGN KEY([ISBN])
REFERENCES [dbo].[Książki] ([ISBN])
GO
ALTER TABLE [dbo].[Wypożyczenia] CHECK CONSTRAINT [FK_Wypożyczenia_Książki]
GO
/****** Object:  StoredProcedure [dbo].[SpAutor]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpAutor]
	@imie varchar(50),
	@nazwisko varchar(50)
AS
BEGIN
Insert into Autorzy (Imię,Nazwisko)
values(@imie,@nazwisko)
end
GO
/****** Object:  StoredProcedure [dbo].[SpDelBook]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpDelBook]
	@isbn varchar(20)
AS
BEGIN
	UPDATE Książki set Aktualna = 0 WHERE ISBN = @isbn
end
GO
/****** Object:  StoredProcedure [dbo].[SpDelUser]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpDelUser]
	@Nr_czytelnika int
AS
BEGIN
	DELETE FROM Czytelnicy WHERE Nr_czytelnika = @Nr_czytelnika
end
GO
/****** Object:  StoredProcedure [dbo].[SPhandingOverTheBook]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPhandingOverTheBook]
	@data datetime,
	@isbn varchar(20),
	@data_Wypozyczenia datetime,
	@oplata money = null
AS
BEGIN
	UPDATE Wypożyczenia
SET Faktyczna_data_zwrotu = @data, Opłata = @oplata
WHERE ISBN = @isbn
and Data_wypożyczenia = @data_Wypozyczenia
end
GO
/****** Object:  StoredProcedure [dbo].[SpNewBook]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpNewBook]
	@isbn nvarchar(20),
	@tytul varchar(50),
	@rok_wydania int,
	@wydawca nvarchar(20),
	@kategoria nvarchar(30),
	@nr_autora int
AS
BEGIN
Insert into Książki(ISBN,Tytuł,Rok_wydania,Wydawca,Kategoria,Aktualna)
values (@isbn,@tytul,@rok_wydania,@wydawca,@kategoria,1);
Insert into KsiążkiAutorzy(ISBN,Nr_autora)
values (@isbn,@nr_autora)
end
GO
/****** Object:  StoredProcedure [dbo].[SpNewRental]    Script Date: 14.02.2021 23:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpNewRental]
	@isbn varchar(50),
	@Nr_Czytelnika varchar(50),
	@spodziewanaDataZwrotu date
AS
BEGIN
Insert into Wypożyczenia(ISBN,Nr_czytelnika,Data_wypożyczenia,Spodziewana_data_zwrotu)
values(@isbn,@Nr_Czytelnika,GETDATE(),@spodziewanaDataZwrotu)
end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Czytelnicy"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetCzytelnicy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetCzytelnicy'
GO
USE [master]
GO
ALTER DATABASE [biblioteka_2] SET  READ_WRITE 
GO
