namespace SalkoDev.EDMS.TestApp
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._ButtonStart = new System.Windows.Forms.Button();
			this._BackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._ButtonLoadFromFossDocDB = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _ButtonStart
			// 
			this._ButtonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._ButtonStart.Location = new System.Drawing.Point(355, 178);
			this._ButtonStart.Name = "_ButtonStart";
			this._ButtonStart.Size = new System.Drawing.Size(92, 23);
			this._ButtonStart.TabIndex = 0;
			this._ButtonStart.Text = "Старт";
			this._ButtonStart.UseVisualStyleBackColor = true;
			this._ButtonStart.Click += new System.EventHandler(this._ButtonStart_Click);
			// 
			// _BackgroundWorker
			// 
			this._BackgroundWorker.WorkerReportsProgress = true;
			this._BackgroundWorker.WorkerSupportsCancellation = true;
			this._BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._BackgroundWorker_DoWork);
			this._BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._BackgroundWorker_RunWorkerCompleted);
			// 
			// _ButtonLoadFromFossDocDB
			// 
			this._ButtonLoadFromFossDocDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._ButtonLoadFromFossDocDB.Location = new System.Drawing.Point(234, 41);
			this._ButtonLoadFromFossDocDB.Name = "_ButtonLoadFromFossDocDB";
			this._ButtonLoadFromFossDocDB.Size = new System.Drawing.Size(213, 23);
			this._ButtonLoadFromFossDocDB.TabIndex = 1;
			this._ButtonLoadFromFossDocDB.Text = "Імпорт кореспондентів з FossDoc";
			this._ButtonLoadFromFossDocDB.UseVisualStyleBackColor = true;
			this._ButtonLoadFromFossDocDB.Click += new System.EventHandler(this._ButtonLoadFromFossDocDB_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(459, 213);
			this.Controls.Add(this._ButtonLoadFromFossDocDB);
			this.Controls.Add(this._ButtonStart);
			this.Name = "Form1";
			this.Text = "Тестовий додаток SalkoDev.EDMS";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _ButtonStart;
		private System.ComponentModel.BackgroundWorker _BackgroundWorker;
		private System.Windows.Forms.Button _ButtonLoadFromFossDocDB;
	}
}

