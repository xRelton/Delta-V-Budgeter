CREATE TABLE StarSystems ( 
	SystemID INTEGER NOT NULL PRIMARY KEY, 
	SystemName NVARCHAR(50) NOT NULL,
    SubCount INTEGER
);

CREATE TABLE SystemPositions (  
    SystemID INTEGER,
	PositionID INTEGER NOT NULL PRIMARY KEY,
	ParentID INTEGER, 
	PositionName NVARCHAR(50) NOT NULL,
	DVFromTransfer INTEGER NOT NULL,
	PosLevel INTEGER NOT NULL,
    Atmosphere BOOLEAN,
    SubCount INTEGER
);

INSERT INTO StarSystems (SystemName) VALUES ('Sol');
INSERT INTO SystemPositions (PosLevel, PositionName, DVFromTransfer) VALUES
 -- ID = (Current line - 20)
(0, 'Sun Transfer', 23649), -- Star
	(1, 'Low Sun (10Mm) Orbit', 178107),
	(1, 'Sun Surface', 618107),
(0, 'Mercury Transfer', 7906), -- Inner Planets
	(1, 'Mercury Capture / Escape', 6310),
	(1, 'Mercury (100km) Orbit', 7530),
	(1, 'Mercury Surface', 10592),
(0, 'Venus Transfer', 5821),
	(1, 'Venus Capture / Escape', 359),
	(1, 'Venus (400km) Orbit', 3298),
	(1, 'Venus Surface', 33003),
(0, 'Earth Capture / Escape', 5541),
	(1, 'Moon Transfer', 93),
		(2, 'Moon Capture / Escape', 145),
		(2, 'Moon (100km) Orbit', 821),
		(2, 'Moon Surface', 2542),
	(1, 'Geostationary Transfer', 772),
		(2, 'Geostationary (35786km) Orbit', 1472),
	(1, 'Low Earth (250km) Orbit', 3212),
	(1, 'Earth Surface', 12468),
(0, 'Mars Transfer', 5153),
	(1, 'Mars Capture / Escape', 673),
	(1, 'Deimos Transfer', 1009),
		(2, 'Deimos Capture / Escape', 649),
		(2, 'Deimos (1km) Orbit', 651),
		(2, 'Deimos Surface', 655),
	(1, 'Phobos Transfer', 1404),
		(2, 'Phobos Capture / Escape', 535),
		(2, 'Phobos (1km) Orbit', 538),
		(2, 'Phobos Surface', 544),
	(1, 'Mars (200km) Orbit', 2102),
	(1, 'Mars Surface', 5680),
(0, 'Vesta Transfer', 4230), -- Asteroid Belt
	(1, 'Vesta Capture / Escape', 4096),
	(1, 'Vesta (400km) Orbit', 4198),
	(1, 'Vesta Surface', 4371),
(0, 'Ceres Transfer', 3851),
	(1, 'Ceres Capture / Escape', 759),
	(1, 'Ceres (400km) Orbit', 7298),
	(1, 'Ceres Surface', 77007),
(0, 'Pallas Transfer', 3848),
	(1, 'Pallas Capture / Escape', 859),
	(1, 'Pallas (400km) Orbit', 8298),
	(1, 'Pallas Surface', 88008),
(0, 'Hygiea Transfer', 3551),
	(1, 'Hygiea Capture / Escape', 959),
	(1, 'Hygiea (400km) Orbit', 9298),
	(1, 'Hygiea Surface', 99009),
(0, 'Jupiter Transfer', 2452), -- Outer Planets
	(1, 'Jupiter Capture / Escape', 1059),
	(1, 'Jupiter (400km) Orbit', 10298),
	(1, 'Jupiter Surface', 10100010);
