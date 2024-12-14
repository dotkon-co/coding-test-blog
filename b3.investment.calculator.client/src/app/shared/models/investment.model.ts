export interface InvestmentRequest {
  monetary: number;
  period: number;
}

export interface InvestmentResponse {
  gross: number;
  net: number;
  messages: string[];
}
