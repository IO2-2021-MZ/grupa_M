use IO2_Restaurants

delete from [Address]
delete from [User]



set identity_insert [Address] on

insert into [Address] 
	(id,city,street,postal_code)
values
	(1, 'Warsaw', 'Aleje Jerozolimskie', '96-500'),
	(2,'London', 'Aleje Jerozolimskie', '96-500'),
	(3, 'New York', 'Aleje Jerozolimskie', '96-500'),
	(4, 'Kijev', 'Aleje Jerozolimskie', '96-500')
set identity_insert [Address] off


set identity_insert [User] on

insert into [User] 
	(id,[name],surname,email,[role],creation_date,password_hash,address_id)
values
	(1, 'User1', 'Smith1', 'abc@s1.com',1,GETDATE(),'ajshgdja',1),
	(2, 'User2', 'Smith2', 'abc@s2.com',1,GETDATE(),'ajshgdja',2),
	(3, 'User3', 'Smith3', 'abc@s3.com',1,GETDATE(),'ajshgdja',3)
set identity_insert [User] off

