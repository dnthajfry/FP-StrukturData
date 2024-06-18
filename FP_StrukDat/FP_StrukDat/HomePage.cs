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
        private int batasPengeluaranHarian; // Batas pengeluaran harian
        private int batasPengeluaranBulanan; // Batas pengeluaran bulanan

        public HomePage()
        {
            transaksi = new Stack<int>();
            batasPengeluaranHarian = 0; // Nilai default
            batasPengeluaranBulanan = 0; // Nilai default
        }

        public void TampilkanMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Home Page ==");
                Console.WriteLine("1. Pemasukan");
                Console.WriteLine("2. Tampilkan Saldo");
                Console.WriteLine("3. Pengeluaran");
                Console.WriteLine("4. Tampilkan Transaksi");
                Console.WriteLine("5. Atur Batas Pengeluaran Harian");
                Console.WriteLine("6. Atur Batas Pengeluaran Bulanan");
                Console.WriteLine("7. Keluar");
                Console.Write("Masukkan pilihan [1/2/3/4/5/6/7]: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Pemasukan();
                        break;
                    case "2":
                        TampilkanSaldo();
                        break;
                    case "3":
                        Pengeluaran();
                        break;
                    case "4":
                        DetailTransaksiPage dtp = new DetailTransaksiPage(transaksi);
                        dtp.TampilkanMenuTransaksi();
                        break;
                    case "5":
                        AturBatasPengeluaranHarian();
                        break;
                    case "6":
                        AturBatasPengeluaranBulanan();
                        break;
                    case "7":
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

        private void Pemasukan()
        {
            Console.Clear();
            Console.WriteLine("=== Pemasukan ===");
            Console.Write("Masukkan jumlah yang ingin dimasukkan: ");
            if (int.TryParse(Console.ReadLine(), out int jumlah) && jumlah > 0)
            {
                Console.Write("Masukkan tanggal (DD/MM/YYYY): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tanggal))
                {
                    transaksi.Push(jumlah, tanggal);
                    Console.WriteLine("Pemasukan berhasil.");
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
            int saldo = HitungSaldo();
            Console.WriteLine($"Saldo Anda saat ini: Rp{saldo}");
        }

        private void Pengeluaran()
        {
            Console.Clear();
            Console.WriteLine("=== Pengeluaran ===");
            Console.Write("Masukkan jumlah yang ingin dikeluarkan: ");
            if (int.TryParse(Console.ReadLine(), out int jumlah) && jumlah > 0)
            {
                Console.Write("Masukkan tanggal (DD/MM/YYYY): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime tanggal))
                {
                    int totalPengeluaranHariIni = HitungPengeluaranHarian(tanggal);
                    int totalPengeluaranBulanIni = HitungPengeluaranBulanan(tanggal);
                    int sisaHarian = batasPengeluaranHarian - (totalPengeluaranHariIni + jumlah);
                    int sisaBulanan = batasPengeluaranBulanan - (totalPengeluaranBulanIni + jumlah);

                    if (totalPengeluaranHariIni + jumlah > batasPengeluaranHarian)
                    {
                        Console.WriteLine($"Pengeluaran gagal. Anda telah mencapai batas pengeluaran harian. Sisa batas pengeluaran hari ini: Rp{batasPengeluaranHarian - totalPengeluaranHariIni}");
                    }
                    else if (totalPengeluaranBulanIni + jumlah > batasPengeluaranBulanan)
                    {
                        Console.WriteLine($"Pengeluaran gagal. Anda telah mencapai batas pengeluaran bulanan. Sisa batas pengeluaran bulan ini: Rp{batasPengeluaranBulanan - totalPengeluaranBulanIni}");
                    }
                    else if (HitungSaldo() >= jumlah)
                    {
                        Console.WriteLine($"Sisa batas pengeluaran hari ini: Rp{sisaHarian}");
                        Console.WriteLine($"Sisa batas pengeluaran bulan ini: Rp{sisaBulanan}");
                        transaksi.Push(-jumlah, tanggal); // Menambahkan nilai negatif untuk menunjukkan pengeluaran
                        Console.WriteLine("Pengeluaran berhasil.");
                    }
                    else
                    {
                        Console.WriteLine("Saldo tidak mencukupi untuk melakukan pengeluaran.");
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

        private void AturBatasPengeluaranHarian()
        {
            Console.Clear();
            Console.WriteLine("=== Atur Batas Pengeluaran Harian ===");
            Console.Write("Masukkan batas pengeluaran harian yang baru: ");
            if (int.TryParse(Console.ReadLine(), out int batasBaru) && batasBaru > 0)
            {
                batasPengeluaranHarian = batasBaru;
                Console.WriteLine($"Batas pengeluaran harian berhasil diatur menjadi: Rp{batasPengeluaranHarian}");
            }
            else
            {
                Console.WriteLine("Input tidak valid. Harap masukkan angka lebih dari 0.");
            }
        }

        private void AturBatasPengeluaranBulanan()
        {
            Console.Clear();
            Console.WriteLine("=== Atur Batas Pengeluaran Bulanan ===");
            Console.Write("Masukkan batas pengeluaran bulanan yang baru: ");
            if (int.TryParse(Console.ReadLine(), out int batasBaru) && batasBaru > 0)
            {
                batasPengeluaranBulanan = batasBaru;
                Console.WriteLine($"Batas pengeluaran bulanan berhasil diatur menjadi: Rp{batasPengeluaranBulanan}");
            }
            else
            {
                Console.WriteLine("Input tidak valid. Harap masukkan angka lebih dari 0.");
            }
        }

        private int HitungPengeluaranHarian(DateTime tanggal)
        {
            int totalPengeluaran = 0;

            foreach (var transaksi in transaksi.GetAllItems())
            {
                if (transaksi.Tanggal.Date == tanggal.Date && transaksi.Data < 0)
                {
                    totalPengeluaran += Math.Abs(transaksi.Data);
                }
            }

            return totalPengeluaran;
        }

        private int HitungPengeluaranBulanan(DateTime tanggal)
        {
            int totalPengeluaran = 0;

            foreach (var transaksi in transaksi.GetAllItems())
            {
                if (transaksi.Tanggal.Month == tanggal.Month && transaksi.Data < 0)
                {
                    totalPengeluaran += Math.Abs(transaksi.Data);
                }
            }

            return totalPengeluaran;
        }

        private int HitungSaldo()
        {
            int totalPemasukan = 0;
            int totalPengeluaran = 0;

            foreach (var transaksi in transaksi.GetAllItems())
            {
                if (transaksi.Data > 0)
                {
                    totalPemasukan += transaksi.Data;
                }
                else
                {
                    totalPengeluaran -= transaksi.Data; // Mengurangi nilai negatif untuk mendapatkan jumlah pengeluaran
                }
            }

            return totalPemasukan - totalPengeluaran;
        }

        
    }
}
