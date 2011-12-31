using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassLikeLanguage
{
	/// <summary>
	/// 失敗したら投げればいいジャマイカ
	/// </summary>
	[Serializable]
	public class GllException : Exception
	{
		public GllException() : base() { }
		public GllException(string message) : base(message) { }
		public GllException(string message, Exception inner) : base(message, inner) { }
		protected GllException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
	/// <summary>
	/// 関数適用なのに引数が見つからないとき、投げつける
	/// </summary>
	[Serializable]
	public class GllArgumentNotFoundException : GllException
	{
		public GllArgumentNotFoundException() : base() { }
		public GllArgumentNotFoundException(string message) : base(message) { }
		public GllArgumentNotFoundException(string message, Exception inner) : base(message, inner) { }
		protected GllArgumentNotFoundException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
	/// <summary>
	/// 関数定義で関数指定がないのに、いきなり引数指定が出てきたら、ぶん投げる
	/// </summary>
	[Serializable]
	public class GllFunctionNotFoundException : GllException
	{
		public GllFunctionNotFoundException() : base() { }
		public GllFunctionNotFoundException(string message) : base(message) { }
		public GllFunctionNotFoundException(string message, Exception inner) : base(message, inner) { }
		protected GllFunctionNotFoundException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
	/// <summary>
	/// 無効なトークンを見つけてしまったら、投げるしかないじゃないか！
	/// </summary>
	[Serializable]
	public class GllDisableTokenException : GllException
	{
		public GllDisableTokenException() : base() { }
		public GllDisableTokenException(string message) : base(message) { }
		public GllDisableTokenException(string message, Exception inner) : base(message, inner) { }
		protected GllDisableTokenException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
