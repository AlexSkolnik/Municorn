FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["Municorn/Municorn.WebAPI/Municorn.WebAPI.csproj", "Municorn/Municorn.WebAPI/"]
RUN dotnet restore "Municorn/Municorn.WebAPI/Municorn.WebAPI.csproj"

COPY . .
WORKDIR "/src/Municorn/Municorn.WebAPI"
RUN dotnet build "Municorn.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Municorn.WebAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Municorn.WebAPI.dll"]