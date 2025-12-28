namespace TextureManagement
{
    public class Texture
    {
        Form form;
        PictureBox texture;
        PictureBox socket;
        PictureBox selectedPicture;
        PictureBox[] gameTextures = new PictureBox[4];
        public PictureBox[] emptySocket = new PictureBox[4];
        PictureBox[,] guessTextures = new PictureBox[4,4];

        int[,] pictureColors =
        {
            {255,0,0}, // Kırmızı
            {0,255,0}, // Yeşil
            {0,0,255}, // Mavi
            {255,255,0}, // Sarı
        };
        int[] socketColors = { 125, 125, 125 };
        string[] pictureColorsEnum =
        {
            "red",
            "green",
            "blue",
            "yellow"
        };

        public int guessCount;

        int pictureWidth;
        int pictureHeight;

        int pictureOffSetX;
        int pictureOffSetY;
        int pictureX;
        int pictureY;

        int socketWidth;
        int socketHeight;
        int socketOffSetX;
        int socketOffSetY;
        int socketX;
        int socketY;

        int guessWidth;
        int guessHeight;
        int guessOffSetX;
        int guessOffSetY;
        int guessX;
        int guessY;

        int margin;
        public Texture(Form form)
        {
            this.form = form;


            this.margin = 25;

            this.pictureWidth = 50;
            this.pictureHeight = 50;
            this.socketWidth = 50;
            this.socketHeight = 50;
            this.guessWidth = 40;
            this.guessHeight = 40;



            this.pictureOffSetX = this.margin;
            this.pictureOffSetY = this.margin;
            this.socketOffSetX = this.form.Width / 4;
            this.socketOffSetY = this.form.Height - (this.socketHeight + this.margin * 2);
            this.guessOffSetX = this.form.Width / 4 + this.margin;
            this.guessOffSetY = (int)(this.margin * 1.5f);

            this.pictureX = 0;
            this.pictureY = 0;
            this.socketX = 0;
            this.socketY = 0;
            this.guessX = 0;
            this.guessY = 0;

        }
        public void setGameTextures()
        {
            this.pictureX = 0;
            this.pictureY = 0;
            this.socketX = 0;
            this.socketY = 0;
            for (int i=0;i<this.gameTextures.Length;i++)
            {
                this.texture = new PictureBox();

                this.texture.Left = this.pictureX + this.pictureOffSetX;
                this.texture.Top = this.pictureY + this.pictureOffSetY;

                this.texture.Width = this.pictureWidth;
                this.texture.Height = this.pictureHeight;

                this.texture.BackColor = Color.FromArgb(this.pictureColors[i,0], this.pictureColors[i, 1], this.pictureColors[i, 2]);

                this.texture.Tag = this.pictureColorsEnum[i];

                this.form.Controls.Add(this.texture);
                this.gameTextures[i] = this.texture;

                this.pictureY += this.pictureHeight + this.margin;
            }

            for(int i=0;i<this.emptySocket.Length;i++)
            {
                this.socket = new PictureBox();

                this.socket.Left = this.socketX + this.socketOffSetX;
                this.socket.Top = this.socketY + this.socketOffSetY;

                this.socket.Width = this.socketWidth;
                this.socket.Height = this.socketHeight;

                this.socket.BackColor = Color.FromArgb(this.socketColors[0], this.socketColors[1], this.socketColors[2]);

                this.socket.Tag = "empty";

                this.form.Controls.Add(this.socket);
                this.emptySocket[i] = this.socket;

                this.socketX += this.socketWidth + this.margin;
                
            }
        }
        public void setGameTextureEvents()
        {
            foreach(PictureBox texture in this.gameTextures)
            {
                texture.Click += this.GameTextures_Click;
            }
            foreach(PictureBox socket in this.emptySocket)
            {
                socket.MouseDown += this.EmptySocket_MouseDown;
            }
        }

        public void setGuessTextures()
        {
            for(int column=0;column<this.guessTextures.GetLength(0);column++)
            {
                for (int row = 0; row < this.guessTextures.GetLength(1); row++)
                {
                    this.texture = new PictureBox();

                    this.texture.Width = this.guessWidth;
                    this.texture.Height = this.guessHeight;

                    this.texture.Left = this.guessOffSetX + this.guessX;
                    this.texture.Top = this.guessOffSetY + this.guessY;

                    this.texture.BackColor = Color.Gray;

                    this.texture.Name = $"{column},{row}";
                    this.form.Controls.Add(this.texture);

                    this.guessTextures[column,row] = this.texture;

                    this.guessX += this.guessWidth + this.margin;
                }
                this.guessX = 0;
                this.guessY += this.guessHeight + this.margin;
            }
        }

        public void showGuess(Label guessText)
        {
            int column = this.guessTextures.GetLength(0) - this.guessCount;
            int lastRow = this.guessTextures.GetLength(1) - 1;
            for(int row=0;row<this.guessTextures.GetLength(1);row++)
            {
                this.guessTextures[column, row].Tag = this.emptySocket[row].Tag;
                this.guessTextures[column, row].BackColor = this.emptySocket[row].BackColor;
            }

            guessText.Left = this.guessTextures[column, lastRow].Left + this.margin + guessText.Width;
            guessText.Top = this.guessTextures[column, lastRow].Top;


        }
        public void GameTextures_Click(object sender,EventArgs e)
        {
            PictureBox texture = sender as PictureBox;

            this.selectedPicture = texture;
        }
        public void EmptySocket_MouseDown(object sender,MouseEventArgs e)
        {
            PictureBox socket = sender as PictureBox;
            bool itExist = false;

            if (e.Button == MouseButtons.Left)
            {

                if (this.selectedPicture == null) return;

                //s stands for socket
                foreach (PictureBox s in this.emptySocket)
                {
                    if (s.Tag.ToString() == this.selectedPicture.Tag.ToString()) itExist = true;
                }

                if(!itExist)
                {
                    socket.Tag = this.selectedPicture.Tag;
                    socket.BackColor = this.selectedPicture.BackColor;
                }


            }
            else if(e.Button == MouseButtons.Right)
            {
                socket.Tag = "empty";
                socket.BackColor = Color.FromArgb(this.socketColors[0], this.socketColors[1], this.socketColors[2]);
            }
        }
    }
}
