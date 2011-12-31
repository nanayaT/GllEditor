using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrassLikeLanguage.Editor.Util;
using GrassLikeLanguage.Rule;

namespace GrassLikeLanguage.Editor
{
    public partial class FormMain : Form
	{
		#region 変数、プロパティ
		/// <summary>カーソル位置とテキスト</summary>
        private class CursorAndText : IEquatable<CursorAndText>
        {
            public int CursorPosition;
            public string Text;
            public override string ToString()
            {
                return string.Format("Cursor:{0},Text:\"{1}\"", CursorPosition, Text);
            }
            public bool Equals(CursorAndText other)
            {
                return this.CursorPosition == other.CursorPosition && this.Text == other.Text;
            }
        }

        /// <summary>Undoの記録タイマーのイベント間隔</summary>
        private const int _TimerInterval = 1500;
        /// <summary>ファイル拡張子とルールの対応表</summary>
		private static readonly Dictionary<string, BaseRule> _FileExtensionToRule = new Dictionary<string, BaseRule>(){
			{".grass", new RuleGrass()},
			{".homehome", new RuleHomuHomu()},
			{".txt", new RuleGrass()},
		};
        /// <summary>非同期処理時の同期用</summary>
        private static readonly object _SyncObjForm = new object();

		/// <summary>現在システム内のイベントでテキストを変更しているか</summary>
		private bool _IsSourceCodeChangedFromSystem = false;
		/// <summary>現在システム内のイベントでテキストを変更しているか</summary>
		private bool _IsVisualizedLanguageChangedFromSystem = false;
		private bool _IsCalculating = false;
		private Cursor _DefaultCursor;
		/// <summary>
		/// 変換中or実行中ならtrue<para></para>
		/// trueならマウスカーソルを"wait"に変更
		/// </summary>
		private bool IsCalculating
		{
			get { return _IsCalculating; }
			set
			{
				_IsCalculating = value;
				var act = new Action(() =>
				{
					lock (_SyncObjForm)
					{
						if (_IsCalculating) Cursor.Current = Cursors.WaitCursor;
						else Cursor.Current = _DefaultCursor;
					}
				});
				this.BeginInvoke(act);
			}
		}

		/// <summary>テキストボックスから対応する条件を設定</summary>
		private Dictionary<TextBox, Action<bool>> _SettingTextChangedFromSystem;
		/// <summary>テキストボックスから対応する条件を取得</summary>
		private Dictionary<TextBox, Func<bool>> _GettingTextChangedFromSystem;

        /// <summary>最新にアクティブになったテキストボックス</summary>
        private TextBox _ActiveTextBox;
        /// <summary>ソースコードの直前のカーソル位置とテキストデータ</summary>
        private CursorAndText _PreviousCursorAndTextSourceCode;
        /// <summary>可視化言語の直前のカーソル位置とテキストデータ</summary>
        private CursorAndText _PreviousCursorAndTextVisualized;
        /// <summary>ソースコードのUndo/Redo管理者</summary>
        private readonly BaseUndoRedoManager _SourceManager;
        /// <summary>可視化言語のUndo/Redo管理者</summary>
        private readonly BaseUndoRedoManager _VisualizedManager;
        /// <summary>テキストボックスとUndo/Redo管理者の対応表</summary>
        private readonly Dictionary<TextBox, BaseUndoRedoManager> _TextBoxToManager;
		/// <summary>文字列とトークンの変換ルール</summary>
		private BaseRule _Rule;
        /// <summary>ソースコードのUndo記録タイマー</summary>
        private readonly Timer _TimerSaveSourceState;
        /// <summary>可視化言語のUndo記録タイマー</summary>
        private readonly Timer _TimerSaveVisualizedState;
		#endregion

