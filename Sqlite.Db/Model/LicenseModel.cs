using SqlSugar;

namespace Sqlite.Db.Model
{
    [SugarTable("license")]
    public class LicenseModel
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string MachineCode { get; set; }
        public string Key { get; set; }
    }
}
