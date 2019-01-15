using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitModel
{
    class Program
    {/// <summary>
     /// System.Threading sinifi eklenmeden çalişmaz
     /// En karmaşık yöntemdir. Her asenkron yordam için zaman aşımı olan bir bekleme yöntemi tanimlanir.
     /// Prosesler ya sonuç döndürünceye kadar yada timeout alana kadar uygulama bekler.
     /// 3 farkli yöntem kullanilir. 
     /// WaitOne() --> Tek bir asenkron SqlCommand Nesnesi için kullanilir.
     /// WaitAll() --> Bütün asenkron çaürılar için kullanilir.
     /// WaitAny() --> Asenkron çağrilarin bitip bitmediği sırasıyla kontol edilerek herhangi biri bitinceye kadar bekletilir
     /// 
     /// IAsyncResult Nesnesinin AsyncWaitHandle özelliği kullanilir.
     /// 
     /// WaitAny ve WaitAll yordamlari parametre olarak System.Threading.WaitHandle alir ve bu sınıf işletim sistemi seviyesinde özel bir siniftir.
     /// 
     /// WaitOne poolingModel'e alternatif kullanilabilecek bir yöntemdir.
     /// poolingModel'de asenkron işlemin bitip bitmediğini tekrar tekrar IsComplate değeri kontrol edilerek anlaşılıyor ve 
     /// while döngüsünde IsComplete true oluncaya kadar bu kontrol devam ediyordu. Uygulamayi bu şekilde döngüyle askiya almak 
     /// kötü  bir yöntem. WaitOne metodunda ise uygulamayi bekletmek bu metodu kullanmak yeterli olacaktir.
     /// Bu daha çok Thread.Sleep yada Application.DoEvents metotlarina benzer.
     /// 
     /// </summary>
     /// 
        public static SqlConnection con;


        static void Main(string[] args)
        {
            //poolingModel();           
            waitModel();

        }
        public static void waitModel()
        {

            con = new SqlConnection("Data Source=.;Initial Catalog=NORTHWIND;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("waitfor delay '00:00:05';Update customers set region ='qwe' ", con);

            con.Open();

            //Asenckron işlemi başlatalim
            IAsyncResult res = cmd.BeginExecuteNonQuery();

            //waitHandle nesnesini alalim
            WaitHandle wait = res.AsyncWaitHandle;


            Console.WriteLine("Diger işlemler çalişiyor..");

            //Burada asenkron işleminin bitmesini bekleyelim
            //o nedenle programi bekletelim

            wait.WaitOne();

            Console.WriteLine($"işlem tamam .Toplam Etkilenen Kayit:{cmd.EndExecuteNonQuery(res)}");
            cmd.Dispose();
            con.Close();

        }
    }
}