		public FormMain()
        {
            InitializeComponent();
			_DefaultCursor = Cursor.Current;
			_Rule = new RuleGrass();
            _PreviousCursorAndTextSourceCode = new CursorAndText() { CursorPosition = 0, Text = string.Empty };
            _PreviousCursorAndTextVisualized = new CursorAndText() { CursorPosition = 0, Text = string.Empty };
            _SourceManager = new FixedSizeUndoRedoManager(100);
            _VisualizedManager = new FixedSizeUndoRedoManager(100);
            _TextBoxToManager = new Dictionary<TextBox, BaseUndoRedoManager>();
			_TextBoxToManager[textBoxSourceCode] = _SourceManager;
			_TextBoxToManager[textBoxVisualizedLanguage] = _VisualizedManager;
			_SettingTextChangedFromSystem = new Dictionary<TextBox, Action<bool>>();
			_SettingTextChangedFromSystem[textBoxSourceCode] = (value) => _IsSourceCodeChangedFromSystem = value;
			_SettingTextChangedFromSystem[textBoxVisualizedLanguage] = (value) => _IsVisualizedLanguageChangedFromSystem = value;
			_GettingTextChangedFromSystem = new Dictionary<TextBox, Func<bool>>();
			_GettingTextChangedFromSystem[textBoxSourceCode] = () => _IsSourceCodeChangedFromSystem;
			_GettingTextChangedFromSystem[textBoxVisualizedLanguage] = () => _IsVisualizedLanguageChangedFromSystem;

            //Ctrl+Tabでソースコード<=>可視化言語を変換する
            textBoxSourceCode.AddKeyPressEvent(
                (Keys.Control | Keys.Tab),
                () =>
                {
					GllSourceToVisualized();
                }
            );
            textBoxVisualizedLanguage.AddKeyPressEvent(
                (Keys.Control | Keys.Tab),
                () =>
                {
					VisualizedToGllSource();
                }
            );

            //タイマーでUndo記録を管理
            _TimerSaveSourceState = new Timer();
            _TimerSaveSourceState.Interval = _TimerInterval;
            _TimerSaveSourceState.Tick += (sender, e) =>
            {
                var breakCondition = new Func<CursorAndText,bool>(
                    (pre)=> pre.Text == textBoxSourceCode.Text ||_GettingTextChangedFromSystem[textBoxSourceCode]());
                lock (_SyncObjForm)
                {
                    //Undo,Redoの挙動を記録
					SaveCursorAndTextOnTextBox(ref _PreviousCursorAndTextSourceCode, textBoxSourceCode, breakCondition);
                }
            };
            _TimerSaveVisualizedState = new Timer();
            _TimerSaveVisualizedState.Interval = _TimerInterval;
            _TimerSaveVisualizedState.Tick += (sender, e) =>
            {
                var breakCondition = new Func<CursorAndText, bool>(
                    (pre) => pre.Text == textBoxVisualizedLanguage.Text || _GettingTextChangedFromSystem[textBoxVisualizedLanguage]());
                lock (_SyncObjForm)
                {
                    //Undo,Redoの挙動を記録
					SaveCursorAndTextOnTextBox(ref _PreviousCursorAndTextVisualized, textBoxVisualizedLanguage, breakCondition);
                }
            };
            _TimerSaveSourceState.Start();
            _TimerSaveVisualizedState.Start();
		}

