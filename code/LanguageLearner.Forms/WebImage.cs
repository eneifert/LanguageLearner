using System;
using System.Drawing;

//TODO
//using ScottWater.Boss;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Management;

namespace LanguageLearner.UI
{    
    public class WebImage
    {
        public static string InternetConnectionErrorMessage = "Error: Problem with internet connection";
        List<WebImageSearchItem> _searchItems;
        
        ImageRecievedCallaback _callback;
        int _processedImagesCount;
        int _maxImageResults = 1;
        public Thread ProcessingThread;

        public delegate void ImageRecievedCallaback(ImageRecievedCallbackItem callbackItem);

        public WebImage(List<WebImageSearchItem> searchItems, ImageRecievedCallaback callback) : this(searchItems, 1, callback)
        { }

        public WebImage(string searchText, int maxImageResults, ImageRecievedCallaback callback) : 
            this(new List<WebImageSearchItem>(new WebImageSearchItem[]{ new WebImageSearchItem(searchText)}), maxImageResults, callback)
        { }

        public WebImage(List<WebImageSearchItem> searchItems, int maxImageResults, ImageRecievedCallaback callback)
        {
            _searchItems = searchItems;
            _maxImageResults = maxImageResults;
            _callback = callback;
            _processedImagesCount = 0;
        } 

        #region Process With ThreadPool

        public List<WorkItem> StartProcessImageBatchWithThreadPool(int maxConcurrentThreads)
        {
            List<WorkItem> workItems = new List<WorkItem>();
            ThreadPool.SetMaxThreads(maxConcurrentThreads, maxConcurrentThreads);

            foreach (WebImageSearchItem searchItem in _searchItems)
            {
                WebImageThreadPoolObject webPool = new WebImageThreadPoolObject(this, searchItem);
                
                workItems.Add(AbortableThreadPool.QueueUserWorkItem(webPool.ThreadPoolCallBack, new object()));               
            }
            return workItems;
        }

        class WebImageThreadPoolObject
        {
            WebImage _webImage;
            WebImageSearchItem _searchItem;

            public WebImageThreadPoolObject(WebImage webImage, WebImageSearchItem searchItem)
            {
                _webImage = webImage;
                _searchItem = searchItem;
            }

            public void ThreadPoolCallBack(object ThreadContext)
            {
                _webImage.getImageWithCallBack(_searchItem);
            }
        }
        #endregion

        #region Process By Single Thread

        void threadStartForProcessImageBatchBySingleThread()
        {
            foreach (WebImageSearchItem searchItem in _searchItems)
            {
                getImageWithCallBack(searchItem);
            }
        }
        #endregion

        void getImageWithCallBack(WebImageSearchItem searchItem)
        {
            try
            {
                List<Image> pics = WebImage.GetImages(searchItem.SearchText.ToString(), _maxImageResults);
                _processedImagesCount++;
                _callback(new ImageRecievedCallbackItem(pics, searchItem, _processedImagesCount, _searchItems.Count, true, string.Empty));
            }
            catch (WebException ex)
            {
                _processedImagesCount++;
                _callback(new ImageRecievedCallbackItem(null, searchItem, _processedImagesCount, _searchItems.Count, false, ex.Message));
            }
            catch (FormatException ex1)
            {
                _processedImagesCount++;
                _callback(new ImageRecievedCallbackItem(null, searchItem, _processedImagesCount, _searchItems.Count, false, ex1.Message));
            }
        }

        #region Static Methods

        /// <summary>
        /// Process a batch of searchTerms and calls the callback whenever an image is downloaded. With this many image request are sent at the same time.        
        /// </summary>
        /// <param name="searchTerms">The list of terms to search for</param>
        /// <param name="callback">The method to call when the image has been downloaded</param>
        /// <param name="maxThreadCount">The amount of image requests that can run at the same time</param>
        /// <returns></returns>
        public static WebImageThreadManager ProcessImageBatchWithThreadPool(List<WebImageSearchItem> searchItems, ImageRecievedCallaback callback, int maxThreadCount)
        {
            WebImage wi = new WebImage(searchItems, callback);
            return new WebImageThreadManager(wi.StartProcessImageBatchWithThreadPool(maxThreadCount));            
        }

        /// <summary>
        /// Process a batch of searchTerms and calls the callback whenever an image is downloaded. With this many image request are sent at the same time.        
        /// </summary>
        /// <param name="searchTerms">The list of terms to search for</param>
        /// <param name="callback">The method to call when the image has been downloaded</param>
        /// <param name="maxThreadCount">The amount of image requests that can run at the same time</param>
        /// <returns></returns>
        public static WebImageThreadManager ProcessImageBatchWithThreadPool(List<WebImageSearchItem> searchItems, int maxImages, ImageRecievedCallaback callback, int maxThreadCount)
        {
            WebImage wi = new WebImage(searchItems, maxImages, callback);
            return new WebImageThreadManager(wi.StartProcessImageBatchWithThreadPool(maxThreadCount));
        }

        /// <summary>
        /// Process a batch of searchTerms and calls the callback whenever an image is downloaded. With this only one thread is started.
        /// </summary>
        /// <param name="searchTerms">The list of terms to search for</param>
        /// <param name="callback">The method to call when the image has been downloaded</param>
        /// <returns>The running Thread</returns>
        public static WebImageThreadManager ProcessImageBatchBySingleThread(List<WebImageSearchItem> searchItems, ImageRecievedCallaback callback)
        {
            WebImage wi = new WebImage(searchItems, callback);
            Thread t = new Thread(new ThreadStart(wi.threadStartForProcessImageBatchBySingleThread));
            t.Start();

            return new WebImageThreadManager(t);
        }

