using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThuongXuyen2
{
    public partial class Form1 : Form
    {

        DataUtil dataUtil = new DataUtil();
        List<NhanVien> nhanViens = new List<NhanVien>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }
        private void HienThi()
        {
            nhanViens.Clear();
            nhanViens = dataUtil.hienThi();
            dgv_dsnv.DataSource = nhanViens;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien=new NhanVien();
            nhanVien.NgayLamThem = txtNgay.Text;
            nhanVien.MaNhanVien = txtMaNV.Text;
            nhanVien.LoaiLamThem=txtLoaiLamThem.Text;
            nhanVien.SoGio=int.Parse(txtSoGio.Text);
            nhanVien.TrangThai=txtTrangThai.Text;
            dataUtil.Them(nhanVien);
            HienThi();
        }

        private void dgv_dsnv_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = dgv_dsnv.CurrentRow;
            txtNgay.Text = row.Cells["NgayLamThem"].Value?.ToString();
            txtMaNV.Text = row.Cells["MaNhanVien"].Value?.ToString();
            txtLoaiLamThem.Text = row.Cells["LoaiLamThem"].Value?.ToString();
            txtSoGio.Text = row.Cells["SoGio"].Value?.ToString();
            txtTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.NgayLamThem = txtNgay.Text;
            nhanVien.MaNhanVien = txtMaNV.Text;
            nhanVien.LoaiLamThem = txtLoaiLamThem.Text;
            nhanVien.SoGio = int.Parse(txtSoGio.Text);
            nhanVien.TrangThai = txtTrangThai.Text;
            dataUtil.Sua(nhanVien);
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa không?","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
            {
                dataUtil.Xoa(txtMaNV.Text, txtNgay.Text);
                HienThi() ;
            }

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            nhanViens.Clear();
            nhanViens = dataUtil.Tim(txtNgay.Text);
            if(nhanViens.Count>0)
            {
                dgv_dsnv.DataSource = nhanViens;
            }
            else
            {
                MessageBox.Show("ktt");
            }
        }
    }
}
