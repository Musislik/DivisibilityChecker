FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV DCID=0
ENV IP=0.0.0.0

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DivisibilityChecker.csproj", "."]
RUN dotnet restore "./DivisibilityChecker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DivisibilityChecker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DivisibilityChecker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DivisibilityChecker.dll"]