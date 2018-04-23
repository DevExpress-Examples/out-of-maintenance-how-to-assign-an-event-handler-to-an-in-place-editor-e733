Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace DoubleClickCell
	''' <summary>
	''' Summary description for Form1.
	''' </summary>
	Public Class Form1
		Inherits System.Windows.Forms.Form
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing

		Public Sub New()
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()

			'
			' TODO: Add any constructor code after InitializeComponent call
			'
		End Sub

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridControl1
			' 
			Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.gridControl1.MainView = Me.gridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(512, 314)
			Me.gridControl1.TabIndex = 1
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
'			Me.gridView1.ShownEditor += New System.EventHandler(Me.gridView1_ShownEditor);
'			Me.gridView1.HiddenEditor += New System.EventHandler(Me.gridView1_HiddenEditor);
'			Me.gridView1.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.gridView1_MouseDown);
			' 
			' Form1
			' 
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.ClientSize = New System.Drawing.Size(512, 314)
			Me.Controls.AddRange(New System.Windows.Forms.Control() { Me.gridControl1})
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread> _
		Shared Sub Main()
			Application.Run(New Form1())
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Dim TempXViewsPrinting As DevExpress.XtraGrid.Design.XViewsPrinting = New DevExpress.XtraGrid.Design.XViewsPrinting(gridControl1)
		End Sub

		Private clickTick As Integer

		Private Sub gridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridView1.MouseDown
			If e.Button <> MouseButtons.Left Then
				Return
			End If
			Dim view As GridView = TryCast(sender, GridView)
			Dim hi As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
			If hi.RowHandle >= 0 Then
				clickTick = System.Environment.TickCount
			Else
				clickTick = 0
			End If
		End Sub

		Private activeEditor As BaseEdit

		Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridView1.ShownEditor
			If (System.Environment.TickCount - clickTick) < SystemInformation.DoubleClickTime Then
				clickTick = 0
				activeEditor = gridView1.ActiveEditor
				AddHandler activeEditor.MouseDown, AddressOf Editor_MouseDown
			End If
		End Sub

		Private Sub gridView1_HiddenEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridView1.HiddenEditor
			If activeEditor IsNot Nothing Then
				RemoveHandler activeEditor.MouseDown, AddressOf Editor_MouseDown
				activeEditor = Nothing
			End If
		End Sub

		Private Sub Editor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
			If e.Button <> MouseButtons.Left Then
				Return
			End If
			If System.Environment.TickCount - clickTick < SystemInformation.DoubleClickTime Then
				DoCellDoubleClick(gridView1, gridView1.FocusedRowHandle, gridView1.FocusedColumn)
			End If
			clickTick = System.Environment.TickCount
		End Sub

		Private Sub DoCellDoubleClick(ByVal view As GridView, ByVal row As Integer, ByVal column As GridColumn)
			MessageBox.Show("Cell Double-Click")
		End Sub
	End Class
End Namespace
