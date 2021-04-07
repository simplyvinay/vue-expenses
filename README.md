## Table Of Contents

- [Description](#Vue-Expenses)
  - [Demo](#demo)
- [Tech Stack](#Tech-Stack)
  - [Server Side](#Server-Side)
  - [Client Side](#Client-Side)
- [Screenshots](#Screenshots)
- [Local Building](#Local-Building)
- [Config](#Config)
- [Future Enhancements](#Future-Enhancements)
- [How to Contribute](#How-to-Contribute)
- [License](#License)

# Vue Expenses

A simple expenses tracking application built with VueJs and .NET

### Demo

[Vue Expenses](https://vue-expense.herokuapp.com)
- Username: `test@demo.com`
- Password: `test`

Please note that you can change the theme in the settings page and the data on the demo website will be reset at regular intervals

# Tech Stack

### Server Side

- [.NET 5.0](https://github.com/dotnet/core) for server side API
- [Fluent Validation](https://github.com/JeremySkinner/FluentValidation)
- CQRS
  - [MediatR](https://github.com/jbogard/MediatR)
  - [Entity Framework Core](https://github.com/aspnet/EntityFrameworkCore) on SQLite.
  - [Dapper](https://github.com/StackExchange/Dapper) for querying
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) for Swagger
- [ASP.NET Core JWT Bearer Authentication](https://github.com/aspnet/Security) for [JWT](https://jwt.io/) authentication with support for [refresh tokens](https://tools.ietf.org/html/rfc6749#section-1.5).
- [Serilog](https://github.com/serilog/serilog) for logging
- Reference architecture [ContosoUniversity](https://github.com/jbogard/ContosoUniversityDotNetCore)

### Client Side

- [VueJs](https://github.com/vuejs/vue) for client application
- [Vuex](https://github.com/vuejs/vuex) with [Vuex-persistedstate](https://github.com/robinvdvleuten/vuex-persistedstate) for state management
- [Vue-router](https://github.com/vuejs/vue-router) for client routing
- [Axios](https://github.com/axios/axios) for HTTP requests
- [Vuetify](https://github.com/vuetifyjs/vuetify) as component framework
- [Lodash](https://github.com/lodash/lodash) for client side utility
- [Vue-echarts](https://github.com/ecomfe/vue-echarts) ([Echarts](https://echarts.apache.org/en/index.html)) for charting

# Screenshots

### Dashboard

<img src="/assets/images/Dashboard.png" alt="Vue Expenses Dashnoard" width="100%" />

### Expense Listing

<img src="/assets/images/Expenses.png" alt="Vue Expenses Listing" width="100%" />

### Stats

<img src="/assets/images/Stats.png" alt="Vue Expenses Stats" width="100%" />

### Settings

<img src="/assets/images/Settings.png" alt="Vue Expenses Settings" width="100%" />

### Profile

<img src="/assets/images/Profile.png" alt="Vue Expenses Profile" width="100%" />

### Mobile View
- [Dashboard](/assets/images/mobile/dashboard.png)
- [Stats](/assets/images/mobile/Stats.png)

# Local Building

### Server

- Install [.NET Core SDK](https://dotnet.microsoft.com/download)
- Go to vue-expenses-api folder and run `dotnet restore` and `dotnet build`
- Run `dotnet run` to start the server at `http://localhost:5000/`
- You can view the API reference at `http://localhost:5000/swagger`

### Client

- Go to vue-expenses-client folder and run `npm install`
- Run `npm run serve` to start the client at `http://localhost:8080/`
- Included database is seeded with dummy data and you can use `email: test@demo.com` and `password: test` to login

# Config

### Server

#### ConnectionStrings

- `DefaultConnection`: `Data Source=App_Data/expenses.db`
  - Where the sqlite db file is located, this can be changed in `appsettings.json` file

#### JwtSettings

- `SecurityKey`: `A super secret long key to encrypt and decrypt the token`
- `Issuer`: `Issuer`
- `Audience`: `Audience`
  - The key, issuer and audience values to generate a jwt token, this can be changed in `appsettings.json` file

#### PasswordHasher

- `Key`: `Secret key to encrypt passwords`
  - The key to encrypt the passwords, this can be changed in `appsettings.json` file

### Client

- `VUE_APP_BASE_URL`: `http://localhost:5000/`
  - Base url to connect to the API, this can be changed in the `.env` file

- `productionSourceMap`: `false`
  - Generates source map file when building for production, this can be changed in `vue.config.js` file

- `outputDir`: commented out by default
  - Where the built files will be copied over, this can be changed in `vue.config.js` file

- `assetsDir`: commented out by default
  - Where the built minified css/js files will be copied over, this path is relative path from the `outputDir`, this can be changed in `vue.config.js` file

# Future Enhancements

- Check project [To do list](https://github.com/simplyvinay/vue-expenses/projects/1)

# How to Contribute

1. Clone repo `git clone https://github.com/simplyvinay/vue-expenses.git`
2. Create a new branch: `git checkout -b new_branch_name`
3. Make changes and test
4. Submit Pull Request with description of changes

## License

[MIT](https://github.com/simplyvinay/vue-expenses/blob/master/LICENSE)
