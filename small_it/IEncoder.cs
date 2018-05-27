namespace small_it
{
    interface IEncoder
    {
        string Destination { get; }
        void Compress();
        byte[] AsByteArray();
    }
}
