# Controle de Gastos Residenciais

Sistema para gerenciamento de gastos de uma residência, permitindo o cadastro de pessoas, o registro de suas transações financeiras (receitas e despesas) e a consulta de totais consolidados.

## Tecnologias utilizadas

| Camada | Tecnologia |
|---|---|
| Back-end | .NET 10 / ASP.NET Core Web API |
| Persistência | Entity Framework Core + SQLite |
| Front-end | React + TypeScript (Vite) |

## Estrutura do projeto

```
GastosResidenciais/
├── GastosResidenciais.Api/     → Back-end (.NET)
└── gastos-frontend/            → Front-end (React + TypeScript)
```

### Organização do back-end

O back-end é dividido em camadas, cada uma com uma responsabilidade única:

```
GastosResidenciais.Api/
├── Models/          → Entidades do domínio (Pessoa, Transacao)
├── DTOs/            → Formato de dados de entrada e saída da API
├── Data/            → Configuração do Entity Framework Core
├── Repositories/     → Acesso direto ao banco de dados
├── Services/         → Regras de negócio da aplicação
├── Controllers/       → Endpoints HTTP da API
├── Middlewares/       → Tratamento centralizado de erros
└── Program.cs         → Configuração e inicialização da aplicação
```

Cada camada só se comunica com a camada imediatamente abaixo dela (Controller → Service → Repository → banco de dados), o que mantém o código organizado e mais fácil de manter.

### Organização do front-end

```
gastos-frontend/src/
├── types/          → Definições de tipos (espelham os DTOs do back-end)
├── services/         → Funções responsáveis por chamar a API
├── components/       → Telas e formulários da aplicação
└── App.tsx           → Componente principal, que une todas as telas
```

## Funcionalidades

### Cadastro de pessoas

- Criar, listar e remover pessoas
- Ao remover uma pessoa, todas as suas transações são apagadas automaticamente

### Cadastro de transações

- Criar e listar transações (não há edição ou remoção, conforme especificação)
- Toda transação precisa estar vinculada a uma pessoa já cadastrada

### Consulta de totais

- Exibe o total de receitas, despesas e saldo de cada pessoa
- Exibe também o total geral, somando todas as pessoas

## Regras de negócio

1. **Menor de idade só cadastra despesa.** Pessoas com menos de 18 anos não podem ter transações do tipo "Receita" cadastradas em seu nome.
2. **A pessoa da transação precisa existir.** Não é possível criar uma transação apontando para um identificador de pessoa que não está cadastrado.
3. **Exclusão em cascata.** Ao remover uma pessoa, todas as transações associadas a ela são removidas junto, automaticamente.

Quando uma dessas regras é violada, a API responde com um erro claro:

| Situação | Código HTTP |
|---|---|
| Pessoa não encontrada | `404 Not Found` |
| Menor de idade cadastrando receita | `400 Bad Request` |

## Como executar o projeto

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado
- [Node.js](https://nodejs.org/) instalado

### 1. Rodando o back-end

Abra um terminal na pasta `GastosResidenciais.Api` e execute:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

O último comando exibirá a porta em que a API está rodando (exemplo: `http://localhost:5077`). O banco de dados (`GastosResidenciais.db`) é criado automaticamente na primeira execução, caso ainda não exista.

### 2. Rodando o front-end

Em **outro terminal**, na pasta `gastos-frontend`, execute:

```bash
npm install
npm run dev
```

A aplicação estará disponível em `http://localhost:5173`.

> **Importante:** o back-end e o front-end precisam estar rodando **ao mesmo tempo**, cada um em seu próprio terminal, para que a aplicação funcione corretamente.

### 3. Testando a API isoladamente (opcional)

O arquivo `GastosResidenciais.Api/requests.http` contém exemplos de requisições prontas para testar cada funcionalidade da API diretamente, sem depender do front-end. Para utilizá-lo, é necessário o VS Code com a extensão **REST Client**.

## Persistência de dados

Os dados são armazenados em um arquivo de banco SQLite (`GastosResidenciais.db`), criado automaticamente na pasta do back-end. Isso significa que as informações cadastradas continuam disponíveis mesmo depois de fechar e abrir a aplicação novamente.

## Possíveis melhorias futuras

- Autenticação e controle de acesso
- Edição de transações já cadastradas
- Filtros e paginação nas listagens
- Testes automatizados (unitários e de integração)
