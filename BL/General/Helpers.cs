using System.Transactions;

namespace HHD.BL.General
{
    public class Helpers
    {
        public static TransactionScope CreateTransactionScope(int seconds = 6000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public static Guid? StringToGuidGef(string str)
        {
            Guid val;
            if(Guid.TryParse(str, out val)) { return val; }
            return null;
        }
    }
}
