FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . "/src/GUI/"
RUN dotnet restore "/src/GUI/GUI.csproj"
COPY . .
WORKDIR "/src/GUI"
RUN dotnet build "GUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GUI.dll"]
