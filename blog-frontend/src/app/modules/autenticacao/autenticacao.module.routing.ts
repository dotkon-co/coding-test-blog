import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AutenticacaoComponent } from "./autenticacao/autenticacao.component";
import { RotasPaginas } from "src/app/shared/enums/rotas-paginas.enum";
import { RegistrarComponent } from "./registrar/registrar.component";

const routes: Routes = [
  { path: RotasPaginas.Root, component: AutenticacaoComponent },
  { path: RotasPaginas.Registro, component: RegistrarComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AutenticacaoRoutingModule { }
