using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP_SD
{

    public class HomePage
    {
        private Stack<int> transaksi;

        public HomePage()
        {
            transaksi = new Stack<int>();
        }

        public void TampilkanMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Home Page ==");
                Console.WriteLine("1. Menabung");
                Console.WriteLine("2. Tampilkan Saldo");
                Console.WriteLine("3. Penarikan");
                Console.WriteLine("4. Tampilkan Transaksi");
                Console.WriteLine("5. Keluar");
                Console.Write("Masukkan pilihan [1/2/3/4]: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Menabung();
                        break;
                    case "2":
                        TampilkanSaldo();
                        break;
                    case "3":
                        Penarikan();
                        break;
                    case "4":
                        TampilkanTransaksi();
                        break;
                    case "5":
                        Console.WriteLine("Terima kasih telah menggunakan layanan kami!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nInput Tidak Valid!!\n");
                        break;
                }

                Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
                Console.ReadKey();
            }
        }

        private void Menabung()
        {
            Console.Clear();
            Console.WriteLine("=== Menabung ===");
            Console.Write("Masukkan jumlah yang ingin ditabung: ");
            if (int.TryParse(Console.ReadLine(), out int jumlah) && jumlah > 0)
            {
                Console.Write("Masukkan tanggal (DD/MM/YYYY): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tanggal))
                {
                    transaksi.Push(jumlah, tanggal);
                    Console.WriteLine("Menabung berhasil.");
                }
                else
                {
                    Console.WriteLine("Format tanggal salah.");
                }
            }
            else
            {
                Console.WriteLine("Input tidak valid. Harap masukkan angka lebih dari 0.");
            }
        }


        private void TampilkanSaldo()
        {
            Console.Clear();
            Console.WriteLine("=== Tampilkan Saldo ===");
            decimal saldo = HitungSaldo();
            Console.WriteLine($"Saldo Anda saat ini: Rp{saldo}");
        }

        private void Penarikan()
        {
            Console.Clear();
            Console.WriteLine("=== Penarikan ===");
            Console.Write("Masukkan jumlah yang ingin ditarik: ");
            if (int.TryParse(Console.ReadLine(), out int jumlah) && jumlah > 0)
            {
                Console.Write("Masukkan tanggal (DD/MM/YYYY): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tanggal))
                {
                    if (HitungSaldo() >= jumlah)
                    {
                        transaksi.Push(-jumlah, tanggal); // Menambahkan nilai negatif untuk menunjukkan penarikan
                        Console.WriteLine("Penarikan berhasil.");
                    }
                    else
                    {
                        Console.WriteLine("Saldo tidak mencukupi untuk melakukan penarikan.");
                    }
                }
                else
                {
                    Console.WriteLine("Format tanggal salah.");
                }
            }
            else
            {
                Console.WriteLine("Input tidak valid. Harap masukkan angka lebih dari 0.");
            }
        }


        private decimal HitungSaldo()
        {
            decimal totalMenabung = 0;
            decimal totalPenarikan = 0;

            foreach (var transaksi in transaksi.GetAllItems())
            {
                if (transaksi.Data > 0)
                {
                    totalMenabung += transaksi.Data;
                }
                else
                {
                    totalPenarikan -= transaksi.Data; // Mengurangi nilai negatif untuk mendapatkan jumlah penarikan
                }
            }

            return totalMenabung - totalPenarikan;
        }

        private void TampilkanTransaksi()
        {
            Console.Clear();
            Console.WriteLine("=== Riwayat Transaksi ===");
            Console.WriteLine("Tanggal\t\tJumlah\t\tJenis");
            Console.WriteLine("---------------------------------------");

            var transaksiArray = transaksi.GetAllItems();
            foreach (var transaksi in transaksiArray)
            {
                string jenis = transaksi.Data > 0 ? "Menabung" : "Penarikan";
                decimal jumlah = Math.Abs(transaksi.Data);
                Console.WriteLine($"{transaksi.Tanggal.ToString("dd/MM/yyyy")}\t{jumlah}\t\t{jenis}");
            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Saldo Akhir: Rp{HitungSaldo()}");
        }
    }
}
