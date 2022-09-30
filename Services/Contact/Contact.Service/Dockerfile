#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Contact/Contact.Service/Contact.Service.csproj", "Services/Contact/Contact.Service/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Services/Contact/Contact.Service/Contact.Service.csproj"
COPY . .
WORKDIR "/src/Services/Contact/Contact.Service"
RUN dotnet build "Contact.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.Service.dll"]