# Pokedex

## Running the API

- Install the ASP.NET Core Runtime 3.1.22 Hosting Bundle from from https://dotnet.microsoft.com/en-us/download/dotnet/3.1
- Clone the repository.
- Navigate to the /src/ folder and run the following commands.
```
dotnet restore
dotnet build
dotnet publish
cd Pokedex
dotnet run
```

## Running the API via Docker

From the root directory of this file run the following commands.
```
docker build -t pokedex .
docker run -d -p 8080:80 --name pokedex pokedex
```
## Things I would do differently in Production

Throughout the project there are a number of comments that specify small changes I would do in a production setting.
