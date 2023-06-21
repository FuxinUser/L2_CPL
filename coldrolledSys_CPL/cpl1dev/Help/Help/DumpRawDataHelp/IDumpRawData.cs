namespace Core.Help.DumpRawDataHelp
{
    public interface IDumpRawData
    {
        void DumpMsg(byte[] data, string filePath);

        string GetMsgID(byte[] data);
    }
}
