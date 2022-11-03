using OfficeOpenXml;
using QLXeBuyt.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace QLXeBuyt.Controllers
{
    public class ReadFileExcelController : Controller
    {
        public ActionResult UploadTuyenXeToEpPlus(string fileName, string soTuyen)
        {
            try
            {
                // path to your excel file
                var FilePath = Server.MapPath("~/FileImport/" + fileName);
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(FilePath)))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["06"];
                    var totalRows = workSheet.Dimension.Rows;

                    for (int i = 2; i <= totalRows; i++)
                    {
                        var _soTuyen = workSheet.Cells[i, 4].Value.ToString().Trim();
                        if (_soTuyen == soTuyen)
                        {
                            var _tenTuyen = workSheet.Cells[i, 2].Value.ToString().Trim();
                            var _maTuyen = workSheet.Cells[i, 3].Value.ToString().Trim();
                            var _luotDi = workSheet.Cells[i, 5].Value.ToString().Trim();
                            var _luotVe = workSheet.Cells[i, 6].Value.ToString().Trim();
                            return Json(new { success = true, luotdi = _luotDi, luotve = _luotVe, matuyen = _maTuyen, tentuyen = _tenTuyen }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}