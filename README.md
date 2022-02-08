# Pokedex

## Running the API

- Install the ASP.NET Core Runtime 3.1.22 Hosting Bundle from https://dotnet.microsoft.com/en-us/download/dotnet/3.1
- Clone the repository.
- Navigate to the /src/ folder and run the following commands.
```
dotnet restore
dotnet build
dotnet publish
cd Pokedex
dotnet run
```
- Make Http Requests to ```localhost:8080``` to your heart's content.
  
## Running the API via Docker

- Install Docker for Desktop, available from https://docs.docker.com/get-docker/
- From the root directory of this file run the following commands.
```
docker build -t pokedex .
docker run -d -p 8080:80 --name pokedex pokedex
```
- Make Http Requests to ```localhost:8080``` to your heart's content.

## Things I would do differently in Production

Throughout the project there are a number of comments that specify small changes I would do in a production setting.

In terms of larger picture design changes, I would do the following:
-  I would use some form of distributed cache (i.e. Redis) to cache the translations as there is a limit to the amount of requests that can be made with the licences available. This would mean that multiple instances of this API (load-balanced) would share the cache and would minimal duplicate requests using precious requests.
- Use NLog (or an alternative) to log messages to an external system (such as Splunk) or a database.
- Have a metrics utility for measuring latency of things such as request execution time, and interactions with external systems.
- Add more configuration options (such as being able to set the default description language, if the request headers doesn't specify a valid or supported language) - this could then be changed based on deployment location (i.e. Japanese in Japan, French in France etc)
- Add Polly (https://github.com/App-vNext/Polly) to the HttpClients to ensure resilience and fault handling in production (retries, circuit breaking etc)
- Not use the questionably supported, third party library for interacting with the PokeAPI. Though I did like its interface.
- I would have integration tests.