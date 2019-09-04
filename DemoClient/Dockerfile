FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DemoClient/DemoClient.csproj", "DemoClient/"]
RUN dotnet restore "DemoClient/DemoClient.csproj"
COPY . .
WORKDIR "/src/DemoClient"
RUN dotnet build "DemoClient.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DemoClient.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DemoClient.dll"]