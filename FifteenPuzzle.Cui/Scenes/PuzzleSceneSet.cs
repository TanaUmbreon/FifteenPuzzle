using System;
using FifteenPuzzle.Core;
using FifteenPuzzle.Cui.Core;
using FifteenPuzzle.Cui.Core.Messages;

namespace FifteenPuzzle.Cui.Scenes
{
    /// <summary>
    /// 15 パズルで遊ぶシーン セットです。
    /// </summary>
    public class PuzzleSceneSet : SceneSet
    {
        /// <summary>入力デバイス</summary>
        private VirtualInput input;
        /// <summary>15 パズル本体</summary>
        private PuzzleBody puzzle;

        /// <summary>
        /// <see cref="PuzzleSceneSet"/> の新しいインスタンスを生成します。
        /// </summary>
        public PuzzleSceneSet()
        {
            input = new VirtualInput();
            puzzle = new PuzzleBody();

            NextScene = Startup;
        }

        private void Startup(ISceneContext context)
        {
            puzzle.Shuffle();

            NextScene = Main;
        }

        private void Main(ISceneContext context)
        {
            Console.Clear();
            Console.Write(puzzle.ToString());
            Console.WriteLine();
            Console.WriteLine("方向キー: 空白パネルの方向にパネルをスライド");
            Console.WriteLine("Esc     : メニューに戻る");

            // シャッフルした結果、偶然完成形となってゲーム終了となる場合もあり得るが、
            // 対策はしてない。仕様です(
            if (puzzle.HasCompleted)
            {
                NextScene = Completed;
                return;
            }

            switch (input.GetKey())
            {
                case VirtualKey.Left:
                    puzzle.SlideLeft();
                    break;
                case VirtualKey.Up:
                    puzzle.SlideUp();
                    break;
                case VirtualKey.Right:
                    puzzle.SlideRight();
                    break;
                case VirtualKey.Down:
                    puzzle.SlideDown();
                    break;
                case VirtualKey.Quit:
                    context.NextSceneSet = new MenuSceneSet();
                    break;
            }
        }

        private void Completed(ISceneContext context)
        {
            Console.Clear();
            Console.Write(puzzle.ToString());
            Console.WriteLine();
            Console.WriteLine("パズル完成！　おめでとう！");
            Console.WriteLine("Esc: メニューに戻る");

            switch (input.GetKey())
            {
                case VirtualKey.Quit:
                    context.NextSceneSet = new MenuSceneSet();
                    break;
            }
        }
    }
}
