# Use a imagem base do SDK do .NET para construir a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copie o arquivo .csproj e restaure as depend�ncias
COPY *.sln .
COPY Fiap_Hackathon/*.csproj ./Fiap_Hackathon/
RUN dotnet restore

# Copie o restante dos arquivos e construa a aplica��o
COPY . .
RUN dotnet publish -c Release -o out

# Use a imagem base do runtime do .NET para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Defina a porta que a aplica��o ir� escutar
EXPOSE 80

# Comando para rodar a aplica��o
ENTRYPOINT ["dotnet", "Fiap_Hackathon.dll"]