import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IPostarConteudoCommand } from '../interfaces/commands/postagem-commands/IPostarConteudoCommand';
import { ICommandResult } from '../interfaces/commands/ICommandResult';
import { Observable } from 'rxjs';
import { IPostagemViewModel } from '../interfaces/queries/postagem-queries/IPostagemViewModel';
import { IAlterarPostagemCommand } from '../interfaces/commands/postagem-commands/IAlterarPostagemCommand';
import { IExcluirPostagemCommand } from '../interfaces/commands/postagem-commands/IExcluirPostagemCommand';

@Injectable({
  providedIn: 'root'
})
export class PostagemService {
  private apiUrl = `${environment.urlBase}/postagem`;

  constructor(private httpClient: HttpClient) { }

  postarConteudo(command: IPostarConteudoCommand): Observable<ICommandResult> {
    return this.httpClient.post<ICommandResult>(`${this.apiUrl}/postar`, command);
  }

  alterarPostagem(command: IAlterarPostagemCommand): Observable<ICommandResult> {
    return this.httpClient.put<ICommandResult>(`${this.apiUrl}/alterar`, command);
  }

  obterPostagens(): Observable<IPostagemViewModel[]> {
    return this.httpClient.get<IPostagemViewModel[]>(`${this.apiUrl}/todas`);
  }

  excluirPostagem(command: IExcluirPostagemCommand): Observable<ICommandResult> {
    return this.httpClient.delete<ICommandResult>(`${this.apiUrl}/excluir`, { body: command });
  }
}
