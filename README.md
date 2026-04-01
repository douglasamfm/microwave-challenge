# Microwave Challenge

Projeto desenvolvido em ASP.NET Core MVC com o objetivo de simular o funcionamento de um micro-ondas digital,
conforme especificacoes de teste tecnico.

---

## Arquitetura

O projeto foi estruturado em camadas, seguindo boas praticas de separacao de responsabilidades:

- Domain -> regras de negocio
- Application -> servicos e orquestracao
- Web (MVC) -> interface com o usuario
- Tests -> testes unitarios

---

## Tecnologias utilizadas

- .NET 8
- ASP.NET Core MVC
- xUnit
- Bootstrap

---

## Funcionalidades

### Nivel 1
- Informar tempo e potencia
- Validacao de tempo (1 a 120 segundos)
- Validacao de potencia (1 a 10)
- Potencia padrao = 10 quando nao informada
- Inicio rapido (30s / potencia 10)
- Pausa e cancelamento
- Acrescimo de 30 segundos durante execucao
- Exibicao de progresso do aquecimento
- Mensagem final: "Aquecimento concluido"

---

### Nivel 2
- Programas de aquecimento pre-definidos:
  - Pipoca
  - Leite
  - Carnes de boi
  - Frango
  - Feijao
- Cada programa possui:
  - Tempo
  - Potencia
  - Caractere de aquecimento
  - Instrucoes
- Selecao automatica dos dados
- Nao permite edicao ao selecionar programa
- Nao permite acrescimo de tempo em programas pre-definidos

---

### Nivel 3
- Cadastro de programas customizados
- Validacao de caracteres:
  - Nao pode repetir
  - Nao pode ser `.`
- Instrucoes opcionais
- Armazenamento em memoria (sem persistencia)

---

## Testes unitarios

Foram implementados testes para:

- Tempo invalido
- Potencia invalida
- Inicio rapido
- Validacao de caractere duplicado
- Pausa e retomada
- Acrescimo de tempo

---

## Como executar

1. Abrir a solution no Visual Studio
2. Definir o projeto MicrowaveChallenge como inicial
3. Executar com F5 ou Ctrl + F5

---

## Observacoes tecnicas

- Os programas customizados sao mantidos em memoria para simplificacao do teste
- Alguns tempos do enunciado ultrapassam 120 segundos; foi adotado o limite maximo conforme regra do sistema
- Aplicados conceitos de orientacao a objetos e boas praticas (SOLID)

---

## Autor

Douglas Fernandes

https://coodesh.com/assessments/project/3b9a653c-0ed7-4fc7-8d13-2accd782639c/intro