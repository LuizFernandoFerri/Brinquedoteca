document.addEventListener("DOMContentLoaded", function () {
    var form = document.querySelector("form");
    form.addEventListener("submit", function () {
        var loadingModal = new bootstrap.Modal(document.getElementById("loadingModal"));
        loadingModal.show();
    });
});

function formatarCPF(cpfInput) {
    let cpf = cpfInput.value;
    cpf = cpf.replace(/\D/g, ''); // Remove todos os caracteres que não são dígitos
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2'); // Insere um ponto após os próximos 3 dígitos
    cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2'); // Insere um ponto após os próximos 3 dígitos
    cpf = cpf.replace(/(\d{3})(\d)/, '$1-$2'); // Insere um traço após os próximos 3 dígitos
    cpfInput.value = cpf;
}

function formatarCEP(cepInput) {
    let cep = cepInput.value;
    cep = cep.replace(/\D/g, '');
    cep = cep.replace(/(\d{5})(\d)/, '$1-$2');
    cepInput.value = cep;
}

function formataTelefoneFixoInput(telefoneFixoInput) {
    let telefoneFixo = celularInput.value;
    telefoneFixo = telefoneFixo.replace(/\D/g, '');
    telefoneFixo = telefoneFixo.replace(/(\d{0})(\d)/, '$1($2');
    telefoneFixo = telefoneFixo.replace(/(\d{2})(\d)/, '$1) $2');
    telefoneFixo = telefoneFixo.replace(/(\d{4})(\d)/, '$1-$2');
    telefoneFixoInput.value = telefoneFixo;
}

function formataCelularInput(celularInput) {
    let celular = celularInput.value;
    celular = celular.replace(/\D/g, '');
    celular = celular.replace(/(\d{0})(\d)/, '$1($2');
    celular = celular.replace(/(\d{2})(\d)/, '$1) $2');
    celular = celular.replace(/(\d{5})(\d)/, '$1-$2');
    celularInput.value = celular;
}