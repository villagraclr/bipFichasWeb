namespace MDS.Identity
{
    public static class ToDecimalExtentions
    {
        public static decimal ToDecimal(this bool obj)
        {
            return (obj) ? 1 : 0;
        }

        public static bool ToBool(this decimal obj)
        {
            return (obj == 1);
        }
    }
}