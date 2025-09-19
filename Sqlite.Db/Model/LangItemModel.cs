using SqlSugar;

namespace Sqlite.Db.Model
{
    [SugarTable("lang")]
    public class LangItemModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
