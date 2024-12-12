$(document).ready(function () {


    AtivarAutoComplete();

    $(document).off("click", "label[totvsSearchAutoComplete]")
        .on("click", "label[totvsSearchAutoComplete]", function () {
            valido = false;

            var elementId = this.attributes["totvsSearchAutoComplete"].value;
            var fieldsContainer = $(this).data('fields-container');
            var input = fieldsContainer !== undefined && fieldsContainer.length ? $('#' + elementId, "#" + fieldsContainer).val("") : $('#' + elementId);

            if ($(input)[0].disabled === true || $(input)[0].readOnly === true) {
                return;
            }

            $(input).autocomplete("search", "%%%");
            $(input).focus();

        });
});

var valido = false;

function AtivarAutoComplete() {
    var NoResultsLabel = "Nenhum resultado encontrado";
    $("input[TotvsAutoComplete]").autocomplete({
        minLength: 3,
        source: function (request, response) {
            beforeSelect(this);

            if (!valido)
                valido = LookupValidateClickFunction(this.element);

            if (valido) {
                var urlConsulta = this.element.data('request-url');
                var data = GetParameters(this, request.term);
                $.ajax({
                    url: urlConsulta,
                    dataType: "json",
                    data: data,
                    statusCode: {
                        404: function () {
                            toastr.warning("Consulta não encontrada.");
                        }
                    },
                    success: function (result) {
                        if (result.success) {
                            if (!result.data.length) {
                                results = [NoResultsLabel];
                                response(results);
                            } else {
                                response($.map(result.data, function (item) {
                                    return {
                                        label: item.Descricao,
                                        value: item.Descricao,
                                        obj: item
                                    }
                                }));
                            }
                        } else {
                            response([]);
                            toastr.error(result.aErro);
                        }
                    }
                });
            } else {
                results = "";
                response(results);
            }
        },
        focus: function (event, ui) {
            $(this).val(ui.item.value);
            return false;
        },
        select: function (event, ui) {
            if (ui.item.label === NoResultsLabel) {
                clearSetFields(this);
                clearFields(this);
            } else {

                setDisplayField(this, ui);
                setFields(this, ui);
                clearFields(this);
                afterSelect(this, event, ui);
            }
            return false;
        },
        change: function (event, ui) {
            if (ui.item == null) {
                clearSetFields(this);
                clearFields(this);
            }
            return false;
        },

        create: function (event, ui) {
            $(this).data('ui-autocomplete')._renderItem = function (ul, item) {

                var itemClass = "";
                var itemTitle = "";

                if (item.obj !== undefined && item.obj !== null) {

                    itemClass = item.obj.Class === undefined ? "" : item.obj.Class;
                    itemTitle = item.obj.Title === undefined ? "" : item.obj.Title;
                }
                
                return $('<li>')
                    .append("<span title = '" + itemTitle + "' class = '" + itemClass + "'>" + item.label + "</span>")
                    .appendTo(ul);
            };
           
        }
    }).keydown(function (event) {

        valido = keysAllowed(event.keyCode);

        if (!valido) {
            valido = LookupValidateClickFunction(this);
            if (!valido) {
                event.preventDefault();
                event.stopPropagation();
                valido = false;
            }
        } else {
            if (focus) {
                valido = false;
                focus = false;
            }
        }
    });
}


function setDisplayField(input, ui) {

    var displayFieldAfterSelection = $(input).data('display-field-after-selecion');

    if (displayFieldAfterSelection !== undefined && displayFieldAfterSelection !== null && displayFieldAfterSelection !== "") {
        input.value = ui.item.obj[displayFieldAfterSelection];
    }
}

function setFields(input, ui) {

    var fieldsSet = $(input).data('fields-set');
    var fieldsGet = $(input).data('fields-get');
    var fieldsContainer = $(input).data('fields-container');

    if (fieldsSet) {
        var sets = fieldsSet.split(';');
        var gets = fieldsGet.split(';');
        $.each(sets, function (i, val) {
            if (fieldsContainer !== undefined && fieldsContainer.length) {
                $("#" + val, "#" + fieldsContainer).val(ui.item.obj[gets[i]]);
            } else {
                $("#" + val).val(ui.item.obj[gets[i]]);
            }
        });
    }
}

function clearSetFields(input) {
    $(input).val("");
    var fieldsSet = $(input).data('fields-set');
    var fieldsContainer = $(input).data('fields-container');
    if (fieldsSet) {
        var fields = fieldsSet.split(';');
        $.each(fields, function (i, val) {
            if (fieldsContainer !== undefined && fieldsContainer.length) {
                $("#" + val, "#" + fieldsContainer).val("");
            } else {
                $("#" + val).val("");
            }
        });
    }
}

