using SqlSugar;

namespace Sqlite.Db.Model
{
    [SugarTable("msg")]
    public class MsgModel  
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string File { get; set; }
        public string Type { get; set; }
        public string Cate { get; set; }
    }
}
