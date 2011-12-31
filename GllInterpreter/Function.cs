using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrassLikeLanguage.Util;

namespace GrassLikeLanguage.Function
{
	#region + Sentence
	/// <summary>
	/// UCWとLCWのカウント
	/// <para></para>関数の定義や適用に使用する
	/// </summary>
	public struct Sentence
	{
		public int CountUpperCaseW;//初期値0
		public int CountLowerCaseW;//初期値0
		public Sentence(Sentence sentence)
		{
			CountLowerCaseW = sentence.CountLowerCaseW;
			CountUpperCaseW = sentence.CountUpperCaseW;
		}
		public void Reset()
		{
			CountUpperCaseW = CountLowerCaseW = 0;
		}
		public override string ToString()
		{
			return string.Format("W:{0},w:{1}", CountUpperCaseW, CountLowerCaseW);
		}
	}
	#endregion

	/// <summary>
	/// Grassの関数を表現する抽象クラス
	/// </summary>
	public abstract class GrassFunc
	{
		/// <summary>
		/// 文字コードの個数、256
		/// </summary>
		public const int MAX_CHAR_LENGTH = byte.MaxValue + 1;

		/// <summary>
		/// グローバル関数スタック
		/// </summary>
		protected readonly Stack<GrassFunc> _FuncStack;
		/// <summary>
		/// この関数の引数の個数
		/// </summary>
		protected readonly int _ArgNumberMax;
		/// <summary>
		/// 現在の指定している引数番号
		/// </summary>
		protected readonly int _CurrentArgNumber;

		/// <summary>
		/// 値
		/// <para></para>入出力などに使用
		/// </summary>
		public virtual int Value
		{
			get { throw new NotImplementedException(); }
			protected set { throw new NotImplementedException(); }
		}

		public GrassFunc(int argNumberMax = 1, int currentArgNumber = 0)
		{
			_ArgNumberMax = argNumberMax;
			_CurrentArgNumber = currentArgNumber;
			_FuncStack = new Stack<GrassFunc>();
		}

		/// <summary>
		/// 関数の適用
		/// <para></para>引数が十分なときは実行し、
		/// <para></para>引数が足りないときはカリー化する
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		public GrassFunc Apply(GrassFunc arg)
		{
			if (_CurrentArgNumber + 1 == _ArgNumberMax)
			{
				return Perform(arg);
			}
			else
			{
				return Curry(arg);
			}
		}

		/// <summary>
		/// 関数をスタックにプッシュ
		/// </summary>
		/// <param name="func"></param>
		public void Push(GrassFunc func)
		{
			_FuncStack.Push(func);
		}

		/// <summary>
		/// 関数のカリー化
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected virtual GrassFunc Curry(GrassFunc arg)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 関数の実行
		/// </summary>
		/// <param name="func"></param>
		/// <returns></returns>
		protected abstract GrassFunc Perform(GrassFunc arg);

		/// <summary>
		/// スタックに<paramref name="stack"/>を追加する
		/// </summary>
		/// <param name="stack"></param>
		public void CopyFuncStack(Stack<GrassFunc> stack)
		{
			_FuncStack.PushRange(stack);
		}

		/// <summary>
		/// 関数の深いコピー
		/// </summary>
		/// <returns></returns>
		public abstract GrassFunc Clone();
	}

