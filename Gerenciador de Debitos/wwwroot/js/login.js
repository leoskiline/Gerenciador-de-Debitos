class Login {
	constructor() {

	}

	validarCamposLogin(email, senha) {
		let errors = 0;
		if (email.length > 0 && !email.match("@")) {
			document.getElementById("obrigatorioEmailLogin").innerHTML = `<b class="text-danger">*</b> Por favor digite seu email corretamente.`;
			document.getElementById("obrigatorioEmailLogin").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioEmailLogin").classList.add("d-none")
			}, 4000);
			errors++;
		}
		else if (email.length == 0){
			document.getElementById("obrigatorioEmailLogin").innerHTML = `<b class="text-danger">*</b> Por favor digite seu email.`;
			document.getElementById("obrigatorioEmailLogin").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioEmailLogin").classList.add("d-none")
			}, 4000);
			errors++;
		}
		if (senha == "") {
			document.getElementById("obrigatorioSenhaLogin").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioSenhaLogin").classList.add("d-none")
			}, 4000);
			errors++;
		}
		return errors;
	}

	validarCamposRegister(email, senha,confirmarSenha,nome) {
		let errors = 0;
		if (email.length > 0 && !email.match("@")) {
			document.getElementById("obrigatorioEmailRegister").innerHTML = `<b class="text-danger">*</b> Por favor digite seu email corretamente.`;
			document.getElementById("obrigatorioEmailRegister").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioEmailRegister").classList.add("d-none")
			}, 4000);
			errors++;
		}
		else if (email.length == 0) {
			document.getElementById("obrigatorioEmailRegister").innerHTML = `<b class="text-danger">*</b> Por favor digite seu email.`;
			document.getElementById("obrigatorioEmailRegister").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioEmailRegister").classList.add("d-none")
			}, 4000);
			errors++;
		}
		if (senha.length == 0) {
			document.getElementById("obrigatorioSenhaRegister").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioSenhaRegister").classList.add("d-none")
			}, 4000);
			errors++;
		}
		if (confirmarSenha.length == 0) {
			document.getElementById("obrigatorioSenhaConfirma").classList.remove("d-none")
			setTimeout(() => {
				document.getElementById("obrigatorioSenhaConfirma").classList.add("d-none")
			}, 4000);
			errors++;
		}
		if (nome.length == 0) {
			document.getElementById("obrigatorioNomeRegister").classList.remove("d-none");
			setTimeout(() => {
				document.getElementById("obrigatorioNomeRegister").classList.add("d-none");
			},4000)
			errors++;
		}
		if (senha != confirmarSenha) {
			document.getElementById("senhasNaoCorrespondem").classList.remove("d-none");
			setTimeout(() => {
				document.getElementById("senhasNaoCorrespondem").classList.add("d-none");
            },4000)
        }
		return errors;
	}

	efetuarRegistro() {
		var dados = new FormData(document.getElementById("register-form"));
		var nome = dados.get("nomeRegister");
		var email = dados.get("emailRegister");
		var senha = dados.get("senhaRegister");
		var confirmarSenha = dados.get("confirmar-senhaRegister");
		var dadosFinal = {
			nome,
			email,
			senha,
			confirmarSenha
        }
		var erros = this.validarCamposRegister(email, senha, confirmarSenha, nome);
		if (erros == 0) {
			$.ajax({
				url: '/Login/Registrar',
				method: "post",
				contentType: false,
				processData: false,
				data: dadosFinal,
				cache: false,
				async: true,
				beforeSend: function () {
					$("#registrando").html(`<b class='text-success'><i class="fas fa-spinner fa-spin"></i> Efetuando Registro...</b>`)
					$("#registrando").removeClass("d-none");
				},
				complete: function () {
					$("#registrando").addClass("d-none");
				},
				success: function (ret) {
					Swal.fire({
						icon: ret.icon,
						title: ret.message,
						showConfirmButton: false,
						timer: 1500,
						width: 800
					});
					if (ret.icon == "success") {
						$("#nomeRegister").val("");
						$("#emailRegister").val("");
						$("#senhaRegister").val("");
						$("#confirmar-senhaRegister").val("");
                    }
				},
				error: function (ret) {

				}
			})
        }
    }

	efetuarLogin() {
		let dados = new FormData(document.getElementById("login-form"));
		let email = dados.get("email");
		let senha = dados.get("senha");
		let erros = this.validarCamposLogin(email, senha);
		if (erros == 0) {
			$.ajax({
				url: '/Login/Logar',
				method: "post",
				contentType: false,
				processData: false,
				data: dados,
				cache: false,
				async: true,
				beforeSend: function () {
					$("#logando").html(`<b class='text-success'><i class="fas fa-spinner fa-spin"></i> Logando...</b>`)
					$("#logando").removeClass("d-none");
				},
				complete: function () {
					$("#logando").addClass("d-none");
				},
				success: function (ret) {
					Swal.fire({
						icon: ret.icon,
						title: ret.message,
						showConfirmButton: false,
						timer: 1500
					});
					if (ret.icon == "success") {
						setTimeout(() => {
							window.location.href = "/Debito";
						},2000)
                    }
				},
				error: function (ret) {

				}
			})
        }
    }
}

_login = new Login();

$(function () {

	$('#login-form-link').click(function (e) {
		$("#login-form").delay(100).fadeIn(100);
		$("#register-form").fadeOut(100);
		$('#register-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});
	$('#register-form-link').click(function (e) {
		$("#register-form").delay(100).fadeIn(100);
		$("#login-form").fadeOut(100);
		$('#login-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});

});

document.addEventListener('keypress', (event) => {
	if (event.keyCode == 13) {
		$("#login-form-link").hasClass("active")
			_login.efetuarLogin();
		if ($("#register-form-link").hasClass("active"))
			_login.efetuarRegistro();
    }
});
