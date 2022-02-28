using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    public class DataService
    {
        public string Title = "Home";

        private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "data.json");

        public Action ToggleSidebar;

        //private UserData loadedData = null;
        public UserData Instance { get; set; }

        public Task<bool> LoadData()
        {
            try
            {

                if (File.Exists(dbPath))
                {
                    using (TextReader reader = new StreamReader(dbPath))
                    {
                        string _data = reader.ReadToEnd();
                        reader.Close();

                        var _loadedData = JsonSerializer.Deserialize<UserData>(_data);
                        Instance = _loadedData;

                        return Task.FromResult(true);
                    }
                }
                else
                {
                    Instance = new UserData();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

        }

        public void SaveData()
        {
            try
            {
                var _data = JsonSerializer.Serialize(Instance);
                File.CreateText(dbPath).Dispose();
                using (TextWriter writer = new StreamWriter(dbPath, false))
                {
                    writer.WriteLine(_data);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
