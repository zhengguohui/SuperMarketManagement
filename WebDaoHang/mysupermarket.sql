/*
Navicat MySQL Data Transfer

Source Server         : 本地MySQL
Source Server Version : 50527
Source Host           : localhost:3306
Source Database       : mysupermarket

Target Server Type    : MYSQL
Target Server Version : 50527
File Encoding         : 65001

Date: 2013-02-01 15:33:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `smm_market`
-- ----------------------------
DROP TABLE IF EXISTS `smm_market`;
CREATE TABLE `smm_market` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `smm_user` int(11) DEFAULT NULL,
  `smm_shop` int(11) DEFAULT NULL,
  `smm_username` text,
  `smm_password` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of smm_market
-- ----------------------------
INSERT INTO `smm_market` VALUES ('3', '2', '2', '123', '456');
INSERT INTO `smm_market` VALUES ('4', '3', '2', '46', '123');

-- ----------------------------
-- Table structure for `smm_user`
-- ----------------------------
DROP TABLE IF EXISTS `smm_user`;
CREATE TABLE `smm_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `smm_email` text,
  `smm_password` text,
  `smm_tag` int(11) DEFAULT NULL,
  `smm_shop_name` text,
  `smm_shop_address` text,
  `smm_shop_keyword` text,
  `smm_shop_about` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of smm_user
-- ----------------------------
INSERT INTO `smm_user` VALUES ('2', 'kandisheng@163.com', '7c382186fb9526c83ffccbe156572c1a', '1', '北京沃尔玛', 'http://www.baidu.com', '789', '0000fgsdfgsdfgsdfg');
INSERT INTO `smm_user` VALUES ('3', 'kandisheng@126.com', '7c382186fb9526c83ffccbe156572c1a', '0', null, null, null, null);
INSERT INTO `smm_user` VALUES ('4', 'kandisheng@qq.com', '7c382186fb9526c83ffccbe156572c1a', '0', null, null, null, null);
INSERT INTO `smm_user` VALUES ('5', 'kandisheng@yeah.net', '7c382186fb9526c83ffccbe156572c1a', '1', null, null, null, null);
INSERT INTO `smm_user` VALUES ('6', 'kandisheng@qqqq.com', '7c382186fb9526c83ffccbe156572c1a', '0', null, null, null, null);
