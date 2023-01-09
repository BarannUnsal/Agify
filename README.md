<p align=center>Agify API</p>


## Dependencies
- mssql/server/2022
- redis

<p>For use</p>
<p>Follow this steps</p>
- Create redis container
1. docker network create redis-network
2. docker run - --name redis --network redis-network redis

- Create mssql container
1. docker run -e “ACCEPT_EULA=Y” -e “SA_PASSWORD=A!STR0NGPA55W0rd!” -p 1433:1433 -d mssql/server:2022-latest

When you complete this steps just follow this command:
- docker run -d --name redis --network redis-network redis

<a href="https://hub.docker.com/r/barannunsal/agifyio">Docker hub</a>
