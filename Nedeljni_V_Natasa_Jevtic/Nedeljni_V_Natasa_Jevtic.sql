IF DB_ID('BetweenUs') IS NULL
    create database BetweenUs;
GO	
use BetweenUs
--Deleting tables and views, if they exist
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblLikedPost')
	drop table tblLikedPost;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblPost')
	drop table tblPost;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblRequest')
	drop table tblRequest;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblUser')
	drop table tblUser;
IF EXISTS(select * FROM sys.views where name = 'vwPost')
	drop view vwPost;
IF EXISTS(select * FROM sys.views where name = 'vwFriendPost')
	drop view vwFriendPost;
IF EXISTS(select * FROM sys.views where name = 'vwRequest')
	drop view vwRequest;
IF EXISTS(select * FROM sys.views where name = 'vwUser')
	drop view vwUser;
GO
create table tblUser(
UserId int identity(1,1) PRIMARY KEY,
FirstName varchar(40) NOT NULL,
LastName varchar(40) NOT NULL,
Username varchar(40) UNIQUE NOT NULL,
Password varchar(40) NOT NULL,
DateOfBirth date NOT NULL,
Gender varchar(20) NOT NULL,
Profession varchar(100),
Location varchar(100),
MarriageStatus varchar(20),
PhoneNumber varchar(13) 
);
create table tblRequest(
RequestId int identity(1,1) PRIMARY KEY,
SenderId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
RecipientId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
Status varchar(10) NOT NULL
);
create table tblPost(
PostId int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
PostContent varchar(100) NOT NULL,
DateOfPost date NOT NULL,
NumberOfLikes int NOT NULL,
);
create table tblLikedPost(
Id int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
PostId int FOREIGN KEY REFERENCES tblPost(PostId) NOT NULL,
);


GO
create view vwUser as
select *, FirstName + ' ' + LastName 'User'
from tblUser;
GO
create view vwRequest as
select r.*, s.FirstName + ' ' + s.LastName 'Sender', u.FirstName + ' ' + u.LastName 'Recipient',
 s.Username 'SenderUsename', u.Username 'RecipientUsername'
from tblRequest r
INNER JOIN tblUser s
ON r.SenderId = s.UserId
INNER JOIN tblUser u
ON r.RecipientId = u.UserId;
GO
create view vwFriendPost as
select p.*, r.RecipientId, r.SenderId,  u.FirstName + ' ' + u.LastName 'User', u.Username
from tblPost p
INNER JOIN tblRequest r
ON p.UserId = r.SenderId OR p.UserId = r.RecipientId
INNER JOIN tblUser u
ON p.UserId = u.UserId
WHERE r.Status LIKE 'Accepted';
GO
create view vwPost as
select p.*, u.FirstName + ' ' + u.LastName 'User', u.Username, l.UserId 'User id who liked', us.Username 'User who liked'
from tblPost p
INNER JOIN tblUser u
ON p.UserId = u.UserId
LEFT JOIN tblLikedPost l
ON p.PostId = l.PostId
LEFT JOIN tblUser us
ON l.UserId = us.UserId;