using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["des"]!=null)
            {
                var lists =  Request["des"].Split(',').ToList();
                List<string> res = new List<string>();
                foreach (var item in lists)
                {
                   var result =  StringHelper.DesEncrypt(item);
                    res.Add(result);
                }
                Response.Write(string.Join(",",res.ToArray()));
            }

            if (Request["urlencode"]!=null)
            {
                var url = Request["urlencode"].ToString();

                byte[] bpath = Convert.FromBase64String(url);
                var   strPath = System.Text.ASCIIEncoding.Default.GetString(bpath);
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(strPath);
               
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
               
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.Default))
                {
                    Response.Write(reader.ReadToEnd());
                }

            }



        }
    }
}