# Coding Test Dotkon | Projeto de Blog Simples

## Descrição

Agradecemos por estar participando do nosso processo seletivo! Estamos muito felizes em ter você por aqui 😃.
A primeira etapa do processo seletivo é um teste técnico, que está descrito abaixo. Boa sorte! \o/

Este projeto consiste na implementação de um sistema básico de blog onde os usuários podem visualizar, criar, editar e excluir postagens. O objetivo é avaliar as habilidades técnicas em C# e o uso do Entity Framework para manipulação de dados.

## Requisitos para a entrega
    1. Faça um fork deste repositório;
    2. Realize o teste;
    3. Adicione seu currículo em PDF na raiz do repositório;
    4. Envie-nos o PULL-REQUEST para que seja avaliado.
    
    OBS: seus dados não ficarão públicos em nosso repositório.
  
## Funcionalidades

### Requisitos Funcionais

1. **Autenticação**: 
    - Usuários devem ser capazes de se registrar e fazer login.

2. **Gerenciamento de Postagens**: 
    - Usuários autenticados podem criar postagens, editar suas próprias postagens e excluir postagens existentes.

3. **Visualização de Postagens**: 
    - Qualquer visitante do site pode visualizar as postagens existentes.

### Requisitos Técnicos

- **.NET**: Utilize a versão 6 ou 8
- **Entity Framework**: Utilize o Entity Framework para interagir com o banco de dados e armazenar informações sobre usuários e postagens.

### Requisitos Opcionais

- **Arquitetura Monolítica**: Organize as responsabilidades do sistema, como autenticação, gerenciamento de postagens e notificações em tempo real.

- **Princípios SOLID**: Aplique os princípios SOLID, com ênfase no Princípio da Responsabilidade Única (SRP) e no Princípio da Inversão de Dependência (DIP).

- **WebSockets**: Implemente WebSockets para notificações em tempo real, como uma notificação simples na interface do usuário sempre que uma nova postagem for feita.

- **Interface Web Simples**: Crie uma interface web simples para a interação com o sistema.

## Observações Finais
Certifique-se de que seu código está bem documentado e limpo.
Inclua qualquer documentação adicional que possa ajudar a entender sua solução (README.md).

---

Este teste prático é uma oportunidade para demonstrar suas habilidades em desenvolvimento C#, arquitetura de software e boas práticas de programação. 
Divirta-se no processo!

##Observação do Desenvolvedor (André Albuquerque)
- Foi criado um Application Web ASP.NET Core na versão 8
- A forma de armazenamento dos dados foi escolhido EntityFrameworkCore In-Memory.
- Salvando os dados em memória, podendo incluir varias postagens para cada login efetuado.
- Acredito que fiz a forma mais simples, e que na minha visão atentendeu os requisitos do teste.
    