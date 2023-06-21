namespace WatchDog.Interface
{
    public interface ISysLog
    {
        void Build();

        void I(string msg);

        void D(string msg);

        void W(string msg);

        void E(string msg);
    }
}
