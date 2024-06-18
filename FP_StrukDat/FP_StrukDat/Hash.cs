using System;

namespace FP
{
    public class HashTableEntry
    {
        public string Username;
        public string Password;

        public HashTableEntry(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }

    public class Hash
    {
        private HashTableEntry[] akun;
        private int size = 100;
        private int count;

        public Hash()
        {
            akun = new HashTableEntry[size];
        }

        private int GetHash(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash += c;
            }
            return hash % size;
        }

        public bool CekDaftar(string username, string password)
        {
            string key = username + password;
            int index = GetHash(key);
            int originalIndex = index;
            bool foundEmptySlot = false;

            while (akun[index] != null && (akun[index].Username != username || akun[index].Password != password))
            {
                index = (index + 1) % size;
                if (index == originalIndex)
                {
                    return false;
                }
            }
            if (akun[index] == null)
            {
                akun[index] = new HashTableEntry(username, password);
                count++;
                foundEmptySlot = true;
            }
            return foundEmptySlot;
        }

        public bool SearchMasukPelanggan(string username, string password)
        {
            string key = username + password;
            int index = GetHash(key);
            int originalIndex = index;

            while (akun[index] != null)
            {
                if (akun[index].Username == username && akun[index].Password == password)
                {
                    return true;
                }
                index = (index + 1) % size;
                if (index == originalIndex)
                {
                    break;
                }
            }
            return false;
        }

        public HashTableEntry[] GetAllAccounts()
        {
            return akun;
        }

        public bool Remove(string username, string password)
        {
            string key = username + password;
            int index = GetHash(key);
            int originalIndex = index;

            while (akun[index] != null)
            {
                if (akun[index].Username == username && akun[index].Password == password)
                {
                    akun[index] = null;
                    count--;
                    Rehash(index);
                    return true;
                }
                index = (index + 1) % size;
                if (index == originalIndex)
                {
                    break;
                }
            }
            return false;
        }

        private void Rehash(int startIndex)
        {
            int index = (startIndex + 1) % size;
            while (akun[index] != null)
            {
                HashTableEntry entry = akun[index];
                akun[index] = null;
                count--;
                CekDaftar(entry.Username, entry.Password); // Meng-insert entry kembali
                index = (index + 1) % size;
            }
        }

        private void Resize()
        {
            int newSize = size * 2;
            HashTableEntry[] newAkun = new HashTableEntry[newSize];

            foreach (var entry in akun)
            {
                if (entry != null)
                {
                    string key = entry.Username + entry.Password;
                    int index = GetHash(key);
                    while (newAkun[index] != null)
                    {
                        index = (index + 1) % newSize;
                    }
                    newAkun[index] = entry;
                }
            }

            akun = newAkun;
            size = newSize;
        }
    }
}
