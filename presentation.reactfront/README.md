# Simple REST API Frontend / Frontend da API REST Simples

[English](#english) | [Português](#português)

## English

### System Architecture Overview
This project is part of a larger system that consists of three main components:

1. **Backend (SimpleRestAPI)**
   - Built with .NET Core
   - RESTful API architecture
   - Entity Framework Core for data access
   - SQL Server database
   - Swagger documentation

2. **React Frontend (presentation.reactfront)**
   - Modern React application
   - TypeScript for type safety
   - Vite for fast development
   - Clean architecture pattern
   - Responsive design

3. **Angular Frontend (presentation.angularfront)**
   - Angular framework
   - TypeScript
   - Angular Material UI
   - Component-based architecture
   - Responsive design

### Overview
This project is a modern React frontend application built with TypeScript and Vite, designed to interact with a REST API. It follows best practices and modern development standards.

### Architecture
The project follows a clean architecture pattern with the following structure:
- **src/**: Contains all source code
  - **components/**: Reusable UI components
  - **services/**: API integration and business logic
  - **types/**: TypeScript type definitions
  - **utils/**: Utility functions and helpers
  - **hooks/**: Custom React hooks
  - **pages/**: Page components and routing

### Technologies Used
- **React 19**: Modern UI library
- **TypeScript**: For type-safe development
- **Vite**: Next-generation frontend build tool
- **ESLint**: Code linting and quality assurance
- **React Hooks**: For state management and side effects

### Features
- Modern and responsive UI
- Type-safe development with TypeScript
- Fast development with Hot Module Replacement (HMR)
- Code quality enforcement with ESLint
- Optimized build process with Vite

### Getting Started

#### Prerequisites
- Node.js (v18 or higher)
- npm or yarn package manager

#### Installation
1. Clone the repository:
```bash
git clone [repository-url]
cd presentation.reactfront
```

2. Install dependencies:
```bash
npm install
# or
yarn install
```

3. Start the development server:
```bash
npm run dev
# or
yarn dev
```

4. Open your browser and navigate to `http://localhost:5173`

#### Available Scripts
- `npm run dev`: Start development server
- `npm run build`: Build for production
- `npm run preview`: Preview production build
- `npm run lint`: Run ESLint

### Docker Deployment

#### Building the Docker Image
```bash
docker build -t react-frontend .
```

#### Running the Container
```bash
docker run -p 80:80 react-frontend
```

The application will be available at `http://localhost:80`

## Português

### Visão Geral da Arquitetura do Sistema
Este projeto faz parte de um sistema maior que consiste em três componentes principais:

1. **Backend (SimpleRestAPI)**
   - Desenvolvido com .NET Core
   - Arquitetura RESTful API
   - Entity Framework Core para acesso a dados
   - Banco de dados SQL Server
   - Documentação Swagger

2. **Frontend React (presentation.reactfront)**
   - Aplicação React moderna
   - TypeScript para tipagem segura
   - Vite para desenvolvimento rápido
   - Padrão de arquitetura limpa
   - Design responsivo

3. **Frontend Angular (presentation.angularfront)**
   - Framework Angular
   - TypeScript
   - Angular Material UI
   - Arquitetura baseada em componentes
   - Design responsivo

### Visão Geral
Este projeto é uma aplicação frontend React moderna construída com TypeScript e Vite, projetada para interagir com uma API REST. Segue as melhores práticas e padrões modernos de desenvolvimento.

### Arquitetura
O projeto segue um padrão de arquitetura limpa com a seguinte estrutura:
- **src/**: Contém todo o código fonte
  - **components/**: Componentes de UI reutilizáveis
  - **services/**: Integração com API e lógica de negócios
  - **types/**: Definições de tipos TypeScript
  - **utils/**: Funções utilitárias e helpers
  - **hooks/**: Hooks personalizados do React
  - **pages/**: Componentes de página e roteamento

### Tecnologias Utilizadas
- **React 19**: Biblioteca moderna de UI
- **TypeScript**: Para desenvolvimento com tipagem segura
- **Vite**: Ferramenta de build frontend de próxima geração
- **ESLint**: Linting de código e garantia de qualidade
- **React Hooks**: Para gerenciamento de estado e efeitos colaterais

### Funcionalidades
- UI moderna e responsiva
- Desenvolvimento com tipagem segura usando TypeScript
- Desenvolvimento rápido com Hot Module Replacement (HMR)
- Garantia de qualidade de código com ESLint
- Processo de build otimizado com Vite

### Como Começar

#### Pré-requisitos
- Node.js (v18 ou superior)
- Gerenciador de pacotes npm ou yarn

#### Instalação
1. Clone o repositório:
```bash
git clone [url-do-repositório]
cd presentation.reactfront
```

2. Instale as dependências:
```bash
npm install
# ou
yarn install
```

3. Inicie o servidor de desenvolvimento:
```bash
npm run dev
# ou
yarn dev
```

4. Abra seu navegador e acesse `http://localhost:5173`

#### Scripts Disponíveis
- `npm run dev`: Inicia o servidor de desenvolvimento
- `npm run build`: Compila para produção
- `npm run preview`: Visualiza a build de produção
- `npm run lint`: Executa o ESLint

### Implantação com Docker

#### Construindo a Imagem Docker
```bash
docker build -t react-frontend .
```

#### Executando o Container
```bash
docker run -p 80:80 react-frontend
```

A aplicação estará disponível em `http://localhost:80`
