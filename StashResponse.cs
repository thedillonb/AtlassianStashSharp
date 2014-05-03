namespace AtlassianStashSharp
{
    public class StashResponse<TData>
    {
        public StashResponse(TData data)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }

    public class StashResponse
    {
    }
}
