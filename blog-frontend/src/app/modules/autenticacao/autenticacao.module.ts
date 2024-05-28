import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AutenticacaoComponent } from './autenticacao/autenticacao.component';
import { AutenticacaoRoutingModule } from './autenticacao.module.routing';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistrarComponent } from './registrar/registrar.component';

@NgModule({
  imports: [
    CommonModule,
    AutenticacaoRoutingModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    AutenticacaoComponent,
    RegistrarComponent,
  ]
})
export class AutenticacaoModule { }
