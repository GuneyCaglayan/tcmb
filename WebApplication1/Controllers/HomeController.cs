using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog=TCMB; Integrated Security=true;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Kurlar", conn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            conn.Close();

            return View(dt);
        }
    }
}