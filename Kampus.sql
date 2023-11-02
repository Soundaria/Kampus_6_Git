
--user table
select * from [dbo].[User]

insert into [dbo].[User] values('soundaria05@gmail.com','sound@123','Admin');
insert into [dbo].[User] values('pradee@coursepedia.com','pradee@123','Trainer');
insert into [dbo].[User] values('archana@gmail.com','archana@123','User');

--Admin Table
select * from Admin;

insert into admin(adminid,name,password,contact,email,address,IsActive,createdAt) 
values(1,'Soundaria','sound@123','7339125450','soundaria05@gmail.com','Tirupur',1,getdate());

--Trainer Table
select * from Trainer;

insert into Trainer(trainerid,name,password,contact,email,address,isactive,createdat,adminid, yearOfExperience,Qualification)
values (2,'Pradee','pradee@123','9488878292','pradee@coursepedia.com',' Trichy',1,getdate(),1,5,'Sql,Java');

--User Table
select * from candidate;

insert into Candidate(candidateid,name,password,contact,email,address,isactive,createdat)
values (3,'Archana','archana@123','9443931792','archana@gmail.com','Chennai',1,getdate());
