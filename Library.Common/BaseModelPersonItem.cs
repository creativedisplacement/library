namespace Library.Common
{
    public abstract class BaseModelPersonItem : BaseModelNameItem
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}