/*('Sun Transfer', 23649, 0), -- Star
	('Low Sun (10Mm) Orbit', 178107, 1),
	('Sun Surface', 618107, 1),
('Mercury Transfer', 7906, 0), -- Inner Planets
	('Mercury Capture / Escape', 6310, 4),
	('Mercury (100km) Orbit', 7530, 4),
	('Mercury Surface', 10592, 4),
('Venus Transfer', 5821, 0),
	('Venus Capture / Escape', 359, 8),
	('Venus (400km) Orbit', 3298, 8),
	('Venus Surface', 33003, 8),
('Earth Capture / Escape', 5541, 0),
	('Moon Transfer', 93, 12),
		('Moon Capture / Escape', 145, 13),
		('Moon (100km) Orbit', 821, 13),
		('Moon Surface', 2542, 13),
	('Geostationary Transfer', 772, 12),
		('Geostationary (35786km) Orbit', 1472, 17),
	('Low Earth (250km) Orbit', 3212, 12),
	('Earth Surface', 12468, 12),
('Mars Transfer', 5153, 0),
	('Mars Capture / Escape', 673, 21),
	('Deimos Transfer', 1009, 21),
		('Deimos Capture / Escape', 649, 23),
		('Deimos (1km) Orbit', 651, 23),
		('Deimos Surface', 655, 23),
	('Phobos Transfer', 1404, 21),
		('Phobos Capture / Escape', 535, 27),
		('Phobos (1km) Orbit', 538, 27),
		('Phobos Surface', 544, 27),
	('Mars (200km) Orbit', 2102, 21),
	('Mars Surface', 5680, 21),
('Vesta Transfer', 4230, 0), -- Asteroid Belt
	('Vesta Capture / Escape', 4096, 33),
	('Vesta (400km) Orbit', 4198, 33),
	('Vesta Surface', 4371, 6),
('Ceres Transfer', 3851, 0),
	('Ceres Capture / Escape', 759, 37),
	('Ceres (400km) Orbit', 7298, 37),
	('Ceres Surface', 77007, 37),
('Pallas Transfer', 3848, 0),
	('Pallas Capture / Escape', 859, 41),
	('Pallas (400km) Orbit', 8298, 41),
	('Pallas Surface', 88008, 41),
('Hygiea Transfer', 3551, 0),
	('Hygiea Capture / Escape', 959, 45),
	('Hygiea (400km) Orbit', 9298, 45),
	('Hygiea Surface', 99009, 45),
('Jupiter Transfer', 2452, 0), -- Outer Planets
	('Jupiter Capture / Escape', 1059, 49),
	('Jupiter (400km) Orbit', 10298, 49),
	('Jupiter Surface', 10100010, 49),
('Saturn Transfer', 1465, 0),
	('Saturn Capture / Escape', 1159, 11),
	('Saturn (400km) Orbit', 11298, 11),
	('Saturn Surface', 11110011, 11),
('Uranus Transfer', 775, 0),
	('Uranus Capture / Escape', 1259, 12),
	('Uranus (400km) Orbit', 12298, 12),
	('Uranus Surface', 12120012, 12),
('Neptune Transfer', 506, 0),
	('Neptune Capture / Escape', 1359, 13),
	('Neptune (400km) Orbit', 13298, 13),
	('Neptune Surface', 13130013, 13),
('Orcus Transfer', 396, 0), -- Kuiper Belt
	('Orcus Capture / Escape', 1459, 14),
	('Orcus (400km) Orbit', 14298, 14),
	('Orcus Surface', 14140014, 14),
('Pluto Transfer', 389, 0),
	('Pluto Capture / Escape', 1559, 15),
	('Pluto (400km) Orbit', 15298, 15),
	('Pluto Surface', 15150015, 15),
('Haumea Transfer', 356, 0),
	('Haumea Capture / Escape', 1659, 16),
	('Haumea (400km) Orbit', 16298, 16),
	('Haumea Surface', 16160016, 16),
('Quaoar Transfer', 352, 0),
	('Quaoar Capture / Escape', 1759, 17),
	('Quaoar (400km) Orbit', 17298, 17),
	('Quaoar Surface', 17170017, 17),
('Makemake Transfer', 339, 0),
	('Makemake Capture / Escape', 1859, 18),
	('Makemake (400km) Orbit', 18298, 18),
	('Makemake Surface', 18180018, 18),
('Gonggong Transfer', 232, 0),
	('Gonggong Capture / Escape', 1959, 19),
	('Gonggong (400km) Orbit', 19298, 19),
	('Gonggong Surface', 19190019, 19),
('Eris Transfer', 230, 0),
	('Eris Capture / Escape', 2059, 20),
	('Eris (400km) Orbit', 20298, 20),
	('Eris Surface', 20200020, 20),
('Sedna Transfer', 30, 0),
	('Sedna Capture / Escape', 2159, 21),
	('Sedna (400km) Orbit', 21298, 21),
	('Sedna Surface', 21210021, 21);*/

UPDATE SystemPositions SET SystemID=1, Atmosphere=false, ParentID = 0;
UPDATE SystemPositions SET Atmosphere=true WHERE PositionName IN ('Venus Transfer', 'Earth Capture / Escape', 'Mars Transfer', 'Jupiter Transfer'); -- Gives atmosphere to the 8 bodies in Sol with one
UPDATE SystemPositions SET ParentID = -- Goes up from pos to find first pos with a lower level by splitting table
	(SELECT MAX(ParentID) FROM
		(SELECT p1.PositionID ID, p2.PositionID ParentID FROM
			SystemPositions p2 LEFT JOIN
			SystemPositions p1 ON p1.PosLevel = p2.PosLevel + 1
		WHERE p2.PositionID < p1.PositionID
		ORDER BY p1.PositionID DESC)
	WHERE SystemPositions.PositionID = ID) WHERE PosLevel != 0;

UPDATE SystemPositions SET SubCount = -- Counts subs to place in subcount storage for use in array creation
	(SELECT SubCount FROM
		(SELECT p1.PositionID ID, COUNT(p2.ParentID) SubCount FROM
			SystemPositions p1 LEFT JOIN
			SystemPositions p2 ON p2.ParentID = p1.PositionID
		GROUP BY p1.PositionID)
    WHERE SystemPositions.PositionID = ID);

UPDATE StarSystems SET SubCount = (SELECT COUNT(*) FROM SystemPositions WHERE PosLevel = 0 AND SystemID = 1) WHERE SystemName = 'Sol';