		#region GrassLikeLanguage <=> VisualizedLanguage
		/// <summary>GLLを可視化言語に変換し表示する</summary>
		private void GllSourceToVisualized()
		{
			if (IsCalculating) return;
            string gllSource;
            //テキストボックスからソースコードを文字列として取得
			lock (_SyncObjForm)
			{
				gllSource = (textBoxSourceCode.SelectionLength > 0) ?
					textBoxSourceCode.SelectedText :
					textBoxSourceCode.Text;
				IsCalculating = true;
			}
            var task = Task.Factory.StartNew<string>(() =>
            {
                try
                {
                    //可視化言語に変換
                    return Visualization.VisualizedLanguage.GllToVisualized(gllSource, _Rule);
                }
                catch (GllException e)
                {
                    MessageBox.Show(e.Message);
                    return string.Empty;
                }
            });
            task.ContinueWith(t =>
            {
                var ilText = t.Result;
                var act = new Action(() =>
                {
                    //可視化言語をテキストボックスに出力
					lock (_SyncObjForm)
					{
						textBoxVisualizedLanguage.Text = ilText;
						IsCalculating = false;
					}
                });
                this.BeginInvoke(act);
            });
		}
		/// <summary>可視化言語をGLLに変換し表示する</summary>
		private void VisualizedToGllSource()
		{
			if (IsCalculating) return;
			string ilSource;
            //テキストボックスから可視化言語を文字列として取得
			lock (_SyncObjForm)
			{
				ilSource = (textBoxVisualizedLanguage.SelectionLength > 0) ?
					textBoxVisualizedLanguage.SelectedText :
					textBoxVisualizedLanguage.Text;
				IsCalculating = true;
			}
            var task = Task.Factory.StartNew<string>(() =>
            {
                try
                {
                    //ソースコードに変換
                    return Visualization.VisualizedLanguage.VisualizedToGll(ilSource, _Rule);
                }
                catch (GllException e)
                {
                    MessageBox.Show(e.Message);
                    return string.Empty;
                }
            });
			task.ContinueWith(t =>
			{
				var gllText = t.Result;
				var act = new Action(() =>
				{
					//ソースコードをテキストボックスに出力
					lock (_SyncObjForm)
					{
						textBoxSourceCode.Text = gllText;
						IsCalculating = false;
					}
				});
				this.BeginInvoke(act);
			});
		}
		#endregion

		#region カーソル位置とテキストを保持
		/// <summary>
		/// カーソル位置とテキストをUndo/Redoに保持する
		/// </summary>
		/// <param name="previous"></param>
		/// <param name="target"></param>
        private void SaveCursorAndTextOnTextBox(ref CursorAndText previous, TextBox target, Func<CursorAndText, bool> breakCondition = null)
        {
			BaseUndoRedoManager manager;
			lock (_SyncObjForm)
			{
				manager = _TextBoxToManager[target];
			}
            CursorAndText _previous;
			CursorAndText current;
			lock (_SyncObjForm)
			{
				_previous = new CursorAndText()
				{
					CursorPosition = previous.CursorPosition,
					Text = previous.Text
				};
				current = new CursorAndText()
				{
					CursorPosition = target.SelectionStart,
					Text = target.Text
				};
				//旧情報の書き換え
				previous = current;
                if (breakCondition != null)
                {
                    if(breakCondition(_previous))
                        return;
                }
			}
            manager.Do(new Command(
                () =>//Doした場合(=今)の処理
                {
                },
                () =>//Undoした場合の処理
				{
                    lock (_SyncObjForm)
                    {
						target.Text = _previous.Text;
						target.SelectionStart = _previous.CursorPosition;
                    }
                },
                () =>//Redoした場合の処理
                {
					lock (_SyncObjForm)
                    {
						target.Text = current.Text;
						target.SelectionStart = current.CursorPosition;
                    }
                }));
        }
		#endregion

		#region Undo/Redo
		/// <summary>Undoの挙動</summary>
        private void Undo()
        {
            _SettingTextChangedFromSystem[_ActiveTextBox](true);//システム側の編集状態に
            _TextBoxToManager[_ActiveTextBox].Undo();//最新に選択されたテキストボックスの方のUndoを実行
        }
        /// <summary>Redoの挙動</summary>
        private void Redo()
        {
			_SettingTextChangedFromSystem[_ActiveTextBox](true);//システム側の編集状態に
            _TextBoxToManager[_ActiveTextBox].Redo();//最新に選択されたテキストボックスの方のRedoを実行
        }
		#endregion

