using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeCafeProject
{
    public partial class FrmMember : Form
    {
        public FrmMember()
        {
            InitializeComponent();
        }

        //สร้างเมธอดแสดงข้อความเตือน
        private void showWarningMSG(string msg)
        {
            MessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void getAllMemberToListView()
        {
            //กำหนด connect String เพื่อติดต่อไปยังฐานข้อมูล
            //string connectionstring = @"server=DESKTOP-6F6L1NQ\SQLEXPRESS2022;Database=coffee_cafe_db;Trusted_Connection=True;";

            // สร้าง connection ไปยังฐานข้อมูล
            using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
            {
                try
                {
                    sqlConnection.Open(); //เปิดการเชื่อมต่อไปยังฐานข้อมูล
                    //การทำงานกับตารางในฐานข้อมูล (select,insert,update, delete)
                    // สร้างคำสั่ง SQL ในที่นี้คือ ดึงข้อมูลทั้งหมดจากตาราง menu_tb
                    string strSQL = "SELECT memberId, memberPhone, memberName, memberScore FROM member_tb";

                    //จัดการให้ SQL ทำงาน
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, sqlConnection))
                    {
                        //เอาข้อมูลที่ได้จาก strSQL ซึ่งเป็นก้อนใน dataadapter มาทำให้เป็นตารางโดยใส่ไว้ใน datatable
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // ตั้งค่า ListView
                        lvShowAllMember.Items.Clear();
                        lvShowAllMember.Columns.Clear();
                        lvShowAllMember.FullRowSelect = true;
                        lvShowAllMember.View = View.Details;

                        //กำหนดรายละเอียดของ column ใน ListView
                        lvShowAllMember.Columns.Add("รหัสสมาชิก", 100, HorizontalAlignment.Center);
                        lvShowAllMember.Columns.Add("เบอร์โทรศัพท์", 100, HorizontalAlignment.Center);
                        lvShowAllMember.Columns.Add("ชื่อ", 250, HorizontalAlignment.Left);


                        // loop วนเข้าไปใน datatable 
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            ListViewItem item = new ListViewItem(dataRow["memberId"].ToString());
                            item.SubItems.Add(dataRow["memberPhone"].ToString());
                            item.SubItems.Add(dataRow["memberName"].ToString());
                            lvShowAllMember.Items.Add(item);

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                }
            }
        }

        private void FrmMember_Load(object sender, EventArgs e)
        {
            getAllMemberToListView();
            tbMemberId.Clear();
            tbMemberName.Clear();
            tbMemberPhone.Clear();
            btSave.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //Validate UI แสดงข้อความเตือนด้วย เมื่อ  Validate แล้วก้เอาข้อมูลไปบันทึกลง DB

            if (tbMemberName.Text.Trim() == "")
            {
                showWarningMSG("ป้อนชื่อด้วย...");
            }
            else if (tbMemberPhone.Text.Trim() == "")
            {
                showWarningMSG("ป้อนเบอร์โทรศัพท์ด้วย...");
            }
            else
            {
                // บันทึกลง DB ->
                //กำหนด connect String เพื่อติดต่อไปยังฐานข้อมูล
                //string connectionstring = @"server=DESKTOP-6F6L1NQ\SQLEXPRESS2022;Database=coffee_cafe_db;Trusted_Connection=True;";

                // สร้าง connection ไปยังฐานข้อมูล
                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); // ใช้กับ Insert/update/delete

                        //คำสั่ง SQL 
                        string strSQL = "INSERT INTO member_tb (memberName,memberPhone,memberScore) " +
                                        "VALUES (@memberName,@memberPhone,@memberScore)";

                        // กำหนดค่าให้กับ SQL Parameter  และสั่งให้คำสั่ง SQL ทำงาน  แล้วมีข้อความแจ้งเมื่อทำงานเสร็จแล้ว
                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@memberName", SqlDbType.NVarChar, 100).Value = tbMemberName.Text;
                            sqlCommand.Parameters.Add("@memberPhone", SqlDbType.NVarChar, 50).Value = tbMemberPhone.Text;
                            sqlCommand.Parameters.Add("@memberScore", SqlDbType.Int).Value = 0;


                            //สั่งให้คำสั่ง sql ทำงาน
                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            //ข้อความแจ้ง
                            MessageBox.Show("บันทึกเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // อัปเดตListView
                            getAllMemberToListView();
                            tbMemberId.Clear();
                            tbMemberName.Clear();
                            tbMemberPhone.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                    }
                }

            }
        }

        private void lvShowAllMember_ItemActivate(object sender, EventArgs e)
        {
            //เอาข้อมูลของรายการที่เลือกไปแสดงที่หน้าจอ แล้วปุ่มบันทึกใช่ไม่ได้  แก้ไขกับลบใช้ได้
            tbMemberId.Text = lvShowAllMember.SelectedItems[0].SubItems[0].Text;
            tbMemberPhone.Text = lvShowAllMember.SelectedItems[0].SubItems[1].Text;
            tbMemberName.Text = lvShowAllMember.SelectedItems[0].SubItems[2].Text;

            btSave.Enabled = false;
            btUpdate.Enabled = true;
            btDelete.Enabled = true;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            //ถามผู้ใช้ก่อนจะลบหรือไม่ มีให้เลือก Yes/No
            if (MessageBox.Show("ต้องการลบสมาชิกหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //ลบออกจาก Database จากตารางใน DB เงื่อนไขคือ menuId
                //กำหนด connect String เพื่อติดต่อไปยังฐานข้อมูล
                //string connectionstring = @"server=DESKTOP-6F6L1NQ\SQLEXPRESS2022;Database=coffee_cafe_db;Trusted_Connection=True;";

                // สร้าง connection ไปยังฐานข้อมูล
                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); // ใช้กับ Insert/update/delete

                        //คำสั่ง SQL
                        string strSQL = "DELETE FROM member_tb WHERE memberId=@memberId";

                        // กำหนดค่าให้กับ SQL Parameter  และสั่งให้คำสั่ง SQL ทำงาน  แล้วมีข้อความแจ้งเมื่อทำงานเสร็จแล้ว
                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@memberId", SqlDbType.Int).Value = int.Parse(tbMemberId.Text);

                            //สั่งให้คำสั่ง sql ทำงาน
                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            //ข้อความแจ้ง
                            MessageBox.Show("ลบเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // อัปเดตListView
                            getAllMemberToListView();
                            tbMemberId.Clear();
                            tbMemberName.Clear();
                            tbMemberPhone.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                    }
                }
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            //Validate UI แสดงข้อความเตือนด้วย เมื่อ  Validate แล้วก้เอาข้อมูลไปบันทึกลง DB

            if (tbMemberName.Text.Trim() == "")
            {
                showWarningMSG("ป้อนชื่อด้วย...");
            }
            else if (tbMemberPhone.Text.Trim() == "")
            {
                showWarningMSG("ป้อนเบอร์โทรศัพท์ด้วย...");
            }
            else
            {
                // บันทึกลง DB ->
                //กำหนด connect String เพื่อติดต่อไปยังฐานข้อมูล
                //string connectionstring = @"server=DESKTOP-6F6L1NQ\SQLEXPRESS2022;Database=coffee_cafe_db;Trusted_Connection=True;";

                // สร้าง connection ไปยังฐานข้อมูล
                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); // ใช้กับ Insert/update/delete

                        //คำสั่ง SQL เขียนแบบนี้ต้องไม่มี , ก่อน where
                        string strSQL = @"UPDATE member_tb  
                                  SET memberName = @memberName,
                                      memberPhone = @memberPhone 
                                  WHERE memberId = @memberId";

                        // กำหนดค่าให้กับ SQL Parameter  และสั่งให้คำสั่ง SQL ทำงาน  แล้วมีข้อความแจ้งเมื่อทำงานเสร็จแล้ว
                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@memberName", SqlDbType.NVarChar, 100).Value = tbMemberName.Text;
                            sqlCommand.Parameters.Add("@memberPhone", SqlDbType.NVarChar, 50).Value = tbMemberPhone.Text;
                            sqlCommand.Parameters.Add("@memberId", SqlDbType.Int).Value = int.Parse(tbMemberId.Text);


                            //สั่งให้คำสั่ง sql ทำงาน
                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            //ข้อความแจ้ง
                            MessageBox.Show("แก้ไขเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // อัปเดตListView
                            getAllMemberToListView();
                            tbMemberId.Clear();
                            tbMemberName.Clear();
                            tbMemberPhone.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            getAllMemberToListView();
            tbMemberId.Clear();
            tbMemberName.Clear();
            tbMemberPhone.Clear();
            btSave.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbMemberPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // อนุญาตให้กด Backspace ได้
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // ถ้าเป็นตัวเลข และยังไม่เกิน 10 ตัว
            else if (char.IsDigit(e.KeyChar))
            {
                if (textBox.Text.Length < 10)
                {
                    e.Handled = false; // อนุญาตให้พิมพ์
                }
                else
                {
                    e.Handled = true; // เกิน 10 ตัว ไม่ให้พิมพ์
                }
            }
            else
            {
                // ถ้าไม่ใช่ตัวเลขหรือ Backspace ไม่ให้พิมพ์
                e.Handled = true;
            }
        }
    }
}
