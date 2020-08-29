using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void GetData()
        {
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";

            try
            {
                XDocument xDoc = XDocument.Load(url);
                XElement xe = xDoc.Element("Tarih_Date");

                if (xe != null)
                {
                    DateTime date = DateTime.ParseExact(xe.Attribute("Date").Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog=TCMB; Integrated Security=true;");

                    conn.Open();
                    int i = 0;
                    foreach (XElement xeCurrency in xe.Elements())
                    {
                        XElement xeName = xeCurrency.Element("CurrencyName"),
                            xeForexBuying = xeCurrency.Element("ForexBuying"),
                            xeForexSelling = xeCurrency.Element("ForexSelling");

                        SqlCommand cmd = new SqlCommand("INSERT INTO Kurlar (Tarih, Kod, Adi, Alis, Satis) VALUES(@Tarih, @Kod, @Adi, @Alis, @Satis)", conn);
                        cmd.Parameters.Add("Tarih", date);
                        cmd.Parameters.Add("Kod", xeCurrency.Attribute("CurrencyCode").Value);
                        cmd.Parameters.Add("Adi", xeName?.Value);
                        cmd.Parameters.Add("Alis", !string.IsNullOrEmpty(xeForexBuying.Value) ? Convert.ToDecimal(xeForexBuying.Value.Replace(".", ",")) : 0);
                        cmd.Parameters.Add("Satis", !string.IsNullOrEmpty(xeForexSelling.Value) ? Convert.ToDecimal(xeForexSelling.Value.Replace(".", ",")) : 0);


                        cmd.ExecuteNonQuery();
                        i++;
                        if (i == 12)
                            break;
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Kurlar güncelleniyor...");

            Task.Delay(new TimeSpan(0, 5, 0)).ContinueWith(o =>
            {
                GetData();
            });
            Console.WriteLine("Kurlar güncellendi");
            Console.ReadLine();
        }
    }
}