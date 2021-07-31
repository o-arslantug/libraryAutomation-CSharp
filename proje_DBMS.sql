CREATE DATABASE IF NOT EXISTS proje_DBMS;
USE proje_DBMS;

CREATE TABLE IF NOT EXISTS `Personel` (
	`personel_id` INT,
    `personel_sifre` VARCHAR(30),    
	`personel_ad` VARCHAR(30),
	`personel_yetki` VARCHAR(30),
	PRIMARY KEY (`personel_id`)
);

CREATE TABLE IF NOT EXISTS `Kitap` (
	`kitap_id` INT NOT NULL AUTO_INCREMENT,
    `kitap_ad` VARCHAR(30),
    `kitap_isbn` VARCHAR(30),    	
	`kitap_basimYili` INT,
    `kitap_cevirmenId` INT,
	`kitap_yayinEviId` INT,
    `kitap_yazarId` INT,
    `kitap_kategori` VARCHAR(30),
	`kitap_konum` VARCHAR(30),
	`kitap_mevcutSayi` INT,
	PRIMARY KEY (`kitap_id`)
);
    
CREATE TABLE IF NOT EXISTS `Yazar` (
	`yazar_id` INT NOT NULL AUTO_INCREMENT,
	`yazar_ad` VARCHAR(30),
	PRIMARY KEY (`yazar_id`)
);    

CREATE TABLE IF NOT EXISTS `yayinEvi` (
	`yayinEvi_id` INT NOT NULL AUTO_INCREMENT,
	`yayinEvi_ad` VARCHAR(30),
	`yayinEvi_adres` VARCHAR(90),	
	PRIMARY KEY (`yayinEvi_id`)
);

CREATE TABLE IF NOT EXISTS `Cevirmen` (
	`cevirmen_id` INT NOT NULL AUTO_INCREMENT,
	`cevirmen_ad` VARCHAR(30),
	PRIMARY KEY (`cevirmen_id`)
);

CREATE TABLE IF NOT EXISTS `Uye` (
	`uye_id` INT NOT NULL AUTO_INCREMENT,
	`uye_ad` VARCHAR(30),
    `uye_gsm` VARCHAR(30),
	`uye_mail`  VARCHAR(45),
	`uye_adres`  VARCHAR(90),
    `uye_yasakDurumu` INT,
	PRIMARY KEY (`uye_id`)
);

CREATE TABLE IF NOT EXISTS `kitapDurum` (
	`kitapDurum_id` INT NOT NULL AUTO_INCREMENT,
	`uye_id` INT,
	`kitap_id` INT,
	`kitapDurum_verilisTarih` DATE,
	`kitapDurum_teslimTarih` DATE,
	PRIMARY KEY (`kitapDurum_id`)
);

INSERT INTO `Personel` VALUES 
(0, '0', 'root', 'Yönetici'),
(1, '1', 'leaf', 'Çalışan');

INSERT INTO `Kitap` VALUES 
(1, 'Simyacı', '9789750726439', 2010, 2, 1, 1, 'Roman', 'K4-S3-S7', 3),
(2, 'Cesur Yeni Dünya', '9789756902165', 2003, 3, 2, 2, 'Roman', 'K4-S4-S6', 2),
(3, 'Otomatik Portakal', '9786052957929', 2019, 4, 3, 3,'Roman', 'K4-S2-S8', 1),
(4, 'Yabancı', '9789750741272', 1996, 5, 1, 4,'Roman', 'K4-S3-S2', 0),
(5, 'Sisifos Söyleni', '9789750726231', 1997, 6, 1, 4,'Deneme', 'K2-S1-S2', 4),
(6, 'Tesla', '9786051715834', 2017, 7, 4, 5,'Biyografi & Oto Biyografi', 'K2-S3-S7', 3),
(7, 'Büyük Sorulara Kısa Yanıtlar', '9786051719092', 2018, 7, 4, 6,'Popüler Bilim', 'K3-S1-S3', 6),
(8, 'İnsan Nedir?', '9786052245095', 2020, 8, 5, 7, 'Felsefe', 'K5-S6-S1', 3),
(9, 'Zübük - Kağnı Gölgesindeki İt', '9789759038496', 2005, 1, 6, 8,'Roman', 'K4-S3-S7', 1);

