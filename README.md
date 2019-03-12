# GraphQL-Core

This demo is based on https://github.com/mmacneil/ASPNetCoreGraphQL with an accompanying blog on https://fullstackmark.com/post/17/building-a-graphql-api-with-aspnet-core-2-and-entity-framework-core

### GraphQL API

- .NET Core 2.1
- SQL Server 2017 (may support lower versions, this codebase was created against 2017 and Azure SQL)
- Hangfire
- Azure SignalR
- NLog
- React
- Apollo

Still in development

For the Database, create two databases: `NHLStats` and `HangfireGQL`

Create a `appsettings.Development.json` for both GraphQL.API and GraphQL.Console applications and add the following:


```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER;Initial Catalog=NHLStats;Persist Security Info=False;User ID=USERID;Password=PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "HangfireConnection": "Server=SERVER;Initial Catalog=HangfireGQL;Persist Security Info=False;User ID=USERID;Password=PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "Azure": {
    "SignalR": {
        "ConnectionString": "Endpoint=AZURE-SIGNALR-ENDPOINT;"
    }
  },  
```


Install React components and restore .NET core project
```
yarn --cwd "services/GraphQL.Api/clientApp" install
dotnet restore
```

To launch, run `dotnet run --project services/GraphQL.Api` or `dotnet watch --project services/GraphQL.Api run`

This will start both server and react application(via webpack) using Spa Services. React application was created with CRA.

**Hangfire Dashboard:** `http://localhost:50000/hangfire/`

**GraphQL Playground:** `http://localhost:50000/ui/playground/`


GraphQL Schema found under `services/GraphQL.Api/schema/schema.graphql`

To generate schema file:
```
npm install -g graphql-cli
cd services/GraphQL.Api/schema
graphql get-schema
```


#### Mutation example

##### Create

```graphql
mutation ($player: PlayerInput!, $skaterStats: [SkaterStatisticInput]) {
    createPlayer(player: $player, skaterStats: $skaterStats) {
        id name skaterSeasonStats {
          id
        }
    }
}
```

vars

```json
{
    "player": {
        "name": "Jaromir Jagr",
        "birthPlace": "Kladno, Czech Republic",
        "height": "6'03",
        "weightLbs": 230,
        "birthDate": "1972-02-15"
    },
    "skaterStats": [
            {
            "seasonId": 17,
            "leagueId": 1,
            "teamId": 5,
            "gamesPlayed": 82,
            "goals": 24,
            "assists": 43,
            "points": 67,
            "plusMinus": 16

    }]
        
}

```

##### Delete

```graphql
mutation ($playerId: Int!) {
    deletePlayer(playerId: $playerId) {
        id, statusType
    }
}

```
vars

```json
{
    "playerId": 5   
}
```


GraphQL.ConsoleApp and GraphQL.Api


### Hangfire Console
- .NET Core 2.1
- Hangfire
- Azure SignalR
- NLog

Still in development


Create a `appsettings.Development.json` for both GraphQL.API and GraphQL.Console applications and add the following:


```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER;Initial Catalog=NHLStats;Persist Security Info=False;User ID=USERID;Password=PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "HangfireConnection": "Server=SERVER;Initial Catalog=HangfireGQL;Persist Security Info=False;User ID=USERID;Password=PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
    "Azure": {
    "SignalR": {
      "ConnectionString": "Endpoint=AZURE-REDIS-ENDPOINT;"
    }
  },


```


```
dotnet run --project services/GraphQL.ConsoleApp
```


