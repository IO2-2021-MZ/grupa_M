use IO2_Restaurants

delete from [Address]
delete from [User]
delete from [Complaint]
delete from [Discount_Code]
delete from [Dish]
delete from [Order]
delete from [Order_Dish]
delete from [Restaurant]
delete from [Review]
delete from [Section]
delete from [User]


set identity_insert [Address] on

insert into [Address] 
	(id,city,street,post_code)
values
	(1, 'Warsaw', 'Aleje Jerozolimskie', '96-500'),
	(2,'London', 'Aleje Jerozolimskie', '96-500'),
	(3, 'New York', 'Aleje Jerozolimskie', '96-500'),
	(4, 'Kijev', 'Aleje Jerozolimskie', '96-500'),
	(5, 'Alabama', 'Aleje Jerozolimskie', '96-500'),
	(6, 'Jerozolimq', 'Aleje Jerozolimskie', '96-500'),
	(7, 'Kracow', 'Aleje Jerozolimskie', '96-500'),
	(8, 'Sosnowiec', 'Aleje Jerozolimskie', '96-500')
set identity_insert [Address] off


set identity_insert [Restaurant] on

insert into [Restaurant] 
	(id, [name], contact_information, rating, [state], owing, date_of_joining, aggregate_payment, address_id)
values
	(1, 'Kasza Jaglana Restauracja', 'kasza@jaglak.pl', 4.1, 0, 110.50, '21-03-2020T11:59:59', 1000.49, 4),
	(2, 'Restauracja Magdy Gessler', 'mg@tvn.pl', 4.1, 1, 110.50, '21-03-2020T11:59:59', 1000.49, 5),
	(3, 'Top Restauracja', 'rest@top.pl', 4.1, 2, 110.50, '21-03-2020T11:59:59', 1000.49, 6)
set identity_insert [Restaurant] off


set identity_insert [Sections] on

insert into [Section] 
	(id, [name], restaurant_id)
values
	(1, 'Kasze', 1),
	(2, 'Napoje', 1),
	(3, 'Zupy', 2),
	(4, 'Pierogi', 2),
	(5, 'Napoje gazowane', 3),
	(6, 'Napoje niegazowane', 3)
set identity_insert [Sections] off


set identity_insert [Dish] on

insert into [Dish] 
	(id, [name], [description], price, section_id )
values
	(1, 'Kasza jaglana', 'Najlepsza kasza na œwiecie!', 24.59, 1),
	(2, 'Kaszanka', 'Idealna na grilla!', 2.59, 1),
	(3, 'Woda', 'Naturalne orzeŸwienie', 1.59, 2),
	(4, 'Pomidorowa', 'Z pomidorów z nad Ba³tyku!', 23.56, 3),
	(5, 'Pierogi z miêsem', 'Miêêêêêêêêso', 3.56, 4),
	(6, 'Pierogi ze szpiankiem', 'Idealne dla wegetarian', 3.56, 4),
	(7, 'Pepsi', 'Nie cola', 4.49, 5)
set identity_insert [Dish] off


set identity_insert [User] on

insert into [User] 
	(id, [name], surname, email, is_restaurateur, is_administrator, creation_date, password_hash, address_id, restaurant_id)
values
	(1, 'Michael', 'Jackson', 'abc@s1.com', 1, 0, '21-03-2020T11:59:59', 'ajshgdja', 1, 1),
	(2, 'Elisabeth', 'Smith', 'abc@s2.com', 0, 1, '21-03-2020T11:59:59', 'ajshgdja', 2, null),
	(3, 'Daniel', 'Craig', 'abc@s3.com', 0, 0, '21-03-2020T11:59:59', 'ajshgdja', 3, null)
set identity_insert [User] off


insert into [Discount_Code] 
	(id, [percent], code, date_from, date_to, restaurant_id)
values
	(1, 10,  'JAGLAK', '21-03-2020T11:59:59', '21-03-2020T11:59:59', 1),
	(2, 20,  'JAGLAK-CODE', '21-03-2020T11:59:59', '21-03-2020T11:59:59', 1)
set identity_insert Discount_Code off

insert into [Order] 
	(id, payment_method, [state], [date], address_id, discount_code_id, customer_id, restaurant_id, employee_id)
values
	(1, 0,  2, '21-03-2020T11:59:59', 7, 1, 3, 1, null),
	(2, 1,  3, '21-03-2020T11:59:59', 8, null, 3, 1, null),
set identity_insert [Order] off