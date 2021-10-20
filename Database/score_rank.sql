/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 80024
Source Host           : localhost:3306
Source Database       : score_rank

Target Server Type    : MYSQL
Target Server Version : 80024
File Encoding         : 65001

Date: 2021-10-20 22:58:39
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for highscore
-- ----------------------------
DROP TABLE IF EXISTS `highscore`;
CREATE TABLE `highscore` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(60) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `score` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of highscore
-- ----------------------------
INSERT INTO `highscore` VALUES ('1', 'Tom', '500');
INSERT INTO `highscore` VALUES ('2', 'Jerry', '200');
INSERT INTO `highscore` VALUES ('3', 'Lilys', '800');
INSERT INTO `highscore` VALUES ('4', 'Lucy EASY', '1000');
INSERT INTO `highscore` VALUES ('5', 'Samon', '550');
INSERT INTO `highscore` VALUES ('6', 'Coike', '600');
INSERT INTO `highscore` VALUES ('7', 'Mmaye', '780');
INSERT INTO `highscore` VALUES ('8', 'HieYe', '890');
INSERT INTO `highscore` VALUES ('9', 'IImerer', '990');
INSERT INTO `highscore` VALUES ('10', 'Coocke', '230');
INSERT INTO `highscore` VALUES ('11', 'Myed', '1100');
INSERT INTO `highscore` VALUES ('12', 'UieEn', '820');
INSERT INTO `highscore` VALUES ('13', 'Yogo', '400');
INSERT INTO `highscore` VALUES ('14', 'Saty', '340');
INSERT INTO `highscore` VALUES ('15', 'MaaeY', '330');
INSERT INTO `highscore` VALUES ('16', 'YEsr3', '340');
INSERT INTO `highscore` VALUES ('17', '1', '100');
INSERT INTO `highscore` VALUES ('18', '11', '11111');
