# Game Store Api

## Start SQL server
```bash
sa_password = "[SA PASSWORD HERE]"

Win
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -p 1436:1436 -v sqlvolume:/var/opt/mssql  --name sqlpreview --hostname sqlpreview -d --rm  mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

MacOs
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -p 1436:1433 -v sqlvolume:/usr/local/opt/mssql  --name game-store --platform=linux/amd64 --hostname mssql -d --rm  mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
916af5c84187bf1983418347798364bc3f80eddd30d067d8c48da7dd7e1fd44b

```

## Setting the connection string to secret manager

```bash
dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Server=localhost; Database=GameStore; User Id=sa; Password=$sa_password;TrustServerCertificate=True"
```