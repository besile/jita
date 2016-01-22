using System;
using System.Collections.Generic;
using System.IO;
using Jita.Data.Model;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace Jita.LuceneManger
{
    public class HumorLucene
    {
        //临时数据代替表单提交
        /// <summary>
        /// 单个创建索引
        /// </summary>
        /// <param name="humor"></param>
        public static void InsertToIndex(T_Humor_HumorInfo humor)
        {
            LuceneManage.Excute(lucene =>
            {
                Document document = new Document();
                document.Add(new Field("id", humor.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("title", humor.HumorTitle, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", humor.HumorContent, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                lucene.AddDocument(document);
                lucene.Commit();
            });
        }

        public static void MultiInsertToIndex(List<T_Humor_HumorInfo> humorList)
        {
            List<Document> docs = new List<Document>(humorList.Count);
            foreach (var humor in humorList)
            {
                Document document = new Document();
                document.Add(new Field("id", humor.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("title", humor.HumorTitle, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", humor.HumorContent, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                docs.Add(document);
            }
            foreach (var document in docs)
            {
                Document document1 = document;
                LuceneManage.Excute(lucene =>
                {
                    lucene.AddDocument(document1);
                    lucene.Commit();
                });
            }
        }

        public static bool UpdateIndex(T_Humor_HumorInfo humor)
        {
            return LuceneManage.Excute<bool>(lucene =>
            {
                Term term = new Term("id", humor.Id.ToString());
                lucene.DeleteDocuments(term);
                var success = lucene.HasDeletions();
                if (success == false)
                {
                    return false;
                }
                Document document = new Document();
                document.Add(new Field("id", humor.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("title", humor.HumorTitle, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("content", humor.HumorContent, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
                lucene.AddDocument(document);
                lucene.Commit();
                return true;
            });
        }

        public static List<T_Humor_HumorInfo> GetHumorList(string queryText, int pageIndex, int pageSize, out int total)
        {
            BooleanQuery bq = new BooleanQuery();
            string[] fileds = { "title", "content" };//查询字段
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
                List<T_Humor_HumorInfo> list = new List<T_Humor_HumorInfo>();
                int counter = 1;
                total = collector.TotalHits;
                foreach (ScoreDoc sd in hits)//遍历搜索到的结果
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        string id = doc.Get("id");
                        string title = doc.Get("title");
                        string content = doc.Get("content");

                        //PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
                        //PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new PanGu.Segment());
                        //highlighter.FragmentSize = 50;
                        //content = highlighter.GetBestFragment(keyword, content);
                        //string titlehighlight = highlighter.GetBestFragment(keyword, title);
                        //if (titlehighlight != "") title = titlehighlight;

                        list.Add(new T_Humor_HumorInfo()
                        {
                            Id = Convert.ToInt32(id),
                            HumorTitle = title,
                            HumorContent = SplitContent.HightLight(queryText, content)
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    counter++;
                }
                return list;
            }
        }
    }
}
