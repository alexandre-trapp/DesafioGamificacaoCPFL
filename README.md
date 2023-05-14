# CPFL Gamification Challenge

# REST API for consumption by the CPFL challenge frontend:

Manage customer data, partial invoice payments and calculation of scores, XPs and bonuses
for customers to exchange for discounts or coupons in stores.

To access the APIs, you can access the external link shown above, or download and run the project locally:

## Download the project

- In your git bash, powershell, vscode or VisualStudio, you can clone the repository and open the project:
  
  **git clone** https://github.com/alexandre-trapp/DesafioGamificacaoCPFL.git

- The mongodb database is already configured in the atlas for use, but I will deactivate the cluster after
  the challenge, if you download it and want to run it, you can create a free account at https://www.mongodb.com/cloud/atlas, create the cluster, user and password
  and generate a connection string, informing it in the appsettings.json file at the root of the project (DatabaseSettings > ConnectionString), here is documentation on how
  do this: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-5.0&tabs=visual-studio
  
   
## Build / Run
- To run the application, it can be through VisualStudio if you are already familiar with it, building the solution or project (Ctrl + B),
or in vscode, bash, powrshell (with .net core 3.1 sdk installed - https://dotnet.microsoft.com/download/dotnet-core/3.1) run the command **dotnet build** and then ** dotnet run**, na
project root.

# REST API

- To access the url with the documentation of the APIs locally, after running the application, access it in the browser
https://localhost:5002/swagger/index.html (or http://localhost:5003/swagger/index.html but will redirect to https)

## Documentation
All API documentation, method names, parameters, request and response bodies will be in the swagger.
