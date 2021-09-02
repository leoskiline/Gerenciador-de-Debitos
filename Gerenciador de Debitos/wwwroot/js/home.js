class GerenciarContas {

    constructor() {
    }

    obterUsuario() {
        $.ajax({
            url: "/Home/obterUsuario",
            method: "GET",
            type: "GET",
            success: function (ret) {
                $("#nomeUsuario").html(ret[0].value);
                $("#nivelUsuario").html(ret[3].value);
            },
            error: function () {
                $("#nomeUsuario").html("Usuario nao carregado");
                $("#nivelUsuario").html("Nivel nao carregado");
            }
        })
    }
    
}

var _gerenciarContas = new GerenciarContas();
$(document).ready(function () {
    _gerenciarContas.obterUsuario();
});