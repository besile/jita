using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jita.Common.Attr;
using Jita.Enum.Model;
using Jita.LuceneManger;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace Jita.LuceneManger
{
    public class LuceneIndex
    {
        /// <summary>
        /// 添加索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void InsertToIndex<T>(T obj)where T:new ()
        {
            Dictionary<string, KeyValuePair<int, object>> dics = GetProperty<T>(obj);
            Document document = new Document();
            foreach (var dic in dics)
            {
                ////WITH_POSITIONS_OFFSETS:指示不仅保存分割后的词 还保存词之间的距离
                if (dic.Value.Key == (int) em_LuceneSplitWord.ANALYZED)
                {
                    document.Add(new Field(dic.Key, dic.Value.Value.ToString(), Field.Store.YES, Field.Index.ANALYZED,
                Field.TermVector.WITH_POSITIONS_OFFSETS));
                }
                else
                {
                    document.Add(new Field(dic.Key, dic.Value.Value.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                }
            }
            LuceneManage.Excute(lucene =>
            {
                lucene.AddDocument(document);
                lucene.Commit();
            });
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Dictionary<string,KeyValuePair<int,object>> GetProperty<T>(T obj)where T:new()
        {
            PropertyInfo[] pis = typeof(T).GetProperties();
            Dictionary<string, KeyValuePair<int, object>> dics = new Dictionary<string, KeyValuePair<int, object>>();
            foreach (PropertyInfo pi in pis)
            {
                object[] attributes = pi.GetCustomAttributes(typeof(Attr_LuceneAttribute), false);
                foreach (Attr_LuceneAttribute attribute in attributes)
                {
                    if (attribute == null)
                    {
                        continue;
                    }
                    object item = pi.GetValue(obj, null);
                    if (item == null)
                    {
                        item = attribute.DefaultValue;
                    }
                    dics.Add(attribute.Key, new KeyValuePair<int, object>(attribute.SplitWord, item));
                }
            }
            return dics;
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="pi">The pi.</param>
        /// <param name="rc">The source.</param>
        private static void SetPropertyValue(object instance, PropertyInfo pi, string rc)
        {
            Type t = pi.PropertyType;
            if (t.IsEnum)
            {
                int eInt;
                if (System.Enum.IsDefined(t, rc) || int.TryParse(rc, out eInt))
                {
                    pi.SetValue(instance, System.Enum.Parse(t, rc), null);
                }
                else
                {
                    pi.SetValue(instance, Activator.CreateInstance(t), null);
                }
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                Type[] typeArray = t.GetGenericArguments();
                pi.SetValue(instance, Convert.ChangeType(rc, typeArray[0]), null);
            }
            else if (t == typeof(Guid))
            {
                Guid result = default(Guid);
                Guid.TryParse(rc, out result);
                pi.SetValue(instance, result, null);
            }
            else
            {
                pi.SetValue(instance, Convert.ChangeType(rc, t), null);
            }
        }

        public static List<T> GetList<T>(string queryText, int pageIndex, int pageSize,string[] fileds, out int total)where T : new()
        {
            BooleanQuery bq = new BooleanQuery();
            QueryParser parser = null;// new QueryParser(version, field, analyzer);//一个字段查询
            parser = new MultiFieldQueryParser(Version.LUCENE_29, fileds, new PanGuAnalyzer());//多个字段查询
            Query queryKeyword = parser.Parse(queryText);
            bq.Add(queryKeyword, Occur.MUST);//与运算
            TopScoreDocCollector collector = TopScoreDocCollector.Create(pageIndex * pageSize, false);
            var directory = LuceneManage.GetConfigFilePath("IndexData");
            using (FSDirectory fsDirectory = FSDirectory.Open(new DirectoryInfo(directory), new NativeFSLockFactory()))
            {
                IndexSearcher searcher = new IndexSearcher(fsDirectory, true);//true-表示只读
                searcher.Search(bq, collector);

                if (collector == null || collector.TotalHits == 0)
                {
                    total = 0;
                    return null;
                }
                int start = pageSize * (pageIndex - 1);
                //结束数
                int limit = pageSize;
                ScoreDoc[] hits = collector.TopDocs(start, limit).ScoreDocs;
                List<T> list = new List<T>();
                int counter = 1;
                total = collector.TotalHits;
                foreach (ScoreDoc sd in hits)//遍历搜索到的结果
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        
                        PropertyInfo[] pis = typeof(T).GetProperties();
                        T obj = new T();
                        foreach (PropertyInfo pi in pis)
                        {
                            
                            object[] attributes = pi.GetCustomAttributes(typeof(Attr_LuceneAttribute), false);
                            foreach (Attr_LuceneAttribute attribute in attributes)
                            {
                                if (attribute == null)
                                {
                                    continue;
                                }
                                string value = doc.Get(attribute.Key);
                                SetPropertyValue(obj, pi, value);
                            }
                        }
                        list.Add(obj);
                      
                        //list.Add(new T_Humor_HumorInfo()
                        //{
                        //    Id = Convert.ToInt32(id),
                        //    HumorTitle = title,
                        //    HumorContent = SplitContent.HightLight(queryText, content)
                        //});
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                    counter++;
                }
                return list;
            }
        }

    }
}
