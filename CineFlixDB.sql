CREATE DATABASE CineFlixDB
GO

USE CineFlixDB
Go 

CREATE TABLE Roles
(
	[RoleId] TINYINT CONSTRAINT pk_RoleId PRIMARY KEY IDENTITY,
	[RoleName] VARCHAR(20) CONSTRAINT uq_RoleName UNIQUE
)
GO 

SET IDENTITY_INSERT Roles ON
INSERT INTO Roles (RoleId, RoleName) VALUES (1, 'Admin')
INSERT INTO Roles (RoleId, RoleName) VALUES (2, 'Customer')
SET IDENTITY_INSERT Roles OFF

CREATE TABLE Users (
    [UserId] int CONSTRAINT pk_UserId PRIMARY KEY IDENTITY (1000,1),
    [FirstName] varchar(50) NOT NULL,
    [LastName] varchar(50) NOT NULL,
    [PhoneNumber] varchar(10) CONSTRAINT uq_phoneNumber UNIQUE,
    [EmailId] varchar(50) CONSTRAINT uq_EmailId UNIQUE,
    [UserPassword] varchar(50) CONSTRAINT uq_pwd UNIQUE,
	[RoleId] TINYINT CONSTRAINT fk_RoleId REFERENCES Roles(RoleId),
	[PlanType] varchar(20) CONSTRAINT chk_Gender CHECK([PlanType]='Basic' OR [PlanType]='Platinum' OR [PlanType]='Gold') NOT NULL,
	[MembershipStartDate] date NOT NULL,
	[MembershipEndDate] date NOT NULL, 
	[IsDeleted] bit DEFAULT '0' NOT NULL
);
GO

