using System.Windows.Forms;

namespace MineSweeper
{
    internal class BombButton : Button
    {           
            public bool HasAMine { get; set; }
            public int AdjacentMines { get; set; }
            public bool IsRevealed { get; set; }
            public bool IsFlagged { get; set; }

        /// <summary>
        /// Change background image to display a bomb
        /// </summary>
        internal void SetBombImage()
        {
            this.BackgroundImage = MainForm.ImageBomb;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        /// <summary>
        /// Change background image to display a flag
        /// </summary>
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

        /// <summary>
        /// Remove image for this button
        /// </summary>
        internal void CleanImage()
        {
            this.BackgroundImage = null;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
