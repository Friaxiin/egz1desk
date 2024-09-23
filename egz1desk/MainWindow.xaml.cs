using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace egz1desk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected string path = "C:\\Users\\patryk.labuz\\Desktop\\egz1desk\\Data.txt";
        List<FilmData> filmDatas = new List<FilmData>();
        public FilmData Film { get; set; }

        public int CurrentIndex { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            GetDataFromFile();
            CurrentIndex = 0;
            Film = filmDatas[CurrentIndex];

            DataContext = Film;
        }

        public void GetDataFromFile()
        {
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);

                List<string> updatedLines = new List<string>();

                foreach (var line in lines)
                {
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        updatedLines.Add(line);
                    }
                }

                for (int i = 0; i < updatedLines.Count; i += 5)
                {
                    try
                    {
                        FilmData newFilmData = new FilmData
                        {
                            artist = updatedLines[i],
                            album = updatedLines[i + 1],
                            songsNumber = int.Parse(updatedLines[i + 2]),
                            year = int.Parse(updatedLines[i + 3]),
                            downloadNumber = int.Parse(updatedLines[i + 4])
                        };
                        filmDatas.Add(newFilmData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas przetwarzania danych: {ex.Message}");
                    }
                }

            }
        }

        private void PrevData(object sender, RoutedEventArgs e)
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
            }
            else
            {
                CurrentIndex = filmDatas.Count - 1;
            }

            Film = filmDatas[CurrentIndex];
            this.DataContext = Film;
        }

        private void NextData(object sender, RoutedEventArgs e)
        {
            if (CurrentIndex < filmDatas.Count - 1)
            {
                CurrentIndex++;
            }
            else
            {
                CurrentIndex = 0;
            }

            Film = filmDatas[CurrentIndex];
            this.DataContext = Film;
        }
    }
}