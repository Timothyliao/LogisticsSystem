using System.Management;

namespace LogisticsSystem.Tools
{
    public class CodeAssist
    {
        private int PixelsPerXLogicalInch = 0;//X方向上1英寸有多少个像素点
        private int PixelsPerYLogicalInch = 0;//Y方向上1英寸有多少个像素点


        /// <summary>
        /// 获取设备的DPI
        /// </summary>
        private void GetDeviceDPI()
        {
            using (ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    foreach (ManagementObject item in moc)
                    {
                        PixelsPerXLogicalInch = int.Parse((item.Properties["PixelsPerXLogicalInch"].Value.ToString()));
                        PixelsPerYLogicalInch = int.Parse((item.Properties["PixelsPerYLogicalInch"].Value.ToString()));
                    }
                }
            }
        }


        /// <summary>
        /// 毫米转换为像素
        /// </summary>
        /// <param name="millimeter">毫米</param>
        /// <param name="DipType">转换类型，1代表X方向，2代表Y方向</param>
        /// <returns></returns>
        public float MillimeterToPixel(double millimeter, int DipType = 1)
        {
            GetDeviceDPI();
            if (DipType == 1)
                return (float)(PixelsPerXLogicalInch * millimeter / 25.4);
            else
                return (float)(PixelsPerYLogicalInch * millimeter / 25.4);
        }

    }
}
