# Desafio Gamificacao CPFL

# REST API para consumo pelo frontend do desafio CPFL:

- a documentação das APIs via Swagger pode ser acessada externamente em:
  - https://desafiogamificacaocpfl20201114183303.azurewebsites.net/swagger/index.html
  
gerenciar os dados dos cliente, pagamentos parciais das faturas e cálculo de pontuações, XPs e bônus 
para os clientes trocarem por descontos ou cupons em lojas.

Para acessar as APIs, pode acessar o link externo exibido acima, ou baixar e executar o projeto localmente:

## Download do projeto

- No seu git bash, powershell, vscode ou VisualStudio, poderá fazer o clone do repositório e abrir o projeto:
  
  **git clone** https://github.com/alexandre-trapp/DesafioGamificacaoCPFL.git

- O banco de dados mongodb já está configurado no atlas para uso, porém irei desativar o cluster após 
  o desafio, se baixar e quiser rodar, poderá criar uma conta free em https://www.mongodb.com/cloud/atlas, criar o cluster, usuário e senha
  e gerar uma string de conexão, informando-a no arquivo appsettings.json na raiz do projeto (DatabaseSettings > ConnectionString), aqui tem uma documentação como 
  fazer isso: https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-5.0&tabs=visual-studio
  
   
## Build / Run 
- Pra rodar a aplicação, pode ser pelo VisualStudio se já tiver familiaridade, dando o build na solution ou projeto (Ctrl + B),
ou no vscode, bash, powrshell (com o sdk do .net core 3.1 instalado - https://dotnet.microsoft.com/download/dotnet-core/3.1) executar o comando **dotnet build** e em seguida **dotnet run**, na
raiz do projeto.

# REST API

- Para acessar a url com documentação das APIs localmente, após executar a aplicação, acessar no navegador
https://localhost:5002/swagger/index.html (ou http://localhost:5003/swagger/index.html, mas irá redirecionar para https)

## Documentação
Toda a documentação da API, nomes dos métodos, parâmetros, corpo de requests e respostas estará no swagger.
