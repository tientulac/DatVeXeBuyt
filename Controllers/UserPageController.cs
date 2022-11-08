using QLXeBuyt.Models;
using QLXeBuyt.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLXeBuyt.Controllers
{
    public class UserPageController : Controller
    {
        private LinqDataContext db = new LinqDataContext();
        // GET: UserPage

        [HttpPost]
        public ActionResult BuyTicket(VeXeDTO req)
        {
            ViewBag.ListTramXe = db.Tramxes.ToList();
            var _veXe = new VeXe();
            _veXe.Ngayphathanh = req.Ngayphathanh;
            _veXe.Ngayhethan = DateTime.Now.AddDays(30);
            _veXe.Tinhtrang = "Đã mua";
            _veXe.Matuyen = req.Matuyen;
            _veXe.Loaive = req.Loaive;
            _veXe.Vethang = req.Vethang;
            _veXe.Giatien = req.Giatien;
            db.VeXes.InsertOnSubmit(_veXe);
            db.SubmitChanges();

            var _hoadon = new Hoadon();
            _hoadon.Ngaymua = DateTime.Now;
            _hoadon.Trangthai = 1;
            _hoadon.Makhachhang = req.Id_Taikhoan;
            db.Hoadons.InsertOnSubmit(_hoadon);
            db.SubmitChanges();

            var _ctHoadon = new CT_Hoadon();
            _ctHoadon.Mave = _veXe.Mave;
            _ctHoadon.MaHD = _hoadon.MaHD;
            _ctHoadon.Trangthai = 1;
            _ctHoadon.Soluotsudung = _veXe.Vethang == true ? 30 : 1;
            _ctHoadon.QRcode = "{ success = true, Mave =" + _veXe.Mave + ", MaHD = " + _hoadon.MaHD + ", Id_Taikhoan = " + req.Id_Taikhoan + "}";
            db.CT_Hoadons.InsertOnSubmit(_ctHoadon);
            db.SubmitChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DatVe()
        {
            ViewBag.ListTuyenXe = db.Tuyenxes.ToList();
            return View();
        }

        public ActionResult DanhSachVe(int id_TK)
        {
            var ma = db.Khachhangs.Where(x => x.Id_Taikhoan == id_TK).FirstOrDefault().Makhachhang;
            ViewBag.ListVe = db.sp_DanhSachVe(ma).ToList() ?? null;
            ViewBag.Khachhang = db.Khachhangs.Where(x => x.Id_Taikhoan == id_TK).FirstOrDefault() ?? null;
            return View();
        }

        public ActionResult TramXe()
        {
            ViewBag.ListTramXe = db.Tramxes.ToList();
            return View();
        }

        public ActionResult TuyenXe()
        {
            ViewBag.ListTuyenXe = db.Tuyenxes.ToList();
            return View();

        }
        public ActionResult TramXeById(int matram)
        {
            var _tramxe = db.Tramxes.Where(M => M.Matram == matram).FirstOrDefault();
            return Json(new { success = true, data = _tramxe }, JsonRequestBehavior.AllowGet);
        }
    }
}