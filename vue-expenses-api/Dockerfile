FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env
WORKDIR /app

ENV PATH="$PATH:$HOME/.dotnet/tools/"

# Copy csproj 
COPY *.csproj ./
RUN dotnet restore

# Copy everything build
COPY . ./
RUN dotnet publish -c Release -o .

# Heroku doesnt like ENTRYPOINT
# ENTRYPOINT ["dotnet", "vue-expenses-api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet vue-expenses-api.dll