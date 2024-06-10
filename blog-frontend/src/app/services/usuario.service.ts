import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRegistrarUsuarioCommand } from '../interfaces/commands/usuario-commands/IRegistrarUsuarioCommand';
import { ICommandResult } from '../interfaces/commands/ICommandResult';
import { IAutenticarUsuarioCommand } from '../interfaces/commands/usuario-commands/IAutenticarUsuarioCommand';
import { IUsuarioLogadoViewModel } from '../interfaces/queries/usuario-queries/IUsuarioLogadoViewModel';
import { JwtHelperService } from '@auth0/angular-jwt';
import { jwtDecode } from 'jwt-decode';
import { RotasPaginas } from '../shared/enums/rotas-paginas.enum';

const tokenConstante = "3c469e9d6c5875d37a43f353d4f88e6";
const usuarioConstante = "9250e222c4c71f0c58d4c54b50a880a31";
const usuarioPerfilConstante = "1361c05de0da40afbcdaf51b34567cbf1d";

const jwtHelper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private apiUrl = `${environment.urlBase}/usuario`;

  constructor(
    private httpClient: HttpClient,
    private router: Router) { }

  criarUsuario(command: IRegistrarUsuarioCommand): Observable<ICommandResult> {
    return this.httpClient.post<ICommandResult>(`${this.apiUrl}/criar`, command);
  }

  autenticarUsuario(command: IAutenticarUsuarioCommand): Observable<ICommandResult> {
    return this.httpClient.post<ICommandResult>(`${this.apiUrl}/autenticar`, command).pipe(
      tap((resposta) => {
        if (!this.podeLogarUsuario(resposta)) return;

        this.salvarUsuarioStorage(resposta);
      }));
  }

  private podeLogarUsuario(resposta: ICommandResult) {
    return resposta.sucesso;
  }

  public salvarUsuarioStorage(resposta: ICommandResult) {
    localStorage.setItem(tokenConstante, btoa(JSON.stringify(resposta.dado['token'])));
    localStorage.setItem(usuarioConstante, btoa(JSON.stringify(resposta.dado['usuario'])));
    localStorage.setItem(usuarioPerfilConstante, btoa(JSON.stringify(resposta.dado['usuarioPerfil'])));
  }

  deslogarUsuario() {
    localStorage.clear();
    this.router.navigate([RotasPaginas.Autenticacao]);
  }

  get obterUsuarioLogado(): IUsuarioLogadoViewModel {
    return localStorage.getItem(usuarioConstante)
      ? JSON.parse(atob(`${localStorage.getItem(usuarioConstante)}`))
      : null;
  }

  get usuarioLogado(): boolean {
    let token = this.decodificarTokenJwt;
    let usuario = this.obterUsuarioLogado;

    if (token == null || usuario == null) return false;

    return token.unique_name == usuario.id ? true : false;
  }

  private get decodificarTokenJwt(): any {
    try {
      let token = this.obterTokenUsuario;
      return jwtDecode(token);
    } catch (error) {
      return null;
    }
  }

  get tokenExpirado(): boolean {
    let token = this.obterTokenUsuario;
    if (token == null) return true;

    return jwtHelper.isTokenExpired(token);
  }

  get obterTokenUsuario(): string {
    return localStorage.getItem(tokenConstante)
      ? JSON.parse(atob(`${localStorage.getItem(tokenConstante)}`))
      : null;
  }
}