function clearFields(input) {
    var fieldsClear = $(input).data('fields-clear');
    var fieldsContainer = $(input).data('fields-container');
    if (fieldsClear) {
        var fields = fieldsClear.split(';');
        $.each(fields, function (i, val) {
            if (fieldsContainer !== undefined && fieldsContainer.length) {
                $("#" + val, "#" + fieldsContainer).val("");
            } else {
                $("#" + val).val("");
            }
        });
    }
}

function GetParameters(input, term) {
    var parameter = {};
    parameter['term'] = term;
    var params = input.element.data('parameters');
    var paramsGet = input.element.data('parameters-get');

    if (params) {
        var parameters = params.split(';');
        var parametersGets = [];
        if (typeof paramsGet !== "undefined")
            parametersGets = paramsGet.split(';');

        //Verifica a existência de conatiner para os parâmetros
        var fieldsContainer = input.element.data('fields-container');
        var parametersContainer = input.element.data('parameters-container');
        var isParametersContainerExist = parametersContainer !== undefined && parametersContainer.length > 0;
        var isParametersContainerArray = false;
        if (isParametersContainerExist && parametersContainer.indexOf(';') !== -1) {
            isParametersContainerArray = true;
            parametersContainer = parametersContainer.split(';');
        }

        $.each(parameters, function (i, val) {
            var value = parametersGets[i];

            fieldsContainer = isParametersContainerArray ? parametersContainer[i] :
                isParametersContainerExist ? parametersContainer : fieldsContainer;

            var isContainerExist = fieldsContainer !== undefined && fieldsContainer.length > 0;

            if (isContainerExist) {

                if ($("#" + val, "#" + fieldsContainer).is(':checkbox') == false) {
                    parameter[val] = $("#" + val, "#" + fieldsContainer).val();
                } else {
                    parameter[val] = $("#" + val, "#" + fieldsContainer).is(':checked');
                }
            } else {

                if ($("#" + val).is(':checkbox') == false) {
                    parameter[val] = $("#" + val).val();
                } else {
                    parameter[val] = $("#" + val).is(':checked');
                }
            }

            if (isNullOrEmpty(value)) {
                if (isContainerExist) {

                    if ($("#" + val, "#" + fieldsContainer).is(':checkbox') == false) {
                        parameter[val] = $("#" + val, "#" + fieldsContainer).val();
                    } else {
                        parameter[val] = $("#" + val, "#" + fieldsContainer).is(':checked');
                    }

                } else {

                    if ($("#" + val).is(':checkbox') == false) {
                        parameter[val] = $("#" + val).val();
                    } else {
                        parameter[val] = $("#" + val).is(':checked');
                    }
                }
            }
            else {
                if (isContainerExist) {

                    if ($("#" + value, "#" + fieldsContainer).is(':checkbox') == false) {
                        parameter[val] = $("#" + value, "#" + fieldsContainer).val();
                    } else {
                        parameter[val] = $("#" + value, "#" + fieldsContainer).is(':checked');
                    }

                } else {

                    if ($("#" + value).is(':checkbox') == false) {
                        parameter[val] = $("#" + value).val();
                    } else {
                        parameter[val] = $("#" + value).is(':checked');
                    }
                }
            }
        });
    }
    return parameter;
}

function LookupValidateClickFunction(input, callBackFunction) {
    var execFunction = $(input).data('validate-click-function');
    if (execFunction === undefined) {
        return true;
    }
    var result = window[execFunction]();
    return result === undefined || result;
}

function beforeSelect(input) {
    var valeu = $(input).data('beforeSelect');
    if (isNullOrEmpty(valeu) === false) {
        var fn = window[valeu];
        if (typeof fn === "function")
            fn(input);
    }
}

function afterSelect(input, event, ui) {
    var valeu = $(input).data('afterselect');
    if (isNullOrEmpty(valeu) === false) {
        var fn = window[valeu];
        if (typeof fn === "function")
            fn(input, event, ui);
    }
}

function keysAllowed(keyCode) {

    // 112 - 123 (Teclas F#)
    // 9 - TAB
    // 16 - Shift
    // 17 - Ctrl
    // 18 - Alt
    // 19 - Pause Breack
    // 20 - CapsLook
    // 27 - Esc
    // 33 - PageUp
    // 34 - PageDown
    // 35 - end
    // 36 - home
    // 37 - esquerda
    // 38 - cima
    // 39 - direita
    // 40 - baixo
    // 45 - insert
    // 144 Num Lock
    // 145 - ScrollLock

    var permitidos = (keyCode >= 33 && keyCode <= 39) || (keyCode >= 16 && keyCode <= 20) || (keyCode >= 112 && keyCode <= 123) || (keyCode === 9) || (keyCode === 45) || (keyCode >= 144 && keyCode <= 145) || (keyCode === 27);
    return permitidos;

}
