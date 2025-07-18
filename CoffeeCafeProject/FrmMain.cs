using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoffeeCafeProject
{
    public partial class FrmMain : Form
    {
        // ตัวแปรเก็บราคาเมนู 
        float[] menuPrice = new float[10];
        //ตัวแปรเก็บรหัสสมาชิก
        int memberId = 0;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btMenu_Click(object sender, EventArgs e)
        {
            FrmMenu frmMenu = new FrmMenu();
            frmMenu.ShowDialog();
            resetForm();
        }

        private void btMember_Click(object sender, EventArgs e)
        {
            FrmMember frmMember = new FrmMember();
            frmMember.ShowDialog();
        }

        // เมธอด resetForm -> เรียกใช้ตอน form load กับคลิกปุ่มยกเลิก
        private void resetForm()
        {
            //ตั้งค่า memberId เป็น 0 
            memberId = 0;
            //ให้ rdMemberNo , rdMemberYes ไม่ถูกเลือก
            rdMenberNo.Checked = false;
            rdMemberYes.Checked = false;
            //ให้ tbMemberPhone ว่าง และใช้ไม่ได้
            tbMemberPhone.Clear();
            tbMemberPhone.Enabled = false;
            //ให้ tbNenberName เป็นข้อความ (ชื่อสมาชิก)
            tbMemberName.Text = "(ชื่อสมาชิก)";
            //ให้ lbMemberScore เป็น 0 
            lbMemberScore.Text = "0";
            //ให้ lbOrderPay เป็น 0.00
            lbOrderPay.Text = "0.00";
            //เคลียร์ lvOderMenu
            lvOrderMenu.Items.Clear();
            lvOrderMenu.Columns.Clear();
            lvOrderMenu.FullRowSelect = true;
            lvOrderMenu.View = View.Details;
            lvOrderMenu.Columns.Add("ชื่อเมนู", 150, HorizontalAlignment.Left);
            lvOrderMenu.Columns.Add("ราคา", 80, HorizontalAlignment.Left);
            //ดึงข้อมูลรายการเมนูมาจาก database มาแสดงที่หน้าจอ  และเก็บไว้ใช้กับตอนที่ผู้ใช้เลือกสั่งเมนู
            // สร้าง connection ไปยังฐานข้อมูล
            using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
            {
                try
                {
                    sqlConnection.Open(); //เปิดการเชื่อมต่อไปยังฐานข้อมูล

                    string strSQL = "SELECT menuName, menuPrice, menuImage FROM menu_tb";

                    //จัดการให้ SQL ทำงาน
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, sqlConnection))
                    {
                        //เอาข้อมูลที่ได้จาก strSQL ซึ่งเป็นก้อนใน dataadapter มาทำให้เป็นตารางโดยใส่ไว้ใน datatable
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        //สร้างตัวแปรอ้างถึง PictureBox และ Button ที่จะเอารูปและชื่อเมนูไปแสดง
                        PictureBox[] pbMenuImage = { pbMenu1, pbMenu2, pbMenu3, pbMenu4, pbMenu5, pbMenu6, pbMenu7, pbMenu8, pbMenu9, pbMenu10 };
                        Button[] btMenuName = { btMenu1, btMenu2, btMenu3, btMenu4, btMenu5, btMenu6, btMenu7, btMenu8, btMenu9, btMenu10 };

                        //เคลียร์ pbMenuImage และ btMenuName ก่อนที่จะใส่ลงไปใหม่
                        for (int i = 0; i < 10; i++)
                        {
                            pbMenuImage[i].Image = Properties.Resources.menu;
                            btMenuName[i].Text = "";
                        }

                        //วนลูปเอาข้อมูลที่อยู่ใน dataTable กำหนดให้กับ pbMenuImage,btMenuName,menuPrice
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            btMenuName[i].Text = dataTable.Rows[i]["menuName"].ToString();
                            menuPrice[i] = float.Parse(dataTable.Rows[i]["menuPrice"].ToString());
                            //เอารูปไปกำหนดให้กับ pbMenuImage
                            if (dataTable.Rows[i]["menuImage"] != DBNull.Value)
                            {
                                //กรณีมีรูปต้องแปลงจาก Binary ให้เป็นรูป
                                byte[] imgByte = (byte[])dataTable.Rows[i]["menuImage"];
                                using (var ms = new System.IO.MemoryStream(imgByte))
                                {
                                    pbMenuImage[i].Image = System.Drawing.Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                //กรณีไม่มีรูป
                                pbMenuImage[i].Image = Properties.Resources.menu;

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void rdMenberNo_CheckedChanged(object sender, EventArgs e)
        {
            tbMemberPhone.Clear();
            tbMemberPhone.Enabled = false;
            tbMemberName.Text = "(ชื่อสมาชิก)";
            lbMemberScore.Text = "0";
            memberId = 0;
        }

        private void rdMemberYes_CheckedChanged(object sender, EventArgs e)
        {
            tbMemberPhone.Clear();
            tbMemberPhone.Enabled = true;
            tbMemberName.Text = "(ชื่อสมาชิก)";
            lbMemberScore.Text = "0";
        }

        private void tbMemberPhone_KeyUp(object sender, KeyEventArgs e)
        {
            //ตรวจสอบว่าปุ่มที่กดแล้วปล่อยใช่ปุ่ม Enter หรือไม่
            //ถ้าไม่ใช่ไม่ต้องทำอะไร แต่ถ้าใช่ให้เอาเบอร์โทรไปค้นใน database
            //แล้วชื่อกับแต้มมาโชว์ ส่วนรหัสเอาไว้ใช้บันทึกลง database
            if (e.KeyCode == Keys.Enter)
            {
                // สร้าง connection ไปยังฐานข้อมูล
                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open(); //เปิดการเชื่อมต่อไปยังฐานข้อมูล

                        string strSQL = "SELECT memberId, memberName, memberScore FROM member_tb WHERE memberPhone=@memberPhone";

                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection))
                        {
                            sqlCommand.Parameters.Add("@memberPhone", SqlDbType.NVarChar, 50).Value = tbMemberPhone.Text;

                            //จัดการให้ SQL ทำงาน
                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                            {
                                //เอาข้อมูลที่ได้จาก strSQL ซึ่งเป็นก้อนใน dataadapter มาทำให้เป็นตารางโดยใส่ไว้ใน datatable
                                DataTable dataTable = new DataTable();
                                dataAdapter.Fill(dataTable);

                                if (dataTable.Rows.Count == 1)
                                {
                                    tbMemberName.Text = dataTable.Rows[0]["memberName"].ToString();
                                    lbMemberScore.Text = dataTable.Rows[0]["memberScore"].ToString();
                                    memberId = int.Parse(dataTable.Rows[0]["memberId"].ToString());
                                }
                                else
                                {
                                    MessageBox.Show("เบอร์โทรนี้ไม่มี กรุณาป้อนเบอร์โทรศัพท์ใหม่อีกรอบ...!");
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                    }
                }
            }
        }

        private void btMenu1_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu1.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu1.Text);
                item.SubItems.Add(menuPrice[0].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[0]).ToString();

            }
        }

        private void btMenu2_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu2.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu2.Text);
                item.SubItems.Add(menuPrice[1].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[1]).ToString();

            }
        }

        private void btMenu3_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu3.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu3.Text);
                item.SubItems.Add(menuPrice[2].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[2]).ToString();
            }
        }

        private void btMenu4_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu4.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu4.Text);
                item.SubItems.Add(menuPrice[3].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[3]).ToString();

            }
        }

        private void btMenu5_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu5.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu5.Text);
                item.SubItems.Add(menuPrice[4].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[4]).ToString();

            }
        }

        private void btMenu6_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu6.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu6.Text);
                item.SubItems.Add(menuPrice[5].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[5]).ToString();

            }
        }

        private void btMenu7_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu7.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu7.Text);
                item.SubItems.Add(menuPrice[6].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[6]).ToString();

            }
        }

        private void btMenu8_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu8.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu8.Text);
                item.SubItems.Add(menuPrice[7].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[7]).ToString();

            }
        }

        private void btMenu9_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu9.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu9.Text);
                item.SubItems.Add(menuPrice[8].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[8]).ToString();

            }
        }

        private void btMenu10_Click(object sender, EventArgs e)
        {
            // ตรวจสอบว่าชื่อบนปุ่มเป็นคำว่า Menu หรือไม่ หากใช่ไม่ต้องทำอะไร  
            //หากไม่ใช่ ให้เอาชื่อเมนูกับราคาไปใส่ใน lvOrderMenu แล้วบวกแต้มเพิ่ม และบวกรวมเป็นเงินเพิ่ม
            if (btMenu10.Text != "Menu")
            {
                //เอาชื่อราคาใส่ใน Listview
                ListViewItem item = new ListViewItem(btMenu10.Text);
                item.SubItems.Add(menuPrice[9].ToString());
                lvOrderMenu.Items.Add(item);

                //บวกแต้มเพิ่ม ต้องตรวจสอบก่อนว่าเป็นสมาชิกหรือไม่
                if (tbMemberName.Text != "(ชื่อสมาชิก)")
                {
                    lbMemberScore.Text = (int.Parse(lbMemberScore.Text) + 1).ToString();
                }

                //บวกราคาเพิ่ม
                lbOrderPay.Text = (float.Parse(lbOrderPay.Text) + menuPrice[9]).ToString();

            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //ตรวจสอบก่อนว่ารวมเป็นเงินมีค่า 0.00 หรือเปล่า
            if (lbOrderPay.Text == "0.00")
            {
                MessageBox.Show("เลือกเมนูที่จะสั่งด้วย...!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdMemberYes.Checked != true && rdMenberNo.Checked != true)
            {
                MessageBox.Show("เลือกสถานะการเป็นสมาชิกด้วย...!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdMemberYes.Checked == true && tbMemberName.Text == "(ชื่อสมาชิก)")
            {
                MessageBox.Show("กรุณาค้นหาสมาชิกด้วย!", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //ผ่านตรวจสอบมาได้ ต้องทำ 3 อย่าง
                //1.บันทึกลง order_tb  (IVSERT INTO ...)
                //2.บันทึกลง order_detail_tb (IVSERT INTO ...)
                //3.บันทึกแก้ไขแต้มคะแนนของสมาชิกที่ member_tb กรณี เป็นสมาชิก (UPDATE ... SET...)
                // สร้าง connection ไปยังฐานข้อมูล
                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); // ใช้กับ Insert/update/delete

                        //บันทึกลง order_tb ---------------------------------------------
                        string strSQL1 = "INSERT INTO order_tb (memberId, orderPay, createAt, updateAt) " +
                                         "VALUES (@memberId, @orderPay, @createAt, @updateAt); " +
                                         "SELECT CAST (SCOPE_IDENTITY() AS INT)";


                        // ตัวแปรเก็บ orderID
                        int orderId;

                        using (SqlCommand sqlCommand = new SqlCommand(strSQL1, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@memberId", SqlDbType.Int).Value = memberId;
                            sqlCommand.Parameters.Add("@orderPay", SqlDbType.Float).Value = float.Parse(lbOrderPay.Text);
                            sqlCommand.Parameters.Add("@createAt", SqlDbType.Date).Value = DateTime.Now;
                            sqlCommand.Parameters.Add("@updateAt", SqlDbType.Date).Value = DateTime.Now;

                            orderId = (int)sqlCommand.ExecuteScalar();
                        }

                        //บันทึกลง order_detail_tb ---------------------------------------------
                        foreach (ListViewItem item in lvOrderMenu.Items)
                        {
                            string strSQL2 = "INSERT INTO order_detail_tb (orderId, menuName, menuPrice) " +
                                         "VALUES (@orderId, @menuName, @menuPrice) ";

                            using (SqlCommand sqlCommand = new SqlCommand(strSQL2, sqlConnection, sqlTransaction))
                            {
                                //กำหนดค่าให้กับ SQL Parameter
                                sqlCommand.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
                                sqlCommand.Parameters.Add("@menuName", SqlDbType.NVarChar, 100).Value = item.SubItems[0].Text;
                                sqlCommand.Parameters.Add("@menuPrice", SqlDbType.Float).Value = float.Parse(item.SubItems[1].Text);

                                sqlCommand.ExecuteNonQuery();
                            }
                        }


                        //แก้ไข memberScore ที่ member_tb ---------------------------------------------
                        if (rdMemberYes.Checked == true)
                        {
                            string strSQL3 = "UPDATE member_tb SET memberScore=@memberScore WHERE memberId=@memberId";

                            using (SqlCommand sqlCommand = new SqlCommand(strSQL3, sqlConnection, sqlTransaction))
                            {
                                //กำหนดค่าให้กับ SQL Parameter
                                sqlCommand.Parameters.Add("@memberScore", SqlDbType.Int).Value = int.Parse(lbMemberScore.Text);
                                sqlCommand.Parameters.Add("@memberId", SqlDbType.Int).Value = memberId;

                                sqlCommand.ExecuteNonQuery();
                            }
                        }

                        //-----------------------------------------
                        sqlTransaction.Commit();
                        MessageBox.Show("บันทึกเรียบร้อยแล้ว...", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        resetForm();

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.StackTrace); //จะเห็น error ละเอียดกว่า
                    }
                }
            }
        }

        private void lvOrderMenu_ItemActivate(object sender, EventArgs e)
        {
            //เอารายการของแถวที่เลือกออกจาก lvOrderMenu
            //ก่อนเอาออก แต้มต้องลดลง 1 แต้ม และจำนวนเงินต้องลดลงด้วย
            // ตรวจสอบว่ามีรายการที่ถูกเลือกหรือไม่
            if (lvOrderMenu.SelectedItems.Count > 0)
            {
                // ได้รับรายการที่ถูกเลือก
                ListViewItem selectedItem = lvOrderMenu.SelectedItems[0];

                // ดึงราคาของเมนูที่ถูกเลือก
                // สมมติว่าราคาอยู่ใน SubItems[1] และเป็น string ที่สามารถแปลงเป็น float ได้
                if (float.TryParse(selectedItem.SubItems[1].Text, out float menuPriceToRemove))
                {
                    // ลดยอดรวมเงิน (lbOrderPay)
                    if (float.TryParse(lbOrderPay.Text, out float currentOrderPay))
                    {
                        lbOrderPay.Text = (currentOrderPay - menuPriceToRemove).ToString("0.00"); // Format ให้มีทศนิยม 2 ตำแหน่ง
                    }

                    // ลดแต้มสมาชิก (lbMemberScore) หากเป็นสมาชิก
                    // ตรวจสอบจาก tbMemberName.Text ว่าไม่ใช่ "(ชื่อสมาชิก)"
                    // และตรวจสอบว่าแต้มปัจจุบันมากกว่า 0 หรือไม่ก่อนลด
                    if (tbMemberName.Text != "(ชื่อสมาชิก)")
                    {
                        if (int.TryParse(lbMemberScore.Text, out int currentMemberScore))
                        {
                            // ลดแต้มลง 1 แต้ม แต่ไม่ให้ติดลบ
                            if (currentMemberScore > 0)
                            {
                                lbMemberScore.Text = (currentMemberScore - 1).ToString();
                            }
                        }
                    }

                    // ลบรายการออกจาก ListView
                    lvOrderMenu.Items.Remove(selectedItem);
                }
                else
                {
                    MessageBox.Show("ไม่สามารถอ่านข้อมูลราคาของเมนูที่เลือกได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
