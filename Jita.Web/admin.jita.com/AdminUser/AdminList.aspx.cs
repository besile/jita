using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminUser_AdminList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<People> list = new List<People>();
        for (int i = 0; i < 50; i++)
        {
            if (i % 2 == 0)
            {
                list.Add(new People("徐兵华" + i.ToString(), i, Sex.nan, Status.有效, "徐兵华说 呵呵 第" + i + "次"));
            }
            else
            {
                list.Add(new People("徐兵华" + i.ToString(), i, Sex.nv, Status.无效, "徐兵华说 呵呵 第" + i + "次"));
            }
        }
        tableContent.DataSource = list;
        tableContent.DataBind();
    }

    public enum Sex
    {
        nan = 2,
        nv = 1,
    };

    public enum Status
    {
        有效 = 1,
        无效 = 2
    }
    public class People
    {
        public People(string name, int age, Sex sex, Status status, string Decription)
        {
            this.Name = name;
            this.Age = age;
            this.Sex = sex;
            this.Status = status;
            this.Decription = Decription;
        }


        public string Name
        { get; set; }
        public int Age
        { get; private set; }
        public Sex Sex
        { get; private set; }
        public Status Status { get; set; }

        public string Decription { set; get; }



    }
}