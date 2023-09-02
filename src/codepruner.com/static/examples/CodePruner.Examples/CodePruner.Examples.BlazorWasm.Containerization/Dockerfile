FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY FC.Frontend.BlazorWebAssembly/FC.Frontend.BlazorWebAssembly.csproj FC.Frontend.BlazorWebAssembly/FC.Frontend.BlazorWebAssembly.csproj
COPY Frontend/FC.Frontend.Integrations/FC.Frontend.Integrations.csproj  Frontend/FC.Frontend.Integrations/FC.Frontend.Integrations.csproj
COPY DevTools/FC.DevTools.Extensions/FC.DevTools.Extensions.csproj DevTools/FC.DevTools.Extensions/FC.DevTools.Extensions.csproj
COPY Modules/Cards/FC.Cards.Dtos/FC.Cards.Dtos.csproj Modules/Cards/FC.Cards.Dtos/FC.Cards.Dtos.csproj
COPY Modules/Learning/FC.Learning.Dtos/FC.Learning.Dtos.csproj Modules/Learning/FC.Learning.Dtos/FC.Learning.Dtos.csproj
RUN dotnet restore ./FC.Frontend.BlazorWebAssembly/FC.Frontend.BlazorWebAssembly.csproj

COPY . .
WORKDIR "/src/FC.Frontend.BlazorWebAssembly"
RUN dotnet build "FC.Frontend.BlazorWebAssembly.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
WORKDIR "/src/FC.Frontend.BlazorWebAssembly"
RUN dotnet publish "FC.Frontend.BlazorWebAssembly.csproj" -c Release -o /app/publish --no-restore

FROM  nginx AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
#COPY --from=publish /app/publish/wwwroot/nginx.conf /etc/nginx/nginx.conf
COPY --from=publish /app/publish/wwwroot/nginx.conf.template /etc/nginx/nginx.conf.template 
CMD ["/bin/sh" , "-c" , "envsubst '${AI_CONNECTIONSTRING}' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf && exec nginx -g 'daemon off;'"]
