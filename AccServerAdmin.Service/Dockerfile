FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["AccServerAdmin.Service/AccServerAdmin.Service.csproj", "AccServerAdmin.Service/"]
RUN dotnet restore "AccServerAdmin.Service/AccServerAdmin.Service.csproj"
COPY . .
WORKDIR "/src/AccServerAdmin.Service"
RUN dotnet build "AccServerAdmin.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccServerAdmin.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccServerAdmin.Service.dll"]