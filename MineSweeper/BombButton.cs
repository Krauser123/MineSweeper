using System;
using System.Windows.Forms;

namespace MineSweeper
{
    class BombButton : Button
    {      
           
            public bool HasAMine { get; set; }
            public int AdjacentMines { get; set; }
            public bool IsRevealed { get; set; }
            public bool IsFlagged { get; set; }

        internal void SetBombImage()
        {
            this.BackgroundImage = MainForm.ImageBomb;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        internal void SetFlagImage()
        {
            if (this.IsFlagged == false)
            {
                this.IsFlagged = true;
                this.BackgroundImage = MainForm.ImageFlag;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else 
            {
                this.IsFlagged = false;
                this.CleanImage();
            }
        }

        internal void CleanImage()
        {
            this.BackgroundImage = null;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
