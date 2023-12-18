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
	(1, 'Jupiter Surface', 10100010),
(0, 'Saturn Transfer', 1465),
	(1, 'Saturn Capture / Escape', 1159),
	(1, 'Saturn (400km) Orbit', 11298),
	(1, 'Saturn Surface', 11110011),
(0, 'Uranus Transfer', 775),
	(1, 'Uranus Capture / Escape', 1259),
	(1, 'Uranus (400km) Orbit', 12298),
	(1, 'Uranus Surface', 12120012),
(0, 'Neptune Transfer', 506),
	(1, 'Neptune Capture / Escape', 1359),
	(1, 'Neptune (400km) Orbit', 13298),
	(1, 'Neptune Surface', 13130013),
(0, 'Orcus Transfer', 396), -- Kuiper Belt
	(1, 'Orcus Capture / Escape', 1459),
	(1, 'Orcus (400km) Orbit', 14298),
	(1, 'Orcus Surface', 14140014),
(0, 'Pluto Transfer', 389),
	(1, 'Pluto Capture / Escape', 1559),
	(1, 'Pluto (400km) Orbit', 15298),
	(1, 'Pluto Surface', 15150015),
(0, 'Haumea Transfer', 356),
	(1, 'Haumea Capture / Escape', 1659),
	(1, 'Haumea (400km) Orbit', 16298),
	(1, 'Haumea Surface', 16160016),
(0, 'Quaoar Transfer', 352),
	(1, 'Quaoar Capture / Escape', 1759),
	(1, 'Quaoar (400km) Orbit', 17298),
	(1, 'Quaoar Surface', 17170017),
(0, 'Makemake Transfer', 339),
	(1, 'Makemake Capture / Escape', 1859),
	(1, 'Makemake (400km) Orbit', 18298),
	(1, 'Makemake Surface', 18180018),
(0, 'Gonggong Transfer', 232),
	(1, 'Gonggong Capture / Escape', 1959),
	(1, 'Gonggong (400km) Orbit', 19298),
	(1, 'Gonggong Surface', 19190019),
(0, 'Eris Transfer', 230),
	(1, 'Eris Capture / Escape', 2059),
	(1, 'Eris (400km) Orbit', 20298),
	(1, 'Eris Surface', 20200020),
(0, 'Sedna Transfer', 30),
	(1, 'Sedna Capture / Escape', 2159),
	(1, 'Sedna (400km) Orbit', 21298),
	(1, 'Sedna Surface', 21210021);

UPDATE SystemPositions SET SystemID=1, Atmosphere=false, ParentID = 0;
UPDATE SystemPositions SET Atmosphere=true WHERE PositionName IN ('Venus Transfer', 'Earth Capture / Escape', 'Mars Transfer', 'Jupiter Transfer', 'Saturn Transfer', 'Titan Transfer', 'Uranus Transfer', 'Neptune Transfer'); -- Gives atmosphere to the 8 bodies in Sol with one
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