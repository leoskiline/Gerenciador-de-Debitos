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

				},
				complete: function () {

				},
				success: function (ret) {
					Swal.fire({
						icon: ret.icon,
						title: ret.message,
						showConfirmButton: false,
						timer: 1500
					});
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
		_login.efetuarLogin();
    }
});
