import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { UsuarioService } from '../usuario.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private usuarioService: UsuarioService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.usuarioService.obterTokenUsuario;
    const usuarioLogado = this.usuarioService.usuarioLogado;

    const urlNaoNecessitaToken = this.verificarSeUrlNaoNecessitaToken(usuarioLogado);

    if (urlNaoNecessitaToken) {
      return next.handle(request);
    }

    request = this.addTokenHeader(request, token);

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.usuarioService.deslogarUsuario();
        }

        return throwError(error);
      })
    );
  }

  private verificarSeUrlNaoNecessitaToken(usuarioLogado: boolean) {
    return !usuarioLogado;
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    return request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        token: `${token}`
      }
    });
  }
}
