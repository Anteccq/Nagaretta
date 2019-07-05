using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TweRiver.Utilities
{
    public static class ProcessUtil
    {
        public static void StartUrl(string url)
        {
            var p = new ProcessStartInfo()
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = @$"/c start {url}"
            };
            Process.Start(p);
        }
    }
}
