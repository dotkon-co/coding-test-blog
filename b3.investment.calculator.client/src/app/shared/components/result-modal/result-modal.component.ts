import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InvestmentResponse } from '../../models/investment.model';


@Component({
  selector: 'app-result-modal',
  templateUrl: './result-modal.component.html',
  styleUrls: ['./result-modal.component.css'],
})
export class ResultModalComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public data: InvestmentResponse) {}

  ngOnInit() {}
}
