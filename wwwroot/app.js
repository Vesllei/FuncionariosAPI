const apiUrl = "/api/funcionarios";

// Captura os links do menu
const menuLinks = document.querySelectorAll(".menu a");
// Todas as páginas
const pages = document.querySelectorAll(".page");

// Containers
const geralCadastroCard = document.querySelector("#geral #cadastroCard");
const geralListaCard = document.querySelector("#geral #listaCard");

const cadastroSection = document.querySelector("#cadastro .cadastro-card");
const funcionariosSection = document.querySelector("#funcionarios .lista-card");

// Função para navegar entre páginas
function navigateTo(pageId) {
  pages.forEach((p) => p.classList.remove("active"));
  document.getElementById(pageId).classList.add("active");

  menuLinks.forEach((link) => {
    link.classList.toggle("active", link.dataset.page === pageId);
  });

  // Renderiza conteúdos conforme página
  if (pageId === "geral") {
    renderCadastroForm(geralCadastroCard);
    fetchFuncionarios(geralListaCard);
  } else if (pageId === "cadastro") {
    renderCadastroForm(cadastroSection);
  } else if (pageId === "funcionarios") {
    fetchFuncionarios(funcionariosSection);
  }
}

// Renderiza formulário de cadastro dentro do container passado
function renderCadastroForm(container) {
  container.innerHTML = `
    <h2>Cadastro de Funcionário</h2>
    <form id="addFuncionarioForm" class="formulario">
      <div class="form-field">
        <label for="nome">Nome:</label>
        <input type="text" id="nome" placeholder="Nome do Funcionário" required />
      </div>
      <div class="form-field">
        <label for="cargo">Cargo:</label>
        <input type="text" id="cargo" placeholder="Cargo do Funcionário" required />
      </div>
      <div class="form-field">
        <label for="salario">Salário:</label>
        <input type="number" id="salario" step="0.01" placeholder="Salário do Funcionário" required />
      </div>
      <div class="form-field">
        <label for="dataAdmissao">Data de Admissão:</label>
        <input type="date" id="dataAdmissao" required />
      </div>
      <button type="submit">Adicionar</button>
    </form>
  `;

  const form = container.querySelector("#addFuncionarioForm");
  form.addEventListener("submit", addFuncionario);
}

// Busca e renderiza lista de funcionários no container passado
async function fetchFuncionarios(container) {
  container.innerHTML = "<p>Carregando...</p>";
  try {
    const res = await fetch(apiUrl);
    if (!res.ok) throw new Error("Erro ao buscar funcionários");
    const funcionarios = await res.json();

    if (funcionarios.length === 0) {
      container.innerHTML = "<p>Nenhum funcionário cadastrado.</p>";
      return;
    }

    container.innerHTML = "";
    funcionarios.forEach((func) => {
      const card = document.createElement("div");
      card.className = "funcionario-card";
      card.innerHTML = `
        <h3>${func.nome}</h3>
        <p><strong>Cargo:</strong> ${func.cargo}</p>
        <p><strong>Salário:</strong> R$ ${func.salario.toFixed(2)}</p>
        <p><strong>Admissão:</strong> ${new Date(
          func.dataAdmissao
        ).toLocaleDateString()}</p>
        <button class="delete-btn" data-id="${func.id}">Excluir</button>
      `;
      container.appendChild(card);
    });

    // Adiciona evento de exclusão em todos os botões
    container.querySelectorAll(".delete-btn").forEach((btn) => {
      btn.addEventListener("click", () => {
        const id = btn.getAttribute("data-id");
        deleteFuncionario(id, container);
      });
    });
  } catch (err) {
    container.innerHTML = `<p style="color:red;">${err.message}</p>`;
  }
}

async function addFuncionario(e) {
  e.preventDefault();
  const form = e.target;

  const novoFuncionario = {
    nome: form.nome.value,
    cargo: form.cargo.value,
    salario: parseFloat(form.salario.value),
    dataAdmissao: form.dataAdmissao.value,
  };

  try {
    const res = await fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(novoFuncionario),
    });

    if (!res.ok) throw new Error("Erro ao adicionar funcionário!");

    form.reset();

    // Atualiza lista na página "Geral" e "Funcionários" se estiverem visíveis
    if (document.getElementById("geral").classList.contains("active")) {
      fetchFuncionarios(geralListaCard);
    }
    if (document.getElementById("funcionarios").classList.contains("active")) {
      fetchFuncionarios(funcionariosSection);
    }
  } catch (err) {
    alert(err.message);
  }
}

async function deleteFuncionario(id, container) {
  if (confirm("Tem certeza que deseja excluir este funcionário?")) {
    try {
      const res = await fetch(`${apiUrl}/${id}`, {
        method: "DELETE",
      });
      if (res.status !== 204) throw new Error("Erro ao excluir funcionário!");

      // Atualiza lista depois de excluir
      fetchFuncionarios(container);
    } catch (err) {
      alert(err.message);
    }
  }
}

// Inicializa na página "Geral"
navigateTo("geral");

// Adiciona evento nos links do menu para navegação
menuLinks.forEach((link) => {
  link.addEventListener("click", (e) => {
    e.preventDefault();
    navigateTo(link.dataset.page);
  });
});
