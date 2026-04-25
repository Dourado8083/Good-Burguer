FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["GoodBurger.Web/GoodBurger.Web.csproj", "GoodBurger.Web/"]
COPY ["GoodBurger.Shared/GoodBurger.Shared.csproj", "GoodBurger.Shared/"]
RUN dotnet restore "GoodBurger.Web/GoodBurger.Web.csproj"

COPY . .
WORKDIR "/src/GoodBurger.Web"
RUN dotnet publish "GoodBurger.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY GoodBurger.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80