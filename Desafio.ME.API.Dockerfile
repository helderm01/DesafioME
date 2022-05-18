FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Desafio.ME.API/Desafio.ME.API.csproj", "Desafio.ME.API/"]
COPY ["Desafio.ME.Database/Desafio.ME.Database.csproj", "Desafio.ME.Database/"]
COPY ["Desafio.ME.Dominio/Desafio.ME.Dominio.csproj", "Desafio.ME.Dominio/"]
COPY ["Desafio.ME.DTOs/Desafio.ME.DTOs.csproj", "Desafio.ME.DTOs/"]
COPY ["Desafio.ME.Handlers/Desafio.ME.Handlers.csproj", "Desafio.ME.Handlers/"]
RUN dotnet restore "Desafio.ME.API/Desafio.ME.API.csproj"
COPY . .
WORKDIR "/src/Desafio.ME.API"
RUN dotnet build "Desafio.ME.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio.ME.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.ME.API.dll"]