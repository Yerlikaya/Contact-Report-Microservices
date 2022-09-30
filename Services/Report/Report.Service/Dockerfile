#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Report/Report.Service/Report.Service.csproj", "Services/Report/Report.Service/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Services/Report/Report.Service/Report.Service.csproj"
COPY . .
WORKDIR "/src/Services/Report/Report.Service"
RUN dotnet build "Report.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.Service.dll"]