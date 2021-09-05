
# 288 Tech Test

For this project I have created a dotnet 5 c# backend api and a react frontend project to consume the api

 ## Requirements

```

As a User

I Want to be able to add a single product to a basket

```

Done through the update basket item method in the api and demo'd in the frontend project.

  

```

As a User

I want to be able to add multiple products to a basket

```

I have a method created an update multiple basket items endpoint where you can specify the products you want to update. In the frontend I use the method to manipulate the quantity which when set to 0 it will be soft delete. However this can also be used to add items to baskets. This is demo'd in the frontend project.

  

```

As a User

I want to be able to see the total cost of my basket

```

Returned as part of the get basket request. Demo'd in the frontend project.

  

```

As a User

I want to be able apply a discount code and see the price before and after the discount.

```

When using get basket you can also provide a discount code. Demo'd in the frontend project.

  

# Backend - Projects

I split the backend api solution into 5 separate projects. For functionality and testing

  

## 288.TechTest.Api

This is where the controller logic lies. In the basket controller I have used a factory pattern to handle discount types.

  

If I had more time I would have created the other controller endpoints for the tables I created. Currently I am just interacting with the basket table in the database. If I had more time I would have added in authentication as well. Currently I am just providing identifying data as part of the request.

  

## 288.TechTest.Api.Tests

In this project I have created a few tests to make sure the correct objects types are returned with the correct status types. I am using `Moq` for my unit test mocking.

  

## 288.TechTest.Data

In the data project I have used entity framework to build the database. I have used a generic base entity which I have used to create a generic repository pattern I did not end up using this though but left it in just to show you.

  

Another thing I have done is overrode the `modelBuilder` in the `DbContext` to allow for soft deletion. The reason I did this was because I was thinking of how a business would interact with this data on a long term basis. With this base entity you could create a `hangfire` task to send marketing emails off the back of this data. For example sending a email to a user who has added items to their basket but have not gone through with their purchase.

  

Another feature of this project is that I tried to keep the tables as generic as possible. To do this I have allowed identifying data such as userId and companyId to be stored as strings as. This means if the company using the system had multiple websites where one would store an id as a guid and another would store an id as an integer it would not matter as it would accommodate both.

  

If I had more time I would probably not use sql for data storage I would opt for something like mongo db instead as their is more flexibility on how I can store data and its also quicker. Also I would look to use an in memory database to write some tests around my soft deletion logic.

  
  

## 288.TechTest.Domain

  

This contains the services which the api project uses to interact with the database. Also includes a custom validation attribute and the factory classes. I also use automapper for mapping my DTO's. I have created extensions method that can be used to register the services in this projects.

  

## 288.TechTest.Domain.Tests

  

This project has a wide variety of tests where I mock the database repo services. I have also written unit tests to test factory logic and the custom attribute.

  

# Frontend

  

This is a simple react project that interacts with the api I have created. Very simple not as clean as I would like it to be. But good for demo purposes.

  

If I had more time I would look in to writing some end to end tests using something like cypress. Also breaking the logic into more components to increase readability. Also I would have neatened up some of the functionality for example having the option to hide the basket.

  
  

# Running locally

Below is a guide on how to run the examples on your local machine.

  

## Backend

Ensure you have ran the `update database` command in the package manager console. This will create a database in your localdb instance of sql which. When viewing the data in sql server management studio I use `(localdb)\MSSQLLocalDB`as my server name and use windows authentication to connect. This will create 4 tables. The discount data has seed data which contains discount codes as I have not added the endpoints for these.

  

Open project in visual studio and run through IIS express. This should open with the url on `https://localhost:44365/`

  

## Frontend

Built using node version `14.15.1.` To run the project open it in `cmd`, `powershell` or `vscode` then type `cd frontend` and then use either `npm install`or `yarn install` to ensure the node dependencies have been installed. Once installed use either `npm run start` or `yarn start` to start the project this will open the project on `http://localhost:3000`

# Test Vouchers

- 5OFF - Take 5 off total over 10
- 10OFF - Take 10 off total over 10
- 5PERCENTOFF - Take 5% off total over 10
- 10PERCENTOFF - Take 10% off total over 20