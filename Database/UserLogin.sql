DROP DATABASE Skrabbl
create database Skrabbl

use Skrabbl
create table Users(
	Id int identity primary key,
	Username varchar not null,
	Email varchar not null,
)
create table Logins(
	Id int,
	HashedPassword varchar not null,
	Salt varchar not null,
	foreign key (Id) references Users(Id)
)


