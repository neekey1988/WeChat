using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Common.Share
{
    public class PathHelper
    {
        /// <summary>
        /// 生成文件路径，其中配置文件的path可选以下参数
        /// $root:根目录
        /// $yyyy$MM$dd等 时间
        /// $filename 文件名
        /// $guid GUID
        /// $ticks Ticks时间戳
        /// $ext 扩展名
        /// $directoryname 提交时要传递directoryname参数,用于不同模块的上传保存不同的目录
        /// </summary>
        /// <param name="sourcePath">原始路径，可带参数</param>
        /// <param name="webroot">web根路径</param>
        /// <param name="filename">文件名，要带后缀</param>
        /// <param name="dirname">目录名，可不传，如果sourcePath带有此参数就要传</param>
        /// <returns></returns>
        public static string BuildPath(string sourcePath,string webroot,string filename,string directoryname) {
            sourcePath = sourcePath.Replace("$root", webroot);
            sourcePath = sourcePath.Replace("$guid", Guid.NewGuid().ToString().Replace("-", ""));
            sourcePath = sourcePath.Replace("$ticks", DateTime.Now.Ticks.ToString());
            sourcePath = sourcePath.Replace("$directoryname", directoryname);
            sourcePath = sourcePath.Replace("$yyyy", DateTime.Now.ToString("yyyy"));
            sourcePath = sourcePath.Replace("$MM", DateTime.Now.ToString("MM"));
            sourcePath = sourcePath.Replace("$dd", DateTime.Now.ToString("dd"));
            sourcePath = sourcePath.Replace("$HH", DateTime.Now.ToString("HH"));
            sourcePath = sourcePath.Replace("$mm", DateTime.Now.ToString("mm"));
            sourcePath = sourcePath.Replace("$ss", DateTime.Now.ToString("ss"));
            sourcePath = sourcePath.Replace("$ext", System.IO.Path.GetExtension(filename).Replace(".", ""));
            sourcePath = sourcePath.Replace("$filename", System.IO.Path.GetFileNameWithoutExtension(filename));
            return sourcePath;
        }
    }
}
