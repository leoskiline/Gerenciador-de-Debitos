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

    dataTable() {
        $('#dataTable').DataTable({
            "destroy": true,
            "searching": true,
            "language": {
                "emptyTable": "Nenhum registro encontrado",
                "info": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "infoEmpty": "Mostrando 0 até 0 de 0 registros",
                "infoFiltered": "(Filtrados de _MAX_ registros)",
                "infoThousands": ".",
                "loadingRecords": "Carregando...",
                "processing": "Processando...",
                "zeroRecords": "Nenhum registro encontrado",
                "search": "Pesquisar",
                "paginate": {
                    "next": "Próximo",
                    "previous": "Anterior",
                    "first": "Primeiro",
                    "last": "Último"
                },
                "aria": {
                    "sortAscending": ": Ordenar colunas de forma ascendente",
                    "sortDescending": ": Ordenar colunas de forma descendente"
                },
                "select": {
                    "rows": {
                        "_": "Selecionado %d linhas",
                        "1": "Selecionado 1 linha"
                    },
                    "cells": {
                        "1": "1 célula selecionada",
                        "_": "%d células selecionadas"
                    },
                    "columns": {
                        "1": "1 coluna selecionada",
                        "_": "%d colunas selecionadas"
                    }
                },
                "buttons": {
                    "copySuccess": {
                        "1": "Uma linha copiada com sucesso",
                        "_": "%d linhas copiadas com sucesso"
                    },
                    "collection": "Coleção  <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                    "colvis": "Visibilidade da Coluna",
                    "colvisRestore": "Restaurar Visibilidade",
                    "copy": "Copiar",
                    "copyKeys": "Pressione ctrl ou u2318 + C para copiar os dados da tabela para a área de transferência do sistema. Para cancelar, clique nesta mensagem ou pressione Esc..",
                    "copyTitle": "Copiar para a Área de Transferência",
                    "csv": "CSV",
                    "excel": "Excel",
                    "pageLength": {
                        "-1": "Mostrar todos os registros",
                        "_": "Mostrar %d registros"
                    },
                    "pdf": "PDF",
                    "print": "Imprimir"
                },
                "autoFill": {
                    "cancel": "Cancelar",
                    "fill": "Preencher todas as células com",
                    "fillHorizontal": "Preencher células horizontalmente",
                    "fillVertical": "Preencher células verticalmente"
                },
                "lengthMenu": "Exibir _MENU_ resultados por página",
                "searchBuilder": {
                    "add": "Adicionar Condição",
                    "button": {
                        "0": "Construtor de Pesquisa",
                        "_": "Construtor de Pesquisa (%d)"
                    },
                    "clearAll": "Limpar Tudo",
                    "condition": "Condição",
                    "conditions": {
                        "date": {
                            "after": "Depois",
                            "before": "Antes",
                            "between": "Entre",
                            "empty": "Vazio",
                            "equals": "Igual",
                            "not": "Não",
                            "notBetween": "Não Entre",
                            "notEmpty": "Não Vazio"
                        },
                        "number": {
                            "between": "Entre",
                            "empty": "Vazio",
                            "equals": "Igual",
                            "gt": "Maior Que",
                            "gte": "Maior ou Igual a",
                            "lt": "Menor Que",
                            "lte": "Menor ou Igual a",
                            "not": "Não",
                            "notBetween": "Não Entre",
                            "notEmpty": "Não Vazio"
                        },
                        "string": {
                            "contains": "Contém",
                            "empty": "Vazio",
                            "endsWith": "Termina Com",
                            "equals": "Igual",
                            "not": "Não",
                            "notEmpty": "Não Vazio",
                            "startsWith": "Começa Com"
                        },
                        "array": {
                            "contains": "Contém",
                            "empty": "Vazio",
                            "equals": "Igual à",
                            "not": "Não",
                            "notEmpty": "Não vazio",
                            "without": "Não possui"
                        }
                    },
                    "data": "Data",
                    "deleteTitle": "Excluir regra de filtragem",
                    "logicAnd": "E",
                    "logicOr": "Ou",
                    "title": {
                        "0": "Construtor de Pesquisa",
                        "_": "Construtor de Pesquisa (%d)"
                    },
                    "value": "Valor",
                    "leftTitle": "Critérios Externos",
                    "rightTitle": "Critérios Internos"
                },
                "searchPanes": {
                    "clearMessage": "Limpar Tudo",
                    "collapse": {
                        "0": "Painéis de Pesquisa",
                        "_": "Painéis de Pesquisa (%d)"
                    },
                    "count": "{total}",
                    "countFiltered": "{shown} ({total})",
                    "emptyPanes": "Nenhum Painel de Pesquisa",
                    "loadMessage": "Carregando Painéis de Pesquisa...",
                    "title": "Filtros Ativos"
                },
                "thousands": ".",
                "datetime": {
                    "previous": "Anterior",
                    "next": "Próximo",
                    "hours": "Hora",
                    "minutes": "Minuto",
                    "seconds": "Segundo",
                    "amPm": [
                        "am",
                        "pm"
                    ],
                    "unknown": "-",
                    "months": {
                        "0": "Janeiro",
                        "1": "Fevereiro",
                        "10": "Novembro",
                        "11": "Dezembro",
                        "2": "Março",
                        "3": "Abril",
                        "4": "Maio",
                        "5": "Junho",
                        "6": "Julho",
                        "7": "Agosto",
                        "8": "Setembro",
                        "9": "Outubro"
                    },
                    "weekdays": [
                        "Domingo",
                        "Segunda-feira",
                        "Terça-feira",
                        "Quarta-feira",
                        "Quinte-feira",
                        "Sexta-feira",
                        "Sábado"
                    ]
                },
                "editor": {
                    "close": "Fechar",
                    "create": {
                        "button": "Novo",
                        "submit": "Criar",
                        "title": "Criar novo registro"
                    },
                    "edit": {
                        "button": "Editar",
                        "submit": "Atualizar",
                        "title": "Editar registro"
                    },
                    "error": {
                        "system": "Ocorreu um erro no sistema (<a target=\"\\\" rel=\"nofollow\" href=\"\\\">Mais informações<\/a>)."
                    },
                    "multi": {
                        "noMulti": "Essa entrada pode ser editada individualmente, mas não como parte do grupo",
                        "restore": "Desfazer alterações",
                        "title": "Multiplos valores",
                        "info": "Os itens selecionados contêm valores diferentes para esta entrada. Para editar e definir todos os itens para esta entrada com o mesmo valor, clique ou toque aqui, caso contrário, eles manterão seus valores individuais."
                    },
                    "remove": {
                        "button": "Remover",
                        "confirm": {
                            "_": "Tem certeza que quer deletar %d linhas?",
                            "1": "Tem certeza que quer deletar 1 linha?"
                        },
                        "submit": "Remover",
                        "title": "Remover registro"
                    }
                },
                "decimal": ","
            }
        });
    }

    obterDebitos() {
        $.ajax({
            url: "/Debito/obterDebitos",
            method: "GET",
            type: "GET",
            async: true,
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
                                    <td class="descricao">
                                        ${item.statusPagamento.status}
                                    </td>
                                    <td class="descricao">
                                        ${item.tipoConta}
                                    </td>
                                    <td class="text-center">
                                         <button type="button" class="btn btn-dark rounded"   type="button"  data-toggle="modal" data-target=".bd-modal-lg" onclick="_gerenciarContas.AlterarConta(${item.idDebito},'${item.descricao}','${item.data}',${item.valor})">Alterar <i class="fas fa-pen-square ml-1"></i></button>
                                          <button type="button" class="btn btn-danger rounded"  type="button" onclick="_gerenciarContas.ExcluirConta(${item.idDebito})">Excluir <i class="fas fa-trash-alt ml-1"></i></button>
                                    </td>
                                </tr>
                                `;
                });
                tbodyTable.innerHTML = linhas;
                _gerenciarContas.dataTable();
            },
            error: function () {
            }
        })
    }


    CadastrarConta() {

        let dados = {
            descricao: document.getElementById("descricao").value,
            data: document.getElementById("data").value,
            valor: document.getElementById("valor").value.slice(2, 30),
            tipoConta: document.getElementById("tipoConta").selectedIndex,
        }
        if (dados.descricao != "" && dados.data != "" && dados.valor != "") {
            $.ajax({
                url: "/Debito/CadastrarConta",
                method: "POST",
                type: "POST",
                data: dados,
                success: (ret) => {
                    if (ret) {
                        Swal.fire(
                            {
                                icon: ret.icon,
                                title: ret.msg,
                                confirmButtonColor: '#3085d6'
                            }
                        )
                        setTimeout(() => {
                            window.location.reload();
                        }, 1000)
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
                error: (ret) => {
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
        else {
            Swal.fire({
                icon: "warning",
                title: "Preencha Todos os Campos devidamente !",
                confirmButtonColor: '#3085d6'
            })
        }

    }


    AlterarConta(codigo, descricao, data, valor) {
        document.getElementById("modalCodigo").value = codigo;
        document.getElementById("modalDescricao").value = descricao;
        document.getElementById("modalData").value = data.slice(0, 10);
        document.getElementById("modalValor").value = new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor)

    }

    SalvarAlteracao() {

        let dados = {

            modalCodigo: document.getElementById("modalCodigo").value,
            modalDescricao: document.getElementById("modalDescricao").value,
            modalData: document.getElementById("modalData").value,
            modalValor: document.getElementById("modalValor").value.slice(3, 30)
        }

        $.ajax({
            url: "/Debito/AlterarDados",
            method: "PUT",
            type: "PUT",
            data: JSON.stringify(dados),
            contentType: "application/json",
            dataType: "json",
            success: (ret) => {
                if (ret) {
                    Swal.fire(
                        {
                            icon: "success",
                            title: "Conta Alterada com Sucesso.",
                            confirmButtonColor: '#3085d6'
                        }
                    )
                    setTimeout(() => {
                        window.location.reload();
                    }, 1000)
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
            error: (ret) => {
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
                                    title: "Conta Deletada com Sucesso.",
                                    confirmButtonColor: '#3085d6'
                                }
                            )
                            setTimeout(() => {
                                window.location.reload();
                            }, 1000)
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