-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 10-04-2016 a las 23:37:05
-- Versión del servidor: 5.6.12-log
-- Versión de PHP: 5.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `lectorcodigo`
--
CREATE DATABASE IF NOT EXISTS `lectorcodigo` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `lectorcodigo`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE IF NOT EXISTS `clientes` (
  `Index` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Cliente` text NOT NULL,
  `Direccion` text NOT NULL,
  `Localidad` text NOT NULL,
  `C.P.` int(11) NOT NULL,
  `Provincia` text NOT NULL,
  `I.V.A.` text NOT NULL,
  `CUIT` text NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `historial_celular`
--

CREATE TABLE IF NOT EXISTS `historial_celular` (
  `Index` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` date NOT NULL,
  `Usuario` text NOT NULL,
  `Nro_Bobina` int(11) NOT NULL,
  `Estado` text NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `maquinista`
--

CREATE TABLE IF NOT EXISTS `maquinista` (
  `Index` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Maquinista` text CHARACTER SET utf8 COLLATE utf8_spanish_ci NOT NULL,
  `Ayudante` text CHARACTER SET utf8 COLLATE utf8_spanish_ci NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `observaciones_generales`
--

CREATE TABLE IF NOT EXISTS `observaciones_generales` (
  `Index` int(11) NOT NULL AUTO_INCREMENT,
  `Observacion` text NOT NULL,
  `Fecha` date NOT NULL,
  `Horario` time NOT NULL,
  `Maquinista` text NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE IF NOT EXISTS `productos` (
  `Index` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Tipo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci NOT NULL,
  `Metros` int(11) NOT NULL,
  PRIMARY KEY (`Index`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reg_2014`
--

CREATE TABLE IF NOT EXISTS `reg_2014` (
  `Numero_Bobina` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `estado_id` int(11) unsigned NOT NULL,
  `producto_id` int(11) unsigned NOT NULL,
  `cliente_id` int(11) unsigned NOT NULL,
  `maquinista_id` int(11) unsigned NOT NULL,
  `Peso` decimal(11,2) NOT NULL,
  `Observacion` text NOT NULL,
  `Gramaje` decimal(11,1) NOT NULL,
  `Espesor` int(11) NOT NULL,
  `Fin_Bob` time NOT NULL,
  `Formato` decimal(11,2) NOT NULL,
  `FECHA_SCANEO` date NOT NULL,
  `FECHA_FABRICACION` date NOT NULL,
  `celular` text NOT NULL,
  `turno` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Numero_Bobina`),
  KEY `Numero_Bobina` (`Numero_Bobina`),
  KEY `estado_id` (`estado_id`),
  KEY `producto_id` (`producto_id`),
  KEY `cliente_id` (`cliente_id`),
  KEY `maquinista_id` (`maquinista_id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE IF NOT EXISTS `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` text NOT NULL,
  `Password` text NOT NULL,
  `Privilegio` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
