using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoolingModel

{
        class Program
    {
        public static SqlConnection con;
        static void Main(string[] args)
        {
            poolingModel();

            Console.ReadLine();
           

        }

        /// <summary>
        /// Aşagidaki örnekte asenkron işlemler başlatildiktan sonra işin bitip bitmediği 
        /// uygulama içerisinde sürekli denetlenir. Bu amaçla yordami bize IAsyncResult arabiriminde
        /// IsComplate özelliği true değeri gönderene kadar tekrar tekrar sınanır.
        /// Bu model kısa süren asenkron işlemleri için iyidir.
        /// </summary>
        public static void poolingModel()
        {

            con = new SqlConnection("Data Source=SEM-BILGISAYAR;Initial Catalog=test;User ID=test2;Password=test2");
            SqlCommand cmd = new SqlCommand("Update customers set region ='qwe' ", con);

            con.Open();

            IAsyncResult res = cmd.BeginExecuteNonQuery();

            // Tabloda çok fazla kayit olursa guncelleme uzun sürecek .
            // Fakat aşagidaki kodlar da bu sırada çalişabilecektir.
            while (!res.IsCompleted)
            {
                Console.WriteLine("İslem devam ediyor..");
            }
            Console.WriteLine($"işlem tamamlandiç Etkilenen Kayit sayisi :{cmd.EndExecuteNonQuery(res)}");
            cmd.Dispose();
            con.Close();
        }

      

    }
}
