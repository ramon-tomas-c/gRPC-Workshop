FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DemoService/DemoService.csproj", "DemoService/"]
RUN dotnet restore "DemoService/DemoService.csproj"
COPY . .
WORKDIR "/src/DemoService"
RUN dotnet build "DemoService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DemoService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DemoService.dll"]