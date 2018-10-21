using MobileManageSysyem.EF_Model;
using MobileManageSysyem.ViewObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MobileManageSysyem
{
    public partial class frmMobile : Form
    {
        public frmMobile()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var dbContext = new MobileManagerEntities())
            {
                var mobileData = dbContext.MobileInfo.Select(x => new MobileData()
                {
                    Brand = x.Brand,
                    Category = x.CategoryInfo.Category,
                    Mid = x.Mid,
                    Price = x.Price,
                    Type = x.Type
                }).ToList();

                this.dataGridView1.DataSource = mobileData;
            }

            dataGridView1.Columns[0].HeaderCell.Value = "序号";
            dataGridView1.Columns[1].HeaderCell.Value = "品牌";
            dataGridView1.Columns[2].HeaderCell.Value = "手机类型";
            dataGridView1.Columns[3].HeaderCell.Value = "型号";
            dataGridView1.Columns[4].HeaderCell.Value = "价格";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbb_type.Text))
            {
                MessageBox.Show("请选择查询种类");
            }

            if (string.IsNullOrEmpty(txt_condition.Text))
            {
                MessageBox.Show("请输入查询条件");
            }

            using (var dbContext = new MobileManagerEntities())
            {
                var result = new List<MobileData>();

                if (cbb_type.Text.Equals("品牌")) {
                    result = dbContext.MobileInfo.Select(x => new MobileData()
                    {
                        Brand = x.Brand,
                        Category = x.CategoryInfo.Category,
                        Mid = x.Mid,
                        Price = x.Price,
                        Type = x.Type
                    }).Where(c => c.Brand.Equals(txt_condition.Text)).ToList();
                }

                if (cbb_type.Text.Equals("种类"))
                {
                    result = dbContext.MobileInfo.Select(x => new MobileData()
                    {
                        Brand = x.Brand,
                        Category = x.CategoryInfo.Category,
                        Mid = x.Mid,
                        Price = x.Price,
                        Type = x.Type
                    }).Where(c => c.Category.Equals(txt_condition.Text)).ToList();
                }

                if (!result.Any())
                {
                    MessageBox.Show("没有您所要查询的手机");
                }

                this.dataGridView1.DataSource = result;
            }
        }

        private void cbb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_condition.Enabled = true;
            if (cbb_type.Text.Equals("全部"))
            {
                txt_condition.Text = "";
                txt_condition.Enabled = false;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_condition.Text = "";
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
