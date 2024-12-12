$(document).ready(function () {

    LimparFormOficina();

    var oTable = $('#tblOficina').dataTable({
        "scrollX": false,
        "scrollCollapse": false,
        "searching": true,
        "order": [],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
            $('td:eq(3)', nRow).html('<a name="btnEditOficina" class="link-editar" title="Editar" href="#" data-identificador="' + aData.Id + '"><i class="fa fa-pencil-square-o fa-lg"></i></a>');
            $('td:last-child', nRow).html('<a name="btnDeleteOficina" class="link-excluir" title="Excluir" href="#" data-identificador="' + aData.Id + '"><i class="fa fa-trash-o fa-lg"></i></a>');
        },
        "aoColumnDefs": [
            {
                "targets": [0],
                "mDataProp": "Id"
            },
            {
                "targets": [1],
                "mDataProp": "Descricao",
                'orderable': true
            },
            {
                "targets": [2],
                "mDataProp": "AtivoDescricao",
                'orderable': true
            },
            {
                'orderable': false,
                'aTargets': [3],
                "data": null,
                "defaultContent": ''
            },
            {
                'orderable': false,
                'aTargets': [4],
                "data": null,
                "defaultContent": ''
            }
        ],
        "ajax": {
            url: $('#tblOficina').data('request-url'),
            beforeSend: function () {

            },
            complete: function () {

            },
            error: function (xhr, textStatus, errorThrown) {
                $("#tblOficina").DataTable().clear().draw();
                toastr.error(textStatus);
            }
        }
    });

    function gridAtivo(ativo) {
        if (ativo) {
            return '<i class="fa fa-check fa-lg" style="color: green;"></i></a>';
        } else {
            return '<i class="fa fa-check fa-lg" style="color: gray;"></i></a>';
        }
    }

    $("#formOficina").submit(function (e) {
        e.preventDefault();

        //Se o formulario não for válido...
        var form = $('#formOficina');
        if (!form.valid()) {
            return false;
        }

        var url = $("#urlSalvarOficina").val();
        $.ajax({
            url: url,
            type: "POST",
            data: $('#formOficina').serialize(),
            success: function (result) {
                if (result.success) {
                    recarregarTabela();
                    toastr.success("Registro salvo com sucesso!");
                    LimparFormOficina();
                } else {
                    toastr.warning(result.aErro);
                }
            },
            beforeSend: function () {
                waitingDialog.show("Aguarde. Salvando o tipo de documento...");
            },
            complete: function () {
                waitingDialog.hide();
            },
            error: function (xhr) {
                toastr.warning("Não foi possivel salvar o registro.");
            }
        });

        return false;
    });


    $(document).on("click", "a[name='btnEditOficina']", function () {
        var btn = $(this);
        var url = $("#urlEditarOficina").val();
        var ident = btn.data("identificador");
        $.ajax({
            url: url,
            type: "POST",
            async: false,
            data: { Id: ident },
            success: function (result) {
                if (result.success) {
                    ResetValidationForm("formOficina");
                    PreencheForm(result.arquivo);

                } else {
                    toastr.warning(result.aErro);
                }
            },
            beforeSend: function () {

                waitingDialog.show("Carregando registro. Aguarde....");
            },
            complete: function () {
                waitingDialog.hide();
            },
            error: function (xhr) {
                toastr.warning("Não foi possivel editar o registro.");
            }
        });
        return false;

    });



    function recarregarTabela() {
        oTable.api().clear().draw();
        oTable.api().ajax.reload();
    }

    function LimparFormOficina() {
        idOficina = "";
        $("#Descricao", '#formOficina').val("");
        $("#Id", '#formOficina').val("");
        $("#IsAtivo", '#formOficina').prop('checked', true);
        ExibeBotaoCancelarOficina(false);
    }

    function ExibeBotaoCancelarOficina(exibir) {
        if (exibir) {
            $("#btnCancelarOficina").show();
            $("#btnSalvarOficina").prop('title', 'Salvar alterações');
        }
        else {
            $("#btnCancelarOficina").hide();
            $("#btnSalvarOficina").prop('title', 'Salvar');
        }
    }

    function PreencheForm(obj) {
        $("#Id", '#formOficina').val(obj.Id);
        $("#Descricao", '#formOficina').val(obj.Descricao);
        $("#IsAtivo", '#formOficina').prop("checked", obj.IsAtivo === true);
        ExibeBotaoCancelarOficina(true);
    }

    $(document).on("click", "button[id='btnCancelarOficina']", function () {
        LimparFormOficina();
    });

    /********************************************************************************/
    /*************************      EXCLUSÃO      **************************/
    /********************************************************************************/

    var idOficina = "";


    $(document).on("click", "a[name='btnDeleteOficina']", function () {
        var btn = $(this);
        idOficina = btn.data("identificador");
        formDeleteDialog.show({ id: idOficina });
        return false;
    });

    $(document).on('click', "#btnDeleteConfirm", function (e) {
        var url = $("#urlDeleteOficina").val();
        var id = $(this).data("identificador");

        $.ajax({
            url: url,
            type: "POST",
            async: false,
            data: { Id: id },
            success: function (result) {
                if (result.success) {
                    recarregarTabela();
                    toastr.success("Registro excluido com sucesso.");
                    //Se o registro excluído estiver em modo de edição, limpo o formulario de edição...
                    if (parseInt($("#Id", '#formOficina').val(), 0) === idOficina) {
                        LimparFormOficina();
                    }
                } else {
                    toastr.warning(result.aErro);
                }
            },
            beforeSend: function () {
                formDeleteDialog.hide();
                waitingDialog.show("Executando exclusão do registro. Aguarde....");
            },
            complete: function () {
                waitingDialog.hide();
            },
            error: function (xhr) {
                toastr.warning("Não foi possivel excluir o resgitro.");
            }
        });
        return false;
    });

    var formDeleteDialog = formDeleteDialog || (function ($) {
        'use strict';
        function GetDialog() {
            var modal = $("#formDeleteDialog");

            if (modal.length === 0) {
                modal = $(
                    '<div id="formDeleteDialog" class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="top:0; overflow-y:visible;">' +
                    '<div class="modal-dialog modal-m">' +
                    '<div id="myModalContent" class="modal-content">' +
                    '<div class="modal-header">' +
                    '<h4 class="modal-title">Confirmação</h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<p> Deseja continuar com a exclusão do registro selecionado? </p>' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    '<button type="button" class="btn pull-left btn-default" data-dismiss="modal">Não</button>' +
                    '<button type="button" class="btn pull-right btn-primary" id="btnDeleteConfirm" data-identificador="0" >Sim</button>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>');
            }
            return modal;
        }

        var $dialog = GetDialog();

        return {
            show: function (options) {

                if (typeof options === 'undefined') {
                    options = {};
                }

                var settings = $.extend({
                    dialogSize: 'm',
                    id: null
                }, options);
                $dialog.find('#btnDeleteConfirm').data('identificador', options.id);
                $dialog.modal();
            },
            hide: function () {
                if (typeof $dialog !== 'undefined') {
                    $dialog.modal('hide');
                }
            }
        };

    })(jQuery);
});