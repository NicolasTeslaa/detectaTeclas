using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DetectorTeclas
{
    class Program
    {
        private static KeyboardHook? _hook;
        private static Dictionary<Keys, int> _keyPressCount = new Dictionary<Keys, int>();
        private static bool _running = true;
        private static string _logFilePath = "teclas_pressionadas.txt";

        [STAThread]
        static void Main()
        {
            // Inicializa o arquivo de log com cabeçalho
            string header = $"=== LOG DE TECLAS - Iniciado em {DateTime.Now:dd/MM/yyyy HH:mm:ss} ===\n\n";
            File.AppendAllText(_logFilePath, header);
            
            Console.WriteLine("=== DETECTOR DE TECLAS ===");
            Console.WriteLine($"Teclas sendo salvas em: {Path.GetFullPath(_logFilePath)}");
            Console.WriteLine("Pressione ESC para sair");
            Console.WriteLine("Pressione C para limpar o contador");
            Console.WriteLine("Pressione S para mostrar estatísticas");
            Console.WriteLine("-----------------------------------\n");

            _hook = new KeyboardHook();
            _hook.KeyPressed += OnKeyPressed;
            _hook.KeyReleased += OnKeyReleased;

            // Aplicação Windows Forms invisível para manter o hook ativo
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Thread separada para o console
            System.Threading.Thread consoleThread = new System.Threading.Thread(() =>
            {
                while (_running)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        _running = false;
                        // Salva mensagem final no arquivo
                        string footer = $"\n\n=== Log encerrado em {DateTime.Now:dd/MM/yyyy HH:mm:ss} ===\n";
                        File.AppendAllText(_logFilePath, footer);
                        Application.Exit();
                        break;
                    }
                    else if (key.Key == ConsoleKey.C)
                    {
                        _keyPressCount.Clear();
                        Console.Clear();
                        Console.WriteLine("=== DETECTOR DE TECLAS ===");
                        Console.WriteLine($"Teclas sendo salvas em: {Path.GetFullPath(_logFilePath)}");
                        Console.WriteLine("Pressione ESC para sair");
                        Console.WriteLine("Pressione C para limpar o contador");
                        Console.WriteLine("Pressione S para mostrar estatísticas");
                        Console.WriteLine("-----------------------------------\n");
                        Console.WriteLine("Contador limpo!\n");
                    }
                    else if (key.Key == ConsoleKey.S)
                    {
                        MostrarEstatisticas();
                    }
                }
            });
            consoleThread.IsBackground = true;
            consoleThread.Start();

            // Executa a aplicação Windows Forms (invisível)
            Application.Run();
        }

        private static void OnKeyPressed(object? sender, KeyPressedEventArgs e)
        {
            // Ignora teclas de sistema e modificadoras quando pressionadas sozinhas
            if (e.Key == Keys.LControlKey || e.Key == Keys.RControlKey ||
                e.Key == Keys.LShiftKey || e.Key == Keys.RShiftKey ||
                e.Key == Keys.LMenu || e.Key == Keys.RMenu ||
                e.Key == Keys.LWin || e.Key == Keys.RWin)
            {
                return;
            }

            // Atualiza contador
            if (!_keyPressCount.ContainsKey(e.Key))
            {
                _keyPressCount[e.Key] = 0;
            }
            _keyPressCount[e.Key]++;

            // Exibe a tecla pressionada
            string keyName = GetKeyName(e.Key);
            string logMessage = $"[{DateTime.Now:HH:mm:ss}] [PRESSIONADA] {keyName} (Total: {_keyPressCount[e.Key]})\n";
            
            Console.WriteLine($"[PRESSIONADA] {keyName} (Total: {_keyPressCount[e.Key]})");
            
            // Salva no arquivo
            try
            {
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar no arquivo: {ex.Message}");
            }
        }

        private static void OnKeyReleased(object? sender, KeyPressedEventArgs e)
        {
            // Ignora teclas de sistema
            if (e.Key == Keys.LControlKey || e.Key == Keys.RControlKey ||
                e.Key == Keys.LShiftKey || e.Key == Keys.RShiftKey ||
                e.Key == Keys.LMenu || e.Key == Keys.RMenu ||
                e.Key == Keys.LWin || e.Key == Keys.RWin)
            {
                return;
            }

            string keyName = GetKeyName(e.Key);
            string logMessage = $"[{DateTime.Now:HH:mm:ss}] [SOLTADA] {keyName}\n";
            
            Console.WriteLine($"[SOLTADA] {keyName}");
            
            // Salva no arquivo
            try
            {
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar no arquivo: {ex.Message}");
            }
        }

        private static string GetKeyName(Keys key)
        {
            // Converte códigos de teclas para nomes legíveis
            if (key >= Keys.A && key <= Keys.Z)
            {
                return key.ToString();
            }
            else if (key >= Keys.D0 && key <= Keys.D9)
            {
                return key.ToString().Substring(1); // Remove o "D" de "D0", "D1", etc.
            }
            else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                return $"NumPad{key.ToString().Substring(7)}";
            }
            else
            {
                switch (key)
                {
                    case Keys.Space: return "ESPAÇO";
                    case Keys.Enter: return "ENTER";
                    case Keys.Back: return "BACKSPACE";
                    case Keys.Tab: return "TAB";
                    case Keys.Escape: return "ESC";
                    case Keys.Delete: return "DELETE";
                    case Keys.Insert: return "INSERT";
                    case Keys.Home: return "HOME";
                    case Keys.End: return "END";
                    case Keys.PageUp: return "PAGE UP";
                    case Keys.PageDown: return "PAGE DOWN";
                    case Keys.Up: return "SETA CIMA";
                    case Keys.Down: return "SETA BAIXO";
                    case Keys.Left: return "SETA ESQUERDA";
                    case Keys.Right: return "SETA DIREITA";
                    case Keys.F1: return "F1";
                    case Keys.F2: return "F2";
                    case Keys.F3: return "F3";
                    case Keys.F4: return "F4";
                    case Keys.F5: return "F5";
                    case Keys.F6: return "F6";
                    case Keys.F7: return "F7";
                    case Keys.F8: return "F8";
                    case Keys.F9: return "F9";
                    case Keys.F10: return "F10";
                    case Keys.F11: return "F11";
                    case Keys.F12: return "F12";
                    case Keys.OemSemicolon: return ";";
                    case Keys.Oemcomma: return ",";
                    case Keys.OemPeriod: return ".";
                    case Keys.OemQuestion: return "?";
                    case Keys.Oemtilde: return "~";
                    case Keys.OemOpenBrackets: return "[";
                    case Keys.OemCloseBrackets: return "]";
                    case Keys.OemPipe: return "|";
                    case Keys.OemQuotes: return "'";
                    case Keys.OemBackslash: return "\\";
                    case Keys.OemMinus: return "-";
                    default: return key.ToString();
                }
            }
        }

        private static void MostrarEstatisticas()
        {
            Console.WriteLine("\n=== ESTATÍSTICAS DE TECLAS ===");
            if (_keyPressCount.Count == 0)
            {
                Console.WriteLine("Nenhuma tecla foi pressionada ainda.");
            }
            else
            {
                var sortedKeys = _keyPressCount.OrderByDescending(kvp => kvp.Value);
                foreach (var kvp in sortedKeys)
                {
                    string keyName = GetKeyName(kvp.Key);
                    Console.WriteLine($"{keyName}: {kvp.Value} vezes");
                }
            }
            Console.WriteLine("==============================\n");
        }
    }
}
