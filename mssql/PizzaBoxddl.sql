use PizzaBoxDb;
go

-- CREATE

/*create database PizzaBoxDb;
go*/

create schema Pizza;
go

create schema Client;
go

-- Pizza tables
create table Pizza.Pizza
(
  PizzaId int not null identity(1,1),
  CrustId int not null,
  SizeId int not null,
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_PizzaId primary key (PizzaId),
);

create table Pizza.Crust
(
  CrustId int not null identity(1,1),
  [Name] nvarchar(100) not null,
  Active bit not null default 1,
  constraint PK_CrustId primary key (CrustId),
);

create table Pizza.Size
(
  SizeId int not null identity(1,1),
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_SizeId primary key (SizeId),
);

create table Pizza.Topping
(
  ToppingId int not null identity(1,1),
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_ToppingId primary key (ToppingId),
);

create table Pizza.PizzaTopping
(
  PizzaToppingId int not null identity(1,1),
  PizzaId int not null,
  ToppingId int not null,
  Active bit not null default 1,
  constraint PK_PizzaToppingId primary key (PizzaToppingId),
);
go

--Client tables
create table Client.PBUser
(
  UserId int not null identity(1,1),
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_UserId primary key (UserId),
);

create table Client.PBStore
(
  StoreId int not null identity(1,1),
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_StoreId primary key (StoreId),
);

create table Client.PBOrder
(
  OrderId int not null identity(1,1),
  UserId int not null,
  StoreId int not null,
  [Name] nvarchar(250) not null,
  Active bit not null default 1,
  constraint PK_OrderId primary key (OrderId),
);

create table Client.PizzaOrder
(
  PizzaOrderId int not null identity(1,1),
  OrderId int not null,
  PizzaId int not null,
  Active bit not null default 1,
  constraint PK_PizzaOrderId primary key (PizzaOrderId),
);
go

-- ALTER
/*alter table Pizza.PizzaTopping
  add constraint PK_PizzaTopping_PizzaToppingId primary key (PizzaToppingId)

alter table Client.PizzaOrder
  add constraint PK_PizzaOrder_PizzaOrderId primary key (PizzaOrderId)*/


-- Crust to Pizza: One to many
/*alter table Pizza.Pizza
  add CrustId int not null
go*/
alter table Pizza.Pizza
  add constraint FK_Pizza_CrustId foreign key (CrustId) references Pizza.Crust(CrustId)
go
-- Size to Pizza: One to many
/*alter table Pizza.Pizza
  add SizeId int not null
go*/
alter table Pizza.Pizza
  add constraint FK_Pizza_SizeId foreign key (SizeId) references Pizza.Size(SizeId)
go

-- Topping to Pizza: Many to Many
alter table Pizza.PizzaTopping
  add constraint FK_PizzaTopping_PizzaId foreign key (PizzaId) references Pizza.Pizza(PizzaId)
go
alter table Pizza.PizzaTopping
  add constraint FK_PizzaTopping_ToppingId foreign key (ToppingId) references Pizza.Topping(ToppingId)
go

-- User to Order: One to Many
/*alter table Client.PBOrder
  add UserId int not null
go*/
alter table Client.PBOrder
  add constraint FK_PBOrder_UserId foreign key (UserId) references Client.PBUser(UserId)
go

-- Store to Order: One to Many
/*alter table Client.PBOrder
  add StoreId int not null
go*/
alter table Client.PBOrder
  add constraint FK_PBOrder_StoreId foreign key (StoreId) references Client.PBStore(StoreId)
go

-- Order to Pizza: Many to Many
alter table Client.PizzaOrder
  add constraint FK_PizzaOrder_OrderId foreign key (OrderId) references Client.PBOrder(OrderId)
go
alter table Client.PizzaOrder
  add constraint FK_PizzaOrder_PizzaId foreign key (PizzaId) references Pizza.Pizza(PizzaId)
go

-- DROP
/*drop database PizzaBoxDb
go*/

-- TRUNCATE