FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CodePruner.Examples.BlazorWasm.Containerization/CodePruner.Examples.BlazorWasm.Containerization.csproj", "CodePruner.Examples.BlazorWasm.Containerization/"]
RUN dotnet restore "CodePruner.Examples.BlazorWasm.Containerization/CodePruner.Examples.BlazorWasm.Containerization.csproj"
COPY . .
WORKDIR "/src/CodePruner.Examples.BlazorWasm.Containerization"
RUN dotnet build "CodePruner.Examples.BlazorWasm.Containerization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodePruner.Examples.BlazorWasm.Containerization.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodePruner.Examples.BlazorWasm.Containerization.dll"]