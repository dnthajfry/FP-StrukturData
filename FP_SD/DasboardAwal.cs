using FP_SD;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP
{
    public class DashboardAwal
    {
        
        
        public static Hash akun = new Hash();
        public static void Daftar()
        {
            Console.WriteLine("\n==== Menu Daftar ====");
            Console.WriteLine("Masukkan Username : ");
            string Username = Console.ReadLine();
            Console.WriteLine("Masukkan Password : ");
            string Password = Console.ReadLine();

            if (akun.CekDaftar(Username, Password))
            {
                Console.WriteLine("\nAkun Berhasil Terdaftar!!\n");
            }
            else
            {
                Console.WriteLine("\nAkun Gagal Terdaftar!!\n");
            }
            Program.Main();
        }
        public static void Masuk()
        {
            
            Console.WriteLine("Masukkan Username :");
            string Username = Console.ReadLine();
            Console.WriteLine("Masukkan Password :");
            string Password = Console.ReadLine();
            
            if (akun.SearchMasukPelanggan(Username, Password))
            {
            Console.WriteLine("\nBerhasil Masuk!!\n");
            HomePage hp = new HomePage();
            hp.TampilkanMenu();
            }
            else
            {
            Console.WriteLine("\nGagal Masuk!! Username atau Password salah!!\n");
            Masuk();
            }
        }
    }
}
