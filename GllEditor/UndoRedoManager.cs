using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassLikeLanguage.Editor.Util
{
	/// <summary>
	/// UndoRedoを定義する
	/// </summary>
	public class Command
	{
		/// <summary>最初の実行時の処理</summary>
		public Action Do;
		/// <summary>Undo時の処理</summary>
		public Action Undo;
		/// <summary>Redo時の処理</summary>
		public Action Redo;
		public Command(Action doing, Action undo, Action redo)
		{
			Do = doing;
			Undo = undo;
			Redo = redo;
		}
	}

	/// <summary>
	/// Undo, Redo時に自動でスタック間を調整する基底クラス
	/// </summary>
    public abstract class BaseUndoRedoManager
    {
        #region フィールド
        /// <summary>
        /// Undo用スタック
        /// </summary>
        protected LinkedList<Command> mUndoStack;
        /// <summary>
        /// Redo用スタック
        /// </summary>
        protected LinkedList<Command> mRedoStack;

        /// <summary>
        /// Undoスタックに格納されている要素数
        /// </summary>
        public int UndoCount
        {
            get { return mUndoStack.Count; }
        }
        /// <summary>
        /// Redoスタックに格納されている要素数
        /// </summary>
        public int RedoCount
        {
            get { return mRedoStack.Count; }
        }
        #endregion

        #region Push / Pop
		protected static void Push<T>(ref LinkedList<T> source, T value)
		{
			source.AddFirst(value);
		}
		protected static T Pop<T>(ref LinkedList<T> source)
		{
			T result = source.First();
			source.RemoveFirst();
			return result;
		}
		#endregion

		#region Do / Undo / Redo
		public virtual void Do(Command command)
		{
			Push(ref mUndoStack, command);
			command.Do();
			mRedoStack.Clear();
		}
		public virtual void Undo()
		{
			if (UndoCount > 0)
			{
				var command = Pop(ref mUndoStack);
				command.Undo();
				Push(ref mRedoStack, command);
			}
		}
		public virtual void Redo()
		{
			if (RedoCount > 0)
			{
				var command = Pop(ref mRedoStack);
				command.Redo();
				Push(ref mUndoStack, command);
			}
		}
        #endregion

        #region {Undo/Redo/All}Clear
        /// <summary>
        /// Undoスタックを空にする
        /// </summary>
        public void ClearUndoStack()
        {
            mUndoStack.Clear();
        }
        /// <summary>
        /// Redoスタックを空にする
        /// </summary>
        public void ClearRedoStack()
        {
            mRedoStack.Clear();
        }
        /// <summary>
        /// Undo, Redoスタックを空にする
        /// </summary>
        public void ClearAllStack()
        {
            mUndoStack.Clear();
            mRedoStack.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Undo, Redo時に自動でスタック間を調整するクラス
    /// </summary>
    public sealed class UndoRedoManager : BaseUndoRedoManager
    {
        public UndoRedoManager()
        {
            mUndoStack = new LinkedList<Command>();
            mRedoStack = new LinkedList<Command>();
        }
    }

    /// <summary>
    /// Undo, Redo時に自動でスタック間を調整するクラス
    /// <para>固定サイズ、オーバーした場合は古い方から削除される</para>
    /// </summary>
    public sealed class FixedSizeUndoRedoManager : BaseUndoRedoManager
    {
        /// <summary>
		/// 最大スタックサイズ
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Undo, Redo時に自動でスタック間を調整するクラス
        /// <para>固定サイズ</para>
        /// </summary>
        /// <param name="capacity">指定する固定長</param>
        public FixedSizeUndoRedoManager(int capacity)
        {
			mUndoStack = new LinkedList<Command>();
            mRedoStack = new LinkedList<Command>();
            Capacity = capacity;
        }
        /// <summary>
        /// 実行<para></para>スタックサイズを超えた要素は古い順に削除する
        /// </summary>
        /// <param name="command"></param>
		public override void Do(Command command)
		{
			base.Do(command);
			while (UndoCount > Capacity)
			{
				mUndoStack.RemoveLast();//あふれた要素を古い順に削除
			}
		}
    }
}
