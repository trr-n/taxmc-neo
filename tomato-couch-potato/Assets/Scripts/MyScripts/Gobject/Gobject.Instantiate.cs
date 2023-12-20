#pragma warning disable IDE0002

using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        /// <summary>
        /// 生成する -> <b>T</b><br/>
        /// <c>t.Instantiate(p, r)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>インスタンスを返す</returns>
        public static T Instantiate<T>(this T t, Vector3 p = new(), Quaternion r = new()) where T : Object
        => GameObject.Instantiate(t, p, r);

        /// <summary>
        /// 配列からランダムに選び生成する -> <b>T</b><br/>
        /// <c>ts.Instantiate(p, r)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象の配列</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>インスタンスを返す</returns>
        public static T Instantiate<T>(this T[] ts, Vector3 p = new(), Quaternion r = new()) where T : Object
        => GameObject.Instantiate(ts.Choice(), p, r);

        /// <summary>
        /// 生成する -> <b>T</b><br/>
        /// <c>ts.Instantiate(transform)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象</param>
        /// <returns>インスタンスを返す</returns>
        public static T Instantiate<T>(this T t, Transform transform) where T : Object
        => GameObject.Instantiate(t, transform.position, transform.rotation);

        /// <summary>
        /// 配列からランダムに選び生成する -> <b>T</b><br/>
        /// <c>ts.TryInstantiate(transform)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="transform">対象</param>
        /// <returns>インスタンスを返す</returns>
        public static T Instantiate<T>(this T[] ts, Transform transform) where T : Object
        => GameObject.Instantiate(ts.Choice(), transform.position, transform.rotation);

        /// <summary>
        /// tがnullでなければ生成する -> <b>T</b><br/>
        /// <c>t.TryInstantiate(p, r)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>tがnullだったらnullを、nullでなければインスタンスを返す</returns>
        public static T TryInstantiate<T>(this T t, Vector3 p = new(), Quaternion r = new()) where T : Object
        => t != null ? GameObject.Instantiate(t, p, r) : null;

        /// <summary>
        /// tがnullでなければ生成する -> <b>T</b><br/>
        /// <c>t.TryInstantiate(transform)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>tがnullだったらnullを、nullでなければインスタンスを返す</returns>
        public static T TryInstantiate<T>(this T t, Transform transform) where T : Object
        => t != null ? GameObject.Instantiate(t, transform.position, transform.rotation) : null;

        /// <summary>
        /// tがnullでなければランダムに選択し生成する -> <b>T</b><br/>
        /// <c>t.TryInstantiate(p, r)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="t">対象の配列</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>tがnullだったらnullを、nullでなければインスタンスを返す</returns>
        public static T TryInstantiate<T>(this T[] ts, Vector3 p = new(), Quaternion q = new()) where T : Object
        => ts.Length > 0 ? GameObject.Instantiate(ts.Choice(), p, q) : null;

        /// <summary>
        /// tがnullでなければランダムに選択し生成する -> <b>T</b><br/>
        /// <c>t.TryInstantiate(transform)</c>
        /// </summary>
        /// <typeparam name="T">対象の型</typeparam>
        /// <param name="transform">対象の配列</param>
        /// <param name="p">座標</param>
        /// <param name="r">回転座標</param>
        /// <returns>tがnullだったらnullを、nullでなければインスタンスを返す</returns>
        public static T TryInstantiate<T>(this T[] ts, Transform transform) where T : Object
        => ts.Length > 0 ? GameObject.Instantiate(ts.Choice(), transform.position, transform.rotation) : null;
    }
}