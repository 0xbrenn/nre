using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TgMuApp.Model;

namespace TgMuApp.Utils
{
    public class LangUtils
    {
        public static string LangKey = "en";
        private static List<LangModel> rList = new List<LangModel>();


        public static List<Language> GetAllLanguage()
        {
            var list = new List<Language> {
                 new Language { Name = "English", Code = "en"  },
                 new Language { Name = "العربية", Code = "ar" },
                 new Language { Name = "azərbaycan", Code = "az" },
                 new Language { Name = "bosanski", Code = "bs" },
                 new Language { Name = "Italiano", Code = "it" },
                 new Language { Name = "Dansk", Code = "da" },
                 new Language { Name = "Deutsch", Code = "de" },
                 new Language { Name = "Français", Code = "fr" },
                 new Language { Name = "Português", Code = "pt" },
                 new Language { Name = "Español", Code = "es" },
                  new Language { Name = "euskara", Code = "eu" },
                 new Language { Name = "Bahasa Indonesia", Code = "id" },
                 new Language { Name = "Bahasa Melayu", Code = "ms" },
                 new Language { Name = "Filipino", Code = "fil" },
                 new Language { Name = "latviešu", Code = "lv" },
                 new Language { Name = "Русский", Code = "ru" },
                 new Language { Name = "lietuvių", Code = "lt" },
                 new Language { Name = "magyar", Code = "hu" },
                 new Language { Name = "Nederlands", Code = "nl" },
                 new Language { Name = "norsk", Code = "no" },
                 new Language { Name = "oʻzbekcha", Code = "uz" },
                 new Language { Name = "Tiếng Việt", Code = "vi" },
                 new Language { Name = "Türkçe", Code = "tr" },
                 new Language { Name = "हिन्दी", Code = "hi" },
                 new Language { Name = "ខ្មែរ", Code = "km" },
                 new Language { Name = "简体中文", Code = "zh-CN" },
                 new Language { Name = "繁體中文", Code = "zh-TW" }

            };
            return list;
        }
        private static List<LangModel> GetAllLang()
        {
            if (rList.Count == 0)
            {
                rList = GetLangFrom();
            }
            return rList;
        }
        private static List<LangModel> GetLangFrom()
        {
            var list = new List<LangModel>();
            string targetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(targetPath, "LangJson");
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var str = File.ReadAllText(file, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(str))
                    {
                        var model = JsonConvert.DeserializeObject<LangModel>(str);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public static LangModel GetLang()
        {
            var list = GetAllLang();
            return list.FirstOrDefault(s => s.Key == LangKey);
        }
    }
}
