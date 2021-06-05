use IO2_Restaurants

delete from [Order_Dish]
delete from [User_Rests]
delete from [Complaint]
delete from [Review]
delete from [Order]
delete from [Discount_Code]
delete from [User]
delete from [Dish]
delete from [Section]
delete from [Restaurant]
delete from [Address]


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
	(1, 'Kasza Jaglana Restauracja', 'kasza@jaglak.pl', 4.1, 0, 110.50, '2020-03-20T11:59:59', 1000.49, 4),
	(2, 'Restauracja Magdy Gessler', 'mg@tvn.pl', 4.1, 1, 110.50, '2020-03-20T11:59:59', 1000.49, 5),
	(3, 'Top Restauracja', 'rest@top.pl', 4.1, 2, 110.50, '2020-03-20T11:59:59', 1000.49, 6)
set identity_insert [Restaurant] off


set identity_insert [Section] on

insert into [Section] 
	(id, [name], restaurant_id)
values
	(1, 'Kasze', 1),
	(2, 'Napoje', 1),
	(3, 'Zupy', 2),
	(4, 'Pierogi', 2),
	(5, 'Napoje gazowane', 3),
	(6, 'Napoje niegazowane', 3)
set identity_insert [Section] off


set identity_insert [Dish] on

insert into [Dish] 
	(id, [name], [description], price, section_id )
values
	(1, 'Kasza jaglana', 'Najlepsza kasza na �wiecie!', 24.59, 1),
	(2, 'Kaszanka', 'Idealna na grilla!', 2.59, 1),
	(3, 'Woda', 'Naturalne orze�wienie', 1.59, 2),
	(4, 'Pomidorowa', 'Z pomidor�w z nad Ba�tyku!', 23.56, 3),
	(5, 'Z mi�sem', 'Mi��������so', 3.56, 4),
	(6, 'Ze szpinakiem ', 'Idealne dla wegetarian', 3.56, 4),
	(7, 'Pepsi', 'Nie cola', 4.49, 5)
set identity_insert [Dish] off


set identity_insert [User] on

insert into [User] 
	(id, [name], surname, email, role, creation_date, password_hash, address_id, restaurant_id)
values
	(1, 'Michael', 'Jackson', 'abc@s1.com', 1,'2020-03-20T11:59:59', '$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK', 1, 1),
	(2, 'Elisabeth', 'Smith', 'abc@s2.com', 2, '2020-03-20T11:59:59', '$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK', 2, null),
	(3, 'Daniel', 'Craig', 'abc@s3.com', 0, '2020-03-20T11:59:59', '$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK', 3, 1),
	(4, 'Marcin', 'Stanowski', 'najman@boxing.com', 3, getdate(), '$2a$11$yJdjz6naBO1kL3O0dc1dke4BOJSuXUm8yNmnkocFRIb/GYCPSehyK', 3, 1)

set identity_insert [User] off

set identity_insert [Discount_Code] on
insert into [Discount_Code] 
	(id, [percent], code, date_from, date_to, restaurant_id)
values
	(1, 10,  'JAGLAK', '2020-03-20T11:59:59', '2020-03-20T11:59:59', 1),
	(2, 20,  'JAGLAK-CODE', '2020-03-20T11:59:59', '2020-03-20T11:59:59', 1)
set identity_insert Discount_Code off


set identity_insert [Order] on
insert into [Order] 
	(id, payment_method, [state], [date], address_id, discount_code_id, customer_id, restaurant_id, employee_id)
values
	(1, 0,  2, '2020-03-20T11:59:59', 7, 1, 3, 1, null),
	(2, 1,  3, '2020-03-20T11:59:59', 8, null, 3, 1, null)
set identity_insert [Order] off


set identity_insert [Complaint] on
insert into [Complaint]
	(id, content, response, [open], customer_id, order_id)
values
	(1, 'Jedzenie by�o zimne', 'Przepraszamy za niedogodno��, do�o�ymy wszelkich stara�, �eby nast�pnym razem by�o lepiej', 0, 3, 1)
set identity_insert [Complaint] off

set identity_insert Review on
insert into Review
	(id, content, rating, customer_id, restaurant_id)
values
	(1, 'Jedzenie z restauracji jest zimne', 1.0, 3, 1) 
set identity_insert Review off


set identity_insert Order_Dish on
insert into Order_Dish
	(id, dish_id, order_id)
values
	(1, 1, 2),
	(2, 1, 1),
	(3, 2, 1)
set identity_insert Order_Dish off


set identity_insert User_Rests on
insert into User_Rests
	(id, user_id, restaurant_id)
values
	(1, 3, 1),
	(2, 3, 2),
	(3, 2, 1),
	(4, 2, 1)
set identity_insert User_Rests off