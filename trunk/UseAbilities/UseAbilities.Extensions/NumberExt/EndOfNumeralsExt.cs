namespace UseAbilities.Extensions.NumberExt
{
    public static class EndOfNumeralsExt
    {
        private const string Yi = "ый";
        private const string Oi = "ой";
        private const string Ii = "ий";

        /// <summary>
        /// Invalid rule for Russian language. Valid use only й letter
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetAccretionOfCaseEndings(this int number)
        {
            var end = string.Empty;
            if (number == 3)  end = Ii;
            if (number == 2 || (number > 5 && number < 9)) end = Oi;
            if (number == 1 || (number > 3 && number < 6) || number >= 9) end =  Yi;

            return string.Format("{0}{1}",number, end);
        }
    }
}
