const imageUploadArea = document.getElementById("imageUploadArea");
const fileInput = document.getElementById("fileInput");
const previewImage = document.getElementById("previewImage");

// Clique na área da imagem para abrir o seletor de arquivos
imageUploadArea.addEventListener("click", () => {
    fileInput.click();
});

// Preview da imagem selecionada
fileInput.addEventListener("change", (event) => {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = () => {
            previewImage.src = reader.result; // Mostra o preview da imagem
        };
        reader.readAsDataURL(file);
    }
});

document.getElementById("formProduto").addEventListener("submit", async (event) => {
    event.preventDefault();
    debugger;
    const formData = new FormData();
    const file = fileInput.files[0];

    // Dados do ProdutoViewModel
    const produtoViewModel = {
        IdProduto: document.getElementById("IdProduto").value,
        CodProduto: document.getElementById("CodProduto").value,
        Descricao: document.getElementById("Descricao").value,
        IdCategoria: document.getElementById("IdCategoria").value,
        FaixaEtaria: document.getElementById("IdFaixaEtaria").value,
        IdEstConservacao: document.getElementById("IdEstConservacao").value,
        Quantidade: document.getElementById("Quantidade").value,
        Status: document.getElementById("Status").value,
        Observacao: document.getElementById("Observacao").value,
    };

    debugger;
    // Adicionar dados do objeto ao FormData
    formData.append("ProdutoViewModel", JSON.stringify(produtoViewModel));

    // Adicionar a imagem ao FormData
    if (file) {
        formData.append("Imagem", file);
    }

    try {
        const response = await fetch("/produto/salvar", {
            method: "POST",
            body: formData,
        });

        if (!response.ok) throw new Error("Erro ao salvar produto!");

        alert("Produto salvo com sucesso!");
    } catch (error) {
        alert(error.message);
    }
});