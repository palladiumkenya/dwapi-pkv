FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 6767
EXPOSE 6763

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY DwapiCentral/DwapiCentral.csproj DwapiCentral/
RUN dotnet restore DwapiCentral/DwapiCentral.csproj
COPY . .
WORKDIR /src/DwapiCentral
RUN dotnet build DwapiCentral.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DwapiCentral.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DwapiCentral.dll"]
