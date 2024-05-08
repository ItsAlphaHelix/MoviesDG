#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieDG.Web/MovieDG.Web.csproj", "MovieDG.Web/"]
COPY ["MovieDG.Common/MovieDG.Common.csproj", "MovieDG.Common/"]
COPY ["MovieDG.Services/MovieDG.Core.csproj", "MovieDG.Services/"]
COPY ["MovieDG.Data/MovieDG.Data.csproj", "MovieDG.Data/"]
RUN dotnet restore "MovieDG.Web/MovieDG.Web.csproj"
COPY . .
WORKDIR "/src/MovieDG.Web"
RUN dotnet build "MovieDG.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieDG.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieDG.Web.dll"]