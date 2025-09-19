namespace TgMuApp.Model
{
    public class ChoiceGroupModel
    {
        public bool IsChoice { get; set; }
        public string Account { get; set; }
        public string GroupName { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public bool IsGroup { get; set; }
        public bool IsChannel { get; set; }
    }
}
