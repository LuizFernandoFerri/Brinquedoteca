$(document).ready(function () {
    $('.js-example-basic-multiple').select2();

    $('#formFilter').on('submit', function (e) {
        e.preventDefault();

        $.ajax({
            url: $(this).attr('action'), // URL do formulário
            type: 'GET',
            data: $(this).serialize(),  // Serializa os dados do formulário
            success: function (data) {
                // Substitui o conteúdo da grid com os dados retornados
                $('#gridContainer').html(data);
            },
            error: function () {
                alert("Erro ao buscar clientes. Tente novamente.");
            }
        });
    });

    $("#codUsuarioSelect").select2();

    $("#codUsuario").on("keyup", function () {
        const query = $(this).val();

        if (query.length >= 3) {
            $.ajax({
                url: '/Cliente/BuscarUsuarios', // Substitua pelo endpoint correto
                type: 'GET',
                data: { termo: query },
                success: function (data) {
                    if (data && data.length > 0) {
                        $("#codUsuarioSelect").empty().show();

                        // Adiciona cada usuário ao select
                        data.forEach(usuario => {
                            $("#codUsuarioSelect").append(
                                `<option value="${usuario.codUsuario}">${usuario.nome}</option>`
                            );
                        });
                    } else {
                        $("#codUsuarioSelect").hide();
                    }
                },
                error: function () {
                    alert("Erro ao buscar usuários.");
                }
            });
        } else {
            $("#codUsuarioSelect").hide();
        }
    });
});