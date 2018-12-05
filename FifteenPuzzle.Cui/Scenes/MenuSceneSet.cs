using System;
using System.Text;
using FifteenPuzzle.Cui.Core;
using FifteenPuzzle.Cui.Core.Messages;
using Util.Common;

namespace FifteenPuzzle.Cui.Scenes
{
    /// <summary>
    /// メニューのシーン セットです。
    /// </summary>
    public class MenuSceneSet : SceneSet
    {
        private VirtualInput input;

        /// <summary>
        /// <see cref="MenuSceneSet"/> の新しいインスタンスを生成します。
        /// </summary>
        public MenuSceneSet()
        {
            input = new VirtualInput();
            NextScene = ShowTitleMenu;
        }

        private void ShowTitleMenu(ISceneContext context)
        {
            var text = new StringBuilder();
            text.AppendLine("------------------------------------------------");
            text.AppendLine("              適当に作った15パズル              ");
            text.AppendLine("------------------------------------------------");
            text.AppendLine("【ルール】");
            text.AppendLine("4×4マスの盤面にある番号パネルをスライドさせて、");
            text.AppendLine();
            text.AppendLine("  [ 1][ 2][ 3][ 4]");
            text.AppendLine("  [ 5][ 6][ 7][ 8]");
            text.AppendLine("  ...");
            text.AppendLine();
            text.AppendLine("と右上から順番に並び替えたら完成です。");
            text.AppendLine();
            text.AppendLine("Enter: 15パズルで遊ぶ");
            text.AppendLine("Esc  : アプリ終了");
            text.AppendLine();
            text.AppendLine("------------------------------------------------");
            text.AppendLine(AssemblyInfo.Copyright.Replace("©", "(C)"));
            text.AppendLine();

            Console.Clear();
            Console.Write(text.ToString());

            switch (input.GetKey())
            {
                case VirtualKey.Commit:
                    context.NextSceneSet = new PuzzleSceneSet();
                    break;
                case VirtualKey.Quit:
                    context.Quit();
                    break;
            }
        }
    }
}
