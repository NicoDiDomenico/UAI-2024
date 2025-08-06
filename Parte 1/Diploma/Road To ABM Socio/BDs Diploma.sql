-- MySQL dump 10.13  Distrib 8.0.20, for Win64 (x86_64)
--
-- Host: localhost    Database: gimnasio
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `gimnasio`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `gimnasio` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `gimnasio`;

--
-- Table structure for table `socios`
--

DROP TABLE IF EXISTS `socios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `socios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `estado` varchar(50) NOT NULL,
  `fechaNacimiento` date DEFAULT NULL,
  `genero` varchar(10) DEFAULT NULL,
  `dni` varchar(20) DEFAULT NULL,
  `direccion` varchar(100) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `obraSocial` varchar(50) DEFAULT NULL,
  `diasAsistencia` varchar(50) DEFAULT NULL,
  `nombreUsuario` varchar(50) DEFAULT NULL,
  `contrasena` varchar(255) DEFAULT NULL,
  `preguntaSeguridad` varchar(255) DEFAULT NULL,
  `respuestaSeguridad` varchar(255) DEFAULT NULL,
  `plan` enum('Mensual','Anual') DEFAULT NULL,
  `fechaInicioActividades` date DEFAULT NULL,
  `fechaFinActividades` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `socios`
--

