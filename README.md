
# <p align="center">Gestão de Investimentos</p> 
<h3 align="center"> 
Projeto de conclusão da Fase 5 da Pós Tech Fiap Curso de Arquitetura de Sistemas .NET com Azure </h3>
</br>

## 🛠️ Construido com:

* .NET CORE 6 - O framework web usado
*  MongoDB - Banco de dados NoSQL
</br>

## ⚙️ Arquitetura : 

Para desenvolvimento do projeto optamos pela utilização do Clean Architeture pois
a mesma possibilita a organização do código, flexibilidade a mudanças e 
melhor manutenibilidade.

Camadas do sistema:

* Camada API: Recebe requisições HTTP e as encaminha para a camada application.
* Camada Application Define os serviçoes da aplicação e executa a lógica de negócios da aplicação.
* Camada Domain: Camada Core da aplicação, é onde as entidades são definidas.
* Camada Infra Data: Contém a implementação da persistência de dados como repositórios e mapeamentos para banco de dados.
* Camada Infra IoC: Responsável por configurar as dependências da aplicação usando DI.
* Camada Tests: Camada dedicada aos testes automatizados (testes unitários e testes de integração).
  
</br>

## ✒️ Autores:

* Andre Toledo Gama - Dev
* Bruna Reveriego - Dev
* Fernando Parissenti - Dev
* Rodrigo Reis - Dev
</br>

##  🔗 API Endpoints :

<h2>Autenticação</h2>
<h3>POST /Usuario/autenticar</h3>
<p>Endpoint para autenticação do usuário.</p>

<h4>Requisição:</h4>
<pre><code>{
  "email": "usuario@exemplo.com",
  "senha": "senha123"
}</code></pre>

<h4>Resposta:</h4>
<pre><code>{
  "token": "JWT_TOKEN"
}</code></pre>

<h2>Usuários</h2>
<h3>POST /Usuario/cadastrar</h3>
<p>Criar um novo usuário.</p>

<h4>Requisição:</h4>
<pre><code>{
  "nome": "João da Silva",
  "email": "joao@exemplo.com",
  "senha": "senha123"
}</code></pre>

<h4>Resposta:</h4>
<pre><code>{
  "id": "1",
  "nome": "João da Silva",
  "email": "joao@exemplo.com"
}</code></pre>

<h3>GET /Usuario/listar</h3>
<p>Lista os usuários do sistema.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token.</p>

<h4>Resposta:</h4>
<pre><code>[
  {
    "id": "1",
    "nome": "João da Silva",
    "email": "joao@exemplo.com"
  }
]</code></pre>

<h3>DELETE /Usuario/remover/{Id}</h3>
<p>Remove um usuário do sistema.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID do usuário.</p>

<h4>Resposta:</h4>
<p>Status code 200 OK.</p>

<h2>Portfólios</h2>

<h3>GET /Portfolio/listar</h3>
<p>Listar todos os portfólios do usuário autenticado.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token.</p>

<h4>Resposta:</h4>
<pre><code>[
  {
    "id": "1",
    "nome": "Portfólio A",
    "descricao": "Meu primeiro portfólio"
  }
]</code></pre>

<h3>POST /Portfolio/cadastrar</h3>
<p>Criar um novo portfólio.</p>

<h4>Requisição:</h4>
<pre><code>{
  "nome": "Portfólio B",
  "descricao": "Portfólio de teste"
}</code></pre>

<h4>Resposta:</h4>
<pre><code>{
  "id": "2",
  "nome": "Portfólio B",
  "descricao": "Portfólio de teste"
}</code></pre>

<h3>GET /Portfolio/{id}</h3>
<p>Obter detalhes de um portfólio específico.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID portolio.</p>

<h4>Resposta:</h4>
<pre><code>{
  "id": "1",
  "nome": "Portfólio A",
  "descricao": "Meu primeiro portfólio"
}</code></pre>

<h3>DELETE /Portfolio/{id}</h3>
<p>Remover um portfólio.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID portifolio .</p>

<h4>Resposta:</h4>
<p>Status code 200 OK.</p>

<h2>Ativos</h2>

<h3>GET /Ativo/listar</h3>
<p>Listar todos os ativos disponíveis.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token.</p>

<h4>Resposta:</h4>
<pre><code>[
  {
    "id": "1",
    "tipoAtivo": "Ações",
    "nome": "Apple",
    "codigo": "AAPL"
  }
]</code></pre>

<h3>POST /Ativo/cadastrar</h3>
<p>Criar um novo ativo.</p>

<h4>Requisição:</h4>
<pre><code>{
  "tipoAtivo": "Ações",
  "nome": "Apple",
  "codigo": "AAPL"
}</code></pre>

<h4>Resposta:</h4>
<pre><code>{
  "id": "1",
  "tipoAtivo": "Ações",
  "nome": "Apple",
  "codigo": "AAPL"
}</code></pre>

<h3>GET /Ativo/{id}</h3>
<p>Obter detalhes de um ativo específico.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID ativo .</p>

<h4>Resposta:</h4>
<pre><code>{
  "id": "1",
  "tipoAtivo": "Ações",
  "nome": "Apple",
  "codigo": "AAPL"
}</code></pre>

<h3>DELETE /Ativo/{id}</h3>
<p>Remover um ativo.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID ativo.</p>

<h4>Resposta:</h4>
<p>Status code 200 OK.</p>

<h2>Transações</h2>

<h3>GET /Transacao/listar</h3>
<p>Listar todas as transações de um portfólio.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token .</p>

<h4>Resposta:</h4>
<pre><code>[
  {
    "id": "1",
    "tipoTransacao": "Compra",
    "quantidade": 10,
    "preco": 150,
    "dataTransacao": "2024-09-03T12:00:00Z"
  }
]</code></pre>

<h3>POST /Transacao/cadastrar</h3>
<p>Registrar uma nova transação.</p>

<h4>Requisição:</h4>
<pre><code>{
  "portfolioId": "1",
  "ativoId": "1",
  "tipoTransacao": "Compra",
  "quantidade": 5,
  "preco": 300
}</code></pre>

<h4>Resposta:</h4>
<pre><code>{
  "id": "2",
  "tipoTransacao": "Compra",
  "quantidade": 5,
  "preco": 300,
  "dataTransacao": "2024-09-03T14:00:00Z"
}</code></pre>

<h3>GET /Transacao/{id}</h3>
<p>Obter detalhes de uma transação específica.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID transação .</p>

<h4>Resposta:</h4>
<pre><code>{
  "id": "1",
  "tipoTransacao": "Compra",
  "quantidade": 10,
  "preco": 150,
  "dataTransacao": "2024-09-03T12:00:00Z"
}</code></pre>

<h3>DELETE /Transacao/{id}</h3>
<p>Remover uma transação.</p>

<h4>Requisição:</h4>
<p>Header com JWT Token e ID transação .</p>

<h4>Resposta:</h4>
<p>Status code 200 OK.</p>


