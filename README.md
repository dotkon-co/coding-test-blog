# B3 Calculadora de Juros Compostos

## Descrição
Este projeto é uma aplicação web para calcular o investimento com base em juros compostos. A aplicação permite que o usuário informe um valor monetário positivo e um prazo em meses (maior que 1) para resgate da aplicação. Após solicitar o cálculo do investimento, a aplicação exibe o resultado bruto e o resultado líquido.

## Estrutura do Projeto
Para tornar o código modular e extensível, o projeto implementa diversos padrões de design e boas práticas, incluindo:

- **Factory Method**: Seguindo o padrão GoF (Gang of Four), utilizamos o Factory Method para o cálculo de impostos. Esse padrão facilita a separação dos cálculos de impostos em classes distintas, tornando o código modular e mais fácil de manter.
  
- **Dictionary para Cálculos de Impostos**: Em vez de utilizar estruturas condicionais (`if` ou `switch`) para decidir qual cálculo de imposto aplicar, utilizamos um `Dictionary` para configurar os diferentes tipos de cálculo de imposto. Essa abordagem permite a adição e manutenção de novos tipos de impostos de maneira simplificada e extensível.

- **FluentValidation para Validação de Entrada**: Utilizamos a biblioteca **FluentValidation** para validar os valores de entrada. Com essa biblioteca, as regras de validação de campos estão separadas das regras de negócios, facilitando a manutenção e a legibilidade do código.

## Princípios SOLID
Este projeto segue os princípios do **SOLID** para garantir uma arquitetura robusta e escalável:

1. **S - Princípio da Responsabilidade Única**: Cada classe ou módulo tem uma única responsabilidade, tornando o código mais organizado e menos suscetível a erros.
2. **O - Princípio do Aberto/Fechado**: O código é projetado para ser aberto para extensão, mas fechado para modificação. O uso de `Dictionary` e `Factory Method` permite que novos tipos de cálculo sejam adicionados sem modificar o código existente.
3. **L - Princípio de Substituição de Liskov**: Implementações de classes podem substituir suas classes base sem alterar o comportamento desejado.
4. **I - Princípio de Segregação de Interface**: As interfaces são projetadas para que os módulos dependam apenas dos métodos necessários.
5. **D - Princípio de Inversão de Dependência**: O projeto utiliza injeção de dependência para desacoplar classes e facilitar testes e manutenção.

## Tecnologias
- **Back-end**: C#
- **Front-end**: Angular

## Instalação
Para configurar e rodar o projeto localmente, siga as etapas abaixo:

1. **Baixar o Visual Studio**: [Visual Studio](https://visualstudio.microsoft.com/).
2. **Instalar o Angular CLI**: Execute o comando abaixo no terminal para instalar o Angular CLI.
    ```bash
    npm install -g @angular/cli
    ```
3. **Clonar o repositório**:
    ```bash
    git clone https://github.com/tarciziojunior/b3.investment.calculator
    ```
4. **Configurar o projeto**: Abra o projeto no Visual Studio e siga as instruções de configuração.

## Uso
Após configurar o ambiente, siga os passos abaixo para iniciar a aplicação e calcular o retorno de investimento:

1. **Abrir a solução**: No Visual Studio, abra o arquivo `b3.investment.calculator.sln`.
2. **Iniciar o servidor**: No Visual Studio, execute o projeto `b3.investment.calculator.Server` como http(Mudar no botão execução do projeto) . Isso iniciará uma API para o cálculo de juros compostos.
3. **Abrir o terminal do cliente**: No Visual Studio, clique com o botão direito no projeto `b3.investment.calculator.client` e selecione a opção "Abrir no terminal".
4. **Executar o cliente**: No terminal, execute o comando:
   ```bash
   ng serve
