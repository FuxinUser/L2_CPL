using System;

namespace Controller
{
    public static class TryFlow
    {

        /// <summary>
        ///     Try action of flow
        /// </summary>
        public static void TryFlowA(Action action, bool @throw = false)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.Message={ex.Message}");
                Console.WriteLine($"ex.StackTrace={ex.StackTrace}");
                if (@throw) throw;
            }
            finally
            {
            }
        }
    }
}
