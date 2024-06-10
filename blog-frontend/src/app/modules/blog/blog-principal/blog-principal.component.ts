import { IExcluirPostagemCommand } from './../../../interfaces/commands/postagem-commands/IExcluirPostagemCommand';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UsuarioService } from 'src/app/services/usuario.service';
import { BaseComponent } from 'src/app/shared/components/base.component';
import { AddEditPostComponent } from './add-edit-post/add-edit-post.component';
import { ICommandResult } from 'src/app/interfaces/commands/ICommandResult';
import { IPostagemViewModel } from 'src/app/interfaces/queries/postagem-queries/IPostagemViewModel';
import { PostagemService } from 'src/app/services/postagem.service';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { BlogHubService } from 'src/app/services/hubs/blog-hub.service';

@Component({
  selector: 'app-blog-principal',
  templateUrl: './blog-principal.component.html',
  styleUrls: ['./blog-principal.component.scss']
})
export class BlogPrincipalComponent extends BaseComponent implements OnInit {
  exibirProgressBar: boolean = false;
  postagens: IPostagemViewModel[] = [];

  constructor(
    private dialog: MatDialog,
    private snackbarService: SnackbarService,
    private blogHubService: BlogHubService,
    private usuarioService: UsuarioService,
    private postagemService: PostagemService) {
    super();
  }

  ngOnInit(): void {
    this.obterPostagens();
    this.inscreverBlogHubEvents();
  }

  inscreverBlogHubEvents() {
    this.addSubscription(this.blogHubService.postagemAdicionada$.subscribe(postagem => {
      this.adicionarAlterarPostagemLista(postagem);
    }));

    this.addSubscription(this.blogHubService.postagemAlterada$.subscribe(postagem => {
      this.adicionarAlterarPostagemLista(postagem);
    }));

    this.addSubscription(this.blogHubService.postagemExcluida$.subscribe(postId => {
      this.removerPostagemLista(postId);
    }));
  }

  obterPostagens() {
    this.exibirProgressBar = true;

    this.addSubscription(
      this.postagemService.obterPostagens().subscribe({
        next: (postagens: IPostagemViewModel[]) => { this.handleObterPostagens(postagens); },
        error: (error: HttpErrorResponse) => { this.handleErrorObterPostagens(error); this.exibirProgressBar = false; },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  handleObterPostagens(postagens: IPostagemViewModel[]) {
    this.postagens = postagens;
  }

  handleErrorObterPostagens(error: HttpErrorResponse) {
    this.snackbarService.abrirMensagemErro(error?.message || "Ocorreu um erro ao buscar postagens.");
  }

  deslogarUsuario() {
    this.usuarioService.deslogarUsuario();
  }

  abrirModalAddEditPost(postagem?: IPostagemViewModel) {
    const dialogRef = this.dialog.open(AddEditPostComponent, {
      disableClose: true,
      data: postagem
    });

    dialogRef.afterClosed().subscribe((resultado: ICommandResult) => {
      if (resultado && resultado.sucesso) {
        this.adicionarAlterarPostagemLista(resultado.dado as IPostagemViewModel);
      }
    });
  }

  podeEditarExcluir(autorId: string): boolean {
    return autorId === this.usuarioService.obterUsuarioLogado.id;
  }

  adicionarAlterarPostagemLista(postagem: IPostagemViewModel) {
    const index = this.postagens.findIndex(p => p.postId === postagem.postId);

    if (index !== -1) {
      this.postagens[index] = postagem;
    } else {
      this.postagens.push(postagem);
    }
  }

  editarPostagem(postagem: IPostagemViewModel) {
    this.abrirModalAddEditPost(postagem);
  }

  excluirPostagem(postId: string) {
    this.exibirProgressBar = true;

    const excluirPostagemCommand: IExcluirPostagemCommand = { postId };
    this.addSubscription(
      this.postagemService.excluirPostagem(excluirPostagemCommand).subscribe({
        next: (resposta: ICommandResult) => { this.handleExcluirPostagem(resposta, postId); },
        error: (error: HttpErrorResponse) => { this.handleErrorExcluirPostagem(error); },
        complete: () => { this.exibirProgressBar = false; }
      })
    );
  }

  handleExcluirPostagem(resposta: ICommandResult, postId: string) {
    this.exibirProgressBar = false;

    if (!resposta.sucesso) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro excluir postagem.");
      return;
    }

    this.removerPostagemLista(postId);
    this.snackbarService.abrirMensagemSucesso(resposta.mensagem);
  }

  removerPostagemLista(postId: string) {
    this.postagens = this.postagens.filter(post => post.postId !== postId);
  }

  handleErrorExcluirPostagem(error: HttpErrorResponse) {
    this.exibirProgressBar = false;

    const resposta = error.error as ICommandResult;
    if (!resposta?.mensagem) {
      this.snackbarService.abrirMensagemErro("Ocorreu um erro excluir postagem.");
      return;
    }

    this.snackbarService.abrirMensagemErro(resposta.mensagem);
  }

}
