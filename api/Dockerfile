# Использование базового образа .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копирование csproj и восстановление зависимостей
COPY *.csproj ./
RUN dotnet restore

# Копирование остального кода и сборка
COPY . ./
RUN dotnet publish -c Debug -o out

# Использование базового образа .NET Runtime для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Открытие порта
EXPOSE 5050 1433 8888

# Указание команды запуска
ENTRYPOINT ["dotnet", "api.dll"]