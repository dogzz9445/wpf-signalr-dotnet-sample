```bash
cd src
dotnet new web -n SignalRServer
cd SignalRServer
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.OpenApi
cd ..
dotnet new wpf -n SignalRWpfClient
cd SignalRWpfClient
dotnet add package Microsoft.AspNetCore.SignalR.Client
```