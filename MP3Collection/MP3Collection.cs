using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace MP3Collection {
    public class MP3Collection : ObservableCollection<MP3File>, ISupportInitialize  {
        public string DirectoryPath { get; set; }
        public bool ExpandFilders { get; set; }
        public string DefaultAlbumArt { get; set; }

        public MP3Collection() {
            DirectoryPath = Directory.GetCurrentDirectory();
        }

        public void BeginInit() {
            
        }

        public void EndInit() {
            
            var files = Directory.EnumerateFiles(DirectoryPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".mp3")).ToList();

            foreach (var item in files) {
                this.Add(new MP3File(item, DefaultAlbumArt));
            }
        }
    }
}
