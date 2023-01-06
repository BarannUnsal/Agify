FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "Agify.API/Agify.API.csproj" --disable-parallel
RUN dotnet publish "Agify.API/Agify.API.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /src
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Agify.API.dll"]