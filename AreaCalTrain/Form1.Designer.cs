namespace haocon_ocr_0518
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonProcSingleImg = new System.Windows.Forms.Button();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.textBoxSettingValue = new System.Windows.Forms.TextBox();
            this.textBoxMeasureValue = new System.Windows.Forms.TextBox();
            this.buttonOpenImg = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelMessage = new System.Windows.Forms.Label();
            this.textBoxPriorChar = new System.Windows.Forms.TextBox();
            this.labelPriorChar = new System.Windows.Forms.Label();
            this.buttonSaveValue = new System.Windows.Forms.Button();
            this.labelPriorCharAll = new System.Windows.Forms.Label();
            this.textBoxPriorCharAll = new System.Windows.Forms.TextBox();
            this.buttonOpenDir = new System.Windows.Forms.Button();
            this.buttonNextImg = new System.Windows.Forms.Button();
            this.buttonProcMultiImg = new System.Windows.Forms.Button();
            this.textBoxAreaMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelChar = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonProcSingleImg
            // 
            this.buttonProcSingleImg.Location = new System.Drawing.Point(181, 133);
            this.buttonProcSingleImg.Name = "buttonProcSingleImg";
            this.buttonProcSingleImg.Size = new System.Drawing.Size(75, 23);
            this.buttonProcSingleImg.TabIndex = 0;
            this.buttonProcSingleImg.Text = "单张处理";
            this.buttonProcSingleImg.UseVisualStyleBackColor = true;
            this.buttonProcSingleImg.Click += new System.EventHandler(this.button1_Click);
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(281, 1);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(417, 383);
            this.hWindowControl1.TabIndex = 1;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(417, 383);
            // 
            // textBoxSettingValue
            // 
            this.textBoxSettingValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSettingValue.Location = new System.Drawing.Point(47, 464);
            this.textBoxSettingValue.Multiline = true;
            this.textBoxSettingValue.Name = "textBoxSettingValue";
            this.textBoxSettingValue.Size = new System.Drawing.Size(651, 27);
            this.textBoxSettingValue.TabIndex = 2;
            // 
            // textBoxMeasureValue
            // 
            this.textBoxMeasureValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMeasureValue.Location = new System.Drawing.Point(47, 433);
            this.textBoxMeasureValue.Multiline = true;
            this.textBoxMeasureValue.Name = "textBoxMeasureValue";
            this.textBoxMeasureValue.Size = new System.Drawing.Size(651, 26);
            this.textBoxMeasureValue.TabIndex = 3;
            // 
            // buttonOpenImg
            // 
            this.buttonOpenImg.Location = new System.Drawing.Point(47, 133);
            this.buttonOpenImg.Name = "buttonOpenImg";
            this.buttonOpenImg.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenImg.TabIndex = 4;
            this.buttonOpenImg.Text = "打开图片";
            this.buttonOpenImg.UseVisualStyleBackColor = true;
            this.buttonOpenImg.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMessage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelMessage.Location = new System.Drawing.Point(10, 258);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 14);
            this.labelMessage.TabIndex = 5;
            // 
            // textBoxPriorChar
            // 
            this.textBoxPriorChar.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPriorChar.Location = new System.Drawing.Point(47, 64);
            this.textBoxPriorChar.Multiline = true;
            this.textBoxPriorChar.Name = "textBoxPriorChar";
            this.textBoxPriorChar.Size = new System.Drawing.Size(209, 44);
            this.textBoxPriorChar.TabIndex = 6;
            this.textBoxPriorChar.Text = "20180827095813E";
            // 
            // labelPriorChar
            // 
            this.labelPriorChar.Location = new System.Drawing.Point(12, 73);
            this.labelPriorChar.Name = "labelPriorChar";
            this.labelPriorChar.Size = new System.Drawing.Size(29, 28);
            this.labelPriorChar.TabIndex = 7;
            this.labelPriorChar.Text = "预存喷码";
            // 
            // buttonSaveValue
            // 
            this.buttonSaveValue.Location = new System.Drawing.Point(181, 226);
            this.buttonSaveValue.Name = "buttonSaveValue";
            this.buttonSaveValue.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveValue.TabIndex = 9;
            this.buttonSaveValue.Text = "保存阈值";
            this.buttonSaveValue.UseVisualStyleBackColor = true;
            this.buttonSaveValue.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelPriorCharAll
            // 
            this.labelPriorCharAll.Location = new System.Drawing.Point(12, 9);
            this.labelPriorCharAll.Name = "labelPriorCharAll";
            this.labelPriorCharAll.Size = new System.Drawing.Size(29, 28);
            this.labelPriorCharAll.TabIndex = 10;
            this.labelPriorCharAll.Text = "喷码范围";
            // 
            // textBoxPriorCharAll
            // 
            this.textBoxPriorCharAll.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPriorCharAll.Location = new System.Drawing.Point(47, 1);
            this.textBoxPriorCharAll.Multiline = true;
            this.textBoxPriorCharAll.Name = "textBoxPriorCharAll";
            this.textBoxPriorCharAll.Size = new System.Drawing.Size(209, 44);
            this.textBoxPriorCharAll.TabIndex = 11;
            this.textBoxPriorCharAll.Text = "0123456789E";
            // 
            // buttonOpenDir
            // 
            this.buttonOpenDir.Enabled = false;
            this.buttonOpenDir.Location = new System.Drawing.Point(47, 179);
            this.buttonOpenDir.Name = "buttonOpenDir";
            this.buttonOpenDir.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenDir.TabIndex = 12;
            this.buttonOpenDir.Text = "选择文件夹";
            this.buttonOpenDir.UseVisualStyleBackColor = true;
            // 
            // buttonNextImg
            // 
            this.buttonNextImg.Enabled = false;
            this.buttonNextImg.Location = new System.Drawing.Point(47, 226);
            this.buttonNextImg.Name = "buttonNextImg";
            this.buttonNextImg.Size = new System.Drawing.Size(75, 23);
            this.buttonNextImg.TabIndex = 13;
            this.buttonNextImg.Text = "下一张";
            this.buttonNextImg.UseVisualStyleBackColor = true;
            // 
            // buttonProcMultiImg
            // 
            this.buttonProcMultiImg.Enabled = false;
            this.buttonProcMultiImg.Location = new System.Drawing.Point(181, 179);
            this.buttonProcMultiImg.Name = "buttonProcMultiImg";
            this.buttonProcMultiImg.Size = new System.Drawing.Size(75, 23);
            this.buttonProcMultiImg.TabIndex = 14;
            this.buttonProcMultiImg.Text = "批量处理";
            this.buttonProcMultiImg.UseVisualStyleBackColor = true;
            // 
            // textBoxAreaMin
            // 
            this.textBoxAreaMin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAreaMin.Location = new System.Drawing.Point(47, 308);
            this.textBoxAreaMin.Multiline = true;
            this.textBoxAreaMin.Name = "textBoxAreaMin";
            this.textBoxAreaMin.Size = new System.Drawing.Size(209, 76);
            this.textBoxAreaMin.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(96, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "所有喷码最小值";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelChar
            // 
            this.labelChar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelChar.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelChar.Location = new System.Drawing.Point(47, 398);
            this.labelChar.Name = "labelChar";
            this.labelChar.Size = new System.Drawing.Size(651, 30);
            this.labelChar.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 28);
            this.label3.TabIndex = 18;
            this.label3.Text = "测量值";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 463);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 28);
            this.label4.TabIndex = 19;
            this.label4.Text = "设定值";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 403);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 14);
            this.label2.TabIndex = 20;
            this.label2.Text = "喷码";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 548);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelChar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAreaMin);
            this.Controls.Add(this.buttonProcMultiImg);
            this.Controls.Add(this.buttonNextImg);
            this.Controls.Add(this.buttonOpenDir);
            this.Controls.Add(this.textBoxPriorCharAll);
            this.Controls.Add(this.labelPriorCharAll);
            this.Controls.Add(this.buttonSaveValue);
            this.Controls.Add(this.labelPriorChar);
            this.Controls.Add(this.textBoxPriorChar);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.buttonOpenImg);
            this.Controls.Add(this.textBoxMeasureValue);
            this.Controls.Add(this.textBoxSettingValue);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.buttonProcSingleImg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProcSingleImg;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.TextBox textBoxSettingValue;
        private System.Windows.Forms.TextBox textBoxMeasureValue;
        private System.Windows.Forms.Button buttonOpenImg;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TextBox textBoxPriorChar;
        private System.Windows.Forms.Label labelPriorChar;
        private System.Windows.Forms.Button buttonSaveValue;
        private System.Windows.Forms.Label labelPriorCharAll;
        private System.Windows.Forms.TextBox textBoxPriorCharAll;
        private System.Windows.Forms.Button buttonOpenDir;
        private System.Windows.Forms.Button buttonNextImg;
        private System.Windows.Forms.Button buttonProcMultiImg;
        private System.Windows.Forms.TextBox textBoxAreaMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelChar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

