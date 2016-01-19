using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jita.Common
{
    /// <summary>
    /// 分页控件
    /// </summary>
    [ToolboxData("<{0}:AspNetPager runat=\"server\"> </{0}:AspNetPager>")]
    public class AspNetPager : WebControl, IPostBackEventHandler
    {
        #region 属性
        private string css = "the_pages";

        /// <summary>
        /// 分页容器DIV样式(默认:the_pages)
        /// </summary>
        public string PageDivCSS
        {
            get { return css; }
            set { css = value; }
        }


        private int totalEntriesCount;
        /// <summary>
        /// 总条目数目
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.totalEntriesCount;
            }
            set { this.totalEntriesCount = value; }
        }
        private int _countPerPage = 1;
        /// <summary>
        /// 每页条目数目
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._countPerPage;
            }
            set
            {
                this._countPerPage = value;
            }
        }
        private int _pageNo;
        /// <summary>
        /// 页码
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (_pageNo == 0)
                {
                    if (HttpContext.Current.Request.QueryString["page"] != null)
                    {
                        int.TryParse(HttpContext.Current.Request.QueryString["page"].ToString(), out this._pageNo);
                    }
                    if (this._pageNo == 0) this._pageNo = 1;
                }
                return _pageNo;
            }
            set
            {
                _pageNo = value;
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (RecordCount == 0)
                    return 1;
                return (int)Math.Ceiling((double)RecordCount / (double)PageSize);
            }
        }

        private bool _DisplayTotalPageCount = true;

        /// <summary>
        /// 显示总页数
        /// </summary>
        public bool DisplayTotalPageCount
        {
            get { return _DisplayTotalPageCount; }
            set { _DisplayTotalPageCount = value; }
        }


        private string _UrlRewritePattern;

        /// <summary>
        /// 地址模式
        /// </summary>
        public string UrlRewritePattern
        {
            get { return _UrlRewritePattern; }
            set { _UrlRewritePattern = value; }
        }

        private bool isShowInput = true;
        /// <summary>
        /// 是否现实输入框
        /// </summary>
        public bool IsShowInput { get { return isShowInput; } set { isShowInput = value; } }

        private bool isShowPreNext = true;
        /// <summary>
        /// 是否显示上下页
        /// </summary>
        public bool IsShowPreNext { get { return isShowPreNext; } set { isShowPreNext = value; } }

        PageChangeMode _Mode = PageChangeMode.Url;
        /// <summary>
        /// 页码改变模式
        /// 默认Url请求方式
        /// </summary>
        public PageChangeMode Mode { get { return _Mode; } set { _Mode = value; } }

        #endregion

        #region Enum
        /// <summary>
        /// 页码改变模式
        /// </summary>
        public enum PageChangeMode
        {
            /// <summary>
            /// url请求方式
            /// </summary>
            Url,
            /// <summary>
            /// postback事件方式
            /// </summary>
            PostBack,
        }
        #endregion

        /// <summary>
        /// 渲染控件内容
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.RenderQueryString(writer);
        }

        #region PageChangeMode_Get
        /// <summary>
        /// 输出页码块
        /// </summary>
        /// <param name="no">页码</param>
        /// <param name="writer">writer</param>
        private void RenderPageNoBlock(int no, HtmlTextWriter writer)
        {
            PagerItem item = new PagerItem();
            item.Text = no.ToString();
            if (no == this.CurrentPageIndex)
            {
                item.ClassName = "linknow";
            }
            else
            {
                item.Href = GetHref(no, UrlRewritePattern);
            }
            item.AppendTo(writer);
        }
        private void RenderPreDotBlock(HtmlTextWriter writer)
        {
            if (this.CurrentPageIndex >= 5)
            {
                writer.Write("<em>...</em>");
            }
        }
        private void RenderPostDotBlock(HtmlTextWriter writer)
        {
            if (this.CurrentPageIndex + 4 <= this.PageCount)
            {
                writer.Write("<em>...</em>");
            }
        }
        private void RenderPageMiddleBlock(HtmlTextWriter writer)
        {
            if (this.CurrentPageIndex >= 4)
            {
                if (this.CurrentPageIndex <= this.PageCount - 3)
                {
                    for (int i = this.CurrentPageIndex - 2; i <= this.CurrentPageIndex + 2; i++)
                    {
                        this.RenderPageNoBlock(i, writer);
                    }
                }
                else
                {
                    for (int i = this.PageCount - 5; i <= this.PageCount - 1; i++)
                    {
                        this.RenderPageNoBlock(i, writer);
                    }
                }
            }
            else
            {
                for (int i = 2; i <= 6; i++)
                {
                    this.RenderPageNoBlock(i, writer);
                }
            }
        }
        private void RenderPrevPageBlock(HtmlTextWriter writer)
        {
            bool gltOne = CurrentPageIndex > 1;
            PagerItem item = new PagerItem();
            if (gltOne) item.Href = GetHref(CurrentPageIndex - 1, UrlRewritePattern);
            item.ClassName = gltOne ? "preview_on" : "preview_off";
            item.Tag = gltOne ? HtmlTextWriterTag.A : HtmlTextWriterTag.Span;
            item.Text = "上一页";
            item.AppendTo(writer);
        }

        string GetHref(int pageIndex, string urlRewritePattern = null)
        {
            if (Mode == PageChangeMode.Url)
            {
                return string.Format(UrlRewritePattern, pageIndex);
            }
            else
            {
                return Page.ClientScript.GetPostBackClientHyperlink(this, pageIndex.ToString());
            }
        }
        private void RenderNextPageBlock(HtmlTextWriter writer)
        {
            bool isEnd = this.CurrentPageIndex >= this.PageCount;
            PagerItem item = new PagerItem();
            item.Text = "下一页";
            if (isEnd)
            {
                item.ClassName = "next_off";
                item.Tag = HtmlTextWriterTag.Span;
            }
            else
            {
                item.ClassName = "next_on";
                item.Tag = HtmlTextWriterTag.A;
                item.Href = GetHref(CurrentPageIndex + 1, UrlRewritePattern);
            }
            item.AppendTo(writer);
        }
        private void RenderQueryString(HtmlTextWriter writer)
        {
            if (this.PageCount > 1)
            {
                //int prev = PageNo - 1;
                //int next = PageNo + 1;
                writer.Write(string.Format("<div class='{0}'><div>", PageDivCSS));
                if (DisplayTotalPageCount)
                {
                    writer.Write(string.Format("<span class=\"num\">共{0}页</span>", PageCount));
                }
                if (IsShowPreNext) this.RenderPrevPageBlock(writer);
                if (this.PageCount <= 9)
                {
                    for (int i = 1; i <= this.PageCount; i++)
                    {
                        this.RenderPageNoBlock(i, writer);
                    }
                }
                else
                {
                    this.RenderPageNoBlock(1, writer);
                    this.RenderPreDotBlock(writer);
                    this.RenderPageMiddleBlock(writer);
                    this.RenderPostDotBlock(writer);
                    this.RenderPageNoBlock(this.PageCount, writer);
                }
                if (IsShowPreNext) this.RenderNextPageBlock(writer);
                if (IsShowInput) writer.Write("<span class=\"num\">页码:</span> <input class=\"text\" name=\"\" type=\"text\"/><input  type=\"button\" value=\"转到\" class=\"button\" onclick=\"goPage(this.previousSibling.value)\" class=\"ok\" />");
                writer.Write("</div></div>");
                writer.Write(@"
<script type='text/javascript'>
function goPage(pageindex)
{
    var pageCount = " + PageCount + @";
    if(pageindex==''||isNaN(pageindex)||pageindex<=0||pageindex>pageCount)
    {
        return;
    }
    else
    {
        
        var url = """ + GetHref(-99999999, UrlRewritePattern) + @""";
        url = url.replace('-99999999',pageindex);
        if(url.indexOf('javascript:')==0){
            eval(url);
        }else{
            location.href = url;
        }
        
    }
}
</script>
");
            }
        }
        #endregion

        #region PageChangeMode_PostBack

        public delegate void PageChangingEventHandler(object src, PageChangingEventArgs e);
        /// <summary>
        ///  PostBack方式分页时，当页导航元素之一被单击或用户手工输入页索引提交时发生
        /// </summary>
        public event PageChangingEventHandler PageChanging;
        /// <summary>
        ///  PostBack方式分页时，当页导航元素之一被单击或用户手工输入页索引提交完成时发生
        /// </summary>
        public event EventHandler PageChanged;
        public void RaisePostBackEvent(string args)
        {
            int newPageIndex = Common.TypeParse.StrToInt(args, CurrentPageIndex);
            PageChangingEventArgs pcArgs = new PageChangingEventArgs() { NewPageIndex = newPageIndex };
            OnPageChanging(this, pcArgs);
        }
        protected virtual void OnPageChanging(object obj, PageChangingEventArgs e)
        {
            CurrentPageIndex = e.NewPageIndex;
            if (PageChanging != null)
            {
                PageChanging(this, e);
                OnPageChanged(EventArgs.Empty);
            }
        }
        protected virtual void OnPageChanged(EventArgs e)
        {
            if (PageChanged != null)
            {
                PageChanged(this, e);
            }
        }
        #endregion
    }

    /// <summary>
    /// 分页项
    /// </summary>
    class PagerItem
    {
        public int Index { get; set; }
        public string Text { get; set; }
        public string ClassName { get; set; }
        public string Href { get; set; }
        public HtmlTextWriterTag Tag = HtmlTextWriterTag.A;
        public void AppendTo(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Href)) writer.AddAttribute(HtmlTextWriterAttribute.Href, Href);
            if (!string.IsNullOrEmpty(ClassName)) writer.AddAttribute(HtmlTextWriterAttribute.Class, ClassName);
            writer.RenderBeginTag(Tag);
            if (!string.IsNullOrEmpty(Text)) writer.Write(Text);
            writer.RenderEndTag();
        }
    }

    public sealed class PageChangingEventArgs : EventArgs
    {
        public int NewPageIndex { get; set; }
    }

    /// <summary>
    /// 简易分页控件
    /// </summary>
    [ToolboxData("<{0}:SimplePager runat=\"server\"> </{0}:SimplePager>")]
    public class SimplePager : WebControl
    {
        #region 属性
        private string css = "rightpart2";

        /// <summary>
        /// 分页容器DIV样式(默认:the_pages)
        /// </summary>
        public string PageDivCSS
        {
            get { return css; }
            set { css = value; }
        }

        private int totalEntriesCount;
        /// <summary>
        /// 总条目数目
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.totalEntriesCount;
            }
            set { this.totalEntriesCount = value; }
        }
        public string RecordCss
        {
            get;
            set;
        }
        private int _countPerPage = 1;
        /// <summary>
        /// 每页条目数目
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._countPerPage;
            }
            set
            {
                this._countPerPage = value;
            }
        }
        private int _pageNo;
        /// <summary>
        /// 页码
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (_pageNo == 0)
                {
                    if (HttpContext.Current.Request.QueryString["page"] != null)
                    {
                        int.TryParse(HttpContext.Current.Request.QueryString["page"].ToString(), out this._pageNo);
                    }
                    if (this._pageNo == 0) this._pageNo = 1;
                }
                return _pageNo > PageCount ? PageCount : _pageNo;
            }
            set
            {
                _pageNo = value;
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (RecordCount == 0)
                    return 1;
                return (int)Math.Ceiling((double)RecordCount / (double)PageSize);
            }
        }

        private string _UrlRewritePattern;

        public string UrlRewritePattern
        {
            get { return _UrlRewritePattern; }
            set { _UrlRewritePattern = value; }
        }

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            this.RenderQueryString(writer);
        }

        private void RenderLastPageBlock(HtmlTextWriter writer)
        {
            if (this.CurrentPageIndex > 1)
            {
                writer.Write(string.Format("<a href=\"{0}\">上一页</a>", string.Format(UrlRewritePattern, (this.CurrentPageIndex - 1))));
            }
            else
            {
                writer.Write("<a href='javascript:void(0)' class=\"nofanye\">上一页</a>");
            }
        }
        private void RenderNextPageBlock(HtmlTextWriter writer)
        {
            if (this.CurrentPageIndex < this.PageCount)
            {
                writer.Write(string.Format("<a href=\"{0}\">下一页</a>", string.Format(UrlRewritePattern, (this.CurrentPageIndex + 1))));
            }
            else
            {
                writer.Write("<a class=\"nofanye\" href='javascript:void(0)'>下一页</a>");
            }
        }
        private void RenderQueryString(HtmlTextWriter writer)
        {
            if (this.PageCount > 1)
            {
                writer.Write(string.Format("<div class='{0}'>", PageDivCSS));
                writer.Write(string.Format("<div><span><span  style='{2}'>共<strong>{0}</strong>页，</span>当前第<strong>{1}</strong>页</span>", PageCount, CurrentPageIndex, RecordCss));
                this.RenderLastPageBlock(writer);
                this.RenderNextPageBlock(writer);
                writer.Write("</div></div>");
            }
        }
    }
}
