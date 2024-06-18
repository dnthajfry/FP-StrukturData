using System;
using System.Collections.Generic;
using System.Linq;

namespace FP_SD
{
    public class DetailTransaksiPage
    {
        private Stack<int> transaksi;

        public DetailTransaksiPage(Stack<int> transaksi)
        {
            this.transaksi = transaksi;
        }

        
        

        public void TampilkanMenuTransaksi()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Detail Transaksi ==");
                Console.WriteLine("1. Transaksi Pemasukan");
                Console.WriteLine("2. Transaksi Pengeluaran Harian");
                Console.WriteLine("3. Transaksi Pengeluaran Bulanan");
                Console.WriteLine("4. Seluruh Transaksi");
                Console.WriteLine("5. Kembali ke Menu Utama");
                Console.Write("Masukkan pilihan [1/2/3/4/5]: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        TampilkanTransaksiPemasukan();
                        break;
                    case "2":
                        TampilkanTransaksiPengeluaranHarian();
                        break;
                    case "3":
                        TampilkanTransaksiPengeluaranBulanan();
                        break;
                    case "4":
                        TampilkanSeluruhTransaksi();
                        break;
                    case "5":
                        return; // Kembali ke menu utama
                    default:
                        Console.WriteLine("\nInput Tidak Valid!!\n");
                        break;
                }

                Console.WriteLine("Tekan tombol apa saja untuk melanjutkan...");
                Console.ReadKey();
            }
        }

        private void TampilkanTransaksiPemasukan()
        {
            Console.Clear();
            Console.WriteLine("=== Transaksi Pemasukan ===");
            Console.WriteLine("Tanggal\t\tJumlah");
            Console.WriteLine("---------------------------------------");

            var pemasukan = transaksi.GetAllItems().Where(t => t.Data > 0);
            foreach (var transaksi in pemasukan)
            {
                Console.WriteLine($"{transaksi.Tanggal.ToString("dd/MM/yyyy")}\t{transaksi.Data}");
            }

            Console.WriteLine("---------------------------------------");
        }

        private void TampilkanTransaksiPengeluaranHarian()
        {
            Console.Clear();
            Console.WriteLine("=== Transaksi Pengeluaran Harian ===");
            Console.Write("Masukkan tanggal (DD/MM/YYYY): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tanggal))
            {
                var pengeluaranHarian = transaksi.GetAllItems().Where(t => t.Tanggal.Date == tanggal.Date && t.Data < 0);
                Console.WriteLine("Tanggal\t\tJumlah");
                Console.WriteLine("---------------------------------------");
                foreach (var transaksi in pengeluaranHarian)
                {
                    Console.WriteLine($"{transaksi.Tanggal.ToString("dd/MM/yyyy")}\t{Math.Abs(transaksi.Data)}");
                }
                Console.WriteLine("---------------------------------------");
            }
            else
            {
                Console.WriteLine("Format tanggal salah.");
            }
        }

        private void TampilkanTransaksiPengeluaranBulanan()
        {
            Console.Clear();
            Console.WriteLine("=== Transaksi Pengeluaran Bulanan ===");
            Console.Write("Masukkan bulan dan tahun (MM/yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime bulanTahun))
            {
                // Prepare to filter and sort transactions based on month and negative values (expenses)
                List<int> pengeluaranBulanan = new List<int>();
                foreach (int transaction in pengeluaranBulanan)
                {
                    DateTime transactionDate = new DateTime(bulanTahun.Year, bulanTahun.Month, 1); // Use the first day of the month for comparison
                    if (transactionDate.Month == bulanTahun.Month && transaction < 0)
                    {
                        pengeluaranBulanan.Add(transaction);
                    }
                }

                // Sort transactions by ascending date
                pengeluaranBulanan.Sort(); // Sorting integers directly

                Console.WriteLine($"Transaksi Pengeluaran untuk Bulan {bulanTahun.ToString("MM/yyyy")}");
                Console.WriteLine("Tanggal\t\tJumlah");
                Console.WriteLine("---------------------------------------");

                foreach (int transaction in pengeluaranBulanan)
                {
                    // Assuming you want to display in the format "01/MM/yyyy" for simplicity
                    Console.WriteLine($"01/{bulanTahun.Month.ToString("00")}/{bulanTahun.Year}\t{Math.Abs(transaction)}");
                }

                Console.WriteLine("---------------------------------------");
            }
            else
            {
                Console.WriteLine("Format bulan dan tahun salah.");
            }
        }


        private void TampilkanSeluruhTransaksi()
        {
            Console.Clear();
            Console.WriteLine("=== Seluruh Transaksi ===");
            Console.WriteLine("Tanggal\t\tJumlah\t\tJenis");
            Console.WriteLine("---------------------------------------");

            var seluruhTransaksi = transaksi.GetAllItems();
            foreach (var transaksi in seluruhTransaksi)
            {
                string jenis = transaksi.Data > 0 ? "Pemasukan" : "Pengeluaran";
                int jumlah = Math.Abs(transaksi.Data);
                Console.WriteLine($"{transaksi.Tanggal.ToString("dd/MM/yyyy")}\t{jumlah}\t\t{jenis}");
            }

            Console.WriteLine("---------------------------------------");
        }
    }
}
