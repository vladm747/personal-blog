FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PersonalBlog.PL/PersonalBlog.PL.csproj", "PersonalBlog.PL/"]
RUN dotnet restore "PersonalBlog.PL/PersonalBlog.PL.csproj"
COPY . .
WORKDIR "/src/PersonalBlog.PL"
RUN dotnet build "PersonalBlog.PL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalBlog.PL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalBlog.PL.dll"]
