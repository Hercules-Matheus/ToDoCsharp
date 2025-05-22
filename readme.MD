# TaskFlow

Aplicação ASP.NET Core MVC para gerenciamento de tarefas (To-Do List).

## Funcionalidades

- Cadastro, edição e exclusão de tarefas
- Marcar tarefas como concluídas
- Validação de campos (título, prazo)
- Interface responsiva com Bootstrap 5
- Banco de dados SQLite (criado automaticamente)
- Visual moderno com customização de cores e fontes

## Como rodar o projeto

1. Clone o repositório:
   git clone https://github.com/Hercules-Matheus/ToDoCsharp.git\\
   e mude para a pasta do projeto:\\
   `cd ToDoCsharp`

2. Restaure os pacotes:\\
   `dotnet restore`

3. Aplique as migrations e crie o banco de dados:\\
   `dotnet ef database update`

4. Rode a aplicação:\\`dotnet run`\\Para desenvolvimento com hot reload:\\`dotnet watch run`

5. Acesse no navegador:\\
   http://localhost:5218
   (ou a porta exibida no terminal)

## Estrutura do Projeto

- Controllers/ — Lógica das rotas e ações
- Models/ — Modelos de dados (entidades)
- Views/ — Views Razor (HTML + C#)
- wwwroot/ — Arquivos estáticos (CSS, JS, imagens)
- Migrations/ — Migrations do Entity Framework

## Pré-requisitos

- .NET 7.0 SDK ou superior (https://dotnet.microsoft.com/download)
- SQLite (https://www.sqlite.org/index.html) (opcional, pois o EF Core cria o arquivo automaticamente)

## Observações

- O banco de dados é local e será criado automaticamente.
- As migrations estão versionadas para facilitar o deploy.
- O projeto utiliza Bootstrap, Bootstrap Icons e Google Fonts.

---

Feito por [Hercules-Matheus](https://github.com/Hercules-Matheus)
