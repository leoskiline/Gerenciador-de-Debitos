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
                                        ${item.idDebito}
                                    </td>
                                    <td>
                                        ${item.descricao}
                                    </td>
                                    <td>
                                        ${dataFormatada}
                                    </td>
                                    <td>
                                        <input class="valor" id="valor" name="valor" value="${item.valor}" type="text" />
                                    </td>
                                    <td>
                                        dois icones (excluir e alterar)
                                    </td>
                                </tr>
                                `;
                });
                tbodyTable.innerHTML = linhas;
                $('#valor').mask('000.000.000.000.000,00', { reverse: true });
                $('#valor').mask("#.##0,00", { reverse: true });
            },
            error: function () {

            }
        })
    }
}

var _gerenciarContas = new GerenciarContas();
$(document).ready(function () {
    _gerenciarContas.obterUsuario();
    _gerenciarContas.obterDebitos();
});