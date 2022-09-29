using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using foxit;
using foxit.common;
using System.IO;
using System;

namespace OfficePDFConverter.Pages
{
    public class IndexModel : PageModel
    {
        public bool filesSubmitted = false;
        public bool filesValid = false;
        public string myFiles = "";
        public string directory = "";
        public string message = "";
        static string sn = "eHZELU0twWL0OuhsAlDNXxxjJvqGKETmxG6Y4zlgGnPdI8dBNRKv7w==";
        static string key = "8f3gFcGNtR8N+Td6AF43CmmH5xt1p8I6Ezse924Y/yIH9u/kcPAE40puFED3oBj7KMrlzYnceCvGoVdvKfiK3Z60JuWDq3fX5hWwd+Dv2B7wUXQpqvIPfarxpgSou3T1hJGpvX1Mp1TZEJ2VcFAckgTqtH4Ru+8Ek8f/YWbv3rPXMsr9Phw9DvYOf4SL9OI1Vm23IiDLLpTnIeMc/MlhILiyQxIT1BYDWX9O6R2IpRaV8cTNRqk/8Qv4nGZ16YJKddfZIrRcPhs0Gbg91cTFDWJsjUxG93xBhoCWhTN76tNeOLNxMVrAuoUS8T5biYio0t06jEm9YjHF/lqCyTRQrBRuWsYE77BlCqoSgjrKsDvDgomn80mT0sKyhorVa0STK0E4zinfQLP/cQmpcbL4P98UnXsRPbNekRNrYId9+Y9GZl/j7HO1f1UJVbjgrsCiOO67aIuPyW7iIphydtDj+92Ky99h9seAA5b9E2ZxR3bV1E4eFBREU36I3DlyhuKtlS6/CsiEpyTYFCYt8TS/77vPkGlEiCm/VpJ/DW7Tlifs4/LYzd02b8eIgYtw4xb4kTpSrp3lMNc8Zc4uq4e3RO4L7RLdNXe2Zpf0QZsBLI9zlW7DMLNy7+vHMd8u6Zfal27Q2MaKC/W+1g4SgN91Zh3GlcMxSfXo2njcNg3nxrck3bJ6lFvkr605K1r1Gb2AaccWr+urqh7X/h1fGj0IokWCJuZYzXDNEIVjYmW8k1AAroC0mD0aPZe4mr6C22rfM/STCBGuSIuglm8hDiPwchqpTC5/MM53kU7yg7IZ1IU3SQTmMBGp2m3p1PqKUUw+30V1+zHKL0KnsNYP/zDZ51qAOfyKJblZvBseKzNwioSUk/HMo3hyqtsP5C/7tJkmjGTtH3y2K9OOPhu4FIa/jdMWsFJ/IMDza63XiXUP5HGu3zZJb58NQ3FqZR+p6Wmgeoyo9OYc0Wt0EUkyOpuDD17PxSSc67baC3y88TjI9JM4Q5+FhE+cm+CMlX6/rZ8KDrz3Xeq78zMoLBA9vOiXmQLqYz7QXEip1LBPDp7xsYeet9nTe9WYb76ukYCSRAJZIqw5NMzWDHsfyknH2C1jq+VgzlVwoCifmfejOsZHtLnl/3u15FZ/ORSsDlCrwTFugmnJJXmDBCsRGlZBbAaSrsd7orbjUNoaOsQQ+xJdcl6ey6IC7uJxo6RtkAsQljFGYQHEMwu+B2xicd99QExRQbcv6Owzl34rvN5+BpXa16ZJWDWgptD7EqP0IjKEDq1RdxFBmErAfYFtMrGIuRQsjVbdIaDJBYLJTlenVpN/XERWfisLnkP6HEM=";
        
        ErrorCode error_code = Library.Initialize(sn, key);
                
             


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {

            if (error_code != ErrorCode.e_ErrSuccess)
                message = "Foxit SDK failed to initialize";


            directory = Request.Form["directory"];
            string[] filePaths = Directory.GetFiles(directory, "*.*", SearchOption.TopDirectoryOnly);
            if (filePaths.Length > 0)
            {
               string savepath = directory + "\\converted files\\";
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                foreach (string file in filePaths)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Extension == ".doc")
                    {
                        using (foxit.addon.conversion.Word2PDFSettingData word_convert_setting_data = new foxit.addon.conversion.Word2PDFSettingData())
                        {
                            foxit.addon.conversion.Convert.FromWord(file, "", savepath + "\\" + fi.Name + ".pdf", word_convert_setting_data);
                        }
                    }
                    else if (fi.Extension == ".ppt")
                    {
                        using (foxit.addon.conversion.PowerPoint2PDFSettingData ppt_convert_setting_data = new foxit.addon.conversion.PowerPoint2PDFSettingData())
                        {
                            foxit.addon.conversion.Convert.FromPowerPoint(file, "", savepath + "\\" + fi.Name + ".pdf", ppt_convert_setting_data);
                        }
                    }
                    else if (fi.Extension == ".xls")
                    {
                        using (foxit.addon.conversion.Excel2PDFSettingData excel_convert_setting_data = new foxit.addon.conversion.Excel2PDFSettingData())
                        {
                            foxit.addon.conversion.Convert.FromExcel(file, "", savepath + "\\" + fi.Name + ".pdf", excel_convert_setting_data);
                        }
                    }
                }
            }
            else
            {
                message = "Please enter a valid directory";
            }
            
            
        }
    }
}
