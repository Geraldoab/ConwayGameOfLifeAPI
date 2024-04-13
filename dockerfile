FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY ["/Game.API/Game.API.csproj", "app/Game.API/"]
COPY ["/Game.Application/Game.Application.csproj", "app/Game.Application/"]
COPY ["/Game.Domain/Game.Domain.csproj", "app/Game.Domain/"]
COPY ["/Game.Infra.Data/Game.Infra.Data.csproj", "app/Game.Infra.Data/"]

RUN dotnet restore "app/Game.API/Game.API.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Game.API.dll" ]
