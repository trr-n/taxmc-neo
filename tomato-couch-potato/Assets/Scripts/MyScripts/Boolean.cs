namespace Chickenen.Pancreas
{
    public static class Boolean
    {
        /// <summary>
        /// 一括ぬるちぇっく
        /// </summary>
        public static bool IsNull(params object[] objs)
        {
            foreach (var o in objs)
            {
                if (o == null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
