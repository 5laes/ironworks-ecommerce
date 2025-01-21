
# Ecommerce Clone

This is a project for me to sharpen my angular skills and learn about implementing payment with cards, user authentication, caching and more.

I wish I could publish this app to some publisher service but I am no economical situation where I can spend the tiniest amount of money on something like that right now. If you want to try this app you could clone this repository and build and run it your self. (You will not see any pictures since they are hosted on my API key for Cloudinary)

## How to Build and Run
Tools you need installed
- .NET 9
- Angular 18
- Redis

I would recommend cloning the ``` 4613ef307147961c1e120c82c6556115fa58e840 ``` commit since that is the last commit before switching to PostgreSQL from Sqlite.

in one terminal cd in to the API folder and run 
- ``` dotnet restore ```
- ``` dotnet run ```

in another terminal cd in to the client folder and run (both terminals needs to run at the same time)
- ``` npm install ```
- ``` ng serve ```

in a third terminal (does not need to be in a specific folder) run
- ``` redis-server ```

you should now be able to test this application by connecting to https://localhost:4200

## Features

- Accepts payments with Stripe
- Authentication needed to access checkout page
- Caches data to reduce database calls
- Users Baskets are saved in a Redis database for up to 7 days
- Users can search and filter product for their needs
- Pagination to only get a certain amount of products at a time
- Users data is secured using .NET Identity
- API Corrects manipulated data from the client
- Users can view order history and order status
- and more
## Tech Stack

**Client:** Angular, BootstrapCSS, Stripe

**Server:** ASP.NET, C#, Sqlite, Entity Framework, Redis

