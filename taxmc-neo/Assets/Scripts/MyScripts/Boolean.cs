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
                if (obj == null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
