using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GrassLikeLanguage.Rule
{
	#region + enum Token
	/// <summary>
	/// Grass用の文字トークン
	/// </summary>
	public enum Token
	{
		/// <summary>無効値、デフォルト</summary>
		None,
		/// <summary>"w" : LowerCaseW</summary>
		LCW,
		/// <summary>"W" : UpperCaseW</summary>
		UCW,
		/// <summary>"v"</summary>
		V,
		/// <summary>EndOfString、終端用トークン</summary>
		EOS
	}
	#endregion

	#region + TokenWithCount
	/// <summary>
	/// 連続するトークンとその個数
	/// </summary>
	public struct TokenWithCount
	{
		/// <summary>
		/// トークン種類
		/// </summary>
		public readonly Token Token;
		/// <summary>
		/// 連続個数
		/// </summary>
		public readonly int Count;
		public TokenWithCount(Token token, int count = 1)
		{
			Token = token;
			Count = count;
		}

		#region override
		public override string ToString()
		{
			return string.Format("Token={0},Count={1}", Token, Count);
		}
		public override bool Equals(object obj)
		{
			var other = (TokenWithCount)obj;
			return this.Token == other.Token && this.Count == other.Count;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Token.GetHashCode() ^ Count.GetHashCode();
		}
		#endregion

		/// <summary>
		/// トークンのリストから、連続トークンをまとめたリストに変換する
		/// <example>{A, A, B, B, B, C} => {[A,2], [B,3], [C,1]}</example>
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="endIndex"></param>
		/// <returns></returns>
		public static IEnumerable<TokenWithCount> CountContinuousTokens(IEnumerable<Token> tokens,
			int endIndex = int.MaxValue)
		{
			int index = 0;
			IEnumerable<Token> tokenList = tokens;
			while (tokenList.Any())//リストが空でないならループ
			{
				Token first = tokenList.First();//リストの先頭トークン
				int firstCount = tokenList.TakeWhile(t => t == first).Count();//先頭から、firstと同じトークンの連続個数
				yield return new TokenWithCount(first, firstCount);
				tokenList = tokenList.Skip(firstCount);//リストを更新
				index += firstCount;
				if (index >= endIndex) break;
			}
		}
	}
	#endregion

	/// <summary>
	/// Grass風言語
	/// </summary>
	public abstract class BaseRule
	{
		/// <summary>トークン、数、位置</summary>
		protected struct TokenWithCountWithIndex
		{
			public readonly TokenWithCount TokenWithCount;
			public readonly int Index;
			public TokenWithCountWithIndex(TokenWithCount token, int index)
			{
				TokenWithCount = token;
				Index = index;
			}
			public override string ToString()
			{
				return string.Format("{0},{1},{2}", TokenWithCount.Token, TokenWithCount.Count, Index);
			}
		}

		public abstract IEnumerable<Token> TextToTokens(string text);
		protected abstract string TokenToText(TokenWithCount token);
		/// <summary>
		/// トークンリストをソースコードに変換
		/// </summary>
		/// <param name="tokens">トークンリスト</param>
		/// <returns></returns>
		public IEnumerable<string> TokensToText(IEnumerable<TokenWithCount> tokens)
		{
			foreach (var token in tokens)
			{
				switch (token.Token)
				{
					case Token.LCW:
					case Token.UCW:
					case Token.V:
						yield return TokenToText(token);
						break;
					case Token.EOS:
						yield break;//Loop end
					default:
						break;
				}
			}
		}
	}
	/// <summary>
	/// 正規表現による言語ルール
	/// </summary>
	public abstract class RegexRule : BaseRule
	{
		public abstract Regex PatternLCW { get; }
		public abstract Regex PatternUCW { get; }
		public abstract Regex PatternV { get; }
		/// <summary>
		/// ソースコードをトークンリストに変換
		/// </summary>
		/// <param name="text">ソースコード</param>
		/// <returns></returns>
		public override IEnumerable<Token> TextToTokens(string text)
		{
			var lcws = PatternLCW.Matches(text).Cast<Match>().Select(lcw =>
				new TokenWithCountWithIndex(
					new TokenWithCount(Token.LCW, lcw.Groups.Count),
					lcw.Index
					)).ToArray();
			var ucws = PatternUCW.Matches(text).Cast<Match>().Select(ucw =>
				new TokenWithCountWithIndex(
					new TokenWithCount(Token.UCW, ucw.Groups.Count),
					ucw.Index
					)).ToArray();
			var vs = PatternV.Matches(text).Cast<Match>().Select(v =>
				new TokenWithCountWithIndex(
					new TokenWithCount(Token.V, v.Groups.Count),
					v.Index
					)).ToArray();
			var tokens = lcws.Concat(ucws).Concat(vs)//リストをつなげて
				.OrderBy(posAndToken => posAndToken.Index).ToArray();//先頭からの位置で昇順ソート
			foreach (var posAndToken in tokens)
			{
				for (int i = 0; i < posAndToken.TokenWithCount.Count; i++)
				{
					yield return posAndToken.TokenWithCount.Token;
				}
			}
			yield return Token.EOS;
		}
	}

	/// <summary>
	/// Grass
	/// </summary>
	public sealed class RuleGrass : RegexRule
	{
		private static readonly Regex _PatternLCW = new Regex(@"[wｗ]");
		private static readonly Regex _PatternUCW = new Regex(@"[WＷ]");
		private static readonly Regex _PatternV = new Regex(@"[vｖ]");
		public override Regex PatternLCW { get { return _PatternLCW; } }
		public override Regex PatternUCW { get { return _PatternUCW; } }
		public override Regex PatternV { get { return _PatternV; } }
		private static readonly Dictionary<Token, string> _PatternTokenToText = new Dictionary<Token, string>(){
				{Token.LCW,"w"},
				{Token.UCW,"W"},
				{Token.V,"v"},
			};
		protected override string TokenToText(TokenWithCount token)
		{
			switch (token.Token)
			{
				case Token.LCW:
				case Token.UCW:
				case Token.V:
					return string.Concat(Enumerable.Repeat(_PatternTokenToText[token.Token], token.Count));
				case Token.EOS:
				case Token.None:
				default:
					return string.Empty;
			}
		}
	}

	/// <summary>
	/// ほむほむ
	/// </summary>
	public sealed class RuleHomuHomu : BaseRule
	{
		private static readonly string _Keyword = "ほむ";
		private static readonly Regex _KeywordRule = new Regex(_Keyword);
		private static readonly Regex _SeparateRule = new Regex(@"[ \t]+");
		private static readonly Regex _NewlineRule = new Regex(@"\n");
		protected override string TokenToText(TokenWithCount token)
		{
			switch (token.Token)
			{
				case Token.LCW:
					return string.Concat(Enumerable.Repeat(_Keyword, token.Count));
				case Token.UCW:
					return " " + string.Concat(Enumerable.Repeat(_Keyword, token.Count)) + " ";
				case Token.V:
					return Environment.NewLine;
				case Token.EOS:
				case Token.None:
				default:
					return string.Empty;
			}
		}

		public override IEnumerable<Token> TextToTokens(string text)
		{
			bool isLowerCase = true;
			var homus = _KeywordRule.Matches(text).Cast<Match>();
			var seps = _SeparateRule.Matches(text).Cast<Match>();
			var newlines = _NewlineRule.Matches(text).Cast<Match>();
			var matches = homus.Concat(seps).Concat(newlines).OrderBy(m => m.Index);
			foreach (var match in matches)
			{
				if (_SeparateRule.IsMatch(match.Value)) isLowerCase = !isLowerCase;//'w'と'W'の状態切り替え
				else if (_KeywordRule.IsMatch(match.Value))
				{
					if (isLowerCase) yield return Token.LCW;//状態に応じて'w'か'W'を出力
					else yield return Token.UCW;
				}
				else if (_NewlineRule.IsMatch(match.Value))
				{
					yield return Token.V;
					isLowerCase = true;//改行後は'w'に状態切り替え
				}

			}
			yield return Token.EOS;
		}
	}
}
