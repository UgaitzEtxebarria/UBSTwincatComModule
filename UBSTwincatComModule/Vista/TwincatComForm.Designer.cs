namespace UBSTwincatComModule.Vista
{
    partial class TwincatComForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbOut = new System.Windows.Forms.GroupBox();
            this.lblValorOut = new System.Windows.Forms.Label();
            this.lbl_Valor_Out = new System.Windows.Forms.Label();
            this.lblComentarioOut = new System.Windows.Forms.Label();
            this.lblOutputs = new System.Windows.Forms.Label();
            this.dataGridViewOut = new System.Windows.Forms.DataGridView();
            this.gbIn = new System.Windows.Forms.GroupBox();
            this.lblValorIn = new System.Windows.Forms.Label();
            this.lbl_Valor_In = new System.Windows.Forms.Label();
            this.lblComentarioIn = new System.Windows.Forms.Label();
            this.lblInputs = new System.Windows.Forms.Label();
            this.dataGridViewIn = new System.Windows.Forms.DataGridView();
            this.lblIO = new System.Windows.Forms.Label();
            this.splitContainerIOs = new System.Windows.Forms.SplitContainer();
            this.gbOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOut)).BeginInit();
            this.gbIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIOs)).BeginInit();
            this.splitContainerIOs.Panel1.SuspendLayout();
            this.splitContainerIOs.Panel2.SuspendLayout();
            this.splitContainerIOs.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOut
            // 
            this.gbOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOut.Controls.Add(this.lblValorOut);
            this.gbOut.Controls.Add(this.lbl_Valor_Out);
            this.gbOut.Controls.Add(this.lblComentarioOut);
            this.gbOut.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOut.Location = new System.Drawing.Point(6, 606);
            this.gbOut.Name = "gbOut";
            this.gbOut.Size = new System.Drawing.Size(629, 88);
            this.gbOut.TabIndex = 3;
            this.gbOut.TabStop = false;
            // 
            // lblValorOut
            // 
            this.lblValorOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblValorOut.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblValorOut.Location = new System.Drawing.Point(76, 57);
            this.lblValorOut.Name = "lblValorOut";
            this.lblValorOut.Size = new System.Drawing.Size(403, 19);
            this.lblValorOut.TabIndex = 2;
            this.lblValorOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Valor_Out
            // 
            this.lbl_Valor_Out.AutoSize = true;
            this.lbl_Valor_Out.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Valor_Out.Location = new System.Drawing.Point(7, 57);
            this.lbl_Valor_Out.Name = "lbl_Valor_Out";
            this.lbl_Valor_Out.Size = new System.Drawing.Size(52, 19);
            this.lbl_Valor_Out.TabIndex = 1;
            this.lbl_Valor_Out.Text = "Valor: ";
            // 
            // lblComentarioOut
            // 
            this.lblComentarioOut.AutoSize = true;
            this.lblComentarioOut.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentarioOut.Location = new System.Drawing.Point(7, 20);
            this.lblComentarioOut.Name = "lblComentarioOut";
            this.lblComentarioOut.Size = new System.Drawing.Size(0, 19);
            this.lblComentarioOut.TabIndex = 0;
            // 
            // lblOutputs
            // 
            this.lblOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputs.BackColor = System.Drawing.Color.Silver;
            this.lblOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputs.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblOutputs.Location = new System.Drawing.Point(6, 9);
            this.lblOutputs.Name = "lblOutputs";
            this.lblOutputs.Size = new System.Drawing.Size(629, 23);
            this.lblOutputs.TabIndex = 2;
            this.lblOutputs.Text = "OUTPUTS";
            // 
            // dataGridViewOut
            // 
            this.dataGridViewOut.AllowUserToAddRows = false;
            this.dataGridViewOut.AllowUserToDeleteRows = false;
            this.dataGridViewOut.AllowUserToOrderColumns = true;
            this.dataGridViewOut.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewOut.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOut.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOut.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOut.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOut.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewOut.ColumnHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOut.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewOut.EnableHeadersVisualStyles = false;
            this.dataGridViewOut.Location = new System.Drawing.Point(6, 35);
            this.dataGridViewOut.Name = "dataGridViewOut";
            this.dataGridViewOut.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOut.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewOut.RowHeadersVisible = false;
            this.dataGridViewOut.RowHeadersWidth = 30;
            this.dataGridViewOut.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewOut.RowTemplate.Height = 30;
            this.dataGridViewOut.RowTemplate.ReadOnly = true;
            this.dataGridViewOut.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOut.Size = new System.Drawing.Size(629, 567);
            this.dataGridViewOut.TabIndex = 0;
            // 
            // gbIn
            // 
            this.gbIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbIn.Controls.Add(this.lblValorIn);
            this.gbIn.Controls.Add(this.lbl_Valor_In);
            this.gbIn.Controls.Add(this.lblComentarioIn);
            this.gbIn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbIn.Location = new System.Drawing.Point(6, 603);
            this.gbIn.Name = "gbIn";
            this.gbIn.Size = new System.Drawing.Size(617, 88);
            this.gbIn.TabIndex = 2;
            this.gbIn.TabStop = false;
            // 
            // lblValorIn
            // 
            this.lblValorIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblValorIn.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblValorIn.Location = new System.Drawing.Point(76, 57);
            this.lblValorIn.Name = "lblValorIn";
            this.lblValorIn.Size = new System.Drawing.Size(403, 19);
            this.lblValorIn.TabIndex = 2;
            this.lblValorIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Valor_In
            // 
            this.lbl_Valor_In.AutoSize = true;
            this.lbl_Valor_In.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Valor_In.Location = new System.Drawing.Point(7, 57);
            this.lbl_Valor_In.Name = "lbl_Valor_In";
            this.lbl_Valor_In.Size = new System.Drawing.Size(52, 19);
            this.lbl_Valor_In.TabIndex = 1;
            this.lbl_Valor_In.Text = "Valor: ";
            // 
            // lblComentarioIn
            // 
            this.lblComentarioIn.AutoSize = true;
            this.lblComentarioIn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentarioIn.Location = new System.Drawing.Point(7, 20);
            this.lblComentarioIn.Name = "lblComentarioIn";
            this.lblComentarioIn.Size = new System.Drawing.Size(0, 19);
            this.lblComentarioIn.TabIndex = 0;
            // 
            // lblInputs
            // 
            this.lblInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInputs.BackColor = System.Drawing.Color.Silver;
            this.lblInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInputs.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblInputs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInputs.Location = new System.Drawing.Point(3, 9);
            this.lblInputs.Name = "lblInputs";
            this.lblInputs.Size = new System.Drawing.Size(620, 23);
            this.lblInputs.TabIndex = 1;
            this.lblInputs.Text = "INPUTS";
            // 
            // dataGridViewIn
            // 
            this.dataGridViewIn.AllowUserToAddRows = false;
            this.dataGridViewIn.AllowUserToDeleteRows = false;
            this.dataGridViewIn.AllowUserToOrderColumns = true;
            this.dataGridViewIn.AllowUserToResizeRows = false;
            this.dataGridViewIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewIn.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewIn.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewIn.ColumnHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewIn.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewIn.EnableHeadersVisualStyles = false;
            this.dataGridViewIn.Location = new System.Drawing.Point(3, 35);
            this.dataGridViewIn.Name = "dataGridViewIn";
            this.dataGridViewIn.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewIn.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewIn.RowHeadersVisible = false;
            this.dataGridViewIn.RowHeadersWidth = 30;
            this.dataGridViewIn.RowTemplate.Height = 30;
            this.dataGridViewIn.RowTemplate.ReadOnly = true;
            this.dataGridViewIn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewIn.Size = new System.Drawing.Size(620, 567);
            this.dataGridViewIn.TabIndex = 0;
            // 
            // lblIO
            // 
            this.lblIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblIO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIO.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblIO.Location = new System.Drawing.Point(10, 7);
            this.lblIO.Name = "lblIO";
            this.lblIO.Size = new System.Drawing.Size(1267, 35);
            this.lblIO.TabIndex = 6;
            this.lblIO.Text = "COMUNICACIÓN";
            // 
            // splitContainerIOs
            // 
            this.splitContainerIOs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerIOs.Location = new System.Drawing.Point(9, 45);
            this.splitContainerIOs.Name = "splitContainerIOs";
            // 
            // splitContainerIOs.Panel1
            // 
            this.splitContainerIOs.Panel1.Controls.Add(this.gbIn);
            this.splitContainerIOs.Panel1.Controls.Add(this.lblInputs);
            this.splitContainerIOs.Panel1.Controls.Add(this.dataGridViewIn);
            // 
            // splitContainerIOs.Panel2
            // 
            this.splitContainerIOs.Panel2.Controls.Add(this.gbOut);
            this.splitContainerIOs.Panel2.Controls.Add(this.lblOutputs);
            this.splitContainerIOs.Panel2.Controls.Add(this.dataGridViewOut);
            this.splitContainerIOs.Size = new System.Drawing.Size(1268, 694);
            this.splitContainerIOs.SplitterDistance = 626;
            this.splitContainerIOs.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 747);
            this.Controls.Add(this.splitContainerIOs);
            this.Controls.Add(this.lblIO);
            this.Name = "Form1";
            this.Text = "Form1";
            this.gbOut.ResumeLayout(false);
            this.gbOut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOut)).EndInit();
            this.gbIn.ResumeLayout(false);
            this.gbIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIn)).EndInit();
            this.splitContainerIOs.Panel1.ResumeLayout(false);
            this.splitContainerIOs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIOs)).EndInit();
            this.splitContainerIOs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOut;
        private System.Windows.Forms.Label lblValorOut;
        private System.Windows.Forms.Label lbl_Valor_Out;
        private System.Windows.Forms.Label lblComentarioOut;
        private System.Windows.Forms.Label lblOutputs;
        private System.Windows.Forms.DataGridView dataGridViewOut;
        private System.Windows.Forms.GroupBox gbIn;
        private System.Windows.Forms.Label lblValorIn;
        private System.Windows.Forms.Label lbl_Valor_In;
        private System.Windows.Forms.Label lblComentarioIn;
        private System.Windows.Forms.Label lblInputs;
        private System.Windows.Forms.DataGridView dataGridViewIn;
        private System.Windows.Forms.Label lblIO;
        private System.Windows.Forms.SplitContainer splitContainerIOs;
    }
}

