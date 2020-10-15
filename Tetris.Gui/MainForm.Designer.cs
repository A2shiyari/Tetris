namespace Tetris.Gui
{
    partial class MainFrom
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameDeckPicBox = new System.Windows.Forms.PictureBox();
            this.nextTetrominoPicBox = new System.Windows.Forms.PictureBox();
            this.scoreGrp = new System.Windows.Forms.GroupBox();
            this.levelCaptionLbl = new System.Windows.Forms.Label();
            this.levelLbl = new System.Windows.Forms.Label();
            this.scoreCaptionLbl = new System.Windows.Forms.Label();
            this.linesCaptionLbl = new System.Windows.Forms.Label();
            this.scoreLbl = new System.Windows.Forms.Label();
            this.linesLbl = new System.Windows.Forms.Label();
            this.nextLbl = new System.Windows.Forms.Label();
            this.shortcutsGrp = new System.Windows.Forms.GroupBox();
            this.exitGameLbl = new System.Windows.Forms.Label();
            this.restartGameLbl = new System.Windows.Forms.Label();
            this.resumeGameLbl = new System.Windows.Forms.Label();
            this.pauseGameLbl = new System.Windows.Forms.Label();
            this.ghostOnOffLbl = new System.Windows.Forms.Label();
            this.hardDropLbl = new System.Windows.Forms.Label();
            this.moveDownLbl = new System.Windows.Forms.Label();
            this.escapeKeyLbl = new System.Windows.Forms.Label();
            this.enterKeyLbl = new System.Windows.Forms.Label();
            this.rKeyLbl = new System.Windows.Forms.Label();
            this.moveLeftLbl = new System.Windows.Forms.Label();
            this.gKeyLbl = new System.Windows.Forms.Label();
            this.pKeyLbl = new System.Windows.Forms.Label();
            this.spaceKeyLbl = new System.Windows.Forms.Label();
            this.downArrowKeyLbl = new System.Windows.Forms.Label();
            this.leftArrowKeyLbl = new System.Windows.Forms.Label();
            this.moveRightLbl = new System.Windows.Forms.Label();
            this.rotateLbl = new System.Windows.Forms.Label();
            this.rightArrowKeyLbl = new System.Windows.Forms.Label();
            this.upArrowKeyLbl = new System.Windows.Forms.Label();
            this.headerPicBox = new System.Windows.Forms.PictureBox();
            this.holdTetrominoPicBox = new System.Windows.Forms.PictureBox();
            this.holdLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameDeckPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextTetrominoPicBox)).BeginInit();
            this.scoreGrp.SuspendLayout();
            this.shortcutsGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.holdTetrominoPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameDeckPicBox
            // 
            this.gameDeckPicBox.Location = new System.Drawing.Point(40, 101);
            this.gameDeckPicBox.Name = "gameDeckPicBox";
            this.gameDeckPicBox.Size = new System.Drawing.Size(378, 416);
            this.gameDeckPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.gameDeckPicBox.TabIndex = 0;
            this.gameDeckPicBox.TabStop = false;
            // 
            // nextTetrominoPicBox
            // 
            this.nextTetrominoPicBox.Location = new System.Drawing.Point(455, 101);
            this.nextTetrominoPicBox.Name = "nextTetrominoPicBox";
            this.nextTetrominoPicBox.Size = new System.Drawing.Size(273, 240);
            this.nextTetrominoPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.nextTetrominoPicBox.TabIndex = 0;
            this.nextTetrominoPicBox.TabStop = false;
            // 
            // scoreGrp
            // 
            this.scoreGrp.Controls.Add(this.levelCaptionLbl);
            this.scoreGrp.Controls.Add(this.levelLbl);
            this.scoreGrp.Controls.Add(this.scoreCaptionLbl);
            this.scoreGrp.Controls.Add(this.linesCaptionLbl);
            this.scoreGrp.Controls.Add(this.scoreLbl);
            this.scoreGrp.Controls.Add(this.linesLbl);
            this.scoreGrp.Location = new System.Drawing.Point(449, 366);
            this.scoreGrp.Name = "scoreGrp";
            this.scoreGrp.Size = new System.Drawing.Size(238, 110);
            this.scoreGrp.TabIndex = 1;
            this.scoreGrp.TabStop = false;
            // 
            // levelCaptionLbl
            // 
            this.levelCaptionLbl.AutoSize = true;
            this.levelCaptionLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.levelCaptionLbl.Location = new System.Drawing.Point(21, 75);
            this.levelCaptionLbl.Name = "levelCaptionLbl";
            this.levelCaptionLbl.Size = new System.Drawing.Size(37, 15);
            this.levelCaptionLbl.TabIndex = 0;
            this.levelCaptionLbl.Text = "Level:";
            // 
            // levelLbl
            // 
            this.levelLbl.AutoSize = true;
            this.levelLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.levelLbl.Location = new System.Drawing.Point(118, 75);
            this.levelLbl.Name = "levelLbl";
            this.levelLbl.Size = new System.Drawing.Size(14, 15);
            this.levelLbl.TabIndex = 0;
            this.levelLbl.Text = "0";
            // 
            // scoreCaptionLbl
            // 
            this.scoreCaptionLbl.AutoSize = true;
            this.scoreCaptionLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.scoreCaptionLbl.Location = new System.Drawing.Point(21, 50);
            this.scoreCaptionLbl.Name = "scoreCaptionLbl";
            this.scoreCaptionLbl.Size = new System.Drawing.Size(40, 15);
            this.scoreCaptionLbl.TabIndex = 0;
            this.scoreCaptionLbl.Text = "Score:";
            // 
            // linesCaptionLbl
            // 
            this.linesCaptionLbl.AutoSize = true;
            this.linesCaptionLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linesCaptionLbl.Location = new System.Drawing.Point(21, 25);
            this.linesCaptionLbl.Name = "linesCaptionLbl";
            this.linesCaptionLbl.Size = new System.Drawing.Size(37, 15);
            this.linesCaptionLbl.TabIndex = 0;
            this.linesCaptionLbl.Text = "Lines:";
            // 
            // scoreLbl
            // 
            this.scoreLbl.AutoSize = true;
            this.scoreLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.scoreLbl.Location = new System.Drawing.Point(118, 50);
            this.scoreLbl.Name = "scoreLbl";
            this.scoreLbl.Size = new System.Drawing.Size(14, 15);
            this.scoreLbl.TabIndex = 0;
            this.scoreLbl.Text = "0";
            // 
            // linesLbl
            // 
            this.linesLbl.AutoSize = true;
            this.linesLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linesLbl.Location = new System.Drawing.Point(118, 25);
            this.linesLbl.Name = "linesLbl";
            this.linesLbl.Size = new System.Drawing.Size(14, 15);
            this.linesLbl.TabIndex = 0;
            this.linesLbl.Text = "0";
            // 
            // nextLbl
            // 
            this.nextLbl.AutoSize = true;
            this.nextLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nextLbl.Location = new System.Drawing.Point(421, 213);
            this.nextLbl.Name = "nextLbl";
            this.nextLbl.Size = new System.Drawing.Size(32, 15);
            this.nextLbl.TabIndex = 0;
            this.nextLbl.Text = "Next";
            // 
            // shortcutsGrp
            // 
            this.shortcutsGrp.Controls.Add(this.exitGameLbl);
            this.shortcutsGrp.Controls.Add(this.restartGameLbl);
            this.shortcutsGrp.Controls.Add(this.resumeGameLbl);
            this.shortcutsGrp.Controls.Add(this.pauseGameLbl);
            this.shortcutsGrp.Controls.Add(this.ghostOnOffLbl);
            this.shortcutsGrp.Controls.Add(this.hardDropLbl);
            this.shortcutsGrp.Controls.Add(this.moveDownLbl);
            this.shortcutsGrp.Controls.Add(this.escapeKeyLbl);
            this.shortcutsGrp.Controls.Add(this.enterKeyLbl);
            this.shortcutsGrp.Controls.Add(this.rKeyLbl);
            this.shortcutsGrp.Controls.Add(this.moveLeftLbl);
            this.shortcutsGrp.Controls.Add(this.gKeyLbl);
            this.shortcutsGrp.Controls.Add(this.pKeyLbl);
            this.shortcutsGrp.Controls.Add(this.spaceKeyLbl);
            this.shortcutsGrp.Controls.Add(this.downArrowKeyLbl);
            this.shortcutsGrp.Controls.Add(this.leftArrowKeyLbl);
            this.shortcutsGrp.Controls.Add(this.moveRightLbl);
            this.shortcutsGrp.Controls.Add(this.rotateLbl);
            this.shortcutsGrp.Controls.Add(this.rightArrowKeyLbl);
            this.shortcutsGrp.Controls.Add(this.upArrowKeyLbl);
            this.shortcutsGrp.Location = new System.Drawing.Point(760, 129);
            this.shortcutsGrp.Name = "shortcutsGrp";
            this.shortcutsGrp.Size = new System.Drawing.Size(238, 288);
            this.shortcutsGrp.TabIndex = 1;
            this.shortcutsGrp.TabStop = false;
            // 
            // exitGameLbl
            // 
            this.exitGameLbl.AutoSize = true;
            this.exitGameLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.exitGameLbl.Location = new System.Drawing.Point(21, 250);
            this.exitGameLbl.Name = "exitGameLbl";
            this.exitGameLbl.Size = new System.Drawing.Size(63, 15);
            this.exitGameLbl.TabIndex = 0;
            this.exitGameLbl.Text = "Exit Game:";
            // 
            // restartGameLbl
            // 
            this.restartGameLbl.AutoSize = true;
            this.restartGameLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.restartGameLbl.Location = new System.Drawing.Point(21, 225);
            this.restartGameLbl.Name = "restartGameLbl";
            this.restartGameLbl.Size = new System.Drawing.Size(80, 15);
            this.restartGameLbl.TabIndex = 0;
            this.restartGameLbl.Text = "Restart Game:";
            // 
            // resumeGameLbl
            // 
            this.resumeGameLbl.AutoSize = true;
            this.resumeGameLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.resumeGameLbl.Location = new System.Drawing.Point(21, 200);
            this.resumeGameLbl.Name = "resumeGameLbl";
            this.resumeGameLbl.Size = new System.Drawing.Size(86, 15);
            this.resumeGameLbl.TabIndex = 0;
            this.resumeGameLbl.Text = "Resume Game:";
            // 
            // pauseGameLbl
            // 
            this.pauseGameLbl.AutoSize = true;
            this.pauseGameLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pauseGameLbl.Location = new System.Drawing.Point(21, 175);
            this.pauseGameLbl.Name = "pauseGameLbl";
            this.pauseGameLbl.Size = new System.Drawing.Size(75, 15);
            this.pauseGameLbl.TabIndex = 0;
            this.pauseGameLbl.Text = "Pause Game:";
            // 
            // ghostOnOffLbl
            // 
            this.ghostOnOffLbl.AutoSize = true;
            this.ghostOnOffLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ghostOnOffLbl.Location = new System.Drawing.Point(21, 150);
            this.ghostOnOffLbl.Name = "ghostOnOffLbl";
            this.ghostOnOffLbl.Size = new System.Drawing.Size(82, 15);
            this.ghostOnOffLbl.TabIndex = 0;
            this.ghostOnOffLbl.Text = "Ghost On/Off:";
            // 
            // hardDropLbl
            // 
            this.hardDropLbl.AutoSize = true;
            this.hardDropLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.hardDropLbl.Location = new System.Drawing.Point(21, 125);
            this.hardDropLbl.Name = "hardDropLbl";
            this.hardDropLbl.Size = new System.Drawing.Size(66, 15);
            this.hardDropLbl.TabIndex = 0;
            this.hardDropLbl.Text = "Hard Drop:";
            // 
            // moveDownLbl
            // 
            this.moveDownLbl.AutoSize = true;
            this.moveDownLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.moveDownLbl.Location = new System.Drawing.Point(21, 100);
            this.moveDownLbl.Name = "moveDownLbl";
            this.moveDownLbl.Size = new System.Drawing.Size(75, 15);
            this.moveDownLbl.TabIndex = 0;
            this.moveDownLbl.Text = "Move Down:";
            // 
            // escapeKeyLbl
            // 
            this.escapeKeyLbl.AutoSize = true;
            this.escapeKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.escapeKeyLbl.Location = new System.Drawing.Point(118, 250);
            this.escapeKeyLbl.Name = "escapeKeyLbl";
            this.escapeKeyLbl.Size = new System.Drawing.Size(65, 15);
            this.escapeKeyLbl.TabIndex = 0;
            this.escapeKeyLbl.Text = "Escape Key";
            // 
            // enterKeyLbl
            // 
            this.enterKeyLbl.AutoSize = true;
            this.enterKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.enterKeyLbl.Location = new System.Drawing.Point(118, 225);
            this.enterKeyLbl.Name = "enterKeyLbl";
            this.enterKeyLbl.Size = new System.Drawing.Size(56, 15);
            this.enterKeyLbl.TabIndex = 0;
            this.enterKeyLbl.Text = "Enter Key";
            // 
            // rKeyLbl
            // 
            this.rKeyLbl.AutoSize = true;
            this.rKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rKeyLbl.Location = new System.Drawing.Point(118, 200);
            this.rKeyLbl.Name = "rKeyLbl";
            this.rKeyLbl.Size = new System.Drawing.Size(36, 15);
            this.rKeyLbl.TabIndex = 0;
            this.rKeyLbl.Text = "R Key";
            // 
            // moveLeftLbl
            // 
            this.moveLeftLbl.AutoSize = true;
            this.moveLeftLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.moveLeftLbl.Location = new System.Drawing.Point(21, 75);
            this.moveLeftLbl.Name = "moveLeftLbl";
            this.moveLeftLbl.Size = new System.Drawing.Size(63, 15);
            this.moveLeftLbl.TabIndex = 0;
            this.moveLeftLbl.Text = "Move Left:";
            // 
            // gKeyLbl
            // 
            this.gKeyLbl.AutoSize = true;
            this.gKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gKeyLbl.Location = new System.Drawing.Point(118, 150);
            this.gKeyLbl.Name = "gKeyLbl";
            this.gKeyLbl.Size = new System.Drawing.Size(37, 15);
            this.gKeyLbl.TabIndex = 0;
            this.gKeyLbl.Text = "G Key";
            // 
            // pKeyLbl
            // 
            this.pKeyLbl.AutoSize = true;
            this.pKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.pKeyLbl.Location = new System.Drawing.Point(118, 175);
            this.pKeyLbl.Name = "pKeyLbl";
            this.pKeyLbl.Size = new System.Drawing.Size(36, 15);
            this.pKeyLbl.TabIndex = 0;
            this.pKeyLbl.Text = "P Key";
            // 
            // spaceKeyLbl
            // 
            this.spaceKeyLbl.AutoSize = true;
            this.spaceKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.spaceKeyLbl.Location = new System.Drawing.Point(118, 125);
            this.spaceKeyLbl.Name = "spaceKeyLbl";
            this.spaceKeyLbl.Size = new System.Drawing.Size(61, 15);
            this.spaceKeyLbl.TabIndex = 0;
            this.spaceKeyLbl.Text = "Space Key";
            // 
            // downArrowKeyLbl
            // 
            this.downArrowKeyLbl.AutoSize = true;
            this.downArrowKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.downArrowKeyLbl.Location = new System.Drawing.Point(118, 100);
            this.downArrowKeyLbl.Name = "downArrowKeyLbl";
            this.downArrowKeyLbl.Size = new System.Drawing.Size(96, 15);
            this.downArrowKeyLbl.TabIndex = 0;
            this.downArrowKeyLbl.Text = "Down Arrow Key";
            // 
            // leftArrowKeyLbl
            // 
            this.leftArrowKeyLbl.AutoSize = true;
            this.leftArrowKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.leftArrowKeyLbl.Location = new System.Drawing.Point(118, 75);
            this.leftArrowKeyLbl.Name = "leftArrowKeyLbl";
            this.leftArrowKeyLbl.Size = new System.Drawing.Size(84, 15);
            this.leftArrowKeyLbl.TabIndex = 0;
            this.leftArrowKeyLbl.Text = "Left Arrow Key";
            // 
            // moveRightLbl
            // 
            this.moveRightLbl.AutoSize = true;
            this.moveRightLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.moveRightLbl.Location = new System.Drawing.Point(21, 50);
            this.moveRightLbl.Name = "moveRightLbl";
            this.moveRightLbl.Size = new System.Drawing.Size(71, 15);
            this.moveRightLbl.TabIndex = 0;
            this.moveRightLbl.Text = "Move Right:";
            // 
            // rotateLbl
            // 
            this.rotateLbl.AutoSize = true;
            this.rotateLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rotateLbl.Location = new System.Drawing.Point(21, 25);
            this.rotateLbl.Name = "rotateLbl";
            this.rotateLbl.Size = new System.Drawing.Size(44, 15);
            this.rotateLbl.TabIndex = 0;
            this.rotateLbl.Text = "Rotate:";
            // 
            // rightArrowKeyLbl
            // 
            this.rightArrowKeyLbl.AutoSize = true;
            this.rightArrowKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rightArrowKeyLbl.Location = new System.Drawing.Point(118, 50);
            this.rightArrowKeyLbl.Name = "rightArrowKeyLbl";
            this.rightArrowKeyLbl.Size = new System.Drawing.Size(92, 15);
            this.rightArrowKeyLbl.TabIndex = 0;
            this.rightArrowKeyLbl.Text = "Right Arrow Key";
            // 
            // upArrowKeyLbl
            // 
            this.upArrowKeyLbl.AutoSize = true;
            this.upArrowKeyLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.upArrowKeyLbl.Location = new System.Drawing.Point(118, 25);
            this.upArrowKeyLbl.Name = "upArrowKeyLbl";
            this.upArrowKeyLbl.Size = new System.Drawing.Size(79, 15);
            this.upArrowKeyLbl.TabIndex = 0;
            this.upArrowKeyLbl.Text = "Up Arrow Key";
            // 
            // headerPicBox
            // 
            this.headerPicBox.Location = new System.Drawing.Point(40, 45);
            this.headerPicBox.Name = "headerPicBox";
            this.headerPicBox.Size = new System.Drawing.Size(378, 50);
            this.headerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.headerPicBox.TabIndex = 2;
            this.headerPicBox.TabStop = false;
            // 
            // holdTetrominoPicBox
            // 
            this.holdTetrominoPicBox.Location = new System.Drawing.Point(739, 452);
            this.holdTetrominoPicBox.Name = "holdTetrominoPicBox";
            this.holdTetrominoPicBox.Size = new System.Drawing.Size(273, 240);
            this.holdTetrominoPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.holdTetrominoPicBox.TabIndex = 0;
            this.holdTetrominoPicBox.TabStop = false;
            // 
            // holdLbl
            // 
            this.holdLbl.AutoSize = true;
            this.holdLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.holdLbl.Location = new System.Drawing.Point(499, 502);
            this.holdLbl.Name = "holdLbl";
            this.holdLbl.Size = new System.Drawing.Size(33, 15);
            this.holdLbl.TabIndex = 0;
            this.holdLbl.Text = "Hold";
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 545);
            this.Controls.Add(this.holdLbl);
            this.Controls.Add(this.headerPicBox);
            this.Controls.Add(this.shortcutsGrp);
            this.Controls.Add(this.scoreGrp);
            this.Controls.Add(this.holdTetrominoPicBox);
            this.Controls.Add(this.nextTetrominoPicBox);
            this.Controls.Add(this.gameDeckPicBox);
            this.Controls.Add(this.nextLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MainFrom";
            this.Text = "Tetris";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainFrom_Activated);
            this.Deactivate += new System.EventHandler(this.MainFrom_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrom_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFrom_KeyUp);
            this.Resize += new System.EventHandler(this.MainFrom_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gameDeckPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextTetrominoPicBox)).EndInit();
            this.scoreGrp.ResumeLayout(false);
            this.scoreGrp.PerformLayout();
            this.shortcutsGrp.ResumeLayout(false);
            this.shortcutsGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.holdTetrominoPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox gameDeckPicBox;
        private System.Windows.Forms.PictureBox nextTetrominoPicBox;
        private System.Windows.Forms.GroupBox scoreGrp;
        private System.Windows.Forms.Label linesLbl;
        private System.Windows.Forms.Label levelCaptionLbl;
        private System.Windows.Forms.Label levelLbl;
        private System.Windows.Forms.Label scoreCaptionLbl;
        private System.Windows.Forms.Label linesCaptionLbl;
        private System.Windows.Forms.Label scoreLbl;
        private System.Windows.Forms.Label nextLbl;
        private System.Windows.Forms.GroupBox shortcutsGrp;
        private System.Windows.Forms.Label moveLeftLbl;
        private System.Windows.Forms.Label leftArrowKeyLbl;
        private System.Windows.Forms.Label moveRightLbl;
        private System.Windows.Forms.Label rotateLbl;
        private System.Windows.Forms.Label rightArrowKeyLbl;
        private System.Windows.Forms.Label upArrowKeyLbl;
        private System.Windows.Forms.Label moveDownLbl;
        private System.Windows.Forms.Label downArrowKeyLbl;
        private System.Windows.Forms.Label hardDropLbl;
        private System.Windows.Forms.Label spaceKeyLbl;
        private System.Windows.Forms.Label resumeGameLbl;
        private System.Windows.Forms.Label pauseGameLbl;
        private System.Windows.Forms.Label rKeyLbl;
        private System.Windows.Forms.Label pKeyLbl;
        private System.Windows.Forms.Label restartGameLbl;
        private System.Windows.Forms.Label enterKeyLbl;
        private System.Windows.Forms.Label exitGameLbl;
        private System.Windows.Forms.Label escapeKeyLbl;
        private System.Windows.Forms.Label ghostOnOffLbl;
        private System.Windows.Forms.Label gKeyLbl;
        private System.Windows.Forms.PictureBox headerPicBox;
        private System.Windows.Forms.PictureBox holdTetrominoPicBox;
        private System.Windows.Forms.Label holdLbl;
    }
}

