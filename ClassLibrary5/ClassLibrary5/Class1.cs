using Microsoft.Web.WebView2.WinForms;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WebView2Sample
{
    public partial class MainForm : Form
    {
        private WebView2 webView;
        private WebView2 webView2;

        public MainForm()
        {
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            // このアセンブリのフォルダ
            // 実行中のアセンブリの場所を取得
            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            // 実行中のアセンブリのフォルダを取得
            string assemblyFolder = Path.GetDirectoryName(assemblyPath);


            // 上下わけ
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Dock = DockStyle.Fill; // 親コントロール全体に広げる

            // 行の高さの割合を設定 (上の行が50%)
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            this.Controls.Add(tableLayoutPanel); // FormにTableLayoutPanelを追加

            // 透明になる色をピンクとする。これはあくまでも、フォーム層で透明になる設定であり、WebView2とは無関係
            this.BackColor = System.Drawing.Color.Pink;
            this.TransparencyKey = System.Drawing.Color.Pink;

            // １つ目
            webView = new WebView2();
            webView.Dock = DockStyle.Fill; // フォーム全体にWebView2を配置
            webView.DefaultBackgroundColor = System.Drawing.Color.Transparent; // 背景を透明にする
            // Controls.Add(webView);

            tableLayoutPanel.Controls.Add(webView, 0, 0); // 0列目、0行目

            // WebView2の初期化を待つ (重要)
            await webView.EnsureCoreWebView2Async(null);
            // Webページを読み込む
            webView.CoreWebView2.Navigate(Path.Combine(assemblyFolder,"a.html")); // または、ローカルファイルへのパス

            // ２つ目
            webView2 = new WebView2();
            webView2.Dock = DockStyle.Fill; // フォーム全体にWebView2を配置
            webView2.DefaultBackgroundColor = System.Drawing.Color.Transparent; // 背景を透明にする

            tableLayoutPanel.Controls.Add(webView2, 0, 1); // 0列目、1行目

            await webView2.EnsureCoreWebView2Async(null);

            webView2.CoreWebView2.Navigate(Path.Combine(assemblyFolder, "b.html")); // または、ローカルファイルへのパス
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}