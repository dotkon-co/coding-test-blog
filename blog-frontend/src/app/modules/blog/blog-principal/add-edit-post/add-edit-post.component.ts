import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ICommandResult } from 'src/app/interfaces/commands/ICommandResult';
import { IAlterarPostagemCommand } from 'src/app/interfaces/commands/postagem-commands/IAlterarPostagemCommand';
import { IPostarConteudoCommand } from 'src/app/interfaces/commands/postagem-commands/IPostarConteudoCommand';
import { IPostagemViewModel } from 'src/app/interfaces/queries/postagem-queries/IPostagemViewModel';
import { PostagemService } from 'src/app/services/postagem.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BaseComponent } from 'src/app/shared/components/base.component';

@Component({
  selector: 'app-add-edit-post',
  templateUrl: './add-edit-post.component.html',
  styleUrls: ['./add-edit-post.component.scss']
})
export class AddEditPostComponent extends BaseComponent implements OnInit {
  exibirProgressBar: boolean = false;
  postForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<AddEditPostComponent>,
    private snackbarService: SnackbarService,
    private postagemService: PostagemService,
    @Inject(MAT_DIALOG_DATA) public data: IPostagemViewModel
  ) {
    super();
  }

  ngOnInit(): void {
    this.carregarFormulario();
  }

  carregarFormulario() {
    this.postForm = this.formBuilder.group({
      postId: [this.data?.postId || ''],
      titulo: [this.data?.titulo || '', [Validators.required]],
      conteudo: [this.data?.conteudo || '', [Validators.required]]
    });
  }

  postarAlterarConteudo(): void {
    if (this.postForm.invalid) return;
    this.exibirProgressBar = true;

    const postId = this.postForm.get('postId')?.value;

    postId
      ? this.alterarConteudo()
      : this.postarConteudo();
  }

  private alterarConteudo() {
    const alterarPostagemCommand = this.postForm.getRawValue() as IAlterarPostagemCommand;

    this.addSubscription(
      this.postagemService.alterarPostagem(alterarPostagemCommand).subscribe({
        next: (resposta: ICommandResult) => { this.handlePostarConteudo(resposta); },
        error: (resposta) => { this.handleErrorPostarConteudo(resposta); },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  private postarConteudo() {
    const postarConteudoCommand = this.postForm.getRawValue() as IPostarConteudoCommand;

    this.addSubscription(
      this.postagemService.postarConteudo(postarConteudoCommand).subscribe({
        next: (resposta: ICommandResult) => { this.handlePostarConteudo(resposta); },
        error: (resposta) => { this.handleErrorPostarConteudo(resposta); },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  handlePostarConteudo(resposta: ICommandResult) {
    this.exibirProgressBar = false;

    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro postar conteúdo.");
      return;
    }

    this.fecharModal(resposta);
  }

  handleErrorPostarConteudo(error: HttpErrorResponse) {
    this.exibirProgressBar = false;

    const resposta = error.error as ICommandResult;
    if (!resposta?.mensagem) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro ao postar conteúdo.");
      this.fecharModal();
      return;
    }

    this.snackbarService.abrirMensagemErro(resposta.mensagem);
    this.fecharModal();
  }

  fecharModal(dialogResult?: ICommandResult): void {
    this.dialogRef.close(dialogResult);
  }
}
