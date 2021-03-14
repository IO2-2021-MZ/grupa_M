use IO2_Restaurants

delete from [Address]


set identity_insert [Address] on

insert into [Address] 
	(id,city,street,postal_code)
values
	(1, 'Waraw', 'Aleje Jerozolimskie', '96500'),
	(2,'London', 'Aleje Jerozolimskie', '96500'),
	(3, 'New York', 'Aleje Jerozolimskie', '96500'),
	(4, 'Kijev', 'Aleje Jerozolimskie', '96500')
set identity_insert [Address] off


