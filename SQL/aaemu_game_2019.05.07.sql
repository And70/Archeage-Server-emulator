/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : localhost:3306
 Source Schema         : aaemu_game

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 07/05/2019 16:04:56
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cash_shop_item
-- ----------------------------
DROP TABLE IF EXISTS `cash_shop_item`;
CREATE TABLE `cash_shop_item`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'shop_id',
  `uniq_id` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '唯一ID',
  `cash_name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '出售名称',
  `main_tab` tinyint(3) UNSIGNED NULL DEFAULT 1 COMMENT '主分类1-6',
  `sub_tab` tinyint(3) UNSIGNED NULL DEFAULT 1 COMMENT '子分类1-7',
  `level_min` tinyint(3) UNSIGNED NULL DEFAULT 0 COMMENT '等级限制',
  `level_max` tinyint(3) UNSIGNED NULL DEFAULT 0 COMMENT '等级限制',
  `item_template_id` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '物品模板id',
  `is_sell` tinyint(1) UNSIGNED NULL DEFAULT 0 COMMENT '是否出售',
  `is_hidden` tinyint(1) UNSIGNED NULL DEFAULT 0 COMMENT '是否隐藏',
  `limit_type` tinyint(3) UNSIGNED NULL DEFAULT 0,
  `buy_count` smallint(5) UNSIGNED NULL DEFAULT 0,
  `buy_type` tinyint(3) UNSIGNED NULL DEFAULT 0,
  `buy_id` int(10) UNSIGNED NULL DEFAULT 0,
  `start_date` datetime(0) NULL DEFAULT '0001-01-01 00:00:00' COMMENT '出售开始',
  `end_date` datetime(0) NULL DEFAULT '0001-01-01 00:00:00' COMMENT '出售截止',
  `type` tinyint(3) UNSIGNED NULL DEFAULT 0 COMMENT '货币类型',
  `price` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '价格',
  `remain` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '剩余数量',
  `bonus_type` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '赠送类型',
  `bouns_count` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '赠送数量',
  `cmd_ui` tinyint(1) UNSIGNED NULL DEFAULT 0 COMMENT '是否限制一人一次',
  `item_count` int(10) UNSIGNED NULL DEFAULT 1 COMMENT '捆绑数量',
  `select_type` tinyint(3) UNSIGNED NULL DEFAULT 0,
  `default_flag` tinyint(3) UNSIGNED NULL DEFAULT 0,
  `event_type` tinyint(3) UNSIGNED NULL DEFAULT 0 COMMENT '活动类型',
  `event_date` datetime(0) NULL DEFAULT '0001-01-01 00:00:00' COMMENT '活动时间',
  `dis_price` int(10) UNSIGNED NULL DEFAULT 0 COMMENT '当前售价',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '此表来自于代码中的字段并去除重复字段生成。字段名称和内容以代码为准。' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cash_shop_item
-- ----------------------------
INSERT INTO `cash_shop_item` VALUES (20100011, 20100011, '1-1', 1, 1, 0, 0, 29176, 0, 0, 0, 0, 0, 0, '2019-05-01 14:10:08', '2055-06-16 14:10:12', 0, 874, 85, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100012, 20100012, '1-2', 1, 2, 0, 0, 29177, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100013, 20100013, '1-3', 1, 3, 0, 0, 29178, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100014, 20100014, '1-4', 1, 4, 0, 0, 29179, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100015, 20100015, '1-5', 1, 5, 0, 0, 29180, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100016, 20100016, '1-6', 1, 6, 0, 0, 29181, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100017, 20100017, '1-7', 1, 7, 0, 0, 29182, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100018, 20100018, '2-1', 2, 1, 0, 0, 29183, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100019, 20100019, '2-1', 2, 1, 0, 0, 29184, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100020, 20100020, '2-2', 2, 2, 0, 0, 29185, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100021, 20100021, '2-3', 2, 3, 0, 0, 29186, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100022, 20100022, '2-4', 2, 4, 0, 0, 29187, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100023, 20100023, '2-5', 2, 5, 0, 0, 29188, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100024, 20100024, '2-6', 2, 6, 0, 0, 29189, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100025, 20100025, '2-7', 2, 7, 0, 0, 29190, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100026, 20100026, '3-1', 3, 1, 0, 0, 29191, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100027, 20100027, '3-2', 3, 2, 0, 0, 29192, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100028, 20100028, '3-3', 3, 3, 0, 0, 29193, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100029, 20100029, '3-4', 3, 4, 0, 0, 29194, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100030, 20100030, '3-5', 3, 5, 0, 0, 29195, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100031, 20100031, '3-6', 3, 6, 0, 0, 29196, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100032, 20100032, '3-7', 3, 7, 0, 0, 29197, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100033, 20100033, '4-1', 4, 1, 0, 0, 29198, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100034, 20100034, '4-2', 4, 2, 0, 0, 29199, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100035, 20100035, '4-3', 4, 3, 0, 0, 29200, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100036, 20100036, '4-4', 4, 4, 0, 0, 29201, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100037, 20100037, '4-6', 4, 5, 0, 0, 29202, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100038, 20100038, '4-6', 4, 6, 0, 0, 29203, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100039, 20100039, '4-7', 4, 7, 0, 0, 29204, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100040, 20100040, '5-1', 5, 1, 0, 0, 29205, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100041, 20100041, '5-2', 5, 2, 0, 0, 29206, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100042, 20100042, '5-3', 5, 3, 0, 0, 29207, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100043, 20100043, '5-4', 5, 4, 0, 0, 29208, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100044, 20100044, '5-5', 5, 5, 0, 0, 29209, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100045, 20100045, '5-6', 5, 6, 0, 0, 29210, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100046, 20100046, '5-7', 5, 7, 0, 0, 29211, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100047, 20100047, '6-1', 6, 1, 0, 0, 29212, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100048, 20100048, '6-2', 6, 2, 0, 0, 29213, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100049, 20100049, '6-3', 6, 3, 0, 0, 29214, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100050, 20100050, '6-4', 6, 4, 0, 0, 29215, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100051, 20100051, '6-5', 6, 5, 0, 0, 29216, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100052, 20100052, '6-6', 6, 6, 0, 0, 29217, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);
INSERT INTO `cash_shop_item` VALUES (20100053, 20100053, '6-7', 6, 7, 0, 0, 29218, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '0001-01-01 00:00:00', 0);

SET FOREIGN_KEY_CHECKS = 1;
