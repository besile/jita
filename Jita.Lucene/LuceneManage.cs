using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Store;

namespace Jita.LuceneManger
{
    public class LuceneManage
    {
        public static void Excute(Action<IndexWriter> logic)
        {
            if (logic == null)
            {
                throw new ArgumentNullException("logic");
            }

            IndexWriter writer = InitIndexWrite();
            using (writer)
            {
                logic(writer);
            }

        }

        public static TReturn Excute<TReturn>(Func<IndexWriter, TReturn> logic)
        {
            if (logic == null)
            {
                throw new ArgumentNullException("logic");
            }
            IndexWriter writer = InitIndexWrite();
            using (writer)
            {
                return logic(writer);
            }
        }

        private static IndexWriter InitIndexWrite()
        {
            string indexPath = GetConfigFilePath("IndexData"); //Context.Server.MapPath("~/IndexData");
            if (!System.IO.Directory.Exists(indexPath))//判断文件夹是否存在 
            {
                System.IO.Directory.CreateDirectory(indexPath);//不存在则创建文件夹 
            }
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isExist = IndexReader.IndexExists(directory);
            if (isExist)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            //writer = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.LIMITED);//false表示追加（true表示删除之前的重新写入）
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
            return writer;
        }
        public static string GetConfigFilePath(string filePath)
        {
            if (filePath == null || filePath.Length == 0) return null;
            //return HttpContext.Current.Server.MapPath("~/IndexData");
            return string.Concat(AppDomain.CurrentDomain.BaseDirectory, filePath);
        }
    }
}
