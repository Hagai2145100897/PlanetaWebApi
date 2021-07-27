namespace PlanetaWebApi.Repositories
{
    public class ItemInfo
    {
        public readonly string ItemName;
        public readonly string[] ItemFieldNames;
        
        public ItemInfo(string itemName, string[] itemFieldNames)
        {
            ItemName = itemName;
            ItemFieldNames = itemFieldNames;
        }
    }   
}