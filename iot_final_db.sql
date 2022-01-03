-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- 主機： 127.0.0.1
-- 產生時間： 2022-01-03 08:46:25
-- 伺服器版本： 10.4.20-MariaDB
-- PHP 版本： 7.4.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫: `iot_final_db`
--

-- --------------------------------------------------------

--
-- 資料表結構 `menu`
--

CREATE TABLE `menu` (
  `id` int(100) NOT NULL,
  `name` varchar(50) NOT NULL,
  `content` varchar(100) NOT NULL,
  `price` int(4) NOT NULL,
  `provide` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 傾印資料表的資料 `menu`
--

INSERT INTO `menu` (`id`, `name`, `content`, `price`, `provide`) VALUES
(4, 'aaa', 'abc', 55, 1);

-- --------------------------------------------------------

--
-- 資料表結構 `orders`
--

CREATE TABLE `orders` (
  `order_id` int(10) NOT NULL,
  `tableNum` int(10) NOT NULL,
  `order_state` int(10) NOT NULL,
  `orderTime` timestamp(6) NOT NULL DEFAULT current_timestamp(6),
  `checkTime` timestamp(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 傾印資料表的資料 `orders`
--

INSERT INTO `orders` (`order_id`, `tableNum`, `order_state`, `orderTime`, `checkTime`) VALUES
(1, 8888, 1, '0000-00-00 00:00:00.000000', '0000-00-00 00:00:00.000000'),
(2, 31, 0, '0000-00-00 00:00:00.000000', '0000-00-00 00:00:00.000000');

-- --------------------------------------------------------

--
-- 資料表結構 `order_details`
--

CREATE TABLE `order_details` (
  `order_id` int(10) NOT NULL,
  `dish_id` int(10) NOT NULL,
  `order_details_state` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- 資料表結構 `service_details`
--

CREATE TABLE `service_details` (
  `service_details_id` int(10) NOT NULL,
  `table_id` int(10) NOT NULL,
  `service_details_state` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 傾印資料表的資料 `service_details`
--

INSERT INTO `service_details` (`service_details_id`, `table_id`, `service_details_state`) VALUES
(1, 999999, 0),
(2, 53, 1);

-- --------------------------------------------------------

--
-- 資料表結構 `tables`
--

CREATE TABLE `tables` (
  `table_id` int(10) NOT NULL,
  `table_x` float NOT NULL,
  `table_y` float NOT NULL,
  `customerCount` int(10) NOT NULL,
  `table_state` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
