-- Active: 1715633441965@@127.0.0.1@3306@adaptable_shaman_540684_db
CREATE TABLE
  IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
  ) default charset utf8mb4 COMMENT '';

CREATE TABLE
  albums (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    creatorId VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description VARCHAR(500),
    coverImg VARCHAR(1000) NOT NULL,
    archived BOOLEAN NOT NULL DEFAULT false,
    category ENUM (
      "animals",
      "misc",
      "aesthetics",
      "crocs",
      "food",
      "games"
    ) DEFAULT "crocs",
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
  );

DROP TABLE albums;

CREATE TABLE
  pictures (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    imgUrl VARCHAR(1000) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    albumId INT NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE
  );

CREATE TABLE
  albumMembers (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    accountId VARCHAR(255) NOT NULL,
    albumId INT NOT NULL,
    FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE,
    UNIQUE (albumId, accountId) -- UNIQUE constraint means that you can only collaborate on an album one single time
  );

SELECT
  *
FROM
  accounts;

SELECT
  *
FROM
  albums;

INSERT INTO
  `albumMembers` (`accountId`, `albumId`)
VALUES
  ('65f87bc1e02f1ee243874743', 2);