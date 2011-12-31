using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassLikeLanguage.Util
{
    /// <summary>
    /// 拡張メソッド
    /// </summary>
    static class StackExtension
    {
        /// <summary>
        /// スタックの指定番号の要素を返す
        /// <para></para>非破壊的メソッド
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Get<T>(this Stack<T> stack, int index)
        {
            return stack.ElementAt(index);
        }

        /// <summary>
        /// スタック(<paramref name="adding"/>)の中身を順序そのままでプッシュする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="adding"></param>
        public static void PushRange<T>(this Stack<T> source, Stack<T> adding)
        {
            foreach (int index in Enumerable.Range(0, adding.Count).Reverse())
            {
                //スタック要素を逆順にプッシュする
                source.Push(
                    adding.Get(index)
                    );
            }
        }
        /// <summary>
        /// まとめてプッシュする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="adding"></param>
        public static void PushRange<T>(this Stack<T> source, params T[] adding)
        {
            //要素一個ずつプッシュするだけの簡単なお仕事
            foreach (T val in adding)
            {
                source.Push(val);
            }
        }
    }
}
