FROM nginx AS base
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

FROM  base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY  CodePruner.Examples.BlazorWasm.Containerization/nginx.conf /etc/nginx/nginx.conf
