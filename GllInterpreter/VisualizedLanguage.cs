using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GrassLikeLanguage.Rule;

namespace GrassLikeLanguage.Visualization
{
	/// <summary>
	/// Grass風言語を可視化するための言語のような何か
	/// </summary>
	public sealed class VisualizedLanguage
	{
		public const string AbsHeader = "Abs";
		public const string AppHeader = "App";
		public const string AbsSeparator = "/";
		public const string EndOfString = "EOS";
		private static readonly Regex _RegexNumber = new Regex(@"(\d+)");
		/// <summary>
		/// 数字にマッチする正規表現
		/// </summary>
		public static Regex RegexNumber { get { return _RegexNumber; } }

		/// <summary>
		/// 中間言語の１要素
		/// </summary>
		public string Line { get; private set; }

		public VisualizedLanguage(string line)
		{
			Line = line;
		}

		#region override
		public override bool Equals(object obj)
		{
			var other = obj as VisualizedLanguage;
			if (other == null) { return false; }
			return this.Line == other.Line;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Line.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("IL( {0} )", Line);
		}
		#endregion

		/// <summary>
		/// トークンリストから可視化言語的な何かのリストに変換
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="GllException"></exception>
		public static IEnumerable<VisualizedLanguage> ToVisualizedLanguage(IList<TokenWithCount> tokens)
		{
			bool endFlag = false;
			int absCount = 0;
			for (int index = 0; !endFlag && index < tokens.Count; index++)
			{
				var car = tokens[index];
				switch (car.Token)
				{
					case Token.LCW:
						{
							#region ABS
							int argCount = car.Count;
							var argList = new List<Tuple<int, int>>();
							while (true)
							{
								index++;
								var firstTwc = tokens[index];
								if (firstTwc.Token == Token.UCW)
								{
									index++;
									var secondTwc = tokens[index];
									if (secondTwc.Token == Token.LCW)
									{
										argList.Add(
											new Tuple<int, int>(firstTwc.Count, secondTwc.Count)
											);
									}
									else
									{
										throw new GllArgumentNotFoundException("引数の指定がねーんだけど？");
									}
								}
								else if (firstTwc.Token == Token.LCW)
								{
									throw new GllFunctionNotFoundException("関数の指定がねーんだけど？");
								}
								else if (firstTwc.Token == Token.V)
								{
									string result;
									if (argList.Any())
									{
										var first = argList.First();
										string apps = argList.Skip(1).Aggregate(
											string.Format("({0},{1})", first.Item1, first.Item2),
											(str, tuple) => string.Format("{0}, ({1},{2})", str, tuple.Item1, tuple.Item2)
												);
										result = string.Format("{0}({1}, [{2}])", AbsHeader, argCount, apps);
									}
									else
									{
										result = string.Format("{0}({1}, [])", AbsHeader, argCount);
									}
									yield return new VisualizedLanguage(result);
									absCount++;
									yield return new VisualizedLanguage(AbsSeparator);
									break;
								}
								else//(upperTwc.Token == Token.EOS)
								{
									string apps = string.Empty;
									if (argList.Any())
									{
										var first = argList.First();
										apps = argList.Skip(1).Aggregate(
											string.Format("({0},{1})", first.Item1, first.Item2),
											(str, tuple) => string.Format("{0}, ({1},{2})", str, tuple.Item1, tuple.Item2)
												);
									}
									string result = string.Format("{0}({1}, [{2}])", AbsHeader, argCount, apps);
									yield return new VisualizedLanguage(result);
									absCount++;
									endFlag = true;
									yield return new VisualizedLanguage(EndOfString);
									break;
								}
							}
							#endregion
							break;
						}
					case Token.UCW:
						{
							#region App
							index++;
							var first = tokens[index];
							if (first.Token == Token.LCW)
							{
								int funcIndex = car.Count;
								int argIndex = first.Count;
								string result = string.Format("{0}({1}, {2})", AppHeader, funcIndex, argIndex);
								yield return new VisualizedLanguage(result);
							}
							else
							{
								throw new GllArgumentNotFoundException("引数の指定がないお");
							}
							#endregion
							break;
						}
					case Token.V:
						{
							#region V
							if (absCount == 0)
							{
								throw new ArgumentException("関数定義がないとかｗｗｗ");
							}
							else
							{
								yield return new VisualizedLanguage(AbsSeparator);
							}
							#endregion
							break;
						}
					case Token.EOS:
						{
							endFlag = true;
							yield return new VisualizedLanguage(EndOfString);
							break;
						}
				}
			}
		}

