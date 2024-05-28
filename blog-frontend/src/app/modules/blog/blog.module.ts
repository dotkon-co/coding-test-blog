import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BlogPrincipalComponent } from './blog-principal/blog-principal.component';
import { BlogRoutingModule } from './blog.module.routing';
import { AddEditPostComponent } from './blog-principal/add-edit-post/add-edit-post.component';

@NgModule({
  declarations: [
    BlogPrincipalComponent,
    AddEditPostComponent
  ],
  imports: [
    MaterialModule,
    CommonModule,
    BlogRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class BlogModule { }
