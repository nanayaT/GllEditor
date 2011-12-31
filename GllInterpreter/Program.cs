using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GrassLikeLanguage.Rule;

namespace GrassLikeLanguage.Interpreter
{
	/// <summary>
	/// インタプリタの支援<para></para>
	/// オプションとして渡す引数など
	/// </summary>
	public static class OptionManager
	{
		/// <summary>
		/// オプション文字列('-'+英小文字)とルールインスタンス生成の対応リスト
		/// </summary>
		public static readonly Dictionary<string, Func<BaseRule>> OptionToRule = new Dictionary<string, Func<BaseRule>>(){
			{"-g", ()=>new RuleGrass()},
			{"-h", ()=>new RuleHomuHomu()},
		};
		/// <summary>スクリプトモードのオプション</summary>
		public const string OptionScriptMode = "-e";
		/// <summary>
		/// 除外リスト<para></para>
		/// ルールと無関係のオプションを登録する
		/// </summary>
		public static readonly List<string> ExceptOptions = new List<string>() { OptionScriptMode };
		/// <summary>
		/// 該当オプションがあればそのルールを、なければnullを返す
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		public static BaseRule GetOption(string option)
		{
			string opt = option.ToLower();
			if (OptionToRule.ContainsKey(opt))
				return OptionToRule[opt]();
			else
				return null;
		}
		/// <summary>
		/// オプションとルールインスタンス生成方法を登録する
		/// </summary>
		/// <param name="option"></param>
		/// <param name="ruleFactory"></param>
		/// <exception cref="ArgumentException">指定オプションが除外リストに登録済み</exception>
		public static void SetOption(string option, Func<BaseRule> ruleFactory)
		{
			string opt = option.ToLower();
			if (ExceptOptions.Contains(opt))
			{
				throw new ArgumentException("そのオプションは除外リストに登録済みです");
			}
			OptionToRule[opt] = ruleFactory;
		}
	}
    public class Interpreter
    {
        static readonly string _ErrorMessage1 = "Usage: GllInterpreter [-g|-h] {path}";
        static readonly string _ErrorMessage2 = "Usage: GllInterpreter [-g|-h] -e {source_code}";
        static ConsoleKeyInfo PrintError()
        {
            Console.Error.WriteLine(_ErrorMessage1);
            Console.Error.WriteLine(_ErrorMessage2);
            return Console.ReadKey();
        }
        public static void Run(string[] args)
        {
            if (args.Length < 2)
            {
                PrintError();
                return;
            }
            Rule.BaseRule rule;
            string option = args[0].ToLower();

            rule = OptionManager.GetOption(option);
            if (rule == null)
            {
                PrintError();
                return;
            }
            bool isScriptMode = string.Compare(args[1], OptionManager.OptionScriptMode, true) == 0;
            string input;
            if (isScriptMode)
            {
                if (args.Length < 3)
                {
                    PrintError();
                    return;
                }
                //引数文字列をそのままソースコードとして解釈し実行
                input = args.Skip(2).Aggregate(
                    new StringBuilder(),
                    (sb, s) => sb.Append(s + ' '),
                    (sb) => sb.ToString());
            }
            else
            {
                //引数パスのソースファイルから読み込み実行
                input = File.ReadAllText(args[1]);
            }
            var runner = new Runner(input, rule);
            runner.Run();
        }
    }
    class Program
    {
		
        static void Main(string[] args)
        {
            try
            {
                Interpreter.Run(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    ex.StackTrace
                );
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
