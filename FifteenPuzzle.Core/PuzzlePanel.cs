using System;

namespace FifteenPuzzle.Core
{
    /// <summary>
    /// 15 パズルのパネルです。
    /// </summary>
    public class PuzzlePanel
    {
        #region テキストリソース

        private const string InvalidNumberMessage = "パネルの番号をゼロ以下にすることはできません。";
        private const string TextFormat = "[{0,2}]";
        private const string FreePanelText = "    ";
        private const int FreePanelNumber = 0;

        #endregion

        #region 固有のインスタンス

        /// <summary>
        /// 空白パネルを取得します。
        /// </summary>
        public static PuzzlePanel Free { get; } = new PuzzlePanel();

        /// <summary>
        /// <see cref="PuzzlePanel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="number"></param>
        private PuzzlePanel()
        {
            Number = FreePanelNumber;
            text = FreePanelText;
        }

        #endregion

        /// <summary>パネルの番号を表す文字列</summary>
        private readonly string text;

        /// <summary>パネルの番号を取得します。</summary>
        public int Number { get; }

        /// <summary>
        /// <see cref="PuzzlePanel"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="number"></param>
        public PuzzlePanel(int number)
        {
            if (number <= 0) { throw new ArgumentException(InvalidNumberMessage, nameof(number)); }

            Number = number;
            text = string.Format(TextFormat, number);
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