		#region ソースファイルを開く
		/// <summary>
        /// ソースファイルを開き、テクストボックスに内容を貼り付ける<para></para>
        /// ついでに拡張子からルールを変更する
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenSource(string fileName)
        {
            var taskReadSource = Task.Factory.StartNew(() =>
            {
                var fileExt = Path.GetExtension(fileName);
                if (_FileExtensionToRule.Any(kvp =>
                    string.Compare(kvp.Key, fileExt, true) == 0))
                {
                    lock(_SyncObjForm) _Rule = _FileExtensionToRule[fileExt];
                }
                else
                {
                    lock (_SyncObjForm) _Rule = new RuleGrass();
                }
                var source = File.ReadAllText(fileName, Encoding.UTF8);//とりあえずUTF8オンリーで
                return source;
            });
            taskReadSource.ContinueWith(t =>
            {
                var source = t.Result;
                var action = new Action(() =>
                {
                    lock (_SyncObjForm)
                    {
                        try
                        {
                            _TimerSaveSourceState.Stop();
                            _TimerSaveVisualizedState.Stop();
                            textBoxSourceCode.Text = source;
                            if (_Rule is RuleGrass)
                            {
                                grassToolStripMenuItem.CheckState = CheckState.Checked;
                            }
                            else if (_Rule is RuleHomuHomu)
                            {
                                ほむほむToolStripMenuItem.CheckState = CheckState.Checked;
                            }
                            _IsSourceCodeChangedFromSystem = false;
                            _IsVisualizedLanguageChangedFromSystem = false;
                            //Undo,Redoの挙動を記録
                            SaveCursorAndTextOnTextBox(ref _PreviousCursorAndTextVisualized, textBoxVisualizedLanguage);
                        }
                        finally
                        {
                            _TimerSaveSourceState.Start();
                            _TimerSaveVisualizedState.Start();
                        }
                    }
                });
                this.BeginInvoke(action);
            });
        }
		#endregion

		#region static
		/// <summary>
		/// コマンドプロンプトを立ち上げ、Grass風言語を実行する
		/// </summary>
		/// <param name="source"></param>
		/// <param name="rule"></param>
		/// <returns></returns>
		private static void GllRun(string source, BaseRule rule)
		{
            string option;
            if (rule is RuleGrass)
                option = "-g";
            else if (rule is RuleHomuHomu)
                option = "-h";
            else
                option = "-g";
            var args = (option + " -e " + source).Split(' ');
            Interpreter.Interpreter.Run(args);
		}
		/// <summary>
		/// 入力文字(0-9, a-zA-Z, +-*, ' ', '\t')ならTrue, 
		/// </summary>
		/// <param name="keyChar"></param>
		/// <returns></returns>
		private static bool IsKeyCharEnable(char keyChar)
		{
			return char.IsLetterOrDigit(keyChar)
				|| char.IsSymbol(keyChar)
				|| keyChar == ' '
				|| keyChar == '\t';
		}
		#endregion