INSERT INTO `yayinEvi` VALUES
(1, 'Can Yayınları', 'Adres Bilgisi'),
(2, 'İthaki Yayınları', 'Adres Bilgisi'),
(3, 'İş Bankası Kültür Yayınları', 'Adres Bilgisi'),
(4, 'Alfa Yayıncılık', 'Adres Bilgisi'),
(5, 'Flipper Yayıncılık', 'Adres Bilgisi'),
(6, 'Nesin Yayınevi', 'Adres Bilgisi');

INSERT INTO `Yazar` VALUES
(1, 'Paulo COELHO'),
(2, 'Aldous HUXLEY'),
(3, 'Anthony BURGESS'),
(4, 'Albert CAMUS'),
(5, 'Nikola TESLA'),
(6, 'Stephen HAWKING'),
(7, 'Mark TWAIN'),
(8, 'Aziz NESİN');

INSERT INTO `Cevirmen` VALUES
(1, 'Çevrilmemiş Kitap.'),
(2, 'Özdemir İNCE'),
(3, 'Ümit TOSUN'),
(4, 'Aziz ÜSTEL'),
(5, 'Samih TİRYAKİOĞLU'),
(6, 'Tahsin YÜCEL'),
(7, 'Mehmet Ata ARSLAN'),
(8, 'Esra Damla İPEKÇİ');

INSERT INTO `Uye` VALUES
(1, 'Test User 1', '0545 000 00 00', 'tu1@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(2, 'Test User 2', '0545 000 00 00', 'tu2@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(3, 'Test User 3', '0545 000 00 00', 'tu3@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(4, 'Test User 4', '0545 000 00 00', 'tu4@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(5, 'Test User 5', '0545 000 00 00', 'tu5@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(6, 'Test User 6', '0545 000 00 00', 'tu6@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(7, 'Test User 7', '0545 000 00 00', 'tu7@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0'),
(8, 'Test User 8', '0545 000 00 00', 'tu8@domain.com', 'A Mahallesi, B Sokak, C Apartmanı, D Kat, E Daire, Merkez/F', '0');

INSERT INTO `kitapDurum` VALUES
(1, 1, 1, '20-5-20', '20-6-11'),
(2, 2, 2, '20-5-07', '20-5-28'),
(3, 3, 3, '20-5-20', '20-6-11'),
(4, 5, 4, '20-5-16', '20-6-7'),
(5, 6, 5, '20-5-08', '20-5-29'),
(6, 7, 6, '20-5-20', '20-6-11'),
(7, 8, 7, '20-5-16', '20-6-7'),
(8, 1, 8, '20-5-11', '20-6-2'),
(9, 2, 9, '20-5-20', '20-6-11'),
(10, 3, 1, '20-5-16', '20-6-7'),
(11, 5, 2, '20-5-14', '20-6-5'),
(12, 6, 3, '20-5-20', '20-6-11'),
(13, 7, 4, '20-5-11', '20-6-2'),
(14, 8, 5, '20-5-11', '20-6-2'),
(15, 1, 6, '20-5-20', '20-6-11'),
(16, 2, 7, '20-5-13', '20-6-4'),
(17, 3, 8, '20-5-20', '20-6-11'),
(18, 4, 9, '20-5-16', '20-6-7');

ALTER TABLE kitap
add FOREIGN KEY (`kitap_yazarid`) REFERENCES Yazar(`yazar_id`),
add FOREIGN KEY (`kitap_cevirmenid`) REFERENCES Cevirmen(`cevirmen_id`),
add FOREIGN KEY (`kitap_yayineviid`) REFERENCES YayinEvi(`yayinevi_id`);

ALTER TABLE kitapdurum
add FOREIGN KEY (`uye_id`) REFERENCES uye(`uye_id`),
add FOREIGN KEY (`kitap_id`) REFERENCES kitap(`kitap_id`);