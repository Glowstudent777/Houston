FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY Houston.Bot/Houston.Bot.csproj Houston.Bot/
COPY Houston.Database/Houston.Database.csproj Houston.Database/
# COPY NuGet.config Houston.Bot/
RUN dotnet restore Houston.Bot/Houston.Bot.csproj
COPY . .
WORKDIR /src/Houston.Bot
RUN dotnet build Houston.Bot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Houston.Bot.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Houston.Bot.dll"]