		#region Event
		#region メニューボタン
		private void 新規作成NToolStripButton_Click(object sender, EventArgs e)
		{
			textBoxSourceCode.Text = string.Empty;
			textBoxVisualizedLanguage.Text = string.Empty;
		}
		private void 開くOToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialogSource.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = openFileDialogSource.FileName;
                OpenSource(fileName);
            }
        }
        private void 名前を付けて保存SToolStripButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSource.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = saveFileDialogSource.FileName;
				var source = textBoxSourceCode.Text;
				File.WriteAllText(fileName, source);
            }
        }
        private void 切り取りUToolStripButton_Click(object sender, EventArgs e)
        {
            _ActiveTextBox.Cut();
        }
        private void コピーCToolStripButton_Click(object sender, EventArgs e)
        {
            _ActiveTextBox.Copy();
        }
        private void 貼り付けPToolStripButton_Click(object sender, EventArgs e)
        {
            _ActiveTextBox.Paste();
        }
		private void ヘルプLToolStripButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(
				"Grass Like Language Editor\nver1.0",
				"Grass Like Language Editor",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information,
				MessageBoxDefaultButton.Button1);
		}
		private void sourceToVisualizedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GllSourceToVisualized();
		}
		private void visualizedToSourceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VisualizedToGllSource();
		}
		private void runToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (IsCalculating) return;
            try
            {
                Console.WriteLine("==============================");
                _TimerSaveSourceState.Stop();
                _TimerSaveVisualizedState.Stop();
				IsCalculating = true;
                string source;
                if (textBoxSourceCode.SelectionLength > 0)
                {
                    source = textBoxSourceCode.SelectedText;
                }
                else
                {
                    source = textBoxSourceCode.Text;
                }
                GllRun(source, _Rule);
            }
            finally
            {
                Console.WriteLine("\n==============================");
                customTextBoxPrintStack.Text = Runner.StackPrinted;
				IsCalculating = false;
                _TimerSaveSourceState.Start();
                _TimerSaveVisualizedState.Start();
            }
		}
		private void grassToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_Rule = new RuleGrass();
		}
		private void ほむほむToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_Rule = new RuleHomuHomu();
		}
		private void grassToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
		{
			if (grassToolStripMenuItem.CheckState == CheckState.Unchecked) return;
			foreach (var item in toolStripDropDownButtonRule.DropDownItems
				.Cast<ToolStripMenuItem>()
				.Where(item => item != grassToolStripMenuItem))
			{
				item.CheckState = CheckState.Unchecked;
			}
		}
		private void ほむほむToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
		{
			if (ほむほむToolStripMenuItem.CheckState == CheckState.Unchecked) return;
			foreach (var item in toolStripDropDownButtonRule.DropDownItems
				.Cast<ToolStripMenuItem>()
				.Where(item => item != ほむほむToolStripMenuItem))
			{
				item.CheckState = CheckState.Unchecked;
			}
		}
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("フォームを終了します", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				this.Close();
			}
		}
		#endregion
		
		#region フォーカス変更
		private void textBoxSourceCode_Enter(object sender, EventArgs e)
        {
            _ActiveTextBox = textBoxSourceCode;
        }
        private void textBoxVisualizedLanguage_Enter(object sender, EventArgs e)
        {
            _ActiveTextBox = textBoxVisualizedLanguage;
        }
		#endregion

		#region テキスト変更
		private void textBoxSourceCode_TextChanged(object sender, EventArgs e)
        {
			//タイマーの時間をリセット
			_TimerSaveSourceState.Stop();
			_TimerSaveSourceState.Start();
		}
		private void textBoxVisualizedLanguage_TextChanged(object sender, EventArgs e)
		{
			//タイマーの時間をリセット
			_TimerSaveVisualizedState.Stop();
			_TimerSaveVisualizedState.Start();
		}
		#endregion

		#region キーボード
		/// <summary>
        /// テキストボックス共通のキーボードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            FormMain_KeyUp(sender, e);
        }
        /// <summary>
        /// フォーム全体のキーボードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void FormMain_KeyUp(object sender, KeyEventArgs e)
		{
            switch (e.KeyData)
            {
                case (Keys.Control | Keys.Z):
                    {
                        Undo();
                    } break;
                case (Keys.Control | Keys.Shift | Keys.Z):
                case (Keys.Control | Keys.Y):
                    {
                        Redo();
                    } break;
                default:
                    break;
            }
		}
		/// <summary>
		/// 押したキーの判定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxSourceCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (IsKeyCharEnable(e.KeyChar))
			{
				lock (_SyncObjForm) _IsSourceCodeChangedFromSystem = false;
			}
		}
		/// <summary>
		/// 押したキーの判定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxVisualizedLanguage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (IsKeyCharEnable(e.KeyChar))
			{
				lock (_SyncObjForm) _IsVisualizedLanguageChangedFromSystem = false;
			}
		}
        #endregion

        #region ドラッグドロップ
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileNames == null || fileNames.Length <= 0) { return; }
            OpenSource(fileNames[0]);
        }
        #endregion

		#region フォーム起動/終了
		/// <summary>
		/// フォーム起動時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormMain_Load(object sender, EventArgs e)
		{
			this.Activate();
		}
		/// <summary>
		/// フォーム終了時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			//タイマー停止
			var timers = new[] { _TimerSaveSourceState, _TimerSaveVisualizedState };
			foreach (var timer in timers)
			{
				if (timer != null)
				{
					timer.Stop();
					timer.Dispose();
				}
			}
		}
		#endregion
		#endregion
	}
}
