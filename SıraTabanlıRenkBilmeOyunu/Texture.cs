namespace TextureManagement
{
    public class Texture
    {
        Form form;
        PictureBox picture;
        PictureBox socket;
        PictureBox selectedPicture;
        PictureBox[] gameTextures = new PictureBox[4];
        public PictureBox[] emptySocket = new PictureBox[4];

        int[,] pictureColors =
        {
            {255,0,0}, // Kırmızı
            {0,255,0}, // Yeşil
            {0,0,255}, // Mavi
            {255,255,0} // Sarı
        };
        string[] pictureColorsEnum =
        {
            "kırmızı",
            "yeşil",
            "mavi",
            "sarı"
        };
        
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

        int margin;
        public Texture(Form form)
        {
            this.form = form;

            this.pictureWidth = 50;
            this.pictureHeight = 50;

            this.socketWidth = 50;
            this.socketHeight = 50;

            this.margin = 25;


            this.pictureOffSetX = this.margin;
            this.pictureOffSetY = this.margin;


            this.pictureX = 0;
            this.pictureY = 0;

            this.socketOffSetX = this.form.Width / 4;
            this.socketOffSetY = this.form.Height - (this.socketWidth + this.margin * 2);
            
            this.socketX = 0;
            this.socketY = 0;
        }
        public void setGameTextures()
        {
            this.pictureX = 0;
            this.pictureY = 0;
            this.socketX = 0;
            this.socketY = 0;
            for (int i=0;i<this.gameTextures.Length;i++)
            {
                this.picture = new PictureBox();

                this.picture.Left = this.pictureX + this.pictureOffSetX;
                this.picture.Top = this.pictureY + this.pictureOffSetY;

                this.picture.Width = this.pictureWidth;
                this.picture.Height = this.pictureHeight;

                this.picture.BackColor = Color.FromArgb(this.pictureColors[i,0], this.pictureColors[i, 1], this.pictureColors[i, 2]);

                this.picture.Tag = this.pictureColorsEnum[i];

                this.form.Controls.Add(this.picture);
                this.gameTextures[i] = this.picture;

                this.pictureY += this.pictureHeight + this.margin;
            }

            for(int i=0;i<this.emptySocket.Length;i++)
            {
                this.socket = new PictureBox();

                this.socket.Left = this.socketX + this.socketOffSetX;
                this.socket.Top = this.socketY + this.socketOffSetY;

                this.socket.Width = this.socketWidth;
                this.socket.Height = this.socketHeight;

                this.socket.BackColor = Color.FromArgb(125, 125, 125);

                this.socket.Tag = "empty";

                this.form.Controls.Add(this.socket);
                this.emptySocket[i] = this.socket;

                this.socketX += this.socketWidth + this.margin;
                
            }
        }
        public void setGameTexturesEvents()
        {
            foreach(PictureBox texture in this.gameTextures)
            {
                texture.Click += this.GameTextures_Click;
            }
        }
        public void GameTextures_Click(object sender,EventArgs e)
        {
            PictureBox texture = sender as PictureBox;

            selectedPicture = texture;
            MessageBox.Show(texture.Tag.ToString());
        }
    }
}
