
- Before starting the project please run Redis-x64-3.0.504/redis-server.exe,
  The project will connect to the Redis with the default path: localhost:6379
- Redis Database store player name and account informations.
- If player name does not exist on db, create new entry automatically.
- You can send a request to API with the link and Json below

request url: http://localhost:5000/api/Game/Play

example request: 
{
    "PlayerName": "player1",
    "Points": 1000,
    "Number" : 3
} 
