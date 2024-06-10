import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ICommandResult } from 'src/app/interfaces/commands/ICommandResult';
import { IRegistrarUsuarioCommand } from 'src/app/interfaces/commands/usuario-commands/IRegistrarUsuarioCommand';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { BaseComponent } from 'src/app/shared/components/base.component';
import { RotasPaginas } from 'src/app/shared/enums/rotas-paginas.enum';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.scss']
})
export class RegistrarComponent extends BaseComponent implements OnInit {
  exibirProgressBar: boolean = false;
  registrarForm!: FormGroup;

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
    this.registrarForm = this.formBuilder.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  registrarUsuario() {
    if (this.registrarForm.invalid) return;

    const registrarUsuarioCommand = this.registrarForm.getRawValue() as IRegistrarUsuarioCommand;

    this.exibirProgressBar = true;

    this.addSubscription(
      this.usuarioService.criarUsuario(registrarUsuarioCommand).subscribe({
        next: (resposta: ICommandResult) => { this.handleCriarUsuario(resposta); },
        error: (resposta) => { this.handleErrorCriarUsuario(resposta); },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  handleCriarUsuario(resposta: ICommandResult) {
    this.exibirProgressBar = false;

    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemAtencao(resposta.mensagem);
      return;
    }

    this.snackbarService.abrirMensagemSucesso(resposta.mensagem);
    this.voltarParaLogin();
  }

  handleErrorCriarUsuario(error: HttpErrorResponse) {
    this.exibirProgressBar = false;

    const resposta = error.error as ICommandResult;
    if (!resposta) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro ao criar usuário.");
      return;
    }

    this.snackbarService.abrirMensagemErro(resposta.mensagem);
  }

  voltarParaLogin() {
    this.router.navigate([`/${RotasPaginas.Autenticacao}`]);
  }

}
