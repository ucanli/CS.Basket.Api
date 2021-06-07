#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["CS.Basket.Api/CS.Basket.Api.csproj", "CS.Basket.Api/"]
RUN dotnet restore "CS.Basket.Api/CS.Basket.Api.csproj"
COPY . .
WORKDIR "/src/CS.Basket.Api"
RUN dotnet build "CS.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CS.Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CS.Basket.Api.dll"]