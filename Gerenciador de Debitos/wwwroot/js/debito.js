class GerenciarContas {

    constructor() {
    }

    obterUsuario() {
        $.ajax({
            url: "/Debito/obterUsuario",
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

    obterDebitos() {
        $.ajax({
            url: "/Debito/obterDebitos",
            method: "GET",
            type: "GET",
            success: function (ret) {
                let tbodyTable = document.getElementById("tabela");
                let linhas = "";
                ret.forEach(item => {
                    let padraoData = item.data.slice(0, 10).split("-");
                    let dataFormatada = `${padraoData[2]}/${padraoData[1]}/${padraoData[0]}`;
                    linhas += `
                                <tr>
                                    <td>
                                        ${item.descricao}
                                    </td>
                                    <td class="text-center">
                                        ${dataFormatada}
                                    </td>
                                    <td class="text-center">
                                        <span name="inputValor">${new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(item.valor)}</span>
                                    </td>
                                    <td class="text-center">
                                        dois icones (excluir e alterar)
                                    </td>
                                </tr>
                                `;
                });
                tbodyTable.innerHTML = linhas;


            },
            error: function () {

            }
        })
    }


    CadastrarConta() {

        let dados = {
            descricao: document.getElementById("descricao").value,
            data: document.getElementById("data").value,
            valor: document.getElementById("valor").value,
        }
        alert(dados.descricao);
        alert(dados.valor);

    $.ajax({
        url: "/Debito/CadastrarConta",
        method: "POST",
        type: "POST",
        data: dados,
    })
}

}

var _gerenciarContas = new GerenciarContas();
$(document).ready(function () {
    _gerenciarContas.obterUsuario();
    _gerenciarContas.obterDebitos();
});