INSERT INTO USERS(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES ('Damon','Salvatore',9886543221,'damonsalvatore@yahoo.com','Damon@123',1,'Gold','12/11/2023','12/12/2023')
INSERT INTO USERS(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES ('Stefan','Salvatore',9889083221,'stefansalvatore@yahoo.com','Stefan@123',2,'Platinum','11/14/2023','12/14/2023')
INSERT INTO USERS(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES ('Elena','Gilbert',7659083221,'elenag@yahoo.com','Elena@123',2,'Basic','11/18/2023','12/18/2023')
INSERT INTO USERS(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES ('Jon','Snow',7876890878,'jon@gmail.com','Jonsnow@123',1,'Gold','11/20/2023','12/20/2023')
INSERT INTO USERS(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES ('Sansa','Stark',8903890878,'sansa@gmail.com','Sansastark@123',2,'Platinum','11/25/2023','12/25/2023')

Select * from Users
Go

CREATE TABLE Movies
(
     [MovieId] INT CONSTRAINT pk_MovieId PRIMARY KEY IDENTITY (2000,1),
     [UserId] int FOREIGN KEY REFERENCES USERS(UserId),
     [MovieName] varchar(50) NOT NULL,
     [MovieDesc] varchar(Max) NOT NULL,
     [Genres] varchar(20) NOT NULL,
     [ImageUrl] varchar(255) NOT NULL,
	 [VideoUrl] varchar(255) NOT NULL,
     [MovieLanguage] varchar(20) NOT NULL,
     [Duration] INT NOT NULL,
	 [isDeleted] bit DEFAULT '0' NOT NULL
)
GO

INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1000,'Yeh Jawani hai diwani','Kabir Thapar aka Bunny (Ranbir Kapoor) is a carefree boy whose dream is to roam around the world, he doesnt believe in the marital bond. He lives with his father (Farukh Sheikh) and stepmother (Tanvi Azmi). Kabir wants to live life on his own terms and doesnt share any kind of emotional bond with his stepmother','Rom-com'
,'https://m.media-amazon.com/images/M/MV5BODA4MjM2ODk4OF5BMl5BanBnXkFtZTcwNDgzODk1OQ@@._V1_.jpg','https://www.youtube.com/watch?v=cuD7P-32wQk','Hindi',159)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1003,'Leo','Parthiban "Parthi" is an animal rescuer and a café owner in Theog, Himachal Pradesh living with his wife, Sathya, son Siddharth "Siddhu" and daughter Mathi "Chintu". One day, Parthis friend, Joshy Andrews, a forest ranger, calls him to help tame a spotted hyena threatening the townsfolk.','Rom-com'
,'https://static.toiimg.com/photo/msid-100276084/100276084.jpg?187396','https://www.youtube.com/watch?v=Po3jStA673Ek','Tamil',164)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1002,'Jawan','Azad is the jailer of a womens prison in Mumbai who hijacks a Mumbai Metro train along with a group of six inmates: Lakshmi, Eeram, Ishkra, Kalki, Helena and Janhvi. Azad negotiates with NSG officer Narmada Rai to ask the Agriculture Minister to send ₹400 billion in exchange for the passengers lives.','Action'
,'https://upload.wikimedia.org/wikipedia/en/3/39/Jawan_film_poster.jpg','https://www.youtube.com/watch?v=COv52Qyctws','Hindi',180)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1004,'Vikram','Police Chief Jose summons Amar and his black-ops team to bring justice to a group of masked vigilantes responsible for the deaths of Stephen Raj (recently released following his arrest three months earlier for helping gangsters Adaikalam and Anbu), ACP Prabhanjan and his foster father Karnan.','Action'
,'https://upload.wikimedia.org/wikipedia/en/thumb/9/93/Vikram_2022_poster.jpg/220px-Vikram_2022_poster.jpg','https://www.youtube.com/watch?v=OKBMCL-frPU','Tamil',174)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1002,'Jaane Tu... Ya Jaane Na','Best friends Jai and Aditi make for a perfect couple but refuse to consider a romantic relationship. However, when they start dating other people, they realise that they are actually in love.','Rom-com'
,'https://upload.wikimedia.org/wikipedia/en/7/7b/Jaane_Tu..._Ya_Jaane_Na.JPG','https://www.youtube.com/watch?v=NYqqFeLzRiI','Tamil',160)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1004,'Ek Thi Daayan','Bobo, a famous magician, plans to marry his girlfriend. However, he is secretly scarred by persistent hallucinations of his dead sister and is forced to seek psychiatric help.','Horror'
,'https://upload.wikimedia.org/wikipedia/en/2/2c/Ek_Thi_Poster.jpg','https://www.youtube.com/watch?v=UN5Ss-EV_qc','Hindi',184)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1002,'Surarai Potru','Maara, a young man from a remote village, dreams of launching his own airline service. However, he must overcome several obstacles and challenges in order to be successful in his quest.','Drama'
,'https://upload.wikimedia.org/wikipedia/en/thumb/7/73/Soorarai_Pottru_Album_Cover.jpg/220px-Soorarai_Pottru_Album_Cover.jpg','https://www.youtube.com/watch?v=fa_DIwRsa9o','Tamil',175)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1001,'Rocky Aur Rani Kii Prem Kahaani','Flamboyant Punjabi Rocky and intellectual Bengali journalist Rani fall in love despite their differences. After facing family opposition, they decide to live with each others families for three months before getting married.','Rom-com'
,'https://upload.wikimedia.org/wikipedia/en/thumb/6/65/Rocky_Aur_Rani_Ki_Prem_Kahani.jpg/220px-Rocky_Aur_Rani_Ki_Prem_Kahani.jpg','https://www.youtube.com/watch?v=6mdxy3zohEk','Hindi',186)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1004,'Jab Harry Met Sejal','Sejal, an exuberant woman, loses her engagement ring on a trip. However, when she enlists the help of Harry, a flirtatious tour guide, to retrieve it, she eventually falls in love with him.','Rom-com'
,'https://upload.wikimedia.org/wikipedia/en/d/d1/Jab_Harry_Met_Sejal_poster.jpg','https://www.youtube.com/watch?v=W5MZevEH5Ns','Hindi',178)
INSERT INTO Movies(UserId,MovieName,MovieDesc,Genres,ImageUrl,VideoUrl,MovieLanguage,Duration) VALUES (1003,'Baahubali','Sivudu, an adventurous young man who helps his love Avantika rescue Devasena, the former queen of Mahishmati who is now a prisoner under the tyrannical rule of king Bhallaladeva. The story concludes in Baahubali 2: The Conclusion.','Action'
,'https://upload.wikimedia.org/wikipedia/en/thumb/5/5f/Baahubali_The_Beginning_poster.jpg/220px-Baahubali_The_Beginning_poster.jpg','https://www.youtube.com/watch?v=3NQRhE772b0','Telugu',198)

