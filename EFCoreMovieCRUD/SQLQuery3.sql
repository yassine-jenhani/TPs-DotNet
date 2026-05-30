DELETE FROM Movies;
DELETE FROM Genres;
DELETE FROM Customers;

-- =====================
-- 1. Insérer des Genres
-- =====================
INSERT INTO Genres (Id, Name) VALUES 
(NEWID(), 'Action'),
(NEWID(), 'Comedy'),
(NEWID(), 'Drama'),
(NEWID(), 'Science Fiction'),
(NEWID(), 'Horror'),
(NEWID(), 'Romance'),
(NEWID(), 'Thriller');

-- Vérifier les genres insérés
SELECT * FROM Genres;

-- =====================
-- 2. Insérer des Movies
-- (récupérer d'abord les IDs des genres)
-- =====================

-- Déclarer les variables pour les IDs
DECLARE @ActionId UNIQUEIDENTIFIER = (SELECT Id FROM Genres WHERE Name = 'Action');
DECLARE @ComedyId UNIQUEIDENTIFIER = (SELECT Id FROM Genres WHERE Name = 'Comedy');
DECLARE @DramaId  UNIQUEIDENTIFIER = (SELECT Id FROM Genres WHERE Name = 'Drama');
DECLARE @SciFiId  UNIQUEIDENTIFIER = (SELECT Id FROM Genres WHERE Name = 'Science Fiction');
DECLARE @HorrorId UNIQUEIDENTIFIER = (SELECT Id FROM Genres WHERE Name = 'Horror');

INSERT INTO Movies (Name, GenreId) VALUES
('Inception',        @SciFiId),
('The Dark Knight',  @ActionId),
('Interstellar',     @SciFiId),
('Titanic',          @DramaId),
('Avatar',           @SciFiId),
('The Hangover',     @ComedyId),
('Get Out',          @HorrorId),
('Mad Max',          @ActionId),
('Joker',            @DramaId),
('The Matrix',       @SciFiId),
('Home Alone',       @ComedyId),
('Avengers',         @ActionId);

-- Vérifier les movies insérés avec leur genre
SELECT m.Id, m.Name, g.Name AS Genre 
FROM Movies m
JOIN Genres g ON m.GenreId = g.Id;