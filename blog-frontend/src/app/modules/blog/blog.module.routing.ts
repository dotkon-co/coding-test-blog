import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { RotasPaginas } from "src/app/shared/enums/rotas-paginas.enum";
import { BlogPrincipalComponent } from "./blog-principal/blog-principal.component";

const routes: Routes = [
  { path: RotasPaginas.Root, component: BlogPrincipalComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
