using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections.Concurrent;


namespace WatchedFolder
{
    /// <summary>
    ///Version:2013-7-8
    /// </summary>
    public class WatchedProcess
    {

        private IList<string> watchList;  //监视目录列表
        Thread tSyncProcess;  //同步线程 
        private ConcurrentQueue<SyncAction> safeQueue;
        SyncAction currentItem;  //当前线程操作项
        IDictionary<string, string> renameRecord = new Dictionary<string, string>(); //nowPath,oldObjPath 

        /// <summary>
        /// 构造函数 初始化 
        /// </summary>
        /// <param name="list"></param>
        public WatchedProcess(IList<string> list)
        {
            this.watchList = list;
            safeQueue = new ConcurrentQueue<SyncAction>();
            tSyncProcess = new Thread(new ThreadStart(this.StartSyncProcess));
            tSyncProcess.IsBackground = true;
            tSyncProcess.Start();  //处理出队后的任务
        }

        /// <summary>
        /// 同步线程方法
        /// </summary>
        private void StartSyncProcess()
        {
            while (true)
            {
                if (this.safeQueue.Count == 0)
                {
                    continue;
                }

                this.safeQueue.TryPeek(out currentItem); //尝试出列

                try
                {
                    SyncProcess(currentItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error :" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="item"></param>
        private void SyncProcess(SyncAction item)
        {
            string logicFullPath = string.Empty;
            //同步watchList下的全部目录
            foreach (string path in this.watchList)
            {
                if (item.FullPath.StartsWith(path)) continue;
                //除去同步目录的全路径
                logicFullPath = item.FullPath.Replace(item.SyncPath, "");
                string fullPath = path + logicFullPath;

                switch (item.ChangedType)
                {
                    //-----------Created ------------
                    case WatcherChangeTypes.Created:

                        //复制文件
                        if (FilesIsUsing(item.FullPath, fullPath, true)) return;
                        if (IsDirectory(item.FullPath))
                        {
                            if (!IsDirectory(fullPath))
                                //创建||复制目录
                                CopyDirectory(item.FullPath, path, item.SyncPath);
                        }
                        else if (IsFile(item.FullPath))
                        {
                            CopyFile(item.FullPath, fullPath, item.SyncPath, false);
                        }

                        break;

                    //-----------------&& Changed-------------
                    case WatcherChangeTypes.Changed:
                        //检查文件是否在使用
                        if (FilesIsUsing(item.FullPath, fullPath, true)) return;
                        //规范出队顺序-》重命名- 修改- 删除
                        foreach (SyncAction ac in safeQueue.ToArray())
                        {
                            if (ac.ChangedType == WatcherChangeTypes.Renamed)
                            {
                                safeQueue.TryDequeue(out item); safeQueue.Enqueue(item); return;
                            }
                        }

                        if (IsDirectory(item.FullPath))
                        {
                            if (!IsDirectory(fullPath)) //在同步目录下是否存在了？
                                CopyDirectory(item.FullPath, path, item.SyncPath);
                        }
                        else if (IsFile(item.FullPath))
                        {
                            if (!CopyFile(item.FullPath, fullPath, item.SyncPath, true))
                            {
                                return;
                            }
                        }

                        break;
                    //---------------&& Deleted----------  
                    case WatcherChangeTypes.Deleted:
                        foreach (SyncAction sa in safeQueue.ToArray())
                        {
                            if (sa.ChangedType != WatcherChangeTypes.Deleted)
                            { safeQueue.TryDequeue(out item); safeQueue.Enqueue(item); return; }
                        }
                        if (FilesIsUsing(item.FullPath, fullPath, true)) return;

                        while (true)
                        {
                            try
                            {
                                if (renameRecord.Keys.Contains(item.FullPath))
                                {
                                    Delete(renameRecord[item.FullPath]);
                                    renameRecord.Remove(item.FullPath);
                                }
                                Delete(fullPath);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Delete Error :" + ex.Message);
                            }
                        }
                        break;
                    //------------&& Renamed------------- 
                    case WatcherChangeTypes.Renamed:
                        string oldLogicFullDir = string.Empty;
                        string newLogicFullDir = string.Empty;
                        oldLogicFullDir = item.OldFullPath.Replace(item.SyncPath, "");
                        newLogicFullDir = item.FullPath.Replace(item.SyncPath, "");
                        string oldFullDir = path + oldLogicFullDir;
                        string newFullDir = path + newLogicFullDir;
                        //保证新路径 旧路径文件都没用户访问 
                        if (FilesIsUsing(item.FullPath, fullPath, true) && FilesIsUsing(item.OldFullPath, path + "\\" + item.OldName, true))
                            return;
                        //把重命名路径 在Queue 中对应的路径改了
                        foreach (SyncAction ac in safeQueue.ToArray())
                        {
                            if (ac.ChangedType == WatcherChangeTypes.Changed)
                            {
                                if (ac.FullPath == item.OldFullPath)
                                    ac.FullPath = item.FullPath;
                            }
                        }
                        // Rename DirectoryName
                        if (this.IsDirectory(item.FullPath) && !Directory.Exists(newFullDir))
                        {
                            DirectoryInfo dirDst = new DirectoryInfo(oldFullDir);
                            if (Directory.Exists(oldFullDir))
                            {
                                dirDst.MoveTo(newFullDir);
                            }
                            else
                            {
                                CopyDirectory(item.FullPath, path, item.SyncPath);
                            }
                        }
                        //Rename FileName
                        else if (IsFile(item.FullPath) && !File.Exists(newFullDir))
                        {
                            FileInfo fileDst = new FileInfo(oldFullDir);
                            if (File.Exists(oldFullDir))
                            {
                                fileDst.MoveTo(newFullDir);
                            }
                            else
                            {
                                CopyFile(item.FullPath, fullPath, item.SyncPath, false);
                            }
                        }
                        if (!renameRecord.Keys.Contains(item.FullPath)) renameRecord.Add(item.FullPath, oldFullDir);
                        //  删除旧路径重新进队
                        safeQueue.Enqueue(new SyncAction() { ChangedType = WatcherChangeTypes.Deleted, FullPath = item.OldFullPath, SyncPath = item.SyncPath });
                        break;
                    default:
                        break;
                }
                safeQueue.TryDequeue(out currentItem); //出列
                Console.WriteLine("同步完成：" + item.ChangedType.ToString() + "--" + fullPath);
                Console.WriteLine("                   " + item.FullPath);
            }

        }

        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <param name="path"></param>
        private void Delete(string path)
        {
            if (IsDirectory(path))
            {
                Directory.Delete(path, true);
            }
            else if (IsFile(path))
            { File.Delete(path); }
        }


        /// <summary>
        /// 存在源文件并判断是否在使用
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="objPath"></param>
        /// <param name="WatchDir"></param>
        /// <param name="checkOpen">是否检查目标路径</param>
        /// <returns></returns>
        private bool FilesIsUsing(string srcPath, string objPath, bool checkOpen)
        {
            FileInfo srcFile = new FileInfo(srcPath);
            if (File.Exists(srcPath) || IsFile(currentItem.OldFullPath) && string.IsNullOrEmpty(currentItem.OldFullPath))  //存在文件
            {
                //文件正在使用，丢去队列尾  error： 应该放回原来的位置
                try
                {
                    if (IsFile(srcPath))
                    { using (FileStream fs = srcFile.OpenRead()) { } }
                    else
                    { using (FileStream fs = new FileInfo(currentItem.OldFullPath).OpenRead()) { } }
                    // 文档是否存在 ，仅对【修改】内容执行下一操作  
                    if (checkOpen && IsFile(objPath))
                    {
                        using (FileStream objFs = new FileInfo(objPath).OpenRead()) { }
                    }

                }
                catch
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="fullPath"></param>
        /// <param name="WatchDir"></param>
        private bool CopyFile(string srcPath, string objPath, string WatchDir, bool checkOpen)
        {
            FileInfo srcFile = new FileInfo(srcPath);
            if (IsFile(objPath))
            {
                FileInfo dstFile = new FileInfo(objPath);
                if (srcFile.LastWriteTime <= dstFile.LastWriteTime)
                    return true;
                srcFile.CopyTo(objPath, true);
                return true;
            }
            try
            {    //防止目录不存在,在未开监视时创建的文件
                if (IsDirectory(objPath.Replace(srcFile.Name, "")) == false)
                    Directory.CreateDirectory(objPath.Replace(srcFile.Name, ""));
            }
            finally
            {
                srcFile.CopyTo(objPath);

            }
            return true;
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="path"></param>
        /// <param name="WatchDir"></param>
        private void CopyDirectory(string srcPath, string syncDir, string WatchDir)
        {
            Directory.CreateDirectory(syncDir + srcPath.Replace(WatchDir, ""));
            DirectoryInfo dirSrc = new DirectoryInfo(srcPath);
            FileInfo[] files = dirSrc.GetFiles();
            foreach (FileInfo file in files)
            {
                CopyFile(file.FullName, syncDir + file.FullName.Replace(WatchDir, ""), WatchDir, false);
            }
            DirectoryInfo[] dirs = dirSrc.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CopyDirectory(dir.FullName, syncDir, WatchDir);
            }
        }


        /// <summary>
        /// 开始监视
        /// </summary>
        public void WatcherStrat()
        {
            //监视所有同步目录
            foreach (string dir in watchList)
            {
                FileSystemWatcher fsWatch = new FileSystemWatcher();
                fsWatch.Path = dir;
                fsWatch.Filter = "";

                fsWatch.Changed += new FileSystemEventHandler(OnChanged);
                fsWatch.Created += new FileSystemEventHandler(OnChanged);
                fsWatch.Deleted += new FileSystemEventHandler(OnChanged);
                fsWatch.Renamed += new RenamedEventHandler(OnRenamed);

                //监视类型 
                fsWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime;
                //缓冲区大小 max=64k
                fsWatch.InternalBufferSize = 8192 * 8;
                fsWatch.IncludeSubdirectories = true;  //是否监视子目录
                fsWatch.EnableRaisingEvents = true;   //开始
            }
        }

        /// <summary>
        /// 监视的Onchang事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!this.IsFile(e.FullPath) && e.ChangeType == WatcherChangeTypes.Changed) return;

            FileSystemWatcher watcher = source as FileSystemWatcher;
            SyncAction syncItem = new SyncAction();
            syncItem.FullPath = e.FullPath;
            syncItem.ChangedTime = DateTime.Now;
            syncItem.Name = e.Name;
            syncItem.ChangedType = e.ChangeType;
            syncItem.SyncPath = watcher.Path;

            string logicFullPath = string.Empty;
            //path 目标路径
            foreach (string path in watchList)
            {
                if (syncItem.FullPath.StartsWith(path)) continue;
                logicFullPath = syncItem.FullPath.Replace(syncItem.SyncPath, "");
                string fullPath = path + logicFullPath;
                switch (syncItem.ChangedType)
                {
                    case WatcherChangeTypes.Created:
                        if (File.Exists(fullPath) || Directory.Exists(fullPath))
                        {
                            return; //已存在 不加入队列
                        }
                        break;
                    case WatcherChangeTypes.Changed:
                        if (IsFile(syncItem.FullPath) && IsFile(fullPath))
                        {
                            FileInfo srcFile = new FileInfo(syncItem.FullPath);
                            FileInfo dstFile = new FileInfo(fullPath);
                            if (srcFile.LastWriteTime <= dstFile.LastWriteTime)
                                return;

                        }
                        break;
                    default: break;
                }
            }
            //同步实体入队列
            if (syncItem.FullPath.ToLower().EndsWith(".tmp")) return;
            this.safeQueue.Enqueue(syncItem);
        }

        /// <summary>
        /// 监视OnRenamed事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        void OnRenamed(object source, RenamedEventArgs e)
        {
            FileSystemWatcher watcher = source as FileSystemWatcher;
            SyncAction syncItem = new SyncAction();
            syncItem.OldFullPath = e.OldFullPath;
            syncItem.FullPath = e.FullPath;
            syncItem.ChangedTime = DateTime.Now;
            syncItem.Name = e.Name;
            syncItem.OldName = e.OldName;
            syncItem.ChangedType = e.ChangeType;
            syncItem.SyncPath = watcher.Path;
            if (syncItem.FullPath.ToLower().EndsWith(".tmp")) return;
            this.safeQueue.Enqueue(syncItem);  //入队列
        }

        /// <summary>
        /// 是否文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsFile(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }

        /// <summary>
        ///是否文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsDirectory(string path)
        {
            if (Directory.Exists(path))
                return true;
            return false;
        }

    }
}
