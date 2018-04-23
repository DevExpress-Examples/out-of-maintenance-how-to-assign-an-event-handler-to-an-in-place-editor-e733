using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace DoubleClickCell
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gridControl1 = new DevExpress.XtraGrid.GridControl();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControl1
			// 
			this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl1.MainView = this.gridView1;
			this.gridControl1.Name = "gridControl1";
			this.gridControl1.Size = new System.Drawing.Size(512, 314);
			this.gridControl1.TabIndex = 1;
			this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
																										this.gridView1});
			// 
			// gridView1
			// 
			this.gridView1.GridControl = this.gridControl1;
			this.gridView1.Name = "gridView1";
			this.gridView1.ShownEditor += new System.EventHandler(this.gridView1_ShownEditor);
			this.gridView1.HiddenEditor += new System.EventHandler(this.gridView1_HiddenEditor);
			this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(512, 314);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.gridControl1});
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e) {
			new DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1);
		}

		int clickTick;
		
		private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Button != MouseButtons.Left) return;
			GridView view = sender as GridView;
			GridHitInfo hi = view.CalcHitInfo(new Point(e.X, e.Y));
			if(hi.RowHandle >= 0)
				clickTick = System.Environment.TickCount;
			else 
				clickTick = 0;
		}

		BaseEdit activeEditor;

		private void gridView1_ShownEditor(object sender, System.EventArgs e) {
			if((System.Environment.TickCount - clickTick) < SystemInformation.DoubleClickTime) {
				clickTick = 0;
				activeEditor = gridView1.ActiveEditor;
				activeEditor.MouseDown += new MouseEventHandler(Editor_MouseDown);
			}
		}

		private void gridView1_HiddenEditor(object sender, System.EventArgs e) {
			if(activeEditor != null) {
				activeEditor.MouseDown -= new MouseEventHandler(Editor_MouseDown);
				activeEditor = null;
			}
		}

		void Editor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(e.Button != MouseButtons.Left) return;
			if(System.Environment.TickCount - clickTick < SystemInformation.DoubleClickTime) {
				DoCellDoubleClick(gridView1, gridView1.FocusedRowHandle, gridView1.FocusedColumn);
			}
			clickTick = System.Environment.TickCount;
		}

		void DoCellDoubleClick(GridView view, int row, GridColumn column) {
			MessageBox.Show("Cell Double-Click");
		}
	}
}
