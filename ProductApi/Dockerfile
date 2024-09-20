
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app


COPY ProductApi/*.csproj ProductApi/
COPY ProductApi.Business/*.csproj ProductApi.Business/
COPY ProductApi.DataAccess/*.csproj ProductApi.DataAccess/
COPY ProductApi.Dto/*.csproj ProductApi.Dto/
COPY ProductApi.Entity/*.csproj ProductApi.Entity/
RUN dotnet restore ProductApi/ProductApi.csproj


COPY . .
RUN dotnet publish ProductApi/ProductApi.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ProductApi.dll"]
