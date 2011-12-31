using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GrassLikeLanguage.Editor
{
    /// <summary>
    /// オレオレTextBox : <para></para>
    /// 上流のイベントのうちにイベント実行できる<para></para>
    /// Control, Shift 押している間は下流イベントで文字入力されない
    /// Tab は入力される(Ctrl,Shift除く)
    /// </summary>
	public partial class CustomTextBox : TextBox
	{
		private readonly Dictionary<Keys, Action> _KeyPressEvent;
		public CustomTextBox()
			: base()
		{
			_KeyPressEvent = new Dictionary<Keys, Action>();
		}
		public void AddKeyPressEvent(Keys keyData, Action keyPressEvent)
		{
			if (_KeyPressEvent.ContainsKey(keyData))
				_KeyPressEvent[keyData] += keyPressEvent;
			else
				_KeyPressEvent[keyData] = keyPressEvent;
		}
		protected override bool IsInputKey(Keys keyData)
		{
			foreach (var keyPressEvent in _KeyPressEvent
				.Where(k => k.Key  == keyData)
				.Select(kvp=>kvp.Value))
			{
				keyPressEvent();
			}

			bool isControl = ((keyData & Keys.Control) == Keys.Control);
			if(isControl) return false;
            if ((keyData & Keys.Tab) == Keys.Tab)
                return true;

			return base.IsInputKey(keyData);
		}
	}
}
