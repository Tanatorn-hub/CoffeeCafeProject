using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CoffeeCafeProject
{
    public partial class FrmMenu : Form
    {

        //สร้างเมธอดแสดงข้อความเตือน
        private void showWarningMSG(string msg)
        {
            MessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        //สร้างตัวแปรเก็บรูปเป็น Binary/Byte Array เอาไว้บันทึกลง DB
        byte[] menuImage;

        //สร้างเมธอดแปลงรูปเป็น Binary/Byte Array
        private byte[] convertImageToByteArray(Image image, ImageFormat imageFormat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, imageFormat);
                return ms.ToArray();
            }
        }

        public FrmMenu()
        {
            InitializeComponent();
        }

        //เมธอด แปลง binary  เป็นรูป  
        private Image convertByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0)
            {
                return null;
            }
            try
            {
                using (MemoryStream ms = new MemoryStream(byteArrayIn))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (ArgumentException ex)
            {
                // อาจเกิดขึ้นถ้า byte array ไม่ใช่ข้อมูลรูปภาพที่ถูกต้อง
                Console.WriteLine("Error converting byte array to image: " + ex.Message);
                return null;
            }
        }


        private void getAllMenuToListView()
        {
            //กำหนด connect String เพื่อติดต่อไปยังฐานข้อมูล
            string connectionstring = @"server=DESKTOP-6F6L1NQ\SQLEXPRESS2022;Database=coffee_cafe_db;Trusted_Connection=True;";

            // สร้าง connection ไปยังฐานข้อมูล
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                try
                {
                    sqlConnection.Open(); //เปิดการเชื่อมต่อไปยังฐานข้อมูล
                    //การทำงานกับตารางในฐานข้อมูล (select,insert,update, delete)
                    // สร้างคำสั่ง SQL ในที่นี้คือ ดึงข้อมูลทั้งหมดจากตาราง menu_tb
                    string strSQL = "SELECT menuId, menuName, menuPrice, menuImage FROM menu_tb";

                    //จัดการให้ SQL ทำงาน
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQL, sqlConnection))
                    {
                        //เอาข้อมูลที่ได้จาก strSQL ซึ่งเป็นก้อนใน dataadapter มาทำให้เป็นตารางโดยใส่ไว้ใน datatable
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // ตั้งค่า ListView
                        lvShowAllMenu.Items.Clear();
                        lvShowAllMenu.Columns.Clear();
                        lvShowAllMenu.FullRowSelect = true;
                        lvShowAllMenu.View = View.Details;

                        //ตั้งค่าการแสดงรูปใน ListView
                        if (lvShowAllMenu.SmallImageList == null)
                        {
                            lvShowAllMenu.SmallImageList = new ImageList();
                            lvShowAllMenu.SmallImageList.ImageSize = new Size(50, 50);
                            lvShowAllMenu.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
                        }
                        lvShowAllMenu.SmallImageList.Images.Clear();

                        //กำหนดรายละเอียดของ column ใน ListView
                        lvShowAllMenu.Columns.Add("รูปเมนู", 100, HorizontalAlignment.Left);
                        lvShowAllMenu.Columns.Add("รหัสเมนู", 100, HorizontalAlignment.Left);
                        lvShowAllMenu.Columns.Add("ชื่อเมนู", 250, HorizontalAlignment.Left);
                        lvShowAllMenu.Columns.Add("ราคา", 80, HorizontalAlignment.Right);


                        // loop วนเข้าไปใน datatable 
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            ListViewItem item = new ListViewItem(); // สร้าง item เพื่อเก็บแต่ละข้อมูลในแต่ละรายการ
                            //เอารูปใส่ใน item
                            Image menuImage = null;
                            if (dataRow["menuImage"] != DBNull.Value)
                            {
                                byte[] imgByte = (byte[])dataRow["menuImage"];
                                //แปลงข้อมูลรูปจากฐานข้อมูลซึ่งเป็น binary ให้เป็นรูป
                                menuImage = convertByteArrayToImage(imgByte);
                            }
                            string imageKey = null;
                            if (menuImage != null)
                            {
                                imageKey = $"menu_{dataRow["menuId"]}";
                                lvShowAllMenu.SmallImageList.Images.Add(imageKey, menuImage);
                                item.ImageKey = imageKey;
                            }
                            else
                            {
                                item.ImageIndex = -1;
                            }
                            // เอาแต่ละรายการมาใส่ใน item
                            item.SubItems.Add(dataRow["menuId"].ToString());
                            item.SubItems.Add(dataRow["menuName"].ToString());
                            item.SubItems.Add(dataRow["menuPrice"].ToString());

                            //เอาข้อมูลลงใน item
                            lvShowAllMenu.Items.Add(item);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                }
            }
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            getAllMenuToListView(); // ดึงข้อมูล
            menuImage = null;
            pbMenuImage.Image = null;
            tbMenuId.Clear();
            tbMenuName.Clear();
            tbMenuPrice.Clear();
            btSave.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
        }

        private void btSelectMenuImage_Click(object sender, EventArgs e)
        {
            //เปิดไฟล์ dialog  ให้เลือกรูปโดยฟิลเตอร์เฉพาะไฟล์ jpg/png
            //แล้วก็แปลงเป็น Binary/Byte เก็บในตัวแปรเพื่อเอาไว้บันทึกลง DB
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"c:\";
            openFileDialog.Filter = "Image File (*.jpg;*.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //เอารูปที่เลือกไปแสดงที่ pbMenuImage
                pbMenuImage.Image = Image.FromFile(openFileDialog.FileName);
                // ตรวจสอบ format ของรูป แล้วส่งรูปไปแปลงเป็น Binary/Byte Array

                if (pbMenuImage.Image.RawFormat == ImageFormat.Jpeg)
                {
                    menuImage = convertImageToByteArray(pbMenuImage.Image, ImageFormat.Jpeg);
                }

                else
                {
                    menuImage = convertImageToByteArray(pbMenuImage.Image, ImageFormat.Png);
                }
            }

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //Validate UI แสดงข้อความเตือนด้วย เมื่อ  Validate แล้วก้เอาข้อมูลไปบันทึกลง DB
            //พอบันทึกเสร็จแสดงข้อความบอกผู้ใช้ และปิดหน้าจอ FrmProductCreate และกลับไปหน้าจอ FrmProductShow

            if (menuImage == null)
            {
                showWarningMSG("เลือกรูปด้วย...");
            }
            else if (tbMenuName.Text.Trim() == "")
            {
                showWarningMSG("ป้อนชื่อสินค้าด้วย...");
            }
            else if (tbMenuPrice.Text.Trim() == "")
            {
                showWarningMSG("ป้อนราคาด้วย...");
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

                        //ก่อนจะบนทึกให้ตรวจสอบก่อนว่าเมนูอยู่แล้ว 10 เมนู หรืยัง ถ้า10 เมนูแล้วให้แสดงข้อความเตือนผู้ใช้บันทึกไม่ได้
                        //ต้องเอาของเก่าออกก่อน

                        string countSQL = "SELECT COUNT(*) FROM menu_tb";
                        using (SqlCommand countCommand = new SqlCommand(countSQL, sqlConnection))
                        {
                            int rowCount = (int)countCommand.ExecuteScalar();
                            if (rowCount >= 10) // ใช้ >= 10 เพื่อป้องกันกรณีเกิน 10 ด้วย
                            {
                                showWarningMSG("เมนูมีได้แค่ 10 เมนู เท่านั้น หากจะเพิ่มให้ลบของเก่าออกก่อน");
                                return;

                            }
                        }

                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); // ใช้กับ Insert/update/delete

                        //คำสั่ง SQL
                        string strSQL = "INSERT INTO menu_tb (menuName,menuPrice,menuImage) " +
                                        "VALUES (@menuName,@menuPrice,@menuImage)";

                        // กำหนดค่าให้กับ SQL Parameter  และสั่งให้คำสั่ง SQL ทำงาน  แล้วมีข้อความแจ้งเมื่อทำงานเสร็จแล้ว
                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@menuName", SqlDbType.NVarChar, 100).Value = tbMenuName.Text;
                            sqlCommand.Parameters.Add("@menuPrice", SqlDbType.Float).Value = float.Parse(tbMenuPrice.Text);
                            sqlCommand.Parameters.Add("@menuImage", SqlDbType.Image).Value = menuImage;

                            //สั่งให้คำสั่ง sql ทำงาน
                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            //ข้อความแจ้ง
                            MessageBox.Show("บันทึกเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // อัปเดตListView
                            getAllMenuToListView(); // ดึงข้อมูล
                            pbMenuImage.Image = null;
                            tbMenuId.Clear();
                            tbMenuName.Clear();
                            tbMenuPrice.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด  กรุณาลองใหม่หรือติดต่อ IT : " + ex.Message);
                    }
                }

            }

        }

        private void tbMenuPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

            TextBox textBox = sender as TextBox;

            // อนุญาตให้กด Backspace ได้
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            // ถ้าเป็นตัวเลข 0-9 อนุญาตให้พิมพ์
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            // ถ้าเป็นจุด
            else if (e.KeyChar == '.')
            {
                // ถ้ายังไม่มีจุดในข้อความ อนุญาตให้พิมพ์
                if (!textBox.Text.Contains("."))
                {
                    e.Handled = false;
                }
                else
                {
                    // ถ้ามีจุดอยู่แล้ว ไม่ให้พิมพ์ซ้ำ
                    e.Handled = true;
                }
            }
            else
            {
                // ถ้าไม่ใช่ตัวเลขหรือจุด ไม่อนุญาตให้พิมพ์
                e.Handled = true;
            }
        }

        private void lvShowAllMenu_ItemActivate(object sender, EventArgs e)
        {
            //เอาข้อมูลของรายการที่เลือกไปแสดงที่หน้าจอ แล้วปุ่มบันทึกใช่ไม่ได้  แก้ไขกับลบใช้ได้
            tbMenuId.Text = lvShowAllMenu.SelectedItems[0].SubItems[1].Text;
            tbMenuName.Text = lvShowAllMenu.SelectedItems[0].SubItems[2].Text;
            tbMenuPrice.Text = lvShowAllMenu.SelectedItems[0].SubItems[3].Text;


            // เอารูปจาก ListView มาแสดงที่ PictureBox 
            var item = lvShowAllMenu.SelectedItems[0];
            if (!string.IsNullOrEmpty(item.ImageKey) && lvShowAllMenu.SmallImageList.Images.ContainsKey(item.ImageKey))
            {
                pbMenuImage.Image = lvShowAllMenu.SmallImageList.Images[item.ImageKey];
            }


            btSave.Enabled = false;
            btUpdate.Enabled = true;
            btDelete.Enabled = true;


        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            //ถามผู้ใช้ก่อนจะลบหรือไม่ มีให้เลือก Yes/No
            if (MessageBox.Show("ต้องการลบเมนูหรือไม่","ยืนยัน",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
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
                        string strSQL = "DELETE FROM menu_tb WHERE menuId=@menuId";

                        // กำหนดค่าให้กับ SQL Parameter  และสั่งให้คำสั่ง SQL ทำงาน  แล้วมีข้อความแจ้งเมื่อทำงานเสร็จแล้ว
                        using (SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@menuId", SqlDbType.Int).Value = int.Parse(tbMenuId.Text);

                            //สั่งให้คำสั่ง sql ทำงาน
                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            //ข้อความแจ้ง
                            MessageBox.Show("ลบเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // อัปเดตListView
                            getAllMenuToListView(); // ดึงข้อมูล
                            pbMenuImage.Image = null;
                            tbMenuId.Clear();
                            tbMenuName.Clear();
                            tbMenuPrice.Clear();
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
            if (menuImage == null)
            {
                showWarningMSG("เลือกรูปเมนูด้วย...");
            }
            else if (tbMenuName.Text.Length == 0)
            {
                showWarningMSG("ป้อนชื่อเมนูด้วย...");
            }
            else if (tbMenuPrice.Text.Length == 0)
            {
                showWarningMSG("ป้อนราคาเมนูด้วย...");
            }
            else
            {
                

                using (SqlConnection sqlConnection = new SqlConnection(ShareResource.connectionstring))
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                        string strSql = "UPDATE menu_tb SET " +
                                        "menuName = @menuName, " +
                                        "menuPrice = @menuPrice, " +
                                        "menuImage = @menuImage " +  // ตัดคอมมาออก
                                        "WHERE menuId = @menuId";

                        using (SqlCommand sqlCommand = new SqlCommand(strSql, sqlConnection, sqlTransaction))
                        {
                            sqlCommand.Parameters.Add("@menuName", SqlDbType.NVarChar, 100).Value = tbMenuName.Text;
                            sqlCommand.Parameters.Add("@menuPrice", SqlDbType.Float).Value = float.Parse(tbMenuPrice.Text);
                            sqlCommand.Parameters.Add("@menuImage", SqlDbType.Image).Value = menuImage;
                            sqlCommand.Parameters.Add("@menuId", SqlDbType.Int).Value = int.Parse(tbMenuId.Text);

                            sqlCommand.ExecuteNonQuery();
                            sqlTransaction.Commit();

                            MessageBox.Show("อัปเดตข้อมูลเรียบร้อยแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            getAllMenuToListView();
                            menuImage = null;
                            pbMenuImage.Image = null;
                            tbMenuId.Clear();
                            tbMenuName.Clear();
                            tbMenuPrice.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("พบข้อผิดพลาด กรุณาลองใหม่ หรือติดต่อ IT : " + ex.Message);
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            getAllMenuToListView(); // ดึงข้อมูล
            menuImage = null;
            pbMenuImage.Image = null;
            tbMenuId.Clear();
            tbMenuName.Clear();
            tbMenuPrice.Clear();
            btSave.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
        }
         
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