Select * from Movies
Go

CREATE TABLE TVShows
(
     [ShowId] INT NOT NULL CONSTRAINT pk_ShowId PRIMARY KEY IDENTITY (5000,1),
     [UserId] int NOT NULL FOREIGN KEY REFERENCES USERS(UserId),
     [Show] varchar(50) NOT NULL,
     [ShowDesc] varchar(Max) NOT NULL,
     [Genres] varchar(20) NOT NULL,
     [ImageUrl] varchar(255) NOT NULL,
	 [VideoUrl] varchar(255) NOT NULL,
     [ShowLanguage] varchar(20) NOT NULL,
     [EpisodeCount] INT NOT NULL,
	 [isDeleted] bit  DEFAULT '0' NOT NULL
)
GO

INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1000,'Girl In The City','This show is about a young girl who comes to Mumbai to live her dreams and aim at her aspirations. It is about her journey in the big city.','Rom-com'
,'https://th.bing.com/th/id/OIP.qb4W19aHRngJ0D6CZKrkKAHaD6?pid=ImgDet&rs=1','https://www.youtube.com/watch?v=9sV2Qet2now&list=PLfyZfwWJwO_ilLPp0ZASauXYYtkYyaUDO','Hindi',13)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1003,'Kaala Paani','When a mysterious illness descends upon the Andaman and Nicobar Islands, a desperate fight for survival collides with a race to find a cure.','Thriller'
,'https://upload.wikimedia.org/wikipedia/en/2/2f/Kaala_Paani_poster.png','https://www.youtube.com/watch?v=6ph4WPZIrbM&pp=ygUSa2FhbGEgcGFhbmkgdGVhc2Vy','Hindi',7)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1002,'Little Things','The story revolves around Kavya Kulkarni (Palkar) and Dhruv Vats (Sehgal), a couple in a live-in relationship in Mumbai. The series progresses with everyday life explored through conversation between the couple.','Rom-com'
,'https://www.iwmbuzz.com/wp-content/uploads/2018/10/little920.jpg','https://www.youtube.com/playlist?list=PL4x7Of-X4XhgNBVfFpd1N4cSU_X1x96gU','Hindi',8)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1004,'The Family Man','A working man from the National Investigation Agency tries to protect the nation from terrorism, but he also needs to keep his family safe from his secret job.','Action'
,'https://upload.wikimedia.org/wikipedia/en/thumb/d/dc/The_Family_Man.jpeg/330px-The_Family_Man.jpeg','https://www.youtube.com/watch?v=XatRGut65VI&pp=ygUOdGhlIGZhbWlseSBtYW4%3D','Hindi',10)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1002,'Fingertip','The lives of six individuals get severely affected by the ever-changing digital space. While some are predators, some are victims of cybercrime and digital depression.','Crime'
,'https://upload.wikimedia.org/wikipedia/en/thumb/3/38/Fingertip_%28web_series%29.jpg/330px-Fingertip_%28web_series%29.jpg','https://www.youtube.com/watch?v=8AvnVmBNyp0','Tamil',5)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1004,'Aspirants','The story revolves around Abhilash, Guri, and SK, three friends who are preparing for the UPSC exam.','Drama'
,'https://upload.wikimedia.org/wikipedia/en/f/fe/TVF_Aspirants_Poster.jpeg','https://www.youtube.com/watch?v=ViOutJ0kuJY','Hindi',5)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1002,'Iru Dhuruvam','The series consists of hard-core police investigation stories dealing with investigation, detection and suspense.','Thriller'
,'https://upload.wikimedia.org/wikipedia/en/1/1d/Iru_Dhuruvam_web_series_poster.jpg','https://www.youtube.com/watch?v=f7XdqS6KzHA','Tamil',9)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1001,'Indian Matchmaking','Sima Taparia, a marriage consultant from Mumbai who uses preferences from the people, their parents, and her years of matchmaking experience.','Drama'
,'https://upload.wikimedia.org/wikipedia/commons/thumb/c/c6/Indian_Matchmaking_logo.png/330px-Indian_Matchmaking_logo.png','https://www.youtube.com/watch?v=aZS2KbLAy5Y&pp=ygUSaW5kaWFuIG1hdGNobWFraW5n','Hindi',8)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1004,'A suitable boy','A vast, panoramic tale charting the fortunes of four large families and exploring India and its rich and varied culture at a crucial point in its history.','Drama'
,'https://en.wikipedia.org/wiki/A_Suitable_Boy_(TV_series)#/media/File:A_Suitable_Boy_Title_Card.png','https://www.youtube.com/watch?v=P9KxAJAWhGc','Bengali',6)
INSERT INTO TVShows(UserId,Show,ShowDesc,Genres,ImageUrl,VideoUrl,ShowLanguage,EpisodeCount) VALUES (1003,'Typewriter','A group of curious children belong to a ghost club and embark on their first mission to find a ghost in an old haunted villa near their neighborhood.','Horror'
,'https://upload.wikimedia.org/wikipedia/en/b/b8/Typewriter_Webseries.jpg','https://www.youtube.com/watch?v=mmyNUPvEF5M','Hindi',5)

