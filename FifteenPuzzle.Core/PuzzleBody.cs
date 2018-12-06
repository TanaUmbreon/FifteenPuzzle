using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FifteenPuzzle.Core
{
    /// <summary>
    /// 15 パズルの本体です。
    /// </summary>
    public class PuzzleBody
    {
        /// <summary>マス目の幅</summary>
        private const int SquareWidth = 4;
        /// <summary>マス目の高さ</summary>
        private const int SquareHeight = 4;

        /// <summary>パズルの盤面</summary>
        /// <remarks>
        /// SquareWidth * SquareHeight のサイズを持つ。
        /// 左上から右方向に 1 から始まる連番のパネルと、右下に空白パネルが格納される。
        /// </remarks>
        private readonly PuzzlePanel[,] board;
        /// <summary>完成形の盤面</summary>
        private readonly PuzzlePanel[,] completedBorad;

        /// <summary>
        /// 空白パネルの位置を取得または設定します。
        /// </summary>
        private Point FreePanelLocation { get; set; }

        /// <summary>
        /// パズルが完成したことを表す値を取得します。
        /// </summary>
        public bool HasCompleted { get; private set; }

        /// <summary>
        /// <see cref="PuzzleBody"/> の新しいインスタンスを生成します。
        /// </summary>
        public PuzzleBody()
        {
            if (SquareWidth <= 0) { throw new NotImplementedException("マス目の幅がゼロ以下に設定されています。"); }
            if (SquareHeight <= 0) { throw new NotImplementedException("マス目の高さがゼロ以下に設定されています。"); }

            board = new PuzzlePanel[SquareWidth, SquareHeight];
            completedBorad = new PuzzlePanel[SquareWidth, SquareHeight];

            // 完成形の盤面を作成
            var innnerPanels = Enumerable.Range(1, (SquareWidth * SquareHeight) - 1).Select(n => new PuzzlePanel(n));
            var panels = new Queue<PuzzlePanel>(innnerPanels);
            for (int y = 0; y < SquareHeight; y++)
            {
                for (int x = 0; x < SquareWidth; x++)
                {
                    // 連番のパネルを右上から右方向に順番にセット。最後に空白パネルをセット
                    completedBorad[x, y] = panels.Count > 0 ? panels.Dequeue() : PuzzlePanel.Free;
                }
            }

            Reset();
        }

        /// <summary>
        /// 盤面を完成形の状態にリセットします。
        /// </summary>
        private void Reset()
        {
            // 完成形の盤面の内容をコピーするだけ
            for (int y = 0; y < SquareHeight; y++)
            {
                for (int x = 0; x < SquareWidth; x++)
                {
                    board[x, y] = completedBorad[x, y];
                }
            }

            UpdateState();
        }

        /// <summary>
        /// 盤面の状態に合わせてインスタンスの状態を更新します。
        /// </summary>
        private void UpdateState()
        {
            FreePanelLocation = GetFreePanelLocation();
            HasCompleted = GetHasCompleted();
        }

        /// <summary>
        /// 空白パネルの位置を返します。
        /// </summary>
        private Point GetFreePanelLocation()
        {
            for (int y = 0; y < SquareHeight; y++)
            {
                for (int x = 0; x < SquareWidth; x++)
                {
                    if (board[x, y] == PuzzlePanel.Free)
                    {
                        return new Point(x, y);
                    }
                }
            }

            throw new InvalidOperationException("空白パネルが見つかりません。");
        }

        /// <summary>
        /// パズルが完成したことを表す値を返します。
        /// </summary>
        private bool GetHasCompleted()
        {
            for (int y = 0; y < SquareHeight; y++)
            {
                for (int x = 0; x < SquareWidth; x++)
                {
                    if (board[x, y] != completedBorad[x, y]) { return false; }
                }
            }
            return true;
        }

        /// <summary>
        /// パズルをシャッフルします。
        /// </summary>
        public void Shuffle()
        {
            Reset();

            var rand = new Random();

            // ランダムにパネルを並べると完成しないパターンが発生する可能性があるので、
            // パネルをランダムにスライドさせてシャッフルする。
            // この問題はWikipediaの「15パズル」のページなどに詳細が載っているので割愛
            const int ShuffleCount = 197;
            for (int i = 0; i < ShuffleCount; i++)
            {
                switch (rand.Next(maxValue: 4)) // 0 - 3 の乱数
                {
                    case 0:
                        SlideLeft();
                        break;
                    case 1:
                        SlideUp();
                        break;
                    case 2:
                        SlideRight();
                        break;
                    case 3:
                        SlideDown();
                        break;
                }
            }
        }

        /// <summary>
        /// 空白パネルの一つ右にあるパネルを左にスライドします。スライドできない場合は何もしません。
        /// </summary>
        public void SlideLeft()
        {
            // 空白パネルが右端にある場合は移動できないので何もしない
            if (FreePanelLocation.X >= SquareWidth - 1) { return; }

            board[FreePanelLocation.X, FreePanelLocation.Y] = board[FreePanelLocation.X + 1, FreePanelLocation.Y];
            board[FreePanelLocation.X + 1, FreePanelLocation.Y] = PuzzlePanel.Free;

            UpdateState();
        }

        /// <summary>
        /// 空白パネルの一つ下にあるパネルを上にスライドします。スライドできない場合は何もしません。
        /// </summary>
        public void SlideUp()
        {
            // 空白パネルが下端にある場合は移動できないので何もしない
            if (FreePanelLocation.Y >= SquareHeight - 1) { return; }

            board[FreePanelLocation.X, FreePanelLocation.Y] = board[FreePanelLocation.X, FreePanelLocation.Y + 1];
            board[FreePanelLocation.X, FreePanelLocation.Y + 1] = PuzzlePanel.Free;

            UpdateState();
        }

        /// <summary>
        /// 空白パネルの一つ左にあるパネルを右にスライドします。スライドできない場合は何もしません。
        /// </summary>
        public void SlideRight()
        {
            // 空白パネルが左端にある場合は移動できないので何もしない
            if (FreePanelLocation.X <= 0) { return; }

            board[FreePanelLocation.X, FreePanelLocation.Y] = board[FreePanelLocation.X - 1, FreePanelLocation.Y];
            board[FreePanelLocation.X - 1, FreePanelLocation.Y] = PuzzlePanel.Free;

            UpdateState();
        }

        /// <summary>
        /// 空白パネルの一つ上にあるパネルを下にスライドします。スライドできない場合は何もしません。
        /// </summary>
        public void SlideDown()
        {
            // 空白パネルが上端にある場合は移動できないので何もしない
            if (FreePanelLocation.Y <= 0) { return; }

            board[FreePanelLocation.X, FreePanelLocation.Y] = board[FreePanelLocation.X, FreePanelLocation.Y - 1];
            board[FreePanelLocation.X, FreePanelLocation.Y - 1] = PuzzlePanel.Free;

            UpdateState();
        }

        /// <summary>
        /// パズルの盤面を文字列に変換して返します。
        /// </summary>
        public override string ToString()
        {
            var text = new StringBuilder();
            for (int y = 0; y < SquareHeight; y++)
            {
                for (int x = 0; x < SquareWidth; x++)
                {
                    text.Append(board[x, y].ToString());
                }
                text.AppendLine();
            }
            return text.ToString();
        }
    }
}
