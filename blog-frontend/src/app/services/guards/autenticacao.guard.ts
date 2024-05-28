import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UsuarioService } from '../usuario.service';
import { RotasPaginas } from 'src/app/shared/enums/rotas-paginas.enum';

@Injectable({
    providedIn: 'root'
})
export class AutenticacaoGuard implements CanActivate {
    constructor(
        private usuarioService: UsuarioService,
        private router: Router) { }

    canActivate() {
        if (this.usuarioService.usuarioLogado)
            return true;

        this.router.navigate([RotasPaginas.Autenticacao]);
        return false;
    }
}
