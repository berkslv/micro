FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Basket/Micro.Basket/Micro.Basket.csproj", "Services/Basket/Micro.Basket/"]
RUN dotnet restore "Services/Basket/Micro.Basket/Micro.Basket.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Micro.Basket"
RUN dotnet build "Micro.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Micro.Basket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Micro.Basket.dll"]
