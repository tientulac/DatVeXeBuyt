using QLXeBuyt.Models;
using QLXeBuyt.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLXeBuyt.Controllers
{
    public class KhachHangController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Khachhang req)
        {
            if (req.Makhachhang > 0)
            {
                var _khachhang = db.Khachhangs.Where(x => x.Makhachhang == req.Makhachhang).FirstOrDefault();
                _khachhang.Ten = req.Ten;
                _khachhang.Ngaysinh = req.Ngaysinh;
                _khachhang.CCCD = req.CCCD;
                _khachhang.TheHSSV = req.TheHSSV;
                _khachhang.Gioitinh = req.Gioitinh;
                _khachhang.Gioitinh = req.Gioitinh;
                db.SubmitChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            db.Khachhangs.InsertOnSubmit(req);
            db.SubmitChanges();
            return Json(new { success = true, data = req }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int makhachhang)
        {
            var _khachhang = db.Khachhangs.Where(M => M.Makhachhang == makhachhang).FirstOrDefault();
            db.Khachhangs.DeleteOnSubmit(_khachhang);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int makhachhang)
        {
            var _khachhang = (from a in db.Khachhangs.Where(x => x.Makhachhang == makhachhang)
                                 select new KhachHangDTO
                                 {
                                     Makhachhang = a.Makhachhang,
                                     Ten = a.Ten,
                                     Ngaysinh = a.Ngaysinh,
                                     CCCD = a.CCCD,
                                     TheHSSV = a.TheHSSV,
                                     Gioitinh = a.Gioitinh,
                                     Diachi = a.Diachi,
                                     Id_Taikhoan = a.Id_Taikhoan,
                                     Tentaikhoan = db.Taikhoans.Where(x => x.Id_Taikhoan == a.Id_Taikhoan).FirstOrDefault().Tentaikhoan ?? "Không xác định",
                                     Tengioitinh = a.Gioitinh == true ? "Nam" : "Nữ"
                                 }).FirstOrDefault();
            return Json(new { success = true, data = _khachhang }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var listKhachHang = (from a in db.Khachhangs
                                 select new KhachHangDTO { 
                                    Makhachhang = a.Makhachhang,
                                    Ten = a.Ten,
                                    Ngaysinh = a.Ngaysinh,
                                    CCCD = a.CCCD,
                                    TheHSSV = a.TheHSSV,
                                    Gioitinh = a.Gioitinh,
                                    Diachi = a.Diachi,
                                    Id_Taikhoan = a.Id_Taikhoan,
                                    Tentaikhoan = db.Taikhoans.Where(x => x.Id_Taikhoan == a.Id_Taikhoan).FirstOrDefault().Tentaikhoan ?? "Không xác định",
                                     Tengioitinh = a.Gioitinh == true ? "Nam" : "Nữ"
                                 }).ToList();
            ViewBag.ListKhachHang = listKhachHang;
            return View();
        }
    }
}