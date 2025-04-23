# API de Autenticação de Usuários

Esta é uma API de autenticação de usuários construída com .NET, utilizando JWT para gerar tokens e SQLite como banco de dados.

## Estrutura do Projeto

O projeto possui os seguintes componentes principais:

- **Controllers**: Controladores para manipulação de rotas.
- **Services**: Serviços responsáveis por funcionalidades específicas, como geração de JWT e manipulação de senhas.
- **Models**: Modelos de dados usados na aplicação.
- **Dtos**: Objetos de Transferência de Dados utilizados para comunicação entre o frontend e o backend.

## Tecnologias Utilizadas

- **.NET 9**
- **SQLite** como banco de dados.
- **JWT** para autenticação.
- **Entity Framework Core** para manipulação do banco de dados.
- **Scalar** para documentação da API.

## Configuração Inicial

1. **Instalar Dependências**:

    Certifique-se de que todas as dependências do projeto estão instaladas:

    ```bash
    dotnet restore
    ```

2. **Configuração do banco de dados**:

    O projeto utiliza SQLite, e a string de conexão está configurada no arquivo `appsettings.json`:

    ```bash
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=app.db"
    }
    ```

3. **Configuração do JWT**:

    A chave secreta, o emissor (Issuer), o público (Audience) e o tempo de expiração do token são configurados no `appsettings.json`:

    ```bash
    "Jwt": {
        "SecretKey": "SecretKey",
        "Issuer": "Issuer",
        "Audience": "Audience",
        "ExpiresInMinutes": 1440
    }
    ```

## Endpoints

1. **GET /api/auth** - Listar todos os usuários

    Retorna todos os usuários cadastardos na base de dados.

    **Reposta**:
    ```json
    [
        {
            "id": 1,
            "name": "John Doe",
            "email": "john.doe@example.com",
            "phone": "1234567890"
        }
    ]
    ```

2. **GET /api/auth/{id}** - Buscar usuário por ID

    Busca um usuário pelo seu ID.

    **Reposta**:
    ```json
    {
        "id": 1,
        "name": "John Doe",
        "email": "john.doe@example.com",
        "phone": "1234567890"
    }
    ```

3. **POST /api/auth** - Criar novo usuário

    Cria um novo usuário. A senha é recebida em texto plano e será criptografada.

    **Corpo da requisição**:
    ```json
    {
        "name": "John Doe",
        "email": "john.doe@example.com",
        "phone": "1234567890",
        "password": "password123"
    }
    ```

    **Reposta**:
    ```json
    {
        "id": 1,
        "name": "John Doe",
        "email": "john.doe@example.com",
        "phone": "1234567890"
    }
    ```

4. **PUT /api/auth** - Atualizar dados do usuário

    Atualiza as informações de um usuário. Requer a senha atual para validação.

    **Corpo da requisição**:
    ```json
    {
        "id": 1,
        "name": "John Doe",
        "email": "john.doe@example.com",
        "phone": "1234567890",
        "password": "password123",
        "newPassword": "newPassword123"
    }
    ```

    **Reposta**:
    ```json
    {
        "id": 1,
        "name": "John Doe",
        "email": "john.doe@example.com",
        "phone": "1234567890"
    }
    ```

5. **DELETE /api/auth/{id}** Deletar usuário

    Deleta um usuário da base de dados pelo seu ID.

    **Reposta**:
    Status HTTP 204 (No Content).

6. **POST /api/auth/login** - login

    Faz o login do usuário e retorna um token JWT. O token é salvo em um cookie seguro (`jwt`).

    **Corpo da requisição**:
    ```json
    {
        "email": "john.doe@example.com",
        "password": "password123"
    }
    ```

    **Reposta**:
    ```json
    {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.4IGmAplfZymfm3aLK9k3d9bcGbgHvw45aRerqTtTuyY"
    }
    ```

7. **POST /api/auth/logout** - Logout

    Remove o token JWT do cookie e encerra a sessão do usuário.

    **Reposta**:
    Status HTTP 200 (OK).

## Execução

**Para rodar a aplicação, basta executar o comando**:
```bash
dotnet run
```

A API estará disponível em `https://localhost:7221`, e `http://localhost:5059`.

Para mais detalhes, acesse a documentação no **Scalar** da aplicação: `https://localhost:7221/scalar/v1`
