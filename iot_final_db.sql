-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- 主機： 127.0.0.1
-- 產生時間： 
-- 伺服器版本： 10.4.8-MariaDB
-- PHP 版本： 7.3.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `iot_final_db`
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
(1, '小籠包', '', 40, 1),
(2, '三明治', '', 30, 1),
(3, '巧克力厚片', '', 20, 1),
(4, '牛奶', '', 30, 1),
(5, '奶茶', '', 25, 1),
(6, '蘿蔔糕', '', 30, 1),
(7, '火腿蛋吐司', '', 35, 1),
(8, '鐵板麵', '', 45, 1),
(9, '薯條', '', 30, 1),
(10, '漢堡', '', 45, 1),
(11, '紅茶', '', 20, 1),
(12, '蛋餅', '', 40, 1),
(13, '玉米濃湯', '', 35, 1),
(14, '鮪魚蛋餅', '', 30, 1);

-- --------------------------------------------------------

--
-- 資料表結構 `orders`
--

CREATE TABLE `orders` (
  `order_id` int(10) NOT NULL,
  `tableNum` int(10) NOT NULL,
  `order_state` int(10) NOT NULL,
  `orderTime` timestamp(6) NULL DEFAULT NULL,
  `checkTime` timestamp(6) NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  `table_name` int(50) DEFAULT NULL,
  `table_x` float NOT NULL,
  `table_y` float NOT NULL,
  `customerCount` int(10) NOT NULL,
  `table_state` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`id`);

--
-- 資料表索引 `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`);

--
-- 資料表索引 `service_details`
--
ALTER TABLE `service_details`
  ADD PRIMARY KEY (`service_details_id`);

--
-- 資料表索引 `tables`
--
ALTER TABLE `tables`
  ADD PRIMARY KEY (`table_id`);

--
-- 在傾印的資料表使用自動遞增(AUTO_INCREMENT)
--

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `menu`
--
ALTER TABLE `menu`
  MODIFY `id` int(100) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `orders`
--
ALTER TABLE `orders`
  MODIFY `order_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `service_details`
--
ALTER TABLE `service_details`
  MODIFY `service_details_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- 使用資料表自動遞增(AUTO_INCREMENT) `tables`
--
ALTER TABLE `tables`
  MODIFY `table_id` int(10) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
