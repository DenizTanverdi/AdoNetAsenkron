using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace UsingAsyncCallback2
{
    public delegate DataSet Temsilci(string sorguCumlesi);

    class Yurutucu
    {
        public DataSet ds;

        public void Baslat(Temsilci t, string sorgu)
        {
            t.BeginInvoke(sorgu, new AsyncCallback(Bitir), t);
        }

        public void Bitir(IAsyncResult ia)
        {
            Temsilci t = (Temsilci)ia.AsyncState;
            ds = t.EndInvoke(ia);
            Console.WriteLine(ds.Tables[0].Rows[1][5] + " " + ds.Tables[0].Rows[1][6] + " " + ds.Tables[0].Rows[1][7]);
        }

        public DataSet SonuclariAl(string sorgu)
        {
            SqlConnection con = new SqlConnection("Data Source=SEM-BILGISAYAR;Initial Catalog=test;User ID=test2;Password=test2");
            SqlDataAdapter da = new SqlDataAdapter(sorgu, con);
            DataSet dsSonucKumesi = new DataSet();
            da.Fill(dsSonucKumesi);
            return dsSonucKumesi;
        }
    }

    class AnaProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            Yurutucu yrtc = new Yurutucu();
            string sorgu = @"SELECT TOP 1000 [BusinessEntityID]
                              ,[Name]
                              ,[AddressType]
                              ,[AddressLine1]
                              ,[AddressLine2]
                              ,[City]
                              ,[StateProvinceName]
                              ,[PostalCode]
                              ,[CountryRegionName]
                          FROM [AdventureWorks].[Purchasing].[vVendorWithAddresses]";
            Temsilci t = new Temsilci(yrtc.SonuclariAl);
            yrtc.Baslat(t, sorgu);
            for (int i = 1; i < 3000; i++)
            {
                Console.Write(".");
            }
            Console.ReadLine();
        }
    }
}