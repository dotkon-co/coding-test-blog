import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './features/landing/landing.component';
import { CalculatorComponent } from './features/calculator/calculator.component';

const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'calculator-form', component: CalculatorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
