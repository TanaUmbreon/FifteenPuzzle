using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenPuzzle.Cui.Puzzle;
using NUnit.Framework;

namespace FifteenPuzzle.Cui.Test
{
    [TestFixture]
    public class PuzzlePanelTest
    {
        [Test]
        public void TestCreateNew()
        {
            Assert.Throws<ArgumentException>(() => new PuzzlePanel(0));

            Assert.DoesNotThrow(() => new PuzzlePanel(1));
        }

        [Test]
        public void TestNumber()
        {
            var p = new PuzzlePanel(1);
            Assert.AreEqual(1, p.Number);
        }

        [Test]
        public void TestToString()
        {
            var p = new PuzzlePanel(1);
            Assert.AreEqual("[ 1]", p.ToString());

            Assert.AreEqual("    ", PuzzlePanel.Free.ToString());
        }
    }
}
