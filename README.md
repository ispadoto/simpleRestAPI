# Simple REST API with React, Angular and .NET

[English](#english) | [Português](#português)

## English

### Overview
A full-stack employee management system built with React, Angular, .NET 8, and SQL Server. The application implements a clean architecture pattern with separation of concerns and follows SOLID principles.

### First Login
- **Username**: admin
- **Password**: 123456

### Architecture

#### Backend (.NET 8)
- **Clean Architecture Pattern**
  - Domain Layer: Core business logic and entities
  - Application Layer: Use cases and business rules
  - Infrastructure Layer: External services and data access
  - Presentation Layer: API controllers and DTOs

#### Frontend (React)
- **Feature-Sliced Architecture**
  - Components: Reusable UI components
  - Services: API communication and business logic
  - Hooks: Custom React hooks for state management
  - Utils: Helper functions and constants

#### Frontend (Angular)
- **Module-Based Architecture**
  - Core Module: Singleton services and guards
  - Shared Module: Common components and directives
  - Feature Modules: Employee and Auth modules
  - Lazy Loading: Route-based code splitting

### Design Patterns Used
1. **Repository Pattern**
   - Data access abstraction
   - Implementation: `GenericRepository<T>`

2. **Unit of Work Pattern**
   - Transaction management
   - Implementation: `UnitOfWork`

3. **Factory Pattern**
   - Object creation
   - Implementation: `EmployeeFactory`

4. **Strategy Pattern**
   - Algorithm selection
   - Implementation: Password validation strategies

5. **Observer Pattern**
   - Event handling
   - Implementation: React's state management and Angular's RxJS

6. **Dependency Injection Pattern**
   - Service injection
   - Implementation: Angular's DI system

### Features
- User authentication with JWT
- Role-based access control
- CRUD operations for employees
- Phone number management
- Form validation
- Responsive design
- Multiple frontend implementations (React and Angular)

### Technologies
- Backend: .NET 8, C#, Entity Framework Core
- Frontend: 
  - React: TypeScript, Vite, CSS Modules
  - Angular: TypeScript, Angular CLI, Angular Material
- Database: SQL Server
- Authentication: JWT
- State Management: 
  - React: React Hooks
  - Angular: RxJS, Services

### Getting Started

#### Prerequisites
- .NET 8 SDK
- Node.js 18+
- SQL Server
- Visual Studio 2022 or VS Code
- Docker

#### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/simpleRestAPIGit.git
```

2. Backend Setup:
```bash
cd Presentation.API
dotnet restore
dotnet run
```

3. Frontend Setup (React):
```bash
cd presentation.reactfront
npm install
npm run dev
```

4. Frontend Setup (Angular):
```bash
cd Presentantion.AngularFront
npm install
ng serve
```

5. Database Setup:
```bash
cd Presentation.API
dotnet ef database update
```

### API Endpoints
- POST /api/Auth/login
- GET /api/Employee
- GET /api/Employee/{id}
- POST /api/Employee
- PUT /api/Employee/{id}
- DELETE /api/Employee/{id}

### Testing
```bash
# Backend Tests
cd Presentation.API
dotnet test

# Frontend Tests (React)
cd presentation.reactfront
npm test

# Frontend Tests (Angular)
cd Presentantion.AngularFront
ng test
```

## Português

### Visão Geral
Sistema de gerenciamento de funcionários full-stack construído com React, Angular, .NET 8 e SQL Server. A aplicação implementa o padrão de arquitetura limpa com separação de responsabilidades e segue os princípios SOLID.

### Primeiro Acesso
- **Usuário**: admin
- **Senha**: 123456

### Arquitetura

#### Backend (.NET 8)
- **Padrão de Arquitetura Limpa**
  - Camada de Domínio: Lógica de negócio e entidades
  - Camada de Aplicação: Casos de uso e regras de negócio
  - Camada de Infraestrutura: Serviços externos e acesso a dados
  - Camada de Apresentação: Controladores da API e DTOs

#### Frontend (React)
- **Arquitetura Fatiada por Funcionalidades**
  - Componentes: Componentes de UI reutilizáveis
  - Serviços: Comunicação com API e lógica de negócio
  - Hooks: Hooks personalizados para gerenciamento de estado
  - Utils: Funções auxiliares e constantes

#### Frontend (Angular)
- **Arquitetura Baseada em Módulos**
  - Módulo Core: Serviços singleton e guards
  - Módulo Shared: Componentes e diretivas comuns
  - Módulos de Feature: Módulos de Employee e Auth
  - Lazy Loading: Divisão de código baseada em rotas

### Padrões de Design Utilizados
1. **Padrão Repository**
   - Abstração de acesso a dados
   - Implementação: `GenericRepository<T>`

2. **Padrão Unit of Work**
   - Gerenciamento de transações
   - Implementação: `UnitOfWork`

3. **Padrão Factory**
   - Criação de objetos
   - Implementação: `EmployeeFactory`

4. **Padrão Strategy**
   - Seleção de algoritmos
   - Implementação: Estratégias de validação de senha

5. **Padrão Observer**
   - Manipulação de eventos
   - Implementação: Gerenciamento de estado do React e RxJS do Angular

6. **Padrão Injeção de Dependência**
   - Injeção de serviços
   - Implementação: Sistema de DI do Angular

### Funcionalidades
- Autenticação de usuários com JWT
- Controle de acesso baseado em funções
- Operações CRUD para funcionários
- Gerenciamento de números de telefone
- Validação de formulários
- Design responsivo
- Múltiplas implementações frontend (React e Angular)

### Tecnologias
- Backend: .NET 8, C#, Entity Framework Core
- Frontend: 
  - React: TypeScript, Vite, CSS Modules
  - Angular: TypeScript, Angular CLI, Angular Material
- Banco de Dados: SQL Server
- Autenticação: JWT
- Gerenciamento de Estado: 
  - React: React Hooks
  - Angular: RxJS, Services

### Começando

#### Pré-requisitos
- .NET 8 SDK
- Node.js 18+
- SQL Server
- Visual Studio 2022 ou VS Code
- Docker

#### Instalação

1. Clone o repositório:
```bash
git clone https://github.com/yourusername/simpleRestAPIGit.git
```

2. Configuração do Backend:
```bash
cd Presentation.API
dotnet restore
dotnet run
```

3. Configuração do Frontend (React):
```bash
cd presentation.reactfront
npm install
npm run dev
```

4. Configuração do Frontend (Angular):
```bash
cd Presentantion.AngularFront
npm install
ng serve
```

5. Configuração do Banco de Dados:
```bash
cd Presentation.API
dotnet ef database update
```

### Endpoints da API
- POST /api/Auth/login
- GET /api/Employee
- GET /api/Employee/{id}
- POST /api/Employee
- PUT /api/Employee/{id}
- DELETE /api/Employee/{id}

### Testes
```bash
# Testes do Backend
cd Presentation.API
dotnet test

# Testes do Frontend (React)
cd presentation.reactfront
npm test

# Testes do Frontend (Angular)
cd Presentantion.AngularFront
ng test
```

### Referências
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [SOLID Principles](https://www.digitalocean.com/community/conceptual_articles/s-o-l-i-d-the-first-five-principles-of-object-oriented-design)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Unit of Work Pattern](https://martinfowler.com/eaaCatalog/unitOfWork.html)
- [React Best Practices](https://reactjs.org/docs/thinking-in-react.html)
- [Angular Documentation](https://angular.io/docs)
- [TypeScript Documentation](https://www.typescriptlang.org/docs/)
