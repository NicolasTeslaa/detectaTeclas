# Detector de Teclas

Sistema em C# que detecta e monitora todas as teclas pressionadas no sistema Windows, mesmo quando a aplicação não está em foco.

## Funcionalidades

- ✅ Detecta todas as teclas pressionadas globalmente (hook de baixo nível)
- ✅ Mostra quando uma tecla é pressionada e quando é soltada
- ✅ Conta quantas vezes cada tecla foi pressionada
- ✅ Exibe estatísticas de uso das teclas
- ✅ **Salva todas as teclas pressionadas em arquivo TXT automaticamente**
- ✅ Interface de console simples e intuitiva
- ✅ Pode ser compilado como executável standalone (.exe)

## Requisitos

- Windows (sistema operacional)
- .NET 6.0 ou superior
- Visual Studio 2022 ou VS Code com extensão C#

## Como Compilar e Executar

### Opção 1: Usando Visual Studio
1. Abra o arquivo `DetectorTeclas.csproj` no Visual Studio
2. Pressione F5 para compilar e executar

### Opção 2: Usando linha de comando
```bash
cd DetectorTeclas
dotnet build
dotnet run
```

### Opção 3: Gerar Executável Standalone (.exe)

Para gerar um executável independente que não precisa do .NET instalado:

```bash
dotnet publish -c Release
```

O executável será gerado em: `bin/Release/net6.0-windows/win-x64/publish/DetectorTeclas.exe`

**Vantagens do executável:**
- ✅ Arquivo único (publish single file)
- ✅ Não precisa do .NET instalado no computador destino
- ✅ Pode ser executado diretamente clicando duas vezes

## Como Usar

1. Execute o programa (ou o executável .exe)
2. O sistema começará a detectar todas as teclas pressionadas
3. As teclas serão exibidas no console em tempo real
4. **Todas as teclas são automaticamente salvas no arquivo `teclas_pressionadas.txt`**

O arquivo de log será criado na mesma pasta onde o programa está sendo executado, contendo:
- Data e hora de cada tecla pressionada/soltada
- Nome da tecla
- Contador total de cada tecla

### Comandos Disponíveis

- **ESC**: Encerra o programa
- **C**: Limpa o contador de teclas
- **S**: Mostra estatísticas de todas as teclas pressionadas (ordenadas por frequência)

## Exemplo de Saída

```
=== DETECTOR DE TECLAS ===
Pressione ESC para sair
Pressione C para limpar o contador
Pressione S para mostrar estatísticas
-----------------------------------

[PRESSIONADA] A (Total: 1)
[SOLTADA] A
[PRESSIONADA] ESPAÇO (Total: 1)
[SOLTADA] ESPAÇO
[PRESSIONADA] E (Total: 1)
[SOLTADA] E
```

## Aviso Legal

Este software é fornecido apenas para fins educacionais e de teste. Certifique-se de ter permissão antes de usar em qualquer sistema. O uso não autorizado de keyloggers pode violar leis de privacidade.

## Estrutura do Projeto

- `Program.cs`: Arquivo principal com a lógica da aplicação
- `KeyboardHook.cs`: Classe que implementa o hook de teclado usando Windows API
- `DetectorTeclas.csproj`: Arquivo de configuração do projeto
