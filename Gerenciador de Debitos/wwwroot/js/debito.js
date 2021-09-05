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
                                <tr class="debito">
                                    <td class="descricao">
                                        ${item.descricao}
                                    </td>
                                    <td class="text-center">
                                        ${dataFormatada}
                                    </td>
                                    <td class="text-center">
                                        <span name="inputValor">${new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(item.valor)}</span>
                                    </td>
                                    <td class="text-center">
                                         <button type="button" class="btn btn-dark rounded"   type="button"  data-toggle="modal" data-target=".bd-modal-lg" onclick="_gerenciarContas.AlterarConta(${item.idDebito},'${item.descricao}','${item.data}',${item.valor})">Alterar <i class="fas fa-pen-square ml-1"></i></button>
                                          <button type="button" class="btn btn-danger rounded"  type="button" onclick="_gerenciarContas.ExcluirConta(${item.idDebito})">Excluir <i class="fas fa-trash-alt ml-1"></i></button>
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

        $.ajax({
            url: "/Debito/CadastrarConta",
            method: "POST",
            type: "POST",
            data: dados,
        })
    }

    FiltrarConta() {
        let dados = {
            fdescricao: document.getElementById("fdescricao").value,
            fdata: document.getElementById("fdata").value,
            fvalor: document.getElementById("fvalor").value,
        }
        alert(dados.fdescricao);
        $.ajax({
            url: "/Debito/filtrarDebitos",
            method: "GET",
            type: "GET",
            data: dados,
        })
    }

    AlterarConta(codigo,descricao,data,valor) {
        document.getElementById("modalDescricao").value = descricao;
        document.getElementById("modalData").value = data.slice(0, 10);
        document.getElementById("modalValor").value = new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor)
    }

    ExcluirConta(idDebito) {
        let dados = {
            "idDebito": idDebito
        }
        Swal.fire({
            icon: "info",
            title: "Deseja Confirmar Exclusao?",
            confirmButtonText: "Confirmar",
            confirmButtonColor: "#3085d6",
            cancelButtonText: "Cancelar",
            cancelButtonColor: '#d33',
            showCancelButton: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Debito/Deletar",
                    type: "DELETE",
                    method: "DELETE",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify(dados),
                    success: (result) => {
                        if (result) {
                            this.obterDebitos()
                            Swal.fire(
                                {
                                    icon: "success",
                                    title: "Despesa Deletada com Sucesso.",
                                    confirmButtonColor: '#3085d6'
                                }
                            )
                        }
                        else {
                            Swal.fire(
                                {
                                    icon: "error",
                                    title: "Opss! Algo de Errado não esta certo.",
                                    confirmButtonColor: '#3085d6'
                                }
                            )
                        }
                    },
                    error: (retorno) => {
                        Swal.fire(
                            {
                                icon: "error",
                                title: "Opss! Algo de Errado não esta certo.",
                                confirmButtonColor: '#3085d6'
                            }
                        )
                    }
                })
                
            }
        })
    }

    mask() {
        $("#valor").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: true, allowZero: false });
        $("#fvalor").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: true, allowZero: false });
        $("#modalValor").maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: true, allowZero: false });
    }

}

var _gerenciarContas = new GerenciarContas();
$(document).ready(function () {
    _gerenciarContas.obterUsuario();
    _gerenciarContas.obterDebitos();
    _gerenciarContas.mask();
});


