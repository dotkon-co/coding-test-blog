import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { InvestmentService } from '../../core/services/investment.service';
import { InvestmentRequest, InvestmentResponse } from '../../shared/models/investment.model';
import { ResultModalComponent } from '../../shared/components/result-modal/result-modal.component';


@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css'],
})
export class CalculatorComponent implements OnInit {
  ngOnInit() {}

  calcForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private investmentService: InvestmentService,
    private dialog: MatDialog
  ) {
    this.calcForm = this.fb.group({
      monetary: ['', [
        Validators.required
      ]],
      period: ['', [
        Validators.required,
        Validators.pattern('^[0-9]*$') // Permitir apenas números inteiros        
      ]],
    });
  }

  onSubmit() {
    let investmentRequest: InvestmentRequest = this.calcForm.getRawValue();
    investmentRequest = {
      ...investmentRequest,
      monetary: investmentRequest.monetary ?? 0,
      period: investmentRequest.period ?? 1,
      
    };
    this.investmentService
      .calculateInvestment(investmentRequest)
      .subscribe((response: InvestmentResponse) => {
        this.dialog.open(ResultModalComponent, {
          data: response,
        });
      },
        (response) => {
          // Tratamento para erro 400 ou outros erros
          if (response.status === 400) {
            // Exemplo de mensagem de erro específica para erro 400
            this.dialog.open(ResultModalComponent, {
              data: { messages: response.error.errors},
            });
          } else {
            // Tratamento para outros tipos de erros, se necessário
            this.dialog.open(ResultModalComponent, {
              data: { messages: ['Ocorreu um erro inesperado.Tente novamente mais tarde.']  },
            });
          }
        }
      );
  }
}
