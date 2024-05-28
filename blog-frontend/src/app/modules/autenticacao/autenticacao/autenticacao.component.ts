import { BaseComponent } from 'src/app/shared/components/base.component';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RotasPaginas } from 'src/app/shared/enums/rotas-paginas.enum';
import { IAutenticarUsuarioCommand } from 'src/app/interfaces/commands/usuario-commands/IAutenticarUsuarioCommand';
import { UsuarioService } from 'src/app/services/usuario.service';
import { ICommandResult } from 'src/app/interfaces/commands/ICommandResult';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-autenticacao',
  templateUrl: './autenticacao.component.html',
  styleUrls: ['./autenticacao.component.scss']
})
export class AutenticacaoComponent extends BaseComponent implements OnInit {
  exibirProgressBar: boolean = false;
  loginForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private snackbarService: SnackbarService,
    private usuarioService: UsuarioService) {
    super();
  }

  ngOnInit(): void {
    this.carregarFormulario();
  }

  carregarFormulario() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  autenticarUsuario() {
    if (this.loginForm.invalid) return;

    const autenticarUsuarioCommand = this.loginForm.getRawValue() as IAutenticarUsuarioCommand;

    this.exibirProgressBar = true;

    this.addSubscription(
      this.usuarioService.autenticarUsuario(autenticarUsuarioCommand).subscribe({
        next: (resposta: ICommandResult) => { this.handleAutenticarUsuario(resposta); },
        error: (resposta) => { this.handleErrorAutenticarUsuario(resposta); },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  handleAutenticarUsuario(resposta: ICommandResult) {
    this.exibirProgressBar = false;

    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro ao autenticar usuário.");
      return;
    }

    this.router.navigate([RotasPaginas.Root]);
  }

  handleErrorAutenticarUsuario(error: HttpErrorResponse) {
    this.exibirProgressBar = false;

    const resposta = error.error as ICommandResult;
    if (!resposta.mensagem) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro ao autenticar usuário.");
      return;
    }

    this.snackbarService.abrirMensagemErro(resposta.mensagem);
  }

  irParaRotaRegistro() {
    this.router.navigate([`${RotasPaginas.Autenticacao}/${RotasPaginas.Registro}`]);
  }

}
