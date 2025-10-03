FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . ./

RUN dotnet restore MSUsuarios/MSUsuarios.csproj
RUN dotnet publish MSUsuarios/MSUsuarios.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "MSUsuarios.dll"]
# This Dockerfile builds a Blazor WebAssembly application using .NET 8.0 SDK