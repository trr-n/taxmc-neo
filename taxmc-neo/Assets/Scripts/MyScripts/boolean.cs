namespace trrne.Bag
{
    public static class Boolean
    {
        /// <summary>
        /// 一括ぬるちぇっく
        /// </summary>
        public static bool IsNull(params object[] objs)
        {
            foreach (var obj in objs)
            {
                // ひとつでもぬるがあったらtrue
                if (obj == null)
                {
                    return true;
                }
            }

            // 走り切ったらfalse
            return false;
        }

    }
}