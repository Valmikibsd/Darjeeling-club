using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
namespace ClubApp.Components;

public class Left: ViewComponent
{
    db_Utility Util = new db_Utility();
    ClsUtility ClsUtil = new ClsUtility();


    //public string ProfileId { get; private set; }

    public IViewComponentResult Invoke()
    {


        ViewBag.menu = BindMenu();
        return View("Left");
    }
    public string BindMenu()
    {

        string ProfileId = HttpContext.Session.GetString("ProfileId");


        string AppPath = "";
        AppPath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
        StringBuilder str = new StringBuilder();
        string sql = "[Sp_SetMenu_GetRights_New] @ProfileId = " + ProfileId + ", @PageUrl = '', @AppPath= '" + AppPath + "',@schoolid=0";
        DataSet ds = Util.TableBind(sql);
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                str.Append(dr["MenuString"].ToString());
            }
        }
        string str1 = str.ToString();
        return (str1);
    }


}
