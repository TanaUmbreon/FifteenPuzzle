using System;

namespace FifteenPuzzle.Cui.Puzzle
{
    /// <summary>
    /// 15 パズルのパネルです。
    /// </summary>
    public class PuzzlePanel
    {
        private const string InvalidNumberMessage = "パネルの番号をゼロ以下にすることはできません。";

        /// <summary>パネルの番号</summary>
        private readonly int number;
        /// <summary>パネルの番号を表す文字列</summary>
        private readonly string text;

        /// <summary>
        /// 空白パネルを取得します。
        /// </summary>
        public static PuzzlePanel Free { get; } = new PuzzlePanel();

        /// <summary>
        /// <see cref="PuzzlePanel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="number"></param>
        public PuzzlePanel(int number)
        {
            if (number <= 0) { throw new ArgumentException(InvalidNumberMessage, nameof(number)); }

            this.number = number;
            text = $"[{number,2}]";
        }

        /// <summary>
        /// <see cref="PuzzlePanel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="number"></param>
        private PuzzlePanel()
        {
            number = 0;
            text = "[  ]";
        }

        /// <summary>
        /// パネルの番号を表す文字列を返します。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return text;
        }
    }
}