Select * from TVShows
Go

CREATE FUNCTION ufn_CheckEmailId
(
	@EmailId VARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	
	DECLARE @ReturnValue BIT
	IF NOT EXISTS (SELECT EmailId FROM Users WHERE EmailId=@EmailId)
		SET @ReturnValue=1
	ELSE 
		SET @ReturnValue=0
	RETURN @ReturnValue
END
GO

CREATE FUNCTION ufn_ValidateUserCredentials
(
	@EmailId VARCHAR(50),
    @UserPassword VARCHAR(15)
)
RETURNS INT
AS
BEGIN
	DECLARE @RoleId INT
	SELECT @RoleId=RoleId FROM Users WHERE EmailId=@EmailId AND UserPassword=@UserPassword
	RETURN @RoleId
END
GO

CREATE PROCEDURE [dbo].[usp_RegisterUser]    
(    
@FirstName VARCHAR(20),    
@LastName VARCHAR(20),    
@PhoneNumber VARCHAR(10),    
@EmailId VARCHAR(50),    
@UserPassword VARCHAR(15),    
@PlanType VARCHAR(20),    
@MembershipStartDate DATE,    
@MembershipEndDate DATE,     
@UserId BIGINT OUT,  
@Result INT OUT  
)    
AS    
BEGIN    
DECLARE @RoleId INT    
SET @UserId=0  
  SET @Result=0  
BEGIN TRY    
  IF (LEN(@EmailId)<4 OR LEN(@EmailId)>50 OR (@EmailId IS NULL))    
   SET @Result=-1    
  ELSE IF (LEN(@UserPassword)<8 OR LEN(@UserPassword)>15 OR (@UserPassword IS NULL))    
   SET @Result= -2    
  ELSE IF (@PlanType<>'Basic' AND @PlanType<>'Platinum' AND @PlanType<>'Gold' OR (@PlanType Is NULL))    
   SET @Result= -3      
  ELSE IF (@MembershipStartDate<CAST(GETDATE() AS DATE) OR (@MembershipStartDate IS NULL))    
   SET @Result= -4     
  ELSE IF (DATEDIFF(DAY,@MembershipStartDate,@MembershipEndDate)>365 OR @MembershipEndDate IS NULL)    
   SET @Result= -5    
  ELSE IF (@FirstName IS NULL)    
   SET @Result= -6    
  ELSE IF (@LastName IS NULL)    
   SET @Result= -7    
  ELSE IF (@PhoneNumber IS NULL OR LEN(@PhoneNumber)>10)    
   SET @Result= -8    
  ELSE  
   BEGIN  
    SET @RoleId = (SELECT RoleId FROM Roles WHERE RoleName='Customer')    
    INSERT INTO Users(FirstName,LastName,PhoneNumber,EmailId,UserPassword,RoleId,PlanType,MembershipStartDate,MembershipEndDate) VALUES     
    (@FirstName,@LastName,@PhoneNumber,@EmailId,@UserPassword,@RoleId,@PlanType, @MembershipStartDate, @MembershipEndDate)    
    SET @UserId = IDENT_CURRENT('Users')    
    SET @Result =1  
   END  
  RETURN @Result    
END TRY    
BEGIN CATCH    
  SET @Result =-99   
  RETURN @Result   
END CATCH    
END