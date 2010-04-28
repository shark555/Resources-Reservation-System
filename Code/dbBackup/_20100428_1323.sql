-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.1.37-1ubuntu5.1


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema RRS
--

CREATE DATABASE IF NOT EXISTS RRS;
USE RRS;

--
-- Definition of table `RRS`.`Diplomas`
--

DROP TABLE IF EXISTS `RRS`.`Diplomas`;
CREATE TABLE  `RRS`.`Diplomas` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `RRS`.`Diplomas`
--

/*!40000 ALTER TABLE `Diplomas` DISABLE KEYS */;
LOCK TABLES `Diplomas` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `Diplomas` ENABLE KEYS */;


--
-- Definition of table `RRS`.`Projects`
--

DROP TABLE IF EXISTS `RRS`.`Projects`;
CREATE TABLE  `RRS`.`Projects` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `RRS`.`Projects`
--

/*!40000 ALTER TABLE `Projects` DISABLE KEYS */;
LOCK TABLES `Projects` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `Projects` ENABLE KEYS */;


--
-- Definition of table `RRS`.`Subjects`
--

DROP TABLE IF EXISTS `RRS`.`Subjects`;
CREATE TABLE  `RRS`.`Subjects` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `RRS`.`Subjects`
--

/*!40000 ALTER TABLE `Subjects` DISABLE KEYS */;
LOCK TABLES `Subjects` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `Subjects` ENABLE KEYS */;


--
-- Definition of table `RRS`.`SubjectsToTeacher`
--

DROP TABLE IF EXISTS `RRS`.`SubjectsToTeacher`;
CREATE TABLE  `RRS`.`SubjectsToTeacher` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ID_Subject` int(10) unsigned NOT NULL,
  `ID_Teacher` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `ID_Subject` (`ID_Subject`),
  KEY `ID_Teacher` (`ID_Teacher`),
  CONSTRAINT `ID_Teacher` FOREIGN KEY (`ID_Teacher`) REFERENCES `Users` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ID_Subject` FOREIGN KEY (`ID_Subject`) REFERENCES `Subjects` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `RRS`.`SubjectsToTeacher`
--

/*!40000 ALTER TABLE `SubjectsToTeacher` DISABLE KEYS */;
LOCK TABLES `SubjectsToTeacher` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `SubjectsToTeacher` ENABLE KEYS */;


--
-- Definition of table `RRS`.`Users`
--

DROP TABLE IF EXISTS `RRS`.`Users`;
CREATE TABLE  `RRS`.`Users` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `RRS`.`Users`
--

/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
LOCK TABLES `Users` WRITE;
UNLOCK TABLES;
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
