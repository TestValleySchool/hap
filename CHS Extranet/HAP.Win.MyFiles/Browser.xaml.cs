﻿using HAP.Win.MyFiles.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace HAP.Win.MyFiles
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class Browser : HAP.Win.MyFiles.Common.LayoutAwarePage
    {
        private CancellationTokenSource cts;
        public Browser()
        {
            this.InitializeComponent();
            cts = new CancellationTokenSource();
        }
        private JSONUploadParams Params;
        private string path;
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            loading.IsIndeterminate = true;
            path = navigationParameter as string;
            HttpWebRequest req;
            if (path == "")
            {
                pageTitle.Text = "My Drives";
                req = WebRequest.CreateHttp(new Uri(HAPSettings.CurrentSite.Address, "./api/myfiles/drives"));
                bottomAppBar.IsEnabled = downloadbutton.IsEnabled = uploadbutton.IsEnabled = false;
            }
            else
            {
                pageTitle.Text = path.Replace("/", "\\").Replace("\\", " \\ ");
                req = WebRequest.CreateHttp(new Uri(HAPSettings.CurrentSite.Address, "./api/myfiles/" + path.Replace('\\', '/')));
            }

            req.Method = "GET";
            req.ContentType = "application/json";

            req.CookieContainer = new CookieContainer();
            bool tokengood = false;
            try
            {
                req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie("token", HAPSettings.CurrentToken[0]));
                req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie(HAPSettings.CurrentToken[2], HAPSettings.CurrentToken[1]));
                tokengood = true;
            }
            catch 
            {
                MessageDialog mes = new MessageDialog("Your Logon Token has Expired");
                mes.Commands.Add(new UICommand("OK"));
                mes.DefaultCommandIndex = 0;
                mes.ShowAsync();
                Frame.Navigate(typeof(MainPage));
            }
            if (tokengood)
            {
                HttpWebResponse x1 = null;
                try
                {
                    WebResponse x = await req.GetResponseAsync();
                    x1 = (HttpWebResponse)x;
                    if (path == "")
                    {
                        JSONDrive[] drives = JsonConvert.DeserializeObject<JSONDrive[]>(new StreamReader(x1.GetResponseStream()).ReadToEnd());
                        itemsViewSource.Source = drives;
                        driveGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        fileGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        driveGridView.SelectedIndex = -1;
                    }
                    else
                    {
                        JSONFile[] files = JsonConvert.DeserializeObject<JSONFile[]>(new StreamReader(x1.GetResponseStream()).ReadToEnd());
                        itemsViewSource.Source = files;
                        driveGridView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        fileGridView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        fileGridView.SelectedIndex = -1;
                        req = WebRequest.CreateHttp(new Uri(HAPSettings.CurrentSite.Address, "./api/myfiles/UploadParams/" + path.Replace('\\', '/')));
                        req.Method = "GET";
                        req.ContentType = "application/json";

                        req.CookieContainer = new CookieContainer();
                        tokengood = false;
                        try
                        {
                            req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie("token", HAPSettings.CurrentToken[0]));
                            req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie(HAPSettings.CurrentToken[2], HAPSettings.CurrentToken[1]));
                            tokengood = true;
                        }
                        catch
                        {
                            MessageDialog mes = new MessageDialog("Your Logon Token has Expired");
                            mes.Commands.Add(new UICommand("OK"));
                            mes.DefaultCommandIndex = 0;
                            mes.ShowAsync();
                            Frame.Navigate(typeof(MainPage));
                        }
                        if (tokengood)
                        {
                            x1 = null;
                            try
                            {
                                x = await req.GetResponseAsync();
                                x1 = (HttpWebResponse)x;
                                JSONUploadParams prop = JsonConvert.DeserializeObject<JSONUploadParams>(new StreamReader(x1.GetResponseStream()).ReadToEnd());
                                Params = prop;
                                bottomAppBar.IsEnabled = prop.Properties.Permissions.ReadData;
                                uploadbutton.IsEnabled = prop.Properties.Permissions.AppendData;
                            }
                            catch (Exception ex)
                            {
                                MessageDialog mes = new MessageDialog(ex.ToString(), "An error has occured processing this request");
                                mes.Commands.Add(new UICommand("OK"));
                                mes.DefaultCommandIndex = 0;
                                mes.ShowAsync();
                                Frame.GoBack();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog mes = new MessageDialog(ex.ToString(), "An error has occured processing this request");
                    mes.Commands.Add(new UICommand("OK"));
                    mes.DefaultCommandIndex = 0;
                    mes.ShowAsync();
                    Frame.GoBack();
                }
            }
            loading.IsIndeterminate = false;
        }

        private void driveGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(Browser), ((JSONDrive)e.ClickedItem).Path);
        }

        private void fileGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            JSONFile file = ((JSONFile)e.ClickedItem);
            if (file.Extension == "") Frame.Navigate(typeof(Browser), ((JSONFile)e.ClickedItem).Path);
            else
            {
                Frame.Navigate(typeof(Details), ((JSONFile)e.ClickedItem).Path);
                //pro.Visibility = Windows.UI.Xaml.Visibility.Visible;
                //pro.Value = 0;
                //downloadfile(file);
            }
        }

        void downloadfile(JSONFile file)
        {
            HttpWebRequest req = WebRequest.CreateHttp(new Uri(HAPSettings.CurrentSite.Address, file.Path.Substring(1)));
            req.Method = "GET";
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie("token", HAPSettings.CurrentToken[0]));
            req.CookieContainer.Add(HAPSettings.CurrentSite.Address, new Cookie(HAPSettings.CurrentToken[2], HAPSettings.CurrentToken[1]));
            req.AllowReadStreamBuffering = false;
            req.BeginGetResponse(a =>
            {
                using (WebResponse response = req.EndGetResponse(a))
                {
                    int expected = (int)response.ContentLength;
                    using (System.IO.BinaryReader reader  = new BinaryReader(response.GetResponseStream()))
                    {
                        MemoryStream ms = new MemoryStream(expected);
                        bool canwrite = true;
                        int count = 0, x = 0;
                        while (canwrite)
                        {
                            try
                            {
                                ms.WriteByte(reader.ReadByte());
                            }
                            catch { canwrite = false; }
                            count++;
                            x++;
                            if (x == 100) { var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { pro.Value = count * 100.0 / expected; }); x = 0; }

                        }
                        var ignored1 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { makefile(ms, (JSONFile)a.AsyncState); });
                        var ignored2 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {pro.Value = 100.0;});
                    }
                }
            }
            , file);
        }


        async void makefile(MemoryStream ms, JSONFile jfile)
        {
            Windows.Storage.StorageFolder temporaryFolder = ApplicationData.Current.TemporaryFolder;
            StorageFile sampleFile = await temporaryFolder.CreateFileAsync(jfile.Name + jfile.Extension, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(sampleFile, ms.ToArray());

            if (EnsureUnsnapped())
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add(jfile.Type, new List<string>() { jfile.Extension });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = jfile.Name;

                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                    CachedFileManager.DeferUpdates(file);
                    // write to file
                    await FileIO.WriteBytesAsync(file, ms.ToArray());
                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                    // Completing updates may require Windows to ask for user input.
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                    XmlNodeList texts = toastXML.GetElementsByTagName("text");
                    if (status == FileUpdateStatus.Complete)
                    {
                        texts[0].AppendChild(toastXML.CreateTextNode("HAP+ - Download Complete"));
                        texts[1].AppendChild(toastXML.CreateTextNode(file.Name + " has been downloaded and is ready to use."));
                    }
                    else
                    {
                        texts[0].AppendChild(toastXML.CreateTextNode("HAP+ - Download Canceled"));
                        texts[1].AppendChild(toastXML.CreateTextNode("You have canceled this download"));
                    }
                    ((XmlElement)toastXML.SelectSingleNode("/toast")).SetAttribute("launch", "{\"type\":\"toast\"}");
                    ToastNotification toast = new ToastNotification(toastXML);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                    var ignored1 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { pro.Visibility = Windows.UI.Xaml.Visibility.Collapsed; });
                }
                else
                {
                    var ignored1 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { pro.Visibility = Windows.UI.Xaml.Visibility.Collapsed; });
                }
            }
            await sampleFile.DeleteAsync();
        }

        internal bool EnsureUnsnapped()
        {
            // FilePicker APIs will not work if the application is in a snapped state.
            // If an app wants to show a FilePicker while snapped, it must attempt to unsnap first
            bool unsnapped = ((ApplicationView.Value != ApplicationViewState.Snapped) || ApplicationView.TryUnsnap());
            //if (!unsnapped)
            //{
            //    NotifyUser("Cannot unsnap the sample.", NotifyType.StatusMessage);
            //}

            return unsnapped;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JSONFile file = ((JSONFile)fileGridView.SelectedItem);
            pro.Visibility = Windows.UI.Xaml.Visibility.Visible;
            pro.Value = 0;
            downloadfile(file);
        }

        private void fileGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (fileGridView.SelectedItem != null)
                {
                    JSONFile file = ((JSONFile)fileGridView.SelectedItem);
                    if (file.Extension != "") { downloadbutton.IsEnabled = bottomAppBar.IsOpen = true; uploadbutton.IsEnabled = false; }
                    else { downloadbutton.IsEnabled = false; uploadbutton.IsEnabled = bottomAppBar.IsOpen = file.Permissions.AppendData; }
                }
                else { downloadbutton.IsEnabled = bottomAppBar.IsOpen = false; uploadbutton.IsEnabled = Params.Properties.Permissions.AppendData; }
            }
            catch { downloadbutton.IsEnabled = bottomAppBar.IsOpen = uploadbutton.IsEnabled = false; }
        }

        private async void upload()
        {
            if (EnsureUnsnapped())
            {
                var ignored1 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { loading.IsIndeterminate = true; });
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.List;
                openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                if (Params.Filters[0] == "*.*") openPicker.FileTypeFilter.Add("*");
                else foreach (string s in Params.Filters) openPicker.FileTypeFilter.Add(s.Substring(1));

                var files = await openPicker.PickMultipleFilesAsync();
                if (files != null)
                {
                    try
                    {
                        List<string> filenames = new List<string>();
                        foreach (StorageFile file in files)
                        {
                            Uri uri = new Uri(HAPSettings.CurrentSite.Address, "./api/myfiles-upload/" + (fileGridView.SelectedItem == null ? path : ((JSONFile)fileGridView.SelectedItem).Path).Replace('\\', '/'));
                            BackgroundUploader uploader = new BackgroundUploader();
                            uploader.SetRequestHeader("X_FILENAME", file.Name);
                            filenames.Add(file.Name);
                            uploader.Method = "POST";
                            uploader.SetRequestHeader("Cookie", string.Format("{0}={1}; token={2}", HAPSettings.CurrentToken[2], HAPSettings.CurrentToken[1], HAPSettings.CurrentToken[0]));
                            UploadOperation upload = uploader.CreateUpload(uri, file);

                            // Attach progress and completion handlers.
                            HandleUploadAsync(upload, true);
                        }
                        MessageDialog mes = new MessageDialog("The upload of " + string.Join(", ", filenames.ToArray()) + " has started, you will get notified when it's done", "Uploading");
                        mes.Commands.Add(new UICommand("OK"));
                        mes.DefaultCommandIndex = 0;
                        mes.ShowAsync();
                        var ignored3 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { loading.IsIndeterminate = false; });
                    }
                    catch (Exception ex)
                    {
                        MessageDialog mes = new MessageDialog(ex.ToString(), "Error");
                        mes.Commands.Add(new UICommand("OK"));
                        mes.DefaultCommandIndex = 0;
                        mes.ShowAsync();
                        var ignored3 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { loading.IsIndeterminate = false; });
                    }


                }
                else
                {
                    var ignored2 = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { pro.Visibility = Windows.UI.Xaml.Visibility.Collapsed; pro.Value = 100.0; loading.IsIndeterminate = false; });
                }
            }
        }
        private async Task HandleUploadAsync(UploadOperation upload, bool start)
        {
            try
            {

                Progress<UploadOperation> progressCallback = new Progress<UploadOperation>(UploadProgress);
                if (start)
                {
                    // Start the upload and attach a progress handler.
                    await upload.StartAsync().AsTask(cts.Token, progressCallback);
                }
                else
                {
                    // The upload was already running when the application started, re-attach the progress handler.
                    await upload.AttachAsync().AsTask(cts.Token, progressCallback);
                }

                ResponseInformation response = upload.GetResponseInformation();
            }
            catch (TaskCanceledException)
            {
                XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                XmlNodeList texts = toastXML.GetElementsByTagName("text");
                texts[0].AppendChild(toastXML.CreateTextNode("HAP+ - Upload Canceled"));
                texts[1].AppendChild(toastXML.CreateTextNode("The uploading of " + upload.SourceFile.Name + " has been Canceled."));
                ((XmlElement)toastXML.SelectSingleNode("/toast")).SetAttribute("launch", "{\"type\":\"toast\"}");
                ToastNotification toast = new ToastNotification(toastXML);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
        }

        // Note that this event is invoked on a background thread, so we cannot access the UI directly.
        private void UploadProgress(UploadOperation upload)
        {
            BackgroundUploadProgress progress = upload.Progress;

            double percentSent = 100;
            if (progress.TotalBytesToSend > 0)
            {
                percentSent = progress.BytesSent * 100 / progress.TotalBytesToSend;
            }
            if (progress.Status == BackgroundTransferStatus.Completed)
            {
                XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
                XmlNodeList texts = toastXML.GetElementsByTagName("text");
                texts[0].AppendChild(toastXML.CreateTextNode("HAP+ - Upload Complete"));
                texts[1].AppendChild(toastXML.CreateTextNode("The uploading of " + upload.SourceFile.Name + " has completed."));
                ((XmlElement)toastXML.SelectSingleNode("/toast")).SetAttribute("launch", "{\"type\":\"toast\"}");
                ToastNotification toast = new ToastNotification(toastXML);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            upload();
        }
    }
}
