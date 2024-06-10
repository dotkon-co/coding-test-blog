import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IPostagemViewModel } from 'src/app/interfaces/queries/postagem-queries/IPostagemViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogHubService {
  private apiUrl = `${environment.urlBase}`
  private hubConnection: HubConnection | undefined;

  private postagemAdicionadaSubject = new Subject<IPostagemViewModel>();
  private postagemAlteradaSubject = new Subject<IPostagemViewModel>();
  private postagemExcluidaSubject = new Subject<string>();

  postagemAdicionada$ = this.postagemAdicionadaSubject.asObservable();
  postagemAlterada$ = this.postagemAlteradaSubject.asObservable();
  postagemExcluida$ = this.postagemExcluidaSubject.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/blog-hub`)
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('PostagemAdicionadaEventHub', (eventHub: IPostagemViewModel) => {
      this.postagemAdicionadaSubject.next(eventHub);
    });

    this.hubConnection.on('PostagemAlteradaEventHub', (eventHub: IPostagemViewModel) => {
      this.postagemAlteradaSubject.next(eventHub);
    });

    this.hubConnection.on('PostagemExcluidaEventHub', (eventHub: { postId: string }) => {
      this.postagemExcluidaSubject.next(eventHub.postId);
    });

    this.hubConnection.start().catch(err => console.error('Ocorreu um erro ao tentar se conectar no SignalR: ' + err));
  }

}
