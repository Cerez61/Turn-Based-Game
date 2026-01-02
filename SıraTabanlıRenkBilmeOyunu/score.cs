namespace ScoreManagement
{
    public class Score {
        Form form;

        System.Windows.Forms.Timer stopwatchTimer;
        int elapsedSeconds; 
        Label timerLabel;


        public float gameScore;
        Label gameScoreLabel;
        public Score(Form form)
        {
            this.form = form;

        }
        public void initScore()
        {
            this.ResetTimer();

            this.stopwatchTimer = new System.Windows.Forms.Timer();
            this.timerLabel = new Label();
            this.gameScoreLabel = new Label();
            this.elapsedSeconds = 0;
            this.SetupTimer();
        }
        public void SetupTimer()
        {
            timerLabel.Text = "Süre: 00:00";
            timerLabel.AutoSize = true;
            timerLabel.BackColor = Color.Transparent;
            timerLabel.ForeColor = Color.Black;

            timerLabel.Left = this.form.Width / 2 - (timerLabel.Width / 2);
            timerLabel.Top = 10;

            this.form.Controls.Add(timerLabel);
            timerLabel.BringToFront(); 

            stopwatchTimer.Interval = 1000;
            stopwatchTimer.Tick += this.StopwatchTimer_Tick;
            this.stopwatchTimer.Start();
        }
        public void ResetTimer()
        {
            if (this.stopwatchTimer != null)
            {
                this.stopwatchTimer.Stop();

                this.stopwatchTimer.Tick -= this.StopwatchTimer_Tick;

                this.stopwatchTimer.Dispose();
            }

            if (this.timerLabel != null) this.timerLabel.Text = "00:00";
        }
        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;

            // Saniyeyi Dakika:Saniye formatýna çevir
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            timerLabel.Text = "Süre: " + time.ToString(@"mm\:ss");
        }
        public void CalculateScore(int guessCount,int totalCount)
        {
            this.gameScore = (float)(totalCount - guessCount) * (float)((this.elapsedSeconds) + 10 / 10);
            MessageBox.Show($"{(this.elapsedSeconds + 10 / 10).ToString()},{totalCount.ToString()},{guessCount.ToString()}");

            this.gameScore = 1000000 * (float)(this.gameScore / 100);
            MessageBox.Show(this.gameScore.ToString());

        }
    }
}