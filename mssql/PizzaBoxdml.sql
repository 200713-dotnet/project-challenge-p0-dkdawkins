use PizzaBoxDb;
go

select *
from Pizza.Crust;

select *
from Pizza.Size;

select *
from Pizza.Pizza;

select *
from Pizza.Topping;

select *
from Pizza.PizzaTopping;

select *
from Client.PBStore;

select *
from Client.PBUser;

select *
from Client.PBOrder;

select *
from Client.PizzaOrder;

/*
insert into Pizza.Crust(Name)
values ('Normal'), ('Stuffed');

insert into Pizza.Size(Name)
values ('S'), ('L');

insert into Pizza.Topping(Name)
values ('cheese'), ('pepperoni'), ('ham'), ('sausage'), ('pineapple');

insert into Pizza.Pizza(CrustId, SizeId, Name)
values
(1, 1, 'S, Normal, cheese, pepperoni, '),
(1, 1, 'S, Normal, cheese, ham, '),
(1, 1, 'S, Normal, cheese, sausage, '),
(1, 1, 'S, Normal, cheese, pineapple, '),
(1, 2, 'L, Normal, cheese, pepperoni, '),
(1, 2, 'L, Normal, cheese, ham, '),
(1, 2, 'L, Normal, cheese, sausage, '),
(1, 2, 'L, Normal, cheese, pineapple, '),
(2, 1, 'S, Stuffed, cheese, pepperoni, '),
(2, 1, 'S, Stuffed, cheese, ham, '),
(2, 1, 'S, Stuffed, cheese, sausage, '),
(2, 1, 'S, Stuffed, cheese, pineapple, '),
(2, 2, 'L, Stuffed, cheese, pepperoni, '),
(2, 2, 'L, Stuffed, cheese, ham, '),
(2, 2, 'L, Stuffed, cheese, sausage, '),
(2, 2, 'L, Stuffed, cheese, pineapple, ');


insert into Pizza.PizzaTopping(PizzaId, ToppingId)
values
(5, 1), (5, 2),
(6, 1), (6, 3),
(7, 1), (7, 4),
(8, 1), (8, 5),
(9, 1), (9, 2),
(10, 1), (10, 3),
(11, 1), (11, 4),
(12, 1), (12, 5),
(13, 1), (13, 2),
(14, 1), (14, 3),
(15, 1), (15, 4),
(16, 1), (16, 5),
(17, 1), (17, 2),
(18, 1), (18, 3),
(19, 1), (19, 4),
(20, 1), (20, 5);


insert into Client.PBStore(Name)
values ('PizzaHut'), ('Dominos')

delete pp
from Pizza.Pizza as pp
go*/