        /// <summary>
        /// Fetches images from the web for a search term.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="maxImages"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static WebImageThreadManager GetImagesWithSingleThread(string searchTerm, int maxImages, ImageRecievedCallaback callback)
        {
            WebImage wi = new WebImage(searchTerm, maxImages, callback);
            Thread t = new Thread(new ThreadStart(wi.threadStartForProcessImageBatchBySingleThread));
            t.Start();

            return new WebImageThreadManager(t);
        }

        /// <summary>
        /// Gets images from the web.
        /// 
        /// To save this image simply:
        ///     Image image = WebImage.GetImage("bird");
        ///     image.Save(@"C:\tt\" + "bird" + ".bmp");
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="maxImages"></param>
        /// <returns></returns>
        public static List<Image> GetImages(string searchTerm, int maxImages)
        {
            int errorCount = 0;
            List<Image> images = new List<Image>();           

            while (errorCount < 2)
            {
                try
                {
                    string bossApiKey = @"cLu6lP3V34EdnCaq33wpD1gpKppR9f8IUmDclhLTa6..UD._ejYFEBnK1IoZUFIu3rSETiVPyPg6";
                    //TODO
                    //ImageSearch imgS = new ImageSearch(bossApiKey);
                    //SearchResult<ImageSearchResultItem> results = imgS.Query(searchTerm).Get();

                    int i = 0;
                    //foreach (ImageSearchResultItem result in results)
                    //{
                    //    WebRequest requestPic = WebRequest.Create(result.Thumbnail_Url);
                    //    WebResponse responsePic = requestPic.GetResponse();
                    //    images.Add(Image.FromStream(responsePic.GetResponseStream()));

                    //    if (++i >= maxImages)
                    //        break;
                    //}

                    break;
                }
                catch (WebException ex)
                {                    
                    errorCount += 1;
                    if (errorCount >= 2)
                        throw new WebException(InternetConnectionErrorMessage, ex);
                }
                catch (FormatException ex1)
                {
                    errorCount += 1;
                    if (errorCount >= 2)
                        throw new FormatException(string.Format("Failed to get image for: {0}", searchTerm), ex1);
                }
            }

            if (images.Count < 1)
                throw new WebException("No images were returned");

            return images;
        }

        //public static bool IsOnline()
        //{            
        //    try
        //    {
        //        WebRequest test = WebRequest.Create("http://www.google.com");
        //        WebResponse responsePic = test.GetResponse();
        //        return true;
        //    }
        //    catch (WebException ex)
        //    {
        //        return false;
        //    }
        //}

        public static bool IsOnline()
        {
            try
            {
                System.Net.IPHostEntry objIPHE = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }

            //System.Net.IPHostEntry host = System.Net.Dns.GetHostByName("http://www.google.com");

            //string wqlTemplate = "SELECT StatusCode FROM Win32_PingStatus WHERE Address = '{0}'";

            //ManagementObjectSearcher query = new ManagementObjectSearcher();
            //query.Query = new ObjectQuery(String.Format(wqlTemplate, host.AddressList[0]));
            ////query.Scope = new ManagementScope("//localhost/root/cimv2");

            //ManagementObjectCollection pings = query.Get();

            //foreach (ManagementObject ping in pings)
            //{
            //    if (Convert.ToInt32(ping.GetPropertyValue("StatusCode")) == 0)
            //        return true;
            //}

            //return false;
        }
        #endregion                               
        
    }

    public class ImageRecievedCallbackItem
    {
        public List<Image> Images; 
        public WebImageSearchItem SearchItem;
        public int ProcessedCount;
        public int Total;
        public bool Success;
        public string ErrorMsg;

        public ImageRecievedCallbackItem(List<Image> images, WebImageSearchItem searchItem, int processed, int total, bool success, string errorMsg)
        {
            Images = images;
            SearchItem = searchItem;
            ProcessedCount = processed;
            Total = total;
            Success = success;
            ErrorMsg = errorMsg;
        }        
    }

    public class WebImageSearchItem
    {
        public int ID;
        public string SearchText;

        public WebImageSearchItem(string searchText) : this(-1, searchText) { }

        public WebImageSearchItem(int id, string searchText)
        {
            ID = id;
            SearchText = searchText;
        }

    }

    /// <summary>
    /// Class that allows you stop any Running WebImage Threads.
    /// </summary>
    public class WebImageThreadManager
    {
        Thread _thread;
        List<WorkItem> _workItems;
        
        public bool IsWorking
        {
            get
            {
                if (_thread != null && _thread.ThreadState == ThreadState.Running)
                    return true;

                if (_workItems != null)
                {
                    foreach (WorkItem workItem in _workItems)
                    {
                        if (AbortableThreadPool.GetStatus(workItem) != WorkItemStatus.Completed)
                            return true;
                    }
                }

                return false;
            }
        }

        public WebImageThreadManager(List<WorkItem> workItems)
        {
            _workItems = workItems;
        }
        public WebImageThreadManager(Thread thread)
        {
            _thread = thread;
        }

        public void StopThreads()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Running)
                _thread.Abort();
            if (_workItems != null)
            {
                foreach (WorkItem workItem in _workItems)
                {
                    AbortableThreadPool.Cancel(workItem, true);
                }
            }

        }
    }
}
