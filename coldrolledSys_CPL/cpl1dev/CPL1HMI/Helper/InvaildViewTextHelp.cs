using System;

namespace CPL1HMI.Helper
{
    /// <summary>
    /// 驗證HMI輸入是否有誤
    /// </summary>
    public static class InvaildViewTextHelp
    {

        /// <summary>
        ///     Try action of flow
        /// </summary>
        public static bool Invaild(Action action, bool @throw = false)
        {
            try
            {
                action?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                DialogHandler.Instance.Fun_DialogShowOk(ex.Message, "資料驗證錯誤", 3);
                if (@throw) throw;
                return false;
            }
            finally
            {
            }
        }

        public static void AssertTextEmpty(this string text, string errorMessage)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(errorMessage);
            }
        }



    }
}
