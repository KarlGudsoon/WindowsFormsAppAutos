-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-05-2026 a las 17:20:16
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_circulacion`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contribuyentes`
--

CREATE TABLE `contribuyentes` (
  `id` int(11) NOT NULL,
  `rut` varchar(12) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `nacionalidad` varchar(50) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `comuna` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contribuyentes`
--

INSERT INTO `contribuyentes` (`id`, `rut`, `nombre`, `apellido`, `nacionalidad`, `direccion`, `comuna`) VALUES
(1, '12345678-9', 'Juan', 'Pérez', 'Chilena', 'Av. Siempre Viva 123', 'Santiago'),
(2, '98765432-1', 'María', 'López', 'Chilena', 'Calle Falsa 456', 'Maipú'),
(3, '11111111-1', 'Carlos', 'Soto', 'Venezolana', 'Los Aromos 789dsa', 'Pudahuel'),
(4, '231sdasda231', '31', '3213', '2131', '123', '123');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `usuario` varchar(50) NOT NULL,
  `contrasena` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`id`, `usuario`, `contrasena`) VALUES
(1, 'admin', '1234');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vehiculos`
--

CREATE TABLE `vehiculos` (
  `id` int(11) NOT NULL,
  `patente` varchar(10) NOT NULL,
  `marca` varchar(50) NOT NULL,
  `modelo` varchar(50) NOT NULL,
  `color` varchar(30) NOT NULL,
  `anio` year(4) NOT NULL,
  `id_contribuyente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `vehiculos`
--

INSERT INTO `vehiculos` (`id`, `patente`, `marca`, `modelo`, `color`, `anio`, `id_contribuyente`) VALUES
(1, 'ABCD12', 'Toyota', 'Corolla', 'Blanco', '2020', 2),
(2, 'EFGH34', 'Chevrolet', 'Sail', 'Gris', '2019', 1),
(3, 'IJKL56', 'Hyundai', 'Accent', 'Negro', '2022', 1),
(4, '1', '1', '1', '1', '2001', 2),
(5, 'DASD', 'asdasd', 'sadas', 'asdas', '2001', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contribuyentes`
--
ALTER TABLE `contribuyentes`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `rut` (`rut`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `patente` (`patente`),
  ADD KEY `id_contribuyente` (`id_contribuyente`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contribuyentes`
--
ALTER TABLE `contribuyentes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `vehiculos`
--
ALTER TABLE `vehiculos`
  ADD CONSTRAINT `vehiculos_ibfk_1` FOREIGN KEY (`id_contribuyente`) REFERENCES `contribuyentes` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
