using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WatchedFolder
{
    /// <summary>
    /// 同步队列实体类
    /// </summary>
    public class SyncAction
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public WatcherChangeTypes ChangedType { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime ChangedTime { get; set; }

        /// <summary>
        /// 同步文件名(除去同步目录"\\1.txt")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 同步路径
        /// </summary>
        public string SyncPath { get; set; }

        /// <summary>
        /// OldName
        /// </summary>
        public string OldName { get; set; }

        /// <summary>
        /// OldFullPath
        /// </summary>
        public string OldFullPath { get; set; }


    }
}
