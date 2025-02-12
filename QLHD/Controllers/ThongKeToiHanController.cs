﻿using QLHD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLHD.Controllers
{
    public class ThongKeToiHanController : Controller
    {
        //
        // GET: /ThongKeToiHan/

        QLHD2Entities db = new QLHD2Entities();
        public ActionResult export_hdcp_toihan(int? namHD, int? loaiHD, int? donvi)
        {
            DateTime date = System.DateTime.Now;
            var hd = this.db.HOPDONG_CHIPHI.ToList();
            if (namHD != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.NAM_HD_ID == namHD && n.NGAY_KT <= date).ToList();
            }
            if (loaiHD != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.NAM_HD_ID == namHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.DONVI_ID == donvi && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD == 0 && loaiHD == 0 && donvi == 0)
            {
                hd = db.HOPDONG_CHIPHI.Where(n =>n.NGAY_KT <= date).ToList();
            }
            var tenhd = "";
            if (loaiHD != 0)
            {
                var a = db.LOAI_HD_SUB.Where(n => n.ID_LOAIHD_SUB == loaiHD).First();
                tenhd = a.TEN_HD_SUB;
            }
            else tenhd = "Tất cả";

            GridView gv = new GridView();
            gv.DataSource = from P in hd
                            select new
                            {
                                //SO_HOP_DONG = P.hd_CHIPHI_ID.
                                SO_HD = P.SO_HD,
                                LOAI_HD = P.LOAI_HD_SUB.TEN_HD_SUB,
                                NGAY_HD = P.NGAY_HD,
                                BEN_THUE = P.TEN_BT,
                                BEN_CHO_THUE = P.TEN_CT,
                                THANG_GT = P.THANG,
                                TONG_GT = P.TONG_GT,
                                CHU_KY = P.CHUKY_TT.CHUKY,
                                HTTT = P.HTTT.TEN_HTTT,
                                TU_NGAY = P.NGAY_BD,
                                DEN_NGAY = P.NGAY_KT,
                                GHI_CHU = P.GHICHU
                            };

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DS_HD_CHI_PHI_" + System.DateTime.Now + ".xls");
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gv.HeaderRow.Cells[0].Style.Add("background-color", "green");
            Response.Write("<table style='font-size:20px'><tr><td colspan='12' align='center'><b>Thống kê danh sách hợp đồng</b></td></tr>"
                + "<tr><td colspan='12' style='color:red' align='center'><b>Loại hợp đồng: " + tenhd + "</b></td></tr>"
                + "</table>");
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Write("<table><tr></tr>"
                + "<tr><td></td><td><b>(Nguồn dữ liệu: chương trình quản lý hợp đồng. Thời gian lấy dữ liệu:" + System.DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy") + ")</b></td></tr>"
                + "</table>");
            Response.Flush();
            Response.End();
            return RedirectToAction("ThongKe", "ketquathongke", new { @HD = hd });
        }
        public ActionResult export_hddt_toihan(int? namHD, int? loaiHD, int? donvi)
        {
            DateTime date = System.DateTime.Now;
            var hd = this.db.HOPDONG_DOANHTHU.ToList();
            if (namHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.NGAY_KT <= date).ToList();
            }
            if (loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.DONVI_ID == donvi && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD == 0 && loaiHD == 0 && donvi == 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NGAY_KT <= date).ToList();
            }
            GridView gv = new GridView();
            gv.DataSource = from P in hd
                            select new
                            {
                                SO_HD = P.SO_HD,
                                LOAI_HD = P.LOAI_HD_SUB.TEN_HD_SUB,
                                NGAY_HD = P.NGAY_HD,
                                BEN_THUE = P.TEN_BT,
                                BEN_CHO_THUE = P.TEN_CT,
                                TONG_GT = P.TONG_GT,
                                CHU_KY = P.CHUKY_TT.CHUKY,
                                HTTT = P.HTTT.TEN_HTTT
                            };
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DS_HOPDONG_DOANHTHU_TOI_HAN_" + System.DateTime.Now + ".xls");
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ketquathongke_HDDT");
        }
        public ActionResult export_hdnc_toihan(int? namHD, int? loaiHD, int? donvi)
        {
            DateTime date = System.DateTime.Now;
            var hd = this.db.HOPDONG_NHANCONG.ToList();
            if (namHD != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.NAM_HD_ID == namHD && n.NGAY_KT <= date).ToList();
            }
            if (loaiHD != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.NAM_HD_ID == namHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.DONVI_ID == donvi && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD == 0 && loaiHD == 0 && donvi == 0)
            {
                hd = db.HOPDONG_NHANCONG.Where(n => n.NGAY_KT <= date).ToList();
            }
            int i = 1;
            GridView gv = new GridView();
            gv.DataSource = from P in hd
                            select new
                            {
                                STT = i++,
                                LOẠI_HĐ = P.LOAI_HD_SUB.TEN_HD_SUB,
                                NĂM_HĐ = P.NAM_HD,
                                SỐ_HĐ = P.SO_HD,
                                NGÀY_HĐ = P.NGAY_HD.Value.ToString("dd/MM/yyyy"),
                                TÊN_CTV = P.TEN_CT,
                                NĂM_SINH = P.NGAYSINH_CT.Value.ToString("dd/MM/yyyy"),
                                ĐỊACHỈ = P.DIACHI_CT,
                                SỐ_ĐIỆN_THOẠI = P.DIENTHOAI_CT,
                                CMND = P.CMND_CT,
                                NGÀY_CẤP = P.NGAYCAP_CT.Value.ToString("dd/MM/yyyy"),
                                NƠI_CẤP = P.NOICAP_CT,
                                TRÌNH_ĐỘ = P.TRINHDO_CT,
                                MS_THUẾ = P.MSTHUE_CT,
                                TỔNG_GIÁ_TRỊ = P.TONG_GT,
                                HTTT = P.HTTT.TEN_HTTT,
                                NGÀY_BĐ = P.NGAY_BD.Value.ToString("dd/MM/yyyy"),
                                NGÀY_KT = P.NGAY_KT.Value.ToString("dd/MM/yyyy"),
                                
                            };
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DS_HOPDONG_NHANCONG_TOI_HAN_" + System.DateTime.Now + ".xls");
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ketquathongke_HDNC");
        }

        public ActionResult export_hdcntt_toihan(int? namHD, int? loaiHD, int? donvi)
        {
            DateTime date = System.DateTime.Now;
            var hd = this.db.HOPDONG_DOANHTHU.ToList();
            if (namHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.NGAY_KT <= date).ToList();
            }
            if (loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (donvi != 0 && loaiHD != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.DONVI_ID == donvi && n.ID_LOAIHD_SUB == loaiHD && n.NGAY_KT <= date).ToList();
            }
            if (namHD != 0 && loaiHD != 0 && donvi != 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NAM_HD_ID == namHD && n.ID_LOAIHD_SUB == loaiHD && n.DONVI_ID == donvi && n.NGAY_KT <= date).ToList();
            }
            if (namHD == 0 && loaiHD == 0 && donvi == 0)
            {
                hd = db.HOPDONG_DOANHTHU.Where(n => n.NGAY_KT <= date).ToList();
            }
            GridView gv = new GridView();
            gv.DataSource = from P in hd
                            select new
                            {
                                SO_HD = P.SO_HD,
                                LOAI_HD = P.LOAI_HD_SUB.TEN_HD_SUB,
                                NGAY_HD = P.NGAY_HD,
                                BEN_THUE = P.TEN_BT,
                                BEN_CHO_THUE = P.TEN_CT,
                                TONG_GT = P.TONG_GT,
                                CHU_KY = P.CHUKY_TT.CHUKY,
                                HTTT = P.HTTT.TEN_HTTT
                            };
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DS_HOPDONG_CNTT_TOI_HAN_" + System.DateTime.Now + ".xls");
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("ketquathongke_HDCNTT");
        }
    }
}
