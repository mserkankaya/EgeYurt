
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app


COPY Consume/*.csproj Consume/
RUN dotnet restore Consume/Consume.csproj


COPY . .
RUN dotnet publish Consume/Consume.csproj -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Consume.dll"]
