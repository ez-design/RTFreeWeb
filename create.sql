CREATE TABLE `Configs` (
  `Id` varchar(20) NOT NULL,
  `Value` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Libraries` (
  `ProgramId` varchar(30) NOT NULL,
  `Path` text,
  `Created` datetime DEFAULT NULL,
  PRIMARY KEY (`ProgramId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Programs` (
  `Id` varchar(30) NOT NULL,
  `StationId` varchar(20) DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `Cast` text,
  `Info` text,
  `Start` datetime DEFAULT NULL,
  `End` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `idx_programs_Start` (`Start`),
  KEY `idx_programs_End` (`End`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Stations` (
  `Id` varchar(20) NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `RegionId` varchar(20) DEFAULT NULL,
  `RegionName` varchar(100) DEFAULT NULL,
  `OrderNo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `Users` (
  `Id` varchar(20) NOT NULL,
  `Password` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
