# ProductStore - проект на микросервисной архитектуре с клиентом на Blazor

## Схема взаимодействия:
![alt text](https://github.com/sanokkk/productstore/blob/master/scheme%20(1).png)

## Сервисы:
1. Identity - сервис для регистрации, авторизации. Авторизация предполагает получение jwt-токена. Также в механизме авторизации используются refresh-токены. БД: Postgres.
2. Shops - сервис для управления, использования интернет-магазина. Можно с помощью UI добавить товары в корзину, купить их. Для выбора есть разные магазины. Также есть возможность просмотра истории покупок. При помощи взаимодействия непосредственно с API администратор может добавлять магазины, товары в них. БД: MSSQL
3. Factory - сервис, который через определенные промежутки времени по брокеру RabbitMQ отправляет команды на пополнение количества товаров в магазины (сервис Shops), а также зарплату покупателям (увеличение баланса в размере n% от зарплаты в сервисе Identity).

## Технологии:
1. В процессе разработки использоватлся .Net 7, UI написан с помощью технологии Blazor.
2. В качестве СУБД использованы MSSQL и PostgreSQL.
3. ORM - EFCore.
4. В организации шины данных использовалась библиотека MassTransit, брокер сообщений - RabbitMQ (разворачивался локально в докере).
5. Авторизация на базе JWT, в проекте Identity использовалась библиотека Identity для управления информацией о пользователях и хеширования данных.
   
