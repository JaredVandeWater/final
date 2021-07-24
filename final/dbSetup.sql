CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  img VARCHAR(255) NOT NULL,
  views int,
  shares int,
  keeps int,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  isPrivate TINYINT DEFAULT 0,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS vaultkeep(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId int,
  keepId int,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY(keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8;
SELECT
  k.*,
  a.*
FROM
  keeps k
  JOIN accounts a ON k.creatorId = a.id;
DROP TABLE accounts;
DROP table vaults;
drop TABLE keeps;
drop table vaultkeep;