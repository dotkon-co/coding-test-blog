import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RotasPaginas } from './shared/enums/rotas-paginas.enum';
import { AutenticacaoGuard } from './services/guards/autenticacao.guard';

const routes: Routes = [
  {
    path: RotasPaginas.Root,
    canActivate: [AutenticacaoGuard],
    loadChildren: () => import('./modules/blog/blog.module').then(m => m.BlogModule)
  },
  {
    path: RotasPaginas.Autenticacao,
    loadChildren: () => import('./modules/autenticacao/autenticacao.module').then(m => m.AutenticacaoModule)
  },
  {
    path: '**',
    redirectTo: RotasPaginas.Root
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
