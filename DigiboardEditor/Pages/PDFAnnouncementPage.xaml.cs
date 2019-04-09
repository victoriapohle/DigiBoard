using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Fixed;

namespace DigiboardEditor.Pages
{
    /// <summary>
    /// Interaction logic for PDFAnnouncementPage.xaml
    /// </summary>
    public partial class PDFAnnouncementPage : Page
    {
        public PDFAnnouncementPage()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
            InitializeComponent();
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            RadOpenFileDialog openFileDialog = new RadOpenFileDialog()
            {
                Filter = "|PDF Files (*.pdf)|*.pdf",
                FilterIndex = 1,
            };
            

            //if (!string.IsNullOrEmpty(storage.FolderName))
            //{
            //    openFileDialog.InitialDirectory = storage.FolderName;
            //}

            openFileDialog.ShowDialog();

            if (openFileDialog.DialogResult == true)
            {
                string exceptionMessage = null;
                try
                {

                    Stream fileStream = openFileDialog.OpenFile();
                    byte[] file;
                    using (var filestream = new StreamReader(fileStream))
                    {
                        using (var reader = new BinaryReader(fileStream))
                        {
                            file = reader.ReadBytes((int)fileStream.Length);
                        }
                    }
                    byte[] documentContent = file;
                    MemoryStream ms = new MemoryStream(documentContent);
                    EmptyContent.Visibility = Visibility.Collapsed;
                    pdfViewer.DocumentSource = new PdfDocumentSource(ms);

                }
                catch (NotSupportedException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (FileNotFoundException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (SecurityException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (DirectoryNotFoundException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (UnauthorizedAccessException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (PathTooLongException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    exceptionMessage = ex.Message;
                }
                catch (IOException ex)
                {
                    exceptionMessage = ex.Message;
                }

                if (exceptionMessage != null)
                {
                    //    DialogParameters dialogParameters = new DialogParameters()
                    //    {
                    //        Content = exceptionMessage,
                    //        Owner = this,
                    //        DialogStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                    //    };
                    //    RadWindow.Alert(dialogParameters);
                    //}
                }
            }
        }

        private void DpEndDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

