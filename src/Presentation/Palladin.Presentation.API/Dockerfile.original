FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Presentation/Palladin.Presentation.API/Palladin.Presentation.API.csproj", "src/Presentation/Palladin.Presentation.API/"]
RUN dotnet restore "src/Presentation/Palladin.Presentation.API/Palladin.Presentation.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Palladin.Presentation.API"
RUN dotnet build "Palladin.Presentation.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Palladin.Presentation.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Palladin.Presentation.API.dll"]