using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Collection {
    public class MP3File {

        public string FileName { get; set; }
        public bool HasTag { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Year { get; set; }
        public string Comment { get; set; }
        public string AlbumArt { get; set; }
        public int Track { get; set; }

        public MP3File(string path, string defaultAlbumArt) {
            HasTag = false;

            //Проверяем - что файл существует
            if (File.Exists(path)) {
                try {
                    //open the file
                    FileStream fs = new FileStream(path, FileMode.Open);

                    //read the tag
                    byte[] buffer = new byte[128];
                    fs.Seek(-128, SeekOrigin.End);
                    fs.Read(buffer, 0, 128);
                    fs.Close();

                    //convert the tag buffer into a string
                    UTF8Encoding encoder = new UTF8Encoding();
                    string tag = encoder.GetString(buffer);

                    FileName = path;                    

                    if (tag.Substring(0, 3) == "TAG") {
                        HasTag = true;

                        Title = RemoveWhiteSpace(tag.Substring(3, 30));
                        Artist = RemoveWhiteSpace(tag.Substring(33, 30));
                        Album = RemoveWhiteSpace(tag.Substring(63, 30));
                        Year = RemoveWhiteSpace(tag.Substring(93, 4));
                        Comment = RemoveWhiteSpace(tag.Substring(97, 28));

                        if (tag[125] == 0)
                            Track = (int)buffer[126];
                        else
                            Track = 0;

                        string[] artPaths = Directory.GetFiles(Path.GetDirectoryName(path), "AlbumArt_*Large.jpg");

                        if (artPaths.Length == 0) artPaths = Directory.GetFiles(Path.GetDirectoryName(path), "AlbumArt*.jpg");
                        if (artPaths.Length == 0) artPaths = Directory.GetFiles(Path.GetDirectoryName(path), "Album*.jpg");
                        if (artPaths.Length == 0) artPaths = Directory.GetFiles(Path.GetDirectoryName(path), "*.jpg");

                        if (artPaths.Length > 0) {
                            AlbumArt = artPaths[0];
                        } else {
                            AlbumArt = defaultAlbumArt;                            
                        }
                    } 

                    if(string.IsNullOrEmpty(Title))
                        Title = Path.GetFileName(path);
                } catch (Exception ex) {

                }
            }
        }

        private string RemoveWhiteSpace(string s) {
            string new_string = "";

            foreach (char c in s) {
                if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSeparator(c))
                    new_string += c;
            }

            return new_string.Trim();
        }
    }
}
