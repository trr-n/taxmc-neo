using System;

namespace trrne.Box
{
    public static class Typing
    {
        /// <summary>
        /// 一括置換する -> <b>string</b><br/>
        /// <c>
        ///     string unicode = "Unicode";<br/>
        ///     string[] target = { "i", "d", "e" };
        ///     string unco = unicode.ReplaceLump(target, "");<br/>
        ///     print(unco);<br/>
        ///     output: Unco
        /// </c>
        /// </summary>
        /// <param name="target">置換前</param>
        /// <param name="befores">置換の対象</param>
        /// <param name="after">置換したい文字</param>
        /// <returns>置換処理後の文字列を返す</returns>
        public static string ReplaceLump(this string target, string[] befores, string after)
        {
            befores.ForEach(before => target = target.Replace(before, after));
            return target;
        }

        /// <summary>
        /// 一括置換する -> <b>void</b><br/>
        /// <c>
        ///     string unicode = "Unicode";<br/>
        ///     string[] target = { "i", "d", "e" };
        ///     Typing.ReplaceLump2(ref unicode, target, "");<br/>
        ///     print(unicode);<br/>
        ///     output: Unco
        /// </c>
        /// </summary>
        /// <param name="target">置換前(参照渡し)</param>
        /// <param name="befores">置換の対象</param>
        /// <param name="after">置換したい文字</param>
        public static void ReplaceLump2(ref string target, string[] befores, string after)
        {
            string dst = target;
            befores.ForEach(before => dst = dst.Replace(before, after));
            target = dst;
        }

        /// <summary>
        /// 文字列から削除する -> <b>string</b><br/>
        /// <c>
        ///     string unity = "Unity-Chan!";<br/>
        ///     string before = unity.Delete("-Chan");<br/>
        ///     print(before);<br/>
        ///     output: Unity!
        /// </c>
        /// </summary>
        /// <param name="target">削除前</param>
        /// <param name="be">削除したい文字列</param>
        /// <returns>削除処理後の文字列を返す</returns>
        public static string Delete(this string target, string be) => target.Replace(be, "");

        /// <summary>
        /// 一括削除する -> <b>string</b><br/>
        /// <c>
        ///     string unity = "ユニティちゃんかわいくない!?";<br/>
        ///     Typing.DeleteLump(unity, "くな", "?");<br/>
        ///     print(unicode);<br/>
        ///     output: ユニティちゃんかわいい!
        /// </c>
        /// </summary>
        /// <param name="target">置換前(参照渡し)</param>
        /// <param name="befores">置換の対象</param>
        /// <param name="after">置換したい文字</param>
        public static string DeleteLump(this string basis, params string[] targets)
        {
            targets.ForEach(target => basis = basis.Delete(target));
            return basis;
        }

        /// <summary>
        /// 継承しているか判定する
        /// </summary>
        /// <param name="t">判定したい型</param>
        /// <returns>objがtを継承していたらtrueを返す</returns>
        public static bool Subclass(this object obj, Type t) => obj.GetType().IsSubclassOf(t);
        public static bool Subclass(this object[] objs, Type t) => objs.GetType().IsSubclassOf(t);

        public static string Join(this object[] objs, string sep) => string.Join(sep, objs);

        public static string Link(this char[] objs) => string.Join("", objs);
        public static string Link(this byte[] b) => string.Join("", b);

        public static int Length<T>(this T t) where T : Enum => Enum.GetValues(t.GetType()).Length;
        public static int Length2<T>() where T : Enum => Enum.GetValues(typeof(T)).Length;
    }
}
