FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
RUN mkdir -p /app
WORKDIR /app
COPY Backend/BankApp.API/*.csproj .
RUN dotnet restore
COPY Backend/BankApp.API/ .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "BankApp.API.dll"]