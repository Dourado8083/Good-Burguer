# 🍔 Good Burger — Sistema de Gestão de Pedidos

O **Good Burger** é uma aplicação Fullstack desenvolvida para gerenciar o fluxo de vendas de uma hamburgueria artesanal. O projeto contempla desde a seleção de produtos pelo cliente até um painel administrativo completo para gestão do cardápio.

---

## 🚀 Funcionalidades

### Área do Cliente
- **Cardápio Dinâmico:** Visualização de lanches, acompanhamentos e bebidas carregados diretamente do banco de dados.
- **Sistema de Combos:** Cálculo automático de descontos progressivos:
  - 🥪 + 🍟 + 🥤 → **20% de desconto**
  - 🥪 + 🥤 → **15% de desconto**
  - 🥪 + 🍟 → **10% de desconto**
- **Resumo de Pedido:** Exibição detalhada de subtotal, descontos aplicados e valor final.

### Área Administrativa (Painel Admin)
- **CRUD de Produtos:** Cadastro, visualização e exclusão de itens do cardápio em tempo real.
- **Controle de Acesso:** Simulação de perfil administrativo para proteção de funções críticas.
- **Persistência:** Integração total com MySQL para garantir que os dados não sejam perdidos ao reiniciar a aplicação.

---

## 🛠️ Tecnologias Utilizadas

### Frontend
- **Blazor WebAssembly (.NET 10):** Interface reativa e performática.
- **Bootstrap 5:** Estilização moderna e responsiva.

### Backend (API)
- **ASP.NET Core Web API:** Processamento das regras de negócio e endpoints RESTful.
- **Entity Framework Core:** ORM para comunicação eficiente com o banco de dados.
- **MySQL:** Banco de dados relacional para armazenamento de pedidos e produtos.

---

## 📐 Arquitetura do Projeto

O projeto segue uma estrutura de **Camadas (N-Tier)** para garantir a separação de responsabilidades:

```
GoodBurger/
├── GoodBurger.Shared/     # Models compartilhados entre API e Web
├── GoodBurger.Api/        # Controllers, Services e acesso ao banco
├── GoodBurger.Web/        # Interface Blazor que consome a API
└── GoodBurger.sln
```

```
Blazor WASM → HttpClient → (HTTP) → ASP.NET Core API → MySQL
      └─────────────────────────────────────┘
                  GoodBurger.Shared (Models)
```

---

## 🔧 Como Executar o Projeto

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- MySQL Server instalado e rodando
- Docker (opcional)

### 1. Configuração do Banco de Dados

Execute o seguinte script no seu MySQL:

```sql
CREATE DATABASE goodburgerdb;
USE goodburgerdb;

CREATE TABLE cardapio (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Categoria VARCHAR(50)
);

CREATE TABLE pedidos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SanduicheNome VARCHAR(100),
    TemBatata TINYINT(1),
    TemRefrigerante TINYINT(1),
    Subtotal DECIMAL(10,2),
    Desconto DECIMAL(10,2),
    TotalFinal DECIMAL(10,2),
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO cardapio (Nome, Preco, Categoria) VALUES
('X Burger', 5.00, 'Sanduiche'),
('X Egg', 4.50, 'Sanduiche'),
('X Bacon', 7.00, 'Sanduiche'),
('Batata frita', 2.00, 'Acompanhamento'),
('Refrigerante', 2.50, 'Bebida');
```

### 2. Configuração da API

No arquivo `appsettings.json` do projeto `GoodBurger.Api`, ajuste a string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=goodburgerdb;Uid=seu_usuario;Pwd=sua_senha;"
}
```

### 3. Rodar a Aplicação

Na raiz do projeto, abra dois terminais:

**Terminal 1 — API:**
```bash
cd GoodBurger.Api
dotnet run
```

**Terminal 2 — Web:**
```bash
cd GoodBurger.Web
dotnet run
```

> O projeto **GoodBurger.Shared** não precisa ser executado — é uma biblioteca compilada automaticamente.

---

## 🐳 Docker (em breve)

Suporte a Docker está sendo preparado. Em breve será possível subir toda a aplicação com:

```bash
docker-compose up
```

---

## 🧑‍💻 Autor

**Gustavo Dourado Santos**  
Systems Analyst & Fullstack Developer

[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?style=flat&logo=linkedin)](https://linkedin.com)
[![GitHub](https://img.shields.io/badge/GitHub-black?style=flat&logo=github)](https://github.com)
