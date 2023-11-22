# Poineers-App

The console applicaion reads csv file with a specific format (you can change the path of the file from the appsettings) , parses the data and generates csv files for each product containing its transactions.

The web api using clean architecture.
- AutoMapper
- MediatR and CQRS architecture.
- Entity framework core.

TODOS
- Console app exception handling + vaidation fixes.
- WebAPI upload file and save data to db.
- Angular client to consume web api with two views (1- search view (end point implemented) 2- import view).

API Architecture
1- Domain Layer
2- Application Layer
3- Infrastructure Layer (Persistance and Shared)
4- The web api layer 
