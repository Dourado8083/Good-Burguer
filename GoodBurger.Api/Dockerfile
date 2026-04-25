FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["GoodBurger.Api/GoodBurger.Api.csproj", "GoodBurger.Api/"]
COPY ["GoodBurger.Shared/GoodBurger.Shared.csproj", "GoodBurger.Shared/"]
RUN dotnet restore "GoodBurger.Api/GoodBurger.Api.csproj"


COPY . .
WORKDIR "/src/GoodBurger.Api"
RUN dotnet publish "GoodBurger.Api.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GoodBurger.Api.dll"]