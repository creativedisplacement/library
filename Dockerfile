FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Library.WebApi/Library.WebApi.csproj", "Library.WebApi/"]
COPY ["Library.Application/Library.Application.csproj", "Library.Application/"]
COPY ["Library.Domain/Library.Domain.csproj", "Library.Domain/"]
COPY ["Library.Persistence/Library.Persistence.csproj", "Library.Persistence/"]
COPY ["Library.Infrastructure/Library.Infrastructure.csproj", "Library.Infrastructure/"]
RUN dotnet restore "Library.WebApi/Library.WebApi.csproj"
COPY . .
WORKDIR "/src/Library.WebApi"
RUN dotnet build "Library.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Library.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Library.WebApi.dll"]