	#region GrassTrue,False
	/// <summary>
	/// ２引数取り、引数１か引数２を返す関数(bool関数)
	/// </summary>
	public abstract class GrassProjection2 : GrassFunc
	{
		protected readonly bool _Which;
		protected GrassProjection2(bool which, int currentArgNumber = 0)
			: base(2, currentArgNumber)
		{
			_Which = which;
		}
		/// <summary>
		/// 条件<paramref name="which"/>によって、<see cref="GrassTrue"/>か<see cref="GrassFalse"/>を返す
		/// </summary>
		/// <param name="which"></param>
		/// <param name="currentArgNumber"></param>
		/// <returns></returns>
		public static GrassProjection2 Condition(bool which, int currentArgNumber = 0)
		{
			return (which) ?
				new GrassTrue(currentArgNumber) as GrassProjection2 :
				new GrassFalse(currentArgNumber) as GrassProjection2;
		}
		public override GrassFunc Clone()
		{
			GrassFunc newFunc = Condition(_Which, _CurrentArgNumber);
			newFunc.CopyFuncStack(_FuncStack);
			return newFunc;
		}
		protected override abstract GrassFunc Perform(GrassFunc arg);
	}
	/// <summary>
	/// True関数
	/// </summary>
	public sealed class GrassTrue : GrassProjection2
	{
		public GrassTrue(int currentArgNumber)
			: base(true, currentArgNumber)
		{
		}
		protected override GrassFunc Curry(GrassFunc arg)
		{
			GrassProjection2 newFunc = new GrassTrue(_CurrentArgNumber + 1);
			newFunc.CopyFuncStack(_FuncStack);
			newFunc.Push(arg);
			return newFunc;
		}
		/// <summary>
		/// Trueなので引数１(スタック先頭)を常に返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			return _FuncStack.Peek();
		}
		public override string ToString()
		{
			return "True";
		}
	}
	/// <summary>
	/// False関数
	/// </summary>
	public sealed class GrassFalse : GrassProjection2
	{
		public GrassFalse(int currentArgNumber)
			: base(false, currentArgNumber)
		{
		}
		protected override GrassFunc Curry(GrassFunc arg)
		{
			GrassProjection2 newFunc = new GrassFalse(_CurrentArgNumber + 1);
			newFunc.CopyFuncStack(_FuncStack);
			newFunc.Push(arg);
			return newFunc;
		}
		/// <summary>
		/// Falseなので引数２(<paramref name="arg"/>)を常に返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			return arg;
		}
		public override string ToString()
		{
			return "False";
		}
	}
	#endregion

	#region 基本関数
	/// <summary>
	/// 文字関数
	/// </summary>
	public class GrassChar : GrassFunc
	{
		/// <summary>
		/// 値(文字コード)
		/// </summary>
		public override int Value
		{
			get;
			protected set;
		}
		public GrassChar(int value)
			: base(1)
		{
			Value = value;
		}
		/// <summary>
		/// 実行
		/// <para></para>引数の値(文字コード)が同じならTrue関数、違うならFalse関数を返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			return GrassProjection2.Condition(Value == arg.Value);
		}
		public override GrassFunc Clone()
		{
			return new GrassChar(Value);
		}
		public override string ToString()
		{
			return string.Format("'{0}'", (char)Value);
		}
	}
	/// <summary>
	/// 入力関数 
	/// <seealso cref="Parameter.In"/>
	/// </summary>
	public class In : GrassFunc
	{
		public In()
			: base(1)
		{
		}
		/// <summary>
		/// 実行
		/// <para></para>標準入力から１文字受け取り、正常な文字ならその文字の関数、異常なら引数をそのまま返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			char ch = (char)Console.Read();
			return char.IsLetter(ch) ? new GrassChar(ch) : arg;
		}
		public override GrassFunc Clone()
		{
			return new In();
		}
		public override string ToString()
		{
			return "In";
		}
	}
	/// <summary>
	/// 出力関数 
	/// <seealso cref="Parameter.Out"/>
	/// </summary>
	public class Out : GrassFunc
	{
		public Out()
			: base(1)
		{
		}
		/// <summary>
		/// 実行
		/// <para></para>引数の値(文字コード)の文字を標準出力し、引数をそのまま返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			Console.Write((char)arg.Value);
			return arg;
		}
		public override GrassFunc Clone()
		{
			return new Out();
		}
		public override string ToString()
		{
			return "Out";
		}
	}
	/// <summary>
	/// 後者関数
	/// </summary>
	public class Succ : GrassFunc
	{
		public Succ()
			: base(1)
		{
		}
		/// <summary>
		/// 実行
		/// <para></para>引数の値の次(mod <see cref="MAX_CHAR_LENGTH"/>)の文字コードとなる文字関数を返す
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			return new GrassChar(
				(arg.Value + 1) % MAX_CHAR_LENGTH
				);
		}
		public override GrassFunc Clone()
		{
			return new Succ();
		}
		public override string ToString()
		{
			return "Succ";
		}
	}
	#endregion

	/// <summary>
	/// プログラマが定義する関数
	/// </summary>
	public class GrassMyFunc : GrassFunc
	{
		protected readonly List<Sentence> _SentenceList = new List<Sentence>();
		public GrassMyFunc(int argNumberMax)
			: base(argNumberMax)
		{
		}
		protected GrassMyFunc(int argNumberMax, int currentArgNumber, List<Sentence> sourceSentenceList)
			: base(argNumberMax, currentArgNumber)
		{
			_SentenceList.AddRange(
				sourceSentenceList
				);
		}
		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="arg"></param>
		/// <returns></returns>
		protected override GrassFunc Perform(GrassFunc arg)
		{
			//追加のローカルスタック
			Stack<GrassFunc> localStack = new Stack<GrassFunc>();
			localStack.Push(arg);
			foreach (var tuple in _SentenceList.Select((sentence, index) => new { sentence, index }))
			{
				if (tuple.sentence.CountUpperCaseW > tuple.index + 1 + _FuncStack.Count ||
					tuple.sentence.CountLowerCaseW > tuple.index + 1 + _FuncStack.Count)
				{
					System.Diagnostics.Debug.WriteLine("スタック内関数の指定は正常？？");
				}
				GrassFunc funcApp = (tuple.sentence.CountUpperCaseW > tuple.index + 1) ?
					_FuncStack.Get(tuple.sentence.CountUpperCaseW - tuple.index - 2) :
					localStack.Get(tuple.sentence.CountUpperCaseW - 1);
				GrassFunc argApp = (tuple.sentence.CountLowerCaseW > tuple.index + 1) ?
					_FuncStack.Get(tuple.sentence.CountLowerCaseW - tuple.index - 2) :
					localStack.Get(tuple.sentence.CountLowerCaseW - 1);
				//GrassFunc func2 = (funcApp == argApp) ? funcApp.Clone() : funcApp;
				//localStack.Push(func2.Apply(argApp));
				localStack.Push(funcApp.Apply(argApp));
			}
			return localStack.Peek();
		}
		public override GrassFunc Clone()
		{
			GrassFunc newFunc = new GrassMyFunc(_ArgNumberMax, _CurrentArgNumber, _SentenceList);
			newFunc.CopyFuncStack(_FuncStack);
			return newFunc;
		}
		protected override GrassFunc Curry(GrassFunc func)
		{
			GrassMyFunc newFunc = new GrassMyFunc(_ArgNumberMax, _CurrentArgNumber + 1, _SentenceList);
			newFunc.CopyFuncStack(_FuncStack);
			newFunc.Push(func);
			return newFunc;
		}
		public void AddSentence(Sentence sentence)
		{
			_SentenceList.Add(
				sentence
				);
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(" { ");
			foreach (var s in _SentenceList)
			{
				sb.Append(string.Format("[{0}], ", s));
			}
			if (_SentenceList.Any()) sb.Remove(sb.Length - 2, 2);
			sb.Append(" }");
			for (int i = 0; i < _ArgNumberMax; i++)
			{
				sb.Append("(");
				if (i < _CurrentArgNumber) sb.Append(string.Format("{0}", _FuncStack.Get(i)));
				sb.Append(")");
			}
			sb.Append(" ");
			return sb.ToString();
		}
	}
}
