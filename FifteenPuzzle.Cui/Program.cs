using System;
using FifteenPuzzle.Cui.Core;
using FifteenPuzzle.Cui.Scenes;
using Util.Common;
using Util.ConsoleApp;

namespace FifteenPuzzle.Cui
{
    /// <summary>
    /// アプリケーションのエントリ ポイントを提供します。
    /// </summary>
    public class Program
    {
        private const string OperationCanceledMessage = "\n操作はキャンセルされました。";
        private const string ErrorMessage = "\n{0} は動作を停止しました。\n{1}";
        private const string ErrorMessageForDebug = "\n{0}\n";
        private const int QuitTimeoutMilliseconds = 15000;

        /// <summary>
        /// アプリケーションのエントリ ポイントです。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        public static void Main(string[] args)
        {
            try
            {
                var loop = new MainLoop()
                {
                    NextSceneSet = new MenuSceneSet(),
                };
                loop.Run();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                ConsoleEx.Timeout(QuitTimeoutMilliseconds);
            }
        }

        private static void ShowErrorMessage(Exception ex)
        {
            var before = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format(ErrorMessage, AssemblyInfo.Title, ex.Message));

            Console.ForegroundColor = before;
            Console.WriteLine(string.Format(ErrorMessageForDebug, ex.StackTrace));
        }
    }
}
