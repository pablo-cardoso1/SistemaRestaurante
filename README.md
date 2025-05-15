# 🍽️ SistemaRestaurante

API RESTful para gerenciamento de reservas de mesas em restaurante, com autenticação JWT, controle de disponibilidade e permissões de usuário.

---

## 🚀 Funcionalidades

- Cadastro e login de usuários (JWT)
- CRUD de mesas (apenas admin)
- Reservas de mesas (usuário autenticado)
- Cancelamento de reservas
- Controle de status de mesas e reservas

---

## 🛠️ Stack Utilizada

- .NET Core  
- Entity Framework Core  
- SQL Server  
- JWT para autenticação

---

## ⚙️ Como Rodar Localmente

### 🔧 Passo a passo

```bash
# Clone o repositório
git clone https://github.com/pablo-cardoso1/SistemaRestaurante.git

# Entre na pasta do projeto
cd SistemaRestaurante

# Restaure as dependências
dotnet restore

# Aplique as migrations
dotnet ef database update

# Rode a aplicação
dotnet run
```

> ⚠️ **Configure o arquivo `appsettings.json`** com sua string de conexão e chave JWT antes de rodar o projeto.

---

## 📚 Principais Endpoints

### 🔐 Autenticação

```http
POST /api/usuario/registrar     → Cadastro de usuário
POST /api/usuario/login         → Login e geração de token JWT
```

### 🍽️ Mesas

```http
GET    /api/mesa                → Listar todas as mesas
POST   /api/mesa                → Criar nova mesa (somente admin)
PATCH  /api/mesa/{id}           → Atualizar mesa (somente admin)
DELETE /api/mesa/{id}           → Remover mesa (somente admin)
```

### 📅 Reservas

```http
POST   /api/reserva             → Criar reserva
GET    /api/reserva             → Listar reservas do usuário autenticado
PATCH  /api/reserva/{id}/cancelar → Cancelar reserva
```

---

## 📝 Exemplo de Payload - Criar Reserva

```json
{
  "mesaId": 1,
  "dataReserva": "2025-05-15T19:30:00",
  "quantidadePessoas": 4
}
```

---

## 🔒 Regras de Negócio

- Apenas administradores podem gerenciar mesas  
- Apenas o dono da reserva pode cancelá-la  
- Mesas são automaticamente marcadas como reservadas  
- Validações:
  - Capacidade da mesa  
  - Disponibilidade no horário  
  - Data/hora futuras

---

## 🔎 Documentação Interativa

Acesse:  
`http://localhost:{porta}/swagger`  
> Após rodar o projeto, você poderá testar todos os endpoints pela interface Swagger.

---

## 🗃️ Estrutura do Banco (Simplificada)

### 🧑‍💼 Usuário

- `id`  
- `nome`  
- `email`  
- `senha (hash)`  
- `role`  

### 🪑 Mesa

- `id`  
- `nome/numero`  
- `capacidade`  
- `status`  

### 📅 Reserva

- `id`  
- `usuarioId`  
- `mesaId`  
- `dataReserva`  
- `quantidadePessoas`  
- `status`  

---

## 🖼️ Prints & Diagrama

> ![image](https://github.com/user-attachments/assets/1325bb9f-17c1-4e17-97c8-c9d752465855)


---

## 📝 Licença

Este projeto está sob a licença [MIT](LICENSE).

---
