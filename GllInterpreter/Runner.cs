using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GrassLikeLanguage.Function;
using GrassLikeLanguage.Rule;
using GrassLikeLanguage.Util;

namespace GrassLikeLanguage
{
    /// <summary>
    /// 文字列を解釈し、実行する
    /// </summary>
    public sealed class Runner
    {
		/// <summary>途中経過を表示するための、{最後まで成功か/結果}をまとめたクラス</summary>
        private class InterpretResult
        {
            public readonly bool Success;
            public readonly IEnumerable<GrassFunc> Result;
            public InterpretResult(bool success, IEnumerable<GrassFunc> result)
            {
                Success = success;
                Result = result;
            }
        }
        /// <summary>入力ソースコード</summary>
        public string Input { get; private set; }
		private BaseRule _Rule;
        private readonly Stack<GrassFunc> _FuncStack;
        private static readonly GrassFunc[] _PrimitiveFuntions = new GrassFunc[]{
                new In(),
                new GrassChar('w'),
                new Succ(),
                new Out()
            };
        /// <summary>結果スタックを格納しておく</summary>
		public static string StackPrinted { get; private set; }
        /// <summary>
        /// 文字列を解釈し、実行する
        /// </summary>
        /// <param name="input">入力文字列</param>
        /// <param name="addingFuncStack">追加プリミティブ関数</param>
		/// <param name="rule">文字列からトークンに変換するためのルール</param>
		public Runner(string input, BaseRule rule, Stack<GrassFunc> addingFuncStack = null)
		{
			Input = input;
			_Rule = rule;
			_FuncStack = new Stack<GrassFunc>();
			_FuncStack.PushRange(_PrimitiveFuntions);
			if (addingFuncStack != null && addingFuncStack.Count > 0)
			{
				_FuncStack.PushRange(addingFuncStack);
			}
		}

        /// <summary>
        /// <see cref="Input"/>を解釈し、処理を実行する
        /// </summary>
        public void Run()
        {
            var result = Interpret();
            foreach (var func in result.Result)
            {
                _FuncStack.Push(
                    func
                    );
                StackPrinted = PrintStack();
            }
            //仕様より、App(1, 1)が自動実行される
            //パース失敗時は実行しない
            if (result.Success)
            {
                try
                {
                    var lastFunc = _FuncStack.Peek();
                    _FuncStack.Push(
                        lastFunc.Apply(lastFunc)
                        );
                    StackPrinted = PrintStack();
                }
                catch (NotImplementedException) { }//誤って値関数以外を評価した場合の例外は無視
            }
        }
        /// <summary>
        /// 文字列 => トークン => 定義/適用した関数リスト
        /// </summary>
        /// <returns>関数の定義/適用結果リスト</returns>
        private InterpretResult Interpret()
        {
            try
            {
                var tokenList = _Rule.TextToTokens(Input);//文字列をトークンリストに変換
                var tokenWithCountList = TokenWithCount.CountContinuousTokens(tokenList);//連続トークンをカウント
                return InterpretFromTokenWithCounts(_FuncStack, tokenWithCountList);//現スタックに対して連続トークンを解釈
            }
            catch (GllException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                System.Diagnostics.Debug.WriteLine("");
                throw;
            }
        }
        /// <summary>
        /// 連続トークンを読み取り、解釈し、関数を定義/適用する
        /// </summary>
        /// <param name="tokenWithCountList"></param>
        /// <returns>関数の定義/適用結果リスト</returns>
        /// <exception cref="GllException">パースに失敗</exception>
        private static InterpretResult InterpretFromTokenWithCounts(Stack<GrassFunc> initStack, 
            IEnumerable<TokenWithCount> tokenWithCountList)
        {
            bool isLoopEnd = false;
            var localFuncStack = new Stack<GrassFunc>();
            try
            {
                //残りのトークンリスト
                var tokenWithCounts = tokenWithCountList.Where(twc => twc.Token != Token.None).ToArray();
				for(int index = 0; !isLoopEnd && index < tokenWithCounts.Length; index++)
                {
                    //行の先頭トークン
					var headTokenCount = tokenWithCounts[index];
                    switch (headTokenCount.Token)
                    {
                        case Token.LCW:
                            {
                                #region 関数定義部(ww..WW..ww..)
                                GrassMyFunc currentFunc = new GrassMyFunc(headTokenCount.Count);
                                while (true)
                                {
                                    //2つ取り出す
									index++;
									var firstTokenCount = tokenWithCounts[index];
                                    if (firstTokenCount.Token == Token.UCW)
                                    {
										index++;
										var secondTokenCount = tokenWithCounts[index];
                                        if (secondTokenCount.Token == Token.LCW)
                                        {
                                            currentFunc.AddSentence(new Sentence()
                                            {
                                                CountUpperCaseW = firstTokenCount.Count,
                                                CountLowerCaseW = secondTokenCount.Count,
                                            });
                                        }
                                        else
                                        {
                                            throw new GllArgumentNotFoundException("関数定義じゃねーの？");
                                        }
                                    }
                                    else if (firstTokenCount.Token == Token.V || firstTokenCount.Token == Token.EOS)
                                    {
                                        currentFunc.CopyFuncStack(initStack);
                                        currentFunc.CopyFuncStack(localFuncStack);
                                        localFuncStack.Push(currentFunc);
                                        break;
                                    }
                                    else // Token.LCW
                                    {
                                        throw new GllDisableTokenException(
                                            "ヤダナー。LCWの後にLCWとか、そんな入力来るわけないじゃないですかー。そんなん引数が悪いっすわー");
                                    }
                                }
                                #endregion
                                break;
                            }
                        case Token.UCW:
                            {
                                #region 関数適用部(WW..ww..)
                                //1つ取り出す
								index++;
								var firstTokenCount = tokenWithCounts[index];
                                if (firstTokenCount.Token == Token.LCW)
                                {
                                    int funcCount = headTokenCount.Count - 1;
                                    GrassFunc func = localFuncStack.Count > funcCount ?
                                        localFuncStack.Get(funcCount) :
                                        initStack.Get(funcCount - localFuncStack.Count);
                                    int argCount = firstTokenCount.Count - 1;
                                    GrassFunc arg = localFuncStack.Count > argCount ?
                                        localFuncStack.Get(argCount) :
                                        initStack.Get(argCount - localFuncStack.Count);
                                    localFuncStack.Push(func.Apply(arg));
                                }
                                else
                                {
                                    throw new GllArgumentNotFoundException("関数適用じゃねーの？");
                                }
                                #endregion
                                break;
                            }
                        case Token.V:
                            break;
                        case Token.EOS:
                            isLoopEnd = true;
                            break;
                        case Token.None:
                        default:
                            throw new GllDisableTokenException("バカな・・・そのトークンは存在しないはずだ・・・");
                    }
                }
                return new InterpretResult(true, localFuncStack.Reverse());//関数のリスト
            }
            catch (GllException)
            {
                return new InterpretResult(false, localFuncStack.Reverse());//致命的でない場合、失敗しても途中経過の関数リストを返す
            }
        }

        /// <summary>
        /// 現在の<see cref="_FuncStack"/>の中身を表示する
        /// </summary>
        private string PrintStack()
        {
			StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _FuncStack.Count; i++)
            {
				sb.AppendLine(
					string.Format("{0} : {1}", i, _FuncStack.Get(i))
					);
            }
			sb.AppendLine();
			return sb.ToString();
        }
    }

}
