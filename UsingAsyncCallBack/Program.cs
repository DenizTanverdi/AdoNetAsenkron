using System;

namespace UsingAsyncCallback
{
    public delegate void Temsilci();

    class Yurutucu
    {
        public void Calistir(Temsilci t)
        {
            t.BeginInvoke(new AsyncCallback(SonuclariAl), t);
        }

        public void SonuclariAl(IAsyncResult ia)
        {
            Temsilci t = (Temsilci)ia.AsyncState;
            t.EndInvoke(ia);
        }

        public void Islemler()
        {
            Console.WriteLine("Async yürütülecek metod");
        }
    }

    class AnaProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            Yurutucu yrtc = new Yurutucu();

            #region Asenkron kullanıldığında

            Temsilci t = new Temsilci(yrtc.Islemler);
            yrtc.Calistir(t);
            Console.WriteLine("Diğer kod satırları...");

            #endregion

            #region Asenkron kullanılmadan

            //yrtc.Islemler();
            //Console.WriteLine("Diğer kod satırları...");

            #endregion

            Console.ReadLine();
        }
    }
}