		/// <summary>
		/// 可視化言語のリストからトークンリストに変換
		/// </summary>
		/// <param name="ils">可視化言語のリスト</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="GllException"></exception>
		public static IEnumerable<TokenWithCount> ToTokens(IEnumerable<VisualizedLanguage> ils)
		{
			return ToTokens(ils.Select(il => il.Line));
		}
		/// <summary>
		/// 可視化言語のリストからトークンリストに変換
		/// </summary>
		/// <param name="ilTexts">可視化言語の文字列</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="GllException"></exception>
		public static IEnumerable<TokenWithCount> ToTokens(params string[] ilTexts)
		{
			return ToTokens(ilTexts.AsEnumerable());
		}
		/// <summary>
		/// 可視化言語のリストからトークンリストに変換
		/// </summary>
		/// <param name="ilTexts">可視化言語の文字列</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="GllException"></exception>
		public static IEnumerable<TokenWithCount> ToTokens(IEnumerable<string> ilTexts)
		{
			const string patternApp = @"^.*(" + AppHeader + @").*\(.*(\d+).*,.*(\d+).*\).*$";
			const string patternAbs = @"^.*(" + AbsHeader + @").*\(.*(\d+).*,.*\[(\((\d+).*,.*(\d+)\))*.*\].*\).*$";
			const string patternAbsSeparator = @"^.*(\" + AbsSeparator + @").*$";
			const string patternEos = @"^.*" + EndOfString + @".*$";
			Regex regexApp = new Regex(patternApp);
			Regex regexAbs = new Regex(patternAbs);
			Regex regexAbsSep = new Regex(patternAbsSeparator);
			Regex regexEos = new Regex(patternEos);

			foreach (var ilText in ilTexts)
			{
				foreach (var line in ilText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (regexEos.IsMatch(line))
					{
						yield return new TokenWithCount(Token.EOS);
					}
					else if (regexAbsSep.IsMatch(line))
					{
						yield return new TokenWithCount(Token.V);
					}
					else
					{
						if (regexAbs.IsMatch(line))
						{
							#region Abs
							int[] nums = _RegexNumber.Matches(line)
								.Cast<Match>()
								.Select(m => int.Parse(m.Value.ToString())).ToArray();
							if (nums.Length % 2 == 0)
							{
								throw new GllArgumentNotFoundException("Abs(n, [(n,n), (n,n), (n,n)])");
							}
							yield return new TokenWithCount(Token.LCW, nums[0]);
							for (int i = 1; i < nums.Length; i += 2)
							{
								yield return new TokenWithCount(Token.UCW, nums[i]);
								yield return new TokenWithCount(Token.LCW, nums[i + 1]);
							}
							#endregion
						}
						else if (regexApp.IsMatch(line))
						{
							#region App
							int[] nums = _RegexNumber.Matches(line)
								.Cast<Match>()
								.Select(m => int.Parse(m.Value.ToString())).ToArray();
							if (nums.Length % 2 == 1)
							{
								throw new GllArgumentNotFoundException("App(n, n)");
							}
							for (int i = 0; i < nums.Length; i += 2)
							{
								yield return new TokenWithCount(Token.UCW, nums[i]);
								yield return new TokenWithCount(Token.LCW, nums[i + 1]);
							}
							#endregion
						}
						else
						{
							throw new GllDisableTokenException("そんな文字列，俺の可視化言語で許されると思ってるの？");
						}
					}
				}
			}
		}

		/// <summary>
		/// Grass風言語ソースコードを可視化言語に変換
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static string GllToVisualized(string source, BaseRule rule)
		{
			var il = Visualization.VisualizedLanguage.ToVisualizedLanguage(
				TokenWithCount.CountContinuousTokens(
				rule.TextToTokens(
				source)
				).ToArray());
			StringBuilder sb = new StringBuilder();
			foreach (var line in il)
			{
				sb.AppendLine(line.Line);
			}
			return sb.ToString();
		}
		/// <summary>
		/// 可視化言語をGrass風言語ソースコードに変換
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static string VisualizedToGll(string source, BaseRule rule)
		{
			var tokens = Visualization.VisualizedLanguage.ToTokens(source);
			StringBuilder sb = new StringBuilder();
			foreach (var text in rule.TokensToText(tokens))
			{
				sb.Append(text);
			}
			return sb.ToString();
		}
	}
}
