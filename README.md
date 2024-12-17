# README - Coding Blog Application

## **Visão Geral**

A Coding Blog é uma aplicação full stack desenvolvida com **ASP.NET Core 8** para o back-end e **Angular 17** para o front-end. O objetivo é fornecer uma plataforma de blog onde usuários podem criar, listar e visualizar posts. A aplicação também conta com **SignalR** para comunicação em tempo real de atualizações de posts.

---

## **Tecnologias Utilizadas**

**Back-end:**

- **ASP.NET Core 8**: Para a construção da API RESTful.
- **Entity Framework Core**: Para manipulação e acesso ao banco de dados.
- **FluentValidation**: Para validação de entradas de dados.
- **SignalR**: Para comunicação em tempo real (notificações de novos posts).

**Front-end:**

- **Angular 17**: Para a interface do usuário.
- **Bootstrap**: Para o design responsivo.

**Banco de Dados:**

- **SQL Server**: Banco relacional para persistência dos dados.

---

## **Como Rodar a Aplicação**

### **1. Requisitos Prévios**

- .NET SDK 8 instalado.
- Node.js (para rodar a aplicação Angular).
- PostgresSQL (local ou remoto).

### **2. Configurações Iniciais**

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/coding-blog.git
   cd coding-blog
   ```

2. Configure a string de conexão no arquivo `appsettings.json` da aplicação back-end:

   ```json
     "Databases": {
       "Postgres": {
         "ConnectionString": "Server=localhost;Port=5432;Database=blog;User Id=postgres;Password=postgres",
         "ConnectionRetryCount": 30,
         "ConnectionRetryDelay": 30
       }
     },
   ```

3. Configure a chaves de JWT no `appsettings.json`:
   ```json
   "Jwt": {
     "SecretKey": "chave-secreta-com-256-bits",
     "Issuer": "https://localhost:5001",
     "Audience": "https://localhost:5001"
   }
   ```

---

## **Comandos Importantes**

### **Back-end**

**Criar Migração:**

```bash
cd CodingBlog.Presentation
 dotnet ef migrations add InitialMigration --project ../CodingBlog.Infrastructure/ --startup-project .
```

**Aplicar Migrações e Criar Banco:**

```bash
cd CodingBlog.Presentation
 dotnet ef database update --project ../CodingBlog.Infrastructure/ --startup-project .
```

**Rodar a API:**

```bash
cd CodingBlog.Presentation
 dotnet run
```

Acesse: [https://localhost:5001/swagger](https://localhost:5001/swagger)

---

### **Front-end**

**Instalar dependências:**

```bash
cd CodingBlog.Frontend
npm install
```

**Rodar o Front-end:**

```bash
ng serve
```

Acesse: [http://localhost:4200](http://localhost:4200)

---

## **Execução de Testes**

**Testes Unitários (Back-end):**

```bash
cd CodingBlog.Tests
 dotnet test --collect:"XPlat Code Coverage"
```

O coverage será gerado na pasta `TestResults`.

**Testes Unitários (Front-end):**

```bash
cd CodingBlog.Frontend
ng test
```

---

## **Estrutura do Código**

```
├── CodingBlog.Infrastructure  // Camada de Acesso a Dados (Repository, Migrations)
├── CodingBlog.Presentation    // Camada de Aplicação (Controllers, Middlewares)
├── CodingBlog.Domain          // Entidades e Regras de Negócio
├── CodingBlog.Tests           // Testes Unitários
├── CodingBlog.Frontend        // Aplicação Angular
```

---

## **Endpoints da API**

**Autenticação**

- **POST /auth/login** - Autentica o usuário e retorna o token JWT.

**Posts**

- **GET /posts** - Lista todos os posts.
- **GET /posts/{id}** - Buscar um post por ID.
- **POST /posts** - Cria um novo post.
- **PUT /posts/{id}** - Atualiza um post existente.
- **DELETE /posts/{id}** - Exclui um post existente.

---

## **Modelo C4**

**1. Contexto**

- Um administrador ou editor de conteúdo acessa o sistema (via web) para publicar posts.
- O sistema envia notificações de novos posts para os clientes que estão logados.

**2. Componentes**

- **Web (Angular 17)** - Interface de acesso dos usuários.
- **API (ASP.NET Core 8)** - Fornece os endpoints para CRUD de posts e autenticação JWT.
- **Banco de Dados (PostgresSQL)** - Armazena as informações de posts e usuários.
- **SignalR** - Canal de comunicação em tempo real para notificações de novos posts.


## **Melhores Práticas Utilizadas**

- **JWT** para autenticação e autorização.
- **SignalR** para notificações em tempo real.
- **CORS** configurado para acesso restrito por origem.
- **Clean Architecture** separando aplicação em camadas.
- **Validação** com FluentValidation para garantir a entrada de dados corretos.
- **Testes Unitários** para garantir a qualidade do software.

---
