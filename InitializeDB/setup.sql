create database mangaldb;

use mangaldb;

CREATE TABLE Users(
  Id varchar(45) NOT NULL PRIMARY KEY,
  Username varchar(60),
  PasswordHash varchar(45) NOT NULL
);

CREATE TABLE Mangas(
  Id varchar(45) NOT NULL PRIMARY KEY,
  MangaTitle varchar(200)
);

CREATE TABLE Comments(
  /*One user may leave many comments*/
  Id varchar(45) NOT NULL PRIMARY KEY,
  CommentMessage varchar(1000) NOT NULL,
  MangaId varchar(45) NOT NULL,
  UserId varchar(45) NOT NULL,
  FOREIGN KEY (MangaId)
    REFERENCES Mangas(Id)
    /*There is no reason to have comments for deleted manga*/
    ON DELETE CASCADE,
  FOREIGN KEY (UserId)
    REFERENCES Users(Id)
    /*May be changed later*/
    ON DELETE CASCADE
);

CREATE TABLE PICTURES(
  Id varchar(45) NOT NULL PRIMARY KEY,
  MangaId varchar(45) NOT NULL,
  PictureOrder INT NOT NULL,
  /*Location relative to wwwroot*/
  ImageLocation varchar(200),
  FOREIGN KEY (MangaId)
    REFERENCES Mangas(Id)
    /*On deletion of manga it should forget all of the pictueres in contained*/
    ON DELETE CASCADE
);