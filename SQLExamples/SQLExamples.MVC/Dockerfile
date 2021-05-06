#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SQLExamples.MVC/SQLExamples.MVC.csproj", "SQLExamples.MVC/"]
RUN dotnet restore "SQLExamples.MVC/SQLExamples.MVC.csproj"
COPY . .
WORKDIR "/src/SQLExamples.MVC"
RUN dotnet build "SQLExamples.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SQLExamples.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SQLExamples.MVC.dll"]