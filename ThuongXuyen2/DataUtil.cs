using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ThuongXuyen2
{
    internal class DataUtil
    {
        XmlDocument doc;
        XmlElement root;
        string filename;

        public DataUtil()
        {
            doc = new XmlDocument();
            filename = "DangKyHoc.xml";
            if (!File.Exists(filename))
            {
                root = doc.CreateElement("DangKyLamThem");
                doc.AppendChild(root);
                doc.Save(filename);
            }
            doc.Load(filename);
            root=doc.DocumentElement;
        }

        public List<NhanVien> hienThi()
        {
            List<NhanVien> nvs = new List<NhanVien>();
            XmlNodeList ngayLamViecs = root.SelectNodes("NgayLamViec");
            foreach (XmlNode ngayLamViec in ngayLamViecs)
            {
                XmlNodeList nhanViens = ngayLamViec.SelectNodes("NhanVien");
                foreach(XmlNode nhanVien in nhanViens)
                {
                    NhanVien nv = new NhanVien
                    {
                        NgayLamThem = ngayLamViec.SelectSingleNode("@Ngay").InnerText,
                        MaNhanVien=nhanVien.SelectSingleNode("@Ma").InnerText,
                        LoaiLamThem=nhanVien.SelectSingleNode("LoaiLamThem").InnerText,
                        SoGio=int.Parse(nhanVien.SelectSingleNode("SoGio").InnerText),
                        TrangThai=nhanVien.SelectSingleNode("TrangThai").InnerText
                    };
                    nvs.Add(nv);
                }
            }

            return nvs;
        }
        public bool KiemTraNV(NhanVien nhanVien)
        {
            XmlNode ngayLam = doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec[@Ngay='{nhanVien.NgayLamThem}']");
            if (ngayLam != null)
            {
                return false;
            }
            XmlNode ma = doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec/NhanVien[@Ma='{nhanVien.NgayLamThem}']");
            if (ma != null)
            {
                return false;
            }
            return true;
        }
        public void Them(NhanVien nv)
        {
            if (KiemTraNV(nv))
            {
                XmlElement ngayLamViec = doc.CreateElement("NgayLamViec");
               

                XmlAttribute ngay = doc.CreateAttribute("Ngay");
                ngay.InnerText=nv.NgayLamThem;

                XmlElement nhanVien = doc.CreateElement("NhanVien");


                XmlAttribute ma = doc.CreateAttribute("Ma");
                ma.InnerText=nv.MaNhanVien;

                XmlElement loaiLamThem = doc.CreateElement("LoaiLamThem");
                loaiLamThem.InnerText=nv.LoaiLamThem;

                XmlElement soGio = doc.CreateElement("SoGio");
                soGio.InnerText = nv.SoGio.ToString();

                XmlElement trangThai = doc.CreateElement("TrangThai");
                trangThai.InnerText = nv.TrangThai;


                ngayLamViec.Attributes.Append(ngay);

                ngayLamViec.AppendChild(nhanVien);

                nhanVien.Attributes.Append(ma);

                nhanVien.AppendChild(loaiLamThem);

                nhanVien.AppendChild(soGio);

                nhanVien.AppendChild(trangThai);

                root.AppendChild(ngayLamViec);

                doc.Save(filename);

                MessageBox.Show("Them thanh cong");
            }
        
            else
            {
                MessageBox.Show("Thong tin da ton tai");
            }          
        }

        public void Sua(NhanVien nhanVien)
        {
            XmlNode NhanVien = doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec/NhanVien[@Ma='{nhanVien.MaNhanVien}']");
            if (NhanVien == null)
            {
                MessageBox.Show("Nhan vien deo ton tai");
            }
            else
            {
                NhanVien.SelectSingleNode("LoaiLamThem").InnerText = nhanVien.LoaiLamThem;
                NhanVien.SelectSingleNode("SoGio").InnerText = nhanVien.SoGio.ToString();
                NhanVien.SelectSingleNode("TrangThai").InnerText = nhanVien.TrangThai;

                doc.Save(filename);

            }
        }

        public void Xoa(string mnv,string ngay)
        {
            XmlNode nhanVien= doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec/NhanVien[@Ma='{mnv}']");

            XmlNode ngayLam = doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec[@Ngay='{ngay}']");

            if(ngayLam != null)
            {
                if(nhanVien != null)
                {
                    ngayLam.RemoveChild(nhanVien);
                    doc.Save(filename);
                }
            }
        }
        public List<NhanVien> Tim(string ngay)
        {
            XmlNode ngayLam = doc.SelectSingleNode($"/DangKyLamThem/NgayLamViec[@Ngay='{ngay}']");
            List<NhanVien> nvs = new List<NhanVien>();
            if ( ngayLam != null )
            {
                XmlNodeList nhanViens = ngayLam.SelectNodes("NhanVien");
               
                foreach(XmlNode node in nhanViens)
                {
                    NhanVien nv = new NhanVien
                    {
                        NgayLamThem = ngayLam.SelectSingleNode("@Ngay").InnerText,
                        MaNhanVien = node.SelectSingleNode("@Ma").InnerText,
                        LoaiLamThem = node.SelectSingleNode("LoaiLamThem").InnerText,
                        SoGio = int.Parse(node.SelectSingleNode("SoGio").InnerText),
                        TrangThai = node.SelectSingleNode("TrangThai").InnerText
                    };
                    nvs.Add(nv);
                }
              
            }
            return nvs;
           
        }
    }
}