LOCK TABLES `socios` WRITE;
/*!40000 ALTER TABLE `socios` DISABLE KEYS */;
INSERT INTO `socios` VALUES (6,'Nicolas Di Domenico','Nuevo','2024-11-20','Hombre','39686403','H. Yrigoyen 737','8446846','sadsadsdgdfgdfQ@gmail.com','OSDE','Lunes','nico_dido','sdasd','holacomoteva?','Bien','Mensual','2024-11-18','2024-12-18'),(7,'Juan Cruz','Nuevo','2024-11-22','Hombre','4545645','asdsadasd545','654454','asdadasd@gmail.com','dsfsdf','Miércoles','adawdaw','sefsefsef12sdf','efsdf','ewfssfds','Mensual','2024-11-15','2024-12-15');
/*!40000 ALTER TABLE `socios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Current Database: `gimnasiodb`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `gimnasiodb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `gimnasiodb`;

--
-- Table structure for table `calentamiento`
--

DROP TABLE IF EXISTS `calentamiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `calentamiento` (
  `idRutina` int NOT NULL,
  `idElemento` int NOT NULL,
  `duracion` int DEFAULT NULL,
  PRIMARY KEY (`idRutina`,`idElemento`),
  KEY `idElemento` (`idElemento`),
  CONSTRAINT `calentamiento_ibfk_1` FOREIGN KEY (`idRutina`) REFERENCES `rutina` (`idRutina`),
  CONSTRAINT `calentamiento_ibfk_2` FOREIGN KEY (`idElemento`) REFERENCES `equipamiento` (`idElemento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `calentamiento`
--

LOCK TABLES `calentamiento` WRITE;
/*!40000 ALTER TABLE `calentamiento` DISABLE KEYS */;
/*!40000 ALTER TABLE `calentamiento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ejercicio`
--

DROP TABLE IF EXISTS `ejercicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ejercicio` (
  `idElemento` int NOT NULL,
  `idGimnasio` int NOT NULL,
  `descripcion` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idElemento`,`idGimnasio`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `ejercicio_ibfk_1` FOREIGN KEY (`idElemento`) REFERENCES `equipamiento` (`idElemento`),
  CONSTRAINT `ejercicio_ibfk_2` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ejercicio`
--

LOCK TABLES `ejercicio` WRITE;
/*!40000 ALTER TABLE `ejercicio` DISABLE KEYS */;
/*!40000 ALTER TABLE `ejercicio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipamiento`
--

DROP TABLE IF EXISTS `equipamiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipamiento` (
  `idElemento` int NOT NULL AUTO_INCREMENT,
  `idGimnasio` int DEFAULT NULL,
  `precio` float DEFAULT NULL,
  `nombreElemento` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idElemento`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `equipamiento_ibfk_1` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipamiento`
--

LOCK TABLES `equipamiento` WRITE;
/*!40000 ALTER TABLE `equipamiento` DISABLE KEYS */;
/*!40000 ALTER TABLE `equipamiento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estiramiento`
--

DROP TABLE IF EXISTS `estiramiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estiramiento` (
  `idRutina` int NOT NULL,
  `descripcionEstiramiento` varchar(50) DEFAULT NULL,
  `duracion` int DEFAULT NULL,
  PRIMARY KEY (`idRutina`),
  CONSTRAINT `estiramiento_ibfk_1` FOREIGN KEY (`idRutina`) REFERENCES `rutina` (`idRutina`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estiramiento`
--

LOCK TABLES `estiramiento` WRITE;
/*!40000 ALTER TABLE `estiramiento` DISABLE KEYS */;
/*!40000 ALTER TABLE `estiramiento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `gimnasio`
--

DROP TABLE IF EXISTS `gimnasio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gimnasio` (
  `idGimnasio` int NOT NULL AUTO_INCREMENT,
  `paginaWeb` varchar(50) DEFAULT NULL,
  `claveAlmacenamiento` varchar(50) DEFAULT NULL,
  `claveRegistroEstancia` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `gimnasio`
--

LOCK TABLES `gimnasio` WRITE;
/*!40000 ALTER TABLE `gimnasio` DISABLE KEYS */;
/*!40000 ALTER TABLE `gimnasio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `maquina`
--

DROP TABLE IF EXISTS `maquina`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `maquina` (
  `idElemento` int NOT NULL,
  `idGimnasio` int DEFAULT NULL,
  `fechaFabricacion` date DEFAULT NULL,
  `fechaCompra` date DEFAULT NULL,
  `peso` int DEFAULT NULL,
  `tipo` varchar(50) DEFAULT NULL,
  `esElectrica` tinyint(1) DEFAULT NULL,
  `nombreElemento` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idElemento`),
  CONSTRAINT `maquina_ibfk_1` FOREIGN KEY (`idElemento`) REFERENCES `equipamiento` (`idElemento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maquina`
--

LOCK TABLES `maquina` WRITE;
/*!40000 ALTER TABLE `maquina` DISABLE KEYS */;
/*!40000 ALTER TABLE `maquina` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisocompuesto`
--

DROP TABLE IF EXISTS `permisocompuesto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permisocompuesto` (
  `idCompuesto` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) DEFAULT NULL,
  `descripcion` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idCompuesto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisocompuesto`
--

LOCK TABLES `permisocompuesto` WRITE;
/*!40000 ALTER TABLE `permisocompuesto` DISABLE KEYS */;
/*!40000 ALTER TABLE `permisocompuesto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisocompuesto_permisosimple`
--

DROP TABLE IF EXISTS `permisocompuesto_permisosimple`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permisocompuesto_permisosimple` (
  `idCompuesto` int NOT NULL,
  `idSimple` int NOT NULL,
  PRIMARY KEY (`idCompuesto`,`idSimple`),
  KEY `idSimple` (`idSimple`),
  CONSTRAINT `permisocompuesto_permisosimple_ibfk_1` FOREIGN KEY (`idCompuesto`) REFERENCES `permisocompuesto` (`idCompuesto`),
  CONSTRAINT `permisocompuesto_permisosimple_ibfk_2` FOREIGN KEY (`idSimple`) REFERENCES `permisosimple` (`idSimple`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisocompuesto_permisosimple`
--

LOCK TABLES `permisocompuesto_permisosimple` WRITE;
/*!40000 ALTER TABLE `permisocompuesto_permisosimple` DISABLE KEYS */;
/*!40000 ALTER TABLE `permisocompuesto_permisosimple` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permisosimple`
--

DROP TABLE IF EXISTS `permisosimple`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permisosimple` (
  `idSimple` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idSimple`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permisosimple`
--

LOCK TABLES `permisosimple` WRITE;
/*!40000 ALTER TABLE `permisosimple` DISABLE KEYS */;
/*!40000 ALTER TABLE `permisosimple` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rangohorario`
--

DROP TABLE IF EXISTS `rangohorario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rangohorario` (
  `idRangoHorario` int NOT NULL AUTO_INCREMENT,
  `horaDesde` time DEFAULT NULL,
  `horaHasta` time DEFAULT NULL,
  `cuota` float DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  PRIMARY KEY (`idRangoHorario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rangohorario`
--

LOCK TABLES `rangohorario` WRITE;
/*!40000 ALTER TABLE `rangohorario` DISABLE KEYS */;
/*!40000 ALTER TABLE `rangohorario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rangohorario_responsable`
--

DROP TABLE IF EXISTS `rangohorario_responsable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rangohorario_responsable` (
  `idRangoHorario` int NOT NULL,
  `idUsuario` int NOT NULL,
  PRIMARY KEY (`idRangoHorario`,`idUsuario`),
  KEY `idUsuario` (`idUsuario`),
  CONSTRAINT `rangohorario_responsable_ibfk_1` FOREIGN KEY (`idRangoHorario`) REFERENCES `rangohorario` (`idRangoHorario`),
  CONSTRAINT `rangohorario_responsable_ibfk_2` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rangohorario_responsable`
--

LOCK TABLES `rangohorario_responsable` WRITE;
/*!40000 ALTER TABLE `rangohorario_responsable` DISABLE KEYS */;
/*!40000 ALTER TABLE `rangohorario_responsable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `responsable`
--

DROP TABLE IF EXISTS `responsable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `responsable` (
  `idUsuario` int NOT NULL,
  `idGimnasio` int DEFAULT NULL,
  `nombreApellido` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `direccion` varchar(50) DEFAULT NULL,
  `nroDocumento` varchar(50) DEFAULT NULL,
  `genero` varchar(50) DEFAULT NULL,
  `fechaNacimiento` date DEFAULT NULL,
  `tipoResponsable` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `responsable_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `responsable_ibfk_2` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `responsable`
--

LOCK TABLES `responsable` WRITE;
/*!40000 ALTER TABLE `responsable` DISABLE KEYS */;
/*!40000 ALTER TABLE `responsable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rutina`
--

DROP TABLE IF EXISTS `rutina`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rutina` (
  `idRutina` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int DEFAULT NULL,
  `idGimnasio` int DEFAULT NULL,
  `fechaModificacion` date DEFAULT NULL,
  `dias` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idRutina`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `rutina_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `rutina_ibfk_2` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rutina`
--

LOCK TABLES `rutina` WRITE;
/*!40000 ALTER TABLE `rutina` DISABLE KEYS */;
/*!40000 ALTER TABLE `rutina` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `socio`
--

DROP TABLE IF EXISTS `socio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `socio` (
  `idUsuario` int NOT NULL,
  `idGimnasio` int DEFAULT NULL,
  `nombreApellido` varchar(100) DEFAULT NULL,
  `fechaNacimiento` date DEFAULT NULL,
  `genero` varchar(50) DEFAULT NULL,
  `nroDocumento` varchar(50) DEFAULT NULL,
  `ciudad` varchar(50) DEFAULT NULL,
  `direccion` varchar(50) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `obraSocial` varchar(50) DEFAULT NULL,
  `nombreUsuario` varchar(50) DEFAULT NULL,
  `contrasena` varchar(50) DEFAULT NULL,
  `pregunta` varchar(50) DEFAULT NULL,
  `respuesta` varchar(50) DEFAULT NULL,
  `plan` varchar(50) DEFAULT NULL,
  `estadoSocio` varchar(50) DEFAULT NULL,
  `fechaInicioActividades` date DEFAULT NULL,
  `fechaFinActividades` date DEFAULT NULL,
  `fechaNotificacion` date DEFAULT NULL,
  `respuestaNotificacion` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `socio_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `socio_ibfk_2` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `socio`
--

LOCK TABLES `socio` WRITE;
/*!40000 ALTER TABLE `socio` DISABLE KEYS */;
INSERT INTO `socio` VALUES (1,NULL,'Juan Pérez','1990-05-15','Hombre','12345678','San Lorenzo','Calle Falsa 123','3415551234','juan.perez@gmail.com','OSDE',NULL,NULL,'¿Color favorito?','Azul','Mensual','Nuevo','2024-11-01','2024-11-30',NULL,NULL),(4,NULL,'efwef','2024-11-14','Hombre','65645','No especificado','efsefdsf','54544','dsfsdfsdf@gmail.com','dsdf',NULL,NULL,'dsfdsf','dsfsdfsd','Mensual','Nuevo',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `socio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `turno`
--

DROP TABLE IF EXISTS `turno`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `turno` (
  `idTurno` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int DEFAULT NULL,
  `idGimnasio` int DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `hora` time DEFAULT NULL,
  `estadoTurno` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idTurno`),
  KEY `idUsuario` (`idUsuario`),
  KEY `idGimnasio` (`idGimnasio`),
  CONSTRAINT `turno_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`),
  CONSTRAINT `turno_ibfk_2` FOREIGN KEY (`idGimnasio`) REFERENCES `gimnasio` (`idGimnasio`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `turno`
--

LOCK TABLES `turno` WRITE;
/*!40000 ALTER TABLE `turno` DISABLE KEYS */;
/*!40000 ALTER TABLE `turno` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `idUsuario` int NOT NULL AUTO_INCREMENT,
  `nombreUsuario` varchar(50) NOT NULL,
  `contrasena` varchar(50) NOT NULL,
  PRIMARY KEY (`idUsuario`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'juan_perez','123456'),(2,'dqwdwq_asdsad','dfsadsad'),(3,'wdwqdwqd','wqdqwd'),(4,'sdfdsf','sdfsdf');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario_componente`
--

DROP TABLE IF EXISTS `usuario_componente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario_componente` (
  `idUsuario` int NOT NULL,
  `idComponente` int NOT NULL,
  PRIMARY KEY (`idUsuario`,`idComponente`),
  CONSTRAINT `usuario_componente_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario_componente`
--

LOCK TABLES `usuario_componente` WRITE;
/*!40000 ALTER TABLE `usuario_componente` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuario_componente` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-18 17:55:07
