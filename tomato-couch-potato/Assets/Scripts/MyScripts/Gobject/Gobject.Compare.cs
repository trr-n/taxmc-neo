using System;
using UnityEngine;

namespace trrne.Box
{
    public static partial class Gobject
    {
        /// <summary>
        /// タグを比較する -> <b>bool</b><br/>
        /// <c> info.CompareTag(tag) </c>
        /// </summary>
        /// <param name="info">オブジェクト</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>タグが設定されていたらtrueを返す</returns>
        public static bool CompareTag(this Collision info, string tag) => info.gameObject.CompareTag(tag);

        /// <summary>
        /// タグを比較する -> <b>bool</b><br/>
        /// <c> info.CompareTag(tag) </c>
        /// </summary>
        /// <param name="info">オブジェクト</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>タグが設定されていたらtrueを返す</returns>
        public static bool CompareTag(this Collision2D info, string tag) => info.gameObject.CompareTag(tag);

        /// <summary>
        /// タグを比較する -> <b>bool</b><br/>
        /// <c> hit.CompareTag(tag) </c>
        /// </summary>
        /// <param name="hit">オブジェクト</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>タグが設定されていたらtrueを返す</returns>
        public static bool CompareTag(this RaycastHit2D hit, string tag) => hit.collider.CompareTag(tag);
        [Obsolete] public static bool CompareTag(this Collider info, string tag) => info.CompareTag(tag);
        [Obsolete] public static bool CompareTag(this Collider2D info, string tag) => info.CompareTag(tag);

        /// <summary>
        /// レイヤーを比較する -> <b>bool</b><br/>
        /// <c> info.CompareLayer(layer) </c>
        /// </summary>
        /// <param name="info">比較したいオブジェクト</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <returns>レイヤーが設定されていたらtrueを返す</returns>
        public static bool CompareLayer(this Collider2D info, int layer) => info.GetLayer() == layer;

        /// <summary>
        /// レイヤーを比較する -> <b>bool</b><br/>
        /// <c> info.CompareLayer(layer) </c>
        /// </summary>
        /// <param name="info">比較したいオブジェクト</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <returns>レイヤーが設定されていたらtrueを返す</returns>
        public static bool CompareLayer(this Collision2D info, int layer) => info.GetLayer() == layer;

        /// <summary>
        /// レイヤーを比較する -> <b>bool</b><br/>
        /// <c> hit.CompareLayer(layer) </c>
        /// </summary>
        /// <param name="hit">比較したいオブジェクト</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <returns>レイヤーが設定されていたらtrueを返す</returns>
        public static bool CompareLayer(this RaycastHit2D hit, int layer) => hit.GetLayer() == layer;

        /// <summary>
        /// レイヤーとタグを比較する -> <b>bool</b><br/>
        /// <c> info.CompareBoth(layer, tag) </c>
        /// </summary>
        /// <param name="info">対象</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>layerとtagが設定されていたらtrueを返す</returns>
        public static bool CompareBoth(this Collision2D info, int layer, string tag)
        => info.CompareLayer(layer) && info.gameObject.CompareTag(tag);

        /// <summary>
        /// レイヤーとタグを比較する -> <b>bool</b><br/>
        /// <c> info.CompareBoth(layer, tag) </c>
        /// </summary>
        /// <param name="info">対象</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>layerとtagが設定されていたらtrueを返す</returns>
        public static bool CompareBoth(this Collider2D info, int layer, string tag)
        => info.CompareLayer(layer) && info.gameObject.CompareTag(tag);

        /// <summary>
        /// レイヤーとタグを比較する -> <b>bool</b><br/>
        /// <c> hit.CompareBoth(layer, tag) </c>
        /// </summary>
        /// <param name="hit">対象</param>
        /// <param name="layer">比較したいレイヤー</param>
        /// <param name="tag">比較したいタグ</param>
        /// <returns>layerとtagが設定されていたらtrueを返す</returns>
        public static bool CompareBoth(this RaycastHit2D hit, int layer, string tag)
        => hit.CompareLayer(layer) && hit.collider.CompareTag(tag);

        public static bool Contain(this Collision info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider info, string tag) => info.tag.Contains(tag);
        public static bool Contain(this Collision2D info, string tag) => info.gameObject.tag.Contains(tag);
        public static bool Contain(this Collider2D info, string tag) => info.gameObject.tag.Contains(tag);
    }
}