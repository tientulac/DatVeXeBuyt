using QLXeBuyt.Models;
using QLXeBuyt.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLXeBuyt.Controllers
{
    public class TaiKhoanController : Controller
    {
        private LinqDataContext db = new LinqDataContext();

        [HttpPost]
        public ActionResult Save(Taikhoan req)
        {
            var _taikhoan = db.Taikhoans.Where(x => x.Id_Taikhoan == req.Id_Taikhoan).FirstOrDefault();
            _taikhoan.Email = req.Email;
            _taikhoan.Sodienthoai = req.Sodienthoai;
            _taikhoan.Code = req.Code;
            _taikhoan.Tinhtrang = req.Tinhtrang;
            _taikhoan.Sodu = req.Sodu;
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id_taikhoan)
        {
            var _taikhoan = db.Taikhoans.Where(x => x.Id_Taikhoan == id_taikhoan).FirstOrDefault();
            db.Taikhoans.DeleteOnSubmit(_taikhoan);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindById(int id_taikhoan)
        {
            var _taikhoan = (from a in db.Taikhoans.Where(x => x.Id_Taikhoan == id_taikhoan)
                             select new TaiKhoanDTO { 
                                Id_Taikhoan = a.Id_Taikhoan,
                                Tentaikhoan = a.Tentaikhoan,
                                Matkhau = a.Matkhau,
                                Email = a.Email,
                                Sodienthoai = a.Sodienthoai,
                                Code = a.Code.GetValueOrDefault(),
                                Tinhtrang = a.Tinhtrang,
                                Sodu = (double)a.Sodu.GetValueOrDefault(),
                                Maloai = a.Maloai
                             }).FirstOrDefault();
            return Json(new { success = true, data = _taikhoan }, JsonRequestBehavior.AllowGet);
        }

        // GET: TaiKhoan
        public ActionResult Index()
        {
            ViewBag.ListTaiKhoan = db.Taikhoans.ToList();
            return View();
        }

        public ActionResult Login(Taikhoan req)
        {
            var check = db.Taikhoans.Where(x => x.Tentaikhoan == req.Tentaikhoan && x.Matkhau == req.Matkhau);
            if (check.Any())
            {
                var _taikhoan = (from a in db.Taikhoans.Where(x => x.Id_Taikhoan == check.FirstOrDefault().Id_Taikhoan)
                                 select new TaiKhoanDTO
                                 {
                                     Id_Taikhoan = a.Id_Taikhoan,
                                     Tentaikhoan = a.Tentaikhoan,
                                     Matkhau = a.Matkhau,
                                     Email = a.Email,
                                     Sodienthoai = a.Sodienthoai,
                                     Code = a.Code.GetValueOrDefault(),
                                     Tinhtrang = a.Tinhtrang,
                                     Sodu = (double)a.Sodu.GetValueOrDefault(),
                                     Maloai = a.Maloai
                                 }).FirstOrDefault();
                return Json(new { success = true, data = _taikhoan }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, data = req }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}